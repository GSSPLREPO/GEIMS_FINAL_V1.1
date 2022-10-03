<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="GeneratePaySlip.aspx.cs" Inherits="GEIMS.PayRoll.GeneratePaySlip" %>

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
                        url: "GeneratePayslip.aspx/GetAllEmployeeNameForReport",
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
            Generate Payslip
            &nbsp;<asp:LinkButton CausesValidation="false" ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnBack_Click">Cancel</asp:LinkButton>
        </div>
        <div id="divContent" runat="server" style="height: 100%; font-family: Verdana; padding-bottom: 10px;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left;">
                <div id="tabs" runat="server" class="gradientBoxesWithOuterShadows" visible="True" style="width: 100%; float: left;">
                    <div style="float: left; width: 100%;">
                        <ul>
                            <li><a id="tabTransferDetails" href="#tabs-2">Generate PaySlip</a></li>
                        </ul>
                    </div>
                    <div id="tabs-1" class="" style="float: left; width: 99%; padding: 0 0 0 10px">
                        <div id="divSearch" style="width: 100%; float: left; padding-bottom: 10px;">
                            <%--<fieldset>
                                <legend style="font-size: 13px;"><b>Employee Details</b></legend>--%>
                            <div style="width: 100%; float: left;" class="label">
                                <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                <div style="float: left; width: 100%; text-align: center; padding-top: 10px;">
                                    Employee Search :
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="Droptextarea" runat="server" Enabled="False">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Employee Name</asp:ListItem>
                                            <%--<asp:ListItem Value="2">Employee Code</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                     <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] TextBox autosuggest" Width="200px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnGo" Text="Go" OnClick="btnGo_OnClick" CssClass="btn-blue-new btn-blue-medium Attach" Style="Top:-2px;" />
                                </div>
                            </div>
                            <div style="text-align: center; width: 100%; float: left" class="label">
                                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvEmployee_RowCommand">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
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
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="divForm" runat="server" style="width: 100%; float: left;" class="label">
                                <div id="divEmployeeTemplate" runat="server" style="width: 100%; float: left">
                                    <%--<fieldset>
                                        <legend>Employee Template</legend>--%>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Employee Template"></asp:Label></strong>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Month :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="TextBox" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hftxtPayTotalDays" runat="server" />
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                Year :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="TextBox" AutoPostBack="false" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Employee Code :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayEmpCode" runat="server" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                Name :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtEmpName" runat="server" Width="220px" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Designation :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayDesignation" runat="server" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                Department :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayDepartment" runat="server" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <%--</fieldset>--%>
                                </div>
                                <div id="divAttendance" runat="server" style="width: 100%; float: left">
                                    <%--<fieldset>
                                        <legend>Attendance Template</legend>--%>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                            <strong>
                                                <asp:Label ID="lblEmployeePayTemplate" runat="server" Text="Attendance Template"></asp:Label></strong>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                <strong>New Employee? / Adjustment Days</strong>
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:RadioButtonList ID="rblnNewEmp" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rblnNewEmp_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div style="float: left; width: 20%;">
                                                <asp:Button ID="btnChange" runat="server" Text="Change" Width="50px" Font-Size="X-Small" CssClass="btn-blue-new btn-blue-medium"
                                                    OnClick="btnChange_Click" Visible="False" style="left: -44px; top: -3px; width: 86px" />
                                                <asp:Label ID="Label12" runat="server" Text="Earned Days :"></asp:Label>
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayEarnedDays" CssClass="TextBox" runat="server" Enabled="False" Style="text-align:right;">0</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Total Days :
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="txtPayTotalDays" runat="server" CssClass="TextBox"
                                                    Enabled="False" Style="text-align:right;">0</asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Total Gross Rs. :
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="txtGoossRS" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="float: left; width: 50%;">
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <strong>Earnings</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <asp:GridView ID="gvEarnings" runat="server" AutoGenerateColumns="False" Width="350px"
                                                            BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" GridLines="Both"
                                                            Font-Names="verdana" Font-Size="12px" BackColor="White">
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                            <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PayItemMID" HeaderText="ID">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                 <%-- Note :  Change for TemplateField replace to BoundField get value Name : Arpit Shah, Date : 22-12-2021 --%>
                                                                <%--<asp:BoundField DataField="Name" HeaderText="Pay Item" />--%>
                                                                 <asp:TemplateField HeaderText="Pay Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text="" Style="text-align: right; border:none; color:black;" CssClass="TextBox"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="textAmount" AutoPostBack="true" OnTextChanged="ChangePF" runat="server" CssClass="TextBox"
                                                                            Style="text-align: right" onkeypress="return numeric(event)"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Percentage" HeaderText="Percantage">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckbAmount" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div style="float: right; width: 50%;">
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <strong>Deduction</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <asp:GridView ID="gvDeduction" runat="server" AutoGenerateColumns="False" Width="350px"
                                                            BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px"
                                                            CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                            Font-Names="verdana" Font-Size="12px" BackColor="White">
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                            <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PayItemMID" HeaderText="ID">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                <%-- Note :  Change for TemplateField replace to BoundField get value Name : Arpit Shah, Date : 22-12-2021 --%>
                                                                <%--<asp:BoundField DataField="Name" HeaderText="Pay Item">
                                                                    <ItemStyle Width="150px" />
                                                                </asp:BoundField>--%>
                                                                <asp:TemplateField HeaderText="Pay Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text="" Style="text-align: right; border:none; color:black;" CssClass="TextBox"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="textAmount" runat="server" Style="text-align: right" CssClass="TextBox" onkeypress="return numeric(event)"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckbAmount" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="float: left; width: 50%;">
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <strong>Earnings</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Basic Total :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtBasicTotal" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Total Earning Rs. :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayTotalEarnings" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Net Salary  Rs. :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayNetSalary" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Net Salary  Rs.(Round Of) :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayNetSalaryRoundOf" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Absent Days :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayAbsenceDay" runat="server" AutoPostBack="True" OnTextChanged="txtPayAbsenceDay_TextChanged" CssClass="TextBox" Style="text-align:right;" Enabled="False">0</asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div style="float: right; width: 50%;">
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <strong>Deduction</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Total Deduction Rs. :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayTotalDeduction" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Loan RS. :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtRentPaid" runat="server" Style="text-align: right" Enabled="False" CssClass="TextBox">0</asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Pension (8.3333%):
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPension" Style="text-align: right" runat="server" CssClass="TextBox" Enabled="False">0</asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Gov.PF (3.6667%):
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtGpf" Style="text-align: right" runat="server" CssClass="TextBox" Enabled="False">0</asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding: 10px;">
                                        <div style="float: right; width: 100%;">
                                            <asp:GridView ID="GDBalance" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" CellPadding="4" ForeColor="Black" GridLines="Both"
                                                Font-Names="verdana" Font-Size="12px" HorizontalAlign="Center"
                                                BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px">
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                                                <Columns>
                                                    <asp:BoundField DataField="LeaveID" HeaderText="LeaveID">
                                                        <HeaderStyle CssClass="hidden" />
                                                        <ItemStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsDeduction" HeaderText="Deduction">
                                                        <HeaderStyle CssClass="hidden" />
                                                        <ItemStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" />
                                                    <asp:BoundField DataField="Total" HeaderText="Opening" />
                                                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Availed">
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="textDays" AutoPostBack="true" OnTextChanged="ChangeDays" runat="server" CssClass="TextBox"
                                                                Style="text-align: center" onkeypress="return numeric(event)" Text="0"></asp:TextBox>--%>
                                                            <asp:TextBox ID="textDays" AutoPostBack="true" OnTextChanged="ChangeDays" runat="server" CssClass="TextBox"
                                                                Style="text-align: center" Text="0"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Balance" />
                                                </Columns>
                                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 100%; text-align: center">
                                                &nbsp;<asp:Label ID="lbltotal" runat="server">Total</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtTotal" runat="server" Height="19px" Enabled="true" CssClass="TextBox">0</asp:TextBox>
                                                <asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Red" Text="Out of Attendence Index" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Visible="False" CssClass="btn-blue-new btn-blue-medium" />
                                                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" Enabled="False" CssClass="btn-blue-new btn-blue-medium" />
                                                <asp:Button ID="btnRedo" runat="server" Text="Redo" Enabled="False" OnClick="btnRedo_Click" CssClass="btn-blue-new btn-blue-medium" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div style="width: 100%; float: left; text-align: right" class="label">
                                    <div style="">
                                        <div style="float: left; width: 100%;">
                                            <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" OnClick="btnSendForApproval_Click" Enabled="False" CssClass="btn-blue-new btn-blue-medium" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rblnNewEmp" EventName="SelectedIndexChanged" />
                            <%-- <asp:AsyncPostBackTrigger ControlID="ChangePF" EventName="TextChanged" />--%>
                            <asp:AsyncPostBackTrigger ControlID="txtPayAbsenceDay" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtRentPaid" EventName="TextChanged" />
                            <%-- <asp:AsyncPostBackTrigger ControlID="ChangeDays" EventName="TextChanged" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btnChange" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnCalculate" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnRedo" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
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
    </script>
</asp:Content>
