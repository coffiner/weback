/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Method\Demo.cs
        运 行 库：2.0.50727.1882
        代码功能：自定义方法示例

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.Method
{
    public class Demo : Wlniao
    {

        #region 演示方法
        public string Hello()
        {
            string[] contents = MsgText.Split(BLL.Rules.Separation, StringSplitOptions.RemoveEmptyEntries);
            if (contents.Length <= 3 || string.IsNullOrEmpty(MsgArgs))
            {
                if (string.IsNullOrEmpty(MsgArgs))
                {
                    return @"您好！
请告诉我您的名字";
                    BLL.Fans.SetGoOnCmd(FromId, "");
                }
                else if (contents.Length == 2)
                {
                    BLL.Fans.SetGoOnCmd(FromId, MsgArgs);
                    return MsgArgs + @"您好！
请告诉我您的性别，“男”或“女”";
                }
                else if (contents.Length == 3)
                {
                    if (MsgArgs == "男" || MsgArgs == "女")
                    {
                        return FlagSuccess(contents[1] + (string.IsNullOrEmpty(contents[2]) ? "未知" : (contents[2] == "男" ? "先生" : "女士")) + ",你好！");
                    }
                    else
                    {
                        return "性别输入错误，请输入“男”或“女”";
                    }
                }
            }
            return "";
        }
        public string HelloWorld()
        {
            return FlagSuccess("您对我说了：" + MsgArgs);
        }
        #endregion
    }
}
