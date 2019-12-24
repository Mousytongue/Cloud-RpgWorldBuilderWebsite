<%@ Page Title="Map Editor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditWorld.aspx.cs" Inherits="rpgworldbuilder.EditWorld" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            SetTabs();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetTabs();
                }
            });
        };
        function SetTabs() {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "information";
            $('#myTab a[href="#' + tabName + '"]').tab('show');
            $("#myTab a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        };
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#creatureModal').modal('show');
        }
    </script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <div>
        <asp:Label ID="lbl_MapName" runat="server" Text="MAP NAME" Font-Bold="true" Font-Size="X-Large"></asp:Label>
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        <asp:Label ID="lbl_Feedback" runat="server" Text="" Font-Bold="true"></asp:Label>
    </div>  
    <div>
        <asp:Label ID="lbl_MapAuthor" runat="server" Text="MAP AUTHOR"></asp:Label>
    </div>
    <div>
        <asp:Label ID="lbl_MapDesc" runat="server" Text="MAP DESCRIPTION" Font-Italic="true"></asp:Label>
    </div>

    <div>
        <p></p>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <div>
                    <p></p>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        &nbsp&nbsp&nbsp
                    </div>
                </div>
                <div class="col-md-7">
                    <asp:ImageButton ID="img_Map" runat="server" Height="450px" Width="600px" OnClick="img_Map_Click" AlternateText="No image. Please upload an image" BorderColor="IndianRed" BorderStyle="Dashed" BorderWidth="3px" OnClientClick="&quot;return false&quot;" />
                </div>
            </div>
            <div class="col-md-5">
                <ul class="nav nav-tabs" id="myTab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="poi-tab" data-toggle="tab" href="#poi" role="tab" aria-controls="poi" aria-selected="true">Points of Interest</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="srd-tab" data-toggle="tab" href="#srd" role="tab" aria-controls="srd" aria-selected="false">DnD SRD API</a>
                    </li>
                </ul>
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane active" id="poi" role="tabpanel" aria-labelledby="poi-tab">
                        <div>
                            <h3>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Points of Interest</h3>
                            <div class="col-sm-6">
                                <asp:Button ID="btn_DeletePoi" runat="server" Text="Delete Selected" OnClick="btn_DeletePoi_Click" />
                            </div>
                            <div class="col-sm-6">
                                <asp:Button ID="btn_SavePoi" runat="server" Text="Save Changes" OnClick="btn_SavePoi_Click" />
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_PointCoords" runat="server" Text="X,Y"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txt_PointDesc" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btn_AddPoi" runat="server" Text="Add POI" OnClick="btn_AddPoi_Click" />

                            </div>

                            <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="tab-pane" id="srd" role="tabpanel" aria-labelledby="srd-tab">
                        <div class="col-md-8 row">
                            <asp:TextBox ID="API_TextBox1" runat="server" ToolTip="Enter something to search!" CssClass="col-md-12" OnTextChanged="textbox_API_Textchanged" AutoPostBack="True"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <asp:ListBox ID="ListBox1" runat="server" OnSelectedIndexChanged="listbox_index_changed" Rows="20" CssClass="col-md-12" AutoPostBack="True"></asp:ListBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="creatureModal" tabindex="-1" role="dialog" aria-labelledby="creatureModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="creatureModalTitle">Creature Information</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lbl_CreatureName" runat="server" Text="Label" Font-Bold="False" Font-Size="X-Large"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_CreatureType" runat="server" Text="Label" Font-Bold="False" Font-Size="Small" Font-Italic="True"></asp:Label>
                    <hr />
                    <asp:Label ID="lbl_ArmorClass" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_HP" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_Speed" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                    <hr />
                    <asp:Label ID="lbl_Senses" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_Languages" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_CR" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Dismiss</button>
                </div>
            </div>
        </div>
    </div>
    <%--<script>
        $(function () {
            $('#myTab li:first-child a').tab('show')
        })
    </script>--%>
    <asp:HiddenField ID="TabName" runat="server" />
    

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
</asp:Content>
