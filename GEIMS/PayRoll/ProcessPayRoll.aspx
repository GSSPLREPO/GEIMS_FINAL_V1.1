<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="ProcessPayRoll.aspx.cs" Inherits="GEIMS.PayRoll.ProcessPayRoll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Process PayRoll Master
           <%-- <asp:LinkButton CausesValidation="false" ID="lnkAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" runat="server" style="height: 100%; font-family: Verdana; padding-bottom: 10px;">
            <div id="divContent1" style="width: 0%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 100%; float: left;">
                <div id="divPurchaseTab">
                    <div id="tabs" runat="server" class="style-tabs" visible="True" style="width: 100%; float: left;">
                        <div style="float: left; width: 100%;">
                            <ul>
                                <li><a id="tabPurchasePaySlipDetails" href="#tabs-1">Employee Payslip Details</a></li>
                            </ul>
                        </div>
                        <div id="tabs-1" class="gradientBoxesWithOuterShadows" style="float: left; width: 99%; padding:5px">
                           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" style=" width: 100%;">
                                <ContentTemplate>--%>
                                    <div id="Div1" style="width: 99%; float: left;" class="label">
                                        <div style="padding: 10px 0 10px 10px;">
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;" align="center">
                                                    <div style="float: left; width: 100%;">
                                                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left; padding-bottom: 10px" class="label">
                                                <div style="padding: 0px 0 10px 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Month :
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                            <asp:ListItem Value=" ">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Jan</asp:ListItem>
                                                            <asp:ListItem Value="2">Feb</asp:ListItem>
                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                            <asp:ListItem Value="8">Aug</asp:ListItem>
                                                            <asp:ListItem Value="9">Sep</asp:ListItem>
                                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div style="float: left; width: 15%;">
                                                        Year :
                                                    </div>
                                                    <div style="float: left; width: 35%;">
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divEmployee" runat="server" style="width: 1290px; float: left; padding: 0 0 10px 10px;">
                                                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 98%; overflow: scroll">
                                                    <asp:GridView ID="gvtemp" runat="server" BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" ShowFooter="False"
                                                        Font-Names="verdana" Font-Size="12px" Width="95%" BackColor="White" OnRowDataBound="gvtemp_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="White" />
                                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkChild" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </div>   
                                            <div id="divButtons" runat="server" style="width: 100%; float: left;" class="label">
                                                <div style="float: left; text-align: right; width: 100%;" align="center">
                                                    <asp:Button ID="btnApprove" runat="server" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnApprove_Click" Text="Approve" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" Text="Export All" CssClass="btn-blue-new btn-blue-medium" OnClick="btnExport_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                             <%--   </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
                                    <asp:PostBackTrigger ControlID="ddlYear" />
                                    <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnExport" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
        </div>
    </div>

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });

    </script>
</asp:Content>

