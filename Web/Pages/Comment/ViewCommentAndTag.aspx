<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewCommentAndTag.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.ViewCommentAndTag" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">        
        <form id="editCommentWithTags" runat="server">
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
                <asp:TextBox ID="EditComment" runat="server" CssClass="entryBig" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="fieldComment"></div>
            <div class="entryTags">
                <asp:TextBox ID="EditTags" runat="server"></asp:TextBox>
            </div>
            <div class="fieldComment"></div>
            <div class="buttons">
                <asp:Button ID="btnEdit" runat="server" OnClick="BtnEdit" meta:resourcekey="btnEdit" />
                <asp:Button ID="btnDelete" runat="server" OnClick="BtnDelete" meta:resourcekey="btnDelete"/>
                <asp:Button ID="btnReturn" runat="server" OnClick="BtnReturn" meta:resourcekey="btnReturn"/>
            </div>

        </form>
    </div>
</asp:Content>
