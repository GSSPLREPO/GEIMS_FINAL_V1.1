<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SerialNo.aspx.cs" Inherits="GEIMS.Accounting.SerialNo" %>
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
			$('#id_search').quicksearch('table#<%=gvSerialNo.ClientID%> tbody tr');
		})
	</script>
	<div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Serial No
			 <asp:LinkButton CausesValidation="false" ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnAddNew_Click">Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnViewList_Click">View List</asp:LinkButton>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div id="Div1" align="center">
					<asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
				</div>
				<div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
					<div id="search">
						<input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
					</div>
					<br />
					<asp:GridView ID="gvSerialNo" runat="server" AutoGenerateColumns="False"
						BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
						Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnPreRender="gvSerialNo_PreRender" OnRowCommand="gvSerialNo_RowCommand">
						<FooterStyle BackColor="White" ForeColor="#333333" />
						<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
						<Columns>
							<%--<asp:BoundField DataField="TrustNameEng" HeaderText="Trust Name">
								<HeaderStyle Width="35%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="35%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="SchoolNameEng" HeaderText="School Name">
								<HeaderStyle Width="35%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="35%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>--%>
							<asp:BoundField DataField="EntryType" HeaderText="Type">
								<HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="60%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="Year" HeaderText="Year">
								<HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="StartNo" HeaderText="Start No">
								<HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="Edit">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
										CommandName="Edit1" CommandArgument='<%# Eval("Id")%>' Height="20px" Width="20px" />
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="center" />
								<ItemStyle HorizontalAlign="center" Width="10%" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Delete">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
										CommandName="Delete1" CommandArgument='<%# Eval("Id")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
						<li><a id="tabStudentTemplateDetails" href="#tabs-1">Serial No</a></li>
					</ul>
					<div id="tabs-1" class="gradientBoxesWithOuterShadows">
						<%--<fieldset>--%>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Trust : <span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlTrust" runat="server" CssClass="validate[required] Droptextarea" Width="500px" OnSelectedIndexChanged="ddlTrust_SelectedIndexChanged" Enabled="False">
									<asp:ListItem Value="">--Select--</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								School : 
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlSchool" runat="server" CssClass="Droptextarea" Width="500px" Enabled="False">
									<asp:ListItem Value="">--Select--</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Type : <span class="Required">*</span>
				
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlType" runat="server" CssClass="validate[required] Droptextarea" >
									<asp:ListItem Value="">--Select--</asp:ListItem>
									<asp:ListItem Value="Journal">Journal</asp:ListItem>
									<asp:ListItem Value="Contra">Contra</asp:ListItem>
									<asp:ListItem Value="Receipt">Receipt</asp:ListItem>
									<asp:ListItem Value="Payment">Payment</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Year : class="Required">*</span>
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" >
									<asp:ListItem Value="">--Select--</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Start No :<span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:TextBox runat="server" ID="txtStartNo" CssClass="validate[required] TextBox" Width="190px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)"></asp:TextBox>
							</div>
						</div>
						<div class="divclasswithoutfloat">
							<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
						</div>
						<%--</fieldset>--%>
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
