<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeChat.Default" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title><%=_titleName%>|微信公众帐号管理系统 - <%=UIConfig("AppName")%></title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="res/bootstrap.min.css" />
    <link rel="stylesheet" href="res/wlniao-style.css" />
    <link rel="stylesheet" href="res/wlniao-media.css" />
    <link rel="stylesheet" href="res/font-awesome/css/font-awesome.css" />
</head>
<body>
<div id="header"><h1><a href="#" class="tip-bottom" title="{UI.AppName}">{UI.AppName}</a></h1></div>
<div id="user-nav" class="navbar navbar-inverse">
  <ul class="nav">
    <li class=""><a title="" href="javascript:GotoPage('base/setting.aspx');"><i class="icon icon-cog"></i> <span class="text">微信设置</span></a></li>
    <li class=""><a title="" href="javascript:GotoPage('base/fans.aspx');"><i class="icon icon-group"></i> <span class="text">订阅者</span><script src="base/fans.aspx?action=getnewcount"></script> </a></li>
    <li class=""><a title="" href="javascript:GotoPage('base/account.aspx');"><i class="icon icon-user"></i> <span class="text">管理员</span></a></li>
    <li class=""><a title="" href="login.aspx?do=logout"><i class="icon-key"></i> <span class="text">注销登录</span></a></li>
  </ul>
  <div style="clear:both;"></div>
</div>
<%= GetTemplate("_menu") %>
<div id="content" style="margin-top:-38px;"></div>
<%= GetTemplate("_footer") %>
<script src="res/jquery.min.js"></script>
<script src="res/wlniao.js"></script>
<script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript" src="http://tajs.qq.com/stats?sId=23924686" charset="UTF-8"></script>
<script type="text/javascript">
    var frameid = "iframepage";
    function setFrameHeight(height) {
        try {
            document.getElementById(frameid).height = height;
        } catch (e) { }
    }
    function iFrameHeight() {
        var ifm = document.getElementById(frameid);
        try {
            var subWeb = document.frames ? document.frames[frameid].document : ifm.contentDocument;
            if (ifm != null) {
                if (subWeb == null) {
                    ifm.height = ifm.ownerDocument.body.scrollHeight;
                    ifm.scrolling = "auto";
                } else {
                    ifm.height = subWeb.body.scrollHeight;
                }
            }
        } catch (e) {}
    }
    function GotoPage(url) {
        var now = new Date();
        var html = '<iframe id="' + frameid + '" src="' + url + '" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" width="100%" height="300"></iframe> ';
        document.getElementById('content').innerHTML = html;
        setTimeout(function () {
            iFrameHeight();
            setTimeout(function () {
                iFrameHeight();
                setTimeout(function () {
                    iFrameHeight();
                }, 2000);
            }, 1000);
        }, 300);
    }
    GotoPage('main.aspx');
</script>
</body>
</html>

