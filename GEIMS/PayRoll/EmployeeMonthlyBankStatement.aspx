<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="EmployeeMonthlyBankStatement.aspx.cs" Inherits="GEIMS.PayRoll.EmployeeMonthlyBankStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Bank Statement
            <%--<asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail"
                OnClientClick="getPrint('divContent');" />--%>
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />&nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                 OnClick="btnReport_Click" OnClientClick="javascript: RedirectToMenu();"  />
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                    <div id="tabs-1" style="min-height: 150px;">

                    <asp:Panel ID="pnlEmployeeInfo" runat="server" GroupingText="Bank Statement">
                           
                            <%--<div style="width: 100%; float: left;" class="label">
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
                            </div>--%>
                           <%-- <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Designation Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>--%>
                           <%-- <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        Employee Role :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlEmployeeRole" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                            
                             <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: Left; width: 15%;">
                                                Month :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: Left; width: 25%;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="validate[required] Droptextarea" Width="100px" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true">
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
                                            <div style="float: left; width: 15%;">
                                                Year :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 32%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="100px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>

                                         
                                        </div>
                                    </div>

                            <%-- <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                          

                                            <div style="float: left; width: 15%;">
                                                Year :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 32%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="100px">
                                                </asp:DropDownList>
                                            </div>

                                           <%-- <div style="float: left; text-align: right; width: 20%;">
                                                <asp:Button runat="server" ID="Button1" Text="Go" CssClass="btn-blue-new btn-blue-medium " OnClick="btnGo_Click" />
                                            </div>--%>
                                        <%--</div>
                                    </div>--%>

                           <%--  <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 490px;">
                                    <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                        <asp:Button runat="server" ID="BtnFind" Text="Find Employee" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="BtnFind_Click"/>
                                    </div>
                                        </div>
                                     </div>--%>

                                           
                                 

                            

                           <%-- <div style="width: 100%; float: left;" class="label">
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
                            </div>--%>

                 <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div  id="divSelectEmp" runat="server" style="float: left; width: 15%;" visible="false">
                                        Select Employee :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: auto; text-align: center">
                                        <asp:GridView ID="gvParameter" runat="server" AutoGenerateColumns="false"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" BackColor="White"  >
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                           <Columns>
                                                <asp:BoundField DataField="EmployeeMID" HeaderText="Employee ID" >
                                                <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true"  />
                                            </asp:BoundField>


                                               <asp:BoundField DataField="SrNo" HeaderText="SrNo">
                                                <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top"  />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true"  />
                                            </asp:BoundField>

                                           

                                         <asp:BoundField DataField="Name" HeaderText="Name">
                                                <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top"  />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="false" />
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
                                        <asp:Button runat="server" ID="BtnFind" Text="Find Employee" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="BtnFind_Click"/>
                                    </div>
                                        </div>
                               </div>

                           <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                    <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                                        <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click1" OnClientClick="javascript:return TestCheckBox();" />
                                    </div>
                                </div>
                            </div> 

                  </asp:Panel>
                         <%--</div>
                                    </div>--%>
                        <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                     <div style="float:left;text-align:left; width: 20%; padding-bottom: 10px;"">
                                        <img src="../Images/Logo1.jpg" style="height:100px;width:100px"/>
                                    </div>
                             
                                      <div style="float: left; text-align: center; width: 60%; padding-bottom: 10px;">
                                       <b> Report : Bank Statement</b>
                                    </div> 
                                      <div style="float: right; text-align: right; width: 20%; padding-bottom: 10px;">
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
                             
                          <%--  <div style="width: 100%; float: left; padding-top: 0px;" class="label">
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
                            </div>--%>

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

    <script type="text/javascript">
        //07 10 2022 Bhandavi
        //To redirect to school portal PayRollReports page.
        function RedirectToMenu() {          
            window.location = "../Client.UI/SchoolReports.aspx?Mode=SchoolPayrollReports";          
        }
</script>
</asp:Content>
