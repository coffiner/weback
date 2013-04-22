using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class Account : LoginPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "get":
                        helper.Add("Username", Session["Account"].ToString());
                        helper.Response();
                        break;
                    case "set":
                        string _username = Session["Account"].ToString();
                        string username = helper.GetParam("Username");
                        string password = helper.GetParam("Password");
                        if (string.IsNullOrEmpty(_username))
                        {
                            helper.Result.Add("您尚未登录或登录已超时，请先登录！");
                        }
                        else if (string.IsNullOrEmpty(username))
                        {
                            helper.Result.Add("Sorry，用户名未填写！");
                        }
                        else if (string.IsNullOrEmpty(password))
                        {
                            helper.Result.Add("Sorry，登录密码未填写！");
                        }
                        else
                        {
                            if (password.Length != 32)
                            {
                                password = Encryptor.Md5Encryptor32(Encryptor.Md5Encryptor32(password));
                            }
                            System.Xml.XmlNode xn = System.IO.XMLHelper.GetDataOne(PathHelper.Map("~/xcenter/data/wechat/manager.xml"), "Manager", System.IO.XMLHelper.CreateEqualParameter("Username", _username));
                            if (xn == null)
                            {
                                helper.Result.Add("Sorry,管理员账号不存在");
                            }
                            else
                            {
                                helper.Result = System.IO.XMLHelper.UpdateData(PathHelper.Map("~/xcenter/data/wechat/manager.xml"), "Manager", System.IO.XMLHelper.CreateEqualParameter("Username", _username), System.IO.XMLHelper.CreateUpdateParameter("Username", username), System.IO.XMLHelper.CreateUpdateParameter("Password", password));
                                if (helper.Result.IsValid)
                                {
                                    Session["Account"] = username;
                                }
                            }
                        }
                        helper.ResponseResult();
                        break;
                }
            }
        }
    }
}