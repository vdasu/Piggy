<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminHomePage.aspx.cs" Inherits="Piggy.AdminHomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
        <asp:Label ID="header" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="notificationPanel" runat="server" Visible="false">
        <asp:SqlDataSource ID="ReviewsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:piggyDB %>" 
            SelectCommand="SELECT UserId,RestaurantId,Name,Comment,Rating FROM Reviews,Restaurants WHERE Reviews.RestaurantId = Restaurants.Id AND Comment IS NOT NULL AND isApproved IS NULL"></asp:SqlDataSource>
        <asp:GridView ID="notificationGrid" DataSourceID="ReviewsDataSource" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowCommand="notificationGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name"> 
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Comment" HeaderText="Comment">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Rating" HeaderText="Rating">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Approval Pending">
                    <ItemTemplate>
                        <asp:Button ID="approve" CommandName="ApproveComment" CausesValidation="false" Text="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server"/>
                        <asp:Button ID="reject" CommandName="RejectComment" CausesValidation="false" Text="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" />
                        <asp:HiddenField ID="userId" Value='<%# Eval("UserId") %>' runat="server"/>
                        <asp:HiddenField ID="restaurantId" Value='<%# Eval("RestaurantId") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="noNotifications" runat="server">
        <asp:Button ID="createNewRestaurant" runat="server" Text="Create New Restaurant" OnClick="createNewRestaurant_Click"></asp:Button>
        
    </asp:Panel>
    <asp:Panel ID="createRestaurant" runat="server" Visible="false">
        <asp:Label ID="nameLabel" runat="server" Text="Enter restaurant name: "></asp:Label>
        <asp:TextBox ID="name" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nameValidator" ControlToValidate="name" ErrorMessage="Enter restaurant name"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="locationLabel" runat="server" Text="Enter location: "></asp:Label>
        <asp:TextBox ID="location" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="locationValidator" ControlToValidate="location" ErrorMessage="Enter location"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="cuisineLabel" runat="server" Text="Enter cuisine: "></asp:Label>
        <asp:TextBox ID="cuisine" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="cuisineLabelValidator" ControlToValidate="location" ErrorMessage="Enter cuisine"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="descriptionLabel" runat="server" Text="Enter restaurant description: "></asp:Label>
        <asp:TextBox ID="description" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="descriptionValidator" ControlToValidate="description" ErrorMessage="Enter restaurant description"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Button ID="CreateButton" runat="server" Text="Create" OnClick="CreateButton_Click" />

        <asp:ValidationSummary ID="validationSummary" ForeColor="Red" runat="server" />
    </asp:Panel>
</asp:Content>
