<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="ClassMaster.aspx.cs" Inherits="GEIMS.Client.UI.ClassMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
		function calendarShown(sender, args) {
			sender._popupBehavior._element.style.zIndex = 10005;
		}
		$(function () {
			$('#tab-panel').tabs();
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Class Master
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_Click">Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<script type="text/javascript">
				$(function () {
					$('#id_search').quicksearch('table#<%=gvClass.ClientID%> tbody tr');
                })
			</script>
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
					<div id="search">
						<input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
					</div>
					<br />
					<div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
						<asp:GridView ID="gvClass" runat="server" AutoGenerateColumns="False"
							BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
							Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvClass_RowCommand"
							OnPreRender="gvClass_OnPreRender">
							<FooterStyle BackColor="White" ForeColor="#333333" />
							<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
							<Columns>
								<asp:BoundField DataField="ClassName" HeaderText="Class Name">
									<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>
								<asp:BoundField DataField="Division" HeaderText="Division">
									<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>
								<%--<asp:BoundField DataField="Priority" HeaderText="Priority">
								 <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>--%>
								<asp:TemplateField HeaderText="Edit">
									<ItemTemplate>
										<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
											CommandName="Edit1" CommandArgument='<%# Eval("ClassMID")%>' Height="20px" Width="20px" />
									</ItemTemplate>
									<HeaderStyle HorizontalAlign="center" />
									<ItemStyle HorizontalAlign="center" Width="10%" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Delete">
									<ItemTemplate>
										<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
											CommandName="Delete1" CommandArgument='<%# Eval("ClassMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
				</div>
				<div id="tabs" runat="server">
					<div id="tab-panel" class="style-tabs" visible="true">
						<ul>
							<li><a id="tabClassDetails" href="#tabs-1">Class Details</a></li>
						</ul>
						<div id="tabs-1" style="padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
							<div style="width: 100%;">
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Section Name :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; width: 80%; float: left;">
										<asp:DropDownList runat="server" CssClass="TextBox" ID="ddlSection" Width="150px" />
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Class Name :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:TextBox ID="txtClassName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Approval No :
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:TextBox ID="txtApprovalNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										Approval Date :
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:TextBox ID="txtApprovalDate" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
										<ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtApprovalDate" TargetControlID="txtApprovalDate">
										</ajaxToolkit:CalendarExtender>
									</div>
								</div>
								<div style="width: 100%" class="divclasswithfloat">
									<div style="text-align: left; width: 20%; float: left;" class="label">
										No Of Period :<span style="color: red">*</span>
									</div>
									<div style="text-align: left; float: left; width: 80%;">
										<asp:TextBox ID="txtNoOfPeriod" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</div>
								</div>
								
								<div style="width: 100%" class="divclasswithfloat">
									<fieldset>
										<legend>Division</legend>
										<%--<div style="width: 100%;">--%>
										<%--<asp:Panel runat="server" ID="pnlDivision">--%>
										<div style="width: 100%" class="divclasswithfloat">
											<div style="text-align: left; width: 20%; float: left;" class="label">
												Division Name :<span style="color: red">*</span>
											</div>
											<div style="text-align: left; float: left; width: 80%;">
												<asp:TextBox ID="txtDivisionName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
												<%--<asp:RequiredFieldValidator runat="server" ID="rf1" ErrorMessage="Enter Division Name." ControlToValidate="txtDivisionName"
                                            SetFocusOnError="True" ValidationGroup="g1" Display="None"></asp:RequiredFieldValidator>--%>
												<asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="btn-blue-new" OnClick="btnAdd_OnClick" ValidationGroup="g1" />
												<%--<asp:ValidationSummary runat="server" ID="vs1" ShowMessageBox="True" ValidationGroup="g1" ShowSummary="False" />--%>
											</div>
										</div>
										<div class="clear"></div>
										<div style="width: 100%;" class="divclasswithoutfloat">
											<asp:GridView ID="gvDivision" runat="server" AutoGenerateColumns="False"
												BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
												Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvDivision_OnRowCommand">
												<FooterStyle BackColor="White" ForeColor="#333333" />
												<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
												<Columns>
													<asp:BoundField DataField="DivisionName" HeaderText="Division Name">
														<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
														<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
													</asp:BoundField>
													<asp:TemplateField HeaderText="Edit">
														<ItemTemplate>
															<asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
																CommandName="EditDivision" CommandArgument='<%# Eval("DivisionTID")%>' Height="20px" Width="20px" />
														</ItemTemplate>
														<HeaderStyle HorizontalAlign="center" />
														<ItemStyle HorizontalAlign="center" Width="10%" />
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Delete">
														<ItemTemplate>
															<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="../Images/delete-1.png"
																CommandName="DeleteDivision" CommandArgument='<%# Eval("DivisionTID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
																Height="20px" Width="20px" CssClass="Detach" />
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
										</div>
										<%--</asp:Panel>--%>
										<%--</div>--%>
									</fieldset>
								</div>
								<div style="width: 100%; text-align: right; padding: 0 10px 10px 0px" class="divclasswithoutfloat">
									<asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" />
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

		$(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
        	$(document.getElementById('<%= ddlSection.ClientID %>')).addClass("validate[required]");
        	$(document.getElementById('<%= txtClassName.ClientID %>')).addClass("validate[required]");
        	$(document.getElementById('<%= txtNoOfPeriod.ClientID %>')).addClass("validate[required,custom[onlyNumberSp]]");
        	$(document.getElementById('<%= txtDivisionName.ClientID %>')).removeClass("validate[required]");
        });

        $(document.getElementById('<%= btnAdd.ClientID %>')).click(function () {
			$(document.getElementById('<%= ddlSection.ClientID %>')).removeClass("validate[required]");
        	$(document.getElementById('<%= txtClassName.ClientID %>')).removeClass("validate[required]");
        	$(document.getElementById('<%= txtNoOfPeriod.ClientID %>')).removeClass("validate[required]");
        	$(document.getElementById('<%= txtDivisionName.ClientID %>')).addClass("validate[required]");
        });
	</script>
</asp:Content>
