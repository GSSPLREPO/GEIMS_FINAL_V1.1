<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EmployeeWiseShiftReport.aspx.cs" Inherits="GEIMS.ReportUI.EmployeeWiseShiftReport" %>
<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        $(document).ready(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "EmployeeWiseShiftReport.aspx/GetAllEmployeeNameForReport",
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
                    $("#<%=hfSearchName.ClientID %>").val(i.item.val);

                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Employee Wise Shift Report 
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
            &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />
            &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                        OnClick="btnReport_Click" />
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                    <div id="tabs-1" style="min-height: 150px;">

                        <asp:Panel ID="pnlStudentAttendanceInfo" runat="server" GroupingText="Employee Shift">
                            <div style="width: 100%; float: left;" class="label">
                                
                            </div>
                            <div style="width: 100%; padding-bottom: 20px; float: left;" class="label">
                                <div style="float: left; width: 15%;">
                                    Employee Name :<span style="color: red">*</span>
                                </div>
                                <div style="width: 80%; float: left;">
                                    <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server">
                                        <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                        <asp:ListItem Value="1">Employee Name</asp:ListItem>
                                        <%--<asp:ListItem Value="2">Employee Code</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] textarea autosuggest"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hfSearchName" />
                                    &nbsp;&nbsp;&nbsp;
                                </div>

                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="float: left; width: 15%;">
                                    From Date :<span style="color: red">*</span>
                                </div>
                                <div style="float: left; width: 185px;">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 15%;">
                                    To Date :<span style="color: red">*</span>
                                </div>
                                <div style="float: left;">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                        <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div id="divButtons" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 30px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <asp:ImageButton ID="btnExportPDF" runat="server" ImageUrl="~/Images/adobe.PNG"
                                        ToolTip="Export to PDF" OnClick="btnExportPDF_Click" />

                                    &nbsp;
                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
                        ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />
                                    &nbsp;
                    <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="~/Images/word.PNG"
                        ToolTip="Export to Word" OnClick="btnExportWord_Click" />
                                </div>
                            </div>
                        </div>

                        <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">

                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 20px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        <b>Report : Employee Wise Shift Report</b>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 20px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        <b>School :</b>
                                        <asp:Label runat="server" ID="lblSchoolName"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <%-- <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                               
                                            </div>
                                        </div>
                                    </div>--%>

                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        <b>Employee Name :</b>
                                        <asp:Label runat="server" ID="lblEmployeeName"></asp:Label> <br/> <br/> 
                                        &nbsp; &nbsp; &nbsp; 
                                        <b>From date :</b>
                                        <asp:Label runat="server" ID="lblFromDate"></asp:Label>
                                        &nbsp; &nbsp; &nbsp;
                                                 <b>ToDate :</b>
                                        <asp:Label runat="server" ID="lblToDate"></asp:Label>
                                    </div>
                                    <%--<asp:Label ID="Label1" ForeColor="red" runat="server">Note *:  'A' Considered as <strong>Absent</strong></asp:Label>
                                    <br />
                                    <asp:Label ID="Label2" ForeColor="red" runat="server">Note *:  'P' Considered as <strong>Present</strong></asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" ForeColor="red" runat="server">Note *:  '+' Considered as <strong>Half Day</strong></asp:Label>
                                    <br />
                                    <asp:Label ID="Label4" ForeColor="red" runat="server">Note *:  '*' Considered as <strong>Holiday</strong></asp:Label>
                                    <br />
                                    <asp:Label ID="Label5" ForeColor="red" runat="server">Note *:  '-' Considered as <strong>Week End</strong></asp:Label>
                                    <br />
                                    <asp:Label ForeColor="red" runat="server">Note *:  'CL/ML/DL..etc.' Considered as <strong>Approved Leave</strong></asp:Label>
                                    <br/>
                                     <asp:Label ForeColor="red" runat="server">Note *:  '+(CL),+(ML),+(DL)..etc.' Considered as <strong>Approved Halfday Leave</strong></asp:Label>  <br/>--%>
                                </div>
                            </div>
                            <%--  <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                              
                                            </div>
                                        </div>
                                    </div>--%>

                            <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1100px">
                                <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="True"
                                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%">
                                    <RowStyle BackColor="White" />
                                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                        BorderWidth="1px" BorderStyle="Solid" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
            </div>
        </div>
        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
            <div style="padding: 10px; padding-right: 30px;">
                <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                </div>
            </div>
        </div>
        <div id="divSchool1" style="width: 100%; padding-top: 0px; display: none;">
            <div id="div1" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                    <div style="padding: 10px; padding-right: 20px;">
                        <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                            <b>Report : Employee Wise Shift Report</b>
                        </div>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                    <div style="padding: 10px; padding-right: 20px;">
                        <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                            <b>School :</b>
                            <asp:Label runat="server" ID="lblSchool1"></asp:Label>
                            &nbsp; &nbsp; &nbsp;
                                                <b>Class :</b>
                            <asp:Label runat="server" ID="lblClass1"></asp:Label>
                        </div>
                    </div>
                </div>



                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                    <div style="padding: 10px; padding-right: 30px;">
                        <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                            <b>Year :</b>
                            <asp:Label runat="server" ID="lblYear1"></asp:Label>
                            &nbsp; &nbsp; &nbsp;
                                                 <b>Month :</b>
                            <asp:Label runat="server" ID="lblMonth1"></asp:Label>
                        </div>
                    </div>
                </div>

                <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1100px">
                    <asp:GridView ID="gvAttendance" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="True"
                        CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%">
                        <RowStyle BackColor="White" />
                        <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                            BorderWidth="1px" BorderStyle="Solid" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportPDF" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
            <asp:PostBackTrigger ControlID="btnExportWord" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });

    </script>
    <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
