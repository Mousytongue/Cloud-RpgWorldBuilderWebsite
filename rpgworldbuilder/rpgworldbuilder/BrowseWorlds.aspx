<%@ Page Title="Browse Worlds" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseWorlds.aspx.cs" Inherits="rpgworldbuilder.BrowseWorlds" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div>
        <asp:Label ID="lbl_BrowseWorlds" runat="server" Text="Browse Worlds" Font-Bold="true" Font-Size="Large"></asp:Label>
    </div>



    <div>
            <asp:ListView ID="lst_listview1" runat="server" DataSourceID="sql_SqlDataSource1"> 
            <LayoutTemplate> 
                <table id="Table1" runat="server" class="TableCSS"> 
                    <tr id="Tr1" runat="server" class="TableHeader"> 
                        <td id="Td1" runat="server">Map Name</td> 
                        <td id="Td2" runat="server">Author</td> 
                    </tr> 
                    <tr id="ItemPlaceholder" runat="server"> 
                    </tr>
                </table>
            </LayoutTemplate> 



                 <ItemTemplate>
                <tr class="TableData"> 

                    <td> 
                        <asp:HyperLink  
                            ID="HyperLink1" 
                            runat="server" 
                            Text='<%# Eval("MapName")%>'
                            NavigateUrl='<%# "~/EditWorld.aspx?MapID=" + Eval("MapID") %>'>
                        </asp:HyperLink>
                    </td> 

                    <td> 
                        <asp:Label  
                            ID="Label2" 
                            runat="server" 
                            Text='<%# Eval("UserName")%>'>
                        </asp:Label>
                    </td>
                </tr>
            </ItemTemplate> 

        </asp:ListView>

        <asp:SqlDataSource ID="sql_SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:DefaultConnection%>"
            SelectCommand="SELECT * FROM Map;">
        </asp:SqlDataSource>
    </div>



    <div>
        <asp:Label ID="lbl_MapNameQuery" runat="server" Text="Map Name:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="txt_MapNameQuery" runat="server"></asp:TextBox>
    </div>

    <div>
        <asp:Label ID="lbl_AuthorNameQuery" runat="server" Text="Author Name:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="txt_AuthorNameQuery" runat="server"></asp:TextBox>
    </div>

    <div>
        <asp:Button ID="btn_QueryButton" runat="server" OnClick="btn_QueryButton_Click" Text="Search" Font-Bold="true"/>
        <asp:Button ID="btn_RefreshButton" runat="server" OnClick="btn_RefreshButton_Click" Text="Refresh Results" Font-Bold="true"/>
    </div>

    <div>
        <asp:Label ID="lbl_errorLabel" runat="server"></asp:Label>
    </div>
</asp:Content>