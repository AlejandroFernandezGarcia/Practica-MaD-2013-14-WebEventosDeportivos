<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="CreateGroup.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.CreateGroup"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="CreateGroupForm" method="POST" runat="server">
        <asp:Label ID="lblGroupCreated" runat="server" CssClass="feedbackSuccess" Visible="False"
            meta:resourcekey="lblGroupCreated" />
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclGroupName" runat="server" meta:resourcekey="lclGroupName" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtGroupName" runat="server" Width="100px" Columns="16" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" />
                    <asp:Label ID="lblGroupNameError" runat="server" ForeColor="Red" Style="position: relative"
                        Visible="False" meta:resourcekey="lblGroupNameError"></asp:Label>
                </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclGroupDescription" runat="server" meta:resourcekey="lclGroupDescription" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtGroupDescription" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGroupDescription" runat="server" ControlToValidate="txtGroupDescription"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" />
                    <asp:Label ID="lblGroupDescriptionError" runat="server" ForeColor="Red" Style="position: relative"
                        Visible="False" meta:resourcekey="lblGroupDescriptionError"></asp:Label>
                </span>
        </div>
        <div class="button">
            <asp:Button ID="btnCreateGroup" runat="server" OnClick="BtnCreateGroupClick" meta:resourcekey="btnCreateGroup" />
        </div>
        </form>
    </div>
</asp:Content>
