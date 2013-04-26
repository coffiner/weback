<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="WeChat.Base.Setting" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>基础设置-<%=UIConfig("AppName")%></title><%= GetTemplate("_meta") %>
</head>
<body>
<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="/main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="/base/setting.aspx" class="current">基础设置</a></div>   
  </div>
  <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>
                                基本情况:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);">
                            <div class="control-group">
                                <label class="control-label">
                                    微信名称</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="WeChatName" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    微信帐号</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="AccountName" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    Token</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="WeChatToken" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    欢迎词</label>
                                <div class="controls">
                                    <textarea id="Subscribe" class="span10" style=" min-width:300px;" rows="7" cols="2"
                                        placeholder="用户订阅时需要向用户发送的内容！"></textarea>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    无匹配回复</label>
                                <div class="controls">
                                    <textarea id="NoMessage" class="span10" style=" min-width:300px;" rows="7" cols="2"
                                        placeholder="未执行任何规则时默认回复的内容！"></textarea>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    简介</label>
                                <div class="controls">
                                    <textarea id="Intro" class="span10" style=" min-width:300px;" rows="7" cols="2"
                                        placeholder="您可以在此设置机构的简单说明..."></textarea>
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
        $.getJSON("setting.aspx", { "action": "set"
        , "WeChatName": $('#WeChatName').val()
        , "AccountName": $('#AccountName').val()
        , "WeChatToken": $('#WeChatToken').val()
        , "Subscribe": $('#Subscribe').val()
        , "NoMessage": $('#NoMessage').val()
        , "Intro": $('#Intro').val()
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
                    fixed: true,
                    icon: 'error',
                    content: json.msg
                });
            }
        });
        return false;
    }
    function init() {
        $.getJSON("setting.aspx", { "action": "get" }, function (json) {
            $('#WeChatName').val(json.WeChatName);
            $('#AccountName').val(json.AccountName);
            $('#WeChatToken').val(json.WeChatToken);
            $('#Subscribe').val(getTextareaValue(json.Subscribe));
            $('#NoMessage').val(getTextareaValue(json.NoMessage));
            $('#Intro').val(getTextareaValue(json.Intro));
        });
    }
    init();
</script>
</body>
</html>

