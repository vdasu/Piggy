<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminHomePage.aspx.cs" Inherits="Piggy.AdminHomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
        <asp:Label ID="header" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="notificationPanel" runat="server">
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
        <asp:Label ID="ApprovalStatus" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="adminLanding" runat="server">
        <asp:Label ID="adminLabel" runat="server" Text="No Notifications"></asp:Label>
    </asp:Panel>
</asp:Content>
