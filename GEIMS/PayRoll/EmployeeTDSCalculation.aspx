<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="EmployeeTDSCalculation.aspx.cs" Inherits="GEIMS.PayRoll.EmployeeTDSCalculation" %>
<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "GeneratePayslip.aspx/GetAllEmployeeNameForReport",
                        data: "{'prefixText':'" + request.term + "','TrustMID':'" + <%=Session[ApplicationSession.TRUSTID] %> + "','SchoolMID':'" + <%=Session[ApplicationSession.SCHOOLID] %> + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('~')[0],
                                    val: item.split('~')[1]
                                };
                            }));
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfEmployeeMID.ClientID %>").val(i.item.val);
                    $("#<%=hfEmployeeCodeName.ClientID %>").val(i.item.label);
                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
          function numeric(e) {
              var unicode = e.charCode ? e.charCode : e.keyCode;
              if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57)) {
                  return true;
              }
              else {
                  return false;
              }
          }
    </script>    
    <style type="text/css">
        .auto-style1 {
            float: left;
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Employee TDS Calculator
            &nbsp;<asp:LinkButton CausesValidation="false" ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnBack_Click">Cancel</asp:LinkButton>
        </div>
        <div id="divContent" runat="server" style="height: 100%; font-family: Verdana; padding-bottom: 10px;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left;">
                <div id="tabs" runat="server" class="gradientBoxesWithOuterShadows" visible="True" style="width: 100%; float: left;">
                    <div style="float: left; width: 100%;">
                        <ul>
                            <li><a id="tabTransferDetails" href="#tabs-2">TDS Calculator</a></li>
                        </ul>
                    </div>
                    <div id="tabs-1" class="" style="float: left; width: 99%; padding: 0 0 0 10px">
                        <div id="divSearch" style="width: 100%; float: left; padding-bottom: 10px;">
                            <%--<b><asp:TextBox ID="TextBox68" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                             <div style="width: 100%; float: left;" class="label">
                                <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                <div style="float: left; width: 100%; text-align: center; padding-top: 10px;">
                                    Employee Search :
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="Droptextarea" runat="server">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Employee Name</asp:ListItem>
                                            <asp:ListItem Value="2">Employee Code</asp:ListItem>
                                        </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                     <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] TextBox autosuggest" Width="200px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnGo" Text="Go" OnClick="btnGo_Click" CssClass="btn-blue-new btn-blue-medium Attach" />
                                </div>
                            </div>
                            <div style="text-align: center; width: 100%; float: left" class="label">
                                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvEmployee_RowCommand">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Trust/School Name">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Designation" HeaderText="Designation">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Department" HeaderText="Department">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <%--<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                    CommandName="Edit1" CommandArgument='<%#Eval("EmployeeMID")+","+ Eval("SchoolMID")%>' Height="20px" Width="20px" />--%>
                                                 <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                    CommandName="Edit1" CommandArgument='<%#Eval("EmployeeMID")%>' Height="20px" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>
                            <%--<b><asp:TextBox ID="TextBox71" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="divForm" runat="server" style="width: 100%; float: left;" class="label">
                                <div id="divEmployeeTemplate" runat="server" style="width: 100%; float: left">
                                    <%--<fieldset>
                                        <legend>Employee Template</legend>--%>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="height: 40px; text-align:center; margin-top: 10px; float: right; width: 100%; font-size:large;">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Calculate TDS on salary"></asp:Label>
                                            </strong>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; margin-left: 230px;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 1000%; margin-top:10px;">
                                                No. of months of work :
                                            </div>
                                            <div style="float: left; width: 100%; margin-top:10px; margin-top:10px;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="TextBox" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                    <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hfddlMonth" runat="server" />
                                            </div>
                                            <div style="float: left; width: 15%; margin-top:10px; display:none;" >
                                                Year :
                                            </div>
                                            <div style="float: left; width: 20%; margin-top:10px; display:none;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="TextBox" AutoPostBack="false" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                             <div style="float: left; width: 20%; margin-top:10px;">
                                                Financial Year :
                                            </div>
                                            <div style="float: left; width: 20%; margin-top:10px;">
                                                <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="TextBox" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                
                                               
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Employee Code :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayEmpCode" runat="server" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                Name :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtEmpName" runat="server" Width="220px" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                   <%-- <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Designation :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayDesignation" runat="server" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                Department :
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayDepartment" runat="server" Enabled="False" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <%--</fieldset>--%>
                                </div>
                                <div id="divAttendance" runat="server" style="width: 100%; float: left">
                                    <%--<fieldset>
                                        <legend>Attendance Template</legend>--%>
                                  <%--  <div style="width: 100%; float: left;" class="label">
                                        <div style="height: 50px; margin-top: 10px; float: left; width: 100%;">
                                            <strong>
                                                <asp:Label ID="lblEmployeePayTemplate" runat="server" Text="TDS Calculate"></asp:Label>
                                            </strong>
                                        </div>
                                    </div>--%>
                                   <%-- <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                <strong>New Employee?</strong>
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:RadioButtonList ID="rblnNewEmp" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rblnNewEmp_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                <asp:Button ID="btnChange" runat="server" Text="Change" Width="50px" Font-Size="X-Small" CssClass="btn-blue-new btn-blue-medium"
                                                    OnClick="btnChange_Click" Visible="False" style="left: -44px; top: -3px; width: 86px" />
                                                <asp:Label ID="Label12" runat="server" Text="Earned Days :"></asp:Label>
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <asp:TextBox ID="txtPayEarnedDays" CssClass="TextBox" runat="server" Enabled="False" Style="text-align:right;">0</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                   <%-- <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Total Days :
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="txtPayTotalDays" runat="server" CssClass="TextBox"
                                                    Enabled="False" Style="text-align:right;">0</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                 <%--   <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 20%;">
                                                Total Gross Rs. :
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="txtGoossRS" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div style="width: 100%; float: left;"> 
                                       <%--<div style="float: left; width: 50%;">--%>
                                        <div style="float: right; width: 79%;"> 
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <%--<strong>Taxable Salary</strong>--%>
                                                        <strong>Whether opting for taxation under Section 115BAC?</strong> Yes / No
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <!-- Grid 1-->
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <asp:GridView ID="gvEarnings" runat="server" AutoGenerateColumns="False" Width="600px"
                                                            BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" GridLines="Both"
                                                            Font-Names="verdana" Font-Size="12px" BackColor="White">
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                            <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PayItemMID" HeaderText="ID">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Name" HeaderText="Particulars" HeaderStyle-Width="50%" />
                                                                 <asp:TemplateField HeaderText="Amount Rs.">
                                                                    <ItemTemplate>
                                                                        <div style="text-align: right;">
                                                                            <asp:TextBox ID="textAmountOld" AutoPostBack="true" Enabled="false"  runat="server" CssClass="TextBox"
                                                                                Style="text-align: right; width:60px; border:none;">0</asp:TextBox>
                                                                         </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Change Amount Rs.">
                                                                    <ItemTemplate>
                                                                        <div style="text-align: right;">
                                                                            <asp:TextBox ID="textAmount" AutoPostBack="true" OnTextChanged="textAmount_TextChanged" runat="server" CssClass="TextBox"
                                                                                Style="text-align: right; width:60px; border:none;" Enabled="False">0</asp:TextBox>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <%-- <asp:BoundField DataField="Percentage" HeaderText="Percantage">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>--%>
                                                               <%-- <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckbAmount" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>                       
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </div>                                              
                                                </div>
                                               
                                                <%--Start--%>
                                                <div style="width: 110%; float: left;">
                                                     <div style="padding: 10px;">
                                                        <div style="float: left; width: 37%;">
                                                            <asp:TextBox ID="txtPayItemName3" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left; width: 17%;">
                                                            <p style="text-align: right">&nbsp;</p>
                                                            <%--<asp:TextBox ID="txtPayItem2" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>--%>                                                            
                                                        </div>
                                                        <div style="float: left; width: 10%;">
                                                            <asp:TextBox ID="txtPayItem3" runat="server" placeholder="Value Here" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="true"  OnTextChanged="txtPayItem3_TextChanged" onkeypress="return numeric(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div style="width: 110%; float: left;">
                                                     <div style="padding: 10px;">
                                                        <div style="float: left; width: 37%;">
                                                            <asp:TextBox ID="txtPayItemName4" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left; width: 17%;">
                                                            <p style="text-align: right">&nbsp;</p>
                                                            <%--<b><asp:TextBox ID="TextBox65" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                                                        </div>
                                                        <div class="auto-style1">
                                                            <asp:TextBox ID="txtPayItem4" runat="server" placeholder="Value Here" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtPayItem4_TextChanged" onkeypress="return numeric(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div style="width: 110%; float: left;">
                                                     <div style="padding: 10px;">
                                                        <div style="float: left; width: 37%;">
                                                            <asp:TextBox ID="txtPayItemName5" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left; width: 17%;">
                                                            <p style="text-align: right">&nbsp;</p>
                                                            <%--<b><asp:TextBox ID="TextBox68" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                                                        </div>
                                                        <div style="float: left; width: 10%;">
                                                            <asp:TextBox ID="txtPayItem5" runat="server" placeholder="Value Here" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtPayItem5_TextChanged" onkeypress="return numeric(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div style="width: 110%; float: left;">
                                                     <div style="padding: 10px;">
                                                        <div style="float: left; width: 37%;">
                                                            <asp:TextBox ID="txtPayItemName6" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left; width: 17%;">
                                                            <p style="text-align: right">&nbsp;</p>
                                                            <%--<b><asp:TextBox ID="TextBox71" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                                                        </div>
                                                        <div style="float: left; width: 10%;">
                                                            <asp:TextBox ID="txtPayItem6" runat="server" placeholder="Value Here" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtPayItem6_TextChanged" onkeypress="return numeric(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div style="width: 110%; float: left;">
                                                     <div style="padding: 10px;">
                                                        <div style="float: left; width: 37%;">
                                                            <asp:TextBox ID="txtPayItemName7" runat="server"  Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left; width: 17%;">
                                                            <p style="text-align: right">&nbsp;</p>
                                                            <%--<b><asp:TextBox ID="TextBox74" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                                                        </div>
                                                        <div style="float: left; width: 10%;">
                                                            <asp:TextBox ID="txtPayItem7" runat="server" placeholder="Value Here" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtPayItem7_TextChanged" onkeypress="return numeric(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="width: 110%; float: left;">
                                                     <div style="padding: 10px;">
                                                        <div style="float: left; width: 37%;">
                                                            <b>Gross Total Salary</b>
                                                        </div>
                                                        <div style="float: left; width: 17%;">
                                                           <p style="text-align: right">&nbsp;</p>
                                                           <%-- <b><asp:TextBox ID="txtPayTotalEarnings1" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                                                        </div>
                                                        <div style="float: left; width: 10%;">
                                                            <b><asp:TextBox ID="txtPayTotalEarnings" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtPayTotalEarnings_TextChanged"></asp:TextBox></b>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Less: Professional Tax
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                   <p style="text-align: right">&nbsp;</p>
                                                                   <%-- <b><asp:TextBox ID="txtProfessionalTax1" runat="server" Enabled="True" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>--%>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtProfessionalTax2" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 27%;">
                                                                    Less: HRA Excemption
                                                                </div>
                                                                 <div style="float: left; width: 27%;">
                                                                     <%--<p style="text-align: right">&nbsp;</p>--%>
                                                                    <asp:Button ID="btnCalculate" runat="server" Text="Click for HRA Calculation" OnClick="btnCalculate_Click"  CssClass="btn-blue-new btn-blue-medium" />
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtHRAExemption2" runat="server" Enabled="False" placeholder="0" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divHRAExcemption"  runat="server"  style="width: 110%; float: left;" visible="false">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 100%;">
                                                                    <b>Less: HRA Excemption Calculation</b>
                                                                        <table style="width: 120%" border="0">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblBasicSalary" runat="server" Height="30px" Text="Basic Salary"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <asp:TextBox ID="txtBasicSalary" runat="server" Width="100px" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtBasicSalary_TextChanged"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblDA" runat="server" Height="30px" Text="Dearness Allowance"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <asp:TextBox ID="txtDA" runat="server" Width="100px" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtDA_TextChanged"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblCommission" runat="server" Height="30px" Text="Commission"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <asp:TextBox ID="txtCommission" runat="server" Width="100px" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtCommission_TextChanged"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <b><asp:Label ID="lblHRATotal" runat="server" Height="30px" Text="Salary for the purpose of HRA"></asp:Label></b>   
                                                                                </td>
                                                                                <td style="width: 179px">
                                                                                    <b><asp:TextBox ID="txtHRATotal" runat="server" Enabled="false" Width="100px" placeholder="0" CssClass="TextBox" TextMode="Number" Style="text-align: right; width:100px;"></asp:TextBox></b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblhraReceived" runat="server" Height="30px" Text="HRA received"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <asp:TextBox ID="txthraReceived" runat="server" Width="100px" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txthraReceived_TextChanged"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblRentPaidHRA" runat="server" Height="30px" Text="Rent paid"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <asp:TextBox ID="txtRentPaidHRa" runat="server" Width="100px" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtRentPaidHRa_TextChanged"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <b><asp:Label ID="lblChk" runat="server" Height="30px" Text="Select if residing at metro city (Delhi, Mumbai, Chennai, Kolkata)"></asp:Label></b>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <asp:CheckBox ID="chkCheck" runat="server" Checked="false" AutoPostBack="True" OnCheckedChanged="chkCheck_CheckedChanged" />
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="width: 30% " align="left" colspan="2">
                                                                                    <b><asp:Label ID="lblMessage" runat="server" Height="30px" Text="Conditions for calculating exempted HRA - lease of the following"></asp:Label></b>
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblMessage1"  runat="server" Height="30px" Text="1. Actual HRA received"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <b><asp:Label ID="lblHRARec" runat="server" Height="30px" ></asp:Label></b>
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblMessage2" runat="server" Height="30px" Text="2. 50% or 40% of salary"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <b><asp:Label ID="lblDeductHRA" runat="server" Height="30px" ></asp:Label></b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30% " align="left">
                                                                                    <asp:Label ID="lblMessage3" runat="server" Height="30px" Text="3. Rent paid less 10% of Basic salary + DA"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 179px" >
                                                                                    <b><asp:Label ID="lblRentPaid" runat="server" Height="30px" ></asp:Label></b>
                                                                                </td>
                                                                            </tr>
                                                                            <caption>
                                                                                <br />
                                                                                <tr>
                                                                                    <td align="left" style="width: 30% ">
                                                                                        <asp:Label ID="lblMeddage4" runat="server" Height="30px" Text="Exempted HRA"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 179px"><b>
                                                                                        <asp:Label ID="lblExtendedHRA" runat="server" Height="30px"></asp:Label>
                                                                                        </b></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" style="width: 30% ">
                                                                                        <asp:Label ID="lblMessage5" runat="server" Height="30px" Text="Taxable HRA"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 179px"><b>
                                                                                        <asp:Label ID="lblTaxableHRA" runat="server" Height="30px"></asp:Label>
                                                                                        </b></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Hide" CssClass="btn-blue-new btn-blue-medium" />
                                                                                    </td>
                                                                                </tr>
                                                                            </caption>
                                                                        </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Less: Standard Deduction
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="TtxtStandardDeduction2" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 27%;">
                                                                    <b>Taxable Salary</b>
                                                                </div>
                                                                <div style="float: left; width: 27%;">
                                                                    <%--<p style="text-align: right">&nbsp;</p>--%>
                                                                    <asp:Button ID="btnTSalary" runat="server" Text="Click Here" OnClick="btnTSalary_Click"  CssClass="btn-blue-new btn-blue-medium" />
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtTaxableSalary2" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Add: any other income reported by employee</b>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   <asp:TextBox ID="txtIncome1" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtIncomeAmt1" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtIncomeAmt1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <asp:TextBox ID="txtIncome2" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtIncomeAmt2" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtIncomeAmt2_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <asp:TextBox ID="txtIncome3" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtIncomeAmt3" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtIncomeAmt3_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <asp:TextBox ID="txtIncome4" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtIncomeAmt4" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtIncomeAmt4_TextChanged"></asp:TextBox>                                                            
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <asp:TextBox ID="txtIncome5" runat="server" Enabled="true" Style="text-align: left;" CssClass="TextBox" placeholder="Text Here"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtIncomeAmt5" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtIncomeAmt5_TextChanged"></asp:TextBox>                                                            
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtTotalOtherIncome" placeholder="0" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" TextMode="Number"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Less: Interest on housing loan (enter amount in negative)
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtInterest1" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtInterest1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Eligible Amount 
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtEligibleAmt1" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtEligibleAmt2" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 27%;">
                                                                    <b>Gross total income</b>
                                                                </div>
                                                                <div style="float: left; width: 27%;">
                                                                    <asp:Button ID="btnGT" runat="server" Text="Click Here" OnClick="btnGT_Click" CssClass="btn-blue-new btn-blue-medium" />
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtGT" runat="server" placeholder="0" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Less: Deductions under chapter VI
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Section 80C - deductions for certain investments / expenses</b>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Home Loan Repayment
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtDeduction1" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtDeduction1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                     &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    LIC Premium
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtDeduction2" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtDeduction2_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                     &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    ELSS Mutual Fund
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtDeduction3" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtDeduction3_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                     &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    School Tution Fee 
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtDeduction4" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtDeduction4_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                     &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    PPF
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtDeduction5" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtDeduction5_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                     &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    &nbsp;
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtTotalDeduction" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                     &nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Eligible deduction u/s. 80C
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtEligibleDeduction1" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Section 80CCD(1B) Deduction - for investment in NPS</b>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Investment in NPS
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <asp:TextBox ID="txtNPS" runat="server" Enabled="true" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtNPS_TextChanged" placeholder="Value Here"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    &nbsp;
                                                                    <%--<asp:TextBox ID="TextBox22" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Eligible deduction u/s. 80CCD(1B)
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                    <%--<asp:TextBox ID="TextBox19" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>--%>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtEligibleDeduction2" runat="server" Enabled="False" Style="text-align: right; width:100px;" CssClass="TextBox" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Section 80D - Mediclaim</b>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <b><p style="text-align: right">Assessee, spouse, dependent children</p></b>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><p style="text-align: right">Assessee, spouse, dependent children</p></b>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <b><p style="text-align: right">Assessee's parents</p></b>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><p style="text-align: right">Assessee's parents</p></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>&nbsp;</b>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <b><p style="text-align: right">Premium amount</p></b>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><p style="text-align: right">Eligible amount</p></b>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <b><p style="text-align: right">Premium amount</p></b>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><p style="text-align: right">Eligible amount</p></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Payment for medical insurance premium (mode other than cash) /contribution to CGHS
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed1" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtMed1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed2" runat="server" Enabled="False" placeholder="0" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed3" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtMed3_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtMed4" runat="server" Enabled="False" placeholder="0" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Payment of medical insurance premium for resident Sr. Citizen – (mode other than cash)
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed5" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtMed5_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed6" runat="server" Enabled="False" placeholder="0" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed7" runat="server" Enabled="True" placeholder="Value Here" Style="text-align: right; width:100px;" CssClass="TextBox" AutoPostBack="True" OnTextChanged="txtMed7_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtMed8" runat="server" Enabled="False" placeholder="0" Style="text-align: right; width:100px;" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Payment made for preventive health check up
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed9" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtMed9_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed10" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed11" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtMed11_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtMed12" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Medical expenditure on the health of Resident senior citizen and very senior citizen for whom no amount is paid to effect/keep in force an insurance on the health)( mode of payment other than cash)
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed13" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtMed13_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed14" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 14%;">
                                                                    <asp:TextBox ID="txtMed15" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here" AutoPostBack="True" OnTextChanged="txtMed15_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtMed16" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 27%;">
                                                                    <b>Eligible deduction u/s.80D</b>
                                                                </div>
                                                                 <div style="float: left; width: 27%;">
                                                                    <asp:Button ID="btnFinal" runat="server" Text="Click Here" OnClick="btnFinal_Click" CssClass="btn-blue-new btn-blue-medium" />
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtEligibleDeduction3" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Available deductions u/s.80</b>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtAvailableDeduction" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Eligible deduction u/s.80</b>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtEligibleDeduction4" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   <b>Taxable income</b>
                                                                </div>
                                                                <div style="float: left; width: 17%;">
                                                                    <p style="text-align: right">&nbsp;</p>
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtTaxableIncome" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Tax payable
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtTaxPayable" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Less: Rebate u/s 87A if Total Income upto Rs.5 L
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtRebate1" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    Tax payable after Rebate u/s 87A
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtRebate2" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Add: Health and Education Cess (HEC) @ 4%
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtHealthEducation" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   <b>Total Tax payable</b>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtTotalTaxPayable" Style="text-align: right; width:100px; background-color:aqua;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Less: Advance tax paid by employee if any
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtAdvanceTaxPaid" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                   Less: TDS deducted by other employer / source
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <asp:TextBox ID="txtDeductEmp" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="True" placeholder="Value Here"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 37%;">
                                                                    <b>Tax payable for the year</b>
                                                                </div>
                                                                 <div style="float: left; width: 17%;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtTaxPayableYear" Style="text-align: right; width:100px;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="width: 110%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 27%;">
                                                                    <b>Monthly TDS to be deducted</b>
                                                                </div>
                                                                 <div style="float: left; width: 27%;">
                                                                   <asp:Button ID="MonthlyTDSDeducted" runat="server" Text="Click Here" OnClick="MonthlyTDSDeducted_Click" CssClass="btn-blue-new btn-blue-medium" />
                                                                </div>
                                                                <div style="float: left; width: 10%;">
                                                                    <b><asp:TextBox ID="txtMonthlyDeduct" Style="text-align: right; width:100px; background-color:aqua;" runat="server" CssClass="TextBox" Enabled="False" placeholder="0"></asp:TextBox></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                <%--End--%>
                                            </div>
                                        </div>
                                        <%--<div style="float: right; width: 50%;">
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <strong>Deduction</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <asp:GridView ID="gvDeduction" runat="server" AutoGenerateColumns="False" Width="350px"
                                                            BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px"
                                                            CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                            Font-Names="verdana" Font-Size="12px" BackColor="White">
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                            <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PayItemMID" HeaderText="ID">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Name" HeaderText="Pay Item">
                                                                    <ItemStyle Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="textAmount" runat="server" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckbAmount" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <%--<div style="float: left; width: 50%;">--%>
                                         <div style="float: right; width: 75%;"> 
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <strong>&nbsp;<%--Earnings--%>&nbsp;</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left;">
                                                <div style="padding: 10px;">
                                                    <div style="float: left; width: 100%;">
                                                        <%--<div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 25%;">
                                                                    Basic Total :
                                                                </div>
                                                                <div style="float: left; width: 49%;">
                                                                    <asp:TextBox ID="txtBasicTotal" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>         
                                                       <%-- <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Net Salary  Rs. 
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayNetSalary" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <%--<div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Net Salary  Rs.(Round Of) :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayNetSalaryRoundOf" runat="server" Enabled="False" Style="text-align: right" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                       <%-- <div style="width: 100%; float: left;">
                                                            <div style="padding: 10px;">
                                                                <div style="float: left; width: 41%;">
                                                                    Absent Days :
                                                                </div>
                                                                <div style="float: left; width: 59%;">
                                                                    <asp:TextBox ID="txtPayAbsenceDay" runat="server" AutoPostBack="True" OnTextChanged="txtPayAbsenceDay_TextChanged" CssClass="TextBox" Style="text-align:right;">0</asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div style="width: 100%; float: left; padding: 10px;">
                                        <div style="float: right; width: 100%;">
                                            <asp:GridView ID="GDBalance" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" CellPadding="4" ForeColor="Black" GridLines="Both"
                                                Font-Names="verdana" Font-Size="12px" HorizontalAlign="Center"
                                                BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px">
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                                                <Columns>
                                                    <asp:BoundField DataField="LeaveID" HeaderText="LeaveID">
                                                        <HeaderStyle CssClass="hidden" />
                                                        <ItemStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsDeduction" HeaderText="Deduction">
                                                        <HeaderStyle CssClass="hidden" />
                                                        <ItemStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" />
                                                    <asp:BoundField DataField="Total" HeaderText="Opening" />
                                                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Availed">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="textDays" AutoPostBack="true" OnTextChanged="ChangeDays" runat="server" CssClass="TextBox"
                                                                Style="text-align: center">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Balance" />
                                                </Columns>
                                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>--%>
                                   <%-- <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 100%; text-align: center">
                                                &nbsp;<asp:Label ID="lbltotal" runat="server">Total</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtTotal" runat="server" Height="19px" Enabled="False" ReadOnly="True" style="text-align:right;" CssClass="TextBox">0</asp:TextBox>
                                                <asp:Label ID="lblerror" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Red" Text="Out of Attendence Index" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Visible="False" CssClass="btn-blue-new btn-blue-medium" />
                                                
                                                <asp:Button ID="btnRedo" runat="server" Text="Redo" Enabled="False" OnClick="btnRedo_Click" CssClass="btn-blue-new btn-blue-medium" />
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div style="width: 100%; float: left; text-align: right" class="label">
                                    <div style="">
                                        <div style="float: left; width: 100%;">
                                            <asp:Button ID="btnSaveForApprovalTDS" runat="server" Text="Record Save" OnClick="btnSaveForApprovalTDS_Click" Enabled="False" CssClass="btn-blue-new btn-blue-medium" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
                            <%--<asp:AsyncPostBackTrigger ControlID="rblnNewEmp" EventName="SelectedIndexChanged" />--%>
                            <%-- <asp:AsyncPostBackTrigger ControlID="ChangePF" EventName="TextChanged" />--%>
                            <%--<asp:AsyncPostBackTrigger ControlID="txtPayAbsenceDay" EventName="TextChanged" />--%>
                            <%--<asp:AsyncPostBackTrigger ControlID="txtRentPaid" EventName="TextChanged" />--%>
                            <%-- <asp:AsyncPostBackTrigger ControlID="ChangeDays" EventName="TextChanged" />--%>
                            <%--<asp:AsyncPostBackTrigger ControlID="btnChange" EventName="Click" />--%>
                            <%--<asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btnCalculate" EventName="Click" />
                           <%-- <asp:AsyncPostBackTrigger ControlID="btnRedo" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
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
         $(document.getElementById('<%= btnSaveForApprovalTDS.ClientID %>')).click(function () {
             var valid = $("#aspnetForm").validationEngine('validate');
             var vars = $("#aspnetForm").serialize();
         });
     </script>
</asp:Content>
