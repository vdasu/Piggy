<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Piggy.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="registerPanel" runat="server">
        <asp:Label ID="firstNameLabel" runat="server" Text="Enter First Name: "></asp:Label>
        <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="firstNameValidator" ControlToValidate="firstName" ErrorMessage="Enter First Name"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="lastNameLabel" runat="server" Text="Enter Last Name: "></asp:Label>
        <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="lastNameValidator" ControlToValidate="lastName" ErrorMessage="Enter Last Name"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="usernameLabel" runat="server" Text="Enter user name: "></asp:Label>
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="usernameValidator" ControlToValidate="username" ErrorMessage="Enter user name"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="usernameRegexValidator" runat="server" ControlToValidate="Username" 
            ErrorMessage="Invalid User Name" Text="*" ValidationExpression="[a-zA-Z0-9]{1,10}" 
            ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
        <br />

        <asp:Label ID="passwordLabel" runat="server" Text="Enter password: "></asp:Label>
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="passwordValidator" ControlToValidate="password" ErrorMessage="Enter password"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="reenterPasswordLabel" runat="server" Text="Re-enter password: "></asp:Label>
        <asp:TextBox ID="reenterPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reenterPasswordValidator" ControlToValidate="reenterPassword" ErrorMessage="Re-enter password"
            Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="passwordMatchValidator" OnServerValidate="passwordMatchValidator_ServerValidate"
            ErrorMessage="Password's must match" Text="*" ForeColor="Red" runat="server"></asp:CustomValidator>
        <br />

        <asp:Label ID="userTypeLabel" runat="server" Text="User type: "></asp:Label>
        <asp:CheckBox ID="isAdminCheckbox" runat="server" Text="Admin" />
        <br />

        <asp:ValidationSummary ID="validationSummary" ForeColor="Red" runat="server" />
    </asp:Panel>
</asp:Content>
