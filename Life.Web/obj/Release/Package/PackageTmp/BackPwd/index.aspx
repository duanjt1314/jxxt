<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="index.aspx.cs"
    Inherits="Life.Web.BackPwd.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>找回密码</title>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" style="margin: 30px auto">
        <tr>
            <td align="right">
                用户名:
            </td>
            <td style="width:300px;">
                <asp:Literal ID="LitUserName" runat="server"></asp:Literal>
        </tr>
        <tr>
            <td align="right">
                新密码:
            </td>
            <td>
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPwd" Display="Dynamic" ErrorMessage="不能为空" 
                    Font-Size="13pt" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                重复新密码:
            </td>
            <td>
                <asp:TextBox ID="txtRPwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtRPwd" Display="Dynamic" ErrorMessage="不能为空" 
                    Font-Size="13pt" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPwd" ControlToValidate="txtRPwd" Display="Dynamic" 
                    ErrorMessage="两次密码不一致" Font-Size="13pt" ForeColor="Red"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnBackPwd" runat="server" Text="修改密码" OnClick="btnBackPwd_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
