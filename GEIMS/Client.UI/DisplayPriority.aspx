<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/SchoolMain.Master" CodeBehind="DisplayPriority.aspx.cs" Inherits="GEIMS.Client.UI.DisplayPriority" %>

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
			Priority Master	
			<%--<asp:LinkButton CausesValidation="false" ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium">Back</asp:LinkButton>--%>
		</div>
		<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
		<div id="divContent2" style="width: 80%; float: left; height: 100%;">
			<div class="divclasswithfloat">
				<div class="label">Select Category: </div>
				<asp:RadioButtonList ID="rblToggleView" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblToggleView_SelectedIndexChanged" OnTextChanged="rblToggleView_TextChanged"></asp:RadioButtonList>
			</div>

			<div class="divclasswithfloat">
				<div runat="server" id="divClass">
					<div style="text-align: left; width: 50%; float: left; padding: 2px;" class="label">
						<asp:GridView ID="gvClass" runat="server" AutoGenerateColumns="False"
							BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
							Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
							<FooterStyle BackColor="White" ForeColor="#333333" />
							<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
							<Columns>
								<asp:BoundField DataField="ClassMID" HeaderText="ClassMID">
									<HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" CssClass="hidden" />
									<ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
								</asp:BoundField>
								<asp:BoundField DataField="ClassName" HeaderText="Class">
									<HeaderStyle Width="85%" HorizontalAlign="Center" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="85%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>
								<asp:TemplateField HeaderText="Priority">
									<ItemTemplate>
										<asp:TextBox ID="txtSequence" runat="server" Width="100px" Text='<%# Eval("Priority") %>' CssClass="txtTotalAmount TextBox"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
							<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" HorizontalAlign="Right" ForeColor="White" />
							<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
							<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
							<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
						</asp:GridView>
					</div>
				</div>
			</div>
			<div class="divclasswithfloat">
				<div runat="server" id="divFeesCategory">
					<div style="text-align: left; width: 48%; float: left; padding: 2px;" class="label">
						<asp:GridView ID="gvFees" runat="server" AutoGenerateColumns="False"
							BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
							Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
							<FooterStyle BackColor="White" ForeColor="#333333" />
							<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
							<Columns>
								<asp:BoundField DataField="FeesCategoryMID" HeaderText="FeesCategoryMID">
									<HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" CssClass="hidden" />
									<ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
								</asp:BoundField>
								<asp:BoundField DataField="FeesName" HeaderText="Fees Name">
									<HeaderStyle Width="85%" HorizontalAlign="Center" VerticalAlign="Top" />
									<ItemStyle HorizontalAlign="left" Width="85%" VerticalAlign="Top" Wrap="true" />
								</asp:BoundField>
								<asp:TemplateField HeaderText="Priority">
									<ItemTemplate>
										<asp:TextBox ID="txtSequence1" runat="server" Width="100px" Text='<%# Eval("Priority") %>' CssClass="txtTotalAmount TextBox"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
							<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" HorizontalAlign="Right" ForeColor="White" />
							<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
							<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
							<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
						</asp:GridView>
					</div>
				</div>
			</div>
			<div style="width: 100%; text-align: right; padding: 0 10px 10px 0px" class="divclasswithoutfloat">
				<asp:Button runat="server" ID="btnGo" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnGo_Click" />
			</div>
		</div>
	</div>
	<script type="text/javascript">
		$(document).ready(function() {
			var gvIncomeRowsCount = $('#<%=gvClass.ClientID %> tr').length;

			var oldVal;

			$('#<%=gvClass.ClientID %>').find("input:text").focus(function() {
			    oldVal = $(this).val();
			    //alert(oldVal);
			});
		    //$(document).keydown(function(objEvent) {
		    //    if (objEvent.keyCode == 9) { //tab pressed
		    //        objEvent.preventDefault(); // stops its action
		    //    }
		    //});
			$('[id$=txtSequence]').change(function() {
				var i = 0;
				var newVal = $(this).val();

				var row = this.parentNode.parentNode;
				var rowIndex = row.rowIndex;
				//alert('RI' + rowIndex);
			    //alert('nv' + newVal);
			    //
				if (newVal != '' && newVal != 0 && newVal != null && newVal < gvIncomeRowsCount && newVal > 0) {
					$('#<%=gvClass.ClientID %> tr').each(function() {
						if (i != 0 && i != rowIndex) {
							//alert('i' + i);
							var orignalVal = $(this).find('input:text').val();
							
							//alert(newVal);
							//alert('orgval' + orignalVal);
							//if () {
							if (newVal == orignalVal) {
								$(this).find('input:text').val(oldVal);
							}
							//}
							//oldVal = $(this).find('input:text').val();
						}

						i = i + 1;
					});
					$('#<%=btnGo.ClientID %>').prop("disabled", false);
				} else {
				    $(this).closest("tr").find($("[id*=txtSequence]")).val(oldVal);
				    alert('Please enter correct sequence no.');
					$('#<%=btnGo.ClientID %>').prop("disabled", true);
				}
			});
		});
			$(document).ready(function() {
				var gvIncomeRowsCount = $('#<%=gvFees.ClientID %> tr').length;
				var oldValExp;
				$('#<%=gvFees.ClientID %>').find("input:text").focus(function() {
					oldValExp = $(this).val();
				});
				$('[id$=txtSequence1]').change(function() {
					var iExp = 0;
					var newValExp = $(this).val();

					var rowExp = this.parentNode.parentNode;
					var rowIndexExp = rowExp.rowIndex;
					//alert('RI' + rowIndex);
					//alert('nv' + newVal);
					if (newValExp != '' && newValExp != 0 && newValExp != null && newValExp < gvIncomeRowsCount && newValExp > 0) {
						$('#<%=gvFees.ClientID %> tr').each(function() {

							if (iExp != 0 && iExp != rowIndexExp) {
								//alert('i' + i);
								var orignalValExp = $(this).find('input:text').val();
								//alert(oldVal);
								//alert(newVal);
								//alert('orgval' + orignalVal);
								//if () {
								if (newValExp == orignalValExp) {
									$(this).find('input:text').val(oldValExp);
								}
								//}
								//oldVal = $(this).find('input:text').val();
							}

							iExp = iExp + 1;
						});
						$('#<%=btnGo.ClientID %>').prop("disabled", false);
					} else {
					    $(this).closest("tr").find($("[id*=txtSequence1]")).val(oldValExp);
						alert('Please enter correct sequence no.');
						$('#<%=btnGo.ClientID %>').prop("disabled", true);
					}
				});
			});
	
	</script>
</asp:Content>
