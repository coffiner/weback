/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\BLL\Rules.cs
        运 行 库：2.0.50727.1882
        代码功能：API规则存储方法定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.BLL
{
    public class Rules : System.ORM.CommonBase<Model.Rules>
    {
        public static Result AddRules(string RuleName, string RuleCode, string SepType, string DoMethod, string ReContent, string RuleHelp, string CallBackText = "", string SendMode="sendnew")
        {
            Result result = new Result();
            Model.Rules rules = new Model.Rules();
            rules.Guid = Guid.NewGuid().ToString();
            rules.RuleName = RuleName;
            rules.DoMethod = DoMethod;
            rules.ReContent = ReContent;
            rules.RuleHelp = RuleHelp;
            rules.CallBackText = CallBackText;
            rules.SendMode = SendMode;
            if (UseXml)
            {
                result.Join(System.IO.XMLHelper.AddData(PathHelper.Map("~/xcenter/data/wechat/rules.xml"), "Rules", System.IO.XMLHelper.CreateInsertParameter("Guid", rules.Guid), System.IO.XMLHelper.CreateInsertParameter("RuleName", rules.RuleName), System.IO.XMLHelper.CreateInsertParameter("DoMethod", rules.DoMethod), System.IO.XMLHelper.CreateInsertParameter("ReContent", rules.ReContent), System.IO.XMLHelper.CreateInsertParameter("RuleHelp", rules.RuleHelp), System.IO.XMLHelper.CreateInsertParameter("CallBackText", rules.CallBackText)));
                if (result.IsValid)
                {
                    result.Join(AddRuleCode(RuleCode, rules.Guid, SepType));
                }
            }
            else
            {
                result.Join(rules.insert());
                result.Join(AddRuleCode(RuleCode, rules.Guid, SepType));
            }
            return result;
        }
        public static Result AddRuleCode(string Code, string RuleGuid, string sepType, string Status = "normal")
        {
            List<Model.RuleCode> rulecodes = new List<Model.RuleCode>();
            if (string.IsNullOrEmpty(sepType))
            {
                if (Code.IndexOf("$") > 0)
                {
                    sepType = "$";
                }
                if (string.IsNullOrEmpty(sepType))
                {
                    sepType = "#";
                }
            }
            string[] codes = Code.Split(new string[] { sepType, ",", ";", " ", "，", "；" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            foreach (string code in codes)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    list.Add(code);
                }
            }
            list.Sort();
            Code = strUtil.Join(sepType, list.ToArray());
            Code = sepType + Code + sepType;

            Model.RuleCode rulecode = new Model.RuleCode();
            rulecode.Guid = Guid.NewGuid().ToString();
            rulecode.Code = Code;
            rulecode.RuleGuid = RuleGuid;
            rulecode.SepType = sepType;
            rulecode.Status = Status;
            rulecode.HitCount = list.Count;
            rulecode.HashCode = Encryptor.Md5Encryptor32(Code);
            _keywords = "";
            if (UseXml)
            {
                return System.IO.XMLHelper.AddData(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode", System.IO.XMLHelper.CreateInsertParameter("Code", rulecode.Code), System.IO.XMLHelper.CreateInsertParameter("RuleGuid", rulecode.RuleGuid), System.IO.XMLHelper.CreateInsertParameter("SepType", rulecode.SepType), System.IO.XMLHelper.CreateInsertParameter("HitCount", rulecode.HitCount.ToString()), System.IO.XMLHelper.CreateInsertParameter("Guid", rulecode.Guid));
            }
            else
            {
                return rulecode.insert();
            }
        }
        public static Result EditRuleCode(string Guid, string Code, string RuleGuid, string sepType, string Status)
        {
            Model.RuleCode rulecode = Model.RuleCode.findByField("StrGuid",Guid);
            if (rulecode == null && rulecode.Id <= 0)
            {
                Result result = new Result();
                result.Add("你操作的内容不存在或已删除！");
                return result;
            }
            List<Model.RuleCode> rulecodes = new List<Model.RuleCode>();
            if (string.IsNullOrEmpty(sepType))
            {
                if (Code.IndexOf("$") > 0)
                {
                    sepType = "$";
                }
                if (string.IsNullOrEmpty(sepType))
                {
                    sepType = "#";
                }
            }
            string[] codes = Code.Split(new string[] { sepType, ",", ";", " ", "，", "；" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            foreach (string code in codes)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    list.Add(code);
                }
            }
            list.Sort();
            Code = strUtil.Join(sepType, list.ToArray());
            Code = sepType + Code + sepType;

            rulecode.Code = Code;
            rulecode.RuleGuid = RuleGuid;
            rulecode.SepType = sepType;
            rulecode.HitCount = list.Count;
            if (!string.IsNullOrEmpty(Status))
            {
                rulecode.Status = Status;
            }
            rulecode.HashCode = Encryptor.Md5Encryptor32(Code);
            _keywords = "";
            if (UseXml)
            {
                return System.IO.XMLHelper.UpdateData(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode",System.IO.XMLHelper.CreateEqualParameter("Guid",Guid), System.IO.XMLHelper.CreateInsertParameter("Code", rulecode.Code), System.IO.XMLHelper.CreateInsertParameter("RuleGuid", rulecode.RuleGuid), System.IO.XMLHelper.CreateInsertParameter("SepType", rulecode.SepType), System.IO.XMLHelper.CreateInsertParameter("HitCount", rulecode.HitCount.ToString()), System.IO.XMLHelper.CreateInsertParameter("Guid", rulecode.Guid));
            }
            else
            {
                return rulecode.update();
            }
        }
        public static Result DelRules(string Guid)
        {
            Result result = new Result();
            if (UseXml)
            {
                result.Join(System.IO.XMLHelper.DeleteData(PathHelper.Map("~/xcenter/data/wechat/rules.xml"), "Rules", System.IO.XMLHelper.CreateEqualParameter("Guid", Guid)));
                if (result.IsValid)
                {
                    result.Join(System.IO.XMLHelper.DeleteData(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode", System.IO.XMLHelper.CreateEqualParameter("RuleGuid", Guid)));
                }
            }
            else
            {
                if (Model.Rules.findByField("StrGuid", Guid).delete() <= 0)
                {
                    result.Add("Sorry,规则删除失败");
                }
            }
            return result;
        }
        public static Result EditRules(string Guid, string RuleName, string RuleCode, string SepType, string DoMethod, string ReContent, string RuleHelp, string CallBackText, string SendMode)
        {
            Result result = new Result();
            try
            {
                if (UseXml)
                {
                    try
                    {
                        result.Join(System.IO.XMLHelper.UpdateData(PathHelper.Map("~/xcenter/data/wechat/rules.xml"), "Rules", System.IO.XMLHelper.CreateEqualParameter("Guid", Guid), System.IO.XMLHelper.CreateUpdateParameter("RuleName", RuleName), System.IO.XMLHelper.CreateUpdateParameter("DoMethod", DoMethod), System.IO.XMLHelper.CreateUpdateParameter("ReContent", ReContent), System.IO.XMLHelper.CreateUpdateParameter("RuleHelp", RuleHelp), System.IO.XMLHelper.CreateUpdateParameter("RuleHelp", CallBackText)));
                        if (result.IsValid)
                        {
                            result.Join(System.IO.XMLHelper.DeleteData(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode", System.IO.XMLHelper.CreateEqualParameter("RuleGuid", Guid)));
                            result.Join(AddRuleCode(RuleCode, Guid, SepType));
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Add("错误：" + ex.Message);
                    }
                }
                else
                {
                    Model.Rules rules = Model.Rules.findByField("StrGuid", Guid);
                    rules.RuleName = RuleName;
                    rules.DoMethod = DoMethod;
                    rules.ReContent = ReContent;
                    rules.RuleHelp = RuleHelp;
                    rules.CallBackText = CallBackText;
                    rules.CallBackText = SendMode;
                    result.Join(rules.update());
                    if (result.IsValid)
                    {
                        List<Model.RuleCode> list = Model.RuleCode.findListByField("RuleGuid", rules.Guid);
                        if (list != null && list.Count > 0)
                        {
                            foreach (Model.RuleCode code in list)
                            {
                                if (code.delete() <= 0)
                                {
                                    result.Add("Sorry,更新规则关键词错误！");
                                }
                            }
                        }
                    }
                    result.Join(AddRuleCode(RuleCode, rules.Guid, SepType));
                }
            }
            catch (Exception ex)
            {
                result.Add("错误：" + ex.Message);
            }
            return result;
        }
        public static Model.Rules Get(string Guid)
        {
            Model.Rules rules = new Model.Rules();
            try
            {
                if (UseXml)
                {
                    System.Xml.XmlNode xn = System.IO.XMLHelper.GetDataOne(PathHelper.Map("~/xcenter/data/wechat/rules.xml"), "Rules", System.IO.XMLHelper.CreateLikeParameter("Guid", Guid));
                    rules.Id = Convert.ToInt32(xn.Attributes["Id"].Value);
                    rules.Guid = xn.Attributes["Guid"].Value;
                    rules.RuleName = xn.Attributes["RuleName"].Value;
                    rules.DoMethod = xn.Attributes["DoMethod"].Value;
                    rules.ReContent = xn.Attributes["ReContent"].Value;
                    rules.RuleHelp = xn.Attributes["RuleHelp"].Value;
                    rules.CallBackText = xn.Attributes["CallBackText"].Value;
                }
                else
                {
                    rules = Model.Rules.findByField("StrGuid", Guid);
                }
            }
            catch { return null; }
            return rules;
        }
        /// <summary>
        /// 根据命令符获取规则
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static Model.Rules GetRuleByCode(string Code,string OpenId)
        {
            try
            {
                return Get(GetRuleCode(Code, Code, OpenId).RuleGuid);
            }
            catch { return null; }
        }
        /// <summary>
        /// 根据文本内容自动获取
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static Model.Rules GetRuleByText(string Text, string OpenId)
        {
            try
            {
                return Get(GetRuleCode(Text.Split(new string[] { "$", "#" }, StringSplitOptions.RemoveEmptyEntries)[0], Text, OpenId).RuleGuid);
            }
            catch { return null; }
        }
        public static Model.RuleCode GetRuleCode(string Code, string Text, string OpenId)
        {
            Model.RuleCode rulecode = null;
            try
            {
                try
                {
                    if (UseXml)
                    {
                        System.Xml.XmlNode xn = System.IO.XMLHelper.GetDataOne(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode", System.IO.XMLHelper.CreateLikeParameter("Code", "#" + Code + "#"));
                        if (xn != null && xn.Attributes["Status"].Value != "close")
                        {
                            rulecode = new Model.RuleCode();
                            rulecode.Id = Convert.ToInt32(xn.Attributes["Id"].Value);
                            rulecode.HitCount = Convert.ToInt32(xn.Attributes["HitCount"].Value);
                            rulecode.Guid = xn.Attributes["Guid"].Value;
                            rulecode.Code = xn.Attributes["Code"].Value;
                            rulecode.RuleGuid = xn.Attributes["RuleGuid"].Value;
                            rulecode.SepType = xn.Attributes["SepType"].Value;
                            rulecode.Status = xn.Attributes["Status"].Value;
                            try
                            {
                                rulecode.HashCode = xn.Attributes["HashCode"].Value;
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        rulecode = Model.RuleCode.find("Status <>'close' and Code like'%#" + Code + "#%'").first();
                    }
                }
                catch { }
                if (rulecode == null)
                {
                    Result result = strUtil.CheckSensitiveWords(Text, KeyWords);
                    int HitCount = result.Errors.Count;
                    if (HitCount > 0)
                    {
                        string temp = "(" + strUtil.Join("|", result.Errors.ToArray()) + ")";
                        for (; HitCount > 0; HitCount--)
                        {
                            rulecode = null;
                            try
                            {
                                if (UseXml)
                                {
                                    System.Xml.XmlNode xn = System.IO.XMLHelper.GetDataOne(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode", System.IO.XMLHelper.CreateLikeParameter("Code", "$" + result.Errors[HitCount - 1] + "$"));
                                    if (xn != null && xn.Attributes["Status"].Value != "close")
                                    {
                                        rulecode = new Model.RuleCode();
                                        rulecode.Id = Convert.ToInt32(xn.Attributes["Id"].Value);
                                        rulecode.HitCount = Convert.ToInt32(xn.Attributes["HitCount"].Value);
                                        rulecode.Guid = xn.Attributes["Guid"].Value;
                                        rulecode.Code = xn.Attributes["Code"].Value;
                                        rulecode.RuleGuid = xn.Attributes["RuleGuid"].Value;
                                        rulecode.SepType = xn.Attributes["SepType"].Value;
                                        rulecode.Status = xn.Attributes["Status"].Value;
                                        try
                                        {
                                            rulecode.HashCode = xn.Attributes["HashCode"].Value;
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    rulecode = Model.RuleCode.find("Status <>'close' and Code like'%$" + Code + "$%'").first();
                                }
                                if (rulecode != null)
                                {
                                    Result t = strUtil.CheckSensitiveWords(rulecode.Code, temp);
                                    List<string> ary = new List<string>();
                                    for (int i = 0; i < t.Errors.Count; i++)
                                    {
                                        if (!ary.Contains(t.Errors[i]))
                                        {
                                            ary.Add(t.Errors[i]);
                                        }
                                    }
                                    if (ary.Count < rulecode.HitCount)
                                    {
                                        rulecode = null;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { return null; }
            try
            {
                if (rulecode != null && rulecode.Status == "test" && Fans.GetBy("WeChatOpenId", OpenId).AllowTest != 1)
                {
                    rulecode = null;
                }
            }
            catch { }
            return rulecode;
        }
        private static string _keywords = "";
        private static string KeyWords
        {
            get
            {
                if (string.IsNullOrEmpty(_keywords))
                {
                    UpdateKeyWords("$");
                }
                return _keywords;
            }
        }
        protected static void UpdateKeyWords(string sepType)
        {
            List<string> list = new List<string>();
            if (UseXml)
            {
                System.Xml.XmlNodeList xnlist = System.IO.XMLHelper.GetDataList(PathHelper.Map("~/xcenter/data/wechat/rulecode.xml"), "RuleCode", System.IO.XMLHelper.CreateEqualParameter("SepType", sepType));
                foreach (System.Xml.XmlNode xn in xnlist)
                {
                    try
                    {
                        string[] codes = xn.Attributes["Code"].Value.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string code in codes)
                        {
                            if (!string.IsNullOrEmpty(code) && !list.Contains(code))
                            {
                                list.Add(code);
                            }
                        }
                    }
                    catch { }
                }
            }
            else
            {
                List<Model.RuleCode> rclist = Model.RuleCode.findListByField("SepType", sepType);
                foreach (Model.RuleCode rc in rclist)
                {
                    try
                    {
                        string[] codes = rc.Code.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string code in codes)
                        {
                            if (!string.IsNullOrEmpty(code) && !list.Contains(code))
                            {
                                list.Add(code);
                            }
                        }
                    }
                    catch { }
                }
            }
            _keywords = strUtil.Join("|", list.ToArray());

        }
        private static string _UseXml = "";
        private static bool UseXml
        {
            get
            {
                if (string.IsNullOrEmpty(_UseXml))
                {
                    _UseXml = Tool.GetConfiger("UseXml");
                }
                return _UseXml == "true";
            }
        }


        public static Result AddRuleContent(string RuleGuid, string ContentType, string Title, string TextContent, string PicUrl, string ThumbPicUrl, string MusicUrl, string LinkUrl, string ContentStatus = "normal")
        {
            Model.RuleContent rulecontent = new Model.RuleContent();
            rulecontent.Guid = Guid.NewGuid().ToString();
            rulecontent.RuleGuid = RuleGuid;
            rulecontent.ContentType = ContentType;
            rulecontent.Title = Title;
            rulecontent.TextContent = TextContent;
            rulecontent.PicUrl = PicUrl;
            if (string.IsNullOrEmpty(ThumbPicUrl))
            {
                ThumbPicUrl = PicUrl;
            }
            rulecontent.ThumbPicUrl = ThumbPicUrl;
            rulecontent.MusicUrl = MusicUrl;
            rulecontent.LinkUrl = LinkUrl;
            rulecontent.ContentStatus = ContentStatus;
            rulecontent.PushCount = 0;
            return rulecontent.insert();
        }
        public static Result EditRuleContent(string Guid, string ContentType, string Title, string TextContent, string PicUrl, string ThumbPicUrl, string MusicUrl, string LinkUrl, string ContentStatus = "normal")
        {
            Model.RuleContent rulecontent = Model.RuleContent.findByField("StrGuid", Guid);
            if (rulecontent == null && rulecontent.Id <= 0)
            {
                Result result = new Result();
                result.Add("你操作的内容不存在或已删除！");
                return result;
            }
            else
            {
                rulecontent.ContentType = ContentType;
                rulecontent.Title = Title;
                rulecontent.TextContent = TextContent;
                rulecontent.PicUrl = PicUrl;
                if (string.IsNullOrEmpty(ThumbPicUrl))
                {
                    ThumbPicUrl = PicUrl;
                }
                rulecontent.ThumbPicUrl = ThumbPicUrl;
                rulecontent.MusicUrl = MusicUrl;
                rulecontent.LinkUrl = LinkUrl;
                rulecontent.ContentStatus = ContentStatus;
                return rulecontent.update();
            }
        }
        public static Result DelRuleContent(string Guid)
        {
            Result result = new Result();
            if (Model.RuleContent.findByField("StrGuid", Guid).delete() <= 0)
            {
                result.Add("Sorry,内容删除失败");
            }
            return result;
        }
    }
}
