<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Paginator.ascx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Controls.Paginator" %>

<asp:Table runat="server" ID="Container" CssClass="paginator">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server" ID="PrevTd">
            <asp:HyperLink runat="server" ID="PrevLink" OnClick="PrevLink_OnClick">&laquo;</asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="FirstTd">
            <asp:HyperLink runat="server" ID="FirstLink" OnClick="FirstLink_OnClick">1</asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="SpaceFirstTd" CssClass="space"><b>...</b></asp:TableCell>
        <asp:TableCell runat="server" ID="Middle1Td">
            <asp:HyperLink runat="server" ID="Middle1Link" OnClick="Middle1Link_OnClick"></asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="Middle2Td">
            <asp:HyperLink runat="server" ID="Middle2Link" OnClick="Middle2Link_OnClick"></asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="Middle3Td" CssClass="current">
            <asp:HyperLink runat="server" ID="Middle3Link" OnClick="Middle3Link_OnClick"></asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="Middle4Td">
            <asp:HyperLink runat="server" ID="Middle4Link" OnClick="Middle4Link_OnClick"></asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="Middle5Td">
            <asp:HyperLink runat="server" ID="Middle5Link" OnClick="Middle5Link_OnClick"></asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="SpaceLastTd" CssClass="space"><b>...</b></asp:TableCell>
        <asp:TableCell runat="server" ID="LastTd">
            <asp:HyperLink runat="server" ID="LastLink" OnClick="LastLink_OnClick"></asp:HyperLink>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="NextTd">
            <asp:HyperLink runat="server" ID="NextLink" OnClick="NextLink_OnClick">&raquo;</asp:HyperLink>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>