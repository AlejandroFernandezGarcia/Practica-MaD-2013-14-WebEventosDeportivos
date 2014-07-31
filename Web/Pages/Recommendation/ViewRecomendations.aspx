<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewRecomendations.aspx.cs" 
Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendation.ViewRecomendations" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Label ID="lblEmptyList" runat="server" CssClass="feedbackInfo" Visible="False"
        meta:resourcekey="lblEmptyList" />
    <form id="viewRecommendationsForm" runat="server">
    <table class="dataView" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th><asp:Localize ID="lclEvent" runat="server" meta:resourcekey="lclEvent" /></th>
                <th><asp:Localize ID="lclGroup" runat="server" meta:resourcekey="lclGroup" /></th>
                <th><asp:Localize ID="lclRecomText" runat="server" meta:resourcekey="lclRecomText" /></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="RecommendationList" runat="server" OnItemDataBound="RecommendationList_OnItemDataBound">
                <ItemTemplate>
                    <asp:TableRow runat="server" ID="tr">
                        <asp:TableCell runat="server">
                            <asp:HyperLink runat="server" ID="linkViewComments"></asp:HyperLink>
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:Label runat="server" ID="lblGroup"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:Label runat="server" ID="lblRecomText"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>                    
                </ItemTemplate>            
            </asp:Repeater>        
        </tbody>
    </table>   
    </form> 
    <div class="nextPrevLinks">
        <asp:HyperLink runat="server" ID="linkPrevius" meta:resourcekey="lkPrevius"></asp:HyperLink>
        <asp:HyperLink runat="server" ID="linkNext" meta:resourcekey="lkNext"></asp:HyperLink>
    </div>
</asp:Content>
