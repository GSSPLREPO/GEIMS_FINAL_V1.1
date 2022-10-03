<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="GeneralLedger.aspx.cs" Inherits="GEIMS.Accounting.GeneralLedger" %>

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
	        $('#id_search').quicksearch('table#<%=gvGeneralLedger.ClientID%> tbody tr');
		})
	</script>
	<div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			General Ledger List
			 <asp:LinkButton CausesValidation="false" ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnAddNew_Click" >Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnViewList_Click">View List</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="btnGoBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Visible="False" OnClick="btnGoBack_OnClick" >Go Back to Previous Screen</asp:LinkButton>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div id="Div1" align="center">
					<asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
				</div>
				<div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
					<div id="search">
						<input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
					</div>
					<br />
					<asp:GridView ID="gvGeneralLedger" runat="server" AutoGenerateColumns="False"
						BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" 
						Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvGeneralLedger_RowCommand" >
						<FooterStyle BackColor="White" ForeColor="#333333" />
						<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
						<Columns>
							<asp:BoundField DataField="AccountName" HeaderText="Account Name">
								<HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="AccountGroupName" HeaderText="Account Group name">
								<HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance">
								<HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="Right" Width="15%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:BoundField DataField="BalanceType" HeaderText="Balance Type">
								<HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="Edit">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
										CommandName="Edit1" CommandArgument='<%# Eval("LedgerID")%>' Height="20px" Width="20px" />
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="center" />
								<ItemStyle HorizontalAlign="center" Width="10%" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Delete">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
										CommandName="Delete1" CommandArgument='<%# Eval("LedgerID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
						<li><a id="tabStudentTemplateDetails" href="#tabs-1">General Ledger</a></li>
					</ul>
					<div id="tabs-1" style="padding:0 0 0 10px" class="gradientBoxesWithOuterShadows">
						<%--<fieldset>--%>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 25%; float: left;" class="label">
								Account Name :<span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 30%; float: left;">
								<asp:TextBox runat="server" ID="txtAccountName" CssClass="validate[required] TextBox" Width="190px"></asp:TextBox>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 25%; float: left;" class="label">
								Account Group Name :<span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 30%; float: left;">
								<asp:DropDownList ID="ddlAccountGroup" runat="server" CssClass="validate[required] Droptextarea">
									<asp:ListItem Value="">--Select--</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 25%; float: left;" class="label">
								Opening Balance :<span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 30%; float: left;">
								<asp:TextBox runat="server" ID="txtOpeningBalance" CssClass="validate[required] TextBox" Width="190px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;"></asp:TextBox>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 25%; float: left;" class="label">
								Balance Type :<span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 30%; float: left;">
								<asp:DropDownList ID="ddlBalanceType" runat="server" CssClass="validate[required] Droptextarea">
									<asp:ListItem Value="">--Select--</asp:ListItem>
									<asp:ListItem Value="Debit">Debit</asp:ListItem>
									<asp:ListItem Value="Credit">Credit</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 25%; float: left;" class="label">
								Description :
							</div>
							<div style="text-align: left; width: 30%; float: left;">
								<asp:TextBox runat="server" ID="txtDescription" CssClass="TextArea" TextMode="MultiLine" Rows="3"></asp:TextBox>
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

