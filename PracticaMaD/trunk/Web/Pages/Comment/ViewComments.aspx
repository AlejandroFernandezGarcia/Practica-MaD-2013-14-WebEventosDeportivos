<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.ViewComments" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    
    <asp:Label ID="lblEmptyList" runat="server" CssClass="feedbackInfo" Visible="False"
        meta:resourcekey="lblEmptyList" />
        
        
    <div class="fieldComment" >
        <span class="labelLeft">
        <asp:Localize ID="lclEventName" runat="server" meta:resourcekey="lclEventName" /></span>
        <span class="labelRight">
        <asp:Localize ID="lclEventNameExt" runat="server"/></span>
    </div>
    <table class="dataView" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th><asp:Localize ID="lclUserName" runat="server" meta:resourcekey="lclUserName" /></th>
                <th><asp:Localize ID="lclDate" runat="server" meta:resourcekey="lclDate" /></th>
                <th><asp:Localize ID="lclComment" runat="server" meta:resourcekey="lclComment" /></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="CommentList" runat="server" OnItemDataBound="CommentList_OnItemDataBound">
                <ItemTemplate>
                    <asp:TableRow runat="server" ID="tr">
                        <asp:TableCell ID="TableCell1" runat="server">
                            <asp:Label runat="server" ID="lblUserName" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server">
                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server">
                            <asp:HyperLink runat="server" ID="linkToComment"></asp:HyperLink>
                        </asp:TableCell>
                    </asp:TableRow>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <form runat="server">
        <div class="nextPrevLinks">
            <asp:LinkButton runat="server" ID="linkPrevius" meta:resourcekey="lkPrevius"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="linkNext" meta:resourcekey="lkNext"></asp:LinkButton>
        </div>
    </form>
</asp:Content>