<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="LeaveTemplate.aspx.cs" Inherits="GEIMS.Leave.LeaveTemplate" %>

<%@ Import Namespace="GEIMS.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/jquery-1.8.3.js"></script>--%>
    <link href="../CSS/ajaxCalender.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $(document.getElementById('<%= txtSearchName.ClientID %>')).tabs();
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "LeaveTemplate.aspx/GetAllEmployeeNameForReport",
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
                    $("#<%=hfEmployeeID.ClientID %>").val(0)
                    $("#<%=hfEmployeeID.ClientID %>").val(i.item.val);
                    $("#<%=hfEmployeeName.ClientID %>").val(i.item.label);
                }
            });

            $('[id$=chkHeader]').click(function () {

                $("[id$=chkChild]").attr('checked', this.checked);
            });
            $("[id$=chkChild]").click(function () {
                if ($('[id$=chkChild]').length == $('[id$=chkChild]:checked').length) {
                    $('[id$=chkHeader]').attr("checked", "checked");
                }
                else {
                    $('[id$=chkHeader]').removeAttr("checked");
                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
         function numeric(e) {
             var unicode = e.charCode ? e.charCode : e.keyCode;
             if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57)) {
                 return true;
             }
             else {
                 alert('Please Enter Only Positive Value.');
                 return false;
             }
         }
     </script>    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Leave Template
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
             <asp:LinkButton CausesValidation="false" ID="btnClear" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnClear_Click">Clear</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <div id="tabs-1" style="padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                        <%--<ajax:ToolkitScriptManager ID="ScriptManager1" runat="server">
                        </ajax:ToolkitScriptManager>--%>
                        <div id="divStudentPanel" style="width: 100%">
                            <fieldset>
                                <legend>Search employee</legend>
                                <div class="divclasswithfloat">
                                    <div style="width: 80%; float: left;">
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" AutoPostBack="true"  Enabled="False">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Employee Name</asp:ListItem>
                                           <%-- <asp:ListItem Value="2">Employee Code</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <span style="color: red">*</span>
                                        &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] textarea autosuggest" OnTextChanged="txtSearchName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfEmployeeName" />
                                        <asp:HiddenField runat="server" ID="hfEmployeeID" />
                                        &nbsp;&nbsp;&nbsp;
                                    </div>
                                </div>
                                <div class="divclasswithfloat">
                                    <div style="width: 80%; float: left;">
                                        Academic Year: <span style="color: red">*</span>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="200px">
                                        </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div style="width: 20%; float: left; text-align: right">
                                        <asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new Attach" Width="50px" Text="Search" CausesValidation="false"
                                            OnClick="btnGo_Click" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvStudent_RowCommand">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="LeaveID" HeaderText="Leave ID">
											<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
											<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
										</asp:BoundField>
                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkChild" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeaveName" Text='<%#Eval("LeaveName") %> ' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Leaves.">
                                            <ItemTemplate>
                                               <%-- <asp:TextBox ID="txtTotalLeaves" runat="server" Width="100px" CssClass="txtTotalLeaves TextBox" onkeypress="return numeric(event)"  Text="0"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtTotalLeaves" runat="server" Width="100px" CssClass="txtTotalLeaves TextBox" placeholder="0" Style="text-align:right;"></asp:TextBox>
                                                <%--<asp:CompareValidator ID="intValidator" runat="server" ControlToValidate="txtTotalLeaves" Operator="DataTypeCheck" Type="Double" ErrorMessage="Value must be a integer or float" ForeColor="Red"></asp:CompareValidator>--%>
                                                <asp:RangeValidator runat="server" ID="RangeValidator1" Type="Double" ControlToValidate="txtTotalLeaves" MaximumValue='9999.99' MinimumValue="0" ErrorMessage="Value must be a Positive integer or float" Display="Dynamic" ForeColor="Red" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalLeaves" runat="server" CssClass="lblTotalLeaves" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                                <div style="width: 100%; padding-top: 20px; text-align: right; margin-bottom: 0px;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnSave_Click" />
                                </div>
                            </fieldset>
                        </div>

                    </div>
                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $('[id*=chkHeader]').click(function () {

            if ($(this).is(":checked")) {

                $('[id*=chkChild]').prop("checked", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkChild]').prop("checked", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });
        $("[id*=chkChild]").click(function () {
            if ($(this).is(":checked")) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            } else {
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                    $('[id*=chkHeader]').prop("checked", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                }
            }
            // alert("chkChild");
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                $('[id*=chkHeader]').prop("checked", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkHeader]').removeAttr("checked");
            }
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });


        //var totalAmount = 0, TotalDiscount = 0, Total = 0, TotalAmountnotCheck = 0;
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });
    </script>
</asp:Content>
