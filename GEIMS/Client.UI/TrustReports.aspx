<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="TrustReports.aspx.cs" Inherits="GEIMS.Client.UI.TrustReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            <%--General Reports--%>
            Reports
            <%--<asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_Click">Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <%--<li><a id="tabClassDetails" href="#tabs-1">General Reports</a></li>--%>
                        <li><a id="tabClassDetails" href="#tabs-1">Reports</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%;">
                            <div id="divGeneralReports" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>General Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolList.aspx">School List</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolInformation.aspx">School Information</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/EmployeeList.aspx">Employee List</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/EmpoyeeInformationReport.aspx">Employee Information</a>
                                            </div>
                                        </div>
                                      <br/>
                                        </div>
                                     <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/EmployeeWiseShiftReport.aspx">Employee Wise Shift</a>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                         <!-- Start Next Line Set Menu -->
                                        <div style="width: 100%; display:block;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 25%; float: left;">
                                                <div style="text-align: left; width: 15%; float: left;" class="label">
                                                    <img src="../Images/checked.gif" />
                                                </div>
                                                <div style="text-align: left; width: 85%; float: left;" class="label">
                                                    <a style="text-decoration: none; color: black" href="../ReportUI/SchoolLeaveBalance.aspx">Employee Leave Balance</a>
                                                </div>
                                            </div>
                                            <div style="text-align: left; width: 25%; float: left;">
                                                <div style="text-align: left; width: 15%; float: left;" class="label">
                                                    <img src="../Images/checked.gif" />
                                                </div>
                                                <div style="text-align: left; width: 85%; float: left;" class="label">
                                                    <a style="text-decoration: none; color: black" href="../ReportUI/TrustEmployeeMonthlyAttendance.aspx">Employee Monthly Attendance</a>
                                                </div>
                                            </div>
                                            <div style="text-align: left; width: 25%; float: left;">
                                                <div style="text-align: left; width: 15%; float: left;" class="label">
                                                    <img src="../Images/checked.gif" />
                                                </div>
                                                <div style="text-align: left; width: 85%; float: left;" class="label">
                                                    <a style="text-decoration: none; color: black" href="../ReportUI/TrustEmployeeWiseAttendance.aspx">Employee Wise Attendance</a>
                                                </div>
                                            </div>
                                             <div style="text-align: left; width: 25%; float: left;">
                                                <div style="text-align: left; width: 15%; float: left;" class="label">
                                                    <img src="../Images/checked.gif" />
                                                </div>
                                                <div style="text-align: left; width: 85%; float: left;" class="label">
                                                    <a style="text-decoration: none; color: black" href="../ReportUI/ApprovedLeaveReport.aspx">Approved Leave Report</a>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- End Next Line Set Menu -->
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divFeesCollection" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Fees Collection Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/FeeCollectionSchoolWise.aspx">School Wise</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/FeeCollectionClassWise.aspx">Class Wise</a>
                                            </div>
                                        </div>
                                        <%--<div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/FeeCollectionStudentWise.aspx">Student Wise</a>
                                            </div>
                                        </div>--%>
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divInventory" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Invetory Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/VendorList.aspx">Vendor Wise Material</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/MaterialVandorWise.aspx">Material Payment List</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/MaterialStock.aspx">Material Stock</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/MaterialTransferReport.aspx">Material Transfer</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/MaterialConsumption.aspx">Material Consumption</a>
                                                <%--<a style="text-decoration:none;color:black" href="../ReportUI/VendorList.aspx">Vandor Wise Material</a>--%>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <%-- <img src="../Images/checked.gif" />--%>
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <%--<a style="text-decoration:none;color:black" href="../ReportUI/MaterialVandorWise.aspx">Material Payment List</a>--%>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <%--<img src="../Images/checked.gif" />--%>
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <%--<a style="text-decoration:none;color:black" href="../ReportUI/MaterialStock.aspx">Material Stock</a>--%>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <%--<img src="../Images/checked.gif" />--%>
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <%--<a style="text-decoration:none;color:black" href="../ReportUI/MaterialTransferReport.aspx">Material Transfer</a>--%>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divPayRoll" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>PayRoll Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                       <%-- <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/EmployeePayrollReport.aspx">Process PayRoll</a>
                                            </div>
                                        </div>--%>
                                      <%--  <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/YearlyEmployeeReport.aspx">Yearly Payroll Report</a>
                                            </div>
                                        </div>--%>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/PaySlip.aspx">PaySlip</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <%--<img src="../Images/checked.gif" />--%>
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <%--<a style="text-decoration:none;color:black" href="../ReportUI/MaterialTransferReport.aspx">Material Transfer</a>--%>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>
                            <div id="divAccounting" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Accounting Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptCashbook.aspx?mode=TU">Cash/Bank book</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptJournalRegister.aspx">Journal Register</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptGeneralLedger.aspx">General Ledger</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptDayBook.aspx">Day Book</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptTrialBalance.aspx">Trial Balance</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptPnL.aspx">Income & Expenditure Account</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptBalanceSheet.aspx">Balance Sheet</a>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>
                            <div id="divStatutory" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Statutory Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">

                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/EmployeeCategoryWise.aspx">Employee Category Wise</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolAcoountingInfoReport.aspx">Student Accounting Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/StudentCategoryWise.aspx">Student Category Wise</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SarasariReport.aspx">Sarasari Report</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divMeeting" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Meeting Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/ScheduleMeetingReport.aspx">Schedule Meeting</a>
                                            </div>
                                        </div> 
                                         <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/PendingPointsReport.aspx">Pending Points</a>
                                            </div>
                                        </div> 
                                    </div>           
                                </fieldset>
                            </div>
                             <div id="divEvent" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Event Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/DetailsEventReport.aspx">Details of Event Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/ListofEventReport.aspx">List of Event Report</a>
                                            </div>
                                        </div> 
                                    </div>
                                </fieldset>
                            </div>
                             <div id="divLibrary" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Library Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">

                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/ListofBoosReport.aspx">List of Books Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/MemberwiseListofBookReportIssue.aspx">Member Wise Book Issue Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/MemberwiseListofBookReturn.aspx">Member Wise Book Return Report</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/PaneltyReport.aspx">Penalty Report</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 14px;"></div>
            </div>
        </div>
</asp:Content>

