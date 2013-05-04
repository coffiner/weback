using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class Setting : LoginPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "get":
                        helper.Add("WeChatName", System.Data.KvTableUtil.GetString("WeChatName"));
                        helper.Add("AccountName", System.Data.KvTableUtil.GetString("AccountName"));
                        helper.Add("WeChatToken", System.Data.KvTableUtil.GetString("WeChatToken"));
                        helper.Add("NoMessage", System.Data.KvTableUtil.GetString("NoMessage"));
                        helper.Add("SessionTimeOut", System.Data.KvTableUtil.GetString("SessionTimeOut"));
                        helper.Add("UploadExt", System.Data.KvTableUtil.GetString("UploadExt"));
                        helper.Add("Intro", System.Data.KvTableUtil.GetString("Intro").Replace("\n", "<br/>"));
                        helper.Response();
                        break;
                    case "set":
                        if (!string.IsNullOrEmpty(helper.GetParam("WeChatName")))
                        {
                        }
                        System.Data.KvTableUtil.Save("WeChatName", helper.GetParam("WeChatName"));
                        System.Data.KvTableUtil.Save("AccountName", helper.GetParam("AccountName"));
                        System.Data.KvTableUtil.Save("WeChatToken", helper.GetParam("WeChatToken"));
                        System.Data.KvTableUtil.Save("NoMessage", helper.GetParam("NoMessage"));
                        Wlniao.WeChat.BLL.Rules.SessionTimeOut = -1;
                        System.Data.KvTableUtil.Save("SessionTimeOut", helper.GetParam("SessionTimeOut"));
                        System.Data.KvTableUtil.Save("UploadExt", helper.GetParam("UploadExt"));
                        System.Data.KvTableUtil.Save("Intro", helper.GetParam("Intro"));
                        helper.ResponseResult();
                        break;
                }
            }
        }
    }
}