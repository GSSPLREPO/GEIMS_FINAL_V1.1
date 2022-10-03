<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="AccountLogin.aspx.cs" Inherits="GEIMS.Accounting.AccountLogin" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $('#tab-panel').tabs();
        });
    </script>
    <style type="text/css">
        .chk {
            vertical-align: text-top;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Account Login            
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <%--<script type="text/javascript">
            	$(function () {
            		$('#id_search').quicksearch('table#<%=gvClass.ClientID%> tbody tr');
                })
            </script>--%>
            <center>
				<asp:Panel ID="pnlLogin" runat="server" GroupingText="Login " Width="500px" style="font-weight:bold;text-align:left;font-size:11px;font-family:Verdana;" DefaultButton="btnLogIn">
					<table align="center" cellpadding="3" cellspacing="10" style="width: 500px;background-color:WhiteSmoke;" >
						<tr>
							<td colspan="2" align="Center">
								<asp:Label ID="lblMessage" runat="server" CssClass="Required"></asp:Label>
							</td>
						</tr>
						<tr>
							<td align="right" class="label" width="150px">
								From :<span class="Required">*</span>
							</td>
							<td align="left">
								<asp:TextBox ID="txtFrom" CssClass="textarea" runat="server" AutoPostBack="True" onclientshown="" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
								<ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
									Format="dd/MM/yyyy" TargetControlID="txtFrom" OnClientShown="calendarShown">
								</ajaxToolkit:CalendarExtender>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFrom" CssClass="Required"
									ErrorMessage="Enter From Date" ValidationGroup="1"> *</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="right" class="label">
								To :<span class="Required">*</span>
							</td>
							<td align="left">
								<asp:TextBox ID="txtTo" CssClass="textarea" runat="server" BackColor="#cae4ff"></asp:TextBox>
								<ajaxToolkit:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy"
									TargetControlID="txtTo">
								</ajaxToolkit:CalendarExtender>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTo" CssClass="Required"
									ErrorMessage="Enter To Date" ValidationGroup="1"> *</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="right" class="label">
								User Name :<span class="Required">*</span>
							</td>
							<td align="left">
								<asp:TextBox ID="txtUserName" runat="server" CssClass="textarea"></asp:TextBox>
								<%--<cc1:TextBoxWatermarkExtender ID="txtUserName_TextBoxWatermarkExtender" runat="server"
									Enabled="True" TargetControlID="txtUserName" WatermarkText="User Name">
								</cc1:TextBoxWatermarkExtender>--%>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" CssClass="Required"
									ErrorMessage="Enter User Name" ValidationGroup="1"> *</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="right" class="label">
								Password :<span class="Required">*</span>
							</td>
							<td align="left">
								<asp:TextBox ID="txtPassword" runat="server" CssClass="textarea" TextMode="Password"></asp:TextBox>
								<%-- <cc1:TextBoxWatermarkExtender ID="txtPassword_TextBoxWatermarkExtender1" runat="server"
									Enabled="True" TargetControlID="txtPassword" WatermarkText="Password">
								</cc1:TextBoxWatermarkExtender>--%>
								<%--<asp:TextBox ID="txtPassword" runat="server" CssClass="textarea" 
									TextMode="Password" AutoPostBack="True" ontextchanged="txtPassword_TextChanged" 
									ValidationGroup="1"></asp:TextBox>
								<cc1:TextBoxWatermarkExtender ID="txtPassword_TextBoxWatermarkExtender" runat="server"
									Enabled="True" TargetControlID="txtPassword" WatermarkText="Password">
								</cc1:TextBoxWatermarkExtender>--%>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" CssClass="Required"
									ErrorMessage="Enter Password" ValidationGroup="1"> *</asp:RequiredFieldValidator>
							</td>
						</tr>
						<%-- <tr>
					<td align="right" class="label">
						Organisation Name :<asp:Label ID="Label3" runat="server" 
							CssClass="ererorlabelmessage" Text="*"></asp:Label>
					</td>
					<td align="left">
						<asp:DropDownList ID="ddlOrg" runat="server" CssClass="textarea">
						</asp:DropDownList>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
							ControlToValidate="ddlOrg" ErrorMessage="Enter Organisation Name" 
							ValidationGroup="1">*</asp:RequiredFieldValidator>
						<cc1:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" 
							runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
						</cc1:ValidatorCalloutExtender>
					</td>
				</tr>--%>
						<tr>
						    <td style="text-align: left; vertical-align: top;">
						        <asp:CheckBox ID="chkLock" runat="server" CssClass="chk" Text=" Lock"></asp:CheckBox>
						    </td>
							<td align="center" class="label">
								
								<%-- <asp:Button ID="btnLogIn" runat="server" Text="Log In" CssClass="button" 
					   onclick="btnLogIn_Click" />--%>
								<asp:Button ID="btnCancel" runat="server" CssClass="btn-blue btn-blue-medium" 
									Text="Cancel" />
								<asp:Button ID="btnLogIn" runat="server" CssClass="btn-blue btn-blue-medium" 
									Text="Log In" ValidationGroup="1" OnClick="btnLogIn_Click" />
								<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="1"> </asp:ValidationSummary>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</center>
        </div>
    </div>
    <script type="text/javascript">
        $('[id$=chkLock]').click(function () {
            if ($(this).is(":checked")) {
                $('[id$=btnLogIn]').val('Lock');
            } else {
                $('[id$=btnLogIn]').val('Log In');
            }
        });
    </script>
</asp:Content>
