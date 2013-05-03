<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fans.aspx.cs" Inherits="WeChat.Base.Fans" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>订阅者列表-<%=UIConfig("AppName")%></title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wlniao-media.css" />
    <link rel="stylesheet" href="../res/font-awesome/css/font-awesome.css" />
    <link href="../res/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .aedit{width:80px; display:inline-block;color:gray; cursor:pointer; background-image:url("../res/img/icon-edit.png"); background-repeat:no-repeat; background-position:100px 5px;}
        .aedit:hover{color:blue;background-position:67px 5px;}
    </style>
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="../base/fans.aspx" class="current">订阅者列表</a></div>   
  </div>
        <div class="container-fluid">
            <div class="row-fluid">
              <div class="span12">
                <div class="widget-box">
                     <div class="widget-title">
                        <div style=" width:300px; text-align:right; float:right; padding:3px;">
                            <a class="btn btn-primary" href="javascript:Search();" style=" float:right;">筛选</a>
                            <input type="text" class="grd-white" id="SearchKey" onchange="Search();" />
                        </div>
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
                                <div field="AllowTest" width="60" headerAlign="center" align="center" allowSort="false" renderer="onAllowTest">测试功能</div>
                            </div>
                        </div>
                    </div>
                </div>
              </div>
            </div>
        </div>

</div>

<script src="../res/jquery.min.js"></script> 
<script src="../res/wlniao.js"></script> 
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script src="../res/miniui/miniui.js" type="text/javascript"></script>
<script type="text/javascript">
    mini.parse();
    var grid = mini.get("datagrid");
    grid.load();
    function Search() {    
        grid.load({ "key": $('#SearchKey').val() }); 
    }
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
    function onAllowTest(e) {
        if (e.value == 1) {
            return '<a class="aedit" style="color:green;" href="javascript:editAllowTest(\'' + e.record.Guid + '\',\'' + e.value + '\');">已开启</a>'
        } else {
            return '<a class="aedit" style="color:gray;" href="javascript:editAllowTest(\'' + e.record.Guid + '\',\'' + e.value + '\');">未开启</a>'
        }
    }
    function editAllowTest(guid, allowtest) {
        var msg = "您确定要为当前用户开启测试功能吗?";
        if (allowtest == 1) {
            allowtest = 0;
            msg = "您确定要禁止当前用户使用测试功能吗?";
        } else {
            allowtest = 1;
        }
        $.dialog({
            content: msg,
            lock: true,
            ok: function () {
                $.getJSON("fans.aspx", { "action": "setallowtest", "Guid": guid, "AllowTest": allowtest }, function (json) {
                    if (json.success) {
                        grid.reload();
                    } else {
                        $.dialog.tips(json.msg);
                    }
                });
            },
            cancelVal: '取消',
            cancel: true
        });
    }
</script>
</body>
</html>

