<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentExamMarks.aspx.cs" Inherits="GEIMS.Result.StudentExamMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Student Marks
            
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div style="text-align: center; width: 100%;">
				</div>

				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

					<div id="tabs1" style="min-height: 150px;" runat="server">

						<asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="Exam Details">
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Class :<span style="color: red">*</span>
										<asp:HiddenField runat="server" ID="hfExamConfigId" />
									</div>
									<div style="float: left; width: 35%;">
										<asp:DropDownList ID="ddlClass" runat="server" CssClass="validate[required] Droptextarea" Width="260px" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged1" AutoPostBack="True">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Division :<span style="color: red">*</span>
									</div>
									<div style="float: left; width: 185px;">
										<asp:DropDownList ID="ddlDivision" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Academic Year :<span style="color: red">*</span>
									</div>
									<div style="float: left; width: 185px;">
										<asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Exam Name :<span style="color: red">*</span>
									</div>
									<div style="float: left; width: 185px;">
										<asp:DropDownList runat="server" ID="ddlExam" CssClass="Droptextarea" Width="260px" AutoPostBack="True" EnableViewState="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
											<asp:ListItem>--Select--</asp:ListItem>
											<asp:ListItem>FA1</asp:ListItem>
											<asp:ListItem>FA2</asp:ListItem>
											<asp:ListItem>SA1</asp:ListItem>
											<asp:ListItem>INT-1</asp:ListItem>
											<asp:ListItem>FA3</asp:ListItem>
											<asp:ListItem>FA4</asp:ListItem>
											<asp:ListItem>SA2</asp:ListItem>
											<asp:ListItem>INT-2</asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Subject Name :<span style="color: red">*</span>
									</div>
									<div style="float: left; width: 185px;">
										<asp:DropDownList ID="ddlSubject" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Total Marks :<span style="color: red">*</span>
									</div>
									<div style="float: left; width: 185px;">
										<asp:TextBox ID="txtTotalMarks" runat="server" CssClass="validate[required] TextBox validate[custom[onlyNumberSp]]" Width="60px">
										</asp:TextBox>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left;" class="label">
								<div style="padding: 10px;">
									<div style="float: left; width: 15%;">
										Passing Marks :<span style="color: red">*</span>
									</div>
									<div style="float: left; width: 185px;">
										<asp:TextBox ID="txtPassingMarks" runat="server" CssClass="validate[required] TextBox validate[custom[onlyNumberSp]]" Width="60px">
										</asp:TextBox>
									</div>
								</div>
							</div>
							<div style="width: 100%; float: left; padding-top: 0px;" class="label">
								<div style="padding: 10px; padding-right: 30px;">
									<div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
										<asp:Button runat="server" ID="btnView" Text="View" CssClass="btn-blue-new btn-blue-medium detach" OnClick="btnView_Click" />
									</div>
								</div>
							</div>
						</asp:Panel>
					</div>
					<div id="divButtons" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
						<div style="padding: 10px; padding-right: 30px;">
							<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
							</div>
						</div>
					</div>
					<div id="divGrid" runat="server" style="padding-top: 10px;">
						<asp:GridView ID="gvStudentMarks" runat="server" AutoGenerateColumns="False"
							BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
							Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowDataBound="gvStudentMarks_RowDataBound">
							<FooterStyle BackColor="White" ForeColor="#333333" />
							<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
							<Columns>
								<asp:BoundField DataField="SrNo" HeaderText="SR.No">
									<HeaderStyle Width="50px" HorizontalAlign="left" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
									<FooterStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true"/>
								</asp:BoundField>
								<asp:BoundField DataField="Id" HeaderText="StudentMID">
									<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
									<ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
									<FooterStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
								</asp:BoundField>
								<%--<asp:TemplateField>
								<HeaderTemplate>
									<asp:CheckBox ID="chkHeader" runat="server" />
								</HeaderTemplate>
								<ItemTemplate>
									<input type="checkbox" id="chkChild" />
									<asp:CheckBox ID="chkChild" runat="server" />
								</ItemTemplate>
							</asp:TemplateField>--%>
								<asp:BoundField DataField="Name" HeaderText="Student Name">
									<HeaderStyle Width="50px" HorizontalAlign="left" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>

								<asp:TemplateField HeaderText="Total Marks">
									<ItemTemplate>
										<asp:TextBox ID="txtTotal" runat="server" Width="100px" CssClass="txttotal TextBox validate[custom[onlyNumberSp]]"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Passing Marks">
									<ItemTemplate>
										<asp:TextBox ID="txtPassing" runat="server" Width="100px" CssClass="txttassing TextBox validate[custom[onlyNumberSp]]"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Obtained Marks">
									<ItemTemplate>
										<asp:TextBox ID="txtObtained" runat="server" Width="100px" CssClass="txtObtained TextBox validate[required] validate[custom[onlyNumberSp]]"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<%--<asp:BoundField HeaderText="Subject">
									<HeaderStyle Width="50px" HorizontalAlign="left" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>--%>
							</Columns>
							<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
							<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
							<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
							<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
						</asp:GridView>
					</div>
					<div style="width: 100%; float: left; padding-top: 0px;" class="label">
						<div style="padding: 10px; padding-right: 0px;">
							<div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
								<asp:Button runat="server" ID="btnsave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach" ValidationGroup="v1" OnClick="btnsave_Click" />
							</div>
						</div>
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

		function FillMarks() {
			//alert("aa");
			var a = $("#<%=txtTotalMarks.ClientID%>").val();
			//alert(a);
			var b = $("#<%=txtPassingMarks.ClientID%>").val();
			//alert(b);
			$("#<%=gvStudentMarks.ClientID%> [id*=txtTotal]").each(function () {
				$(this).val(a);
			});
			$("#<%=gvStudentMarks.ClientID%> [id*=txtPassing]").each(function () {
				$(this).val(b);
			});
		}

		$("#<%=btnView.ClientID%>").click(function (e) {

			var txtTotal = $("#<%=txtTotalMarks.ClientID%>").val();
			var txtPassing = $("#<%=txtPassingMarks.ClientID%>").val();
			var total = parseInt(txtTotal);
			var passing = parseInt(txtPassing);

			if (total != NaN || passing != NaN) {
				if (total < passing) {
					alert("Passing Marks Must less than Total marks.");
					e.preventDefault();
				}
			}
		});

	

			
		$("#<%=gvStudentMarks.ClientID%> [id*=txtObtained]").change(function () {
			var txtTotal = $("#<%=txtTotalMarks.ClientID%>").val();
			var total = parseInt(txtTotal);
			var obtained = parseInt($(this).val());
			if (obtained > total) {
					alert("Obtained marks must be less than Total Marks.");
				$(this).val('');
			}
			});

		
		$(document).ready(function () {
			

		});
	</script>
</asp:Content>
