<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GEIMS.Accounting.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script>
		$(function () {
			$(document.getElementById('<%= tabs.ClientID %>')).tabs();
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
	<div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
		<div id="divTitle" class="pageTitle" style="width: 100%;">
			Accounting
			
		</div>
		<div id="divContent" style="height: 100%; font-family: Verdana;">
			<div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
			<div id="divContent2" style="width: 80%; float: left; height: 100%;">
				<div id="Div1" align="center">
					<asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
				</div>


				<div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
					<ul>
						<li><a id="tabStudentTemplateDetails" href="#tabs-1">Accounting</a></li>
					</ul>
					<div id="tabs-1" style="padding:0 0 0 10px" class="gradientBoxesWithOuterShadows">
						<%--<fieldset>--%>
						<asp:Panel ID="Panel1" runat="server">
							<table width="100%">
								<tr>
									<td width="75%">
										<p>
											Finance is the science of funds management.[1] The general areas of finance arebusiness finance, personal finance, and public finance.[2] Finance includes savingmoney and often includes lending money. The field of finance deals with the conceptsof time, money and risk and how they are interrelated. It also deals with how moneyis spent and budgeted. Finance works most basically through individuals and businessorganizations depositing money in a bank. The bank then lends the money out to other individuals or corporations for consumption or investment, and charges intereston the loans.
										</p>
									</td>
									<td width="25%">
										<img src="../Images/Accouting.jpg" width="150px" height="150px"  />
									</td>
								</tr>
							</table>

						</asp:Panel>
						<%--</fieldset>--%>
					</div>
				</div>
				<div id="divContent3" style="width: 10%; float: right;"></div>
			</div>
		</div>
	</div>

</asp:Content>
