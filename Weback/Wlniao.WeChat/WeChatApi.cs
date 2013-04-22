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
                bool fromPost = true;
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
                        fromPost = false;
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
                            string cmdstr = "";
                            Wlniao.WeChat.Model.Rules cmd = null;
                            try
                            {
                                cmdstr = Content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                cmd = Wlniao.WeChat.BLL.Rules.GetRuleByCode(cmdstr);
                            }
                            catch { }
                            if (cmd != null)
                            {
                                Wlniao.WeChat.BLL.Fans.NewSession(fromUser, cmdstr, cmd.DoMethod, cmdstr, cmd.CallBackText);
                                if (!string.IsNullOrEmpty(cmd.DoMethod))
                                {
                                    cmd.ReContent = RunMethod(cmd.DoMethod, fromUser, cmdstr, Content);
                                }
                                Response.Write(ResponseTextMsg(fromUser, toUser, cmd.ReContent));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(fans.DoMethod) || fans.LastCmdTime < DateTools.GetNow().AddMinutes(-3))
                                {
                                    Response.Write(ResponseTextMsg(fromUser, toUser, RunDefaultMethod(fromUser, Content)));
                                }
                                else
                                {
                                    Response.Write(ResponseTextMsg(fromUser, toUser, RunMethod(fans.DoMethod, fromUser, fans.GoOnCmd, fans.CmdContent.TrimEnd() + " " + Content)));
                                }
                            }
                            break;
                        //case "event":
                        //    cmd = Wlniao.WeChat.BLL.Rules.GetRuleByCode(Event);
                        //    if (cmd != null)
                        //    {
                        //        if (!string.IsNullOrEmpty(cmd.DoMethod))
                        //        {
                        //            cmd.ReContent = RunMethod(cmd.DoMethod, fromUser, Event, Content);
                        //        }
                        //        Response.Write(ResponseTextMsg(fromUser, toUser, cmd.ReContent));
                        //    }
                        //    break;
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
    }
}
