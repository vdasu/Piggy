<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Piggy.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="detailPanel" runat="server">

        <asp:Label ID="restaurantNameLabel" runat="server"></asp:Label>
        //Restaurant description goes here

        <asp:Panel ID="makeReviewPanel" runat="server">
             <asp:TextBox ID="commentEntry" runat="server"></asp:TextBox>
             <asp:DropDownList ID="ratingDDL" runat="server">
                 <asp:ListItem>0</asp:ListItem>
                 <asp:ListItem>1</asp:ListItem>
                 <asp:ListItem>2</asp:ListItem>
                 <asp:ListItem>3</asp:ListItem>
                 <asp:ListItem>4</asp:ListItem>
                 <asp:ListItem>5</asp:ListItem>
             </asp:DropDownList>
        </asp:Panel>

        <asp:Button ID="submitComment" runat="server" onClick="submitComment_Click"/>
        <asp:Label ID="test" runat="server"></asp:Label>
    </asp:Panel>

</asp:Content>
