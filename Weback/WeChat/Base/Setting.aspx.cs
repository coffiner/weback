using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class Setting : LoginPage
    {
        protected string _website = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Url.Port == 80)
                {
                    _website = Request.Url.Host;                    
                }
                else
                {
                    _website = Request.Url.Host + ":" + Request.Url.Port;
                }
                _website += Request.Url.AbsolutePath.Replace("/base/setting.aspx", "");
                switch (helper.GetParam("action").ToLower())
                {
                    case "get":
                        helper.Add("WeChatName", System.Data.KvTableUtil.GetString("WeChatName"));
                        helper.Add("AccountName", System.Data.KvTableUtil.GetString("AccountName"));
                        helper.Add("WeChatToken", System.Data.KvTableUtil.GetString("WeChatToken"));
                        helper.Add("NoMessage", System.Data.KvTableUtil.GetString("NoMessage"));
                        helper.Add("SessionTimeOut", System.Data.KvTableUtil.GetString("SessionTimeOut"));
                        helper.Add("UploadExt", System.Data.KvTableUtil.GetString("UploadExt"));
                        helper.Add("Appid", System.Data.KvTableUtil.GetString("Appid"));
                        helper.Add("Secret", System.Data.KvTableUtil.GetString("Secret"));
                        helper.Add("Intro", System.Data.KvTableUtil.GetString("Intro").Replace("\n", "<br/>"));
                        helper.Add("FootCopyRight", System.Data.KvTableUtil.GetString("FootCopyRight"));
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
                        System.Data.KvTableUtil.Save("Appid", helper.GetParam("Appid"));
                        System.Data.KvTableUtil.Save("Secret", helper.GetParam("Secret"));
                        System.Data.KvTableUtil.Save("Intro", helper.GetParam("Intro"));
                        System.Data.KvTableUtil.Save("FootCopyRight", helper.GetParam("FootCopyRight"));

                        try
                        {
                            if (!string.IsNullOrEmpty(helper.GetParam("Appid")) || !string.IsNullOrEmpty(helper.GetParam("Secret")))
                            {
                                Wlniao.WeChat.WeixinMP.MP.Init();
                            }
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("设置保存成功！但APPID校验失败，错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                }
            }
        }
    }
}