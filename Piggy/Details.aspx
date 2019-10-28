<%@ Page Title="" Language="C#" MasterPageFile="~/Piggy.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Piggy.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="detailPanel" runat="server">

        <asp:Panel ID="descriptionPanel" runat="server">
            <div style="float:left">
                <asp:Label ID="restaurantNameLabel" runat="server" ></asp:Label>
                <br />
                <asp:Label ID="restaurantDescription" runat="server" SkinID="description"></asp:Label>
            </div>

            <div style="float:right">
                <asp:Label ID="restaurantRatingLabel" Text="Rating: " runat="server" ></asp:Label>
                <asp:Label ID="restaurantRating" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <br /><br /><br /><br /><br /><br /><br /><br />

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
            <br />
            <asp:Button ID="submitComment" runat="server" onClick="submitComment_Click"/>
        </asp:Panel>

        <br /><br />

        <asp:Panel ID="commentsPanel" runat="server">
            <asp:GridView ID="commentsGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="User Name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:Label ID="commentsLabel" runat="server" Text='<%# Eval("Comment") %>' SkinID="description"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rating" HeaderText="Rating">
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>

</asp:Content>
