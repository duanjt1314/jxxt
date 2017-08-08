<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Life.Web.Phone.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>手机用户登录</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <style type="text/css">
        div
        {
            line-height:25px;
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                帐号:<asp:TextBox ID="txtLoginId" runat="server" Text=""></asp:TextBox><br />
                密码:<asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Text=""></asp:TextBox><br />
                <asp:Button ID="btnLogin" runat="server" Text="登录" onclick="btnLogin_Click" />
            </p>
        </div>
    </form>
</body>
</html>
