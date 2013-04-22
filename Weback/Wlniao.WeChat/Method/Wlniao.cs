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
            return FlagSuccess(System.Data.KvTableUtil.GetString("NoMessage"));
        }
        /// <summary>
        /// 被用户关注时触发的事件
        /// </summary>
        /// <returns></returns>
        public string Subscribe()
        {
            BLL.Fans.Subscribe(FromId);
            return FlagSuccess(System.Data.KvTableUtil.GetString("Subscribe"));
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
            string[] contents = CmdContent.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (contents.Length == 0 || string.IsNullOrEmpty(contents[0]))
            {
                BLL.Fans.SetCmdContent(FromId, "");
                return @"亲！你好
可以告诉我您的名字吗？";
            }
            else if (contents.Length < 2)
            {
                BLL.Fans.SetCmdContent(FromId, contents[0]);
                return "您叫" + contents[0] + "，对吗？";
            }
            else if (contents.Length == 2 && !(contents[1].Contains("不") || contents[1].Contains("no")))
            {
                BLL.Fans.SetNickName(FromId, contents[0]);
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
                msg = System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(url + CmdContent));
                //if (string.IsNullOrEmpty(msg))
                //{
                //    BLL.Fans.SetSession(FromId, Cmd, "Wlniao.API", "", url);
                //}
            }
            catch { }
            return msg;
        }


    }
}
