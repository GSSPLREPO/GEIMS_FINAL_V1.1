<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SchoolReports.aspx.cs" Inherits="GEIMS.Client.UI.SchoolReports" %>

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
            School Reports
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
                <div id="tabs" runat="server" class="style-tabs" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">School Reports</a></li>
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
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolEmployeeList.aspx">Employee List</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolEmpoyeeInformationReport.aspx">Employee Information</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/StudentList.aspx">Student List</a>
                                            </div>
                                        </div>
                                          <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/ExportStudentList.aspx">Export Student List</a>
                                            </div>
                                        </div>

                                     



                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                     
                                           <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/StudentAttendenceReport.aspx">Student Attendance</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolSarasariReport.aspx">Sarasari</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/EmployeeMonthlyAttendance.aspx">Employee Monthly Attendance</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/EmployeeWiseAttendance.aspx">Employee Wise Attendance</a>
                                            </div>
                                        </div>
                                      
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/TimeTableClassWise.aspx">Class Wise Time Table</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/TimeTableTeacherWise.aspx">Teacher Wise Time Table</a>
                                            </div>
                                        </div>
                                          <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/LeaveBalance.aspx">Employee Leave Balance</a>
                                            </div>
                                        </div>

                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../leave/LeaveApprovalReport.aspx">Employee Approved Leave Report</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divPayRoll" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>School PayRoll Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                           <%-- <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/SchoolEmployeePayrollReport.aspx">Process PayRoll</a>
                                            </div>--%>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/EmployeePayrollReport.aspx">Employee Payroll Report (Monthly)</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <%--<div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>--%>
                                          <%--  <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/SchoolYearlyEmployeeReport.aspx">Yearly Payroll Report</a>
                                            </div>--%>
                                            <%-- <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/YearlyEmployeeReport.aspx">Yearly Payroll Report</a>
                                            </div>--%>
                                        </div>

                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/SchoolPaySlip.aspx">School PaySlip</a>
                                            </div>
                                        </div>

                                         <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/MonthlyPFRegisterReport.aspx">Employee PF Register</a>
                                            </div>
                                        </div>

                                          <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/EmployeeMonthlyBankStatement.aspx">Employee Bank Statement</a>
                                            </div>
                                        </div>
                                          </div>
                                      <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/ProfessionalTaxReport.aspx">Employee Professional Tax</a>
                                            </div>
                                        </div>

                                         <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/MonthlyEDLIReport.aspx">E.D.L.I. Report</a>
                                            </div>
                                        </div>

                                          <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../PayRoll/SchoolSalarySummary.aspx">School Salary Summary</a>
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

                            <div id="divFees" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Fees Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <%--<div style="text-align: left; width: 25%; float: left;" class="label">
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
                                                <a style="text-decoration: none; color: black" href="../ReportUI/FeeCollectionClassWiseForSchool.aspx">Class Wise</a>
                                            </div>
                                        </div>--%>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/FeesTypeReport.aspx">Fee Type Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/YearWiseFeesCollectionReport.aspx">Year wise Fees Collection for Accounting Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/CompactReport.aspx">Compact Report</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/ClassWiseStudentFees.aspx">Class Wise Fees Collection</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/DateWiseFessReport.aspx">Date Wise Fees Collection</a>
                                            </div>
                                        </div>
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
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolVendorList.aspx">Vendor Wise Material</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolMaterialVandorWise.aspx">Material Payment List</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolMaterialStock.aspx">Material Stock</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolMaterialTransfer.aspx">Material Transfer</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolMaterialConsumption.aspx">Material Consumption</a>
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

                            <div id="divAccounting" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Accounting Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptCashbook.aspx">Cash/Bank book</a>
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
                                        <div style="text-align: left; width: 25%; float: left;">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Accounting/RptTrialBalance.aspx">Trial Balance</a>
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
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolEmployeeCategoryWise.aspx">Employee Category Wise</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/SchoolStudentCategoryWise.aspx">Student Category Wise</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/StudentDEoReport.aspx">Student Deo Report</a>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>

                            <div id="divTimeTable" runat="server" style="width: 100%;">
                                <fieldset>
                                    <legend>Time Table Reports</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/TimeTableClassWise.aspx">Class Wise</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/TeacherWiseTimeTable.aspx">Teacher Wise</a>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>

                            <div id="divDEOReports" runat="server" style="width: 100%">
                                <fieldset>
                                    <legend>DEO Reports
                                    </legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                    <%--    <div style="text-align: left; width: 25%; float: left;" class="label">
	                                        <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
											</div>
											<div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/DEO_Patrak_1.aspx">Patrak-1</a>
                                            </div>
										</div>--%>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/DEO_Patrak_2.aspx">Patrak-2</a>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>  
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../ReportUI/DEO_Patrak_3.aspx">Patrak-3</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                              <div id="divResultReports" runat="server" style="width: 100%">
                                <fieldset>
                                    <legend>Result Reports
                                    </legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
	                                        <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
											</div>
											<div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Result/ClassWiseResultReport.aspx">ClassWise Result Report</a>
                                            </div>
										</div>
                                        <div style="text-align: left; width: 25%; float: left;" class="label">
                                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                                <img src="../Images/checked.gif" />
                                            </div>
                                            <div style="text-align: left; width: 85%; float: left;" class="label">
                                                <a style="text-decoration: none; color: black" href="../Result/GradeWiseResultReport.aspx">ClassWise Grade Report</a>
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
    </div>
</asp:Content>
