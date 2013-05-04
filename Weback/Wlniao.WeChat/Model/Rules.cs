/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Model\Rules.cs
        运 行 库：2.0.50727.1882
        代码功能：API处理规则实体类定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.WeChat.Model
{
    /// <summary>
    /// 规则
    /// </summary>
    public class Rules : ObjectBase<Rules>
    {
        private string _Guid;
        /// <summary>
        /// 所绑定的用户Guid
        /// </summary>
        [Column(Name = "StrGuid", Length = 50), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _RuleName;
        /// <summary>
        /// 规则名称
        /// </summary>
        [Column(Length = 120)]
        public string RuleName
        {
            get { return _RuleName; }
            set { _RuleName = value; }
        }
        private string _DoMethod;
        /// <summary>
        /// 需要执行的方法（为空则直接回复内容）
        /// </summary>
        [Column(Length = 50)]
        public string DoMethod
        {
            get { return _DoMethod; }
            set { _DoMethod = value; }
        }
        private string _ReContent;
        /// <summary>
        /// 回复内容
        /// </summary>
        [LongText]
        public string ReContent
        {
            get { return _ReContent; }
            set { _ReContent = value; }
        }
        private string _RuleHelp;
        /// <summary>
        /// 使用帮助
        /// </summary>
        [LongText]
        public string RuleHelp
        {
            get { return _RuleHelp; }
            set { _RuleHelp = value; }
        }
        private string _CallBackText;
        /// <summary>
        /// 回调内容
        /// </summary>
        [Column(Length = 150)]
        public string CallBackText
        {
            get { return _CallBackText; }
            set { _CallBackText = value; }
        }
        private string _SendMode;
        /// <summary>
        /// 自动回复模式 SendNew（最新）,SendRandom(随机),SendGroup（组合仅图文）
        /// </summary>
        [Column(Length = 20)]
        public string SendMode
        {
            get { return _SendMode; }
            set { _SendMode = value; }
        }

    }
    /// <summary>
    /// 规则命令
    /// </summary>
    public class RuleCode : ObjectBase<RuleCode>
    {
        private string _Guid;
        /// <summary>
        /// 所绑定的用户Guid
        /// </summary>
        [Column(Name = "StrGuid", Length = 50), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _RuleGuid;
        /// <summary>
        /// 所属规则
        /// </summary>
        [Column(Length = 50)]
        public string RuleGuid
        {
            get { return _RuleGuid; }
            set { _RuleGuid = value; }
        }
        private string _Code;
        /// <summary>
        /// 编码
        /// </summary>
        [Column(Length = 100)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        private int _HitCount;
        /// <summary>
        /// 命中次数
        /// </summary>
        public int HitCount
        {
            get { return _HitCount; }
            set { _HitCount = value; }
        }
        private string _HashCode;
        /// <summary>
        /// 哈希值
        /// </summary>
        [Column(Length = 50)]
        public string HashCode
        {
            get { return _HashCode; }
            set { _HashCode = value; }
        }
        private string _SepType;
        /// <summary>
        /// 分隔符
        /// </summary>
        [Column(Length = 10)]
        public string SepType
        {
            get { return _SepType; }
            set { _SepType = value; }
        }
        private string _Status;
        /// <summary>
        /// 规则状态 normal（正常），close（关闭），test（测试中）
        /// </summary>
        [Column(Length=20)]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        

    }
    /// <summary>
    /// 规则内容
    /// </summary>
    public class RuleContent : ObjectBase<RuleContent>
    {
        private string _Guid;
        /// <summary>
        /// 所绑定的用户Guid
        /// </summary>
        [Column(Name = "StrGuid", Length = 50), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _RuleGuid;
        /// <summary>
        /// 所属规则
        /// </summary>
        [Column(Length = 50)]
        public string RuleGuid
        {
            get { return _RuleGuid; }
            set { _RuleGuid = value; }
        }
        private string _ContentType;
        /// <summary>
        /// 内容类型 text（文本）,pictext(图文) ,music（音乐）
        /// </summary>
        [Column(Length = 30)]
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }
        private string _Title;
        /// <summary>
        /// 标题
        /// </summary>
        [Column(Length = 180)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _LinkUrl;
        /// <summary>
        /// 外链地址
        /// </summary>
        [Column(Length = 120)]
        public string LinkUrl
        {
            get { return _LinkUrl; }
            set { _LinkUrl = value; }
        }
        private string _PicUrl;
        /// <summary>
        /// 图片外链地址
        /// </summary>
        [Column(Length = 120)]
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        private string _ThumbPicUrl;
        /// <summary>
        /// 小图外链地址
        /// </summary>
        [Column(Length = 120)]
        public string ThumbPicUrl
        {
            get { return _ThumbPicUrl; }
            set { _ThumbPicUrl = value; }
        }
        private string _MusicUrl;
        /// <summary>
        /// 声音文件外链地址
        /// </summary>
        [Column(Length = 120)]
        public string MusicUrl
        {
            get { return _MusicUrl; }
            set { _MusicUrl = value; }
        }
        private string _TextContent;
        /// <summary>
        /// 文本内容
        /// </summary>
        [LongText]
        public string TextContent
        {
            get { return _TextContent; }
            set { _TextContent = value; }
        }
        private int _PushCount;
        /// <summary>
        /// 推送次数
        /// </summary>
        public int PushCount
        {
            get { return _PushCount; }
            set { _PushCount = value; }
        }
        private string _ContentStatus;
        /// <summary>
        /// 规则状态 normal（正常），close（关闭），test（测试中）
        /// </summary>
        [Column(Length = 20)]
        public string ContentStatus
        {
            get { return _ContentStatus; }
            set { _ContentStatus = value; }
        }

        private DateTime _LastStick;
        /// <summary>
        /// 最后置顶的时间
        /// </summary>
        public DateTime LastStick
        {
            get { return _LastStick; }
            set { _LastStick = value; }
        }

    }
}
