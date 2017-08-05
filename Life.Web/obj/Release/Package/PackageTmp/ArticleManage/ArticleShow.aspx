<%@ Page Language="C#" MasterPageFile="~/ArticleManage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ArticleShow.aspx.cs" Inherits="Life.Web.ArticleManage.ArticleShow"
    StylesheetTheme="Blog" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="Server">
    文章列表
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:TextBox ID="txtKey" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn" OnClick="btnSearch_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="border: 1px solid #E5E7E6; float: right; width: 700px; background-color: White;">
        <div style="background-color: #F4F5F5; color: #767775; font-weight: bold; line-height: 22px;">
            博文目录</div>
        <ul class="sp" style="padding: 5px;">
            <asp:Repeater ID="rp_art_data" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="spliLeft">
                            <a target="_blank" href='ArticleDetail.aspx?artId=<%#Eval("id") %>'>
                                <%#Eval("title")%></a>
                        </div>
                        <div class="spliRight">
                            <%#Eval("CreateTime","{0:yyyy-MM-dd HH:mm:ss}")%>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <li>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" HorizontalAlign="Right"
                    ShowCustomInfoSection="Left" ShowNavigationToolTip="True" CustomInfoHTML="<div class='page_info'>当前为<strong>%CurrentPageIndex%/%PageCount%</strong>页 | 每页<strong>%PageSize%</strong>条 | 共<strong>%RecordCount%</strong>条记录</div>"
                    PageIndexBoxType="TextBox" ShowPageIndexBox="Always" SubmitButtonText="跳转" TextAfterPageIndexBox="页"
                    TextBeforePageIndexBox="转到" Wrap="False" FirstPageText="首页" LastPageText="末页"
                    NextPageText="下一页" PrevPageText="上一页" 
                    OnPageChanged="AspNetPager1_PageChanged" NumericButtonCount="5">
                </webdiyer:AspNetPager>
            </li>
        </ul>
    </div>
</asp:Content>
