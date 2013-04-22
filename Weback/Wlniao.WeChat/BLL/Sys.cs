/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\BLL\Sys.cs
        运 行 库：2.0.50727.1882
        代码功能：系统数据存储方法定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
namespace Wlniao.WeChat.BLL
{
    public class Sys
    {
        public static Result Register(String username, String password,Boolean supper)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("Sorry,管理员账号不能为空");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("Sorry,管理员密码不能为空");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(Encryptor.Md5Encryptor32(password));
                }
                result.Join(System.IO.XMLHelper.AddData(PathHelper.Map("~/xcenter/data/wechat/manager.xml"), "Manager", System.IO.XMLHelper.CreateInsertParameter("Username", username), System.IO.XMLHelper.CreateInsertParameter("Password", password), System.IO.XMLHelper.CreateInsertParameter("Supper", supper ? "true" : "false")));
            } 
            return result;
        }
        public static Result CheckLogin(String username, String password)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("Sorry,管理员账号不能为空");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("Sorry,管理员密码不能为空");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(Encryptor.Md5Encryptor32(password));
                }
                System.Xml.XmlNode xn = System.IO.XMLHelper.GetDataOne(PathHelper.Map("~/xcenter/data/wechat/manager.xml"), "Manager", System.IO.XMLHelper.CreateEqualParameter("Username", username));
                if (xn == null)
                {
                    result.Add("Sorry,管理员账号不存在");
                }
                else if (xn.Attributes["Password"].Value != password)
                {
                    result.Add("Sorry,您输入的密码不正确");
                }
            }
            return result;
        }
    }
}
