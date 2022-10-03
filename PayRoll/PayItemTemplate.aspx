<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="PayItemTemplate.aspx.cs" Inherits="GEIMS.PayRoll.PayItemTemplate" %>

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
            Pay Item Template
            <asp:LinkButton CausesValidation="false" ID="lnkAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkAddNewTemplate" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewTemplate_Click">Add Template</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <%-- <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
                <ContentTemplate>--%>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                </div>
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Pay Item Template Details</a></li>
                        </ul>
                        <div id="tabs-1" style="padding: 5px" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;">
                                <div id="divTemplateName" style="width: 100%" class="divclasswithfloat">
                                    <%--<div style="width: 100%; float: left;" class="label">
                                        <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Text="Add Template"></asp:Label></strong>
                                        </div>
                                    </div>--%>
                                    <div style="width: 100%" class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Template Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 80%; float: left;">
                                            <asp:TextBox ID="txtTemplateName" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>

                                            &nbsp;&nbsp;&nbsp<asp:Button runat="server" ID="btnAddTemplate" Text="Add Template" CssClass="btn-blue-new" OnClick="btnAddTemplate_OnClick" />
                                        </div>
                                    </div>
                                    <div style="width: 100%" class="divclasswithfloat" align="center">
                                        <asp:GridView ID="gvTemplates" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" Width="60%" BackColor="White" OnRowCommand="gvTemplates_RowCommand">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="TrustTemplateID" HeaderText="ID">
                                                    <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TrustTemplateName" HeaderText="Template Name">
                                                    <HeaderStyle Width="100%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="100%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                            CommandName="Edit1" CommandArgument='<%# Eval("TrustTemplateID")%>' Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                                            CommandName="Delete1" CommandArgument='<%# Eval("TrustTemplateID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                            Height="20px" Width="20px" />
                                                        
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                </asp:TemplateField>
                                                <%-- <asp:CommandField SelectText="Edit" ShowSelectButton="True" />--%>
                                            </Columns>
                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div style="width: 100%; float: left" class="divclasswithoutfloat">
                                    <asp:GridView ID="gvPayTemplate" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvPayTemplate_RowCommand">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="PayItemID" HeaderText="ID">
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Pay Item Name">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="35%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PayItemType" HeaderText="PayItemType">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PayItemDependsOn" HeaderText="Depends On ID">
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PayItemDependsOnName" HeaderText="PayemItDependsOn Name">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="35%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Percentage" HeaderText="Percentage">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" HeaderText="Amount">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TrustTemplateID" HeaderText="Trust Temp ID">
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                        CommandName="Edit1" CommandArgument='<%# Eval("TrustTemplateID")%>' Height="20px" Width="20px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                                        CommandName="Delete1" CommandArgument='<%# Eval("TrustTemplateID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                        Height="20px" Width="20px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" Width="10%" />
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField SelectText="Edit" ShowSelectButton="True" />--%>
                                        </Columns>
                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                                <div id="divEditTemplate" style="width: 100%" class="divclasswithfloat">
                                    <%--<fieldset>
                                        <legend>Edit Template</legend>--%>
                                    <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Select Template :
                                        </div>
                                        <div style="text-align: left; width: 80%; float: left;">
                                            <asp:DropDownList ID="ddlSelectTemplateName" runat="server" Width="160px" AutoPostBack="True" CssClass="TextBox"
                                                OnSelectedIndexChanged="ddlSelectTemplateName_SelectedIndexChanged" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Pay Item Type :
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:DropDownList ID="ddlPayItemType" runat="server" Width="160px" OnSelectedIndexChanged="ddlPayItemType_SelectedIndexChanged"
                                                AutoPostBack="True" TabIndex="4" CssClass="TextBox">
                                                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                                <asp:ListItem Value="0">Independent</asp:ListItem>
                                                <asp:ListItem Value="1">Dependent</asp:ListItem>
                                                <asp:ListItem Value="2">Depends On Gross</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Pay Item Name : 
                                        </div>
                                        <div style="text-align: left; width: 30%; float: right; vertical-align: top;">
                                            <asp:DropDownList ID="ddlPayItemName" runat="server" Width="160px" TabIndex="3" CssClass="TextBox" OnSelectedIndexChanged="ddlPayItemName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="height: 70px; margin-top: 10px; float: left; width: 100%;">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Depends On :
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:ListBox ID="lbDependsOn" runat="server" SelectionMode="Multiple" Width="160px" Height="70px"></asp:ListBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Percentage :
                                        </div>
                                        <div style="text-align: left; width: 30%; float: right; vertical-align: top;">
                                            <asp:TextBox ID="txtPercentage" runat="server" Width="160px" TabIndex="6" CssClass="validate[required] TextBox autosuggest" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                        <asp:Label ID="lblDependent" runat="server" Text="" ForeColor="red" Font-Size="Small"></asp:Label>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Amount (Rs.) :
                                        </div>
                                        <div style="text-align: left; width: 80%; float: left;">
                                            <asp:TextBox ID="txtAmount" Style="text-align: right;" runat="server" Width="160px" CssClass="validate[custom[onlyNumberSp]] TextBox"
                                                TabIndex="7" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--</fieldset>--%>
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
            <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSelectTemplateName" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlPayItemName" EventName="SelectedIndexChanged" />
                      <asp:AsyncPostBackTrigger ControlID="ddlPayItemType" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
    </div>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        function divHide() {
            $("#divTemplateName").hide();
            $("#divEditTemplate").show();
        }
        function divShow() {
            $("#divTemplateName").show();
            $("#divEditTemplate").hide();
        }
        $(document.getElementById('<%= btnAddTemplate.ClientID %>')).click(function () {
            $(document.getElementById('<%= txtTemplateName.ClientID %>')).addClass("validate[required] ");
        });
        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            $(document.getElementById('<%= ddlPayItemType.ClientID %>')).addClass("validate[required] ");
            $(document.getElementById('<%= ddlPayItemName.ClientID %>')).addClass("validate[required] ");

        });
    </script>
</asp:Content>
