<%@ Page Title="" Language="C#" MasterPageFile="~/ArticleManage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="ArticleDetail.aspx.cs" Inherits="Life.Web.ArticleManage.WebForm1" StylesheetTheme="Blog"%>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <asp:Literal ID="lTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="border: 1px solid #E5E7E6; float: right; width: 700px; background-color: White">
        <div style="background-color: #F4F5F5; color: #767775; font-weight: bold; line-height: 22px;">
            正文</div>
        <div>
            <asp:Label ID="labTitle" runat="server" Style="color: #526673; font-size: 25px"></asp:Label>
            <asp:HyperLink ID="hlEdit" runat="server">[编辑]</asp:HyperLink>
            <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirm('确认删除？')">[删除]</asp:LinkButton></div>
        <div>
            创建时间:<asp:Label ID="labCreateTime" runat="server" Text=""></asp:Label>
        </div>
        <div style="margin: 5px">
            <asp:Label ID="labContent" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
