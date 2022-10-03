<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainSection.aspx.cs" MasterPageFile="~/Master/TrustMain.Master" Inherits="GEIMS.Client.UI.MainSection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
	<div id="divMain" style="width: 100%; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			School Details
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div style="text-align: center; width: 100%;">
					<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>
				</div>
				<div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
					<asp:GridView ID="grCollDetails" runat="server" AutoGenerateColumns="False"
						BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
						Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowDataBound="grCollegeDetails_RowDataBound" OnRowCommand="grCollegeDetails_RowCommand">
						<FooterStyle BackColor="White" ForeColor="#333333" />
						<RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
						<Columns>
							<asp:BoundField DataField="SchoolMID" HeaderText="SchoolID">
								<HeaderStyle BackColor="#3B5998" HorizontalAlign="Center" VerticalAlign="Middle"
									Width="500px" CssClass="hidden" />
								<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" Width="300px" CssClass="hidden" />
							</asp:BoundField>
							<asp:BoundField DataField="SchoolNameEng" HeaderText="School Name">
								<HeaderStyle BackColor="#3B5998" HorizontalAlign="Center" VerticalAlign="Middle"
									Width="500px" />
								<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" Width="300px" />
							</asp:BoundField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="linkManage" runat="server" CommandArgument='<%# Eval("SchoolMID")%>'
										CommandName="Manage" Style="text-decoration: none; color: #3B5998" Font-Bold="true"
										Width="120px">Manage</asp:LinkButton>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Center" BackColor="#3B5998" Width="80px" />
								<ItemStyle HorizontalAlign="Center" Width="80px" />
							</asp:TemplateField>
						</Columns>
						<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
						<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
						<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
						<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
					</asp:GridView>
				</div>
			</div>
			<div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
		</div>
	</div>
</asp:Content>
