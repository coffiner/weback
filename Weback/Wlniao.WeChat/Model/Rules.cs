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
        [Column(Name = "StrGuid"), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _RuleName;
        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName
        {
            get { return _RuleName; }
            set { _RuleName = value; }
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
        public string RuleHelp
        {
            get { return _RuleHelp; }
            set { _RuleHelp = value; }
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
        [Column(Name = "StrGuid"), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _RuleGuid;
        /// <summary>
        /// 所属规则
        /// </summary>
        public string RuleGuid
        {
            get { return _RuleGuid; }
            set { _RuleGuid = value; }
        }
        private string _Code;
        /// <summary>
        /// 编码
        /// </summary>
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
        public string HashCode
        {
            get { return _HashCode; }
            set { _HashCode = value; }
        }
        private string _SepType;
        /// <summary>
        /// 分隔符
        /// </summary>
        public string SepType
        {
            get { return _SepType; }
            set { _SepType = value; }
        }
        

    }
}
