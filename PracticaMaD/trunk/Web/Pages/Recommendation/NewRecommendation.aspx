<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="NewRecommendation.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendation.NewRecommendation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="NewRecommendationForm" method="POST" runat="server">
        <asp:Label ID="lblOperationSucceed" runat="server" CssClass="feedbackSuccess" Visible="False"
            meta:resourcekey="lblOperationSucceed" />
        <div class="fieldComment" >
            <span class="labelLeft">
            <asp:Localize ID="lclEventName" runat="server" meta:resourcekey="lclEventName" /></span>
            <span class="labelRight">
            <asp:Localize ID="lclEventNameExt" runat="server"/></span>
        </div>        
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclRecommendationDescription" runat="server" meta:resourcekey="lclRecommendationDescription" />
            </span>
            <span class="entry">
                <asp:TextBox ID="txtRecommendation" runat="server" CssClass="entryBig" TextMode="MultiLine" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRecommendationDescription" runat="server" ControlToValidate="txtRecommendation"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" />
            </span>
        </div>
        <div class="field">
        <table class="dataView" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <th><asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" /></th>
                    <th><asp:Localize ID="lclRecommend" runat="server" meta:resourcekey="lclRecommend" /></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="GroupList" runat="server" OnItemDataBound="GroupList_OnItemDataBound">
                    <ItemTemplate>
                        <asp:TableRow runat="server" ID="tr">
                            <asp:TableCell runat="server">
                                <asp:Label runat="server" ID="lblName"></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell runat="server">
                                <asp:CheckBox runat="server" ID="cbRecommend" />
                                <asp:HiddenField runat="server" ID="hfId" />
                            </asp:TableCell>

                        </asp:TableRow>     
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        </div>
        <div class="button">
            <asp:Button ID="btnRecommend" runat="server" OnClick="BtnRecommendClick" meta:resourcekey="btnRecommend" />
        </div>         
        </form>     
    </div>
</asp:Content>
