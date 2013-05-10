using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
	public partial class MyButton : LoginPage
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (!IsPostBack) {
				switch (helper.GetParam ("action").ToLower ()) {
				case "sync":
					string sync = Sync();
					if (!string.IsNullOrEmpty (sync)) {
						helper.Result.Add (sync);
					}
					helper.ResponseResult();
					break;
				case "get":
					Wlniao.WeChat.Model.Menus modelGet = null;
					try {
						modelGet = Wlniao.WeChat.Model.Menus.findById (Convert.ToInt32 (helper.GetParam ("Id")));
					} catch {
					}
					if (modelGet == null) {
						modelGet = new Wlniao.WeChat.Model.Menus ();
					}
					helper.Response (modelGet);
					break;
				case "gettops":
					List<Wlniao.WeChat.Model.Menus> menutop = db.find<Wlniao.WeChat.Model.Menus> ("MenuPath='button'").list();
					if (menutop == null) {
						menutop = new List<Wlniao.WeChat.Model.Menus> ();
					}
					helper.Response (menutop);
					helper.Response ();
					break;
				case "getsub":
					List<Wlniao.WeChat.Model.Menus> menusub = db.find<Wlniao.WeChat.Model.Menus> ("MenuPath='button" + helper.GetParam ("pid") + "'").list();
					if (menusub == null) {
						menusub = new List<Wlniao.WeChat.Model.Menus> ();
					}
					helper.Response (menusub);
					helper.Response ();
					break;

				case "save":
					if (string.IsNullOrEmpty (helper.GetParam ("MenuName"))) {
						helper.Result.Add ("Sorry,按钮名称未填写！");
					} else {
						Wlniao.WeChat.Model.Menus model = null;
						try {
							model = Wlniao.WeChat.Model.Menus.findById (Convert.ToInt32 (helper.GetParam ("Id")));
						} catch {
						}
						if (model == null) {
							model = new Wlniao.WeChat.Model.Menus ();
						}
						model.MenuName = helper.GetParam ("MenuName");
						model.MenuKey = helper.GetParam ("MenuKey");
						if (model.Id > 0) {
							helper.Result = model.update ();
						} else {
							model.MenuPath = "button" + helper.GetParam ("Pid");
							helper.Result = model.insert ();
						}
					}
					helper.ResponseResult();
					break;
				case "del":
					try {
						if (string.IsNullOrEmpty (helper.GetParam ("Id"))) {
							helper.Result.Add ("请指定要删除的菜单");
						} else if (db.findPage<Wlniao.WeChat.Model.Menus> ("MenuPath='button" + helper.GetParam ("Id") + "'").RecordCount > 0) {
							helper.Result.Add ("当前菜单包含子菜单，请先删除子菜单");
						} else if (Wlniao.WeChat.Model.Menus.findById (Convert.ToInt32 (helper.GetParam ("Id"))).delete () <= 0) {
							helper.Result.Add ("错误：数据删除失败");
						}
					} catch (Exception ex) {
						helper.Result.Add ("错误：" + ex.Message);
					}
					helper.ResponseResult ();
					break;

				}


			}
		}

		protected string Sync ()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			List<Wlniao.WeChat.Model.Menus> menutop = db.find<Wlniao.WeChat.Model.Menus> ("MenuPath='button'").list();
			if(menutop!=null&&menutop.Count>0){
				foreach(Wlniao.WeChat.Model.Menus top in menutop){			
					if(sb.Length>0){
						sb.Append(",");
					}
					List<Wlniao.WeChat.Model.Menus> menusub = db.find<Wlniao.WeChat.Model.Menus> ("MenuPath='button" + top.Id.ToString() + "'").list();
					if(menusub!=null&&menusub.Count>0){
						System.Text.StringBuilder sbsub=new System.Text.StringBuilder();
						try{
						foreach(Wlniao.WeChat.Model.Menus sub in menusub){	
							if(sbsub.Length>0){
								sbsub.Append(",");
							}
							sbsub.Append("{\"type\":\"click\", \"name\":\""+sub.MenuName+"\",\"key\":\""+sub.MenuKey+"\"}");
							}
						}catch{
							sbsub=new System.Text.StringBuilder();
						}
						if(sbsub.Length>0){
							sb.Append("{\"name\":\""+top.MenuName+"\",\"sub_button\":["+sbsub.ToString()+"]}");
						}else{
							sb.Append("{\"type\":\"click\", \"name\":\""+top.MenuName+"\",\"key\":\""+top.MenuKey+"\"}");
						}
					}else{
						sb.Append("{\"type\":\"click\", \"name\":\""+top.MenuName+"\",\"key\":\""+top.MenuKey+"\"}");
					}
				}
			}
			string json=" {\"button\":["+sb.ToString()+"]}";
			return Wlniao.WeChat.WeixinMP.MP.SyncMenu (json);
		}
	}
}