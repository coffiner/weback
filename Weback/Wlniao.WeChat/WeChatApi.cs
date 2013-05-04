/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\WeChatApi.cs
        运 行 库：2.0.50727.1882
        代码功能：解析微信服务器的请求和控制输出

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using Wlniao.WeChat.BLL;

namespace Wlniao.WeChat
{
    public class WeChatApi : Api
    {
        public WeChatApi()
        {
            this.Load += new EventHandler(WeChatApi_Load);
        }

        void WeChatApi_Load(object sender, EventArgs e)
        {
            Response.Clear();
            if (string.IsNullOrEmpty(Request.QueryString["echostr"]))
            {
                string toUser = Request.QueryString["toUser"];
                string fromUser = Request.QueryString["fromUser"];
                string MsgType = Request.QueryString["MsgType"];
                string Event = Request.QueryString["Event"];
                string Content = Request.QueryString["Content"];
                string MsgId = "";
                try
                {
                    //声明一个XMLDoc文档对象，LOAD（）xml字符串
                    if (string.IsNullOrEmpty(Content))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(new StreamReader(Request.InputStream).ReadToEnd());
                        toUser = doc.GetElementsByTagName("ToUserName")[0].InnerText;
                        fromUser = doc.GetElementsByTagName("FromUserName")[0].InnerText;
                        MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText;
                        try
                        {
                            MsgId = doc.GetElementsByTagName("MsgId")[0].InnerText;
                        }
                        catch { }
                        try
                        {
                            Content = doc.GetElementsByTagName("Content")[0].InnerText;
                        }
                        catch { }
                        try
                        {
                            Event = doc.GetElementsByTagName("Event")[0].InnerText;
                            if (string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Event))
                            {
                                Content = Event;
                            }
                        }
                        catch { }
                    }

                    Wlniao.WeChat.Model.Fans fans = Wlniao.WeChat.BLL.Fans.Check(fromUser, Content);
                    switch (MsgType.ToLower())
                    {
                        case "event":
                        case "text":
                            Wlniao.WeChat.Model.Rules rule = null;
                            string msgArgs = "";
                            try
                            {
                                rule = Wlniao.WeChat.BLL.Rules.GetRule(Content, fromUser);
                            }
                            catch { }
                            if (rule != null)
                            {
                                if (Content.StartsWith(rule.GoOnCmd))
                                {
                                    msgArgs = Content.Substring(rule.GoOnCmd.Length).Trim();
                                }
                                Wlniao.WeChat.BLL.Fans.SetSession(fromUser, rule.GoOnCmd, rule.DoMethod, msgArgs, rule.CallBackText);
                                if (!string.IsNullOrEmpty(rule.DoMethod))
                                {
                                    rule.ReContent = RunMethod(rule.DoMethod, fromUser, toUser, Content, msgArgs);
                                }
                                else if (string.IsNullOrEmpty(rule.ReContent))
                                {
                                    string where = "RuleGuid='" + rule.Guid + "'";
                                    if (fans.AllowTest == 1)
                                    {
                                        where += " and (ContentStatus='normal' or ContentStatus='test')";
                                    }
                                    else
                                    {
                                        where += " and ContentStatus='normal'";
                                    }

                                    List<Wlniao.WeChat.Model.RuleContent> listAll = Wlniao.WeChat.Model.RuleContent.find(where + " order by LastStick desc").list();
                                    List<Wlniao.WeChat.Model.RuleContent> listText = Wlniao.WeChat.Model.RuleContent.find(where + " and ContentType='text' order by LastStick desc").list();
                                    List<Wlniao.WeChat.Model.RuleContent> listPicText = Wlniao.WeChat.Model.RuleContent.find(where + " and ContentType='pictext' order by LastStick desc").list();
                                    List<Wlniao.WeChat.Model.RuleContent> listMusic = Wlniao.WeChat.Model.RuleContent.find(where + " and ContentType='music' order by LastStick desc").list();
                                    if (rule.SendMode == "sendgroup" && listPicText != null && listPicText.Count > 0)
                                    {
                                        rule.ReContent = ResponsePicTextMsg(fromUser, toUser, listPicText);
                                    }
                                    else if (listAll.Count > 0)
                                    {
                                        int i = 0;
                                        if (rule.SendMode == "sendrandom")
                                        {
                                            i = new Random().Next(0, listAll.Count);
                                        }
                                        if (listAll[i].ContentType == "text")
                                        {
                                            rule.ReContent = listAll[i].TextContent;
                                            try
                                            {
                                                //更新推送次数
                                                listAll[i].PushCount++;
                                                listAll[i].update("PushCount");
                                            }
                                            catch { }
                                        }
                                        else if (listAll[i].ContentType == "music")
                                        {
                                            rule.ReContent = ResponseMusicMsg(fromUser, toUser, listAll[i].Title, listAll[i].TextContent, listAll[i].MusicUrl, listAll[i].MusicUrl);
                                            try
                                            {
                                                //更新推送次数
                                                listAll[i].PushCount++;
                                                listAll[i].update("PushCount");
                                            }
                                            catch { }
                                        }
                                        else if (listAll[i].ContentType == "pictext")
                                        {
                                            List<Wlniao.WeChat.Model.RuleContent> listTemp = new List<Model.RuleContent>();
                                            listTemp.Add(listAll[i]);
                                            rule.ReContent = ResponsePicTextMsg(fromUser, toUser, listTemp);
                                        }
                                    }
                                }
                                Response.Write(ResponseTextMsg(fromUser, toUser, rule.ReContent));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(fans.DoMethod) || fans.LastCmdTime < DateTools.GetNow().AddSeconds(0-BLL.Rules.SessionTimeOut))
                                {
                                    Response.Write(ResponseTextMsg(fromUser, toUser, RunDefaultMethod(fromUser, toUser, Content)));
                                }
                                else
                                {
                                    Response.Write(ResponseTextMsg(fromUser, toUser, RunMethod(fans.DoMethod, fromUser, toUser, (fans.GoOnCmd + BLL.Rules.Separation[0] + Content).Trim(), Content)));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch { }
            }
            else if (CheckSignature(Context))
            {
                Response.Write(Request.QueryString["echostr"]);
            }
            Response.End();
        }

        private static string _WeChatToken = null;
        private static string WeChatToken
        {
            get
            {
                if (_WeChatToken == null)
                {
                    _WeChatToken = System.Data.KvTableUtil.GetString("WeChatToken");
                    if (string.IsNullOrEmpty(_WeChatToken))
                    {
                        _WeChatToken = System.Configuration.ConfigurationManager.AppSettings["WeChatToken"];
                    }
                    if (string.IsNullOrEmpty(_WeChatToken))
                    {
                        _WeChatToken = "";
                    }
                }
                return _WeChatToken;
            }
        }
        /// <summary>
        /// 根据参数和密码生成签名字符串
        /// </summary>
        /// <param name="parameters">API参数</param>
        /// <param name="secret">密码</param>
        /// <returns>签名字符串</returns>
        internal static bool CheckSignature(HttpContext context)
        {
            if (string.IsNullOrEmpty(WeChatToken))
            {
                return true;
            }
            else
            {
                string[] arr = { WeChatToken, context.Request.QueryString["timestamp"], context.Request.QueryString["nonce"] };
                Array.Sort(arr);     //字典排序
                return System.Encryptor.GetSHA1(string.Join("", arr)).ToLower() == context.Request.QueryString["signature"];
            }
        }
        /// <summary>
        /// 回复文本内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public static string ResponseTextMsg(string to, string from, string content)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(content);
                if(doc.InnerText.Length<50){
                    doc = null;
                }
            }
            catch { doc = null; }
            if (doc == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<xml>");
                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", to);
                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", from);
                sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
                sb.AppendFormat("<FuncFlag>0</FuncFlag>");
                sb.AppendFormat("</xml>");
                return sb.ToString();
            }
            else
            {
                return doc.InnerXml;
            }
        }
        /// <summary>
        /// 回复图文内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public static string ResponsePicTextMsg(string to, string from, List<Model.RuleContent> articles)
        {
            if (articles == null)
            {
                articles = new List<Model.RuleContent>();
            }
            int count = 0;
            StringBuilder sbItems = new StringBuilder();
            foreach (Model.RuleContent article in articles)
            {
                try
                {
                    if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.PicUrl) || string.IsNullOrEmpty(article.TextContent))
                    {
                        continue;
                    }
                    StringBuilder sbTemp = new StringBuilder();
                    sbTemp.AppendFormat("<item>");
                    sbTemp.AppendFormat("   <Title><![CDATA[{0}]]></Title>", article.Title);
                    sbTemp.AppendFormat("   <Description><![CDATA[{0}]]></Description>", article.TextContent);
                    string urlTemp=article.PicUrl;
                    if (count > 0)
                    {
                        urlTemp = string.IsNullOrEmpty(article.ThumbPicUrl) ? article.PicUrl : article.ThumbPicUrl;
                    }
                    sbTemp.AppendFormat("   <PicUrl><![CDATA[{0}]]></PicUrl>",( !string.IsNullOrEmpty(urlTemp)&& urlTemp.Contains("http://")) ? urlTemp : ApiUrl + urlTemp);
                    sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", article.LinkUrl);
                    sbTemp.AppendFormat("   <FuncFlag>0</FuncFlag>");
                    sbTemp.AppendFormat("</item>");
                    sbItems.Append(sbTemp.ToString());
                    count++;
                    //更新推送次数
                    article.PushCount++;
                    article.update("PushCount");
                    if (count == 9)
                    {
                        break;
                    }
                }
                catch { }
            }


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", to);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", from);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}></ArticleCount>", count);
            sb.AppendFormat("<Articles>");
            sb.AppendFormat(sbItems.ToString());
            sb.AppendFormat("</Articles>");
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
        /// <summary>
        /// 回复音乐内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述信息</param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <returns>生成的输出文本</returns>
        public static string ResponseMusicMsg(string to, string from, string title, string description, string musicurl, string hqmusicurl)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", to);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", from);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
            sb.AppendFormat("<MsgType><![CDATA[music]]></MsgType>");
            sb.AppendFormat("<Music>");
            sb.AppendFormat("   <Title><![CDATA[{0}]]></Title>", title);
            sb.AppendFormat("   <Description><![CDATA[{0}]]></Description>", description);
            sb.AppendFormat("   <MusicUrl><![CDATA[{0}]]></MusicUrl>",( !string.IsNullOrEmpty(musicurl)&& musicurl.Contains("http://")) ? musicurl : ApiUrl + musicurl);
            sb.AppendFormat("   <HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>",( !string.IsNullOrEmpty(hqmusicurl)&& hqmusicurl.Contains("http://")) ? hqmusicurl : ApiUrl + hqmusicurl);
            sb.AppendFormat("   <FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</Music>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
    }
}
