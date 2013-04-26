<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WeChat.Login" %><!DOCTYPE html>
<html lang="en">    
<head>
    <title>Weback|微信公众帐号管理系统 - <%=UIConfig("AppName")%></title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="/res/bootstrap.min.css" />
    <link rel="stylesheet" href="/res/font-awesome/css/font-awesome.css" />
    <style type="text/css">
        html, body {width: 100%;height: 100%;}
        body {overflow-x: hidden;margin-top: -10px;  font-family: 'Open Sans', sans-serif; font-size:12px; color:#666;}
        a{color:#666;}a:hover, a:focus { text-decoration: none; color:#28b779;}
        .dropdown-menu .divider{ margin:4px 0px;}
        .dropdown-menu{ min-width:180px;}
        .dropdown-menu > li > a{ padding:3px 10px; color:#666; font-size:12px;}
        .dropdown-menu > li > a i{ padding-right:3px;}
        .userphoto img{ width:19px; height:19px;}
        select, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"], input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"], input[type="search"], input[type="tel"], input[type="color"], .uneditable-input, .label, .dropdown-menu, .btn, .well, .progress, .table-bordered, .btn-group > .btn:first-child, .btn-group > .btn:last-child, .btn-group > .btn:last-child, .btn-group > .dropdown-toggle, .alert{ border-radius:0px;}
        .btn, textarea, input[type="text"], input[type="password"], input[type="datetime"], input[type="datetime-local"], input[type="date"], input[type="month"], input[type="time"], input[type="week"], input[type="number"], input[type="email"], input[type="url"], input[type="search"], input[type="tel"], input[type="color"], .uneditable-input{ box-shadow:none;}
        .progress, .progress-success .bar, .progress .bar-success, .progress-warning .bar, .progress .bar-warning, .progress-danger .bar, .progress .bar-danger, .progress-info .bar, .progress .bar-info, .btn, .btn-primary{background-image:none;}
        .accordion-heading h5{ width:70%; }
        .form-horizontal .form-actions{ padding-left:20px; }
        #footer{ padding:10px; text-align:center;}
        hr{ border-top-color:#dadada;}.carousel{ margin-bottom:0px;}
        .fl { float:left}.fr {float:right}
        .label-important, .badge-important{ background:#f74d4d;}
        .bg_lb{ background:#27a9e3;}
        .bg_db{ background:#2295c9;}
        .bg_lg{ background:#28b779;}
        .bg_dg{ background:#28b779;}
        .bg_ly{ background:#ffb848;}
        .bg_dy{ background:#da9628;}
        .bg_ls{ background:#2255a4;}
        .bg_lo{ background:#da542e;}
        .bg_lr{ background:#f74d4d;}
        .bg_lv{ background:#603bbc;}
        .bg_lh{ background:#b6b3b3;}

        body { background-color:#2E363F;    padding: 0;    margin-top:0%;}
        #logo, #loginbox {    width: 32%;    margin-left: auto;    margin-right: auto;    position: relative;}
        #logo img {  margin: 0 auto;    display: block;}
        #loginbox { overflow: hidden !important;    text-align: left;    position: relative; }
        #loginbox form{ width:100%; background:#2E363F; position:relative; top:0; left:0; }
        #loginbox .form-actions { padding: 14px 20px 15px;}
        #loginbox .form-actions .pull-left { margin-top:0px;}
        #loginbox form#loginform { z-index: 200; display:block;}
        #loginbox form#recoverform { z-index: 100;     display:none;}
        #loginbox form#recoverform .form-actions {    margin-top: 10px;}
        #loginbox .main_input_box { margin:0 auto; text-align:center; font-size:13px;}
        #loginbox .main_input_box .add-on{  padding:9px 9px; *line-height:31px; color:#fff;  width:30px; display:inline-block;}
        #loginbox .main_input_box input{ height:30px; border:0px; display:inline-block; width:75%; line-height:28px;  margin-bottom:3px;}
        #loginbox .controls{ padding:0 20px;}
        #loginbox .control-group{ padding:20px 0; margin-bottom:0px;}
        .form-vertical, .form-actions {  margin-bottom: 0; background:none; border-top:1px solid #3f4954; }
        #loginbox .normal_text{ padding:15px 10px; text-align:center; font-size:14px; line-height:20px; background:#2E363F; color:#fff; }
        @media (max-width:800px){#logo { width: 60%; } #loginbox{ width:80%}}
        @media (max-width: 480px){#logo { width: 40%; }#loginbox{ width:90%;}#loginbox .control-group{ padding:8px 0; margin-bottom:0px;}}
    </style>
</head>
    <body>
        <div id="loginbox">
			<div class="control-group normal_text"> <h3><img src="/res/img/logo-big.png" alt="Wlniao未来鸟软件" /></h3></div>
        <%if (Tool.GetConfiger("HasInit") == "true")
          { %>
            <form id="loginform" class="form-vertical" action="javascript:void(0);" style="height:580px;">
                <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_lg"><i class="icon-user"></i></span><input id="login_username" type="text" onkeydown="KeyDownToGo(event);" placeholder="管理员帐号" />
                        </div>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_ly"><i class="icon-lock"></i></span><input id="login_password" type="password" onkeydown="KeyDown(event);" placeholder="登录密码" />
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <span class="pull-right"><a href="#" id="do-login" class="btn btn-success" >登录</a></span>
                </div>
            </form>
            <%}
          else
          { %>
            <form id="installform" action="#" class="form-vertical">
				<p class="normal_text">系统初始化</p>
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_ls"><i class="icon-user"></i></span><input id="install_username" type="text" placeholder="超级管理员帐号" />
                        </div>
                    </div>
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_ly"><i class="icon-lock"></i></span><input id="install_password" type="password" placeholder="登录密码" />
                        </div>
                    </div>
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on bg_lo"><i class="icon-lock"></i></span><input id="install_repassword" type="password" placeholder="确认登录密码" />
                        </div>
                    </div>
                    <div class="form-actions">
                        <span class="pull-right"><a class="btn btn-info" id="to-install">立即初始化</a></span>
                    </div>
            </form>
            <%} %>
        </div>
        
        <script src="/res/jquery.min.js"></script> 
        <script src="/res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
        <script src="/res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
        <script type="text/javascript" src="http://tajs.qq.com/stats?sId=23924686" charset="UTF-8"></script>
        <script type="text/javascript">
            function KeyDownToGo(event) {
                if (event.keyCode == 13) {
                    $('#login_password').focus();
                }
            }
            function KeyDown(event) {
                if (event.keyCode == 13) {
                    goLogin();
                }
            }

            function goLogin() {
                if (!$('#login_username').val()) {
                    $.dialog.tips('管理员帐号未填写，请填写后再提交', 2);
                    $('#login_username').focus();
                }
                else if (!$('#login_password').val()) {
                    $.dialog.tips('登录密码未填写，请填写后再提交', 2);
                    $('#login_password').focus();
                } else {
                    var dialog = art.dialog({
                        id: 'login',
                        lock: true,
                        drag: false,
                        resize: false,
                        title: '管理平台登录',
                        content: '正在校验您的帐号密码,请稍等！',
                        ok: function () {
                            return false;
                        },
                        close: function () {
                        },
                        button: [{
                            name: '确定',
                            disabled: true,
                            focus: true
                        }]
                    });
                    dialog.button({
                        name: '确定',
                        callback: function () {
                        },
                        disabled: true
                    });
                    $.getJSON("login.aspx", {
                        "do": "login",
                        "username": $('#login_username').val(),
                        "password": $('#login_password').val()
                    }, function (json) {
                        if (json.success) {
                            dialog.content('Success,登录成功,点击确定转向系统首页!<br/><span color="gray">3秒后系统也会自动为您跳转</span>');
                            dialog.button({
                                name: '确定',
                                callback: function () {
                                    top.location.href = 'default.aspx';
                                },
                                disabled: false
                            });
                            setTimeout(function () {
                                top.location.href = 'default.aspx';
                            }, 3000);
                        } else {
                            dialog.close();
                            $.dialog({
                                lock: true,
                                icon: 'error',
                                title: '错误提示',
                                content: json.msg
                            });
                        }
                    });
                }
            }
            $(document).ready(function () {

                var login = $('#loginform');
                var recover = $('#recoverform');
                var speed = 400;

                $('#to-recover').click(function () {

                    $("#loginform").slideUp();
                    $("#recoverform").fadeIn();
                });
                $('#to-login').click(function () {

                    $("#recoverform").hide();
                    $("#loginform").fadeIn();
                });
                $('#to-install').click(function () {
                    if (!$('#install_username').val()) {
                        $.dialog.tips('管理员帐号未填写，请填写后再提交', 2);
                        $('#install_username').focus();
                    }
                    else if (!$('#install_password').val()) {
                        $.dialog.tips('登录密码未填写，请填写后再提交', 2);
                        $('#install_password').focus();
                    }
                    else if ($('#install_repassword').val() != $('#install_password').val()) {
                        $.dialog.tips('您两次输入的密码不一致，请返回修改', 2);
                        $('#install_repassword').focus();
                    } else {
                        var dialog = art.dialog({
                            id: 'install',
                            lock: true,
                            drag: false,
                            resize: false,
                            title: '系统初始化',
                            content: '正在为您初始化系统,请稍等！',
                            ok: function () {
                                return false;
                            },
                            close: function () {
                            },
                            button: [{
                                name: '确定',
                                disabled: true,
                                focus: true
                            }]
                        });
                        dialog.button({
                            name: '确定',
                            callback: function () {
                            },
                            disabled: true
                        });
                        $.getJSON("login.aspx", {
                            "do": "init",
                            "username": $('#install_username').val(),
                            "password": $('#install_password').val()
                        }, function (json) {
                            if (json.success) {
                                setTimeout(function () {
                                    dialog.content('Success,系统初始化成功，赶紧登录您的系统吧!');
                                    dialog.button({
                                        name: '确定',
                                        callback: function () {
                                            window.location.reload();
                                        },
                                        disabled: false
                                    });
                                }, 5000);
                            } else {
                                dialog.close();
                                $.dialog({
                                    lock: true,
                                    icon: 'error',
                                    title: '错误提示',
                                    content: json.msg
                                });
                            }
                        });
                    }
                });
                $('#do-login').click(function () {
                    goLogin();
                });
                if ($.browser.msie == true && $.browser.version.slice(0, 3) < 10) {
                    $('input[placeholder]').each(function () {
                        var input = $(this);
                        $(input).val(input.attr('placeholder'));
                        $(input).focus(function () {
                            if (input.val() == input.attr('placeholder')) {
                                input.val('');
                            }
                        });
                        $(input).blur(function () {
                            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                                input.val(input.attr('placeholder'));
                            }
                        });
                    });
                }
            });
        </script>
    </body>
</html>
