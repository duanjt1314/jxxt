<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Detail" CodeBehind="AddArticle.aspx.cs" Inherits="Life.Web.ArticleManage.AddArticle" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章编辑</title>
    <link href="/Content/Script/kindeditor-4.1.4/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <link href="/Content/Script/kindeditor-4.1.4/plugins/code/prettify.css" rel="stylesheet"
        type="text/css" />
    <script src="/Content/Script/kindeditor-4.1.4/kindeditor.js" type="text/javascript"></script>
    <script src="/Content/Script/kindeditor-4.1.4/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/Content/Script/kindeditor-4.1.4/plugins/code/prettify.js" type="text/javascript"></script>
    <script type="text/javascript">
        KindEditor.ready(function (K) {
            var editor1 = K.create('#txtContent', {
                //cssPath: '../plugins/code/prettify.css',
                uploadJson: '/AJ/UpdImage/upload_json.ashx',
                fileManagerJson: '/AJ/UpdImage/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });  
    </script>
    <style type="text/css">
        body
        {
            background-image: url(../images/addArtBg.jpg);
            background-repeat: no-repeat;
            background-color: #BCD3E5;
        }
        #footer
        {
            text-align:center;
            clear:both;
            line-height:60px;
            height:100px;
        }
        .detail td
        {
            padding:2px;
        }
    </style>
</head>
<body>
    <form id="example" runat="server">    
        <table class="detail" width="800" cellpadding="0" cellspacing="0" style="margin:20px auto;">
            <tr>
                <th colspan="2">
                    增加文章
                </th>
            </tr>
            <tr class="tr_1">
                <td style="width: 100px;">
                    文章标题
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTitle" CssClass="txt" runat="server" Width="417px"></asp:TextBox>
                </td>
            </tr>
            <tr class="tr_2">
                <td>
                    文章内容
                </td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Style="width: 100%;
                        height: 500px;"></asp:TextBox>
                </td>
            </tr>
            <tr class="tr_1">
                <td>
                    文章类别
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlCategory" runat="server" Height="20px" Width="145px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_2">
                <td colspan="2">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="确定新增" OnClick="btnAdd_Click" />&nbsp;
                    <input class="btn" id="btnCancle" type="button" value="返回文章" onclick="javascript:location.href='ArticleShow.aspx'" />
                </td>
            </tr>
        </table>
    
    <div id="footer">版权所有&copy;段江涛原创系统</div>
    </form>
</body>
</html>

