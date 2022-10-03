<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="SchoolLeaveBalance.aspx.cs" Inherits="GEIMS.ReportUI.SchoolLeaveBalance" %>
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
                        url: "LeaveBalance.aspx/GetAllEmployeeNameForReport",
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Leave Balance
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
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
                                <legend>Search Employee</legend>
                                <div class="divclasswithfloat">
                                    <div style="width: 80%; float: left;">
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server" Enabled="false">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Employee Name</asp:ListItem>
                                            <asp:ListItem Value="2">Employee GR NO</asp:ListItem>
                                           <%-- <asp:ListItem Value="3">Employee Form No</asp:ListItem>
                                            <asp:ListItem Value="4">Employee Unique ID</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] textarea autosuggest"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfEmployeeName" />
                                        <asp:HiddenField runat="server" ID="hfEmployeeID" />
                                        &nbsp;&nbsp;&nbsp;
                                       
                                    </div>
                                    <div style="width: 100%; padding-top: 20px; text-align: right; margin-bottom: 0px;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnGo" Text="View" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_OnClick" />
                                </div>
                                </div>
                                <div class="clear"></div>
                                <div id="divgrid" runat="server">
                                 <asp:GridView ID="gvLeaveBalance" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                        Font-Names="verdana" Font-Size="12px" Width="30%" BackColor="White">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="LeaveName" HeaderText="Leave Name">
                                                <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Total" HeaderText="Leave Balance">
                                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
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
        //var totalAmount = 0, TotalDiscount = 0, Total = 0, TotalAmountnotCheck = 0;
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });
    </script>
</asp:Content>
