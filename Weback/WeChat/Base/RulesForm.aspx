<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesForm.aspx.cs" Inherits="WeChat.Base.RulesForm" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>基础设置-<%=UIConfig("AppName")%></title>
    <%= GetTemplate("_meta") %>
    <link href="../res/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="/main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="<%=_GobackUrl %>" title="点击返回规则列表" class="tip-bottom">规则列表</a> <a href="#" class="current">规则详细</a></div>   
  </div>

<!--Action boxes-->
  <div class="container-fluid">
            <div class="row-fluid">
                <div class="span6">
                    <div class="widget-box" id="baseFrom">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>规则详细:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);">
                            <div class="control-group">
                                <label class="control-label">
                                    名称</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="RuleName" />
                                </div>
                            </div>
                            <div class="control-group" style="<%=_DoMethodDisplay%>">
                                <label class="control-label">
                                    执行函数</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="DoMethod" />
                                </div>
                            </div>
                            <div class="control-group" style="<%=_ReContentDisplay%>">
                                <label class="control-label">回复内容</label>
                                <div class="controls">
                                    <textarea id="ReContent" class="span10" rows="7" cols="2" data-form="wysihtml5" placeholder=""></textarea>
                                </div>
                            </div>
                            <div class="control-group" style="<%=_DoMethodDisplay%>">
                                <label class="control-label">
                                    回调内容</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="CallBackText" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">使用帮助</label>
                                <div class="controls">
                                    <textarea id="RuleHelp" class="span10" rows="7" cols="2" data-form="wysihtml5" placeholder=""></textarea>
                                </div>
                            </div>
                            <div class="form-actions">
                                <button type="submit" class="btn btn-primary" onclick="submitForm();">保存</button>
                                <a class="btn" href="<%=_GobackUrl %>">返回规则列表</a>
                            </div>
                            </form>
                        </div>
                    </div>
                    <div class="widget-box" id="codeFrom" style=" display:none;">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>匹配关键字:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);">
                            <div class="control-group">
                                <label class="control-label">
                                    关键字</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="Code" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    方式</label>
                                <div class="controls">
                                    <div data-toggle="buttons-radio" class="btn-group">
                                      <button class="btn btn-primary" type="button" onclick="setSepType('#');">完全匹配</button>
                                      <button class="btn btn-primary" type="button" onclick="setSepType('$');">包含匹配项</button>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <button type="submit" class="btn btn-primary" onclick="submitCode();">保存</button>
                                <a class="btn" href="javascript:hideCode();">取消</a>
                            </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="span6" style="<%=_Display%> ">
                    <div class="widget-box">
                         <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
                            <div style=" float:right; padding:3px;">
                                <a class="btn btn-primary" href="javascript:addCode();">新建项</a>
                            </div>
                            <h5>匹配内容列表</h5>
                        </div>
                        <div class="mini-fit" style="clear:both;height:461px;">
                            <div id="datagrid" class="mini-datagrid" style="width:100%;height:100%" pageSize="300" showFooter="false" showPageIndex="true" showLoading="false" showTotalCount="true" allowResize="false" url="rulesform.aspx?action=getlist&RuleGuid=<%=_Guid %>" idField="Guid" multiSelect="true" >
                                <div property="columns">
                                    <div type="indexcolumn" width="25"></div>
                                    <div field="Code" width="180" headerAlign="center" align="left" allowSort="false">匹配项</div>
                                    <div field="SepType" width="60" headerAlign="center" align="center" allowSort="false" renderer="onSepType">方式</div>
                                    <%--<div field="HitCount" width="60" headerAlign="center" align="center" allowSort="false" renderer="onHitCount">命中次数</div>--%>
                                    <div field="Guid" width="60" headerAlign="center" align="center" allowSort="false" renderer="onOp">操作</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
  </div>
</div>

    <%= GetTemplate("_loader") %>
