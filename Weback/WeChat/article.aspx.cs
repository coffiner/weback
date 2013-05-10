using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class article : System.Web.UI.Page
    {
        public string _FootCopyRight = "<p><a href=\"http://weback.wlniao.com\">&copy;Weback</a></p>";
        public string _Title = "";
        public string _PicUrl = "";
        public string _Content = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Wlniao.WeChat.Model.RuleContent rc =null;
                try
                {
                    rc = Wlniao.WeChat.Model.RuleContent.findById(Convert.ToInt32(Request["id"]));
                }
                catch { }
                if (rc == null)
                {
                    Response.Clear();
                    Response.End();
                }
                else
                {
                    _Title = rc.Title;
                    _PicUrl = rc.PicUrl;
                    _Content = strUtil.HtmlDecode(rc.TextContent);

                    string str = System.Data.KvTableUtil.GetString("FootCopyRight");
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (strUtil.RemoveHtmlTag(str).Length == str.Length)
                        {
                            _FootCopyRight = "<p>" + str + "</p>";
                        }
                        else
                        {
                            _FootCopyRight = str;
                        }
                    }
                }
            }
        }
    }
}