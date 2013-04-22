<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeChat.Default" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>Weback|微信公众帐号管理系统 - <%=UIConfig("AppName")%></title><%= GetTemplate("_meta") %>
</head>
<body>
<%= GetTemplate("_header") %>
<%= GetTemplate("_menu") %>
<div id="content" style="margin-top:-38px;"></div>
<%= GetTemplate("_footer") %>
<%= GetTemplate("_loader") %>
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

