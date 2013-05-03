<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesForm.aspx.cs" Inherits="WeChat.Base.RulesForm" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>基础设置-<%=UIConfig("AppName")%></title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wlniao-media.css" />
    <link rel="stylesheet" href="../res/font-awesome/css/font-awesome.css" />
    <link href="../res/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="<%=_GobackUrl %>" title="点击返回规则列表" class="tip-bottom">规则列表</a> <a href="#" class="current">规则详细</a></div>   
  </div>

<!--Action boxes-->
  <div class="container-fluid">
            <div class="row-fluid">
                <div class="span6">
                    <div class="widget-box" id="baseFrom">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>规则详细:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);">
                            <div class="control-group">
                                <label class="control-label">
                                    名称</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="RuleName" placeholder="为规则填写一个名称，便于查看" />
                                </div>
                            </div>
                            <div class="control-group" style="<%=_DoMethodDisplay%>">
                                <label class="control-label">
                                    执行函数</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="DoMethod" placeholder="要执行的函数名,如Demo.Hello" />
                                </div>
                            </div>
                            <div class="control-group" style="<%=_ReContentDisplay%>">
                                <label class="control-label">优先回复内容</label>
                                <div class="controls">
                                    <textarea id="ReContent" class="span10" style=" min-width:300px;" rows="7" cols="2" placeholder="此处可不填写,为空时则发送回复列表中的内容,不为空时则优先使用此处的内容。"></textarea>
                                </div>
                            </div>
                            <div class="control-group" style="<%=_ReContentDisplay%>">
                                <label class="control-label">
                                    自动回复方式</label>
                                <div class="controls">
                                    <select id="SendMode">
                                        <option value="sendnew">顶部</option>
                                        <option value="sendrandom">随机</option>
                                        <option value="sendgroup">组合（仅图文）</option>
                                    </select>
                                </div>
                            </div>
                            <div class="control-group" style="<%=_DoMethodDisplay%>">
                                <label class="control-label">
                                    回调内容</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="CallBackText" placeholder="用于跳转到另外一个流程的指令" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">规则描述</label>
                                <div class="controls">
                                    <textarea id="RuleHelp" class="span10" style=" min-width:300px;" rows="7" cols="2" placeholder="如：使用帮助，功能说明等"></textarea>
                                </div>
                            </div>
                            <div class="form-actions">
                                <button type="submit" class="btn btn-primary" onclick="submitForm();">保存</button>
                                <a class="btn" href="<%=_GobackUrl %>">返回规则列表</a>
                            </div>
                            </form>
                        </div>
                    </div>
                    <div class="widget-box" id="codeFrom" style=" display:none;">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>匹配关键字:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);">
                            <div class="control-group">
                                <label class="control-label">
                                    关键字</label>
                                <div class="controls">
                                    <input type="text" class="grd-white" id="Code" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    方式</label>
                                <div class="controls">
                                    <div data-toggle="buttons-radio" class="btn-group">
                                      <button class="btn btn-primary" type="button" onclick="setSepType('#');">完全匹配</button>
                                      <button class="btn btn-primary" type="button" onclick="setSepType('$');">包含匹配项</button>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    状态</label>
                                <div class="controls">
                                    <select id="Status">
                                        <option value="normal">启用</option>
                                        <option value="close">停用</option>
                                        <option value="test">测试</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-actions">
                                <button type="submit" class="btn btn-primary" onclick="submitCode();">保存</button>
                                <a class="btn" href="javascript:hideCode();">取消</a>
                            </div>
                            </form>
                        </div>
                    </div>
                    <div class="widget-box" id="contentForm" style=" display:none;">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-info-sign"></i></span>
                            <h5>回复内容详情:</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <form class="form-horizontal" action="javascript:void(0);">
                            <div class="control-group">
                                <label class="control-label">
                                    标题</label>
                                <div class="controls">
                                    <input type="text" id="Title" />
                                </div>
                            </div>
                            <div class="control-group" id="divPicUrl" style=" display:none;">
                                <label class="control-label">
                                    图片</label>
                                <div class="controls">
                                    <input type="text" id="PicUrl" style=" display:none;" />
                                    <input id="swfUploadPic" class="mini-fileupload" name="Fdata" buttonText="浏览图片" limitSize="512KB" uploadOnSelect="true" limitType="*.png;*.jpg" flashUrl="../res/miniui/swfupload/swfupload.swf" uploadUrl="../upload.ashx?filetype=pic" onuploadsuccess="onUploadPicSuccess" />
                                    <%--<span>最大限制512KB</span>--%>
                                    <img id="imgPicUrl" src="#" style=" width:120px; height:60px; display:none;" />
                                </div>
                            </div>
                            <div class="control-group" id="divThumbPicUrl" style=" display:none;">
                                <label class="control-label">
                                    小图</label>
                                <div class="controls">
                                    <input type="text" id="ThumbPicUrl" style=" display:none;" />
                                    <input id="swfUploadThumbPic" class="mini-fileupload" name="Fdata" buttonText="浏览图片" limitSize="256KB" uploadOnSelect="true" limitType="*.png;*.jpg" flashUrl="../res/miniui/swfupload/swfupload.swf" uploadUrl="../upload.ashx?filetype=pic" onuploadsuccess="onUploadThumbPicSuccess" />
                                    <%--<span>最大限制512KB</span>--%>
                                    <img id="imgThumbPicUrl" src="#" style=" width:52px; height:52px; display:none;" />
                                </div>
                            </div>
                            <div class="control-group" id="divMusicUrl" style=" display:none;">
                                <label class="control-label">
                                    声音</label>
                                <div class="controls">
                                    <input type="text" id="MusicUrl" style=" display:none;" />
                                    <input id="swfUploadMusic" class="mini-fileupload" name="Fdata" buttonText="浏览媒体文件" limitSize="10MB" uploadOnSelect="true" limitType="*.mp3;*.avi;*.rm" flashUrl="/res/miniui/swfupload/swfupload.swf" uploadUrl="/upload.ashx?filetype=audio" onuploadsuccess="onUploadMusicSuccess" />
                                    <span>最大限制10MB</span>
                                    <br /><span id="mp3MusicUrl"></span>
                                </div>
                            </div>
                            <div class="control-group" id="divTextContent"  style=" display:none;">
                                <label class="control-label">文本</label>
                                <div class="controls">
                                    <textarea id="TextContent" class="span10" style=" min-width:300px;" rows="7" cols="2" placeholder="需要发送的文本内容。"></textarea>
                                </div>
                            </div>
                            <div class="control-group" id="divLinkUrl" style=" display:none;">
                                <label class="control-label">
                                    链接</label>
                                <div class="controls">
                                    <input type="text" id="LinkUrl" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    状态</label>
                                <div class="controls">
                                    <select id="ContentStatus">
                                        <option value="normal">启用</option>
                                        <option value="close">停用</option>
                                        <option value="test">测试</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-actions">
                                <button type="submit" class="btn btn-primary" onclick="submitContent();">保存</button>
                                <a class="btn" href="javascript:hideContent();">取消</a>
                            </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="span6" style="overflow:hidden; <%=_DisplayNew%> ">
                    <div class="widget-box" style=" border:none; ">                    
                        <img src="fulecodedemo.jpg" width="auto;" height="auto" />
                    </div>
                    <div class="widget-box" style=" border:none;<%=_ReContentDisplay%> ">                    
                        <img src="fulecontentdemo.jpg" width="auto;" height="auto" />
                    </div>
                </div>
                <div class="span6" style="<%=_Display%> ">
                    <div class="widget-box">
                         <div class="widget-title">
                            <div style=" float:right; padding:3px;">
                                <a class="btn btn-primary" href="javascript:addCode();">新建匹配项</a>
                            </div>
                            <h5>匹配内容列表</h5>
                        </div>
                        <div class="mini-fit" style="clear:both;height:168px; border-bottom:1px solid #cccccc;">
                            <div id="datagrid" class="mini-datagrid" style="width:100%;height:100%" pageSize="300" showFooter="false" showPageIndex="true" showLoading="false" showTotalCount="true" allowResize="false" url="rulesform.aspx?action=getlist&RuleGuid=<%=_Guid %>" idField="Guid" multiSelect="true" >
                                <div property="columns">
                                    <div type="indexcolumn" width="25"></div>
                                    <div field="Code" width="180" headerAlign="center" align="left" allowSort="false">匹配项</div>
                                    <div field="SepType" width="60" headerAlign="center" align="center" allowSort="false" renderer="onSepType">方式</div>
                                    <div field="Status" width="60" headerAlign="center" align="center" allowSort="false" renderer="onStatus">状态</div>
                                    <%--<div field="HitCount" width="60" headerAlign="center" align="center" allowSort="false" renderer="onHitCount">命中次数</div>--%>
                                    <div field="Guid" width="60" headerAlign="center" align="center" allowSort="false" renderer="onOpCode">操作</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget-box" style="<%=_ReContentDisplay%> ">
                        <div class="widget-title">
                            <div style=" float:right; padding:3px;">
                                <a class="btn btn-primary" href="javascript:addContent('text');">新文本</a>
                                <a class="btn btn-primary" href="javascript:addContent('pictext');">新图文</a>
                                <a class="btn btn-primary" href="javascript:addContent('music');">新声音</a>
                            </div>
                            <h5>回复列表</h5>
                        </div>
                        <div class="mini-fit" style="clear:both;height:218px; border-bottom:1px solid #cccccc;">
                            <div id="contentgrid" class="mini-datagrid" style="width:100%;height:100%" pageSize="300" showFooter="false" showPageIndex="true" showLoading="false" showTotalCount="true" allowResize="false" url="rulesform.aspx?action=getcontentlist&RuleGuid=<%=_Guid %>" idField="Guid" multiSelect="true" >
                                <div property="columns">
                                    <div type="indexcolumn" width="25" renderer="onTop"></div>
                                    <div field="Title" width="180" headerAlign="center" align="left" allowSort="false">标题</div>
                                    <div field="ContentType" width="60" headerAlign="center" align="center" allowSort="false" renderer="onContentType">类型</div>
                                    <div field="ContentStatus" width="60" headerAlign="center" align="center" allowSort="false" renderer="onStatus">状态</div>
                                    <div field="Guid" width="60" headerAlign="center" align="center" allowSort="false" renderer="onOpContent">操作</div>
                                </div>
                            </div>
                        </div>
                        <span>&nbsp;*注：如有图文内容，则优先回复图文列表，否则将从普通文本列表中随机采用一条数据。</span>
                    </div>
                </div>
            </div>
  </div>
