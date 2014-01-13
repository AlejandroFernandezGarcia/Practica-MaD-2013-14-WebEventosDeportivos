<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
CodeBehind="SearchEvents.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.SearchEvents"
 meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="SearchForm" runat="server"> <!-- method="GET" hay que ponerlo? -->
        <div class="field">
            <span class="label">
            <asp:Localize ID="lclKeywords" runat="server" meta:resourcekey="lclSearchKeywords" /></span>
            <span class="entry">
                <asp:TextBox ID="txtKeywords" runat="server" Width="100px" Columns="30" ></asp:TextBox>
                <asp:Label ID="lblKeywordsError" runat="server" ForeColor="Red" Style="position: relative"
                    Visible="False" meta:resourcekey="lblKeywordsError"></asp:Label>
            </span>
        </div>
        <div class="field">
            <span class="label">
            <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></span>
            <span class="dropDownList">
                <asp:DropDownList ID="CategoryDropDownList" runat="server" 
                DataSourceID="EntityDataSource1" DataTextField="name" DataValueField="name">
                </asp:DropDownList>
                <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
                    ConnectionString="name=PracticaMaDEntitiesContainer" 
                    DefaultContainerName="PracticaMaDEntities" EnableFlattening="False" 
                    EntitySetName="Category" EntityTypeFilter="Category" Select="it.[name]">
                </asp:EntityDataSource>
            </span>
        </div>
        

        <div class="button">
            <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchClick" meta:resourcekey="btnSearch" />
        </div>
        </form>
    </div>
</asp:Content>
