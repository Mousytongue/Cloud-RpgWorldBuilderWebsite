<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="rpgworldbuilder._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-image:url('Images/Dragon.png');">
    <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
    <div>
        <asp:Button 
            ID="BuildWorldButton" 
            runat="server" 
            Text="Build World" 
            OnClick="buildWorldButton_Click"
            Font-Bold="true"
            ForeColor="DodgerBlue"
            Height="45"
            Width="150"
            />
        <asp:Label ID="lbl_BuildWorldLabel" runat="server" ForeColor="IndianRed" Font-Bold="true" Text="To create your own exciting world, click here"></asp:Label>
    </div>
    <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
    <div>
        <asp:Button 
            ID="BrowseWorldsButton" 
            runat="server" 
            Text="Browse Worlds" 
            OnClick="browseWorldsButton_Click"
            Font-Bold="true"
            ForeColor="DodgerBlue"
            Height="45"
            Width="150"
            />
        <asp:Label ID="lbl_BrowseWorldsLabel" runat="server" ForeColor="IndianRed" Font-Bold="true" Text="To view a myriad of user-created worlds, click here"></asp:Label>
    </div>
    <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
    <div>
        <asp:Button 
            ID="ViewAPIButton" 
            runat="server" 
            Text="View API" 
            OnClick="viewAPIButton_Click"
            Font-Bold="true"
            ForeColor="DodgerBlue"
            Height="45"
            Width="150"
            />
        <asp:Label ID="lbl_ViewAPILabel" runat="server" ForeColor="IndianRed" Font-Bold="true" Text="To view API documentation, click here"></asp:Label>
    </div>
    <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>
        <div>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</div>

    </div>
</asp:Content>
