﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ResultEventsSearch.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.ResultEventsSearch" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    
    <asp:Label ID="lblEmptyList" runat="server" CssClass="feedbackInfo" Visible="False"
        meta:resourcekey="lblEmptyList" />
    <asp:Label ID="lblAddComment" runat="server" CssClass="feedbackSuccess" Visible="False"
            meta:resourcekey="lblAddComment" />
    <table class="dataView" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th><asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" /></th>
                <th><asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></th>
                <th><asp:Localize ID="lclDate" runat="server" meta:resourcekey="lclDate" /></th>
                <th><asp:Localize ID="lclAddComment" runat="server" meta:resourcekey="lclAddComment" /></th>
                <th><asp:Localize ID="lclViewComments" runat="server" meta:resourcekey="lclViewComments" /></th>
                <th><asp:Localize ID="lclRecommend" runat="server" meta:resourcekey="lclRecommend" /></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="EventList" runat="server" OnItemDataBound="EventList_OnItemDataBound">
                <ItemTemplate>
                    <asp:TableRow runat="server" ID="tr">
                        <asp:TableCell ID="TableCell1" runat="server">
                            <asp:Label runat="server" ID="lblName" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server">
                            <asp:Label runat="server" ID="lblCategory"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server">
                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell4" runat="server">
                            <asp:HyperLink runat="server" ID="linkAddComment" meta:resourcekey="lclAddComment"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell5" runat="server">
                            <asp:HyperLink runat="server" ID="linkViewComments" meta:resourcekey="lclViewComments"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell6" runat="server">
                            <asp:HyperLink runat="server" ID="linkRecommend" meta:resourcekey="lclRecommend"></asp:HyperLink>
                        </asp:TableCell>
                    </asp:TableRow>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div class="nextPrevLinks">
        <asp:HyperLink runat="server" ID="linkPrevius" meta:resourcekey="lkPrevius"></asp:HyperLink>
        <asp:HyperLink runat="server" ID="linkNext" meta:resourcekey="lkNext"></asp:HyperLink>
    </div>
</asp:Content>
