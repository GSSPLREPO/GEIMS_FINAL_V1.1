<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="ClassTemplate.aspx.cs" Inherits="GEIMS.Client.UI.ClassTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
	<link href="../CSS/screen.css" rel="stylesheet" />
	<script src="../JS/ValidationEngine.js"></script>
	<script src="../JS/ValidationEngine-en.js"></script>
	<link href="../CSS/ValidationEngine.css" rel="stylesheet" />
	<script type="text/javascript">


		function calendarShown(sender, args) {
			sender._popupBehavior._element.style.zIndex = 10005;
		}
		$(function () {
			$(document.getElementById('<%= tabs.ClientID %>')).tabs();
		});

	</script>
	<style type="text/css">
		.myTable {
			border-collapse: collapse;
		}

			.myTable th {
				background-color: #3b5998;
				color: white;
				width: 50%;
			}

			.myTable td, .myTable th {
				padding: 5px;
				border: 1px solid #000;
			}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Class Template
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div style="text-align: center; width: 100%;">
					<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>
				</div>
				<%--<div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
					<asp:GridView ID="gvSection" runat="server" AutoGenerateColumns="False"
						BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
						Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
						<FooterStyle BackColor="White" ForeColor="#333333" />
						<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
						<Columns>
							<asp:BoundField DataField="SectionName" HeaderText="Section Name">
								<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="Edit">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
										CommandName="Edit1" CommandArgument='<%# Eval("SectionMID")%>' Height="20px" Width="20px" />
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="center" />
								<ItemStyle HorizontalAlign="center" Width="10%" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Delete">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
										CommandName="Delete" CommandArgument='<%# Eval("SectionMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
				</div>--%>
				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
					<ul>
						<li><a id="tabClassTemplateDetails" href="#tabs-1">Class Template Details</a></li>

					</ul>
					<div id="tabs-1" style="height: 245px;" class="gradientBoxesWithOuterShadows">

						<div style="height: 30px; margin-top: 10px; width: 100%;">
							<div style="text-align: left; width: 21%; float: left;" class="label">
								Class :<span style="color: red">*</span>
							</div>
							<div style="text-align: left; width: 29%; float: left;">
								<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlClass" Width="150px">
									<asp:ListItem Value="">Select Class</asp:ListItem>
									<asp:ListItem>1</asp:ListItem>
									<asp:ListItem>2</asp:ListItem>
									<asp:ListItem>3</asp:ListItem>
									<asp:ListItem>4</asp:ListItem>
									<asp:ListItem>5</asp:ListItem>
									<asp:ListItem>6</asp:ListItem>

								</asp:DropDownList>
							</div>
							<div style="text-align: left; width: 21%; float: left;" class="label">
								Division :<span style="color: red">*</span>
							</div>
							<div style="text-align: left; width: 29%; float: left; vertical-align: top;">
								<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDivision" Width="150px">
									<asp:ListItem Value="">Select All</asp:ListItem>
									<asp:ListItem>A</asp:ListItem>
									<asp:ListItem>B</asp:ListItem>
									<asp:ListItem>C</asp:ListItem>
									<asp:ListItem>D</asp:ListItem>

								</asp:DropDownList>
							</div>
						</div>

						<div style="margin-top: 10px; width: 100%;">
							<table class="myTable" style="width:100%">
								<tr>
									<th style="width:20%">Choose Category
									</th>
									<th style="width:40%">Fees Category Name
									</th>
									<th style="width:40%">Amount(In Rs.)
									</th>
								</tr>
								<tr>
									<td>
										<asp:CheckBox runat="server" ID="chk1" />
									</td>
									<td>Fee Category - January
									</td>
									<td>
										<asp:TextBox ID="txtCurAdmissionDate" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:CheckBox runat="server" ID="CheckBox1" />
									</td>
									<td>Fee Category - February
									</td>
									<td>
										<asp:TextBox ID="TextBox1" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:CheckBox runat="server" ID="CheckBox2" />
									</td>
									<td>Fee Category - March
									</td>
									<td>
										<asp:TextBox ID="TextBox2" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td>
										<asp:CheckBox runat="server" ID="CheckBox3" />
									</td>
									<td>Fee Category - April
									</td>
									<td>
										<asp:TextBox ID="TextBox3" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
									</td>
								</tr>
							</table>



						</div>
						<div style="height: 30px; margin-top: 10px; float: right; width: 100%;">
							&nbsp;&nbsp;
							<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancel_Click" />
					&nbsp;&nbsp;
					<asp:Button runat="server" ID="btnSaveClassTemplate" Text="Save" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnSaveClassTemplate_Click" />
						</div>
					</div>
				</div>
				<div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
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

	 	$(document.getElementById('<%= btnSaveClassTemplate.ClientID %>')).click(function () {
        	var valid = $("#aspnetForm").validationEngine('validate');
        	var vars = $("#aspnetForm").serialize();

        	
        });

    </script>
</asp:Content>
