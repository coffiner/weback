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
                if (helper.GetParam("type") == "auto")
                {
                    _GobackUrl = "/base/rulesauto.aspx";
                    _DoMethodDisplay = "display:none;";
                }
                else
                {
                    _ReContentDisplay = "display:none;";
                    _GobackUrl = "/base/rulesmethod.aspx";
                }
                switch (helper.GetParam("action").ToLower())
                {
                    case "get":
                        Wlniao.WeChat.Model.Rules rulesGet = Wlniao.WeChat.BLL.Rules.Get(_Guid);
                        if (rulesGet == null)
                        {
                            rulesGet = new Wlniao.WeChat.Model.Rules();
                        }
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
                        rulesSet.ReContent = helper.GetParam("ReContent");
                        rulesSet.RuleHelp = helper.GetParam("RuleHelp");
                        rulesSet.CallBackText = helper.GetParam("CallBackText");
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
                            helper.Result = Wlniao.WeChat.BLL.Rules.EditRuleCode(_Guid, helper.GetParam("Code"), helper.GetParam("RuleGuid") , helper.GetParam("SepType"));
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
                        helper.Response("{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}