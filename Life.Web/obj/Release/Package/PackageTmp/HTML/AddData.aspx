<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddData.aspx.cs" Inherits="Life.Web.HTML.AddData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="用户导入" onclick="Button1_Click" />
        &nbsp;&nbsp; 
        <asp:Button ID="Button2" runat="server" Text="导入文章类型" onclick="Button2_Click" />
        &nbsp;&nbsp; 
        <asp:Button ID="Button3" runat="server" Text="导入文章" onclick="Button3_Click" />
        &nbsp;&nbsp; 
        <asp:Button ID="Button4" runat="server" Text="导入字典" onclick="Button4_Click" />
        &nbsp;&nbsp; 
        <asp:Button ID="Button5" runat="server" Text="导入生活费" onclick="Button5_Click" />
        &nbsp;&nbsp; 
        <asp:Button ID="Button6" runat="server" Text="导入银行卡" onclick="Button6_Click" />
        &nbsp;&nbsp; 
        <asp:Button ID="Button7" runat="server" Text="导入纯收入" onclick="Button7_Click" />
        &nbsp;&nbsp; 
        <br />
        <asp:Button ID="Button8" runat="server" Text="清空数据" onclick="Button8_Click" />
    </div>
    </form>
</body>
</html>
