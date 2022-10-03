<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="PaySlip.aspx.cs" Inherits="GEIMS.PayRoll.PaySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--  <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Payslip_Monthly
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />&nbsp;
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

                            <div id="tabs-1" style="min-height: 150px;padding:5px">

                                <asp:Panel ID="pnlEmployeePayrollInfo" runat="server" GroupingText="Payroll Details">

                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Employee Name :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="validate[required] TextBox autosuggest" Width="50%" Height="100%"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfSchoolMID" />
                                                <asp:HiddenField runat="server" ID="hfTrustMID" />
                                                <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                                <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: Left; width: 15%;">
                                                Month :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: Left; width: 25%;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="validate[required] Droptextarea" Width="100px">
                                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                            <div style="float: left; width: 8%;">
                                                Year :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 32%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="100px">
                                                </asp:DropDownList>
                                            </div>

                                            <div style="float: left; text-align: right; width: 20%;">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium " OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 20px">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 10px; padding-right: 30px; width: 100%">
                                        ----
                                        <asp:DataList ID="dlPaySlip" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlPaySlip_ItemDataBound" OnSelectedIndexChanged="dlPaySlip_SelectedIndexChanged" >
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                                    <!-- CHNAGE -->
                                                    <tr>
                                                        <td style="width: 20%; border: 1px solid black" align="center">
                                                            <img src="../Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" width="100" height="100" />
                                                        </td>
                                                        <td style="width: 40%; border: 1px solid black; font-size: 12px;padding-left: 5px" colspan="3">
                                                                <b>
                                                                    <h3 style="text-align:center;">FERTILIZERNAGAR SCHOOL</h3><br />
                                                                    <h3 style="text-align:center;">P O FERTILIZERNAGAR - 391750 VADODARA</h3>
                                                                </b>
                                                        </td>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px">
                                                            
                                                            <strong>For the Month:<br />
                                                              PR DAYS:<br />
                                                              PAY DAYS:</strong>
                                                        </td>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px">
                                                             <%--<strong>OC/EC</strong><br />
                                                            <br />
                                                            <br />--%>
                                                            <asp:Label runat="server" ID="lblMonth"></asp:Label><br />
                                                            <%# Eval("TotalDaysofMonth") %><br />
                                                            <%# Eval("EarnedDaysofMonth") %>
                                                        </td>
                                                    </tr>
                                                    <!-- CHNAGE ROW 1 -->
                                                    <tr>
                                                        <td colspan="1" style="border: 1px solid black; padding-left: 5px"><strong>Emp NO</strong></td>
                                                        <td colspan="2" style="border: 1px solid black; width: 30%; padding-left: 5px"><strong>Name</strong></td>
                                                        <%--<td style="border: 1px solid black; width: 20%; padding-left: 5px"><strong>Location</strong></td>--%>
                                                        <td colspan="2" style="border: 1px solid black; width: 10%; padding-left: 5px"><strong>Designation</strong></td>
                                                        <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>SECTION</strong></td>
                                                       <%-- <td style="border: 1px solid black; width: 11%; padding-left: 5px"><strong>Join Date</strong></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: 1px solid black; padding-left: 5px"><%# Eval("EmployeeCode") %></td>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px"><%# Eval("EmployeeName") %></td>
                                                        <%--<td style="border: 1px solid black; padding-left: 5px"><%# Eval("Location") %></td>--%>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px"><%# Eval("DesignationName") %></td>
                                                        <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DepartmentName") %></td>
                                                        <%--<td style="border: 1px solid black; padding-left: 5px"><%# Eval("DeptJoin") %></td>--%>
                                                    </tr>
                                                    <!-- CHNAGE ROW 2 -->
                                                    <tr>
                                                        <td colspan="1" style="border: 1px solid black; padding-left: 5px"><strong>GRADE</strong></td>
                                                        <td colspan="2" style="border: 1px solid black; width: 30%; padding-left: 5px"><strong>PF A/C NO.</strong></td>                 
                                                        <td colspan="2" style="border: 1px solid black; width: 10%; padding-left: 5px"><strong>BANK A/C NO.</strong></td>
                                                        <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>PAN NO.</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: 1px solid black; padding-left: 5px"><%--<%# Eval("Grade") %>--%>NA</td>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px"><%# Eval("PFNo") %></td>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px"><%# Eval("AccountNo") %></td>
                                                        <td style="border: 1px solid black; padding-left: 5px"><%# Eval("PANNo") %></td>
                                                    </tr>
                                                     <!-- CHNAGE ROW 3 -->
                                                    <tr>
                                                        <td colspan="1" style="border: 1px solid black; padding-left: 5px"><strong>JOINING DT</strong></td>
                                                        <td colspan="2" style="border: 1px solid black; width: 30%; padding-left: 5px"><strong>RETIRED DT</strong></td>                 
                                                        <td colspan="2" style="border: 1px solid black; width: 10%; padding-left: 5px"><strong>RESIGNED DT</strong></td>
                                                        <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>TAN NO.</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DeptJoin") %></td>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px"><%# Eval("RetirementDate") %></td>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px"><%# Eval("ResignedDate") %></td>
                                                        <td style="border: 1px solid black; padding-left: 5px"><%# Eval("TANNO") %></td>
                                                    </tr>

                                                     <!-- CHNAGE ROW 4 --><!-- Leave -->
                                                     <tr>
                                                        <td colspan="6" style="border: 1px solid black; background-color:lightgrey;" align="center"><strong>Leaves</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <%--   <td colspan="1" style="border: 1px solid black"></td>--%>
                                                        <td align="center" colspan="6" style="border: 1px solid black; padding-left: 0px">
                                                            <asp:GridView ID="gvLeave" runat="server" Width="100%">
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <!-- CHNAGE ROW 5 -->
                                                    <tr>
                                                        <td colspan="1" style="border: 1px solid black; padding-left: 5px"><strong>EXTRA HR WORK</strong></td>
                                                        <td colspan="2" style="border: 1px solid black; padding-left: 5px">NA</td>
                                                        <%--<td colspan="2" style="border: 1px solid black; width: 30%; padding-left: 5px"><strong>&nbsp;</strong></td> --%>                
                                                        <td colspan="2" style="border: 1px solid black; width: 10%; padding-left: 5px"><strong>&nbsp;</strong></td>
                                                        <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>&nbsp;</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="border: 1px solid black">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <%--<td colspan="1" style="border: 1px solid black"></td>--%>
                                                        <td colspan="3" style="border: 1px solid black; padding-left: 5px; text-align:center; background-color:lightgrey;"><strong>Earning</strong> </td>
                                                        <td colspan="3" style="border: 1px solid black; padding-left: 5px; text-align:center; background-color:lightgrey;"><strong>Deduction</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <%--<td colspan="1" style="border: 1px solid black"></td>--%>
                                                        <td colspan="3" style="border: 1px solid black; vertical-align: top">
                                                            <asp:GridView ID="gvEarning" runat="server" HorizontalAlign="left" Width="100%">
                                                            </asp:GridView>
                                                        </td>
                                                        <td colspan="3" style="border: 1px solid black; vertical-align: top">
                                                            <asp:GridView ID="gvDeduction" runat="server" HorizontalAlign="Left" Width="100%">
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--<td colspan="1" style="border: 1px solid black"></td>--%>
                                                        <td colspan="1" style="border: 1px solid black; vertical-align: top; padding-left: 5px"><strong>TOTAL EARNING  </strong></td>
                                                        <td colspan="2" style="border: 1px solid black"><strong><%# Eval("TotalEarning") %></strong></td>
                                                        <td colspan="2" style="border: 1px solid black; vertical-align: top; padding-left: 5px"><strong>TOTAL DEDUCTION </strong></td>
                                                        <td colspan="1" style="border: 1px solid black"><strong><%# Eval("TotalDeduction") %></strong></td>
                                                    </tr>
                                                   
                                                </table>

                                                <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%; border-collapse: collapse; border-left: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black">      
                                                    <tr>
                                                        <td colspan="4" style="border: 1px solid black">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>GROSS SALARY</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px"><strong><%# Eval("TotalEarning") %></strong></td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label1"></asp:Label></td>
                                                    </tr>
                                                   <%--  <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>EXEMPTIONS U/S 10</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">NA</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label2"></asp:Label></td>
                                                    </tr>--%>
                                                  <%--  <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>BALANCE</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">NA</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label3"></asp:Label></td>
                                                    </tr>--%>
                                                    <%--<tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>STD DEDUCTION</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">NA</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label4"></asp:Label></td>
                                                    </tr>--%>
                                                    <%--<tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>PROF TAX</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">NA</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label5"></asp:Label></td>
                                                    </tr>--%>
                                                    <%--<tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>SALARY INCOME</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("NetSalary") %></td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="lblAmountInWords"></asp:Label></td>
                                                    </tr>--%>
                                                   <%-- <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>AGG CH VI</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">NA</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label6"></asp:Label></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>NET SALARY</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px"><strong><%# Eval("NetSalary") %></strong></td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <strong><asp:Label runat="server" ID="lblAmountInWords"></asp:Label></strong></td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>TAX ON TOT INCOME</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">//</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label8"></asp:Label></td>
                                                    </tr>--%>
                                                   <%-- <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>TAX DEDUCTED</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">//</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label9"></asp:Label></td>
                                                    </tr>--%>
                                                   <%-- <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>TOTAL TAX DED</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px">//</td>
                                                        <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                        <asp:Label runat="server" ID="Label10"></asp:Label></td>
                                                    </tr>--%>
                                                    <!-- ---------------------------------------------------------------------------------------------------------------------- -->
                                                   <%-- <tr>
                                                        <td style="border: 1px solid black; padding-left: 5px"><strong>Net Salary Credited To Your A/C No.:</strong></td>
                                                        <td colspan="3" style="border: 1px solid black; padding-left: 5px"><%# Eval("AccountNo") %>, <%# Eval("BankName") %>,<%# Eval("BranchName") %></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>PF No.</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("PFNo") %></td>
                                                        <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>ESIC No.:</strong></td>
                                                        <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("ESICNo") %></td>
                                                    </tr>--%>
                                                  <%--  <tr>
                                                        <td style="border: 1px solid black; padding-left: 5px"><strong>Remarks:</strong></td>
                                                        <td colspan="3" style="border: 1px solid black; padding-left: 5px">&nbsp;</td>
                                                    </tr>--%>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
                    </div>
                </div>
                <div id="divReport1" style="width: 100%; padding-top: 0px; display: none;">
                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="padding: 10px; padding-right: 30px;">
                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                <asp:DataList ID="dlPaySlip1" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 6px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlPaySlip1_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%; font-size: 9px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                            <tr>
                                                <td style="width: 20%; border: 1px solid black" align="center">
                                                    <img src="../Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 40%; border: 1px solid black; font-size: 12px;padding-left: 5px" colspan="3">FERTILIZERNAGAR SCHOOL<br /></td>
                                                <td style="width: 40%; border: 1px solid black; font-size: 11px;padding-left: 5px" colspan="3">P O FERTILIZERNAGAR - 391750 VADODARA<br /></td>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px">
                                                   
                                                    <strong>For the Month:<br />
                                                        Total Days:<br />
                                                        Earned Days:</strong>
                                                </td>

                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px">
                                                     <strong>OC/EC</strong><br />
                                                    <br />
                                                    <br />
                                                    <asp:Label runat="server" ID="lblMonth1"></asp:Label><br />
                                                    <%# Eval("TotalDaysofMonth") %><br />
                                                    <%# Eval("EarnedDaysofMonth") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="border: 1px solid black; padding-left: 5px"><strong>Emp Code</strong></td>
                                                <td colspan="1" style="border: 1px solid black; width: 15%; padding-left: 5px"><strong>Name</strong></td>
                                                <td style="border: 1px solid black; width: 20%; padding-left: 5px"><strong>Location</strong></td>
                                                <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>Department</strong></td>
                                                <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>Designation</strong></td>
                                                <td style="border: 1px solid black; width: 11%; padding-left: 5px"><strong>Join Date</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("EmployeeCode") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("EmployeeName") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("Location") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DepartmentName") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DesignationName") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DeptJoin") %></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="border: 1px solid black">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="border: 1px solid black"></td>
                                                <td colspan="2" style="border: 1px solid black; padding-left: 5px"><strong>Earnings(RS.)</strong> </td>
                                                <td colspan="3" style="border: 1px solid black; padding-left: 5px"><strong>Deductions(RS.)</strong></td>
                                            </tr>


                                            <tr>
                                                <td colspan="1" style="border: 1px solid black"></td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top;font-size: 10px;">
                                                    <asp:GridView ID="gvEarning1" runat="server" HorizontalAlign="left" Width="100%" Font-Size="8px">
                                                    </asp:GridView>
                                                </td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top;font-size: 10px;">
                                                    <asp:GridView ID="gvDeduction1" runat="server" HorizontalAlign="Left" Width="100%" Font-Size="8px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="border: 1px solid black"></td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top; padding-left: 5px"><strong>TotalEarnings(RS.): <%# Eval("TotalEarning") %> </strong></td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top; padding-left: 5px"><strong>TotalDeductions(RS.): <%# Eval("TotalDeduction") %></strong></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="border: 1px solid black" align="center"><strong>Leaves</strong></td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; font-size: 10px; vertical-align: top; height: 100%; border-collapse: collapse; border-left: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black">
                                            <tr>
                                                <%--   <td colspan="1" style="border: 1px solid black"></td>--%>
                                                <td align="center" colspan="4" style="border: 1px solid black; padding-left: 66px;font-size: 8px;">
                                                    <asp:GridView ID="gvLeave1" runat="server" Width="72%" Font-Size="8px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="border: 1px solid black">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>Net Salary(Rs.)</strong></td>
                                                <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("NetSalary") %></td>
                                                <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                    <asp:Label runat="server" ID="lblAmountInWords1"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; padding-left: 5px"><strong>Net Salary Credited To Your A/C No.:</strong></td>
                                                <td colspan="3" style="border: 1px solid black; padding-left: 5px"><%# Eval("AccountNo") %>, <%# Eval("BankName") %>,<%# Eval("BranchName") %></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>PF No.</strong></td>
                                                <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("PFNo") %></td>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>ESIC No.:</strong></td>
                                                <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("ESICNo") %></td>

                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; padding-left: 5px"><strong>Remarks:</strong></td>
                                                <td colspan="3" style="border: 1px solid black; padding-left: 5px">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                <br />
                                <br />
                                <asp:DataList ID="dlPaySlipOffice" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 6px; border: solid 2px black; padding-left: 2px; padding-top:4px" OnItemDataBound="dlPaySlipOffice_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%; font-size: 9px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                            <tr>
                                                <td style="width: 20%; border: 1px solid black" align="center">
                                                    <img src="../Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 40%; border: 1px solid black; font-size: 11px;padding-left: 5px" colspan="3">Fertilizer Nagar English and Gujarati Medium School<br />
                                                  
                                                    
                                                </td>

                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px">
                                                     
                                                    <strong>For the Month:<br />
                                                        Total Days:<br />
                                                        Earned Days:</strong>
                                                </td>

                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px">
                                                  <strong>OC/EC</strong>  <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label runat="server" ID="lblMonth12"></asp:Label><br />
                                                    <%# Eval("TotalDaysofMonth") %><br />
                                                    <%# Eval("EarnedDaysofMonth") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="border: 1px solid black; padding-left: 5px"><strong>Emp Code</strong></td>
                                                <td colspan="1" style="border: 1px solid black; width: 15%; padding-left: 5px"><strong>Name</strong></td>
                                                <td style="border: 1px solid black; width: 20%; padding-left: 5px"><strong>Location</strong></td>
                                                <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>Department</strong></td>
                                                <td style="border: 1px solid black; width: 12%; padding-left: 5px"><strong>Designation</strong></td>
                                                <td style="border: 1px solid black; width: 11%; padding-left: 5px"><strong>Join Date</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("EmployeeCode") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("EmployeeName") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("Location") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DepartmentName") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DesignationName") %></td>
                                                <td style="border: 1px solid black; padding-left: 5px"><%# Eval("DeptJoin") %></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="border: 1px solid black">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="border: 1px solid black"></td>
                                                <td colspan="2" style="border: 1px solid black; padding-left: 5px"><strong>Earnings(RS.)</strong> </td>
                                                <td colspan="3" style="border: 1px solid black; padding-left: 5px"><strong>Deductions(RS.)</strong></td>
                                            </tr>


                                            <tr>
                                                <td colspan="1" style="border: 1px solid black"></td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top; font-size: 10px;">
                                                    <asp:GridView ID="gvEarning12" runat="server" HorizontalAlign="left" Width="100%" Font-Size="8px">
                                                    </asp:GridView>
                                                </td>
                                                <td colspan="2" style="border: 1px solid black; font-size: 10px ;vertical-align: top;">
                                                    <asp:GridView ID="gvDeduction12" runat="server" HorizontalAlign="Left" Width="100%" Font-Size="8px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="border: 1px solid black"></td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top; padding-left: 5px"><strong>TotalEarnings(RS.): <%# Eval("TotalEarning") %> </strong></td>
                                                <td colspan="2" style="border: 1px solid black; vertical-align: top; padding-left: 5px"><strong>TotalDeductions(RS.): <%# Eval("TotalDeduction") %></strong></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="border: 1px solid black" align="center"><strong>Leaves</strong></td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; font-size: 10px; vertical-align: top; height: 100%; border-collapse: collapse; border-left: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black">
                                            <tr>
                                                <%--   <td colspan="1" style="border: 1px solid black"></td>--%>
                                                <td align="center" colspan="4" style="border: 1px solid black;font-size: 10px; padding-left: 66px;">
                                                    <asp:GridView ID="gvLeave12" runat="server" Width="72%" Font-Size="8px">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="border: 1px solid black">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>Net Salary(Rs.)</strong></td>
                                                <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("NetSalary") %></td>
                                                <td style="width: 50%; border: 1px solid black; padding-left: 5px" colspan="2">
                                                    <asp:Label runat="server" ID="lblAmountInWords12"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; padding-left: 5px"><strong>Net Salary Credited To Your A/C No.:</strong></td>
                                                <td colspan="3" style="border: 1px solid black; padding-left: 5px"><%# Eval("AccountNo") %>, <%# Eval("BankName") %>,<%# Eval("BranchName") %></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>PF No.</strong></td>
                                                <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("PFNo") %></td>
                                                <td style="width: 20%; border: 1px solid black; padding-left: 5px"><strong>ESIC No.:</strong></td>
                                                <td style="width: 30%; border: 1px solid black; padding-left: 5px"><%# Eval("ESICNo") %></td>

                                            </tr>
                                            <tr>
                                                <td style="border: 1px solid black; padding-left: 5px"><strong>Remarks:</strong></td>
                                                <td colspan="3" style="border: 1px solid black; padding-left: 5px">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        //jQuery("#aspnetForm").validationEngine('attach', {
        //    promptPosition: "bottomRight",
        //    validationEventTrigger: "submit",
        //    validateNonVisibleFields: false,
        //    updatePromptsPosition: true
        //});
        $(".autosuggest").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PaySlip.aspx/GetAllEmployeeNameForReport",
                    data: "{'prefixText':'" + request.term + "','TrustMID':'" + $(document.getElementById('<%= hfTrustMID.ClientID %>')).val() + "','SchoolMID':'" + $(document.getElementById('<%= hfSchoolMID.ClientID %>')).val() + "'}",
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
        $(document.getElementById('<%= btnBack.ClientID %>')).click(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "PaySlip.aspx/GetAllEmployeeNameForReport",
                        data: "{'prefixText':'" + request.term + "','TrustMID':'" + $(document.getElementById('<%= hfTrustMID.ClientID %>')).val() + "','SchoolMID':'" + $(document.getElementById('<%= hfSchoolMID.ClientID %>')).val() + "'}",
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
