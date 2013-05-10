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
                    System.Data.KvTableUtil.Save("WeChatName", "Weback");
                    System.Data.KvTableUtil.Save("NoMessage", "nomatchfound");
                    System.Data.KvTableUtil.Save("SessionTimeOut", "180");
                    System.Data.KvTableUtil.Save("UploadExt", ".jpg,.gif,.png,.mp3,.wav,.acc,.wma,.rm");
                    Wlniao.WeChat.BLL.Rules.AddRules("订阅事件", "subscribe", "#", "Wlniao.Subscribe", "你好，感谢您的订阅，我们为您提供了丰富的功能，回复“帮助”查看命令列表吧", "系统默认事件");
                    Wlniao.WeChat.BLL.Rules.AddRules("取消订阅事件", "unsubscribe", "#", "Wlniao.UnSubscribe", "", "系统默认事件");
                    Wlniao.WeChat.BLL.Rules.AddRules("无匹配信息", "nomatchfound", "#", "", "您说的内容好复杂哦，我没法理解啦！请说点其他的吧~~", "用户内容无匹配信息时执行");
                    Wlniao.WeChat.BLL.Rules.AddRules("帮助菜单", "帮助", "#", "", "微信管理员尚未设置帮助菜单", "默认的帮助菜单内容");
                    Wlniao.WeChat.BLL.Rules.AddRules("在线翻译", "在线翻译", "#", "Wlniao.API", "", "在线翻译接口", "http://wxapi.azurewebsites.net/Translation.aspx");
                    Wlniao.WeChat.BLL.Rules.AddRules("Wlniao测试接口：Hello", "Hello 你好 未来鸟 机器人", "#", "Wlniao.Hello", "", "Wlniao测试接口(如：Hello 未来鸟 机器人)");
                    try
                    {
                        file.Delete(PathHelper.Map("~/xcenter/data/wechat/manager.xml"));
                    }
                    catch { }
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