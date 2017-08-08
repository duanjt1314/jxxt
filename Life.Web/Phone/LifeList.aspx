<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LifeList.aspx.cs" Inherits="Life.Web.Phone.LifeList"
    StylesheetTheme="Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <style type="text/css">
        td
        {
            text-align: left;
            font-size: 13px;
        }
        
        ul
        {
            margin: 0;
            padding: 0;
        }
        ul li
        {
            margin: 0;
            padding: 0;
            list-style-type: none;
            border-bottom: 1px solid black;
            padding-right:20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ul>
        <li>
            <h2>生活费信息展示</h2>
        </li>
        <asp:Repeater ID="rp_data" runat="server">
            <AlternatingItemTemplate>
                <li class="tr_1">
                    <table width="100%">
                        <tr>
                            <td>
                                <%#Eval("Reason")%>[<%#Eval("CostTypeName")%>]
                            </td>
                            <td style="text-align: right">
                                <%#Convert.ToDateTime( Eval("time")).ToString("yyyy-MM-dd") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                ￥<%#Eval("price")%></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%#Eval("notes")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <a href="javascript:confirm('确认删除')">删 除</a>&nbsp;&nbsp;<a href='LifeOpe.aspx?id=<%#Eval("Id")%>'>
                                    修 改</a>
                            </td>
                        </tr>
                    </table>
                </li>
            </AlternatingItemTemplate>
            <ItemTemplate>
                <li class="tr_2">
                    <table width="100%">
                        <tr>
                            <td>
                                <%#Eval("Reason")%>[<%#Eval("CostTypeName")%>]
                            </td>
                            <td style="text-align: right">
                                <%#Convert.ToDateTime( Eval("time")).ToString("yyyy-MM-dd") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                ￥<%#Eval("price")%></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%#Eval("notes")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <a href="javascript:confirm('确认删除')">删 除</a>&nbsp;&nbsp;<a href='LifeOpe.aspx?id=<%#Eval("Id")%>'>
                                    修 改</a>
                            </td>
                        </tr>
                    </table>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div style="text-align: center" runat="server" id="divPage">
        <asp:Button ID="btnPrex" runat="server" Text="上一页" onclick="btnPrex_Click" />
        <asp:DropDownList ID="ddp_page" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddp_page_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnNext" runat="server" Text="下一页" onclick="btnNext_Click" />
    </div>
    </form>
</body>
</html>
