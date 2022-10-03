<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="Vendor.aspx.cs" Inherits="GEIMS.Client.UI.Vendor" %>

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
            Vendor
            <asp:LinkButton ID="lnkAddNewVendor" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewVendor_Click">Add New</asp:LinkButton>
            &nbsp;
			<asp:LinkButton ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <script type="text/javascript">
                $(function () {
                    $('#id_search').quicksearch('table#<%=gvVendor.ClientID%> tbody tr');
                })
            </script>
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                </div>
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);" />
                    </div>
                    <br />
                    <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                        <asp:GridView ID="gvVendor" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvVendor_RowCommand"
                            OnPreRender="gvVendor_PreRender">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="80%" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="80%"></asp:BoundField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                            CommandName="Edit1" CommandArgument='<%# Eval("VendorID")%>' Height="20px" Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                            CommandName="Delete1" CommandArgument='<%# Eval("VendorID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                        <li><a id="tabVendorDetails" href="#tabs-1">Vendor Details</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 10px 10px 10px 10px" class="gradientBoxesWithOuterShadows">
                        <div style="float: left; width: 100%;">
                            <div style="width: 50%; float: left;">
                                <div style="width: 22%; float: left;" class="label">
                                    Vendor Name :<span style="color: red">*</span>
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="validate[required] TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="width: 50%; float: left;">
                                <div style="width: 22%; float: left;" class="label">
                                    Address :<span style="color: red">*</span>
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="width: 50%; float: left;">
                                <div style="width: 22%; float: left; margin-top: 10px;" class="label">
                                    TINGST :
                                </div>
                                <div style="float: left; width: 78%; margin-top: 10px;">
                                    <asp:TextBox ID="txtTinGst" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="width: 50%; float: left;">
                                <div style="width: 22%; float: left; margin-top: 10px;" class="label">
                                    TINCST :
                                </div>
                                <div style="margin-top: 10px; float: left; width: 78%;">
                                    <asp:TextBox ID="txtTinCst" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="margin-top: 10px; width: 50%; float: left;">
                                <div style="width: 22%; float: left;" class="label">
                                    Tax Reg. No :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtTaxRegNo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; width: 50%; float: left;">
                                <div style="width: 22%; float: left;" class="label">
                                    Telephone No. :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="margin-top: 10px; width: 50%; float: left;">
                                <div style="width: 22%; float: left;" class="label">
                                    Mobile No. :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; width: 50%; float: left;">
                                <div style="width: 22%; float: left;" class="label">
                                    FAX :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; margin-top: 10px; width: 50%;">
                                <div style="text-align: left; width: 22%; float: left;" class="label">
                                    Email :
                                </div>
                                <div style="text-align: left; float: left; width: 78%;">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="validate[custom[email]] TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; margin-top: 10px; width: 50%;">
                                <div style="width: 22%; float: left;" class="label">
                                    Bank Name :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; margin-top: 10px; width: 50%;">
                                <div style="width: 22%; float: left;" class="label">
                                    Account No. :
                                </div>
                                <div style="text-align: left; float: left; width: 78%;">
                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; margin-top: 10px; width: 50%;">
                                <div style="width: 22%; float: left;" class="label">
                                    Account Name :<span style="color: red">*</span>
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtAccountName" runat="server" CssClass="validate[required] TextBox" Width="300px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; margin-top: 10px; width: 50%;">
                                <div style="width: 22%; float: left;" class="label">
                                    IFSC Code :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; margin-top: 10px; width: 50%;">
                                <div style="width: 22%; float: left;" class="label">
                                    PAN No :
                                </div>
                                <div style="float: left; width: 78%;">
                                    <asp:TextBox ID="txtPanNo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="text-align: right; width: 100%;">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
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
    </script>
</asp:Content>
