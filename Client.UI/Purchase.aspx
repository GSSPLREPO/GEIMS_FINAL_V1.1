<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Purchase.aspx.cs" Inherits="GEIMS.Client.UI.Purchase" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $("#divTotalAmount").hide();
        });
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(document).ready(function () {
            var tab = $(document.getElementById('<%= hfTab.ClientID %>')).val();

            if (tab == "0") {
                $("#aspnetForm").validationEngine();
                $("#btnAddNewMain").hide();
                $("#btnViewListMain").show();
                $("#divPurchase").show();
                $("#divSearch").hide();
                $("#divTransfer").hide();
                $("#divConsumption").hide();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $("#divMultipleMaterialTransfer").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).show();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                //$(document.getElementById('<%= tabs.ClientID %>')).tabs();
                //btnAddNewMain();
                // btnViewListMain();
            }
            else if (tab == "1") {
                $(document.getElementById('<%= tabs1.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs1.ClientID %>')).show();
                $(document.getElementById('<%= tabs.ClientID %>')).hide();
                $("#divTransfer").show();
                $("#divConsumption").hide();
                $("#divTransferSearch").hide();
                $("#tabs1").show();
                $("#divPurchase").hide();
                $("#divMaterialGroup").hide();
                $("#divSingleMaterialTransfer").hide();
                $("#divMultipleMaterialTransfer").hide();
                $("#divSchoolOrTrust").hide();
                $("#divBtnTransfer").hide();
                $('input:radio[name=rblMaterialSelection]').attr('checked', false);
                var radiolist = $('#<%= rblMaterialSelection.ClientID %>').find('input:radio');
                radiolist.removeAttr('checked');
                $("#btnAddNewMain").hide();
                $("#btnViewListMain").show();
                // $("#divMultipleMaterialTransfer").hide();
                // $("#divSingleMaterialTransfer").show();
            }
            else if (tab == "2") {
                $(document.getElementById('<%= tabs1.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $(document.getElementById('<%= tabs.ClientID %>')).hide();
                $("#divTransfer").show();
                $("#divTransferSearch").show();
                $("#tabs1").hide();
                $("#divConsumption").hide();
                $("#divPurchase").hide();
                $("#divMaterialGroup").hide();
                $("#divSingleMaterialTransfer").hide();
                $("#divMultipleMaterialTransfer").hide();
                $("#divSchoolOrTrust").hide();
                $("#divBtnTransfer").hide();
                $('input:radio[name=rblMaterialSelection]').attr('checked', false);
                var radiolist = $('#<%= rblMaterialSelection.ClientID %>').find('input:radio');
                radiolist.removeAttr('checked');
                ClearOnTransfer();
                $("#btnAddNewMain").show();
                // $("#btnAddNewMain").;
                $("#btnViewListMain").hide();
                // btnAddNewMain();
            }
            else if (tab == "3") {
              //  alert("3");
                btnViewList();
            }
            else if (tab == "4") {
                btnAddNew();
                var optionhtml1 = '<option value="">' + "--Select--" + '</option>';
                $(document.getElementById('<%= lbMaterialConsume.ClientID %>')).append(optionhtml1);
            }
            else if (tab == "5") {
                btnAddNew();
            }
            else if (tab == "6") {
                $("#divPurchase").show();
                $("#divSearch").show();
                //$("#divPurchaseTab").show();
                $("#divTransfer").hide();
                $("#divConsumption").hide();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $("#divMultipleMaterialTransfer").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).hide();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
            }
            else if (tab == "8") {
                $("#divPurchase").show();
                $("#divSearch").hide();
                //$("#divPurchaseTab").show();
                $("#divTransfer").hide();
                $("#divConsumption").hide();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $("#divMultipleMaterialTransfer").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).show();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px; padding-right:10px">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            <div style="width: 80%; float: left">
                <span style="font-size: 18px;">Select Operation: </span>&nbsp;&nbsp;
            <asp:RadioButton ID="rbtnPurchase" Text="Purchase" Checked="true" runat="server" GroupName="Material" />&nbsp;&nbsp;
            <asp:RadioButton ID="rbtnTransfer" Text="Transfer" runat="server" GroupName="Material" />&nbsp;&nbsp;
            <asp:RadioButton ID="rbtnConsumption" Text="Consumption" runat="server" GroupName="Material" />
            </div>
            <div style="width: 20%; float: left">
                <%--<asp:Button runat="server" ID="btnAddNewMain" Text="Add New" CssClass="btn-blue-new btn-blue-medium Detach" />--%>
                <button id="btnAddNewMain" type="button" class="btn-blue btn-blue-medium Detach">Add New</button>&nbsp;&nbsp;&nbsp;
                                       <%-- <asp:Button runat="server" ID="btnViewListMain" Text="ViewList" CssClass="btn-blue-new btn-blue-medium Detach" />--%>
                <button id="btnViewListMain" type="button" class="btn-blue btn-blue-medium Detach">View List</button>
            </div>
        </div>
        <div id="divContent" runat="server" style="height: 100%; font-family: Verdana; padding-bottom: 10px;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left;">
                <div id="divPurchase" style="font-family: verdana">
                    <div id="divSearch" style="width: 100%; float: left; padding-bottom: 10px;">
                        <fieldset>
                            <legend style="font-size: 13px;"><b>Search Purchase Details</b></legend>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="float: left; width: 100%; text-align: center;">
                                    From Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    To Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:Button runat="server" ID="btnGo" Text="Go" OnClick="btnGo_OnClick" CssClass="btn-blue-new btn-blue-medium Attach" />
                                </div>
                            </div>
                        </fieldset>
                        <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvPurchase" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvPurchase_OnRowCommand">
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Date & Time">
                                        <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BillNo" HeaderText="Bill No">
                                        <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Year" HeaderText="Year">
                                        <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount">
                                        <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png" CssClass="Detach"
                                                CommandName="EditPurchase" CommandArgument='<%# Eval("PurchaseID")%>' Height="20px" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png" CssClass="Detach"
                                                CommandName="DeletePurchase" CommandArgument='<%# Eval("PurchaseID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                Height="20px" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="divPurchaseTab">
                        <div id="tabs" runat="server" class="style-tabs" visible="True" style="width: 100%; float: left;padding:5px 5px 5px 5px">
                            <div style="float: left; width: 100%;">
                                <ul>
                                    <li><a id="tabPurchaseDetails" href="#tabs-1">Purchase Details</a></li>
                                </ul>
                            </div>
                            <div id="tabs-1" class="gradientBoxesWithOuterShadows" style="float: left; width: 100%;">
                                <div id="Div1" style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <fieldset>
                                            <legend style="font-size: 13px;"><b>Basic Details</b></legend>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Vendor Name :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="float: left; width: 85%;">
                                                        <asp:DropDownList ID="ddlVendorName" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--<div style="float: left; width: 15%;">
                                        Year :<span style="color: red;">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass=" Droptextarea" Width="160px" Enabled="true">
                                        </asp:DropDownList>
                                    </div>--%>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        PO No.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtPONo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        PO Date.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtPoDate" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtPoDate" TargetControlID="txtPoDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Bill No.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        Bill Date.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtBillDate" TargetControlID="txtBillDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Voucher No.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtVoucharNo" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        Voucher Date.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtVoucharDate" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtVoucharDate" TargetControlID="txtVoucharDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Payment Type:<span style="color: red">*</span>
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass=" Droptextarea" Width="160px" Enabled="true">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Advance</asp:ListItem>
                                                            <asp:ListItem Value="2">As Per Bill</asp:ListItem>
                                                            <asp:ListItem Value="3">Post</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        Payment Mode.:<span style="color: red">*</span>
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass=" Droptextarea" Width="160px" Enabled="true">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Cash</asp:ListItem>
                                                            <asp:ListItem Value="2">Credit</asp:ListItem>
                                                            <asp:ListItem Value="3">Bank</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        VAT:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtVAT" runat="server" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)" MaxLength="10" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        Add VAT.:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtAddVAT" runat="server" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)" MaxLength="10" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        CST:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtCST" runat="server" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)" MaxLength="10" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        Discount:
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)" MaxLength="10" Width="150px" Height="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divPurchaseUpdate" runat="server" style="width: 100%; float: left;" class="label">
                                                <div style="float: left; text-align: right; width: 100%;">
                                                    <asp:Button runat="server" ID="btnPurchaseUpdate" Text="Update" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div id="subDiv" style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <fieldset>
                                            <legend style="font-size: 13px;"><b>Material Details</b></legend>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div style="width: 100%; float: left;" class="label">
                                                        <div style="padding: 10px;">
                                                            <div style="float: left; width: 15%;">
                                                                Material Group :<span style="color: red">*</span>
                                                            </div>
                                                            <div style="float: left; width: 35%;">
                                                                <asp:DropDownList ID="ddlMaterialGroupName" runat="server" OnSelectedIndexChanged="ddlMaterialGroupName_OnSelectedIndexChanged" AutoPostBack="True" CssClass=" Droptextarea" Width="260px" Enabled="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlMaterialGroupName"
                                                                    ErrorMessage="Enter Material Group Name." SetFocusOnError="true" ValidationGroup="g1" InitialValue=""
                                                                    ForeColor="red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div style="float: left; width: 15%;">
                                                                Material :<span style="color: red">*</span>
                                                            </div>
                                                            <div style="float: left; width: 35%;">
                                                                <asp:DropDownList ID="ddlMaterialName" runat="server" CssClass=" Droptextarea" AutoPostBack="True" Width="260px" Enabled="true" OnSelectedIndexChanged="ddlMaterialName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMaterialName"
                                                                    ErrorMessage="Enter Material Name." SetFocusOnError="true" ValidationGroup="g1" InitialValue=""
                                                                    ForeColor="red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%; float: left;" class="label">
                                                        <div style="padding: 10px;">
                                                            <div style="float: left; width: 15%;">
                                                                Quantity :<span style="color: red">*</span>
                                                            </div>
                                                            <div style="float: left; width: 35%;">
                                                                <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="True" onkeypress="return NumericKeyPressFraction(event)" CssClass=" TextBox" Width="75px" Height="100%" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlMaterialName"
                                                                    ErrorMessage="Enter Quantity." SetFocusOnError="true" ValidationGroup="g1" InitialValue=""
                                                                    ForeColor="red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div style="float: left; width: 15%;">
                                                                UOM :<span style="color: red">*</span>
                                                            </div>
                                                            <div style="float: left; width: 35%;">
                                                                <asp:DropDownList ID="ddlUOM" runat="server" CssClass=" Droptextarea" Width="160px" Enabled="False">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMaterialName"
                                                                    ErrorMessage="Enter UOM." SetFocusOnError="true" ValidationGroup="g1" InitialValue=""
                                                                    ForeColor="red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%; float: left;" class="label">
                                                        <div style="padding: 10px;">
                                                            <div style="float: left; width: 15%;">
                                                                Per Unit Price(In Rs.) :<span style="color: red">*</span>
                                                            </div>
                                                            <div style="float: left; width: 35%;">
                                                                <asp:TextBox ID="txtPerUnitPrice" runat="server" AutoPostBack="True" CssClass=" TextBox" onkeypress="return NumericKeyPressFraction(event)" Width="75px" Height="100%" OnTextChanged="txtPerUnitPrice_TextChanged"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPerUnitPrice"
                                                                    ErrorMessage="Enter UOM." SetFocusOnError="true" ValidationGroup="g1" InitialValue=""
                                                                    ForeColor="red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div style="float: left; width: 15%;">
                                                                Total Amount :<span style="color: red">*</span>
                                                            </div>
                                                            <div style="float: left; width: 35%;">
                                                                <asp:TextBox ID="txtMaterialTotalAmount" runat="server" onkeypress="return NumericKeyPressFraction(event)" CssClass=" TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="hfMaterialID" runat="server" />
                                                    <asp:HiddenField ID="hf" runat="server" />
                                                    <div style="width: 100%; float: left;" class="label">
                                                        <div style="padding: 10px;">
                                                            <%--<div style="float: left; width: 15%; display: none;">
                                                    Delivery Note :
                                                </div>
                                                <div style="float: left; width: 35%; display: none;">
                                                    <asp:TextBox ID="txtDeliveryNote" runat="server" CssClass="TextArea" TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox>
                                                </div>--%>

                                                            <div style="text-align: right; float: left; vertical-align: bottom; width: 100%;">
                                                                <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="btn-blue-new btn-blue-medium" ValidationGroup="g1" OnClick="btnAdd_Click" />
                                                                <asp:Button runat="server" ID="btnUpdateMaterial" Text="Update" CssClass="btn-blue-new btn-blue-medium" Visible="False" OnClick="btnUpdateMaterial_OnClick" />&nbsp;
                                                                <button id="btnCancel" type="button" class="Detach btn-blue-new btn-blue-medium">Cancel</button>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; float: left; padding-bottom: 10px; display: none; width: 100%;">
                                                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="False" Visible="False"
                                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvMaterial_RowCommand">
                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchaseTID" HeaderText="PurchaseTID">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="hidden" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="left" CssClass="hidden" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MaterialName" HeaderText="Material Name">
                                                                    <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                    <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount">
                                                                    <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="Right" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png" CssClass="Detach"
                                                                            CommandName="EditMaterial" CommandArgument='<%# Eval("PurchaseTID")%>' Height="20px" Width="20px" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" />
                                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png" CssClass="Detach"
                                                                            CommandName="DeleteMaterial" CommandArgument='<%# Eval("PurchaseTID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                                            Height="20px" Width="20px" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="center" />
                                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" ForeColor="White" Font-Size="12px" />
                                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="divTotalAmount" style="padding: 10px;">
                                                        <div style="float: left; font-size: 17px; width: 15%;">
                                                            Total Amount :
                                                        </div>
                                                        <div style="float: left; width: 35%;">
                                                            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                                        </div>
                                                        <asp:HiddenField ID="hfTotalamt" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <%--<asp:AsyncPostBackTrigger ControlID="ddlsection" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlClass" EventName="SelectedIndexChanged" />--%>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </div>
                                    <div id="divPurchaseSave" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hfUOM" runat="server" />
                                    <asp:HiddenField ID="hfMaterial" runat="server" />
                                    <asp:HiddenField ID="hfValidate" runat="server" />
                                    <asp:HiddenField ID="hfTab" runat="server" />
                                    <asp:HiddenField ID="hfMode" runat="server" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div id="divTransfer">
                    <div id="divTransferSearch" style="width: 100%; float: left; padding-bottom: 10px;">
                        <fieldset>
                            <legend style="font-size: 13px;"><b>Search Transfer Details</b></legend>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="float: left; width: 100%; text-align: center;">
                                    From Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTransferFromDate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTransferFromDate" TargetControlID="txtTransferFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    To Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTransferTodate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTransferTodate" TargetControlID="txtTransferTodate">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:Button runat="server" ID="BtnGoTransfer" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="BtnGoTransfer_Click" />
                                </div>
                            </div>
                        </fieldset>
                        <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvTransfer" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Date & Time">
                                        <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MaterialName" HeaderText="Material">
                                        <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TransferTo1" HeaderText="Transferred To">
                                        <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MainQuantity" HeaderText="Availabel Stock">
                                        <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Transferred Stock">
                                        <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" Width="10%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>

                                </Columns>
                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>

                    <div id="tabs1" runat="server" class="style-tabs" visible="True" style="width: 100%; float: left;padding:5px 5px 5px 5px">
                        <div style="float: left; width: 100%;">
                            <ul>
                                <li><a id="tabTransferDetails" href="#tabs-2">Transfer Details</a></li>
                            </ul>
                        </div>
                        <div id="tabs-2" class="gradientBoxesWithOuterShadows" style="float: left; width: 100%;">
                            <asp:HiddenField ID="hfMaterialGroup" runat="server" />
                            <asp:HiddenField ID="hfMaterialName" runat="server" />
                            <asp:HiddenField ID="hfAvailableQuantity" runat="server" />
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Transfer Type:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:RadioButtonList ID="rblMaterialSelection" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Transfer Single Material</asp:ListItem>
                                            <asp:ListItem Value="1">Transfer Multiple Material</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div id="divSchoolOrTrust" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Transfer To:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:RadioButtonList ID="rblSchoolOrTrust" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Value="0" Selected="True">School</asp:ListItem>
                                            <asp:ListItem Value="1">Trust</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div id="divMaterialGroup" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Material Group :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlTMaterialGroup" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div style="width: 100%" id="divSingleMaterialTransfer">
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 15%;">
                                            Material :<span style="color: red">*</span>
                                        </div>
                                        <div style="float: left; width: 35%;">
                                            <asp:DropDownList ID="ddlTMaterialName" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 15%;">
                                            UOM :<span style="color: red">*</span>
                                        </div>
                                        <div style="float: left; width: 35%;">
                                            <asp:DropDownList ID="ddlTUOM" runat="server" CssClass="Droptextarea" Width="160px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 15%;">
                                            Available Quantity :<span style="color: red">*</span>
                                        </div>
                                        <div style="float: left; width: 85%;">
                                            <asp:TextBox ID="txtAvailableQuantity" runat="server" CssClass="TextBox" Width="150px" Height="100%">0</asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="padding: 10px; padding-right: 30px;">
                                        <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                            <asp:Button runat="server" ID="btnViewSchools" Text="View Transfer" CssClass="btn-blue-new btn-blue-medium" OnClick="btnViewSchools_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: center; padding-top: 10px; float: left; padding-bottom: 10px; width: 100%;">
                                    <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvSchoolForMaterial" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                                <FooterStyle BackColor="White" ForeColor="White" />
                                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="SchoolMID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <%--<input type="checkbox" id="chkChild" />--%>
                                                            <asp:CheckBox ID="chkChild" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SchoolNameEng" HeaderText="Schools">
                                                        <HeaderStyle Width="70%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="70%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Stock">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuantity" runat="server" Width="150px" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)">0</asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalQuantity" runat="server" CssClass="lblTotalQuantity" Text="0" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnViewSchools" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                            <div style="width: 100%" id="divMultipleMaterialTransfer">
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 15%;">
                                            Material :<span style="color: red">*</span>
                                        </div>
                                        <div style="float: left; width: 20%;">
                                            <asp:ListBox ID="lbMaterial" runat="server" Height="150px" Width="150px" SelectionMode="Multiple" CssClass="selector"></asp:ListBox>
                                        </div>
                                        <div style="float: left; width: 65%; padding-top: 110px;">
                                            <asp:Button runat="server" ID="btnAddMaterial" Text="Add Material" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnAddMaterial_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: center; padding-top: 10px; float: left; padding-bottom: 10px; width: 100%;">
                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvMultipleMaterial" runat="server" AutoGenerateColumns="False" ShowFooter="False"
                                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowDataBound="gvMultipleMaterial_RowDataBound">
                                                <FooterStyle BackColor="White" ForeColor="White" />
                                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="MaterialID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MaterialTID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UOMID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MaterialName" HeaderText="Material">
                                                        <HeaderStyle Width="40%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UOMName" HeaderText="UOM">
                                                        <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Available Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMainQuantity" runat="server" Text='<%# Bind("MainQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuantity" runat="server" Width="150px" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)">0</asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="School Selection">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" CssClass="TextBox" ID="ddlGridSchool" Width="250px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAddMaterial" EventName="Click" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnTrnasfer" EventName="Click" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div id="divBtnTransfer" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                        <asp:Button runat="server" ID="btnTrnasfer" Text="Transfer" CssClass="btn-blue-new btn-blue-medium" OnClick="btnTrnasfer_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="padding: 10px;">
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
                <div id="divConsumption">
                    <div id="divButtons" style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="padding: 10px; padding-right: 30px;">
                            <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                <%--   <asp:Button runat="server" ID="btnAddNewConsume" Text="Add New" CssClass="btn-blue-new btn-blue-medium Detach" OnClick="btnAddNewConsume_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnViewListConsume" Text="ViewList" CssClass="btn-blue-new btn-blue-medium Detach" OnClick="btnViewListConsume_Click" />--%>
                                <%--<button id="btnAddNew" type="button" class="btn-blue-new btn-blue-medium">Add New</button>&nbsp;&nbsp;&nbsp;--%>
                                <%--<button id="btnViewList" type="button" class="btn-blue-new btn-blue-medium">View List</button>--%>
                            </div>
                        </div>
                    </div>

                    <div id="divSearchConsume" style="width: 100%; float: left; padding-bottom: 10px;">
                        <fieldset>
                            <legend style="font-size: 13px;"><b>Search Consumption Details</b></legend>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="float: left; width: 100%; text-align: center;">
                                    <div style="padding: 10px; padding-right: 30px;">
                                        <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                            From Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFromConsume" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromConsume" TargetControlID="txtFromConsume">
                                    </ajaxToolkit:CalendarExtender>
                                            To Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtToConsume" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender10" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToConsume" TargetControlID="txtToConsume">
                                    </ajaxToolkit:CalendarExtender>

                                            <asp:Button runat="server" ID="btnGoConsume" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGoConsume_Click" />
                                        </div>
                                    </div>
                                </div>

                                <%--   <div style="float: left; width: 100%; text-align: center;">
                                    <div style="padding: 10px; padding-right: 30px;">
                                        <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                            Select Type:<span style="color: red">*</span>
                                            <asp:DropDownList runat="server" ID="ddlSelectType" Width="200px">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="0">Consumption</asp:ListItem>
                                                <asp:ListItem Value="1">Return</asp:ListItem>
                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </fieldset>
                        <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvConsumption" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvConsumption_RowCommand">
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                <Columns>

                                    <asp:BoundField DataField="MaterialName" HeaderText="Material">
                                        <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MainQuantity" HeaderText="Availabel Stock">
                                        <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Consume Quantity">
                                        <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ConsumptionDate" HeaderText="Date & Time">
                                        <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/Edit.png"
                                                CommandName="Edit1" CommandArgument='<%# Eval("ConsumptionID") + "," + Eval("MaterialID")%>' Height="20px" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                                CommandName="DeleteConsume" CommandArgument='<%# Eval("ConsumptionID") + "," + Eval("MaterialID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                Height="20px" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tab2" runat="server" class="style-tabs" visible="True" style="width: 100%; float: left;padding:5px 5px 5px 5px">
                        <div style="float: left; width: 100%;">
                            <ul>
                                <li><a id="tabConsumptionDetails" href="#tabs-2">Consumption Details</a></li>
                            </ul>
                        </div>
                        <div id="tab-3" class="gradientBoxesWithOuterShadows" style="float: left; width: 100%;">
                            <asp:HiddenField ID="hfMaterialConsume" runat="server" />
                            <asp:HiddenField ID="hfMaterialGroupConsume" runat="server" />
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                            <%--      <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Select Type:<span style="color: red">*</span>
                                    </div>
                                 <div style="float: left; width: 85%;">
                                        <asp:RadioButtonList ID="rblSelectType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Material Consumption</asp:ListItem>
                                            <asp:ListItem Value="1">Material Return</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>--%>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDate" TargetControlID="txtDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div id="div5" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Material Group :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlMaterialGroupConsume" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Material :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        <asp:ListBox ID="lbMaterialConsume" runat="server" Height="150px" Width="180px" SelectionMode="Multiple" CssClass="selector"></asp:ListBox>
                                    </div>
                                    <div style="float: left; width: 65%; padding-top: 110px;">
                                        <asp:Button runat="server" ID="btnAddMaterialConsume" Text="Add Material" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnAddMaterialConsume_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%">
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center; padding-top: 10px; float: left; padding-bottom: 10px; width: 100%;">
                                            <asp:GridView ID="gvMaterialConsume" runat="server" AutoGenerateColumns="False" ShowFooter="False"
                                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                                <FooterStyle BackColor="White" ForeColor="White" />
                                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="MaterialID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MaterialTID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UOMID" HeaderText="ID">
                                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                                        <FooterStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MaterialName" HeaderText="Material">
                                                        <HeaderStyle Width="40%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UOMName" HeaderText="UOM">
                                                        <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Available Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMainQuantityConsume" runat="server" Text='<%# Bind("MainQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuantityConsume" runat="server" Width="150px" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)">0</asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                        <div id="divButtonConsume" style="width: 100%; float: right; text-align: right; padding-top: 10px;" class="label">
                                            <%--<div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">--%>
                                            <%--<asp:Button runat="server" ID="btnSaveConsume" Text="Consume/Return" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveConsume_Click" />--%>
                                            <asp:Button ID="btnSaveConsume" runat="server" Text="Consume" CssClass="btn-blue-new btn-blue-medium" CausesValidation="false" OnClick="btnSaveConsume_Click" Visible="False" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAddMaterialConsume" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveConsume" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div style="padding: 10px;">
                            <div class="clear"></div>
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

        $('#<%= btnSave.ClientID %>').click(function () {
            $('#<%= ddlVendorName.ClientID %>').addClass("validate[required]");
            $('#<%= ddlPaymentMode.ClientID %>').addClass("validate[required]");
            $('#<%= ddlPaymentType.ClientID %>').addClass("validate[required]");
            $('#<%= ddlMaterialGroupName.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlMaterialName.ClientID %>').removeClass("validate[required]");
            $('#<%= txtQuantity.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlUOM.ClientID %>').removeClass("validate[required]");
            $('#<%= txtPerUnitPrice.ClientID %>').removeClass("validate[required]");
            $('#<%= txtMaterialTotalAmount.ClientID %>').removeClass("validate[required]");
        });

        $("#divTitle input[name*='Material']").click(function () {
            if ($('input:radio[id*=rbtnPurchase]:checked').val() == "rbtnPurchase") {
                $("#divPurchase").show();
                $("#divTransfer").hide();
                $("#divConsumption").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).show();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $(document.getElementById('<%= hfMode.ClientID %>')).text("Save");
                $('input:radio[name=rblMaterialSelection]').attr('checked', false);
                var radiolist = $('#<%= rblMaterialSelection.ClientID %>').find('input:radio');
                radiolist.removeAttr('checked');
                $('#<%= lbMaterialConsume.ClientID %>').empty();
                $("#divTotalAmount").hide();
            }
            else if ($('input:radio[id*=rbtnTransfer]:checked').val() == "rbtnTransfer") {
                $("#divPurchase").hide();
                $("#divTransfer").show();
                $("#divConsumption").hide();
                $("#divMaterialGroup").hide();
                $("#divSingleMaterialTransfer").hide();
                $("#divSchoolOrTrust").hide();
                $("#divTransferSearch").hide();
                $("#tabs1").show();
                $(document.getElementById('<%= tabs1.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs1.ClientID %>')).show();
                $("#divBtnTransfer").hide();
                $(document.getElementById('<%= hfMode.ClientID %>')).text("Save");
                $('#<%= lbMaterialConsume.ClientID %>').empty();
                CleanPurchase();
               
            }
            else if ($('input:radio[id*=rbtnConsumption]:checked').val() == "rbtnConsumption") {
                btnAddNew();
                $(document.getElementById('<%= hfMode.ClientID %>')).text("Save");
                $('input:radio[name=rblMaterialSelection]').attr('checked', false);
                var radiolist = $('#<%= rblMaterialSelection.ClientID %>').find('input:radio');
                radiolist.removeAttr('checked');
                CleanPurchase();
            }
        });



