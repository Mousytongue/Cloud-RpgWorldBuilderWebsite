<%@ Page Title="View API" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="rpgworldbuilder.ViewAPI" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="Panel3" runat="server">
            <asp:Label ID="lbl_ViewAPI" runat="server" Text="Maps API" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
            Endpoint: rpgworldbuilder.com/api/maps
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" CssClass="Container">
            <asp:Label ID="Label1" runat="server" Font-Size="Medium" Text="GET: api/Maps"></asp:Label>
            <br />
            Returns all maps contained in the service.</asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server">
            <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="GET: api/Maps/{mapID}"></asp:Label>
            <br />
            Returns a specific map.</asp:Panel>
        <br />
        <asp:Panel ID="Panel4" runat="server">
            <asp:Label ID="Label3" runat="server" Font-Size="Medium" Text="PUT: api/Maps/{mapID}"></asp:Label>
            <br />
            Updates a specific map via MapID</asp:Panel>
        <br />
        <asp:Panel ID="Panel5" runat="server">
            <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="POST: api/Maps/"></asp:Label>
            <br />
            Creates a new map using submitted map data</asp:Panel>
        <br />
    </div>
</asp:Content>