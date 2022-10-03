<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="GEIMS.Client.UI.Material" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Material
            <%--<asp:LinkButton ID="lnkAddNewMaterialGroup" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewSection_Click">Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <script type="text/javascript">
                $(function () {
                    $('#id_search').quicksearch('table#<%=gvMaterial.ClientID%> tbody tr');
			    })
            </script>
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%; padding-bottom: 10px;padding-right:10px">
                    <fieldset>
                        <legend>Material Details</legend>
                        <div style="text-align: left; width: 20%; float: left;" class="label">
                            Material Group :<span style="color: red">*</span>
                        </div>
                        <div style="text-align: left; width: 80%; float: right;">
                            <div style="float: left; text-align: left;">
                                <asp:DropDownList runat="server" ID="ddlMaterialGroup" CssClass="Droptextarea" OnSelectedIndexChanged="ddlMaterialGroup_SelectedIndexChanged" Height="100%" AutoPostBack="True" />
                            </div>
                            <div style="float: right; text-align: left;">
                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnAddNew_Click" Text="Add New" />
                                &nbsp;
                                <asp:Button ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click" Text="View List" />
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);" />
                    </div>
                    <br />
                    <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvMaterial_RowCommand"
                            OnPreRender="gvMaterial_PreRender">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="MaterialCode" HeaderText="Material Code" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="MaterialName" HeaderText="Material Name" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="50%"></asp:BoundField>
                                <asp:BoundField DataField="ModelNo" HeaderText="Model No" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="UOMName" HeaderText="UOM" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                            CommandName="Edit1" CommandArgument='<%# Eval("MaterialID")%>' Height="20px" Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                            CommandName="Delete1" CommandArgument='<%# Eval("MaterialID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                            Height="20px" Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabMaterialGroupDetails" href="#tabs-1">Material Details</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 275px;padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%; float: left;">
                        </div>
                        <div style="width: 100%; float: left;">
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Material Code :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="validate[required] TextBox" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Material Name :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:TextBox ID="txtMaterialName" runat="server" CssClass="validate[required] TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 55px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Description :
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    UOM :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:DropDownList runat="server" ID="ddlUOM" CssClass="validate[required] Droptextarea" Height="100%" />
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Model No :
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:TextBox ID="txtModelNo" runat="server" CssClass="TextBox" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="height: 30px; float: right; width: 100%;">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });

        $("#<%= btnAddNew.ClientID%>").click(function () {
            $("#<%=ddlMaterialGroup.ClientID%>").addClass("validate[required]");
            $("#<%=txtMaterialCode.ClientID%>").removeClass("validate[required]");
            $("#<%=txtMaterialName.ClientID%>").removeClass("validate[required]");
            $("#<%=ddlUOM.ClientID%>").removeClass("validate[required]");
        });

        $("#<%= btnViewList.ClientID%>").click(function () {
            $("#<%=ddlMaterialGroup.ClientID%>").addClass("validate[required]");
            $("#<%=txtMaterialCode.ClientID%>").removeClass("validate[required]");
            $("#<%=txtMaterialName.ClientID%>").removeClass("validate[required]");
            $("#<%=ddlUOM.ClientID%>").removeClass("validate[required]");
        });

        $("#<%= btnSave.ClientID%>").click(function () {
            $("#<%=ddlMaterialGroup.ClientID%>").addClass("validate[required]");
            $("#<%=txtMaterialCode.ClientID%>").addClass("validate[required]");
            $("#<%=txtMaterialName.ClientID%>").addClass("validate[required]");
            $("#<%=ddlUOM.ClientID%>").addClass("validate[required]");
        });
    </script>
</asp:Content>
