<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Piggy.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="registerPanel" runat="server">
        <asp:Label ID="FirstNameLabel" runat="server" Text="Enter First Name: "></asp:Label>
        <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="FirstNameValidator" ControlToValidate="FirstName" ErrorMessage="Enter First Name"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
    </asp:Panel>
</asp:Content>
