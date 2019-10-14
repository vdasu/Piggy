<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserHomePage.aspx.cs" Inherits="Piggy.UserHomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="userHomePagePanel" runat="server">
        <asp:Label ID="searchCategoryLabel" runat="server" Text="Search by: "></asp:Label>
        <asp:RadioButtonList ID="searchCategoryList" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
            <asp:ListItem Selected="True">Name</asp:ListItem>
            <asp:ListItem>Location</asp:ListItem>
            <asp:ListItem>Cuisine</asp:ListItem>
            <asp:ListItem>Average Rating</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <asp:Button ID="search" runat="server" Text="Search" OnClick="search_Click" />
    </asp:Panel>
</asp:Content>
