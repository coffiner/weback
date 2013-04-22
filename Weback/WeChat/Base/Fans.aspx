<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fans.aspx.cs" Inherits="WeChat.Base.Fans" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>订阅者列表-<%=UIConfig("AppName")%></title>
    <%= GetTemplate("_meta") %>
    <link href="../res/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .aedit{width:80px; display:inline-block;color:gray; cursor:pointer; background-image:url("../res/img/icon-edit.png"); background-repeat:no-repeat; background-position:100px 5px;}
        .aedit:hover{color:blue;background-position:67px 5px;}
    </style>
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="/main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="/base/fans.aspx" class="current">订阅者列表</a></div>   
  </div>

        <div class="container-fluid">
            <div class="row-fluid">
              <div class="span12">
                <div class="widget-box">
                     <div class="widget-title"> <span class="icon"><i class="icon-th"></i></span>
                        <h5>我的微信订阅者</h5>
                    </div>
                    <div class="mini-fit" style="clear:both;height:560px;">
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:100%" pageSize="18" showPageIndex="true" showLoading="false" showTotalCount="true" allowResize="false" url="fans.aspx?action=getlist" idField="Guid" multiSelect="true" >
                            <div property="columns">
                                <div field="IsNewFans" width="18" headerAlign="center" align="center" allowSort="false" renderer="onIsNewFans">&nbsp;</div>
                                <div field="NickName" width="40" headerAlign="center" align="center" allowSort="false" renderer="onNikeName">备注名</div>
                                <div field="Subscribe" width="40" headerAlign="center" align="center" allowSort="false" renderer="onSubscribe">订阅状态</div>
                                <div field="SubscribeTime" width="98" headerAlign="center" align="center" align="center" allowSort="false" renderer="onDataRenderer">订阅时间</div>
                                <div field="LastVisit" width="98" headerAlign="center" align="center" align="center" allowSort="false" renderer="onDataRendererLast">最后来访</div>
                                <div field="Sid" width="60" headerAlign="center" align="center" allowSort="false" renderer="onBind">绑定状态</div>
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
    function onDataRenderer(e) {
        return mini.formatDate(new Date(e.value), 'yyyy-MM-dd HH:mm:ss');
    }
    function onDataRendererLast(e) {
        var tipstr = '';
        if (e.record.LastArgs) {
            tipstr = e.record.LastArgs;
        }
        return '<a href="#" title="' + tipstr + '">' + mini.formatDate(new Date(e.value), 'yyyy-MM-dd HH:mm:ss') + '</a>';
    }
    function onNikeName(e) {
        if (e.value) {
            return '<a class="aedit" href="javascript:editNickname(\'' + e.record.Guid + '\',\'' + e.value + '\');">' + e.value + '</a>';
        } else {
        return '<a class="aedit" href="javascript:editNickname(\'' + e.record.Guid + '\',\'' + e.value + '\');">暂无</a>';
        }
    }
    function editNickname(guid,nickname) {
        $.dialog.prompt('请填写用户备注', function (val) {
            $.getJSON("fans.aspx", { "action": "setnickname", "Guid": guid, "NickName": val }, function (json) {
                if (json.success) {
                    grid.reload();
                } else {
                    $.dialog.tips(json.msg);
                }
            });
        }, nickname);
    }
    function onSubscribe(e) {
        if (e.record.Subscribe == 1) {
            return '<a style="color:green;">正常</a>'
        } else {
            return '<a style="color:gray;">已退订</a>'
        }
    }
    function onIsNewFans(e) {
        if (e.record.IsNewFans == 1) {
            return '<a style="color:red;">新</a>'
        } else {
            return '';
        }
    }
    function onBind(e) {
        if (e.value) {
            return '已绑定';
        } else {
            return '未绑定';
        }
    }
</script>
</body>
</html>

