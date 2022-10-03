<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentAttendanceConsolidated.aspx.cs" Inherits="GEIMS.Client.UI.StudentAttendanceConsolidated" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
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
      <script type="text/javascript">
          $(function () {
              $('#id_search').quicksearch('table#<%=gvStudentAttendance.ClientID%> tbody tr');
        })
    </script>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Student Attendance
             <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>

        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">

            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">

                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <fieldset>
                          <legend>Student Attendance Listing</legend>
                        <div style="width: 100%;" class="divclasswithfloat">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Date :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtAttendanceTakenDate" runat="server" Width="150px" CssClass="validate[required] TextBox"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtAttendanceTakenDate" TargetControlID="txtAttendanceTakenDate">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Class :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:DropDownList runat="server" CssClass="validate[required] TextBox" AutoPostBack="true" ID="ddlClass1" Width="150px" OnSelectedIndexChanged="ddlClass1_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div style="width: 100%;" class="divclasswithfloat">

                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Division :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDivision1" Width="150px">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <asp:Button ID="btnGo" runat="server" CssClass="btn-blue Attach" Width="50px" Text="Go" CausesValidation="false" OnClick="btnGo_Click" />
                    </fieldset>
                    <%--   <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>--%>
                    <br />
                    <asp:GridView ID="gvStudentAttendance" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvStudentAttendance_RowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="TotalStudentCount" HeaderText="Total Student">
                                <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PresentStudentCount" HeaderText="Present Student">
                                <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AbsentStudentCount" HeaderText="Absent Student">
                                <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("StudentAttendanceConsolidatedMID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("StudentAttendanceConsolidatedMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                <center> <div id="NoDataFound" runat="server" visible="false">No Data Found</div></center>

                <div id="divEmployee" runat="server">
                    <div id="tabs" runat="server" visible="false">
                        <div id="tab-panel" class="style-tabs" visible="true">
                            <ul>
                                <li><a id="tabClassDetails" href="#tabs-1">Student Attendance</a></li>
                            </ul>
                            <div id="tabs-1" style="padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <asp:HiddenField ID="hfCLassMID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hfDivisionTID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hfEmployeeID" runat="server" ClientIDMode="Static" />


                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Date :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtdate" runat="server" Width="150px" CssClass="validate[required] TextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="Calendar1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>

                                          <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Academic Year :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Width="150px">
                                            </asp:DropDownList>
                                        </div>


                                      
                                      
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Class :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" AutoPostBack="true" ID="ddlClass" Width="150px" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                           <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Division :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left; vertical-align: top;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDivision" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                     
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                       <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Total Student :
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtTotalStudentCount" runat="server" Width="150px" CssClass="TextBox" ReadOnly="true"></asp:TextBox>

                                        </div>
                                      
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                      
                                           <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Present Student Count :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtPresentStudentCount" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp][required]] TextBox"></asp:TextBox>

                                        </div>

                                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Absent Student Count:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtAbsentStudentCount" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp][required]] TextBox"></asp:TextBox>

                                        </div>
                                    </div>






                                    <div style="width: 100%; text-align: right;" class="divclasswithOutfloat">

                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />

                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right;"></div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
     <%--   $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });--%>
        </script>
</asp:Content>
