using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.Extend
{
    public class Base : Wlniao.WeChat.ActionBase
    {
        /// <summary>
        /// 定以后将不再调用Wlniao.WeChat.Wlniao中的Default方法
        /// </summary>
        /// <returns></returns>
        public string Default()
        {
            return System.Data.KvTableUtil.GetString("NoMessage");
        }
    }
}