$('#<%= ddlTMaterialGroup.ClientID %>').change(function () {

            $('#<%=rblMaterialSelection.ClientID%> input:radio:checked').each(function () {
                if (($(this).val()) == "0") {
            $("#divMultipleMaterialTransfer").hide();
        }
        else {
            $("#divMultipleMaterialTransfer").show();
        }
    });

    $('#<%= ddlTMaterialName.ClientID %>').empty();
    $('#<%= lbMaterial.ClientID %>').empty();
    $('#<%= ddlTUOM.ClientID %>').empty();
    $(document.getElementById('<%= txtAvailableQuantity.ClientID %>')).val("0");

    var MaterialGroupId = $('#<%=ddlTMaterialGroup.ClientID %>').val();
    var MaterialGroupName = $('#<%=ddlTMaterialGroup.ClientID %> option:selected').text();

    if (MaterialGroupId != '') {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Purchase.aspx/GetMaterial",
            data: "{'MaterialGroupID':'" + MaterialGroupId + "','TrustID':'" + <%=Session[ApplicationSession.TRUSTID] %> + "','SchoolID':'" + <%=Session[ApplicationSession.SCHOOLID] %> + "'}",
            dataType: "json",
            success: function (data) {
                var count = -1;
                var temp = $.parseJSON(data.d);

                var optionhtml1 = '<option value="">' + "--Select--" + '</option>';
                $(document.getElementById('<%= ddlTMaterialName.ClientID %>')).append(optionhtml1);


                $.each(temp, function (i) {
                    count = i;
                    var optionhtml = '<option value="' +
                        temp[i].MaterialID + '">' + temp[i].MaterialName + '</option>';
                    $(document.getElementById('<%= ddlTMaterialName.ClientID %>')).append(optionhtml);
                    $(document.getElementById('<%= lbMaterial.ClientID %>')).append(optionhtml);
                });
                if (count == "-1") {
                    alert("Material is not exist for" + MaterialGroupName);
                }
            },
            error: function (error) {
                alert(error);
            }

        });
    } else {
        alert("Select at least one Material Group.");
    }
});



