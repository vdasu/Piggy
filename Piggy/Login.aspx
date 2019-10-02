<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Piggy.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="loginPanel" runat="server">
        <asp:TextBox ID="username" runat="server" placeholder="User Name"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="username" ErrorMessage="User Name required" 
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="usernameValidator" runat="server" ControlToValidate="username" 
            ErrorMessage="Invalid User Name" Text="*" ValidationExpression="[a-zA-Z0-9]{1,10}" 
            ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="password" ErrorMessage="Password required" 
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br /> <br />
        <asp:Button ID="login" runat="server" Text="Login"/>
        <asp:Button ID="signup" runat="server" Text="Sign Up" onClick="signup_Click"/>
        <br />
        <asp:ValidationSummary ID="validationSummary" runat="server" ForeColor="Red"/>
    </asp:Panel>
</asp:Content>
