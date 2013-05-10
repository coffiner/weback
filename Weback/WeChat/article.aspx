<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="article.aspx.cs" Inherits="WeChat.article" %>
<!DOCTYPE HTML>
<html>
<head>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;">
    <meta charset="utf-8">
    <title><%=_Title%></title>
    <link href="res/css/page-style.css" rel="stylesheet" type="text/css" />
    <script src="res/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('.nav').hide();
            $('#show').click(function () {
                $(".nav").toggle();
                $("span").toggle();
            });
            var winWidth = $(window).width();
            $("#pageTitle").width(winWidth - 80);
            $(".blog-banner").width(winWidth - 20);
        });
    </script>
</head>
<body>
    <div class="header">
        <div class="title">
            <h1 id="pageTitle"><%=_Title%></h1>
        </div>
        <%--<button id="show">更多 <span>+</span> <span style="display:none;">-</span></button>--%>
        <div class="clear"></div>
    </div>
    <div class="nav">
        <ul>
            <li><a href="#">主页</a></li>
            <li><a href="#">精品推荐</a></li>
            <li><a href="#">优惠活动</a></li>
            <li><a href="#">在线留言</a></li>
        </ul>
    </div>
    <div class="content">
        <div class="blog">
            <h2><a href="#"><%=_Title%></a></h2>
            <div class="blog-img"><a href="#"><img class="blog-banner" src="<%=_PicUrl%>"></a></div>
            <div class="blog-content">
                <%=_Content%>
            </div>
        </div>
<%--        <div class="pagination">
            <ul>
                <li><a href="#"><</a></li>
                <li><a href="#">1</a></li>
                <li class="current"><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">></a></li>
            </ul>
        </div>--%>
    </div>
    <div class="footer">
        <%=_FootCopyRight%>
    </div>
</body>
</html>
