using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class Default : LoginPage
    {
        protected string _titleName = System.Data.KvTableUtil.GetString("WeChatName");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}