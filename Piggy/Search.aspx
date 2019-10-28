<%@ Page Title="" Language="C#" MasterPageFile="~/Piggy.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Piggy.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="header" runat="server"></asp:Label>
    <br />

    <asp:Panel ID="searchPagePanel" runat="server">
        <asp:Label ID="searchCategoryLabel" runat="server" Text="Search by: "></asp:Label>
        <asp:RadioButtonList ID="searchCategoryList" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" 
            OnSelectedIndexChanged="searchCategoryList_SelectedIndexChanged" runat="server">
            <asp:ListItem Value="Name">Name</asp:ListItem>
            <asp:ListItem Value="Location">Location</asp:ListItem>
            <asp:ListItem Value="Cuisine">Cuisine</asp:ListItem>
            <asp:ListItem Value="AvgRating">Average Rating</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <asp:Label ID="searchKeyLabel" runat="server"></asp:Label>
        <asp:TextBox ID="searchKey" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="searchKeyValidator" ControlToValidate="searchKey" ErrorMessage="Search key required" runat="server"></asp:RequiredFieldValidator>
        <br /> <br />

        <asp:Button ID="search" runat="server" Text="Search" OnClick="search_Click" />
        <br />

        <asp:Panel ID="MostViewedPanel" runat="server">
            <asp:Label ID="mostViewedLabel" Text="Most viewed restaurant by you: " runat="server"></asp:Label>
            <br />
            <asp:HyperLink ID="MostViewedHLink" runat="server" NavigateUrl="~/Details.aspx?restaurantId={0}&restaurantName={1}">HyperLink</asp:HyperLink>

        </asp:Panel>

        <asp:Label ID="searchResultsLabel" runat="server" Text="Search results: "></asp:Label>
        <br />
        <asp:GridView ID="searchGridView" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" AllowPaging="true" PageSize="5">
            <Columns>
                <asp:HyperLinkField HeaderText=" Restaurant Name" SortExpression="Name" DataTextField="Name" DataNavigateUrlFields="Name, Id" DataNavigateUrlFormatString="Details.aspx?restaurantName={0}&restaurantId={1}">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>

                <asp:BoundField DataField="Location" HeaderText="Location">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="Cuisine" HeaderText="Cuisine">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="Views" HeaderText="Views">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="searchStatus" Text="No results found." runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>
