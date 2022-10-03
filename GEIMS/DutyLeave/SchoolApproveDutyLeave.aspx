<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SchoolApproveDutyLeave.aspx.cs" Inherits="GEIMS.Leave.SchoolApproveDutyLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
     <script type="text/javascript" language="javascript">
         //Textbox Enter only Character Validation
         function AllowAlphabet(e) {
             isIE = document.all ? 1 : 0
             keyEntry = !isIE ? e.which : event.keyCode;
             if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                 return true;
             else {
                 alert('Please Enter Only Character values.');
                 return false;
             }
         }
         //Textbox Length Check Validation
         function CheckTextLength(text, long) {
             var maxlength = new Number(long); // Change number to your max length.
             if (text.value.length > maxlength) {
                 text.value = text.value.substring(0, maxlength);
                 alert(" Only " + long + " characters allowed");
             }
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvLeaveApprove.ClientID%> tbody tr');
        })
    </script>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Approve Leave
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                    <asp:GridView ID="gvLeaveApprove" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvRole_OnRowCommand"
                        OnPreRender="gvRole_OnPreRender">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="DutyLeaveApplyID" HeaderText="DutyLeaveApplyID">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmployeeMID" HeaderText="EmployeeMID">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name">
                                <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FromDate" HeaderText="From Date">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ToDate" HeaderText="To Date">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Reason" HeaderText="Reason">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Approve">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Status.png"
                                        CommandName="Approve1" CommandArgument='<%# Eval("DutyLeaveApplyID")%>' Height="20px" Width="20px" OnClientClick="javascript:return confirm('Are you sure,You want to approve leave?');" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("DutyLeaveApplyID")%>' OnClientClick="javascript:return confirm('Are you sure, You want to change approve leave?');"
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
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Approve Leave</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 130px; padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div id="divStatus" runat="server" style="width: 100%; float: left">
                            <div style="padding-top: 10px">
                                <div style="float: left; width: 15%;">
                                    Status :<span style="color: red">*</span>
                                </div>
                                <div style="float: left;">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="textbox validate[required]" Width="200px">
                                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                        <asp:ListItem Value="0">Applied</asp:ListItem>
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; padding-left: 100px;">
                                    <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_OnClick" />
                                </div>
                            </div>
                            <br />

                        </div>
                       <%-- <div style="width: 100%; text-align: right; float: left">
                            <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_OnClick" />
                        </div>--%>
                    </div>
                </div>
                <asp:Panel ID="pnlApproveLeave" runat="server" GroupingText="Approve Leave">
                    <asp:GridView ID="gvLeaveBalance" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="30%" BackColor="White">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="LeaveName" HeaderText="Leave Name">
                                <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top"  />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true"/>
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
                    <br/>
                    <div id="div1" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                        <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCreated="gvLeave_OnRowCreated">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                            <Columns>
                                 <asp:TemplateField HeaderText="LeaveApprovalID" >
                                    <ItemTemplate >
                                        <asp:Label runat="server" ID="lblLeaveApprovalID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve Leave">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbApprove" runat="server"
                                            Font-Names="Verdana" Font-Size="12px" Width="50px"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridDates" runat="server" CssClass="validate[required] TextBox"
                                            Font-Names="Verdana" Height="13px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;" Width="110px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="txtGridDates">
                                        </ajaxToolkit:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Half Day?">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbHalfDay" runat="server"
                                            Font-Names="Verdana" Font-Size="12px" Width="50px"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave Type" FooterText="Total Amount">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="validate[required] TextBox"
                                            Font-Names="Verdana" Font-Size="12px" Width="200px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Reason" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="validate[required] TextBox"
                                                    Font-Names="Verdana" TextMode="MultiLine" Rows="2" Style="text-align: left;" Width="300px" Height="40px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="JournalID" Visible="False" />
                            </Columns>
                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <div id="divReason" runat="server" style="padding-top: 20px">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                <%--Reject--%> Reason :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; float: left; width: 80%;">
                                <asp:TextBox ID="txtReason" runat="server" CssClass="validate[required] TextArea" Width="150px" Height="100%" TextMode="MultiLine" onKeyUp="CheckTextLength(this,250)"></asp:TextBox>
                            </div>
                        </div>
                        <div id="divApproved" runat="server" style="padding-top: 20px;">
                            <div style="text-align: left; width: 20%; float: left; margin-top:15px;" class="label">
                               Approved Date :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; float: left; width: 80%; margin-top:15px;">
                              <%--  <asp:TextBox ID="txtApprovedDate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%" TextMode="Date"></asp:TextBox>--%>
                                <asp:TextBox ID="txtApprovedDate" runat="server" CssClass="validate[required] TextBox"
                                            Font-Names="Verdana" Height="13px" onblur="howManyDecimals(this.id,'#FFFFFF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;" Width="110px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="txtApprovedDate">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 30px;">
                                <div style="float: left; text-align: left; width: 100%; padding-bottom: 10px;">
                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnSave_OnClick"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div id="divContent3" runat="server" style="width: 100%; float: left;"><span style="color: red">*</span> No Leaves Applied</div>
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

    </script>
</asp:Content>
