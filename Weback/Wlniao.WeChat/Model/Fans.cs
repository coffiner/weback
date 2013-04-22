/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Model\Fans.cs
        运 行 库：2.0.50727.1882
        代码功能：订阅者信息实体类定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.WeChat.Model
{
    public class Fans : ObjectBase<Fans>
    {
        private string _Guid;
        /// <summary>
        /// 所绑定的用户Guid
        /// </summary>
        [Column(Name = "StrGuid"), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _NickName;
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }
        private string _Sid;
        /// <summary>
        /// 所绑定的用户Sid
        /// </summary>
        public string Sid
        {
            get { return _Sid; }
            set { _Sid = value; }
        }

        private string _WeChatOpenId;
        /// <summary>
        /// 微信OpenId
        /// </summary>
        [Unique("微信OpenId不唯一")]
        public string WeChatOpenId
        {
            get { return _WeChatOpenId; }
            set { _WeChatOpenId = value; }
        }
        private DateTime _BindTiem;
        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime BindTiem
        {
            get { return _BindTiem; }
            set { _BindTiem = value; }
        }
        /// <summary>
        /// 订阅是否有效
        /// </summary>
        private int _Subscribe;
        /// <summary>
        /// 订阅是否有效
        /// </summary>
        public int Subscribe
        {
            get { return _Subscribe; }
            set { _Subscribe = value; }
        }
        private DateTime _SubscribeTime;
        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime SubscribeTime
        {
            get { return _SubscribeTime; }
            set { _SubscribeTime = value; }
        }
        private int _IsNewFans;
        /// <summary>
        /// 是否新粉丝
        /// </summary>
        public int IsNewFans
        {
            get { return _IsNewFans; }
            set { _IsNewFans = value; }
        }
        private string _GoOnCmd;
        /// <summary>
        /// 正在继续的命令符
        /// </summary>
        public string GoOnCmd
        {
            get { return _GoOnCmd; }
            set { _GoOnCmd = value; }
        }
        private string _DoMethod;
        /// <summary>
        /// 需要执行的方法（为空则直接回复内容）
        /// </summary>
        public string DoMethod
        {
            get { return _DoMethod; }
            set { _DoMethod = value; }
        }
        private string _CmdContent;
        /// <summary>
        /// 除去命令符后的操作内容
        /// </summary>
        public string CmdContent
        {
            get { return _CmdContent; }
            set { _CmdContent = value; }
        }
        private string _CallBackText;
        /// <summary>
        /// 回调内容
        /// </summary>
        public string CallBackText
        {
            get { return _CallBackText; }
            set { _CallBackText = value; }
        }
        private string _LastArgs;
        /// <summary>
        /// 最后接收的参数
        /// </summary>
        [LongText]
        public string LastArgs
        {
            get { return _LastArgs; }
            set { _LastArgs = value; }
        }
        private DateTime _LastVisit;
        /// <summary>
        /// 最后来访时间
        /// </summary>
        public DateTime LastVisit
        {
            get { return _LastVisit; }
            set { _LastVisit = value; }
        }
        private DateTime _LastCmdTime;
        /// <summary>
        /// 最后记录的命令时间
        /// </summary>
        public DateTime LastCmdTime
        {
            get { return _LastCmdTime; }
            set { _LastCmdTime = value; }
        }

    }
}
