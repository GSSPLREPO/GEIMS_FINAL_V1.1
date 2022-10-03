<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="GEIMS.ReportUI.EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Empolyee List
            <%--<asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail"
                OnClientClick="getPrint('divContent');" />--%>
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />&nbsp;
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

                        <asp:Panel ID="pnlEmployeeInfo" runat="server" GroupingText="Employee Details">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Department Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfTab" runat="server" ClientIDMode="Static" />
                                    </div>

                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Designation Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Employee Role :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlEmployeeRole" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Select Parameter :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%; text-align: center">
                                        <asp:GridView ID="gvParameter" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" BackColor="White">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="SrNo" HeaderText="SrNo">
                                                    <HeaderStyle BackColor="#3B5998" HorizontalAlign="Center" VerticalAlign="Middle"
                                                        Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Fields" HeaderText="Field Selection">
                                                    <HeaderStyle BackColor="#3B5998" HorizontalAlign="Center" VerticalAlign="Middle"
                                                        Width="100px" Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" Width="400px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" CssClass="label" OnCheckedChanged="chkHeader_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" CssClass="label" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#3B5998" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
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
                                       <b> Report : Employee List</b>
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
                                        Department :
                                                <asp:Label runat="server" ID="lblDepartmenmt"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        Designation  :
                                                <asp:Label runat="server" ID="lblDesignation"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                        Role :
                                                <asp:Label runat="server" ID="lblRole"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1100px">
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
        var TargetBaseControl = null;

        window.onload = function () {
            try {
                //get target base control.
                TargetBaseControl =
                  document.getElementById('<%= this.gvParameter.ClientID %>');
            }
            catch (err) {
                TargetBaseControl = null;
            }
        }

        function TestCheckBox() {
            if (TargetBaseControl == null) return false;

            //get target child control.
            var TargetChildControl = "chk";

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                   Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
                   Inputs[n].checked)
                    return true;

            alert('Select at least one checkbox!');
            return false;
        }
    </script>
</asp:Content>
