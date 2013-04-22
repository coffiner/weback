using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class Fans : LoginPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "getnewcount":
                        int count = db.count<Wlniao.WeChat.Model.Fans>("Subscribe=1 and IsNewFans=1");
                        if (count > 0)
                        {
                            helper.Response(string.Format("document.write('<span class=\"label label-important tip-bottom\" title=\"{0} 位新增订阅者\">{0}</span>');", count));
                        }
                        else
                        {
                            helper.Response("document.write('');");
                        }
                        break;
                    case "setnickname":
                        Wlniao.WeChat.Model.Fans fansNickName = Wlniao.WeChat.Model.Fans.findByField("StrGuid", helper.GetParam("Guid"));
                        if (fansNickName == null)
                        {
                            helper.Result.Add("Sorry,要更新的订阅者不存在或已删除！");
                        }
                        else
                        {
                            fansNickName.NickName = helper.GetParam("NickName");
                        }
                        if (fansNickName != null && fansNickName.Id > 0)
                        {
                            helper.Result = fansNickName.update();
                        }
                        helper.ResponseResult();
                        break;
                    case "getlist":
                        int pageIndex = 0;
                        int pageSize = int.MaxValue;
                        try
                        {
                            pageIndex = int.Parse(helper.GetParam("pageIndex"));
                            pageSize = int.Parse(helper.GetParam("pageSize"));
                        }
                        catch { }

                        System.DataPage<Wlniao.WeChat.Model.Fans> items = db.findPage<Wlniao.WeChat.Model.Fans>("Subscribe=1", pageIndex, pageSize);

                        try
                        {
                            foreach (Wlniao.WeChat.Model.Fans fans in items.Results)
                            {
                                try
                                {
                                    fans.Sid = string.IsNullOrEmpty(fans.Sid) ? "" : "已绑定";
                                }
                                catch { }
                            }
                        }
                        catch { }
                        List<Wlniao.WeChat.Model.Fans> list = null;
                        try
                        {
                            //删除新订阅标记
                            list = db.findPage<Wlniao.WeChat.Model.Fans>("IsNewFans=1", 0, int.MaxValue).Results;
                            foreach (Wlniao.WeChat.Model.Fans fans in list)
                            {
                                fans.IsNewFans = 0;
                                fans.update("IsNewFans");
                            }
                        }
                        catch { };
                        list = items.Results;
                        if (list == null)
                        {
                            list = new List<Wlniao.WeChat.Model.Fans>();
                        }
                        helper.Response("{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}");
                        break;
                }
            }
        }
    }
}