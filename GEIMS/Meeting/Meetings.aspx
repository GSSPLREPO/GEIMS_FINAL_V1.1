<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="Meetings.aspx.cs" Inherits="GEIMS.Meeting.Meetings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function numericTime(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 58)) {
                return true;
            }
            else {
                return false;
            }
        }

        function numericDate(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 47 && unicode <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }

        function charName(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || unicode == 32 || unicode == 34 || unicode == 39 || unicode == 95 || (unicode >= 48 && unicode <= 57)
                || (unicode >= 64 && unicode <= 90) || (unicode >= 97 && unicode <= 122)) {
                return true;
            }
            else {
                return false;
            }
        }

        function charNamePer(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || unicode == 32 || (unicode >= 64 && unicode <= 90) || (unicode >= 97 && unicode <= 122)) {
                return true;
            }
            else {
                return false;
            }
        }

        function numericMobile(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckTextLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                alert(" Only " + long + " characters allowed");
            }
        }

    </script>

    <style>
        .TextArea {
            max-width: 100%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvMeeting.ClientID%> tbody tr');
        })
    </script>

    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Meeting
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">



                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <asp:Panel ID="pnlSearch" runat="server">
                        <div id="search">
                            <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                        </div>
                        <br />
                    </asp:Panel>

                    <div class="divclasswithfloat" style="height: 30px; width: 70%; padding-left: 30%;">
                        <div style="text-align: left; width: 10%; float: left;" class="label">
                            Month :
                        </div>
                        <div style="text-align: left; width: 15%; float: left;">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="Droptextarea" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>

                        </div>


                        <div style="text-align: left; padding-left: 30px; width: 10%; float: left;" class="label">
                            Year :
                        </div>
                        <div style="text-align: left; width: 15%; float: left;">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="Droptextarea" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>

                        </div>
                    </div>


                    <asp:GridView ID="gvMeeting" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvMeeting_RowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="CategoryName" HeaderText="Cateogry">
                                <HeaderStyle Width="8%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="8%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Topic" HeaderText="Topic">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MeetingDate" HeaderText="Date">
                                <HeaderStyle Width="8%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="8%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MeetingTime" HeaderText="Time">
                                <HeaderStyle Width="6%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="6%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Venue" HeaderText="Venue">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mode" HeaderText="Mode">
                                <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OEmployee" HeaderText="Organize By">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MEmployee" HeaderText="Minutes By">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <HeaderStyle Width="6%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="6%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("MeetingID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("MeetingID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%; align-content: center; padding-top: 1px;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Meeting</a></li>
                    </ul>
                    <%--Set Height--%>


                    <div id="tabs-1" style="height: auto; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <fieldset>
                            <legend>Meeting Details</legend>
                            <div style="width: 100%;">
                                <div class="divclasswithfloat" style="height: 30px;">
                                    <div style="text-align: left; width: 15%; float: left" class="label">
                                        Meeting Category :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 31%; float: left;">
                                        <asp:DropDownList ID="ddlMettingCategory" runat="server" Width="100%" CssClass="Droptextarea" ValidationGroup="vg1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This Field Required" ControlToValidate="ddlMettingCategory" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="text-align: left; padding-left: 90px; width: 10%; float: left;" class="label">
                                        Topic :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:TextBox ID="txtTopic" runat="server" onKeyUp="CheckTextLength(this,50)" onkeypress="return charName(event);" CssClass="TextBox" Width="100%" ValidationGroup="vg1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtTopic" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="divclasswithfloat" style="height: 50px;">
                                    <div style="text-align: left; width: 15%; float: left;" class="label">
                                        Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:TextBox ID="txtDate" runat="server" onKeyUp="CheckTextLength(this,10)" onclick="return validateDate()" onkeypress="return numericDate(event);" CssClass="TextBox" placeholder="DD/MM/YYYY" Width="100%" ValidationGroup="vg1"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtDate" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:RangeValidator ID="valDate" runat="server" ControlToValidate="txtDate" MinimumValue="31/01/2020"
                                                MaximumValue="31/12/2100" ErrorMessage="Enter Valid Date in DD/MM/YYYY Format" Type="Date" Display="Dynamic" ForeColor="#FF3300" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDate" TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="text-align: left; padding-left: 100px; width: 10%; float: left;" class="label">
                                        Time :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 6%; float: left;" class="label">
                                        From :
                                    </div>
                                    <div style="text-align: left; width: 12%; float: left;">
                                        <asp:TextBox ID="txtFromTime" runat="server" onKeyUp="CheckTextLength(this,5)" onkeypress="return numericTime(event);" CssClass="TextBox" placeholder="HH:MM" Width="80%" ValidationGroup="vg1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Field Required" ControlToValidate="txtFromTime" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Width="200px" runat="server" ErrorMessage="Time Format(23:59)"
                                            ControlToValidate="txtFromTime" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$" CssClass="boldText" ForeColor="#FF3300" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                    </div>

                                    <div style="text-align: left; width: 6%; float: left;" class="label">
                                        To :
                                    </div>
                                    <div style="text-align: left; width: 10%; float: left;">
                                        <asp:TextBox ID="txtToTime" runat="server" onKeyUp="CheckTextLength(this,5)" onkeypress="return numericTime(event);" CssClass="TextBox" placeholder="HH:MM" Width="80%" ValidationGroup="vg1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Field Required" ControlToValidate="txtToTime" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Width="200px" runat="server" ErrorMessage="Time Format(23:59)"
                                            ControlToValidate="txtToTime" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$" CssClass="boldText" ForeColor="#FF3300" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                    </div>
                                    <div>

                                    </div>
                                    <div style="float: right; padding-right:20px;">
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From Time Cannot Be Greter then To Time" ControlToValidate="txtToTime" ControlToCompare="txtFromTime" Font-Overline="False" ValidationGroup="vg1" Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True"></asp:CompareValidator>
                                    </div>

                                </div>

                                <br />

                                <div class="divclasswithfloat" style="height: 30px;">
                                    <div style="text-align: left; width: 15%; float: left;" class="label">
                                        Venue :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:TextBox ID="txtVenue" runat="server" onKeyUp="CheckTextLength(this,50)" onkeypress="return charName(event);" CssClass="TextBox" Width="100%" ValidationGroup="vg1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtVenue" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="text-align: left; padding-left: 100px; width: 10%; float: left;" class="label">
                                        Mode:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 31%; float: left;">
                                        <asp:DropDownList ID="ddlMode" runat="server" CssClass="Droptextarea" Width="100%" ValidationGroup="vg1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="This Field Required" ControlToValidate="ddlMode" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>



                                <div class="divclasswithfloat" style="height: 30px;">
                                    <div style="text-align: left; width: 15%; float: left;" class="label">
                                        Organize By :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 31%; float: left;">
                                        <asp:DropDownList ID="ddlOrganize" runat="server" CssClass="Droptextarea" Width="100%" ValidationGroup="vg1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="This Field Required" ControlToValidate="ddlOrganize" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>


                                    <div style="text-align: left; padding-left: 90px; width: 10%; float: left;" class="label">
                                        Status:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 31%; float: left;">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="Droptextarea" Width="100%" ValidationGroup="vg1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="This Field Required" ControlToValidate="ddlStatus" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="divclasswithfloat" style="height: 30px;">
                                    <div style="text-align: left; width: 15%; float: left;" class="label">
                                        Minitues By :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 31%; float: left;">
                                        <asp:DropDownList ID="ddlMinituesBy" runat="server" CssClass="Droptextarea" Width="100%" ValidationGroup="vg1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="This Field Required" ControlToValidate="ddlMinituesBy" ForeColor="#FF3300" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <br />



                                <div style="width: 97%; text-align: right;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" ValidationGroup="vg1" />
                                </div>

                            </div>
                            <div>
                            </div>
                            <asp:Panel ID="pnlButton" runat="server">
                                <div style="width: 40%; float: left; text-align: right;">
                                    <asp:Button runat="server" ID="btnAgendaShow" Text="Add Agenda" CssClass="btn-blue-new btn-blue-medium" OnClick="btnAgendaShow_Click" CausesValidation="False" />
                                </div>
                                <div style="width: 40%; float: right; text-align: left">
                                    <asp:Button runat="server" ID="btnParticipantShow" Text="Add Participant" CssClass="btn-blue-new btn-blue-medium" OnClick="btnParticipantShow_Click" CausesValidation="False" />
                                </div>
                            </asp:Panel>
                        </fieldset>



                        <%--  <asp:UpdatePanel ID="UpdatePanelAgenda" runat="server">

                            <ContentTemplate>--%>
                        <asp:Panel ID="pnlAgenda" runat="server">

                            <fieldset>
                                <legend>agenda</legend>
                                <div class="divclasswithfloat">
                                    <div style="text-align: center; padding-left: 200px; width: 15%; float: left;" class="label">
                                        Agenda Point :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: center; width: 30%; float: left;">
                                        <asp:TextBox ID="txtAgendaPoint" runat="server" onKeyUp="CheckTextLength(this,100)" onkeypress="return charName(event);" CssClass="TextBox" Width="100%" ValidationGroup="vg2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtAgendaPoint" ForeColor="#FF3300" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="text-align: right; padding-right: 27%;">
                                        <asp:Button runat="server" ID="btnAgenda" Text="Add" CssClass="btn-blue-new btn-blue-medium" ValidationGroup="vg2" OnClick="btnAgenda_Click" />
                                    </div>


                                </div>

                                <asp:GridView ID="gvAgenda" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvAgenda_RowCommand" OnRowDataBound="gvAgenda_RowDataBound">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="50px" ControlStyle-Width="50px" FooterStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AgendaPoint" HeaderText="Agenda Point">
                                            <HeaderStyle Width="80%" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>


                                        <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                                    CommandName="DeleteAgenda" CommandArgument='<%# Eval("AgendaID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                    Height="20px" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </fieldset>
                        </asp:Panel>

                        <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>

                        <%--  <asp:UpdatePanel ID="UpdatePanelParticipant" runat="server">
                            <ContentTemplate>--%>

                        <asp:Panel ID="pnlParticipant" runat="server">
                            <fieldset>
                                <legend>Participants</legend>
                                <%--  <asp:UpdatePanel ID="UpdatePanelParticipant" runat="server">
                                    <ContentTemplate>--%>
                                <div class="divclasswithfloat">
                                    <div style="text-align: center; padding-left: 35%; width: 15%; float: left;" class="label">
                                        Employee :
                                    <asp:RadioButton ID="rdoEmployee" runat="server" GroupName="g1" OnCheckedChanged="rdoEmployee_CheckedChanged" AutoPostBack="True" />
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        External :
                                   <asp:RadioButton ID="rdoExternal" runat="server" GroupName="g1" OnCheckedChanged="rdoExternal_CheckedChanged" AutoPostBack="True" />

                                    </div>



                                </div>
                                <asp:Panel ID="pnlEmployee" runat="server">
                                    <div class="divclasswithfloat">
                                        <div style="text-align: center; padding-left: 200px; width: 15%; float: left;" class="label">
                                            Select Employee :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: center; width: 30%; float: left;">
                                            <asp:DropDownList ID="ddlEmployeeName" runat="server" Width="100%" ValidationGroup="vg3"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="This Field Required" ControlToValidate="ddlEmployeeName" ForeColor="#FF3300" ValidationGroup="vg3"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="text-align: right; padding-right: 27%;">
                                            <asp:Button runat="server" ID="btnAddParticipantEmployee" Text="Add" CssClass="btn-blue-new btn-blue-medium" ValidationGroup="vg3" OnClick="btnAddParticipantEmployee_Click" />
                                        </div>
                                    </div>

                                </asp:Panel>

                                <asp:Panel ID="pnlExternal" runat="server">
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; padding-left: 80px; width: 10%; float: left" class="label">
                                            Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtParticipantName" runat="server" onKeyUp="CheckTextLength(this,50)" onkeypress="return charNamePer(event);" CssClass="TextBox" Width="100%" ValidationGroup="vg4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtParticipantName" ForeColor="#FF3300" ValidationGroup="vg4"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="text-align: left; padding-left: 60px; width: 10%; float: left;" class="label">
                                            Orgenization :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtOrgName" runat="server" onKeyUp="CheckTextLength(this,100)" onkeypress="return charName(event);" CssClass="TextBox" Width="100%" ValidationGroup="vg4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtOrgName" ForeColor="#FF3300" ValidationGroup="vg4"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; padding-left: 80px; width: 10%; float: left" class="label">
                                            Email :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtEmail" runat="server" onKeyUp="CheckTextLength(this,50)" CssClass="TextBox" Width="100%" TextMode="Email" ValidationGroup="vg4" AutoCompleteType="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtEmail" ForeColor="#FF3300" ValidationGroup="vg4"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="text-align: right; padding-right: 42%;">
                                            <asp:Button runat="server" ID="btnAddParticipantExternal" Text="Add" CssClass="btn-blue-new btn-blue-medium" ValidationGroup="vg4" OnClick="btnAddParticipantExternal_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="gvParticipant" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvParticipant_RowCommand" OnRowDataBound="gvParticipant_RowDataBound">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="30px" ControlStyle-Width="30px" FooterStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo1" runat="server" Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name">
                                            <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrgName" HeaderText="Orgenization">
                                            <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Email" HeaderText="Email">
                                            <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Send Invite">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSendInvite" runat="server" TextAlign="Right" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="55px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send MOM">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSendMom" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAbsent" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="35px" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="EditParticipant" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                            CommandName="Edit1" CommandArgument='<%# Eval("ParticipantID")%>' Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                                </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="DeleteParticipant" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                                    CommandName="DeleteParticipant" CommandArgument='<%# Eval("ParticipantID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                    Height="20px" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="40px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                                <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <br />
                                <div class="divclasswithfloat">
                                    <div style="text-align: right; float: right;">
                                        <asp:Button runat="server" ID="btnUpdate" Text="Absent Update" CssClass="btn-blue-new btn-blue-medium" CausesValidation="False" OnClick="btnUpdate_Click" />
                                    </div>
                                </div>

                                <div class="divclasswithfloat">
                                    <div style="text-align: right; float: right;">
                                        <asp:Button runat="server" ID="btnSendMOM" Text="Send MOM" CssClass="btn-blue-new btn-blue-medium" CausesValidation="False" OnClick="btnSendMOM_Click" />
                                    </div>
                                    <div style="text-align: right; float: right;">
                                        <asp:Button runat="server" ID="btnSendInvite" Text="Send Invite" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSendInvite_Click" CausesValidation="False" />
                                    </div>

                                </div>

                            </fieldset>
                        </asp:Panel>

                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>

                </div>

                <div id="divContent3" style="width: 10%; float: right;"></div>
            </div>
        </div>
    </div>
    <div>
        <asp:Panel ID="pnlSendMOM" runat="server">

            <div runat="server" id="div2" style="width: 100%; float: left; padding-top: 0px; padding-bottom: 20px;" class="label">

                <div class="headerLogo" style="margin-top: 9px; text-align: left; width: 10%; float: left;">
                    <asp:Image ImageUrl="~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" runat="server" ID="Image1"
                        Width="100px" Height="100px" />
                </div>

                <div style="float: right; text-align: left; width: 100%; padding-bottom: 10px; padding-top: 20px; margin-left: 0%;">
                    <div style="text-align: center; padding-bottom: 20px;">
                        <asp:Label runat="server" Text="MINUTES OF MEETING" Font-Size="Medium" Font-Bold="true"></asp:Label>
                    </div>
                    <hr />
                </div>

            </div>

            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                        Topic :
                                                <asp:Label runat="server" ID="lblTopic"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                        Date :
                                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                    </div>
                    <div style="float: right; text-align: right; width: 40%; padding-bottom: 10px;">
                        Start Time :
                                                <asp:Label runat="server" ID="lblFromTime"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                        Place :
                                                <asp:Label runat="server" ID="lblPlace"></asp:Label>
                    </div>
                    <div style="float: right; text-align: right; width: 40%; padding-bottom: 10px;">
                        End Time :
                                                <asp:Label runat="server" ID="lblToTime"></asp:Label>
                    </div>
                </div>
            </div>

            <%--<div style="width: 100%; float: left; padding-top: 20px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: left;  width: 40%; padding-bottom: 10px;">
                                        Mode :
                                                <asp:Label runat="server" ID="lblMode"></asp:Label>
                                    </div>
                                   
                                </div>
                            </div>--%>

            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                        <asp:Label Text="Agenda" runat="server" ID="Label1" Font-Size="16px" Font-Underline="True"></asp:Label>

                    </div>

                </div>
            </div>

            <div style="padding-top: 10px; padding-right: 0px; float: left; width: 100%">
                <asp:GridView ID="gvReportAgenda" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="False"
                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" Width="100%" OnRowDataBound="gvReportAgenda_RowDataBound">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="50px" ControlStyle-Width="50px" FooterStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblSrAgenda" runat="server" Width="10px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AgendaPoint" HeaderText="Agenda">
                            <HeaderStyle Width="95%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="90%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                        BorderWidth="1px" BorderStyle="Solid" />

                </asp:GridView>
            </div>

            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                        <asp:Label Text="Participant" runat="server" ID="Label2" Font-Size="16px" Font-Underline="True"></asp:Label>

                    </div>

                </div>
            </div>

            <div style="padding-top: 10px; padding-right: 0px; float: left; width: 100%">
                <asp:GridView ID="gvReportPaticipant" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="False"
                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" Width="100%" OnRowDataBound="gvReportPaticipant_RowDataBound">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="50px" ControlStyle-Width="50px" FooterStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblSrParticipant" runat="server" Width="10px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name">
                            <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrgName" HeaderText="Orgenization">
                            <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email">
                            <HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                        BorderWidth="1px" BorderStyle="Solid" />

                </asp:GridView>
            </div>

            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                        <asp:Label Text="Meeting Point" runat="server" ID="Label3" Font-Size="16px" Font-Underline="True"></asp:Label>

                    </div>

                </div>
            </div>

            <div style="padding-top: 10px; padding-right: 0px; float: left; width: 100%">
                <asp:GridView ID="gvReportPoint" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="False"
                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" Width="100%" OnRowDataBound="gvReportPoint_RowDataBound">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="50px" ControlStyle-Width="50px" FooterStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblSrPoint" runat="server" Width="10px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Point" HeaderText="Point Discussed">
                            <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Action" HeaderText="Action To Be Taken">
                            <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AssignTo" HeaderText="Responsible Person">
                            <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TargetDate" HeaderText="Target Date">
                            <HeaderStyle Width="8%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="8%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                            <HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                        BorderWidth="1px" BorderStyle="Solid" />

                </asp:GridView>
            </div>
        </asp:Panel>


    </div>



    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>

    <%-- <triggers>
        <asp:PostBackTrigger ControlID="btnExportPDF" />
        <asp:PostBackTrigger ControlID="btnExportExcel" />
            <asp:PostBackTrigger ControlID="btnExportWord" />
    </triggers>--%>
</asp:Content>