$('#<%=rblMaterialSelection.ClientID%>').click(function () {
            $('#<%=rblMaterialSelection.ClientID%> input:radio:checked').each(function () {
            if (($(this).val()) == "0") {
                ClearOnTransfer();
                $("#divSingleMaterialTransfer").show();
                $("#divMultipleMaterialTransfer").hide();
                $("#divMaterialGroup").show();
                $("#divSchoolOrTrust").show();
            }
            else {
                ClearOnTransfer();
                $("#divSingleMaterialTransfer").hide();
                $("#divMultipleMaterialTransfer").hide();
                $("#divMaterialGroup").show();
                $("#divSchoolOrTrust").show();
            }
        });
    });

        function CleanPurchase() {
            $(document.getElementById('<%= ddlVendorName.ClientID %>')).val('');
            $(document.getElementById('<%= txtPoDate.ClientID %>')).val('');
            $(document.getElementById('<%= txtPONo.ClientID %>')).val('');
            $(document.getElementById('<%= txtBillDate.ClientID %>')).val('');
            $(document.getElementById('<%= txtBillNo.ClientID %>')).val('');
            $(document.getElementById('<%= txtVoucharDate.ClientID %>')).val('');

            $(document.getElementById('<%= txtVoucharNo.ClientID %>')).val('');
            $(document.getElementById('<%= ddlPaymentMode.ClientID %>')).val('');
            $(document.getElementById('<%= ddlPaymentType.ClientID %>')).val('');
            $(document.getElementById('<%= txtVAT.ClientID %>')).val('');
            $(document.getElementById('<%= txtAddVAT.ClientID %>')).val('');
            $(document.getElementById('<%= txtCST.ClientID %>')).val('');

            $(document.getElementById('<%= txtDiscount.ClientID %>')).val('');
            $(document.getElementById('<%= ddlMaterialGroupName.ClientID %>')).val('');
            $(document.getElementById('<%= ddlMaterialName.ClientID %>')).val('');
            $(document.getElementById('<%= txtQuantity.ClientID %>')).val('');
            $(document.getElementById('<%= ddlUOM.ClientID %>')).val('');
            $(document.getElementById('<%= txtPerUnitPrice.ClientID %>')).val('');

            $(document.getElementById('<%= txtMaterialTotalAmount.ClientID %>')).val('');
            $(document.getElementById('<%= gvMaterial.ClientID %>')).hide();
            $(document.getElementById('<%= txtTotalAmount.ClientID %>')).val('');
            $("#divTotalAmount").hide();
        }

    $(document.getElementById('<%= ddlTMaterialName.ClientID %>')).change(function () {
            $('#<%= ddlTUOM.ClientID %>').empty();
        var MaterialId = $('#<%=ddlTMaterialName.ClientID %> option:selected').val();
        var MaterialName = $('#<%=ddlTMaterialName.ClientID %> option:selected').text();
        var SchoolMID = "0";

        if (MaterialId != '') {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Purchase.aspx/GetUOM",
                data: "{'MaterialID':'" + MaterialId + "'}",
                dataType: "json",
                success: function (data) {
                    var count = -1;
                    var temp = $.parseJSON(data.d);

                    $.each(temp, function (i) {
                        count = i;
                        var optionhtml = '<option value="' +
                            temp[i].UOMID + '">' + temp[i].UOMName + '</option>';

                        $(document.getElementById('<%= ddlTUOM.ClientID %>')).append(optionhtml);

                    });
                    if (count == "-1") {
                        alert("UOM is not exist for " + MaterialName);
                    }
                },
                error: function (error) {
                    alert(error);
                }

            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Purchase.aspx/GetMaterialQuantity",
                data: "{'MaterialID':'" + MaterialId + "','TrustID':'" + <%=Session["TrustID"] %> + "','SchoolID':'" + SchoolMID + "'}",
                dataType: "json",
                success: function (data) {
                    var count = -1;
                    var temp = $.parseJSON(data.d);

                    $.each(temp, function (i) {
                        count = i;
                        $(document.getElementById('<%= txtAvailableQuantity.ClientID %>')).val(temp[i].MainQuantity);
                        $(document.getElementById('<%= hfAvailableQuantity.ClientID %>')).val(temp[i].MainQuantity);
                    });
                    if (count == "-1") {
                        alert("Quanity is not exist for " + MaterialName);
                    }
                },
                error: function (error) {
                    alert(error);
                }
            });
        } else {
            alert("Select at least one Material.");
        }
    });
    //Sys.Application.add_load({
    function gridChekbox() {

        $('#<%= btnSaveConsume.ClientID %>').click(function () {
            //var valid = $("#aspnetForm").validationEngine('validate');
            //var vars = $("#aspnetForm").serialize();
            var i = 0;
            var rowCount = $('#<%=gvMaterialConsume.ClientID %> tr').length;
            $('#<%=gvMaterialConsume.ClientID %> tr').each(function () {
                if (i != 0) {

                    $(this).closest("tr").find($("[id*=txtQuantityConsume]")).addClass("validate[required, min[1]]");
                    //var ddlSchoolGrid = $("ctl00_ContentPlaceHolder1_gvMultipleMaterial_ctl02_ddlGridSchool").text;

                    var valid = $("#aspnetForm").validationEngine('validate');
                    var vars = $("#aspnetForm").serialize();
                    //if (ddlSchoolGrid == '') {
                    //valid = false;
                    //}

                    if (valid == false) {
                        $('#<%=hfValidate.ClientID %>').val("0");
                    } else {
                        $('#<%=hfValidate.ClientID %>').val("1");
                    }
                }
                i = i + 1;
            });

        });

        $("[id*=chkChild]").click(function () {
            if ($(this).is(":checked")) {
                $("#divBtnTransfer").show();
            } else {

            }

            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                $("#divBtnTransfer").show();
            }

            if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                $("#divBtnTransfer").hide();
            }
        });

        $('#<%=gvSchoolForMaterial.ClientID %>').find("input:checkbox").click(function () {

            if ($(this).is(":checked")) {
                //  alert("alert");
                $("#divBtnTransfer").show();
                var Stock = parseInt($(this).closest("tr").find($("[id*=txtQuantity]")).val());
                var MainQuantity = parseInt($(document.getElementById('<%= hfAvailableQuantity.ClientID %>')).val());
              //  alert($(document.getElementById('<%= hfAvailableQuantity.ClientID %>')).val());
                $(this).closest("tr").find($("[id*=txtQuantity]")).prop('readonly', true);
                // alert(MainQuantity);
                // alert(Stock);
                if (MainQuantity < Stock) {
                    //  alert(MainQuantity);
                    alert("Stock is Not Greater Than Main Quantity");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).val("0");
                    $(this).closest("tr").find($("[id*=chkChild]")).removeAttr("checked");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).prop('readonly', false);
                } else if (parseInt($(this).closest("tr").find($("[id*=txtQuantity]")).val()) <= 0) {
                    alert("stock can never be zero");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).val("0");
                    $(this).closest("tr").find($("[id*=chkChild]")).removeAttr("checked");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).prop('readonly', false);
                }
            } else {
                $(this).closest("tr").find($("[id*=txtQuantity]")).prop('readonly', false);
            }
            CalculateStock();

        });

        $('#<%=gvMultipleMaterial.ClientID %> tr').each(function () {
            var totalStock = 0;
            $(this).change(function (event) {
                var stock = parseInt($(this).closest("tr").find($("[id*=txtQuantity]")).val());
                var Availabel = parseInt($(this).closest("tr").find($("[id*=lblMainQuantity]")).text());

                if (Availabel < stock) {
                    alert("Stock is Not Greater Than Main Quantity");
                    var minusStock = parseInt($(this).closest("tr").find($("[id*=txtQuantity]")).val());
                    $(this).closest("tr").find($("[id*=txtQuantity]")).val("0");
                }
            });
        });

        $('#<%=gvMaterialConsume.ClientID %> tr').each(function () {
            var totalStock = 0;
            $(this).change(function (event) {
                var stock = parseInt($(this).closest("tr").find($("[id*=txtQuantityConsume]")).val());
                var Availabel = parseInt($(this).closest("tr").find($("[id*=lblMainQuantityConsume]")).text());

                if (Availabel < stock) {
                    alert("Stock is Not Greater Than Main Quantity");
                    $(this).closest("tr").find($("[id*=txtQuantityConsume]")).val("0");
                }
            });
        });
        function CalculateStock() {
            var TotalStock = 0, ToalValue = 0;
            var StockToMinus = 0;
            var i = 0;
            var MainQuantity = parseInt($(document.getElementById('<%= txtAvailableQuantity.ClientID %>')).val());
            var RowCount = $('#<%=gvSchoolForMaterial.ClientID %> tr').length;
            $('#<%=gvSchoolForMaterial.ClientID %> tr').each(function () {
                if (i != 0) {
                    if (i != RowCount - 1) {
                        if ($(this).find('input:checkbox').is(":checked")) {
                            TotalStock += parseInt($(this).closest("tr").find($("[id*=txtQuantity]")).val());
                            StockToMinus = parseInt($(this).closest("tr").find($("[id*=txtQuantity]")).val());
                        }
                    }
                }
                i = i + 1;
                if (MainQuantity < TotalStock) {
                    $(this).closest("tr").find($("[id*=chkChild]")).removeAttr("checked");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).val("0");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).prop('readonly', false);

                }
            });
            $('.lblTotalQuantity').text(TotalStock);
            if (MainQuantity < TotalStock) {
                alert("Total Stock is Not Greater Than Main Quantity");
                ToalValue = TotalStock - StockToMinus;
                $('.lblTotalQuantity').text(parseInt(ToalValue));
                $(this).closest("tr").find($("[id*=txtQuantity]")).prop('readonly', false);
            }
        }


        $('#<%= btnUpdateMaterial.ClientID %>').
        click(function () {
            $('#<%= ddlVendorName.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlPaymentMode.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlPaymentType.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlMaterialGroupName.ClientID %>').addClass("validate[required]");
            $('#<%= ddlMaterialName.ClientID %>').addClass("validate[required]");
            $('#<%= txtQuantity.ClientID %>').addClass("validate[required]");
            $('#<%= ddlUOM.ClientID %>').addClass("validate[required]");
            $('#<%= txtPerUnitPrice.ClientID %>').addClass("validate[required]");
            $('#<%= txtMaterialTotalAmount.ClientID %>').addClass("validate[required]");

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == false) {
                $('#<%=hfValidate.ClientID %>').val("0");
            } else {
                $('#<%=hfValidate.ClientID %>').val("1");
                $('#<%=hfMaterial.ClientID %>').val($('#<%= ddlMaterialName.ClientID %> option:selected').text());
                $('#<%=hfUOM.ClientID %>').val($('#<%= ddlUOM.ClientID %> option:selected').text());
            }
        });
        function Validation() {
            $('#<%= ddlVendorName.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlPaymentMode.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlPaymentType.ClientID %>').removeClass("validate[required]");
            $('#<%= ddlMaterialGroupName.ClientID %>').addClass("validate[required]");
            $('#<%= ddlMaterialName.ClientID %>').addClass("validate[required]");
            $('#<%= txtQuantity.ClientID %>').addClass("validate[required]");
            $('#<%= ddlUOM.ClientID %>').addClass("validate[required]");
            $('#<%= txtPerUnitPrice.ClientID %>').addClass("validate[required]");
            $('#<%= txtMaterialTotalAmount.ClientID %>').addClass("validate[required]");
        }

        $('#<%= btnAdd.ClientID %>').
                    click(function () {
                        $("#divTotalAmount").show();
                        Validation();

                        var valid = $("#aspnetForm").validationEngine('validate');
                        var vars = $("#aspnetForm").serialize();

                        if (valid == false) {
                            $('#<%=hfValidate.ClientID %>').val("0");
                    } else {
                        $('#<%=hfValidate.ClientID %>').val("1");
                        $('#<%=hfMaterial.ClientID %>').val($('#<%= ddlMaterialName.ClientID %> option:selected').text());
                        $('#<%=hfUOM.ClientID %>').val($('#<%= ddlUOM.ClientID %> option:selected').text());
                    }
                });


            }
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(gridChekbox);



            $('#<%= btnTrnasfer.ClientID %>').click(function () {
            //var valid = $("#aspnetForm").validationEngine('validate');
            //var vars = $("#aspnetForm").serialize();
            var i = 0;
            var rowCount = $('#<%=gvMultipleMaterial.ClientID %> tr').length;
                $('#<%=gvMultipleMaterial.ClientID %> tr').each(function () {
                if (i != 0) {

                    //  alert(i);
                    $(this).closest("tr").find($("[id*=ddlGridSchool]")).addClass("validate[required]");
                    $(this).closest("tr").find($("[id*=txtQuantity]")).addClass("validate[required, min[1]]");
                    //var ddlSchoolGrid = $("ctl00_ContentPlaceHolder1_gvMultipleMaterial_ctl02_ddlGridSchool").text;

                    var valid = $("#aspnetForm").validationEngine('validate');
                    var vars = $("#aspnetForm").serialize();
                    //if (ddlSchoolGrid == '') {
                    //valid = false;
                    /// alert(valid);
                    //}

                    if (valid == false) {
                        $('#<%=hfValidate.ClientID %>').val("0");
                        } else {
                            $('#<%=hfValidate.ClientID %>').val("1");
                            $('#<%=hfMaterial.ClientID %>').val($('#<%= ddlTMaterialName.ClientID %> option:selected').text());
                            $('#<%=hfUOM.ClientID %>').val($('#<%= ddlTUOM.ClientID %> option:selected').text());
                        }
                    }
                    i = i + 1;
                });

        });



            $('#<%= btnViewSchools.ClientID %>').click(function () {
            $('#<%= ddlTMaterialGroup.ClientID %>').addClass("validate[required]");
                $('#<%= ddlTMaterialName.ClientID %>').addClass("validate[required]");
                $('#<%= ddlTUOM.ClientID %>').addClass("validate[required]");
                $('#<%= txtAvailableQuantity.ClientID %>').addClass("validate[required]");

                var valid = $("#aspnetForm").validationEngine('validate');
                var vars = $("#aspnetForm").serialize();

                if (valid == false) {
                    $('#<%=hfValidate.ClientID %>').val("0");
            } else {
                $('#<%=hfValidate.ClientID %>').val("1");
                $('#<%=hfMaterial.ClientID %>').val($('#<%= ddlTMaterialName.ClientID %> option:selected').text());
                $('#<%=hfUOM.ClientID %>').val($('#<%= ddlTUOM.ClientID %> option:selected').text());
            }
            });
        $('#<%= btnAddMaterial.ClientID %>').click(function () {

            $("#divBtnTransfer").show();
            $('#<%= ddlTMaterialGroup.ClientID %>').addClass("validate[required]");
            $('#<%= ddlTMaterialName.ClientID %>').removeClass("validate[required]");
            $('#<%= txtAvailableQuantity.ClientID %>').removeClass("validate[required]");
            $('#<%= lbMaterial.ClientID %>').addClass("validate[required,maxList[1]]");
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == false) {
                $('#<%=hfValidate.ClientID %>').val("0");
        } else {
            $('#<%=hfValidate.ClientID %>').val("1");
            $('#<%=hfMaterial.ClientID %>').val($('#<%= ddlTMaterialName.ClientID %> option:selected').text());
            $('#<%=hfUOM.ClientID %>').val($('#<%= ddlTUOM.ClientID %> option:selected').text());
        }
        });

    function ClearOnTransfer() {
        $('#<%=rblMaterialSelection.ClientID%>').click(function () {
        $(document.getElementById('<%= gvSchoolForMaterial.ClientID %>')).hide();
        $(document.getElementById('<%= gvMultipleMaterial.ClientID %>')).hide();
        $('#<%=rblMaterialSelection.ClientID%> input:radio:checked').each(function () {
            if (($(this).val()) == "0") {

                $("#divBtnTransfer").hide();
                $(document.getElementById('<%= ddlTMaterialGroup.ClientID %>')).val('');
                $(document.getElementById('<%= ddlTMaterialName.ClientID %>')).empty();
                $(document.getElementById('<%= ddlTUOM.ClientID %>')).empty();
                $(document.getElementById('<%= txtAvailableQuantity.ClientID %>')).val("0");
            } else {

                $("#divBtnTransfer").hide();
                $(document.getElementById('<%= ddlTMaterialGroup.ClientID %>')).val('');
                $(document.getElementById('<%= lbMaterial.ClientID %>')).empty();
            }
        });
    });
    function ClearOnConsumption() {
        // alert("Clear");
        $(document.getElementById('<%= ddlMaterialGroupConsume.ClientID %>')).val('');
        $(document.getElementById('<%= tab2.ClientID %>')).tabs();
        $(document.getElementById('<%= tab2.ClientID %>')).show();
        $(document.getElementById('<%= tabs1.ClientID %>')).hide();
        $(document.getElementById('<%= tabs.ClientID %>')).hide();
        $("#divTransfer").hide();
        $("#divConsumption").show();
        $("#divPurchase").hide();
        $("#divMaterialGroup").hide();
        $("#divSingleMaterialTransfer").hide();
        $("#divMultipleMaterialTransfer").hide();
        $("#divSchoolOrTrust").hide();
        $("#divBtnTransfer").hide();
    }
}

