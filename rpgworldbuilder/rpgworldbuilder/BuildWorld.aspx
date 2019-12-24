<%@ Page Title="Map Builder" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuildWorld.aspx.cs" Inherits="rpgworldbuilder.BuildWorld" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
 
    
    
    <div>
        <asp:Label ID="lbl_MapName" runat="server" Text="MAP NAME:" Font-Bold="true" Font-Size="Large" ></asp:Label>
        <asp:TextBox ID ="txt_MapName" runat="server" style = "resize:none"></asp:TextBox>
        <asp:ImageMap ID="img_Map" runat="server" Height="450px" Width="600px" AlternateText="No image. Please upload an image" BorderColor="IndianRed" BorderStyle="Dashed" BorderWidth="3px" style="float:right"></asp:ImageMap>
    </div>
    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
    <div>
        <asp:Label ID="lbl_MapDesc" runat="server" Text="MAP DESCRIPTION:" Font-Bold="true" Font-Size="Large" style="vertical-align:top"></asp:Label>
        <asp:TextBox ID="txt_MapDesc" runat="server" TextMode="MultiLine" style = "resize:none" Width ="300px" Height ="200px"></asp:TextBox>
    </div>
    <div">                      
        <asp:Label ID="lbl_MapUpload" runat="server" Text="MAP FILE: (.JPG/.PNG ONLY)" Font-Bold="true" Font-Size="Large"></asp:Label>
        <asp:FileUpload ID="MapUpload" runat="server" />
        <asp:Label ID="lbl_Message" runat="server" Text="File uploaded successfully." ForeColor="Green" Visible="false" Font-Size="Large" />
    </div>
    <div>                       
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
	    <asp:Button ID="btn_UploadMapToPage" runat="server" OnClick="btn_SaveMap_Click" Text="Upload Map" Width="100px" Height="100px" BackColor="IndianRed" Font-Bold="true"/>
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        <asp:Button ID="btn_SaveMap" Text="Upload" runat="server" OnClick="btn_Upload_Click" Style="display: none" />
        <asp:Label ID="lbl_MapActionFeedback" runat="server" Text=""></asp:Label>
    </div>
    
    <div>
        <asp:Label ID="lbl_MapFolderPath" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_MapFileName" runat="server" Visible="false"></asp:Label>
    </div>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btn_SaveMap.ClientID %>").click();
            }
        }
</script>
</asp:Content>

