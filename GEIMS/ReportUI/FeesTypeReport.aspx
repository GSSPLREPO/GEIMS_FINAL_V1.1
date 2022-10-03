<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="FeesTypeReport.aspx.cs" Inherits="GEIMS.ReportUI.FeesTypeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Fees Type Report
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
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>---%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                    <div id="tabs1" style="min-height: 150px;" runat="server">

                        <asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="Student Details">

                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Class Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <%--<asp:DropDownList ID="ddlclass" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                        <asp:GridView ID="gvClass" runat="server" CssClass="label" AutoGenerateColumns="False" Width="50%"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" AllowPaging="false" OnRowDataBound="gvClass_OnRowDataBound"
                                            PageSize="10" BackColor="White">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                            <Columns>
                                                <%--<asp:TemplateField ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <a href="javascript:expandcollapse('DivBid<%# Eval("BiddingID") %>', 'block');">
                                                            <img id='imgDivBid<%# Eval("BiddingID") %>' alt="Click to show/hide SOR <%# Eval("BiddingID") %>"
                                                                width="9px" border="0" src="../_Images/plus.png" />
                                                            <asp:Label ID="lblEmpID" runat="server" Visible="false" Text='<%# Eval("BiddingID") %>'></asp:Label>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="ClassMID" HeaderText="Class ID">
                                                    <HeaderStyle Width="30px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="30px" VerticalAlign="Top" CssClass="hidden" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ClassName" HeaderText="Class Name">
                                                    <HeaderStyle Width="100%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="100%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="DMSOrderNo" HeaderText="DMS OrderNo.">
                                                    <HeaderStyle Width="150px" HorizontalAlign="center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="center" Width="20%" VerticalAlign="middle" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                                    <HeaderStyle Width="45%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="200px" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name">
                                                    <HeaderStyle Width="25%" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="200px" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BiddingStatus" HeaderText="Status">
                                                    <HeaderStyle Width="150px" HorizontalAlign="center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="center" Width="30%" VerticalAlign="middle" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/_Images/Edit.png"
                                                            CommandName="Edit1" CommandArgument='<%# Eval("BomCostSheetBasicID")%>' Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/_Images/delete-1.png"
                                                            CommandName="Delete" CommandArgument='<%# Eval("BomCostSheetBasicID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                            Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="5%" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div style="position: relative; left: 15px; overflow: auto; width: 60%">
                                                                    <asp:GridView ID="gvChild" runat="server" CssClass="label" AutoGenerateColumns="False" Width="100%"
                                                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                                                        Font-Names="verdana" Font-Size="12px" AllowPaging="false" HorizontalAlign="Center"
                                                                        PageSize="10" BackColor="White">
                                                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="DivisionTID" HeaderText="DivisionTID">
                                                                                <HeaderStyle Width="30px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                                                <ItemStyle HorizontalAlign="left" Width="30px" VerticalAlign="Top" CssClass="hidden" Wrap="true" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DivisionName" HeaderText="Division Name">
                                                                                <HeaderStyle Width="80%" HorizontalAlign="left" VerticalAlign="Top" />
                                                                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                                                                            </asp:BoundField>
                                                                            <%--<asp:BoundField DataField="SkidName" HeaderText="SOR Name">
                                                                                <HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle HorizontalAlign="left" Width="200px" VerticalAlign="Top" Wrap="true" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FreightTotal" HeaderText="Freight Total">
                                                                                <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle HorizontalAlign="right" Width="150px" VerticalAlign="Top" Wrap="true" />
                                                                            </asp:BoundField>--%>
                                                                            <%--<asp:BoundField DataField="LandedCostTotal" HeaderText="Landed Cost Total">
                                                                                <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle HorizontalAlign="right" Width="150px" VerticalAlign="Top" Wrap="true" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="SellTotal" HeaderText="Sell Total">
                                                                                <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle HorizontalAlign="right" Width="150px" VerticalAlign="Top" Wrap="true" />
                                                                            </asp:BoundField>--%>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" Width="50px" Checked="True" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="center" />
                                                                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>
                            <%--<div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Division Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
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
                                    <div style="float: left; width: 15%;">
                                        From Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 185px;">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        To Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
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
                    </div>
                    <div id="divButtons" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="padding: 10px; padding-right: 30px;">
                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                <%-- <asp:ImageButton ID="btnExportPDF" runat="server" ImageUrl="~/Images/adobe.PNG"
                                            ToolTip="Export to PDF" OnClick="btnExportPDF_Click" />
                                        &nbsp;--%>
                                <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/excel.PNG"
                                    ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />
                                <%-- &nbsp;
                                        <asp:ImageButton ID="btnExportWord" runat="server" ImageUrl="~/Images/word.PNG"
                                            ToolTip="Export to Word" OnClick="btnExportWord_Click" />--%>
                            </div>
                        </div>
                    </div>
                    <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 20px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <b>Report : Outstanding Fees Report</b>
                                </div>
                            </div>
                        </div>
                        <%--<div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 20px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <b>Class :</b>
                                    <asp:Label runat="server" ID="lblClassName"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;
                                            <b>Division :</b>
                                    <asp:Label runat="server" ID="lblDivision"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 30px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <b>Status :</b>
                                    <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;
                                                <b>Academic Year :</b>
                                    <asp:Label runat="server" ID="lblAYear"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1100px" runat="server">
                            <%--<asp:Label ID="lblErrMsg" runat="server" ForeColor="red" Style="text-align: center"></asp:Label>--%>
                            <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%" OnRowDataBound="gvReport_OnRowDataBound">
                                <RowStyle BackColor="White" Wrap="false" />
                                <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" Wrap="false" />
                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                    BorderWidth="1px" BorderStyle="Solid" Wrap="false" />
                            </asp:GridView>
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
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlClass" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>
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

        $('.Attach').click(function () {
            $("#aspnetForm").validationEngine('attach');
        });
    </script>
    <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
