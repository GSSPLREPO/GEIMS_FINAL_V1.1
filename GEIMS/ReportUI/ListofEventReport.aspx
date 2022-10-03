<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ListofEventReport.aspx.cs" Inherits="GEIMS.ReportUI.ListofEventReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script>
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
                    List of Event Details
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium label" Text="Print Detail"
                OnClientClick="getPrint('divContent');" OnClick="btnPrintDetail_Click"  />
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

                                <asp:Panel ID="pnlVandorMaterialInfo" runat="server" GroupingText="List Of Event Details">

                                    <div runat="server" id="divDisplaySchool" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div class="label" style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                School Name :
                                                <asp:DropDownList ID="ddlSchoolName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolName_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="divSectionName" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div class="label" style="float: left; text-align: left; margin-left:24%;  width: 80%; padding-bottom: 10px;">
                                                Section Name :
                                                <asp:DropDownList ID="ddlSectionName" runat="server" Width="52%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="divFromDate" style="width: 25%; float: left; margin-left:23%;" class="label" >
                                        <div style="padding: 10px;">
                                            <div style="text-align: left; width: 34%; float: left;" class="label">
                                                From Date:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 30%; float: left; margin-left:6%;">

                                                 <asp:TextBox ID="txtFromDate" runat="server" onKeyUp="CheckTextLength(this,10)" onkeypress="return numericDate(event);" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                     <div runat="server" id="divToDate" style="width: 30%; float:left; margin-left:3%;" class="label">
                                        <div style="padding: 10px;" >
                                            <div style="text-align: left; width: 25%; float: left;" class="label">
                                                To Date:<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 25%; float: left;">
                                                 <asp:TextBox ID="txtToDate" runat="server" onKeyUp="CheckTextLength(this,10)" onkeypress="return numericDate(event);" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                
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
                                </asp:Panel>

                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
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
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; font-size:larger; width: 100%; padding-bottom: 10px;">
                                                <b>Report :List of Events</b>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="div2" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                            <div class="headerLogo" style="margin-top: 9px; float: left">
                                                <asp:Image ImageUrl="~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" runat="server" ID="imgphoto"
                                                    Width="100px" Height="100px" />
                                            </div>
                                            <div style="padding: 10px; width: 80%; padding-right: 30px; margin-left: 15%;">

                                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px; margin-left: -7%;">
                                                    <asp:Label runat="server" ID="lblSchoolName" Font-Size="Medium" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px; margin-left: -7%;">
                                                    <asp:Label runat="server" ID="lblPhoneNo" Font-Size="Small"></asp:Label>
                                                </div>
                                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px; margin-left: -7%;">
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    
                                    
                                    
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                Date : From
                                                <asp:Label runat="server" ID="lblFromDate"></asp:Label>&nbsp;To&nbsp; 
                                        <asp:Label runat="server" ID="lblToDate"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 10px; padding-right: 30px; float: left; width: 100%">
                                        <asp:GridView ID="gvReport" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="False"
                                            CellPadding="4" Font-Names="Verdana" Font-Size="11px" Width="100%">
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="EventFromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="8%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EventToDate" HeaderText="To Date">
                                                    <HeaderStyle Width="7%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                              <%-- <asp:BoundField DataField="EventCategoryName" HeaderText="Event Category">
                                                    <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="EventName" HeaderText="Event Name">
                                                    <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="SchoolNameEng" HeaderText="School Name">
                                                    <HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="DepartmentNameEng" HeaderText="Section Name">
                                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EventLocation" HeaderText="Location">
                                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EventOrgeniserName" HeaderText="Orgeniser Name">
                                                    <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                            </Columns>
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
      
    </script>
</asp:Content>
