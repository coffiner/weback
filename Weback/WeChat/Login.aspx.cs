using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class Login : System.TemplateEngine.PageBase
    {
        protected string _titleName = System.Data.KvTableUtil.GetString("WeChatName");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["do"] == "init")
            {
                AshxHelper helper = new AshxHelper(Context);
                if (helper.Result.IsValid)
                {
                    System.Data.KvTableUtil.Save("UseXml", "false");
                    Wlniao.WeChat.BLL.Rules.AddRules("订阅事件", "subscribe", "#", "Wlniao.Subscribe", "", "系统默认事件");
                    Wlniao.WeChat.BLL.Rules.AddRules("取消订阅事件", "unsubscribe", "#", "Wlniao.UnSubscribe", "", "系统默认事件");
                    Wlniao.WeChat.BLL.Rules.AddRules("Hello", "Hello", "#", "Wlniao.Hello", "", "Wlniao测试接口(如：Hello 未来鸟 机器人)");
                    Wlniao.WeChat.BLL.Rules.AddRules("HelloWorld", "HelloWorld", "#", "Wlniao.HelloWorld", "", "Wlniao测试接口(如：HelloWorld 你好!)");
                    Wlniao.WeChat.BLL.Rules.AddRules("在线翻译", "在线翻译", "#", "Wlniao.API", "", "在线翻译接口", "http://wxapi.azurewebsites.net/Translation.aspx");
                    file.Delete(PathHelper.Map("~/xcenter/data/wechat/manager.xml"));
                    helper.Result = Wlniao.WeChat.BLL.Sys.Register(Request["username"], Request["password"], true);
                    if (helper.Result.IsValid)
                    {
                        System.Data.KvTableUtil.Save("HasInit", "true");
                    }
                }
                helper.ResponseResult();
            }
            else if (Request["do"] == "login")
            {
                AshxHelper helper = new AshxHelper(Context);
                helper.Result = Wlniao.WeChat.BLL.Sys.CheckLogin(Request["username"], Request["password"]);
                if (helper.Result.IsValid)
                {
                    Session["Account"] = Request["username"];
                }
                helper.ResponseResult();
            }
            else
            {
                Session["Account"] = null;
            }
        }
    }
}