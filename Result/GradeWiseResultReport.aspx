<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="GradeWiseResultReport.aspx.cs" Inherits="GEIMS.Result.GradeWiseResultReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	 <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	 <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            ClassWise Grade Result Report
            <%--<asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail"
                OnClientClick="getPrint('divContent');" />--%>
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel" OnClick="btnBack_Click"/>
              &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu" OnClick="btnReport_Click"/>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                    <div id="tabs-1" style="min-height: 150px;">

                        <asp:Panel ID="pnlEmployeeInfo" runat="server" GroupingText="Result Details">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Class :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="validate[required] Droptextarea" Width="260px" AutoPostBack="True" Enabled="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Division :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
							 <%-- <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Exam :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList runat="server" ID="ddlExam" CssClass="validate[required] Droptextarea" Width="260px">
										<asp:ListItem Value="">--Select--</asp:ListItem>
										<asp:ListItem Value="FA1">FA1</asp:ListItem>
										<asp:ListItem Value="FA2">FA2</asp:ListItem>
										<asp:ListItem Value="SA1">SA1</asp:ListItem>
										<asp:ListItem Value="INT-1">INT-1</asp:ListItem>
										<asp:ListItem Value="FA3">FA3</asp:ListItem>
										<asp:ListItem Value="FA4">FA4</asp:ListItem>
										<asp:ListItem Value="SA2">SA2</asp:ListItem>
										<asp:ListItem Value="INT-2">INT-2</asp:ListItem>
									</asp:DropDownList>
                                    </div>

                                </div>
                            </div>--%>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Academic Year :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                        <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        <asp:ImageButton ID="btnExportPDF" runat="server" ImageUrl="~/Images/adobe.PNG"
                                            ToolTip="To export data in this format selected fields is maximum 10." OnClick="btnExportPDF_Click" />

                                        &nbsp;
                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
                        ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />
                                        &nbsp;
                    <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="~/Images/word.PNG"
                        ToolTip="To export data in this format selected fields is maximum 10." OnClick="btnExportWord_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                       <b> Report : ClassWise Grade Result</b>
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
                                        Class :
                                                <asp:Label runat="server" ID="lblClass"></asp:Label>
                                    &nbsp;Division :
										<asp:Label runat="server" ID="lblDivision"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        Academic Year  :
										<asp:Label runat="server" ID="lblAcademicYear"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <%--<div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        Exam :
                                                <asp:Label runat="server" ID="lblExam"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>

                            <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1000px;">
                                <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%" OnRowDataBound="gvReport_RowDataBound">
                                    <RowStyle BackColor="White" />
									
                                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                        BorderWidth="1px" BorderStyle="Solid" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
            </div>
        </div>
        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
            <div style="padding: 10px; padding-right: 30px;">
                <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                </div>
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


		$('.Detach').click(function () {
			$("#aspnetForm").validationEngine('detach');
		});

	</script>
</asp:Content>