<script src="../res/miniui/miniui.js" type="text/javascript"></script>
<script type="text/javascript">
    mini.parse();
    var grid = mini.get("datagrid");
    grid.load();
    function onSepType(e) {
        if (e.value == '#') {
            return '<a title="用户发送的内容和当前组任意匹配项一直时才触发规则">完全匹配</a>';
        } else {
            return '<a title="用户发送的内容包含当前组所有单个匹配项即触发规则">包含匹配项</a>';
        }
    }
    function onHitCount(e) {
        return '<a title="用户发送的内容和当前组任意匹配项匹配时便记一次命中数,当命中次数大于等于 ' + e.value + ' 时触发本规则">' + e.value + '</a>';
    } 
    function onOp(e) {
        return '<a href="javascript:editCode(\'' + e.value + '\',\'' + e.record.Code + '\',\'' + e.record.SepType + '\');">编辑</a>&nbsp;<a href="javascript:delCode(\'' + e.value + '\');">删除</a>';
    }
    var codeguid;
    var SepType;
    function setSepType(sep) {
        if (sep == '$') {
            SepType = '$';
        } else {
            SepType = '#';
        }
    }
    function addCode() {
        $('#baseFrom').hide();
        $('#codeFrom').show();
        $('#Code').val('');
        codeguid = '';
        SepType = '#';
    }
    function hideCode() {
        $('#baseFrom').show();
        $('#codeFrom').hide();
        $('#Code').val('');
        codeguid = '';
        SepType = '#';
    }
    function editCode(guid,code,septype) {
        $('#baseFrom').hide();
        $('#codeFrom').show();
        $('#Code').val(code);
        SepType = septype;
        codeguid = guid;
    }
    function submitForm() {
        $.getJSON("rulesform.aspx", { "action": "set", "guid": "<%=_Guid %>"
        , "RuleName": $('#RuleName').val()
        , "DoMethod": $('#DoMethod').val()
        , "ReContent": $('#ReContent').val()
        , "CallBackText": $('#CallBackText').val()
        , "RuleHelp": $('#RuleHelp').val()
        }, function (json) {
            if (json.success) {
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                        if (!"<%=_Guid %>") {
                            self.location.href = "<%=_GobackUrl %>";
                        }
                    }
                });
            } else {
                $.dialog({
                    fixed: true,
                    icon: 'error',
                    content: json.msg
                });
            }
        });
        return false;
    }
    function delCode(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>数据删除后无法恢复，需谨慎操作；<br/>您确定要删除当前匹配项吗？',
            ok: function () {
                $.getJSON("rulesform.aspx", { "action": "delcode", "Guid": guid }, function (json) {
                    if (json.success) {
                        hideCode();
                        grid.reload();
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！', 
                            close: function () {
                                grid.reload();
                            }
                        });
                    } else {
                        $.dialog({
                            lock: true,
                            fixed: true,
                            icon: 'error',
                            content: json.msg
                        });
                    }
                });
            },
            okVal: '是,删除',
            cancelVal: '取消',
            cancel: true //为true等价于function(){}
        });
    }
    function submitCode() {
        $.getJSON("rulesform.aspx", { "action": "setcode", "RuleGuid": "<%=_Guid %>"
        , "guid": codeguid
        , "Code": $('#Code').val()
        , "SepType": SepType
        }, function (json) {
            if (json.success) {
                hideCode();
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                    }, close: function () {
                        grid.reload();
                    }
                });
            } else {
                $.dialog({
                    fixed: true,
                    icon: 'error',
                    content: json.msg
                });
            }
        });
        return false;
    }
    function init() {
        $.getJSON("rulesform.aspx", { "action": "get", "guid": "<%=_Guid %>" }, function (json) {
            $('#RuleName').val(json.RuleName);
            $('#DoMethod').val(json.DoMethod);
            $('#ReContent').val(json.ReContent);
            $('#CallBackText').val(json.CallBackText);
            $('#RuleHelp').val(json.RuleHelp);
        });
    }
    init();
</script>
</body>
</html>

