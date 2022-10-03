<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="MeetingPoints.aspx.cs" Inherits="GEIMS.Meeting.MeetingPoints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
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
            $('#id_search').quicksearch('table#<%=gvMeetingPoints.ClientID%> tbody tr');
        })
    </script>



    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Meeting Points
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <asp:Panel ID="pnlDropdown" runat="server">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                        <asp:Panel ID="pnlMonthYear" runat="server">
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
                        <asp:Panel ID="pnlMeetingName" runat="server">
                        <div id="divMeetingName" class="divclasswithfloat" style="height: 30px; width: 70%; padding-left: 25%;">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Meeting Name :
                            </div>
                            <div style="text-align: left; width: 50%; float: left;">
                                <asp:DropDownList ID="ddlMeetingName" runat="server" CssClass="Droptextarea" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMeetingName_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                        </div>
                            </asp:Panel>
                    </asp:Panel>

                    <asp:GridView ID="gvMeetingPoints" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvMeetingPoints_RowCommand" OnRowDataBound="gvMeetingPoints_RowDataBound">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="50px" ControlStyle-Width="50px" FooterStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrNo" runat="server" Width="10px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField DataField="MeetingName" HeaderText="Meeting Name">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Point" HeaderText="Point">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="Action">
                                <HeaderStyle Width="35%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="35%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AssignTo" HeaderText="Assign">
                                <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TargetDate" HeaderText="Target Date">
                                <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                <HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("PointID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("PointID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="2%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%; padding-top: 1px;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Meeting Points</a></li>
                    </ul>

                    <%--set height--%>
                    <div id="tabs-1" style="height: 380px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">

                        <div style="width: 100%;">
                            
                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Meeting :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 30%; float: left;">
                                    <asp:DropDownList ID="ddlMeeting" runat="server" CssClass="Droptextarea validate[required]" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Point :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 40%; float: left;">
                                    <asp:TextBox ID="txtPoint" runat="server" onKeyUp="CheckTextLength(this,50)" onkeypress="return charName(event);" CssClass="validate[required] TextBox" Width="100%"></asp:TextBox>
                                </div>
                            </div>

                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Action :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 40%; float: left;">
                                    <asp:TextBox ID="txtAction" runat="server" onKeyUp="CheckTextLength(this,5000)" CssClass="validate[required] TextBox" Width="150%" Height="77px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Assign To :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 41%; float: left;">
                                    <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="Droptextarea validate[required]" Width="100%"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Target Date :<span style="color: red">*</span>

                                </div>
                                <div style="text-align: left; width: 8%; float: left;">
                                    <asp:TextBox ID="txtTargetDate" runat="server" onKeyUp="CheckTextLength(this,10)" onclick="return validateDate()" onkeypress="return numericDate(event);" CssClass="validate[required] TextBox" placeholder="DD/MM/YYYY" Width="150%"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTargetDate" TargetControlID="txtTargetDate">
                                    </ajaxToolkit:CalendarExtender>

                                </div>

                                <div style="text-align: left; width: 8%; float: left; margin-left: 100px;" class="label">
                                    Status :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 15%; float: left;">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="Droptextarea validate[required]" Width="100%"></asp:DropDownList>
                                </div>

                            </div>
                            <div style="padding-left: 150px">
                                <asp:RangeValidator ID="valDate" runat="server" ControlToValidate="txtTargetDate" MinimumValue="31/01/2020"
                                    MaximumValue="31/12/2100" ErrorMessage="Enter Valid Date in DD/MM/YYYY Formate" Type="Date" Display="Dynamic" ForeColor="#FF3300" />
                            </div>
                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Remarks :
                                </div>
                                <div style="text-align: left; width: 40%; float: left;">

                                    <asp:TextBox ID="txtRemarks" runat="server" onKeyUp="CheckTextLength(this,5000)" CssClass="TextArea" Width="150%" TextMode="MultiLine" Height="75px"></asp:TextBox>


                                </div>
                            </div>

                            <br />



                            <div style="width: 95%; text-align: right; margin-right: 100px;" class="divclasswithoutfloat">
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
