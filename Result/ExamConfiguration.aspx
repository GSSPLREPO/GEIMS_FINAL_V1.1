<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="ExamConfiguration.aspx.cs" Inherits="GEIMS.Result.Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">

		$(function () {
			$(document.getElementById('<%= tabs.ClientID %>')).tabs();
		});

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<script type="text/javascript">
		$(function () {
			$('#id_search').quicksearch('table#<%=gvExamConfig.ClientID%> tbody tr');
 	})
	</script>
	<div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Exam Configuration
			 <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click1">View List</asp:LinkButton>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">

				<div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
					<div id="search">
						<input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
					</div>
					<br />
					<asp:GridView ID="gvExamConfig" runat="server" AutoGenerateColumns="False"
						BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvExamConfig_RowCommand" OnPreRender="gvExamConfig_OnPreRender">
						<FooterStyle BackColor="White" ForeColor="#333333" />
						<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
						<Columns>
							<asp:BoundField DataField="ExamConfigID" HeaderText="ID">
								<HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
								<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
							</asp:BoundField>
							<asp:BoundField DataField="ClassName" HeaderText="Class">
								<HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="DivisionName" HeaderText="Division">
								<HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="Exam" HeaderText="Exam Name">
								<HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="AcademicYear" HeaderText="Year">
								<HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
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
				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
					<ul>
						<li><a id="tabClassDetails" href="#tabs-1">Exam Configuration</a></li>
					</ul>
					<div id="tabs-1" style="height: 395px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
						<div style="width: 100%;">
							<div class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Class :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: left;">
									<asp:DropDownList runat="server" ID="ddlClass" CssClass="validate[required] Droptextarea" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
									</asp:DropDownList>
								</div>
							</div>
							<div class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Division:<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: right; vertical-align: top;">
									<asp:ListBox ID="lbDivision" runat="server" Height="70px" SelectionMode="Multiple" Width="150px"></asp:ListBox>
								</div>
							</div>
							<div class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Academic Year :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: left;">
									<asp:DropDownList runat="server" ID="ddlAcademicYear" CssClass="Droptextarea">
									</asp:DropDownList>
								</div>
							</div>
							<div class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Exam :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: left;">
									<asp:DropDownList runat="server" ID="ddlExam" CssClass="validate[required] Droptextarea">
										<asp:ListItem Value="">--Select--</asp:ListItem>
										<asp:ListItem Value="FA1">FA1</asp:ListItem>
										<asp:ListItem Value="FA2">FA2</asp:ListItem>
										<asp:ListItem Value="SA1">SA1</asp:ListItem>
										<asp:ListItem Value="INT-1">INT-1</asp:ListItem>
										<asp:ListItem Value="FA3">FA3</asp:ListItem>
										<asp:ListItem Value="FA4">FA4</asp:ListItem>
										<asp:ListItem Value="SA2">SA2</asp:ListItem>
										<asp:ListItem Value="INT-2">INT-2</asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>
							<div class="divclasswithfloat">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									Subject :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; width: 81%; float: left;">
										<table>
											<tr>
												<td>
													<asp:ListBox ID="lbSrcSubject" runat="server" Height="100px" SelectionMode="Multiple" Width="150px" CausesValidation="False"></asp:ListBox></td>
												<td>
													<td>
														<asp:Button runat="server" ID="btnAdd" Text=">" CssClass="label Detach" OnClick="btnAdd_Click"></asp:Button>
													</td>
													<td>
														<asp:Button runat="server" ID="btnRemove" Text="<" CssClass="label Detach" OnClick="btnRemove_Click"></asp:Button>
													</td>

												</td>
												<td>
													<asp:ListBox ID="lbDestSubject" runat="server" Height="100px" SelectionMode="Multiple" Width="150px" CssClass="validate[required]"></asp:ListBox></td>
												<%--<td>
													<asp:Button runat="server" ID="btnUp" Text="Up" CssClass="button" OnClick="btnUp_Click"></asp:Button>
												</td>
												<td>
													<asp:Button runat="server" ID="btnDown" Text="Down" CssClass="button" OnClick="btnDown_Click"></asp:Button>
												</td>--%>
											</tr>
										</table>
								</div>
							</div>
							<div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
								<asp:Button runat="server" ID="btnsave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click"/>
							</div>
						</div>
						<%--<div id="divExamDetails" runat="server" class="divclasswithOutfloat" style="height: 100%;">
							<div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
								

							</div>
							<div style="height: 70px; margin-top: 10px; float: left; width: 100%;">
								
							</div>
							<div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
								
							</div>
							<div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
								
							</div>
							<div style="height: 70px; margin-top: 10px; float: left; width: 100%;">
								<asp:Label runat="server" Text="" ID="lbltxt"></asp:Label>
							</div>
							<div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
								<div style="text-align: left; width: 19%; float: left;" class="label">
									
								</div>
							</div>
						</div>
						<div style="width: 100%; float: left; padding-top: 0px;" class="label">
							<div style="padding: 10px; padding-right: 30px;">
								<div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;" class="divclasswithOutfloat">
									
									
								</div>
								<div id="divExamConfigGrid" runat="server" class="divclasswithfloat" style="height: 100%;">
									<div style="width: 100%;" class="divclasswithfloat">
										
									</div>
								</div>
							</div>
						</div>--%>
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


		$('.Detach').click(function () {
			$("#aspnetForm").validationEngine('detach');
		});

		function BindClass() {

			bindClass();

		}

		
		$('.lbSrcSubject option').each(function(index) {
			if  ($(this).is(':selected')) {
				$("#<%=btnAdd.ClientID%>").enable = true;

			}
			else {
				$("#<%=btnAdd.ClientID%>").enable = false;
			}
		});

		$(document).ready(function () {
			var selected = $("#<%=lbSrcSubject.ClientID%>").find(':selected').text();
			
			//alert(selected);
			if (selected != '' ) {
			
				$("#<%=btnAdd.ClientID%>").prop('disabled', false);
				
			}
			else{
				$("#<%=btnAdd.ClientID%>").prop('disabled', true);
			}

			var selected1 = $("#<%=lbDestSubject.ClientID%>").find(':selected').text();
			//alert('PreDest');
			if (selected1 == NaN || selected1 == '') {
				//alert('Dest');
				$("#<%=btnRemove.ClientID%>").prop('disabled', true);
			}
			else {
				$("#<%=btnRemove.ClientID%>").prop('disabled', false);
			}
		});

		$("#<%=lbSrcSubject.ClientID%>").click(function () {
			var selected = $("#<%=lbSrcSubject.ClientID%>").find(':selected').text();
			if (selected != '') {
				$("#<%=btnAdd.ClientID%>").prop('disabled', false);
			}
			
		});
		$("#<%=lbDestSubject.ClientID%>").click(function () {
			var selected1 = $("#<%=lbDestSubject.ClientID%>").find(':selected').text();
			if (selected1 != '') {
				$("#<%=btnRemove.ClientID%>").prop('disabled', false);
			}
			
		});

		//$("[id*=btnsave]").click(function () {
		//var a = $("#<%=lbDestSubject.ClientID%>").val();
		//var b = $("#<%=ddlClass.ClientID%>").val();
		//alert(a);
		//	if ($('#lbDestSubject option').val() == null) {
		//	}

		//	if ($("#<%=ddlClass.ClientID%>").val() == "") {
		//	alert('Please select Class.');
		//		return false;
		//	}
		//	else if ($("#<%=ddlExam.ClientID%>").val() == -1) {
		//alert($("#<%=lbDestSubject.ClientID%> option").length);
		//	alert('Please select item from Exam.');
		//	return false;
		//}
		//else if ($("#<%=lbDestSubject.ClientID%> option").length == 0) {
		// user has selected at least one value
		//alert('Please select item from Subject list.');
		//return false;
		//}
		//});

	</script>
</asp:Content>
