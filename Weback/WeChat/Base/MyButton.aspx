<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyButton.aspx.cs" Inherits="WeChat.Base.MyButton" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>自定义菜单-Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wlniao-media.css" />
    <link rel="stylesheet" href="../res/font-awesome/css/font-awesome.css" />
</head>
<body>
<div id="content" style=" margin:0px; height:800px; ">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="../base/mybutton.aspx" class="current">自定义菜单</a></div>   
  </div>
  <div class="container-fluid">
            <div style="width: 210px; float:left; background-color: transparent;">
                <div class="row-fluid">
                    <div class="widget-box">
                      <div class="widget-title">
                        <span class="icon tip-bottom" style=" float:right; cursor:pointer;" onclick="Add();" title="新增一级菜单"> <i class="icon-plus">&nbsp;&nbsp;新增</i></span>
                        <h5>一级菜单</h5>
                      </div>
                      <style type="text/css">
                          .account-ui .btn-group{ clear:both;margin:5px;}
                          .account-ui .btn-group .btn.nomarl{width:143px;}
                      </style> 
                      <div id="menuListTop" class="widget-content account-ui">
                      </div>
                    </div>
                </div>                
                <div id="dialogForm" class="widget-box" style=" display:none; ">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-info-sign"></i></span>
                        <h5>菜单信息:</h5>
                    </div>
                    <div class="widget-content nopadding">
                        <form class="form-horizontal" action="javascript:void(0);">
                        <div class="control-group" style=" padding-right:20px;">
                            <label class="control-label" for="required" style=" width:100px;">按钮名称</label>
                            <div class="controls" style=" margin-left:120px;">
                                <input type="text" class="grd-white" id="MenuName" placeholder="按钮描述，既按钮名字" style="width: 160px;" />
                            </div>
                        </div>
                        <div class="control-group" style=" padding-right:20px;">
                            <label class="control-label" for="required" style=" width:100px;">按钮KEY值</label>
                            <div class="controls" style=" margin-left:120px;">
                                <textarea id="MenuKey" class="span10" rows="5" cols="2" placeholder="按钮KEY值，用于消息接口(event类型)推送，不超过128字节" style="width: 160px;"></textarea>
                            </div>
                        </div>
                        <div class="form-actions">
                            <input type="hidden" id="opGuid" />
                            <button type="submit" class="btn btn-primary" onclick="save();">
                                保存</button>
                            <button type="button" class="btn" onclick="hide();">
                                取消</button>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
            <div style="width: 210px; margin-left:18px; float:left; background-color: transparent;">
                <div class="row-fluid">
                    <div class="widget-box">
                      <div class="widget-title">
                        <h5 id="menusubTitle">二级菜单</h5>
                      </div>
                      <style type="text/css">
                          .account-ui .btn-group{ clear:both;margin:5px;}
                          .account-ui .btn-group .btn.nomarl{width:143px;}
                      </style> 
                      <div id="menuListSub" class="widget-content account-ui">
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
<script src="../res/bootstrap.min.js" type="text/javascript"></script>
<script type="text/javascript">

    var opId = '';
    var opIdP = '';
    var opIdPt = '';
    function hide() {
        actorDialog.close();
        $('#MenuName').val('');
        $('#MenuKey').val('');
    }
    function loadListTop() {
        $.getJSON("mybutton.aspx", { "action": "gettops" }, function (data) {
            var str = '';
            $.each(data, function (i, item) {
                var temp = '<div class="btn-group">'
                          + '<button class="btn nomarl" onclick="loadListSubP(\'' + item.Id + '\',\'' + item.MenuName + '\');">' + item.MenuName + '</button>'
                          + '<button data-toggle="dropdown" class="btn dropdown-toggle"> <span class="caret"></span></button>'
                          + '<ul class="dropdown-menu">'
                          + '  <li><a href="javascript:void(0);" onclick="AddSub(\'' + item.Id + '\');">添加子菜单</a></li>'
                          + '  <li class="divider"></li>'
                          + '  <li><a href="javascript:void(0);" onclick="edit(\'' + item.Id + '\');">编辑当前菜单</a></li>'
                          + '  <li><a href="javascript:void(0);" onclick="del(\'' + item.Id + '\');">删除当前菜单</a></li>'
                          + '</ul>'
                        + '</div>';
                str = str + temp;
            });
            str=str+'<div style="text-align:center;"><a href="javascript:sync();">同步自定义菜单</a></div>';
            $('#menuListTop').html(str);
        });
    }
    function loadListSubP(id,title) {
        if (id) {
            $.getJSON("mybutton.aspx", { "action": "getsub", "pid": id }, function (data) {
                var str = '';
                $.each(data, function (i, item) {
                    opIdPt = id;
                    var temp = '<div class="btn-group">'
                          + '<button class="btn nomarl">' + item.MenuName + '</button>'
                          + '<button data-toggle="dropdown" class="btn dropdown-toggle"> <span class="caret"></span></button>'
                          + '<ul class="dropdown-menu">'
                          + '  <li><a href="javascript:void(0);" onclick="edit(\'' + item.Id + '\');">编辑当前菜单</a></li>'
                          + '  <li><a href="javascript:void(0);" onclick="del(\'' + item.Id + '\');">删除当前菜单</a></li>'
                          + '</ul>'
                        + '</div>';
                    str = str + temp;
                });
                $('#menuListSub').html(str);
            });
        }
        if (title) {
            $('#menusubTitle').html(title + "  子级菜单");
        }
    }
    function loadListSub() {
        loadListSubP(opIdP, '');
    }
    loadListTop();
    loadListSub();
    function Add() {
        opId = '';
        opIdP = '';
        $('#MenuName').val('');
        $('#MenuKey').val('');
        actorDialog = $.dialog({
            lock: true,
            fixed: true,
            content: document.getElementById('dialogForm'),
            id: 'fromDialog'
        });
    }
    function AddSub(id) {
        opId = '';
        opIdP = id;
        $('#MenuName').val('');
        $('#MenuKey').val('');
        actorDialog = $.dialog({
            lock: true,
            fixed: true,
            content: document.getElementById('dialogForm'),
            id: 'fromDialog'
        });
    }
    function edit(id) {
        opId = id;
        opIdP = '';
        $('#MenuName').val('');
        $('#MenuKey').val('');
        $.getJSON("mybutton.aspx", { "action": "get", "id": id }, function (json) {
            $('#MenuName').val(json.MenuName);
            $('#MenuKey').val(json.MenuKey);
            actorDialog = $.dialog({
                lock: true,
                fixed: true,
                content: document.getElementById('dialogForm'),
                id: 'fromDialog'
            });
        });
    }
    function del(id) {
        if (id) {
            $.dialog({
                lock: true,
                fixed: true,
                content: '注意：<br/>1、菜单删除后无法恢复，需谨慎操作；<br/>2、有子菜单的菜单禁止删除；<br/>您确定要删除当前菜单吗？',
                ok: function () {
                    $.getJSON("mybutton.aspx", { "action": "del", "Id": id }, function (json) {
                        loadListTop();
                        loadListSub();
                        if (json.success) {
                            $.dialog({
                                time: 2,
                                lock: true,
                                fixed: true,
                                icon: 'succeed',
                                content: 'Success,菜单删除成功！',
                                ok: function () {
                                    loadListTop();
                                    loadListSub();
                                    actorDialog.close();
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
        } else {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,请先选中需要删除的角色'
            });
        }
    }
    function save() {
        $.getJSON("mybutton.aspx", { "action": "save", "Id": opId, "Pid": opIdP, "MenuName": $('#MenuName').val(), "MenuKey": $('#MenuKey').val() }, function (json) {
            $.dialog.tips('正在为您提交数据！');
            if (json.success) {
                $('#MeunName').val('');
                $('#MenuKey').val('');
                loadListTop();
                if (opIdP) {
                    loadListSubP(opIdP, '');
                } else {
                    loadListSubP(opIdPt, '');
                }
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,菜单保存成功！',
                    ok: function () {
                        loadListTop();
                        if (opIdP) {
                            loadListSubP(opIdP, '');
                        } else {
                            loadListSubP(opIdPt, '');
                        }
                        actorDialog.close();
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
    }
    function sync() {
        $.getJSON("mybutton.aspx", { "action": "sync"}, function (json) {
                        if (json.success) {
                            $.dialog({
                                lock: true,
                                fixed: true,
                                icon: 'succeed',
                                content: '菜单同步成功'
                            });succeed
                        } else {
                            $.dialog({
                                lock: true,
                                fixed: true,
                                icon: 'error',
                                content: json.msg
                            });
                        }
        });
    }
</script>
</body>
</html>

