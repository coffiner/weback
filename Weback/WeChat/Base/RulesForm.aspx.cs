using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class RulesForm : LoginPage
    {
        protected string _Display = "";
        protected string _DisplayNew = "";
        protected string _DoMethodDisplay = "";
        protected string _ReContentDisplay = "";
        protected string _GobackUrl = "#";
        protected string _Guid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _Guid = Request["guid"];
                if (string.IsNullOrEmpty(_Guid))
                {
                    _Display = "display:none;";
                }
                else
                {
                    _DisplayNew = "display:none;";
                }
                if (helper.GetParam("type") == "auto")
                {
                    _GobackUrl = "rulesauto.aspx";
                    _DoMethodDisplay = "display:none;";
                }
                else
                {
                    _ReContentDisplay = "display:none;";
                    _GobackUrl = "rulesmethod.aspx";
                }
                switch (helper.GetParam("action").ToLower())
                {
                    case "get":
                        Wlniao.WeChat.Model.Rules rulesGet = Wlniao.WeChat.BLL.Rules.Get(_Guid);
                        if (rulesGet == null)
                        {
                            rulesGet = new Wlniao.WeChat.Model.Rules();
                        }
                        try
                        {
                            rulesGet.ReContent = rulesGet.ReContent.Replace("\n", "<br/>");
                        }
                        catch { }
                        try
                        {
                            rulesGet.RuleHelp = rulesGet.RuleHelp.Replace("\n", "<br/>");
                        }
                        catch { }
                        helper.Response(rulesGet);
                        break;
                    case "set":
                        Wlniao.WeChat.Model.Rules rulesSet = Wlniao.WeChat.BLL.Rules.Get(_Guid);
                        if (rulesSet == null)
                        {
                            rulesSet = new Wlniao.WeChat.Model.Rules();
                            rulesSet.Guid = Guid.NewGuid().ToString();
                        }
                        rulesSet.RuleName = helper.GetParam("RuleName");
                        rulesSet.DoMethod = helper.GetParam("DoMethod");
                        rulesSet.ReContent = helper.GetParam("ReContent").Replace("<br/>", "\n");
                        rulesSet.RuleHelp = helper.GetParam("RuleHelp").Replace("<br/>", "\n");
                        rulesSet.CallBackText = helper.GetParam("CallBackText");
                        rulesSet.SendMode = helper.GetParam("SendMode");
                        if (rulesSet.Id > 0)
                        {
                            if (Tool.GetConfiger("UseXml") == "true")
                            {
                                try
                                {
                                    helper.Result.Join(System.IO.XMLHelper.UpdateData(PathHelper.Map("~/xcenter/data/wechat/rules.xml"), "Rules", System.IO.XMLHelper.CreateEqualParameter("Guid", rulesSet.Guid), System.IO.XMLHelper.CreateUpdateParameter("RuleName", rulesSet.RuleName), System.IO.XMLHelper.CreateUpdateParameter("DoMethod", rulesSet.DoMethod), System.IO.XMLHelper.CreateUpdateParameter("ReContent", rulesSet.ReContent), System.IO.XMLHelper.CreateUpdateParameter("RuleHelp", rulesSet.RuleHelp), System.IO.XMLHelper.CreateUpdateParameter("RuleHelp", rulesSet.CallBackText)));
                                }
                                catch (Exception ex)
                                {
                                    helper.Result.Add("错误：" + ex.Message);
                                }
                            }
                            else
                            {
                                helper.Result = rulesSet.update();
                            }
                        }
                        else
                        {
                            if (Tool.GetConfiger("UseXml") == "true")
                            {
                                helper.Result.Join(System.IO.XMLHelper.AddData(PathHelper.Map("~/xcenter/data/wechat/rules.xml"), "Rules", System.IO.XMLHelper.CreateInsertParameter("Guid", rulesSet.Guid), System.IO.XMLHelper.CreateInsertParameter("RuleName", rulesSet.RuleName), System.IO.XMLHelper.CreateInsertParameter("DoMethod", rulesSet.DoMethod), System.IO.XMLHelper.CreateInsertParameter("ReContent", rulesSet.ReContent), System.IO.XMLHelper.CreateInsertParameter("RuleHelp", rulesSet.RuleHelp), System.IO.XMLHelper.CreateInsertParameter("CallBackText", rulesSet.CallBackText)));
                            }
                            else
                            {
                                helper.Result = rulesSet.insert();
                            }
                        }
                        helper.ResponseResult();
                        break;
                    case "setcode":
                        Wlniao.WeChat.Model.RuleCode codeSet = Wlniao.WeChat.Model.RuleCode.findByField("StrGuid", _Guid);
                        if (codeSet == null)
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.AddRuleCode(helper.GetParam("Code"), helper.GetParam("RuleGuid"), helper.GetParam("SepType"));
                        }
                        else
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.EditRuleCode(_Guid, helper.GetParam("Code"), helper.GetParam("RuleGuid"), helper.GetParam("SepType"), helper.GetParam("Status"));
                        }
                        helper.ResponseResult();
                        break;
                    case "delcode":
                        try
                        {
                            if (Wlniao.WeChat.Model.RuleCode.findByField("StrGuid", helper.GetParam("Guid")).delete() <= 0)
                            {
                                helper.Result.Add("Sorry，删除匹配项失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                    case "getlist":
                        int pageIndex = 0;
                        int pageSize = int.MaxValue;
                        try
                        {
                            pageIndex = int.Parse(helper.GetParam("pageIndex"));
                            pageSize = int.Parse(helper.GetParam("pageSize"));
                        }
                        catch { }

                        System.DataPage<Wlniao.WeChat.Model.RuleCode> items = db.findPage<Wlniao.WeChat.Model.RuleCode>("RuleGuid='" + helper.GetParam("RuleGuid") + "'", pageIndex, pageSize);
                        List<Wlniao.WeChat.Model.RuleCode> list = items.Results;
                        if (list == null)
                        {
                            list = new List<Wlniao.WeChat.Model.RuleCode>();
                        }
                        foreach (Wlniao.WeChat.Model.RuleCode rulecode in list)
                        {
                            rulecode.Code = rulecode.Code.Replace("#", " ").Replace("$", " ").TrimStart().TrimEnd().Replace(" ", ",");
                        }
                        helper.Response("{total:" + items.RecordCount + ",data:" + Json.ToStringList(list) + "}");
                        break;
                    case "setcontent":
                        Wlniao.WeChat.Model.RuleContent codeContent = Wlniao.WeChat.Model.RuleContent.findByField("StrGuid", _Guid);
                        if (codeContent == null)
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.AddRuleContent(helper.GetParam("RuleGuid"), helper.GetParam("ContentType"), helper.GetParam("Title"), helper.GetParam("TextContent").Replace("<br/>", "\n"), helper.GetParam("PicUrl"), helper.GetParam("ThumbPicUrl"), helper.GetParam("MusicUrl"), helper.GetParam("LinkUrl"), helper.GetParam("ContentStatus"));
                        }
                        else
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.EditRuleContent(_Guid, helper.GetParam("ContentType"), helper.GetParam("Title"), helper.GetParam("TextContent").Replace("<br/>", "\n"), helper.GetParam("PicUrl"), helper.GetParam("ThumbPicUrl"), helper.GetParam("MusicUrl"), helper.GetParam("LinkUrl"), helper.GetParam("ContentStatus"));
                        }
                        helper.ResponseResult();
                        break;
                    case "delcontent":
                        try
                        {
                            if (Wlniao.WeChat.Model.RuleContent.findByField("StrGuid", helper.GetParam("Guid")).delete() <= 0)
                            {
                                helper.Result.Add("Sorry，删除内容失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                    case "stickcontent":
                        try
                        {
                            var stick = Wlniao.WeChat.Model.RuleContent.findByField("StrGuid", helper.GetParam("Guid"));
                            stick.LastStick = DateTools.GetNow();
                            stick.update("LastStick");
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                    case "getcontentlist":
                        pageIndex = 0;
                        pageSize = int.MaxValue;
                        try
                        {
                            pageIndex = int.Parse(helper.GetParam("pageIndex"));
                            pageSize = int.Parse(helper.GetParam("pageSize"));
                        }
                        catch { }

                        System.DataPage<Wlniao.WeChat.Model.RuleContent> itemsContent = db.findPage<Wlniao.WeChat.Model.RuleContent>("RuleGuid='" + helper.GetParam("RuleGuid") + "' order by LastStick desc", pageIndex, pageSize);
                        List<Wlniao.WeChat.Model.RuleContent> listContent = itemsContent.Results;
                        if (listContent == null)
                        {
                            listContent = new List<Wlniao.WeChat.Model.RuleContent>();
                        }
                        foreach (Wlniao.WeChat.Model.RuleContent rulecontent in listContent)
                        {
                            rulecontent.TextContent = rulecontent.TextContent.Replace("\n", "<br/>");
                        }
                        helper.Response("{total:" + itemsContent.RecordCount + ",data:" + Json.ToStringList(listContent) + "}");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}