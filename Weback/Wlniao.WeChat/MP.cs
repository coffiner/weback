using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.WeixinMP
{
	public class MP
	{
		private static string _appid = "";
		private static string _secret = "";
		private static string _token = "";
		private static DateTime _tokenexpires = DateTime.Now;

		private static string Token {
			get {
				if (DateTime.Now < _tokenexpires) {
					return _token;
				} else {
					return "";
				}
			}
		}

		private static  System.Collections.Hashtable err = new System.Collections.Hashtable ();

		public static Msg Init ()
		{
			if (string.IsNullOrEmpty (_appid)) {
				_appid = System.Data.KvTableUtil.GetString ("Appid");
			}
			if (string.IsNullOrEmpty (_secret)) {
				_secret = System.Data.KvTableUtil.GetString ("Secret");
			}
			if (err.Count == 0) {
				err.Add ("", "");
				err.Add (" -1", "系统繁忙");
				err.Add ("0", "请求成功");
				err.Add ("40001", "验证失败");
				err.Add ("40002", "不合法的凭证类型");
				err.Add ("40003", "不合法的OpenID");
				err.Add ("40004", "不合法的媒体文件类型");
				err.Add ("40005", "不合法的文件类型");
				err.Add ("40006", "不合法的文件大小");
				err.Add ("40007", "不合法的媒体文件id");
				err.Add ("40008", "不合法的消息类型");
				err.Add ("40009", "不合法的图片文件大小");
				err.Add ("40010", "不合法的语音文件大小");
				err.Add ("40011", "不合法的视频文件大小");
				err.Add ("40012", "不合法的缩略图文件大小");
				err.Add ("40013", "不合法的APPID");
				err.Add ("40014", "不合法的access_token");
				err.Add ("40015", "不合法的菜单类型");
				err.Add ("40016", "不合法的按钮个数");
				err.Add ("40017", "不合法的按钮个数");
				err.Add ("40018", "不合法的按钮名字长度");
				err.Add ("40019", "不合法的按钮KEY长度");
				err.Add ("40020", "不合法的按钮URL长度");
				err.Add ("40021", "不合法的菜单版本号");
				err.Add ("40022", "不合法的子菜单级数");
				err.Add ("40023", "不合法的子菜单按钮个数");
				err.Add ("40024", "不合法的子菜单按钮类型");
				err.Add ("40025", "不合法的子菜单按钮名字长度");
				err.Add ("40026", "不合法的子菜单按钮KEY长度");
				err.Add ("40027", "不合法的子菜单按钮URL长度");
				err.Add ("40028", "不合法的自定义菜单使用用户");
				err.Add ("41001", "缺少access_token参数");
				err.Add ("41002", "缺少appid参数");
				err.Add ("41003", "缺少refresh_token参数");
				err.Add ("41004", "缺少secret参数");
				err.Add ("41005", "缺少多媒体文件数据");
				err.Add ("41006", "缺少media_id参数");
				err.Add ("41007", "缺少子菜单数据");
				err.Add ("42001", "access_token超时");
				err.Add ("43001", "需要GET请求");
				err.Add ("43002", "需要POST请求");
				err.Add ("43003", "需要HTTPS请求");
				err.Add ("44001", "多媒体文件为空");
				err.Add ("44002", "POST的数据包为空");
				err.Add ("44003", "图文消息内容为空");
				err.Add ("45001", "多媒体文件大小超过限制");
				err.Add ("45002", "消息内容超过限制");
				err.Add ("45003", "标题字段超过限制");
				err.Add ("45004", "描述字段超过限制");
				err.Add ("45005", "链接字段超过限制");
				err.Add ("45006", "图片链接字段超过限制");
				err.Add ("45007", "语音播放时间超过限制");
				err.Add ("45008", "图文消息超过限制");
				err.Add ("45009", "接口调用超过限制");
				err.Add ("45010", "创建菜单个数超过限制");
				err.Add ("46001", "不存在媒体数据");
				err.Add ("46002", "不存在的菜单版本");
				err.Add ("46003", "不存在的菜单数据");
				err.Add ("47001", "解析JSON/XML内容错误");
			}
			string url = string.Format ("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", _appid, _secret);
			string result = System.Web.PostAndGet.GetResponseString (url);
			GetToken token = null;
			try {
				Json.ToObject<GetToken> (result);
			} catch {
			}
			if (token != null) {
				_token = token.access_token;
				_tokenexpires = DateTime.Now.AddSeconds (Convert.ToInt32 (token.expires_in) - 30);
				return null;
			} else {
				return Json.ToObject<Msg> (result);
			}
		}


		public static string SyncMenu (string json)
		{
			try {
				if (string.IsNullOrEmpty (Token)) {
					Init ();
				}
				string url = string.Format ("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", Token);
				string result = System.Web.PostAndGet.PostWebRequest (url,json,"utf-8");
			
				Msg error = Json.ToObject<Msg> (result);
				if (error != null&&error.errcode=="0") {
					return "菜单更新成功";
				}else{
					return "错误："+err [error.errcode].ToString ();
				}
			} catch(Exception ex) {
				return "错误："+ex.Message;
			}		
		}
	}

	public class GetToken
	{
		private string _access_token;
		private string _expires_in;

		public string access_token { get { return _access_token; } set { _access_token = value; } }

		public string expires_in { get { return _expires_in; } set { _expires_in = value; } }
	}

	public class Msg
	{
		private string _errcode;
		private string _errmsg;

		public string errcode { get { return _errcode; } set { _errcode = value; } }

		public string errmsg { get { return _errmsg; } set { _errmsg = value; } }
	}
}
