<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="StudentCategoryWise.aspx.cs" Inherits="GEIMS.ReportUI.StudentCategoryWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Student Reports
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />  &nbsp;
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
                                <asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="Student Details">
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                School Name :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:DropDownList ID="ddlSchool" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Section :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Year :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Status :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; text-align: right; width: 100%;">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>Report: Student Gender Wise</b>
                                            </div>
                                        </div>

                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                School:  
                                                <asp:Label runat="server" ID="lblSchool"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                     <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                Section :
                                                <asp:Label runat="server" ID="lblSection"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                     <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                              Academic Year :
                                                <asp:Label runat="server" ID="lblYear"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                Status :
                                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
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

                <div id="divPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                      <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>Report: Student Gender Wise</b>
                                            </div>
                                        </div>

                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                School:  
                                                <asp:Label runat="server" ID="lblSchool1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                     <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                Section :
                                                <asp:Label runat="server" ID="lblSection1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                     <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                              Academic  Year :
                                                <asp:Label runat="server" ID="lblYear1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                Status :
                                                <asp:Label runat="server" ID="lblStatus1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <asp:GridView ID="gvReport1" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                            CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="true" Width="100%">
                                            <RowStyle BackColor="White" />
                                            <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                                BorderWidth="1px" BorderStyle="Solid" />
                                        </asp:GridView>
                                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSchool" EventName="SelectedIndexChanged" />
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

