<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SchoolMaterialReturn.aspx.cs" Inherits="GEIMS.ReportUI.SchoolMaterialReturn" %>
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
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Material Return
            <%--<asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail"
                OnClientClick="getPrint('divContent');" />--%>
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />
              &nbsp;
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

                        <asp:Panel ID="pnlConsumptionInfo" runat="server" GroupingText="Material Consumption Details">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="float: left; width: 100%; text-align: center;">
                                    From Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtConsumptionFromDate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtConsumptionFromDate" TargetControlID="txtConsumptionFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                    To Date :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtConsumptionTodate" runat="server" CssClass="validate[required] TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtConsumptionTodate" TargetControlID="txtConsumptionTodate">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
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
                                        Trust :
                                                <asp:Label runat="server" ID="lblTrust"></asp:Label>
                                    </div>
                                </div>
                            </div>
                             <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        School :
                                                <asp:Label runat="server" ID="lblSchool"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        Date :
                                                <asp:Label runat="server" ID="lblFromDate"></asp:Label>&nbsp;To&nbsp; 
                                        <asp:Label runat="server" ID="lblToDate"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 10px; padding-right: 30px; float: left; width: 100%">
                                <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
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
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        function validateFromTODate() {
            var from = $("#<%=txtConsumptionFromDate.ClientID %>").val();
             var to = $("#<%=txtConsumptionTodate.ClientID %>").val();

             var dateStrA = from.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");
             var dateStrB = to.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");

             // now you can compare them using:

             var fromDate = new Date(dateStrA);
             var toDate = new Date(dateStrB);

             if (fromDate > toDate) {
                 alert('Enter valid Date For Search Data.');
                 $("#<%=txtConsumptionFromDate.ClientID %>").val('');
                $("#<%=txtConsumptionTodate.ClientID %>").val('');
                return false;
            }
        }

        $('#<%=txtConsumptionFromDate.ClientID%>').change(function () {
            validateFromTODate();
        });
        $('#<%=txtConsumptionTodate.ClientID%>').change(function () {
            validateFromTODate();
        });
         </script>
</asp:Content>
