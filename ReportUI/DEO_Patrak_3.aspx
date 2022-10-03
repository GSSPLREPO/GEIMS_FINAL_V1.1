<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="DEO_Patrak_3.aspx.cs" Inherits="GEIMS.ReportUI.DEO_Patrak_3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../CSS/TabPanel.css" rel="stylesheet" />
	<link href="../CSS/screen.css" rel="stylesheet" />
	<style type="text/css">
		.FixedHeader {
			position: absolute;
			font-weight: bold;
		}

		.VertiColumn th {
			height: 100px;
			width: 100px;
			vertical-align: baseline;
			-webkit-transform: rotate(-90deg);
			-moz-transform: rotate(-90deg);
			-o-transform: rotate(-90deg);
			filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
		}

		.headerBottom {
			vertical-align: bottom;
		}
	</style>


	} 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div id="divCurrenTabSelected" class="hidden" visible="false">
	</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Patrak-3
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
			&nbsp;
             <asp:Button ID="btnBack1" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
				 OnClick="btnBack_Click" />
			&nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
						OnClick="btnReport_Click" />
		</div>
		<div id="divReport" runat="server" style="width: 100%; padding-top: 0px;">
			<div>
				<div id="divButtons" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
					<div style="padding: 10px; padding-right: 30px;">
						<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">

							<asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
								ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />

						</div>
					</div>
				</div>
				<div style="width: 100%; float: left; padding-top: 0px;" class="label">
					<div style="padding: 10px; padding-right: 30px;">
						<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
							<b>Report : DEO Report (Patrak - 3)</b>
						</div>
					</div>
				</div>
				
				<div style="width: 100%; float: left; padding-top: 0px;" class="label">
					<div style="padding: 10px; padding-right: 30px;">
						<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
							Trust Name :
                                                <asp:Label runat="server" ID="lblTrustName"></asp:Label>
						</div>
					</div>
				</div>
				<div style="width: 100%; float: left; padding-top: 0px;" class="label">
					<div style="padding: 10px; padding-right: 30px;">
						<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
							School Name :
                                                <asp:Label runat="server" ID="lblSchoolName"></asp:Label>
						</div>
					</div>
				</div>
				<div style="width: 100%; float: left; padding-top: 0px;" class="label">
					<div style="padding: 10px; padding-right: 30px;">
						<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
							<%-- Cluster Name :
							--%>
							<asp:Label runat="server" ID="lblClusterName"></asp:Label>
						</div>
					</div>
				</div>
			</div>
			<div style="padding: 10px; padding-right: 30px; width: 1100px; float: left" style="width: 100%; filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);">
				<asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true" SortedAscendingHeaderStyle="FixedHeader"
					CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%" HeaderStyle="">
					<RowStyle BackColor="White" Width="150px" />
					<FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
					<PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
					<HeaderStyle CssClass="" BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
						BorderWidth="1px" BorderStyle="Solid" Wrap="True" Height="150px" />
				</asp:GridView>
			</div>
		</div>
	</div>
	<div id="divReport1" style="width: 100%; padding-top: 0px; display: none">
		<div style="width: 100%; float: left; padding-top: 0px;" class="label">
			<div style="padding: 10px; padding-right: 30px;">
				<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
					<b>Report : DEO Report (Patrak - 3)</b>
				</div>
			</div>
		</div>
		<div style="width: 100%; float: left; padding-top: 0px;" class="label">
			<div style="padding: 10px; padding-right: 30px;">
				<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
					Trust Name :
                                                <asp:Label runat="server" ID="lblTrust"></asp:Label>
				</div>
			</div>
		</div>
		<div style="width: 100%; float: left; padding-top: 0px;" class="label">
			<div style="padding: 10px; padding-right: 30px;">
				<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
					School Name :
                                                <asp:Label runat="server" ID="lblSchool"></asp:Label>
				</div>
			</div>
		</div>
		<div style="width: 100%; float: left; padding-top: 0px;" class="label">
			<div style="padding: 10px; padding-right: 30px;">
				<div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
					Cluster Name :
                                                <asp:Label runat="server" ID="lblCluster"></asp:Label>
				</div>
			</div>
		</div>
		<div style="padding: 10px; padding-right: 30px; width: 1100px; float: left">
			<asp:GridView ID="gvReport1" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
				CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%">
				<RowStyle BackColor="White" />
				<FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
				<PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
				<HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
					BorderWidth="1px" BorderStyle="Solid" />
			</asp:GridView>
		</div>
	</div>
</asp:Content>

