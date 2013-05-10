/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Model\Menus.cs
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
    public class Menus : ObjectBase<Menus>
    {
        private string _MenuPath;
        /// <summary>
        /// 菜单路径（"button即第一级菜单"）
        /// </summary>
        [Column(Length = 50)]
        public string MenuPath
        {
            get { return _MenuPath; }
            set { _MenuPath = value; }
        }
        private string _MenuName;
        /// <summary>
        /// 按钮描述（既按钮名字，不超过16个字节，子菜单不超过40个字节）
        /// </summary>
        [Column(Length = 50)]
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }
        private string _MenuType;
        /// <summary>
        /// 按钮类型，目前有click类型
        /// </summary>
        [Column(Length = 30)]
        public string MenuType
        {
            get { return _MenuType; }
            set { _MenuType = value; }
        }
        private string _MenuKey;
        /// <summary>
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        [Column(Length = 200)]
        public string MenuKey
        {
            get { return _MenuKey; }
            set { _MenuKey = value; }
        }
    }
}
