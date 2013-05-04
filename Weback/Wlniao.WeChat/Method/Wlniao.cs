/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Method\Wlniao.cs
        运 行 库：2.0.50727.1882
        代码功能：系统常用方法

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using BLL = Wlniao.WeChat.BLL;
namespace Wlniao.WeChat.Method
{
    public class Wlniao : ActionBase
    {
        /// <summary>
        /// 你可以定义自己的Default方法（命名空间：Wlniao.WeChat.Extend.Base）
        /// </summary>
        /// <returns></returns>
        public string Default()
        {
            Model.Fans fans = BLL.Fans.GetBy("WeChatOpenId", FromId);
            Model.Rules rule = null;
            string msgtext = System.Data.KvTableUtil.GetString("NoMessage");
            try
            {
                rule = BLL.Rules.GetRule(msgtext, FromId);
            }
            catch { }
            try
            {
                BLL.Fans.SetSession(FromId, "", rule.DoMethod, MsgArgs, rule.CallBackText);
                if (!string.IsNullOrEmpty(rule.DoMethod))
                {
                    String classname = rule.DoMethod.Substring(0, rule.DoMethod.LastIndexOf('.'));        //获取类名
                    String methodname = rule.DoMethod.Substring(rule.DoMethod.LastIndexOf('.') + 1);      //获取方法名
                    Type type = null;
                    try
                    {
                        type = Type.GetType(String.Format("Wlniao.WeChat.Extend.{0}, Wlniao.WeChat.Extend", classname), false, true);
                        if (type == null)
                        {
                            type = Type.GetType(String.Format("Wlniao.WeChat.Method.{0}, Wlniao.WeChat", classname), false, true);
                        }
                    }
                    catch { }
                    ActionBase action = (ActionBase)Activator.CreateInstance(type);
                    action.FromId = FromId;
                    action.MsgText = MsgText;
                    action.MsgArgs = MsgArgs; ;
                    return type.InvokeMember(methodname, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.IgnoreCase, null, action, new object[] { }).ToString();
                }
                else
                {
                    return rule.ReContent;
                }
            }
            catch { }
            return "";
        }
        /// <summary>
        /// 空函数
        /// </summary>
        /// <returns></returns>
        public string Empty()
        {
            return FlagSuccess();
        }
        /// <summary>
        /// 被用户关注时触发的事件
        /// </summary>
        /// <returns></returns>
        public string Subscribe()
        {
            BLL.Fans.Subscribe(FromId);
            return FlagSuccess();
        }
        /// <summary>
        /// 用户取消关注时触发的事件
        /// </summary>
        /// <returns></returns>
        public string UnSubscribe()
        {
            BLL.Fans.UnSubscribe(FromId);
            return FlagSuccess();
        }
        /// <summary>
        /// 设置用户昵称
        /// </summary>
        /// <returns></returns>
        public string SetNickName()
        {
            string[] contents = MsgText.Split(BLL.Rules.Separation, StringSplitOptions.RemoveEmptyEntries);
            if (contents.Length == 1 || string.IsNullOrEmpty(contents[0]))
            {
                BLL.Fans.SetGoOnCmd(FromId, "");
                return @"亲！你好
可以告诉我您的名字吗？";
            }
            else if (contents.Length == 2)
            {
                BLL.Fans.SetGoOnCmd(FromId, contents[1]);
                return "您叫" + contents[1] + "，对吗？";
            }
            else if (contents.Length == 3 && !(contents[2].Contains("不") || MsgArgs.Contains("no")))
            {
                BLL.Fans.SetNickName(FromId, contents[2]);
            }
            return FlagSuccess();
        }

        public string API()
        {
            string msg = "";
            try
            {
                string url = BLL.Fans.GetBy("WeChatOpenId", FromId).CallBackText;
                if (!url.Contains("?"))
                {
                    url += "?";
                }
                url += "openid=" + FromId + "&fromid=" + ToId + "&text=" + MsgArgs;
                msg = System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(url));
                //if (string.IsNullOrEmpty(msg))
                //{
                //    BLL.Fans.SetSession(FromId, Cmd, "Wlniao.API", "", url);
                //}
            }
            catch { }
            return msg;
        }

        public string MPAPI()
        {
            string msg = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<xml>");
                sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", ToId);
                sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromId);
                sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", MsgText);
                sb.AppendFormat("</xml>");

                string url = BLL.Fans.GetBy("WeChatOpenId", FromId).CallBackText;
                byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webRequest.Method = "POST";
                //webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                msg = php.ReadToEnd();
            }
            catch { msg = ""; }
            return msg;
        }


    }
}
