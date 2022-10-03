<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="ApplyLeave.aspx.cs" Inherits="GEIMS.Leave.ApplyLeave" %>
<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <%--<script>
          $(function () {
              $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
      </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Apply for Leave
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                    <div id="tabs1" style="min-height: 150px;" runat="server">

                        <asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="Apply For Leave">
                            <div id="divPanel" runat="server">
                                <div style="width: 100%; float: right; padding-top: 0px;" class="label">
                                    <div style="padding: 10px; padding-right: 30px;">
                                        <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                            <asp:Button runat="server" ID="btnApplyLeave" Text="Apply Leave" CssClass="btn-blue-new btn-blue-medium" OnClick="btnApplyLeave_OnClick" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divDate" runat="server">
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
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
                                    </div>

                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: left; width: 100%; padding-bottom: 10px;">
                                                <asp:Button runat="server" ID="btnApply" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnApply_OnClick" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divLeaveApprove" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                                <%-- <div id="search">
                                    <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                                </div>--%>
                                <br />
                                <asp:GridView ID="gvLeaveApprove" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvLeaveApprove_OnRowCommand"
                                    OnPreRender="gvLeaveApprove_OnPreRender">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="LeaveApplyID" HeaderText="LeaveApplyID">
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
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                                    CommandName="Delete1" CommandArgument='<%# Eval("LeaveApplyID")%>' Height="20px" Width="20px" CssClass="detach" OnClientClick="javascript:return confirm('Are you sure,You want to approve leave?');" />
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
                            <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                                <div id="div1" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                                    <br />
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
                                <%--<div style="padding-bottom: 20px;">  <span style="color: red">Note :*</span> Selection of apply dates must be between FromDate and ToDate.</div>--%>
                                <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnSelectedIndexChanged="gvLeave_SelectedIndexChanged">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGridDates" runat="server" CssClass="validate[required] TextBox"
                                                    Font-Names="Verdana" Height="13px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;" Width="110px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" TargetControlID="txtGridDates">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
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
                                                    Font-Names="Verdana" Font-Size="12px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" >
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
                                <div style="width: 100%; float: left;" class="label">
                                    <div style="padding: 10px;">
                                        <div style="float: left; width: 15%;">
                                            Reason :<span style="color: red">*</span>
                                        </div>
                                        <div style="float: left; width: 185px;">
                                            <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="textbox validate[required]" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="padding: 10px; padding-right: 30px;">
                                        <div style="float: left; text-align: left; width: 100%; padding-bottom: 10px;">
                                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnSave_OnClick" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>

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
    </div>

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });
    </script>
    <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
