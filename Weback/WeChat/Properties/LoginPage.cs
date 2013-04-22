using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat
{
    public class LoginPage : System.Web.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            if (Session["Account"] == null || string.IsNullOrEmpty(Session["Account"].ToString()))
            {
                if (string.IsNullOrEmpty(helper.GetParam("action")))
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    Result result = new Result();
                    result.Add("Sorry,您尚未登录或登录已经超时！");
                    helper.Result = result;
                    helper.ResponseResult();
                }
            }
            else
            {
                base.OnLoad(e);
            }
        }
        protected string GetAccountGuid()
        {
            if (Session["Account"] == null)
            {
                return "";
            }
            else
            {
                return Session["Account"].ToString();
            }
        }
    }
}
