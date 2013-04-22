/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\BLL\Fans.cs
        运 行 库：2.0.50727.1882
        代码功能：订阅者信息存储方法定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.BLL
{
    public class Fans : System.ORM.CommonBase<Model.Fans>
    {
        /// <summary>
        /// 检查粉丝帐号（已存在则直接返回粉丝信息，不存在则添加新粉丝）
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans Check(string openid, string text = "")
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans == null)
            {
                fans = new Model.Fans();
                fans.Guid = Guid.NewGuid().ToString();
                fans.WeChatOpenId = openid;
                fans.Subscribe = 1;
                fans.SubscribeTime = DateTools.GetNow();
                fans.IsNewFans = 1;
                fans.LastArgs = text;
                fans.LastVisit = DateTools.GetNow();
                fans.insert();
            }
            else
            {
                fans.LastArgs = text;
                fans.LastVisit = DateTools.GetNow();
                fans.update(new string[] { "LastArgs", "LastVisit" });
            }
            return fans;
        }
        /// <summary>
        /// 开始新的会话
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans NewSession(string openid, string GoOnCmd, string DoMethod, string CmdContent, string CallBackText)
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans != null)
            {
                fans.GoOnCmd = GoOnCmd;
                fans.DoMethod = DoMethod;
                fans.CmdContent = CmdContent;
                fans.CallBackText = CallBackText;
                fans.LastCmdTime = DateTools.GetNow();
                fans.update(new string[] { "GoOnCmd", "DoMethod", "CmdContent", "CallBackText", "LastCmdTime" });
            }
            return fans;
        }
        /// <summary>
        /// 设置会话
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans SetSession(string openid, string GoOnCmd, string DoMethod, string CmdContent, string CallBackText)
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans != null)
            {
                fans.GoOnCmd = GoOnCmd;
                fans.DoMethod = DoMethod;
                fans.CmdContent = CmdContent;
                fans.CallBackText = CallBackText;
                fans.LastCmdTime = DateTools.GetNow();
                fans.update(new string[] { "GoOnCmd", "DoMethod", "CmdContent", "CallBackText", "LastCmdTime" });
            }
            return fans;
        }
        /// <summary>
        /// 设置会话参数
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans SetCmdContent(string openid, string CmdContent)
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans != null)
            {
                fans.CmdContent = CmdContent;
                fans.LastCmdTime = DateTools.GetNow();
                fans.update(new string[] { "CmdContent", "LastCmdTime" });
            }
            return fans;
        }
        /// <summary>
        /// 设置昵称
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans SetNickName(string openid,string nickname)
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans != null)
            {
                fans.NickName = nickname;
                fans.LastCmdTime = DateTools.GetNow();
                fans.update(new string[] { "NickName"});
            }
            return fans;
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans Subscribe(string openid)
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans == null)
            {
                fans = new Model.Fans();
                fans.Guid = Guid.NewGuid().ToString();
                fans.WeChatOpenId = openid;
                fans.Subscribe = 1;
                fans.LastVisit = DateTools.GetNow();
                fans.insert();
            }
            else
            {
                fans.Subscribe = 1;
                fans.LastVisit = DateTools.GetNow();
                fans.update(new string[] { "Subscribe", "LastVisit" });
            }
            return fans;
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static Model.Fans UnSubscribe(string openid)
        {
            Model.Fans fans = GetBy("WeChatOpenId", openid);
            if (fans != null)
            {
                fans.Subscribe = 0;
                fans.LastVisit = DateTools.GetNow();
                fans.update(new string[] { "Subscribe", "LastVisit" });
            }
            return fans;
        }
        
    }
}
