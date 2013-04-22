/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Api.cs
        运 行 库：2.0.50727.1882
        代码功能：Api程序基础类

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
    public class Api : System.Web.UI.Page
    {
        public string RunMethod(string method, String cmdForm, String cmdText, String msgText)
        {
            String content = "";             //方法执行结果
            try
            {
                String classname = method.Substring(0, method.LastIndexOf('.'));        //获取类名
                String methodname = method.Substring(method.LastIndexOf('.') + 1);      //获取方法名
                Type type = null;
                try
                {
                    type = Type.GetType(String.Format("Wlniao.WeChat.Extend.{0}, Wlniao.WeChat.Extend", classname), false, true);
                    if (type == null)
                    {
                        type = Type.GetType(String.Format("Wlniao.WeChat.Method.{0}, Wlniao.WeChat", classname), false, true);
                    }
                }
                catch{}
                try
                {
                    ActionBase action = (ActionBase)Activator.CreateInstance(type);
                    action.FromId = cmdForm.TrimStart().TrimEnd();
                    action.Cmd = cmdText.TrimStart().TrimEnd();
                    action.MsgContent = msgText.TrimStart().TrimEnd();
                    content = type.InvokeMember(methodname, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, action, new object[] { }).ToString();
                }
                catch{}
            }
            catch { }
            return content;
        }
        public string RunDefaultMethod(String cmdForm, String msgText)
        {
            String content = "";
            Type type = null;
            try
            {
                type = Type.GetType("Wlniao.WeChat.Extend.Base, Wlniao.WeChat.Extend", false, true);
                if (type == null)
                {
                    type = Type.GetType("Wlniao.WeChat.Method.Wlniao, Wlniao.WeChat", false, true);
                }
            }
            catch { }
            try
            {
                ActionBase action = (ActionBase)Activator.CreateInstance(type);
                action.FromId = cmdForm;
                action.Cmd = "";
                action.MsgContent = msgText;
                content = type.InvokeMember("Default", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, action, new object[] { }).ToString();
            }
            catch { }
            return content;
        }
    }
}