$('#<%=txtTransferFromDate.ClientID%>').change(function () {
            validateFromTODate();
        });
        $('#<%=txtTransferTodate.ClientID%>').change(function () {
            validateFromTODate();
        });

        function validateFromTODate() {
            var from = $("#<%=txtTransferFromDate.ClientID %>").val();
            var to = $("#<%=txtTransferTodate.ClientID %>").val();

            var dateStrA = from.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");
            var dateStrB = to.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");

            // now you can compare them using:

            var fromDate = new Date(dateStrA);
            var toDate = new Date(dateStrB);

            if (fromDate > toDate) {
                alert('Enter valid Date For Search Data.');
                $("#<%=txtTransferFromDate.ClientID %>").val('');
                $("#<%=txtTransferTodate.ClientID %>").val('');
                return false;
            }
        }

        $('#<%=txtFromConsume.ClientID%>').change(function () {
            validateFromTODate();
        });
        $('#<%=txtToConsume.ClientID%>').change(function () {
            validateFromTODate();
        });

        function validateFromTODate() {
            var from = $("#<%=txtFromConsume.ClientID %>").val();
            var to = $("#<%=txtToConsume.ClientID %>").val();

            var dateStrA = from.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");
            var dateStrB = to.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");

            // now you can compare them using:

            var fromDate = new Date(dateStrA);
            var toDate = new Date(dateStrB);

            if (fromDate > toDate) {
                alert('Enter valid Date For Search Data.');
                $("#<%=txtFromConsume.ClientID %>").val('');
                $("#<%=txtToConsume.ClientID %>").val('');
                return false;
            }
        }

        function btnAddNew() {
            $(document.getElementById('<%= ddlMaterialGroupConsume.ClientID %>')).val('');
            $(document.getElementById('<%= txtDate.ClientID %>')).val('');
            $(document.getElementById('<%= tab2.ClientID %>')).tabs();
            $(document.getElementById('<%= tab2.ClientID %>')).show();
            $(document.getElementById('<%= tabs1.ClientID %>')).hide();
            $(document.getElementById('<%= tabs.ClientID %>')).hide();
            $(document.getElementById('<%= gvConsumption.ClientID %>')).hide();
            $(document.getElementById('<%= gvMaterialConsume.ClientID %>')).hide();
            $("#divTransfer").hide();
            $("#divSearchConsume").hide();
            $("#divButtons").show();
            $("#divPurchase").hide();
            $("#divSearch").hide();
            $("#divConsumption").show();
            $("#divMaterialGroup").hide();
            $("#divTransferSearch").hide();
            $("#divSingleMaterialTransfer").hide();
            $("#divMultipleMaterialTransfer").hide();
            $("#divSchoolOrTrust").hide();
            $("#divBtnTransfer").hide();
            $("#btnAddNewMain").hide();
            $("#btnViewListMain").show();


        }
        function btnViewList() {
            // alert("view");
            $(document.getElementById('<%= ddlMaterialGroupConsume.ClientID %>')).val('');
            $(document.getElementById('<%= txtDate.ClientID %>')).val('');
            $(document.getElementById('<%= tab2.ClientID %>')).tabs();
            $(document.getElementById('<%= tab2.ClientID %>')).hide();
            $(document.getElementById('<%= tabs1.ClientID %>')).hide();
            $(document.getElementById('<%= tabs.ClientID %>')).hide();
            $(document.getElementById('<%= gvConsumption.ClientID %>')).show();
            $("#divTransfer").hide();
            $("#divSearch").hide();
            $("#divButtons").show();
            $("#divSearchConsume").show();
            $("#divMaterialGroup").hide();
            $("#divConsumption").show();
            $("#divTransferSearch").hide();
            $("#divSingleMaterialTransfer").hide();
            $("#divMultipleMaterialTransfer").hide();
            $("#divSchoolOrTrust").hide();
            $("#divBtnTransfer").hide();
            $("#btnAddNewMain").show();
            $("#btnViewListMain").hide();

        }

        $('#<%= ddlMaterialGroupConsume.ClientID %>').change(function () {

            $('#<%= lbMaterialConsume.ClientID %>').empty();

            var MaterialGroupId = $('#<%=ddlMaterialGroupConsume.ClientID %>').val();
            var MaterialGroupName = $('#<%=ddlMaterialGroupConsume.ClientID %> option:selected').text();

            if (MaterialGroupId != '') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Purchase.aspx/GetMaterial",
                    data: "{'MaterialGroupID':'" + MaterialGroupId + "','TrustID':'" + <%=Session[ApplicationSession.TRUSTID] %> + "','SchoolID':'" + <%=Session[ApplicationSession.SCHOOLID] %> + "'}",
                    dataType: "json",
                    success: function (data) {
                        var count = -1;
                        var temp = $.parseJSON(data.d);

                        $.each(temp, function (i) {
                            count = i;
                            var optionhtml = '<option value="' +
                                temp[i].MaterialID + '">' + temp[i].MaterialName + '</option>';
                            $(document.getElementById('<%= lbMaterialConsume.ClientID %>')).append(optionhtml);
                        });
                        if (count == "-1") {
                            alert("Material is not exist for " + MaterialGroupName);
                        }
                    },
                    error: function (error) {
                        alert(error);
                    }

                });
            } else {
                alert("Select at least one Material Group.");
            }
        });


        $('#<%= btnAddMaterialConsume.ClientID %>').click(function () {

            $('#<%= ddlMaterialGroupConsume.ClientID %>').addClass("validate[required]");
            $('#<%= txtDate.ClientID %>').addClass("validate[required]");
            $('#<%= lbMaterialConsume.ClientID %>').addClass("validate[required,maxList[1]]");
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == false) {
                $('#<%=hfValidate.ClientID %>').val("0");
            } else {
                $('#<%=hfValidate.ClientID %>').val("1");
            }
        });

        function ClearConsumption() {
            btnAddNew();
            $(document.getElementById('<%= lbMaterialConsume.ClientID %>')).empty();
            var optionhtml1 = '<option value="">' + "--Select--" + '</option>';
            $(document.getElementById('<%= lbMaterialConsume.ClientID %>')).append(optionhtml1);
        }

        $("#btnAddNewMain").click(function () {
            // alert("AddNewMain");
            $(document.getElementById('<%= hfMode.ClientID %>')).text = "Save";
            $("#btnAddNewMain").hide();
            $("#btnViewListMain").show();
            // alert($('input:radio[id*=rbtnPurchase]:checked').val());
            if ($('input:radio[id*=rbtnPurchase]:checked').val() == "rbtnPurchase") {
                // alert("Purchase");
                $("#divPurchase").show();
                $("#divPurchaseTab").show();
                $("#divSearch").hide();
                $("#divTransfer").hide();
                $("#divConsumption").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).show();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $("#divMultipleMaterialTransfer").hide();
                $("#divTotalAmount").hide();
                $(document.getElementById('<%= divGrid.ClientID %>')).hide();

            }
            else if ($('input:radio[id*=rbtnTransfer]:checked').val() == "rbtnTransfer") {
                $("#divPurchase").hide();
                $("#divTransfer").show();
                $("#divTransferSearch").hide();
                $("#tabs1").show();
                $("#divConsumption").hide();
                $("#divMaterialGroup").hide();
                $("#divSingleMaterialTransfer").hide();
                $("#divSchoolOrTrust").hide();
                $(document.getElementById('<%= tabs1.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs1.ClientID %>')).show();
                $("#divBtnTransfer").hide();
                $('input:radio[name=rblMaterialSelection]').attr('checked', false);
                var radiolist = $('#<%= rblMaterialSelection.ClientID %>').find('input:radio');
                 radiolist.removeAttr('checked');

            }
            else if ($('input:radio[id*=rbtnConsumption]:checked').val() == "rbtnConsumption") {
                // alert("ConsumeNew");
                btnAddNew();
            }
        });


    $("#btnViewListMain").click(function () {
        //  alert("btnViewList");
        $("#btnAddNewMain").show();
        $("#btnViewListMain").hide();
        if ($('input:radio[id*=rbtnPurchase]:checked').val() == "rbtnPurchase") {
            //  alert("Purchase");
            $("#divPurchase").show();
            $("#divSearch").show();
            // $("#divPurchaseTab").hide();
            $("#divTransfer").hide();
            $("#divConsumption").hide();
            $(document.getElementById('<%= tabs1.ClientID %>')).hide();
            $("#divMultipleMaterialTransfer").hide();
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).hide();
            $(document.getElementById('<%= tabs1.ClientID %>')).hide();
        }
        else if ($('input:radio[id*=rbtnTransfer]:checked').val() == "rbtnTransfer") {
            // alert("transfer");
            $("#divPurchase").hide();
            $("#divTransfer").show();
            $("#divTransferSearch").show();
            $("#tabs1").hide();
            $("#divConsumption").hide();
            $("#divMaterialGroup").hide();
            $("#divSingleMaterialTransfer").hide();
            $("#divSchoolOrTrust").hide();
            $(document.getElementById('<%= tabs1.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs1.ClientID %>')).hide();
                $("#divBtnTransfer").hide();

            }
            else if ($('input:radio[id*=rbtnConsumption]:checked').val() == "rbtnConsumption") {
                //  alert("ConsumeView");
                btnViewList();
            }
    });
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });

        $('.Attach').click(function () {
            $("#aspnetForm").validationEngine('attach');
        });
    </script>
</asp:Content>
