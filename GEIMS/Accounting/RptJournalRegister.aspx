<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="RptJournalRegister.aspx.cs" Inherits="GEIMS.Accounting.RptJournalRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
	</script>
	<script>
	    $(function () {
	        $(document.getElementById('<%= tabs.ClientID %>')).tabs();
	    });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvJournalRegisterReport.ClientID%> tbody tr');
        })
	</script>
	<div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Journal Register
			<asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="../Images/excel.PNG" Width="25px" Height="25px"
							CssClass="btn-blue" ToolTip="Export to Excel" OnClick="btnExportExcel_Click"  />
			&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
			<asp:LinkButton CausesValidation="false" ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnBack_Click" >Back</asp:LinkButton>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div id="Div1" align="center">
					<asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
				</div>
				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%; height: 100%; float: left;">
					<ul>
						<li><a id="tabStudentTemplateDetails" href="#tabs-1">Report</a></li>
					</ul>
					<div id="tabs-1" class="gradientBoxesWithOuterShadows">
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Date : <span class="Required">*</span>
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
								&nbsp;
								<asp:TextBox ID="txtToDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
							</div>
                            <div style="text-align: left; width: 15%; float: left;" class="label">
								Narration : 
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:CheckBox ID="chkNarration" runat="server"></asp:CheckBox>
							</div>
						</div>
						
						<div class="divclasswithoutfloat">
							<asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new btn-blue-medium" Text="Get Report" OnClick="btnGo_Click" />
						</div>
					</div>
				</div>

				<div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
					<div id="search">
						<input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
					</div>
					<br />
					<div id="Div2">
						<asp:Label ID="lblHeading" runat="server" CssClass="label"></asp:Label>
					</div>
					<div class="divclasswithfloat">
						<div style="text-align: left; width: 15%; float: left;" class="label">
							<%--<b>Opening Balance : </b>--%>
						</div>
						<div style="text-align: right; width: 85%; float: right;">
							<%--<asp:Label runat="server" ID="lblOpening" CssClass="label"></asp:Label>--%>
						</div>
					</div>
					<div class="divclasswithfloat">
						<div style="text-align: left; width: 1070px; float: left;">
							<asp:GridView ID="gvJournalRegisterReport" runat="server" AutoGenerateColumns="False" OnPreRender="gvJournalRegisterReport_OnPreRender"
								BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" ShowFooter="False"
								Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowDataBound="gvJournalRegisterReport_RowDataBound">
								<FooterStyle BackColor="White" ForeColor="#333333" />
								<RowStyle BackColor="White" Height="100%" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Top" />
								<Columns>
									<asp:BoundField DataField="OperationDate" HeaderText="Date">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
                                    <asp:BoundField DataField="LedgerName" HeaderText="Particulars" HtmlEncode="False">
										<HeaderStyle Width="40%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
									<asp:BoundField DataField="VoucherType" HeaderText="Voucher Type">
										<HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
									<asp:BoundField DataField="VoucherNo" HeaderText="Voucher No">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
									<asp:BoundField DataField="Debit" HeaderText="Debit" HtmlEncode="False">
										<HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="Right" Width="10%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
									<asp:BoundField DataField="Credit" HeaderText="Credit" HtmlEncode="False">
										<HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="Right" Width="10%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
									<%--<asp:BoundField DataField="Description" HeaderText="Narration" >
								<HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>--%>
								</Columns>
								<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" HorizontalAlign="Right" ForeColor="White"/>
								<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
								<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
								<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
							</asp:GridView>
						</div>
					</div>

					<br />
					<div class="divclasswithfloat">
						<div style="text-align: left; width: 15%; float: left;" class="label">
							<%--<b>Closing Balance : </b>--%>
						</div>
						<div style="text-align: right; width: 85%; float: right;">
							<%--<asp:Label runat="server" ID="lblClosing" CssClass="label"></asp:Label>--%>
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
	    $(document.getElementById('<%= btnGo.ClientID %>')).click(function () {
	        var valid = $("#aspnetForm").validationEngine('validate');
	        var vars = $("#aspnetForm").serialize();
	    });
	</script>
	<ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
		Format="dd/MM/yyyy" TargetControlID="txtFromDate">
	</ajaxToolkit:CalendarExtender>
	<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
		Format="dd/MM/yyyy" TargetControlID="txtToDate">
	</ajaxToolkit:CalendarExtender>
</asp:Content>
