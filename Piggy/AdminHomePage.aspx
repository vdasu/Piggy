<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminHomePage.aspx.cs" Inherits="Piggy.AdminHomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="notificationPanel" runat="server">
        <asp:GridView ID="notificationGrid" runat="server" HorizontalAlign="Center"></asp:GridView>
        <asp:Button 
    </asp:Panel>
</asp:Content>
