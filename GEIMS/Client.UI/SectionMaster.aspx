<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SectionMaster.aspx.cs" Inherits="GEIMS.Client.UI.SectionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
	<link href="../CSS/screen.css" rel="stylesheet" />
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
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Section Master
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewSection" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewSection_Click">Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div style="text-align: center; width: 100%;">
					<%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
				</div>
				<div style="text-align: center; padding-top: 10px; padding-bottom: 10px;padding-right:10px; width: 100%;">
					<asp:GridView ID="gvSection" runat="server" AutoGenerateColumns="False"
						BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
						Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvSection_RowCommand" >
						<FooterStyle BackColor="White" ForeColor="#333333" />
						<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
						<Columns>
							<asp:BoundField DataField="SectionName" HeaderText="Section Name">
								<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
								<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="Edit">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png" CssClass="Detach"
										CommandName="Edit1" CommandArgument='<%# Eval("SectionMID")%>' Height="20px" Width="20px" />
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="center" />
								<ItemStyle HorizontalAlign="center" Width="10%" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Delete">
								<ItemTemplate>
									<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png" CssClass="Detach"
										CommandName="Delete1" CommandArgument='<%# Eval("SectionMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
						<li><a id="tabSectionDetails" href="#tabs-1">Section Details</a></li>

					</ul>
					<div id="tabs-1" style="height: 185px;padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">

						<div style="width: 100%; float: left;">
							<%--<div style="height: 15%; margin-top: 10px; width: 100%;">
								<div style="text-align: left; width: 20%; float: left;" class="label">
									Name of School :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; float: left; width: 80%;">
									<asp:DropDownList runat="server" CssClass="TextBox" ID="ddlSchool" Width="50%">
										<asp:ListItem>Select School</asp:ListItem>
										<asp:ListItem>School1</asp:ListItem>
										<asp:ListItem>School2</asp:ListItem>
										<asp:ListItem>School3</asp:ListItem>
										<asp:ListItem>School4</asp:ListItem>
										<asp:ListItem>School5</asp:ListItem>
									</asp:DropDownList>
								</div>

							</div>--%>
						</div>
						<div style="width: 100%; float: left;">
							<div style="height: 30px; margin-top: 10px; width: 100%;">
								<div style="text-align: left; width: 20%; float: left;" class="label">
									Section Name :<span style="color: red">*</span>
								</div>
								<div style="text-align: left; float: left; width: 80%;">
									<asp:TextBox ID="txtSectionName" runat="server" CssClass="validate[required,custom[onlyLetterSp]] TextBox" Width="300px" Height="100%"></asp:TextBox>
								</div>
							</div>
                           	<div style="height: 30px; margin-top: 10px; width: 100%;">
                               <div style="text-align: left; width: 20%; float: left;" class="label">
                                   Section Abbrev. :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 80%; float:left;">
                                    <asp:TextBox ID="txtAbbreviation" runat="server" CssClass="validate[required,maxSize[1],minSize[1]]] TextBox" Width="150px"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtApprovalNo" runat="server" CssClass="validate[groupRequired[]] TextBox" Width="150px"></asp:TextBox>--%>
                                </div>
                            </div>
							<div style="height: 55px; margin-top: 10px; width: 100%;">
								<div style="text-align: left; width: 20%; float: left;" class="label">
									Section Description :
								</div>
								<div style="text-align: left; float: left; width: 80%;">
									<asp:TextBox ID="txtSectionDesc" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
								</div>

							</div>
						</div>

						<div style="height: 30px; float: right; width: 100%; padding:0 10px 10px 0px">
							<%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach"  OnClientClick="myFunction()" />--%>&nbsp;&nbsp;					
							&nbsp;&nbsp;
							<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium" OnClick="btnSave_Click" />
					        
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

		$(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
        	var valid = $("#aspnetForm").validationEngine('validate');
        	var vars = $("#aspnetForm").serialize();

        	
		});
	 
	   
    </script>
</asp:Content>
