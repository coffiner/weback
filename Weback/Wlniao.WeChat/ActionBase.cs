/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\ActionBase.cs
        运 行 库：2.0.50727.1882
        代码功能：执行API请求的基类

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Wlniao.WeChat
{
    /// <summary>
    /// 执行API请求的基类
    /// </summary>
    public abstract class ActionBase
    {
        private String fromId = "";
        private String cmd = "";
        private String msgContent;
        private String cmdContent;
        private Boolean onSession;
        /// <summary>
        /// 消息来源ID
        /// </summary>
        public String FromId
        {
            get { return fromId; }
            set { fromId = value; }
        }
        /// <summary>
        /// 执行的命令符
        /// </summary>
        public String Cmd
        {
            get { return cmd; }
            set { cmd = value; }
        }
        /// <summary>
        /// 整个消息内容
        /// </summary>
        public String MsgContent
        {
            get { return msgContent; }
            set { msgContent = value; }
        }
        /// <summary>
        /// 除去命令符后的操作内容
        /// </summary>
        public String CmdContent
        {
            get { return msgContent.Replace(cmd, "").Trim(); }
        }
        /// <summary>
        /// 标记当前流程执行成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected string FlagSuccess(string msg = "")
        {
            string temp = GotoSession();
            if (string.IsNullOrEmpty(msg))
            {
                msg = temp;
            }
            else if (!string.IsNullOrEmpty(temp))
            {
                msg += @"
" + temp;
            }
            return msg;
        }
        /// <summary>
        /// 跳转到一个新的流程
        /// </summary>
        /// <returns></returns>
        private string GotoSession()
        {
            Model.Fans fans = BLL.Fans.GetBy("WeChatOpenId", FromId);
            if (!string.IsNullOrEmpty(fans.CallBackText))
            {
                Model.Rules cmd = null;
                string cmdstr = "";
                try
                {
                    cmdstr = fans.CallBackText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    cmd = BLL.Rules.GetRuleByCode(cmdstr);
                }
                catch { }
                try
                {
                    BLL.Fans.SetSession(FromId, cmdstr, cmd.DoMethod, fans.CallBackText, cmd.CallBackText);
                    if (!string.IsNullOrEmpty(cmd.DoMethod))
                    {
                        String classname = cmd.DoMethod.Substring(0, cmd.DoMethod.LastIndexOf('.'));        //获取类名
                        String methodname = cmd.DoMethod.Substring(cmd.DoMethod.LastIndexOf('.') + 1);      //获取方法名
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
                        action.Cmd = cmdstr;
                        action.MsgContent = fans.CallBackText;
                        return type.InvokeMember(methodname, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.IgnoreCase, null, action, new object[] { }).ToString();
                    }
                    else
                    {
                        return cmd.ReContent;
                    }
                }
                catch { }
            }
            return "";
        }
    }
    
}
