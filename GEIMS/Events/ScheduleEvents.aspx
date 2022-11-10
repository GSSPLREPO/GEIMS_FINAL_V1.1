<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="ScheduleEvents.aspx.cs" Inherits="GEIMS.Events.ScheduleEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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

        function charCategory(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || unicode == 32 || (unicode >= 65 && unicode <= 90) || (unicode >= 97 && unicode <= 122)) {
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
            $('#id_search').quicksearch('table#<%=gvScheduleEvent.ClientID%> tbody tr');
        })
    </script>


    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Events
            
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
                       </asp:Panel>
                    <asp:GridView ID="gvScheduleEvent" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvScheduleEvent_RowCommand"  OnSelectedIndexChanged="gvScheduleEvent_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="EventName" HeaderText="Event Name">
                                <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventFromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventToDate" HeaderText="To Date">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventLocation" HeaderText="Location">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%#Eval("ScheduledEventID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center"/>
                                <ItemStyle HorizontalAlign="center" Width="80px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("ScheduledEventID")%>' OnClientClick="javascript:return confirm('These Event Related Images Also Delete Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="120px" />
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
                        <li><a id="tabClassDetails" href="#tabs-1">Events</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 700px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <%--Height : 559Px--%>
                        <div style="width: 100%;">
                            <div style="width: 100%;">

                                <fieldset>
                                    <div>
                                    </div>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            School Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:DropDownList ID="ddlSchoolName" runat="server" CssClass="validate[required] Droptextarea" Width="232px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="text-align: left; width: 15%; float: left;" class="label">
                                            Section Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; float: right; width: 30%;">
                                            <asp:DropDownList ID="ddlSectionName" runat="server" CssClass=" validate[required] Droptextarea" Width="232px" Enabled="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Event Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtEventName" runat="server" onKeyUp="CheckTextLength(this,50)" onkeypress="return charName(event);" Width="225px" CssClass="validate[required] textarea autosuggest"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 15%; float: left;" class="label">
                                            Event Category :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; float: right; width: 30%;">
                                            <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass=" validate[required] Droptextarea" Width="232px" Enabled="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            From Date :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtFromDate" runat="server" onKeyUp="CheckTextLength(this,10)" onclick="return validateDate()" onkeypress="return numericDate(event);" placeholder="DD/MM/YYYY" CssClass="TextBox validate[required]" Width="225px"></asp:TextBox>
                                           <div>
                                            <asp:RangeValidator ID="valDate" runat="server" ControlToValidate="txtFromDate" MinimumValue="31/01/2020"
                                                MaximumValue="31/12/2100" ErrorMessage="Enter Valid Date in DD/MM/YYYY Format" Type="Date" Display="Dynamic" ForeColor="#FF3300" />
                                            </div>
                                               <ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            
                                             
                                        </div>
                                       
                                        <div style="text-align: left; width: 20%; float: left; height: 13px;" class="label">
                                            From Time :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 8%; float: left;">
                                            <asp:TextBox ID="txtFromDateFromTime" runat="server" onKeyUp="CheckTextLength(this,5)" onkeypress="return numericTime(event);" CssClass="TextBox validate[required]" Width="70px" placeholder="HH:MM"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Width="190px" runat="server" ErrorMessage="Time Formate(23:59)"
                                                ControlToValidate="txtFromDateFromTime" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$" CssClass="boldText" ForeColor="#FF3300" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                        </div>
                                        <div style="text-align: left; width: 7%; float: left; height: 13px;" class="label">
                                            To Time :<span style="color: red">*</span>
                                        </div>

                                        <div style="text-align: left; width: 10%; float: left;">
                                            
                                            <asp:TextBox ID="txtFromDateToTime" runat="server" onKeyUp="CheckTextLength(this,5)" onkeypress="return numericTime(event);" CssClass="TextBox validate[required]" Width="70px" placeholder="HH:MM"></asp:TextBox>
                                            <div>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Width="190px" runat="server" ErrorMessage="Time Formate(23:59)"
                                                ControlToValidate="txtFromDateToTime" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$" CssClass="boldText" ForeColor="#FF3300" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        
                                        <div style="text-align:right; padding-right:70px;">
                                            <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="From Time Cannot Be Greter then To Time" ControlToValidate="txtFromDateToTime" ControlToCompare="txtFromDateFromTime" Font-Overline="False" Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True" Font-Bold="False"></asp:CompareValidator>--%>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="From Time Cannot Be Greater than To Time" ControlToValidate="txtFromDateToTime" ControlToCompare="txtFromDateFromTime" Font-Overline="False" Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True" Font-Bold="False"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            To Date :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtToDate" runat="server" onKeyUp="CheckTextLength(this,10)" onkeypress="return numericDate(event);" placeholder="DD/MM/YYYY" CssClass="TextBox validate[required]" Width="225px"></asp:TextBox>
                                            
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <div>
                                                 <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtToDate" MinimumValue="31/01/2020"
                                                MaximumValue="31/12/2100" ErrorMessage="Enter Valid Date in DD/MM/YYYY Format" Type="Date" Display="Dynamic" ForeColor="#FF3300" />
                                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Operator="LessThanEqual" ControlToValidate="txtFromDate" ControlToCompare="txtToDate" ErrorMessage="From Date cannot be greter then To Date" Font-Bold="False" ForeColor="#FF3300" SetFocusOnError="True" Type="Date"></asp:CompareValidator>--%>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="LessThanEqual" ControlToValidate="txtFromDate" ControlToCompare="txtToDate" ErrorMessage="From Date cannot be greater than To Date" Font-Bold="False" ForeColor="#FF3300" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left; height: 13px;" class="label">
                                            From Time :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 8%; float: left;">
                                            <asp:TextBox ID="txtToDateFromTime" runat="server" onKeyUp="CheckTextLength(this,5)" onkeypress="return numericTime(event);" CssClass="TextBox validate[required]" Width="70px" placeholder="HH:MM"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Width="190px" runat="server" ErrorMessage="Time Formate(23:59)"
                                                ControlToValidate="txtToDateFromTime" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$" CssClass="boldText" ForeColor="#FF3300" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        </div>
                                        <div style="text-align: left; width: 7%; float: left; height: 13px;" class="label">
                                            To Time :<span style="color: red">*</span>
                                        </div>

                                        <div style="text-align: left; width: 10%; float: left;">
                                            <asp:TextBox ID="txtToDateToTime" runat="server" onKeyUp="CheckTextLength(this,5)" onkeypress="return numericTime(event);" CssClass="TextBox validate[required]" Width="70px" placeholder="HH:MM"></asp:TextBox>
                                            
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Width="190px" runat="server"  ErrorMessage="Time Formate(23:59)"
                                                ControlToValidate="txtToDateToTime" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$" CssClass="boldText" ForeColor="#FF3300" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        </div>
                                        <div style="text-align:right; padding-right:70px;">
                                            <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="From Time Cannot Be Greter then To Time" ControlToValidate="txtToDateToTime" ControlToCompare="txtToDateFromTime" Font-Overline="False" Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True" Font-Bold="False"></asp:CompareValidator>--%>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="From Time Cannot Be Greater than To Time" ControlToValidate="txtToDateToTime" ControlToCompare="txtToDateFromTime" Font-Overline="False" Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True" Font-Bold="False"></asp:CompareValidator>
                                        </div>
                                     
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Platform :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; float: left; width: 78%;">
                                            <asp:DropDownList ID="ddlPlatform" runat="server" CssClass=" validate[required] Droptextarea" Width="232px" Enabled="true">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Event Location :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; float: left; width: 78%;">
                                            <asp:TextBox ID="txtEventLocation" runat="server" onKeyUp="CheckTextLength(this,150)" CssClass="TextBox validate[required]" Width="93%"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Event  Description :
                                        </div>
                                        <div style="text-align: left; float: left; width: 78%;">
                                            <asp:TextBox ID="txtEventDescription" runat="server" onKeyUp="CheckTextLength(this,5000)" CssClass="TextArea" Width="93%" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div style="width: 100%;">
                                <fieldset>
                                    <legend>Event Organiser Details</legend>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Organiser Name: 
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtOrganiserName" runat="server" onkeypress="return charCategory(event);" onKeyUp="CheckTextLength(this,50)" CssClass="TextBox" Width="230px" OnTextChanged="txtOrganiserName_TextChanged"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left; height: 4px;" class="label">
                                            Organiser Section:
                                        </div>
                                        <div style="text-align: left; float: right; width: 30%;">
                                            <asp:DropDownList ID="ddlOrganiserSection" runat="server" CssClass=" validate" Width="232px" Enabled="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left; height: 15px;" class="label">
                                            Event Notes :
                                        </div>
                                        <div style="text-align: left; float: left; width: 78%;">
                                            <asp:TextBox ID="txtEventNote" runat="server" onKeyUp="CheckTextLength(this,5000)" CssClass="TextArea" Width="93%" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Mobile No: 
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtMobileNo" runat="server" onKeyUp="CheckTextLength(this,10)" onkeypress="return numericMobile(event);" CssClass="TextBox" Width="230px" TextMode="Phone"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left; height: 4px;" class="label">
                                            Email:
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtEmailID" runat="server" onKeyUp="CheckTextLength(this,50)" CssClass="validate[custom[email]] TextBox" Width="232px" TextMode="Email"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Enter Valid 10 Digit Mobile Number First Digit Start with 5 to 9" ControlToValidate="txtMobileNo" Font-Bold="False" MaximumValue="9999999999" MinimumValue="5000000000" ForeColor="#FF3300" SetFocusOnError="True" Type="Double"></asp:RangeValidator>
                                        <br />
                                    </div>
                                </fieldset>
                            </div>
                            <br />
                            <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right;"></div>
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
        $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });


    </script>
</asp:Content>
