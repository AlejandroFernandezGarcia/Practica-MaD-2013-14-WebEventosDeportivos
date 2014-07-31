<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.AddComment" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">        
        <form id="addCommentWithTags" method="POST" runat="server">
            <asp:Label ID="lblEmptyComment" runat="server" CssClass="feedbackFailure" Visible="False"
                       meta:resourcekey="lblEmptyComment" />
            <asp:Label ID="lblCommentMaxLength" runat="server" CssClass="feedbackFailure" Visible="False"
            meta:resourcekey="lblCommentMaxLength" />
            <asp:Label ID="lblTagMaxLenght" runat="server" CssClass="feedbackFailure" Visible="False"
            meta:resourcekey="lblTagMaxLenght" />
            <asp:Label ID="lblCommentSuccess" runat="server" CssClass="feedbackSuccess" Visible="False"
            meta:resourcekey="lblCommentSuccess" />

            <div class="fieldComment" >
                <span class="labelLeft">
                <asp:Localize ID="lclEventName" runat="server" meta:resourcekey="lclEventName" /></span>
                <span class="labelRight">
                <asp:Localize ID="lclEventNameExt" runat="server"/></span>
            </div>
            <div class="fieldComment"></div>
            <div class="entryBig">
                <asp:TextBox ID="NewComment" runat="server" CssClass="entryBig" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="fieldComment"></div>
            <div class="entryTags">
                <asp:TextBox ID="NewTags" runat="server"></asp:TextBox>
            </div>
            <div class="fieldComment"></div>
            <div class="buttons">
                <asp:Button ID="btnSendComment" runat="server" OnClick="BtnSendComment" meta:resourcekey="btnSendComment" />
                <asp:Button ID="btnReturn" runat="server" OnClick="BtnReturn" meta:resourcekey="btnReturn" />
            </div>

        </form>
    </div>
</asp:Content>
