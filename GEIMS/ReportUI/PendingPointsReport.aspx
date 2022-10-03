<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="PendingPointsReport.aspx.cs" Inherits="GEIMS.ReportUI.PendingPointsReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function CheckTextLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                alert(" Only " + long + " characters allowed");
            }
        }

        function numericDate(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 47 && unicode <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Pending Point Report
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium label" Text="Print Detail"
                OnClientClick="getPrint('divContent');" OnClick="btnPrintDetail_Click" />
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
                        </div>

                        <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                            <div id="tabs-1" style="min-height: 150px;">

                                <asp:Panel ID="pnlVandorMaterialInfo" runat="server" GroupingText="Pending Point Report">

                                    <div style="width: 100%; float: left; padding-bottom: 20px;" class="label">
                                        <div style="text-align: center; padding-left: 35%; width: 15%; float: left;" class="label">
                                            All :
                                            <asp:RadioButton ID="rdoAll" runat="server" AutoPostBack="True" GroupName="g1" OnCheckedChanged="rdoAll_CheckedChanged" />
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            Filter By Date :
                                            <asp:RadioButton ID="rdoDate" runat="server" AutoPostBack="True" GroupName="g1" OnCheckedChanged="rdoDate_CheckedChanged" />

                                        </div>
                                    </div>


                                    <div runat="server" id="divFromDate" style="width: 25%; float: left; margin-left: 23%;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="text-align: left; width: 34%; float: left;" class="label">
                                                From Date:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 30%; float: left; margin-left: 6%;">

                                                <asp:TextBox ID="txtFromDate" runat="server" placeholder="DD/MM/YYYY" onKeyUp="CheckTextLength(this,10)" onkeypress="return numericDate(event);" CssClass="TextBox validate[required]" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="divToDate" style="width: 30%; float: left; margin-left: 3%;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="text-align: left; width: 25%; float: left;" class="label">
                                                To Date:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 25%; float: left;">
                                                <asp:TextBox ID="txtToDate" runat="server" placeholder="DD/MM/YYYY" onKeyUp="CheckTextLength(this,10)" onkeypress="return numericDate(event);" CssClass="TextBox validate[required]" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTodate" TargetControlID="txtTodate">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="div1" style="width: 60%; float: left; margin-left: 23%;" class="label">
                                      
                                            <div style="text-align: left; padding-left:20%; width: 30%; float: left;" class="label">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter From Date" ControlToValidate="txtFromDate" ValidationGroup="g1" SetFocusOnError="True" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                            </div>
                                            <div style="text-align: left; padding-left:8%; width: 30%; float: left; margin-left: 6%;">

                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter To Date" ControlToValidate="txtToDate" ValidationGroup="g1" SetFocusOnError="True" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                            </div>
                                    </div>
                                     <div runat="server" id="div3" style="width: 60%; float: left; margin-left: 23%;" class="label">
                                      
                                            <div style="text-align: left; padding-left:30%; width: 80%; float: left;" class="label">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From Date Cannot be Grater then To Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" ForeColor="#FF3300" Operator="GreaterThan" Type="Date" ValidationGroup="g1"></asp:CompareValidator>
                                            </div>
                                            
                                    </div>

                                    <div runat="server" id="divGO" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: right; width: 80%; padding-bottom: 10px;">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" ValidationGroup="g1" />
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="divMeetingName" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; text-align: center; padding-right: 0px;">
                                            <div class="label" style="float: left; padding-left: 8%; text-align: center; width: 80%; padding-bottom: 10px;">
                                                Meeting Name :
                                                <asp:DropDownList ID="ddlMeeting" runat="server" Width="52%" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel runat="server" ID="pnlReport">
                                    <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                            <div style="padding: 0px; padding-right: 30px;">
                                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                    <asp:ImageButton ID="btnExportPDF" runat="server" ImageUrl="~/Images/adobe.PNG"
                                                        ToolTip="Export to PDF" OnClick="btnExportPDF_Click" />

                                                    &nbsp; <%--  
                                                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
                                                        ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />
                                                                                &nbsp;
                                                    <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="~/Images/word.PNG"
                                                        ToolTip="Export to Word" OnClick="btnExportWord_Click" />--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div style="width: 100%; text-align: center; float: left; padding-top: 0px;" class="label">
                                            <div style="padding: 0px; padding-right: 30px;">
                                                <div style="float: left; text-align: center; font-size: larger; width: 100%; padding-bottom: 10px;">
                                                    <b>Report : Pending Point</b>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="div2" style="width: 100%; float: left; padding-top: 0px; padding-bottom: 20px;" class="label">

                                            <div class="headerLogo" style="margin-top: 9px; text-align: left; width: 10%; float: left;">
                                                <asp:Image ImageUrl="~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" runat="server" ID="Image1"
                                                    Width="100px" Height="100px" />
                                            </div>

                                            <div style="float: right; text-align: left; width: 100%; padding-bottom: 10px; padding-top: 20px; margin-left: 0%;">
                                                <div style=" text-align: center; padding-right: 0%;">
                                                    <asp:Label runat="server" ID="lblMeetingName" Font-Size="Medium" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                        <hr />
                                       


                                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                            <div style="padding: 10px; padding-right: 30px;">
                                                <div style=" float: left; text-align: left; width: 40%; padding-bottom: 10px;">
                                                    <asp:Label Text="Pending Point" runat="server" ID="Label2" Font-Size="16px" Font-Underline="True"></asp:Label>

                                                </div>

                                            </div>
                                        </div>

                                        <div style="padding-top: 10px; padding-right: 0px; float: left; width: 100%">
                                            <asp:GridView ID="gvPendingPoint" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="False"
                                                CellPadding="4" Font-Names="Verdana" Font-Size="11px" Width="100%" OnRowDataBound="gvPendingPoint_RowDataBound">
                                                <RowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="50px" ControlStyle-Width="50px" FooterStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Point" HeaderText="Point">
                                                        <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AssignTo" HeaderText="Assign">
                                                        <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Action" HeaderText="Action">
                                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="TargetDate" HeaderText="Target Date">
                                                        <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>

                                                </Columns>
                                                <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                                    BorderWidth="1px" BorderStyle="Solid" />

                                            </asp:GridView>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                    <div style="padding: 0px; padding-right: 30px;">
                        <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportPDF" />
            <%--<asp:PostBackTrigger ControlID="btnExportExcel" />
            <asp:PostBackTrigger ControlID="btnExportWord" />--%>
        </Triggers>
    </asp:UpdatePanel>
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

        function validateFromTODate() {
            var from = $("#<%=txtFromDate.ClientID %>").val();
            var to = $("#<%=txtToDate.ClientID %>").val();

            var dateStrA = from.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");
            var dateStrB = to.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3");

            // now you can compare them using:

            var fromDate = new Date(dateStrA);
            var toDate = new Date(dateStrB);

            if (fromDate > toDate) {
                alert('Enter valid Date For Search Data.');
                $("#<%=txtFromDate.ClientID %>").val('');
                $("#<%=txtToDate.ClientID %>").val('');
                return false;
            }
        }

        $('#<%=txtFromDate.ClientID%>').change(function () {
            validateFromTODate();
        });
        $('#<%=txtToDate.ClientID%>').change(function () {
            validateFromTODate();
        });
    </script>

    
</asp:Content>
