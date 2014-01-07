<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.AddComment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="addCommentWithTags" runat="server">
        <asp:Label ID="lblEmptyComment" runat="server" CssClass="feedbackFailure" Visible="False"
                   meta:resourcekey="lblEmptyComment" />
        <asp:Label ID="lblCommentMaxLength" runat="server" CssClass="feedbackFailure" Visible="False"
        meta:resourcekey="lblCommentMaxLength" />
        <asp:Label ID="lblTagMaxLenght" runat="server" CssClass="feedbackFailure" Visible="False"
        meta:resourcekey="lblTagMaxLenght" />
        <asp:Label ID="lblCommentSuccess" runat="server" CssClass="feedbackSuccess" Visible="False"
        meta:resourcekey="lblCommentSuccess" />
        <div class="field" >
            <span class="label">
            <asp:Localize ID="lclEventName" runat="server" meta:resourcekey="lclEventName" /></span>
            <span class="label">
            <asp:Localize ID="lblEventNameExt" runat="server"/></span>
        </div>
        <div class="entryBig">
            <asp:TextBox ID="NewComment" runat="server" TextMode="MultiLine"
            Height="140px" Width="628px"></asp:TextBox>
        </div>
        <div class="entryTags">
            <asp:TextBox ID="NewTags" runat="server" ></asp:TextBox>
        </div>
        <div class="buttons">
            <asp:Button ID="btnSendComment" runat="server" OnClick="BtnSendComment" meta:resourcekey="btnSendComment" />
            <asp:Button ID="btnCancelComment" runat="server" OnClick="BtnCancelComment" meta:resourcekey="btnCancelComment" />
        </div>

    </form>
</asp:Content>
