<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="MaterialStock.aspx.cs" Inherits="GEIMS.ReportUI.MaterialStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script>
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Material Stock
            <%--<asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail"
                OnClientClick="getPrint('divContent');" />--%>
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" /> &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                 OnClick="btnReport_Click" />

                </div>
                <div id="divContent" style="height: 100%; font-family: Verdana;">
                    <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
                    <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                        <div style="text-align: center; width: 100%;">
                            <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                        </div>

                        <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                            <div id="tabs-1" style="min-height: 150px;">

                                <asp:Panel ID="pnlStockInfo" runat="server" GroupingText="Material Stock Details">
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="text-align: left; width: 30%; float: left;" class="label">
                                                Select:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 70%; float: left;">
                                                <%--<asp:RadioButton runat="server" CssClass="validate[required]" ValidationGroup="Gender" Text="Male" ID="rdMale" />
										<asp:RadioButton runat="server" CssClass="validate[required]" ValidationGroup="Gender" Text="Female" ID="rdFemale" />--%>
                                                <asp:RadioButtonList ID="rblSelect" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblSelect_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Trust</asp:ListItem>
                                                    <asp:ListItem Value="1">School</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divTrust" style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="text-align: left; width: 30%; float: left;" class="label">
                                                Trust:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 70%; float: left;">
                                                <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlTrust" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="ddlTrust_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divSchool" style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="text-align: left; width: 30%; float: left;" class="label">
                                                School:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 70%; float: left;">
                                                <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlSchool" Width="50%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divGO" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" OnClientClick="javascript:return TestCheckBox();" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <asp:ImageButton ID="btnExportPDF" runat="server" ImageUrl="~/Images/adobe.PNG"
                                                    ToolTip="Export to PDF" OnClick="btnExportPDF_Click" />

                                                &nbsp;
                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
                        ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />
                                                &nbsp;
                    <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="~/Images/word.PNG"
                        ToolTip="Export to Word" OnClick="btnExportWord_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>Report : Material Stock List</b>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divDisplayTrust" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                Trust :
                                                <asp:Label runat="server" ID="lblTrust"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="divDisplaySchool" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                School :
                                                <asp:Label runat="server" ID="lblSchool"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="padding: 10px; padding-right: 30px; float: left; width: 100%">
                                        <asp:GridView ID="gvReport" Visible="true" OnRowDataBound="gvReport_OnRowDataBound" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                            CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%">
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblSelect" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTrust" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExportPDF" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
            <asp:PostBackTrigger ControlID="btnExportWord" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
         </script>
</asp:Content>
