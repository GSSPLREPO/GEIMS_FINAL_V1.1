<%@ Page Language="C#"  MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="EmployeePayrollReport.aspx.cs" Inherits="GEIMS.ReportUI.EmployeePayrollReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Employee Payroll Report (Monthly)
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click"/>&nbsp;
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
                            <div id="tabs-1" style="min-height: 150px;padding:5px">
                                <div id="divEmployee" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <asp:Panel ID="pnlEmployeePayrollInfo" runat="server" GroupingText="Payroll Details">
                                    <div style="width: 100%; float: left;" class="label">
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                             <div style="float:Left; width: 10%;">
                                                Month :<span style="color: red">*</span>
                                            </div>
                                            <div style="float:Left; width: 30%;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="Droptextarea" Width="210px">
                                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            

                                            <div style="float: left; width: 10%;">
                                                Year :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="Droptextarea" Width="210px" >
                                                </asp:DropDownList>
                                            </div>
                                           
                                            <div style="float: left; text-align: right; width: 20%; ">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium" OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <%--<div style="float: left; width: 15%;">
                                                Month :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                        </div>
                                    </div>
                                                                      
                                </asp:Panel>
                                    </div>

                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                      <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float:left;text-align:left; width: 20%; padding-bottom: 10px;"">
                                        <img src="../Images/Logo1.jpg" style="height:100px;width:100px"/>
                                    </div>
                                     <div style="float: left; text-align: right; width: 20%; padding-bottom: 10px;">
                                         </div>
                                    <div style="float: left; text-align: right; width: 20%; padding-bottom: 10px;">
                                       
                                        <asp:ImageButton ID="btnExportPDF" runat="server" ImageUrl="~/Images/adobe.PNG"
                                            ToolTip="Export to PDF" />

                                        &nbsp;
                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
                        ToolTip="Export to Excel"  />
                                        &nbsp;
                    <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="~/Images/word.PNG"
                        ToolTip="Export to Word" />
                                    </div>
                                </div>
                            </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 20px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>Trust :</b>
                                                <asp:Label runat="server" ID="lblTrustName"></asp:Label>
                                                &nbsp; &nbsp; &nbsp;
                                                
                                            </div>
                                        </div>
                                    </div>

                                    <%-- <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                               
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                  <b>Month :</b>
                                                <asp:Label runat="server" ID="lblMonth"></asp:Label>
                                                &nbsp; &nbsp; &nbsp;
                                               
                                                
                                                <b>Year :</b>
                                                <asp:Label runat="server" ID="lblYear"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <%--  <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                              
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1450px">
                                         <asp:Label ID="lblErrMsg" runat="server" ForeColor="red" style="text-align: center"></asp:Label>
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
           
                  <div id="divSchool1" style="width: 100%; padding-top: 0px; display: none;">
                    <div id="div1" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 20px;">
                                <div style="float: left; text-align: left; width: 25%; padding-bottom: 10px;">
<img src="../Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" width="100" height="100" />
                                    </div>
                                <div style="float: left; text-align: center; width: 75%; padding-bottom: 10px;">
                                    <b>Trust :</b>
                                    <asp:Label runat="server" ID="lblTrust"></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 30px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                      <b>Month :</b>
                                    <asp:Label runat="server" ID="lblMonth1"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;
                                               
                                    <b>Year :</b>
                                    <asp:Label runat="server" ID="lblYear1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="padding: 10px; padding-right: 30px; width: 1450px; float :left ">
                            <asp:GridView ID="gvReport1" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="True"
                                          CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%" OnRowDataBound ="gvReport1_RowDataBound"/>
                                <RowStyle BackColor="White" />
                                <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                    BorderWidth="1px" BorderStyle="Solid" />
                            
                        </div>
                    </div>
                </div>
            </div>
       <%-- </ContentTemplate>
      <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlClass" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExportPDF" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
            <asp:PostBackTrigger ControlID="btnExportWord" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
