<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SOC接口.aspx.cs" Inherits="Life.Web.ArticleManage.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        短信号码：<asp:TextBox 
            ID="TextBox1" runat="server" Width="178px"></asp:TextBox>
        <br />
        短信内容：<asp:TextBox ID="TextBox2" runat="server" Height="77px" 
            TextMode="MultiLine" Width="294px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="发送" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
