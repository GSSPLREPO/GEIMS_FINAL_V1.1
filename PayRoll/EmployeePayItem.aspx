<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EmployeePayItem.aspx.cs" Inherits="GEIMS.PayRoll.EmployeePayItem" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "EmployeePayItem.aspx/GetAllEmployeeNameForReport",
                        data: "{'prefixText':'" + request.term + "','TrustMID':'" + <%=Session[ApplicationSession.TRUSTID] %> + "','SchoolMID':'" + <%=Session[ApplicationSession.SCHOOLID] %> + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('~')[0],
                                    val: item.split('~')[1]
                                };
                            }));
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfEmployeeMID.ClientID %>").val(i.item.val);
                    $("#<%=hfEmployeeCodeName.ClientID %>").val(i.item.label);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Employee Pay-Item Template
        </div>
        <div id="divContent" runat="server" style="height: 100%; font-family: Verdana; padding-bottom: 10px;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left;">
                <div id="tabs" runat="server" class="style-tabs" visible="True" style="width: 100%; float: left;">
                    <div style="float: left; width: 100%;">
                        <ul>
                            <li><a id="tabTransferDetails" href="#tabs-2">Employee Pay-Item Template Details</a></li>
                        </ul>
                    </div>
                    <div id="tabs-1" class="gradientBoxesWithOuterShadows" style="float: left; width: 99%; padding: 0 0 0 10px">
                        <div id="divSearch" style="width: 100%; float: left; padding-bottom: 10px;">
                            <%--<fieldset>
                                <legend style="font-size: 13px;"><b>Employee Details</b></legend>--%>
                            <div style="width: 100%; float: left; padding: 15px;" class="label">
                                    <div style="float: left; width: 100%; text-align: center;">
                                    Employee Search :
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="Droptextarea" runat="server">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Employee Name</asp:ListItem>
                                            <asp:ListItem Value="2">Employee Code</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;
                                     <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] TextBox autosuggest" Width="200px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnGo" Text="Go" OnClick="btnGo_OnClick" CssClass="btn-blue-new btn-blue-medium Attach" />
                                    </div>
                                </div>
                            <div style="text-align: center; width: 100%; float: left" class="label">
                                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvEmployee_RowCommand">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Trust/School Name">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Department" HeaderText="Department">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                        CommandName="Edit1" CommandArgument='<%#Eval("EmployeeMID")+","+ Eval("SchoolMID")%>' Height="20px" Width="20px" />
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
                            <%--</fieldset>--%>
                        </div>
                        <div id="divPayItem" runat="server" style="width: 100%">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                    <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                    <strong>
                                        <asp:Label ID="lbl1" runat="server" Text="Trust Template"></asp:Label></strong>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <asp:GridView ID="gvPayItem" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="PayItemID" HeaderText="ID">
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="TrustTemplateID" HeaderText="Trust Temp ID">
                                                    <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>--%>
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
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>

                        </div>

                        <div id="divEmployeePayItem" runat="server" style="width: 100%">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <strong>
                                        <asp:Label ID="lblEmployeePayTemplate" runat="server" Text="Employee Pay Template"></asp:Label></strong>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <asp:GridView ID="gvSelectedPayItem" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvSelectedPayItem_RowCommand">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="PayItemMID" HeaderText="ID">
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UserTemplateID" HeaderText="Trust Temp ID">
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Pay Item Name">
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="35%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Type" HeaderText="PayItemType">
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DependsOn" HeaderText="Depends On ID">
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
                                        <asp:BoundField DataField="UserPayItemTemplateID" HeaderText="UserPayItemTemplateID">
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                    CommandName="Edit1" CommandArgument='<%#Eval("UserTemplateID")+","+ Eval("PayItemMID")+","+Eval("UserPayItemTemplateID")%>' Height="20px" Width="20px" />
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
                        <div id="divApplyTemplate" runat="server" style="width: 100%; float: left">
                            <%--<fieldset>
                                <legend>Apply Template</legend>--%>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text="Apply Template"></asp:Label></strong>
                                </div>
                            </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 20%;">
                                            CTC : (Annual)
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:TextBox ID="txtAnnual" runat="server" Width="160px" TabIndex="6" CssClass="TextBox" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 20%;">
                                            Monthly :
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:TextBox ID="txtMonthly" runat="server" Width="160px" TabIndex="6" CssClass="TextBox" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            <div style="width: 100%; float: left; padding-bottom: 10px;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 20%;">
                                            Gross (Monthly) :
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:TextBox ID="txtGross" runat="server" Width="160px" TabIndex="6" CssClass="TextBox" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 20%;">
                                            Template :
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:DropDownList ID="ddlTemplate" runat="server" Width="150px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="float: left; text-align: right; width: 100%;">
                                        <asp:Button runat="server" ID="btnApplyTemplate" Text="Apply Template" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnApplyTemplate_Click" />
                                    </div>
                            </div>
                            <%--</fieldset>--%>
                        </div>
                        <div id="divEditTemplate" runat="server" style="width: 100%; float: left">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Text="Edit Template"></asp:Label></strong>
                                </div>
                        </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 20%;">
                                            Pay Item Type :
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:DropDownList ID="ddlPayItemType" runat="server" Width="160px" OnSelectedIndexChanged="ddlPayItemType_SelectedIndexChanged"
                                                AutoPostBack="True" TabIndex="4" CssClass="TextBox">
                                                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                                <asp:ListItem Value="0">Independent</asp:ListItem>
                                                <asp:ListItem Value="1">Dependent</asp:ListItem>
                                                <asp:ListItem Value="2">Depends On Gross</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 20%;">
                                            Pay Item Name : 
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:TextBox ID="txtPayItemName" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 20%;">
                                            Depends On :
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:DropDownList ID="ddlDependsOn" runat="server" Width="160px"
                                                TabIndex="4" CssClass="TextBox">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 20%;">
                                            Percentage :
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            <asp:TextBox ID="txtPercentage" runat="server" Width="160px" TabIndex="6" CssClass="TextBox" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 20%;">
                                            Amount (Rs.) :
                                        </div>
                                        <div style="float: left; width: 80%;">
                                            <asp:TextBox ID="txtAmount" Style="text-align: right;" runat="server" Width="160px" CssClass="TextBox"
                                                TabIndex="7" onKeypress="return isNumberKey(evt);"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="float: left; text-align: right; width: 100%;">
                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            <%--</fieldset>--%>
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
        $(document.getElementById('<%= btnApplyTemplate.ClientID %>')).click(function () {
            $(document.getElementById('<%= txtAnnual.ClientID %>')).addClass("validatecustom[onlyNumberSp]] ");
            $(document.getElementById('<%= txtGross.ClientID %>')).addClass("validate[required,custom[onlyNumberSp]] ");
            $(document.getElementById('<%= txtMonthly.ClientID %>')).addClass("validate[custom[onlyNumberSp]] ");
            $(document.getElementById('<%= ddlTemplate.ClientID %>')).removeClass("validate[required]");
        });
        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            $(document.getElementById('<%= ddlPayItemType.ClientID %>')).addClass("validate[required] ");
            $(document.getElementById('<%= txtPayItemName.ClientID %>')).addClass("validate[required] ");
            $(document.getElementById('<%= ddlDependsOn.ClientID %>')).addClass("validate[required] ");
            $(document.getElementById('<%= txtPercentage.ClientID %>')).removeClass("validate[required,custom[onlyNumberSp]]");
            $(document.getElementById('<%= txtAmount.ClientID %>')).removeClass("validate[required,custom[onlyNumberSp]]");
        });
    </script>
</asp:Content>
