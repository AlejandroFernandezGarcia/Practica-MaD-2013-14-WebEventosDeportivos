<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ViewGroups.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.ViewGroups"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="viewGroupsForm" runat="server">
    <asp:Label ID="lblLoginRequired" runat="server" CssClass="feedbackInfo" Visible="False"
        meta:resourcekey="lblLoginRequired" />
    <asp:Label ID="lblOperationFailed" runat="server" CssClass="feedbackFailure" Visible="False"
        meta:resourcekey="lblOperationFailed" />
    <asp:Label ID="lblUserAdded" runat="server" CssClass="feedbackSuccess" Visible="False"
        meta:resourcekey="lblUserAdded" />
    <table class="dataView" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th><asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" /></th>
                <th><asp:Localize ID="lclUserCount" runat="server" meta:resourcekey="lclUserCount" /></th>
                <th><asp:Localize ID="lclRecomCount" runat="server" meta:resourcekey="lclRecomCount" /></th>
                <th><asp:Localize ID="lclJoin" runat="server" meta:resourcekey="lclJoin" /></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="GroupList" runat="server" OnItemDataBound="GroupList_OnItemDataBound">
                <ItemTemplate>
                    <asp:TableRow runat="server" ID="tr">
                        <asp:TableCell runat="server">
                            <asp:Label runat="server" ID="lblName"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell1" runat="server">
                            <asp:Label runat="server" ID="lblUserCount"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:Label runat="server" ID="lblRecomCount"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:CheckBox runat="server" ID="cbJoin" />
                            <asp:HiddenField runat="server" ID="hfId" />
                        </asp:TableCell>
                    </asp:TableRow>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div class="button">
        <asp:Button ID="btnJoinGroups" runat="server" meta:resourcekey="btnJoinGroups" />
    </div>
    </form>
    <div class="nextPrevLinks">
        <asp:HyperLink runat="server" ID="linkPrevius" meta:resourcekey="lkPrevius"></asp:HyperLink>
        <asp:HyperLink runat="server" ID="linkNext" meta:resourcekey="lkNext"></asp:HyperLink>
    </div>
</asp:Content>
