<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WeChat.Base.Account" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>帐号设置-<%=UIConfig("AppName")%></title><%= GetTemplate("_meta") %>
</head>
<body>
<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="/main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="/base/Account.aspx" class="current">帐号设置</a></div>   
  </div>
  <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>
                                帐号设置:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);" style="height:520px;">
                            <div class="control-group">
                                <label class="control-label">
                                    用户名</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="Username" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    登录密码</label>
                                <div class="controls">
                                    <input type="password" class="grd-white" id="Password" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    确认密码</label>
                                <div class="controls">
                                    <input type="password" class="grd-white" id="RePassword" />
                                </div>
                            </div>
                            <div class="form-actions">
                                <button type="submit" class="btn btn-primary" onclick="submitForm();">
                                    保存</button>
                                <button type="button" class="btn" onclick="init();">
                                    取消</button>
                            </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
  </div>
</div>

    <%= GetTemplate("_loader") %>
<script type="text/javascript">
    function submitForm() {
        if (!$('#Username').val()) {
            $.dialog({
                lock: true,
                fixed: true,
                icon: 'error',
                content: '用户名未填写，请填写！'
            });
            return false;
        }
        if (!$('#Password').val()) {
            $.dialog({
                lock: true,
                fixed: true,
                icon: 'error',
                content: '登录密码未填写，请填写！'
            });
            return false;
        }
        if ($('#Password').val()!=$('#RePassword').val()) {
            $.dialog({
                lock: true,
                fixed: true,
                icon: 'error',
                content: '两次输入的密码不一致，请返回修改！'
            });
            return false;
        }
        $.getJSON("account.aspx", { "action": "set"
        , "Username": $('#Username').val()
        , "Password": $('#Password').val()
        }, function (json) {
            if (json.success) {
                $.dialog({
                    time: 2,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！'
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
        return false;
    }
    function init() {
        $.getJSON("account.aspx", { "action": "get" }, function (json) {
            $('#Username').val(json.Username);
        });
    }
    init();
</script>
</body>
</html>

