<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="PayItem.aspx.cs" Inherits="GEIMS.PayRoll.PayItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $('#tab-panel').tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Pay Item
            <asp:LinkButton CausesValidation="false" ID="lnkAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <script type="text/javascript">
                $(function () {
                    $('#id_search').quicksearch('table#<%=gvPayItem.ClientID%> tbody tr');
                })
            </script>
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                    <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                        <asp:GridView ID="gvPayItem" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvPayItem_RowCommand"
                            OnPreRender="gvPayItem_OnPreRender">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="PayItemMID" HeaderText="ID">
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="PayItem Name">
                                    <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description">
                                    <HeaderStyle Width="50%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Type" HeaderText="Earning/Deduction">
                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <%--<asp:CommandField ShowSelectButton="True" SelectText="Edit" />--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                            CommandName="Edit1" CommandArgument='<%# Eval("PayItemMID")%>' Height="20px" Width="20px" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                            CommandName="Delete1" CommandArgument='<%# Eval("PayItemMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Pay Item Details</a></li>
                        </ul>
                        <div id="tabs-1" style="padding:9px" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;">
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Pay Item :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 80%; float: left;">
                                        <asp:TextBox ID="txtPayItemName" runat="server" Width="300px" CssClass="validate[required] TextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Description :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:TextBox ID="txtPayItemDescription" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Pay Type :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:RadioButtonList ID="rdEraningDeductionList" runat="server" RepeatDirection="Horizontal"
                                            Width="200px" Height="17px" TabIndex="3" RepeatLayout="Flow">
                                            <asp:ListItem Value="0" Selected="True">Earnings</asp:ListItem>
                                            <asp:ListItem Value="1">Deduction</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div id="divDeductionVIA" style="width: 100%" class="divclasswithfloat">
                                    <fieldset>
                                        <legend>Deduction under Chapter VI A</legend>
                                        <%--<div style="width: 100%;">--%>
                                        <%--<asp:Panel runat="server" ID="pnlDivision">--%>
                                        <div style="width: 100%" class="divclasswithfloat">
                                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                                Deduction Type :<span style="color: red">*</span>
                                            </div>
                                          <%--  <div style="text-align: left; float: left; width: 100%;">
                                                <asp:RadioButtonList ID="rbtnDeduction" runat="server"
                                                    Width="800px" Height="17px" TabIndex="3" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                                                    <asp:ListItem Value="1" Selected="True">80C Deduction</asp:ListItem>
                                                    <asp:ListItem Value="2">80CCC Deduction</asp:ListItem>
                                                    <asp:ListItem Value="3">80CCD Deduction</asp:ListItem>
                                                    <asp:ListItem Value="4">Professional Tax</asp:ListItem>
                                                    <asp:ListItem Value="5">Tax Deducted at Source (TDS)</asp:ListItem>
                                                    <asp:ListItem Value="6">ESIC</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>--%>
                                            <div style="text-align: left; float: left; width: 100%;">
                                                <asp:DropDownList ID="ddlDeduction" runat="server" CssClass="validate[required] TextBox"  style="margin-top: 5px;">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">80C Deduction</asp:ListItem>
                                                    <asp:ListItem Value="2">80CCC Deduction</asp:ListItem>
                                                    <asp:ListItem Value="3">80CCD Deduction</asp:ListItem>
                                                    <asp:ListItem Value="4">Professional Tax</asp:ListItem>
                                                    <asp:ListItem Value="5">Tax Deducted at Source (TDS)</asp:ListItem>
                                                    <asp:ListItem Value="6">ESIC</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="clear"></div>

                                    </fieldset>
                                </div>
                                <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right;"></div>
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

        $("table[id*=rdEraningDeductionList]").validationEngine('attach', { promptPosition: "bottomRight" });
        $("table[id*=rdEraningDeductionList] input").addClass("validate[required]");
        $("table[id*=rbtnDeduction]").validationEngine('attach', { promptPosition: "bottomRight" });
        $("table[id*=rbtnDeduction] input").addClass("validate[required]");
        $("[id*=btnSave]").bind("click", function () {
            if (!$("table[id*=rdEraningDeductionList]").validationEngine('validate')) {
                return false;
            }
            if (!$("table[id*=rbtnDeduction]").validationEngine('validate')) {
                return false;
            }
            return true;
        });
        $('#<%=rdEraningDeductionList.ClientID%>').find('input[type=radio]').click(function () {

            if ($(this).is(':checked')) {
                var selectedValue = $(this).val();
                if (selectedValue == "0") {
                    $("#divDeductionVIA").hide();
                }
                else {
                    $("#divDeductionVIA").show();
                }
            }
        });
        function divHide() {
            $("#divDeductionVIA").hide();
        }
        function divShow() {
            $("#divDeductionVIA").show();
        }      
    </script>
</asp:Content>
