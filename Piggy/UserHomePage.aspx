<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserHomePage.aspx.cs" Inherits="Piggy.UserHomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="header" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="userHomePagePanel" runat="server">
        <asp:Label ID="searchCategoryLabel" runat="server" Text="Search by: "></asp:Label>
        <asp:RadioButtonList ID="searchCategoryList" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" 
            OnSelectedIndexChanged="searchCategoryList_SelectedIndexChanged" runat="server">
            <asp:ListItem>Name</asp:ListItem>
            <asp:ListItem>Location</asp:ListItem>
            <asp:ListItem>Cuisine</asp:ListItem>
            <asp:ListItem>Average Rating</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <asp:Label ID="searchKeyLabel" runat="server"></asp:Label>
        <asp:TextBox ID="searchKey" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="searchKeyValidator" ControlToValidate="searchKey" ErrorMessage="Search key required" runat="server"></asp:RequiredFieldValidator>
        <br /> <br />

        <asp:Button ID="search" runat="server" Text="Search" OnClick="search_Click" />
        <br />

        <asp:Label ID="searchResultsLabel" runat="server" Text="Search results: "></asp:Label>
        <br />
        <asp:GridView ID="searchGridView" runat="server" HorizontalAlign="Center"></asp:GridView>
    </asp:Panel>
</asp:Content>