</div>
<script src="../res/jquery.min.js"></script> 
<script src="../res/wlniao.js"></script> 
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script src="../res/jquery.jmp3.js" type="text/javascript"></script>
<script src="../res/miniui/miniui.js" type="text/javascript"></script>
<script src="../res/miniui/swfupload/swfupload.js" type="text/javascript"></script>
<script type="text/javascript">
    mini.parse();
    var grid = mini.get("datagrid");
    grid.load();
    var contentgrid = mini.get("contentgrid");
    contentgrid.load();
    function onTop(e) {
        return '<a title="使当前内容置顶" href="javascript:stickContent(\'' + e.record.Guid + '\');" style="color:gray;">↑</a>';
    }
    function onSepType(e) {
        if (e.value == '#') {
            return '<a title="用户发送的内容和当前组任意匹配项一直时才触发规则">完全匹配</a>';
        } else {
            return '<a title="用户发送的内容包含当前组所有单个匹配项即触发规则">包含匹配项</a>';
        }
    }
    function onContentType(e) {
        if (e.value == 'text') {
            return '<a title="纯文本内容">文本</a>';
        } else if (e.value == 'pictext') {
            return '<a title="图文内容">图文</a>';
        } else if (e.value == 'music') {
            return '<a title="音乐媒体文件">媒体</a>';
        } else {
            return '';
        }
    } 
    function onStatus(e) {
        if (e.value == 'close') {
            return '<a title="已停止使用">已停用</a>';
        } else if (e.value == 'test') {
            return '<a title="仅测试帐号可用">测试中</a>';
        } else {
            return '<a title="所有用户均可正常使用">已启用</a>';
        }
    }
    function onHitCount(e) {
        return '<a title="用户发送的内容和当前组任意匹配项匹配时便记一次命中数,当命中次数大于等于 ' + e.value + ' 时触发本规则">' + e.value + '</a>';
    } 
    function onOpCode(e) {
        return '<a href="javascript:editCode(\'' + e.value + '\',\'' + e.record.Code + '\',\'' + e.record.SepType + '\',\'' + e.record.Status + '\');">编辑</a>&nbsp;<a href="javascript:delContent(\'' + e.value + '\');">删除</a>';
    }
    function onOpContent(e) {
        return '<a href="javascript:editContent(\'' + e.value + '\',\'' + e.record.Title + '\',\'' + e.record.ContentType + '\',\'' + e.record.PicUrl + '\',\'' + e.record.ThumbPicUrl + '\',\'' + e.record.MusicUrl + '\',\'' + e.record.LinkUrl + '\',\'' + e.record.TextContent + '\',\'' + e.record.ContentStatus + '\');">编辑</a>&nbsp;<a href="javascript:delContent(\'' + e.value + '\');">删除</a>';
    }
    function submitForm() {
        try {
            if ($('#ReContent').val().length > 300) {
                $.dialog({
                    lock: true,
                    fixed: true,
                    content: '注意：<br/>您填写的自动回复内容太长可能会导致发送失败；<br/>微信自动回复的最大中文长度为300字符；<br/>您是否要返回修改？',
                    ok: function () {
                        GoonSave();
                    },
                    okVal: '不,继续保存',
                    cancelVal: '返回修改',
                    cancel: function () { }
                });
            } else {
                GoonSave();
            }
        } catch (e) { }
    }
    function GoonSave() {
        $.getJSON("rulesform.aspx", { "action": "set", "guid": "<%=_Guid %>"
        , "RuleName": $('#RuleName').val()
        , "DoMethod": $('#DoMethod').val()
        , "ReContent": $('#ReContent').val()
        , "CallBackText": $('#CallBackText').val()
        , "SendMode": $('#SendMode').val()
        , "RuleHelp": $('#RuleHelp').val()
        }, function (json) {
            if (json.success) {
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                        if (!"<%=_Guid %>") {
                            self.location.href = "<%=_GobackUrl %>";
                        }
                    }
                });
            } else {
                $.dialog({
                    fixed: true,
                    icon: 'error',
                    content: json.msg
                });
            }
        });
    }
    function init() {
        $.getJSON("rulesform.aspx", { "action": "get", "guid": "<%=_Guid %>" }, function (json) {
            $('#RuleName').val(json.RuleName);
            $('#DoMethod').val(json.DoMethod);
            $('#ReContent').val(getTextareaValue(json.ReContent));
            $('#CallBackText').val(json.CallBackText);
            $('#SendMode').val(json.SendMode);
            $('#RuleHelp').val(getTextareaValue(json.RuleHelp));
        });
    }
    init();
    var codeguid;
    var SepType;
    function setSepType(sep) {
        if (sep == '$') {
            SepType = '$';
        } else {
            SepType = '#';
        }
    }
    function addCode() {
        $('#baseFrom').hide();
        $('#contentForm').hide();
        $('#codeFrom').show();
        $('#Code').val('');
        $('#Status').val('normal');
        codeguid = '';
        SepType = '#';
    }
    function hideCode() {
        $('#baseFrom').show();
        $('#contentForm').hide();
        $('#codeFrom').hide();
        $('#Code').val('');
        $('#Status').val('normal');
        codeguid = '';
        SepType = '#';
    }
    function editCode(guid, code, septype, status) {
        $('#baseFrom').hide();
        $('#contentForm').hide();
        $('#codeFrom').show();
        $('#Code').val(code);
        $('#Status').val(status);
        SepType = septype;
        codeguid = guid;
    }
    function submitCode() {
        $.getJSON("rulesform.aspx", { "action": "setcode", "RuleGuid": "<%=_Guid %>"
        , "guid": codeguid
        , "Code": $('#Code').val()
        , "SepType": SepType
        , "Status": $('#Status').val()
        }, function (json) {
            if (json.success) {
                hideCode();
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                    }, close: function () {
                        grid.reload();
                    }
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
    function delCode(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>数据删除后无法恢复，需谨慎操作；<br/>您确定要删除当前匹配项吗？',
            ok: function () {
                $.getJSON("rulesform.aspx", { "action": "delcode", "Guid": guid }, function (json) {
                    if (json.success) {
                        hideCode();
                        grid.reload();
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
                            close: function () {
                                grid.reload();
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
    }
    var contentguid;
    var ContentType;
    function addContent(type) {
        $('#baseFrom').hide();
        $('#codeFrom').hide();
        $('#contentForm').show();

        $('#divLinkUrl').hide();
        $('#divPicUrl').hide();
        $('#divThumbPicUrl').hide();
        $('#divMusicUrl').hide();
        $('#divTextContent').hide();
        if (type == 'pictext') {
            $('#divPicUrl').show();
            $('#divThumbPicUrl').show();
            $('#divLinkUrl').show();
            $('#divTextContent').show();
        } else if (type == 'music') {
            $('#divMusicUrl').show();
            $('#divTextContent').show();
        } else {
            $('#divTextContent').show();
        }
        mini.get("swfUploadPic").setText('');
        mini.get("swfUploadMusic").setText('');
        $('#imgPicUrl').attr('src', '#').hide();
        $('#imgThumbPicUrl').attr('src', '#').hide();
        //document.getElementById("PicUrlUpf").outerHTML = document.getElementById("PicUrlUpf").outerHTML;
        //document.getElementById("MusicUrlUpf").outerHTML = document.getElementById("MusicUrlUpf").outerHTML;

        $('#Title').val('');
        $('#PicUrl').val('');
        $('#ThumbPicUrl').val('');
        $('#MusicUrl').val('');
        $('#LinkUrl').val('');
        $('#TextContent').val('');
        $('#ContentStatus').val('normal');
        contentguid = '';
        ContentType = type;
    }
    function hideContent() {
        $('#baseFrom').show();
        $('#contentForm').hide();
        $('#codeFrom').hide();
        
        $('#divLinkUrl').hide();
        $('#divPicUrl').hide();
        $('#divThumbPicUrl').hide();
        $('#divMusicUrl').hide();
        $('#divTextContent').hide();
        mini.get("swfUploadPic").setText('');
        mini.get("swfUploadMusic").setText('');
        $('#imgPicUrl').attr('src', '#').hide();
        $('#imgThumbPicUrl').attr('src', '#').hide();
        //document.getElementById("PicUrlUpf").outerHTML = document.getElementById("PicUrlUpf").outerHTML;
        //document.getElementById("MusicUrlUpf").outerHTML = document.getElementById("MusicUrlUpf").outerHTML;

        $('#Title').val('');
        $('#PicUrl').val('');
        $('#ThumbPicUrl').val('');
        $('#MusicUrl').val('');
        $('#LinkUrl').val('');
        $('#TextContent').val('');
        $('#ContentStatus').val('normal');
        contentguid = '';
        ContentType = 'pictext';
    }
    function onUploadMusicSuccess(e) {
        var stringArray = e.serverData.split("|");
        if (stringArray[0] == "1") {
            $("#MusicUrl").val(stringArray[1]);

            $("#mp3MusicUrl").html('<span class="mp3">' + stringArray[1] + '</span>');
            $(".mp3").jmp3();

            art.dialog.tips(stringArray[2], 1);
            //this.setText("");
        }
        else {
            art.dialog.tips(stringArray[2], 3);
        }
    }
    function onUploadPicSuccess(e) {
        var stringArray = e.serverData.split("|");
        if (stringArray[0] == "1") {
            $("#PicUrl").val(stringArray[1]);
            $('#imgPicUrl').attr('src', '..' + stringArray[1]).show();
            if ($("#ThumbPicUrl").val() == '') {
                if (stringArray.length > 3) {
                    $("#ThumbPicUrl").val(stringArray[3]);
                    $('#imgThumbPicUrl').attr('src', '..' + stringArray[3]).show();
                } else {
                    $("#ThumbPicUrl").val(stringArray[1]);
                    $('#imgThumbPicUrl').attr('src', '..' + stringArray[1]).show();
                }
            }
            art.dialog.tips(stringArray[2], 1);
            //this.setText("");
        }
        else {
            art.dialog.tips(stringArray[2], 3);
        }
    }
    function onUploadThumbPicSuccess(e) {
        var stringArray = e.serverData.split("|");
        if (stringArray[0] == "1") {
            $("#ThumbPicUrl").val(stringArray[3]);
            $('#imgThumbPicUrl').attr('src', '..' + stringArray[3]).show();
            art.dialog.tips(stringArray[2], 1);
            //this.setText("");
        }
        else {
            art.dialog.tips(stringArray[2], 3);
        }
    }
    function ajaxFileUpload(argnane,filetype, obfile_id, input_id) {
        $.ajax({
            type: "POST",
            url: "/upload.ashx",
            data: { "upfile": $("#" + obfile_id).val(), "filetype": filetype },
            success: function (data, status) {
                //alert(data);
            },
            error: function (data, status, e) {
                alert("上传失败:" + e.toString());
            }
        });
    }
    function editContent(guid, title, type, pic, thumbpic, music, link, text, status) {
        $('#baseFrom').hide();
        $('#codeFrom').hide();
        $('#contentForm').show();

        $('#divLinkUrl').hide();
        $('#divPicUrl').hide();
        $('#divThumbPicUrl').hide();
        $('#divMusicUrl').hide();
        $('#divTextContent').hide();
        if (type == 'pictext') {
            $('#divPicUrl').show();
            $('#divThumbPicUrl').show();
            $('#divLinkUrl').show();
            $('#divTextContent').show();
            if (pic) {
                $('#imgPicUrl').attr('src', '..' + pic).show();
            } else {
                $('#imgPicUrl').attr('src', '#').hide();
            }
            if (thumbpic) {
                $('#imgThumbPicUrl').attr('src', '..' + thumbpic).show();
            } else if (pic) {
                $('#imgThumbPicUrl').attr('src', '..' + pic).show();
            } else {
                $('#imgThumbPicUrl').attr('src', '#').hide();
            }
        } else if (type == 'music') {
            $('#divMusicUrl').show();
            $('#divTextContent').show();
            if (music) {
                $("#mp3MusicUrl").html('<span class="mp3">' + music + '</span>');
                $(".mp3").jmp3();
            }
        } else {
            $('#divTextContent').show();
        }
        mini.get("swfUploadPic").setText('');
        mini.get("swfUploadMusic").setText('');

        $('#Title').val(title);
        $('#PicUrl').val(pic);
        $('#ThumbPicUrl').val(thumbpic);
        $('#MusicUrl').val(music);
        $('#LinkUrl').val(link);
        $('#TextContent').val(getTextareaValue(text));
        $('#ContentStatus').val(status);
        ContentType = type;
        contentguid = guid;
    }
    function submitContent() {
        if (!$('#Title').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,内容标题未填写，请填写！'
            });
            $('#Title').focus();
            return;
        }
        if (ContentType == 'text' && !$('#TextContent').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,内容未填写，请填写！'
            });
            $('#TextContent').focus();
            return;
        }
        if (ContentType == 'pictext' && !$('#PicUrl').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,图片未上传，请上传！'
            });
            $('#PicUrl').focus();
            return;
        }
        if (ContentType == 'music' && !$('#MusicUrl').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,媒体文件未上传，请上传！'
            });
            $('#MusicUrl').focus();
            return;
        }
        $.getJSON("rulesform.aspx", { "action": "setcontent", "RuleGuid": "<%=_Guid %>"
        , "guid": contentguid
        , "ContentType": ContentType
        , "Title": $('#Title').val()
        , "PicUrl": $('#PicUrl').val()
        , "ThumbPicUrl": $('#ThumbPicUrl').val()
        , "MusicUrl": $('#MusicUrl').val()
        , "LinkUrl": $('#LinkUrl').val()
        , "TextContent": $('#TextContent').val()
        , "ContentStatus": $('#ContentStatus').val()
        }, function (json) {
            if (json.success) {
                hideContent();
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                    }, close: function () {
                        contentgrid.reload();
                    }
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
    function delContent(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>数据删除后无法恢复，需谨慎操作；<br/>您确定要删除当前内容吗？',
            ok: function () {
                $.getJSON("rulesform.aspx", { "action": "delcontent", "Guid": guid }, function (json) {
                    if (json.success) {
                        hideCode();
                        contentgrid.reload();
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
                            close: function () {
                                contentgrid.reload();
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
    }
    function stickContent(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '您确定要置顶当前内容吗？',
            ok: function () {
                $.getJSON("rulesform.aspx", { "action": "stickcontent", "Guid": guid }, function (json) {
                    if (json.success) {
                        contentgrid.reload();
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
                            close: function () {
                                contentgrid.reload();
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
            okVal: '是,置顶',
            cancelVal: '取消',
            cancel: true //为true等价于function(){}
        });
    }
</script>
</body>
</html>

