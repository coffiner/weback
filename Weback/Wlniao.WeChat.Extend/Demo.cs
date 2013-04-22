using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.Extend
{
    public class Demo : Wlniao.WeChat.ActionBase
    {
        public string Chs2Pinyin()
        {
            string msg = strUtil.Chs2Pinyin(CmdContent);    //将用户输入内容转换成拼音
            return FlagSuccess(msg);                      
        }
    }
}
