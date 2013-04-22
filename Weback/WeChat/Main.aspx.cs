using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class Main : LoginPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/base/setting.aspx");
        }
    }
}