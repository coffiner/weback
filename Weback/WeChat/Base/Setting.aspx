<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="WeChat.Base.Setting" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>基础设置-Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wlniao-media.css" />
    <link rel="stylesheet" href="../res/font-awesome/css/font-awesome.css" />
</head>
<body>
<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="../base/setting.aspx" class="current">基础设置</a></div>   
  </div>
  <div class="container-fluid">
    <table style="width:100%;">
        <tr>
        <td valign="top" style=" width:568px;">
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-info-sign"></i></span>
                    <h5>基本设置</h5>
                </div>
                <div class="widget-content nopadding">
                    <form class="form-horizontal" action="javascript:void(0);" style=" margin-right:75px;">
                    <div class="control-group">
                        <label class="control-label">微信名称</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="WeChatName" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">微信帐号</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="AccountName" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Token</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="WeChatToken" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            请设置接口为</label>
                        <div class="controls">
                            http://<%=_website%>/wechatapi.aspx
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            无匹配回复</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="NoMessage" placeholder="未执行任何规则时的默认回调内容！" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            会话失效时间</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="SessionTimeOut" placeholder="" style=" width:30px;" />
                            <span style="">单位：秒；为 0 则不起用会话机制</span>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            允许上传的文件类型</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="UploadExt" placeholder="文件类型列表，如：.jpg,.gif,.png" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Appid</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="Appid" placeholder="在公众平台申请内测资格，审核通过后可获得" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Secret</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="Secret" placeholder="在公众平台申请内测资格，审核通过后可获得" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            简介</label>
                        <div class="controls">
                            <textarea id="Intro" class="span10" style=" width:300px;" rows="7" cols="2"
                                placeholder="您可以在此设置机构的简单说明..."></textarea>
                        </div>
                    </div>
                    </form>
                    <div class="form-actions">
                        <button type="submit" class="btn btn-primary" onclick="submitForm();">
                            保存</button>
                        <button type="button" class="btn" onclick="init();">
                            取消</button>
                    </div>
                </div>
            </div>
        </td>
        <td valign="top" style=" width:350px; display:none; padding-left:18px;">
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-info-sign"></i></span>
                    <h5>页面设置</h5>
                </div>
                <div class="widget-content nopadding" style=" text-align:center;">
                    <br />
                    <br />
                    <br />
                    <br />
                    
                    <input type="text" class="grd-white" id="FootCopyRight" placeholder="此处填写底部版权内容" style=" text-align:center; width:300px;" />
                </div>
            </div>
        </td>
        <td>&nbsp;</td>    
        </tr>
    </table>
  </div>
</div>
<script src="../res/jquery.min.js"></script> 
<script src="../res/wlniao.js"></script> 
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    function submitForm() {
        $.getJSON("setting.aspx", { "action": "set"
        , "WeChatName": $('#WeChatName').val()
        , "AccountName": $('#AccountName').val()
        , "WeChatToken": $('#WeChatToken').val()
        , "NoMessage": $('#NoMessage').val()
        , "SessionTimeOut": $('#SessionTimeOut').val()
        , "UploadExt": $('#UploadExt').val()
        , "Appid": $('#Appid').val()
        , "Secret": $('#Secret').val()
        , "Intro": $('#Intro').val()
        , "FootCopyRight": $('#FootCopyRight').val()
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
                    lock: true,
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
            $('#NoMessage').val(json.NoMessage);
            $('#SessionTimeOut').val(json.SessionTimeOut);
            $('#UploadExt').val(json.UploadExt);
            $('#Appid').val(json.Appid);
            $('#Secret').val(json.Secret);
            $('#Intro').val(getTextareaValue(json.Intro));
            $('#FootCopyRight').val(json.FootCopyRight);
        });
    }
    init();
</script>
</body>
</html>

