<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="Holiday.aspx.cs" Inherits="GEIMS.Client.UI.Holiday" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvHoliday.ClientID%> tbody tr');
        })
		function calendarShown(sender, args) {
		    sender._popupBehavior._element.style.zIndex = 10005;
		}
        $(function () {
            $('#tab-panel').tabs();
        });
    </script>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Holiday Master
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px;padding-right:10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div><br/>
                    <asp:GridView ID="gvHoliday" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvHoliday_OnRowCommand"
                        OnPreRender="gvHoliday_OnPreRender">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Holiday Name">
                                <HeaderStyle Width="80%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("HolidayID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("HolidayID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                <div id="tabs" runat="server">
					<div id="tab-panel" class="style-tabs" visible="true">
						<ul>
							<li><a id="tabClassDetails" href="#tabs-1">Holiday Details</a></li>
						</ul>
						<div id="tabs-1" style="padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
							<div style="width: 100%;">
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Holiday Name :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; width: 80%; float: left;">
										<asp:TextBox ID="txtHolidayName" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Academic Year :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Width="150px" />
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										From Date :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:TextBox ID="txtFromDate" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
										</ajaxToolkit:CalendarExtender>
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										To Date :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:TextBox ID="txtToDate" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
										<ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate">
										</ajaxToolkit:CalendarExtender>
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Description :
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										 <asp:TextBox ID="txtDescription" runat="server" CssClass="validate[required] TextArea" Width="50%"  TextMode="MultiLine"></asp:TextBox>
									</div>
								</div>
								
								
								<div style="width: 100%; text-align: right; padding: 0 10px 10px 0px" class="divclasswithoutfloat">
									<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_OnClick" />
								</div>
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
        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
</asp:Content>
