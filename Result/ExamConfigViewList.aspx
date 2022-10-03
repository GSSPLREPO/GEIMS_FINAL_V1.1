<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/SchoolMain.Master" CodeBehind="ExamConfigViewList.aspx.cs" Inherits="GEIMS.Result.ExamConfigViewList" %>

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
	<%--<asp:UpdatePanel ID="upResult" UpdateMode="Conditional" runat="server">--%>
	<div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			EXAM CONFIGURATION
          <%--  <asp:LinkButton CausesValidation="false" ID="lnkAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div style="text-align: center; width: 100%;">
					<%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
				</div>
				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
					<ul>
						<li><a id="tabClassDetails" href="#tabs-1">Exam Configuration</a></li>

					</ul>
					<div id="tabs-1" style="height: 570px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
						
						
						<div style="width: 100%; float: left; padding-top: 0px;" class="label">
							<div style="padding: 10px; padding-right: 30px;">
								<div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
									
									<asp:Button runat="server" ID="btnExam" Text="Add Exam" CssClass="btn-blue-new btn-blue-medium Attach"  ValidationGroup="v1" OnClick="btnExam_Click" />

								</div>
								
								<div style="width: 100%;" class="divclasswithfloat">
									<asp:GridView ID="gvExamConfig" runat="server" AutoGenerateColumns="False"
										BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvExamConfig_RowCommand">
										<FooterStyle BackColor="White" ForeColor="#333333" />
										<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
										<Columns>
											<asp:BoundField DataField="ExamConfigID" HeaderText="ID">
												<HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
												<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
											</asp:BoundField>
											<asp:BoundField DataField="ClassName" HeaderText="Class">
												<HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
												<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
											</asp:BoundField>
											<asp:BoundField DataField="DivisionName" HeaderText="Division">
												<HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
												<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
											</asp:BoundField>
											<asp:BoundField DataField="Exam" HeaderText="Exam Name">
												<HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
												<ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
											</asp:BoundField>
											<asp:BoundField DataField="AcademicYear" HeaderText="Year">
												<HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
												<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="Edit">
												<ItemTemplate>
													<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
														CommandName="Edit1" CommandArgument='<%# Eval("ExamConfigID")%>' CssClass="Detach" Height="20px" Width="20px" />
												</ItemTemplate>
												<HeaderStyle HorizontalAlign="center" />
												<ItemStyle HorizontalAlign="center" Width="10%" />
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Delete">
												<ItemTemplate>
													<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
														CommandName="Delete1" CommandArgument='<%# Eval("ExamConfigID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
						</div>

					</div>
				</div>
				<div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
			</div>
		</div>
	</div>
	<%--</asp:UpdatePanel>--%>
	<script type="text/javascript">
		jQuery("#aspnetForm").validationEngine('attach', {
			promptPosition: "bottomRight",
			validationEventTrigger: "submit",
			validateNonVisibleFields: false,
			updatePromptsPosition: true
		});

		function BindClass() {

			bindClass();

		}
		$("[id*=btnsave]").click(function () {
			var a = $("#<%=lbDestSubject.ClientID%>").val();
			var b = $("#<%=ddlClass.ClientID%>").val();
			alert(b);
			if ($("#<%=ddlClass.ClientID%>").val() == "--Select--") {
				alert('Please select Class.');
				return false;
			}
			else if ($("#<%=ddlExam.ClientID%>").val() == "--Select--") {
				alert('Please select item from Exam.');
				return false;
			}
			else if ($("#<%=lbDestSubject.ClientID%>").val() == null) {
				// user has selected at least one value
				alert('Please select item from list.');
				return false;
			}
		});

	</script>
</asp:Content>