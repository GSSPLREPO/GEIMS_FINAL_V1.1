<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="FeeCollection.aspx.cs" Inherits="GEIMS.Client.UI.FeeCollection" %>


<%@ Import Namespace="GEIMS.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/jquery-1.8.3.js"></script>--%>
    <link href="../CSS/ajaxCalender.css" rel="stylesheet" />
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(document).ready(function () {

            $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(0);
            $(document.getElementById('<%= txtAmountToBePaid.ClientID %>')).val(0);
            $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "FeeCollection.aspx/GetAllStudentNameForReport",
                        data: "{'prefixText':'" + request.term + "','SearchType':'" + $(document.getElementById('<%= ddlSearchBy.ClientID %>')).val() + "','SchoolMID':'" +<%=Session[ApplicationSession.SCHOOLID] %> + "'}",
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
                    // $("#<%=hfSearchName.ClientID %>").val(i.item.val);
                    $("#<%=hfSearchName.ClientID %>").val(i.item.label);
                }
            });
            $('#<%=gvFees.ClientID %> tr').each(function () {
                $(this).closest("tr").find($("[id*=txtFeeAmount]")).prop('readonly', true);
                $(this).closest("tr").find($("[id*=lblTotalAmount]")).prop('readonly', true);
                $(this).closest("tr").find($("[id*=txtRemainingAmount]")).prop('readonly', true);
            });

            $('[id$=chkHeader]').click(function () {

                $("[id$=chkChild]").attr('checked', this.checked);
            });
            $("[id$=chkChild]").click(function () {
                if ($('[id$=chkChild]').length == $('[id$=chkChild]:checked').length) {
                    $('[id$=chkHeader]').attr("checked", "checked");
                }
                else {
                    $('[id$=chkHeader]').removeAttr("checked");
                }
            });
        });

    </script>
    <style type="text/css">
        .pnlCSS {
            font-weight: bold;
            cursor: pointer;
            border: solid 1px #c0c0c0;
            width: 99%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Fees Collection
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1">Fees Collection</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <%--<ajax:ToolkitScriptManager ID="ScriptManager1" runat="server">
                        </ajax:ToolkitScriptManager>--%>
                        <div id="divStudentPanel" style="width: 100%">
                            <fieldset>
                                <legend>Search Student</legend>
                                <div class="divclasswithfloat">
                                    <div style="width: 80%; float: left;">
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Student Name</asp:ListItem>
                                            <asp:ListItem Value="2">Student GR NO</asp:ListItem>
                                            <asp:ListItem Value="3">Student Form No</asp:ListItem>
                                            <asp:ListItem Value="4">Student Unique ID</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] textarea autosuggest"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfSearchName" />
                                        &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div style="width: 20%; float: left; text-align: right">
                                        <asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new Attach" Width="50px" Text="Search"
                                            OnClick="btnGo_Click" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvStudent_RowCommand">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <Columns>

                                        <%-- <asp:BoundField DataField="StudentFirstNameEng + ' ' + StudentLastNameEng" HeaderText="Student Name">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>--%>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" Text='<%#Eval("StudentFirstNameEng") + " " + Eval("StudentLastNameEng") %> ' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class/Division">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" Text='<%#Eval("CLassName") + "-" + Eval("DivisionName") %> ' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CurrentGrNo" HeaderText="Current GR No">
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AdmissionNo" HeaderText="AdmissionNo">
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Add Fee">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/Edit.png"
                                                    CommandName="Edit1" CommandArgument='<%# Eval("StudentMID") %>' Width="20px" />
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
                            </fieldset>
                        </div>
                        <div id="divFeePanel" runat="server">
                            <div class="divclasswithfloat">
                                Mode of Payment:<span style="color: red">*</span>
                                <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="validate[required] Droptextarea" AutoPostBack="True" OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged"  >
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                    <asp:ListItem Value="3">Deposit In Bank</asp:ListItem>
                                </asp:DropDownList>
                                Select Bank:
                                  <asp:DropDownList ID="ddlBankList" runat="server" CssClass="Droptextarea">
                                      <asp:ListItem Value="">--Select--</asp:ListItem>
                                  </asp:DropDownList>
                                <div style="float: right">
                                    Date:<span style="color: red">*</span>
                                    <asp:TextBox ID="txtdate" runat="server" Width="150px" CssClass="validate[required] TextBox"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <br />
                                <br />

                                <div id="divclasswithfloat" >
                                    Cheque No.:<span style="color: red">*</span>
                                    <asp:TextBox ID="txtChequeNo" runat="server" Width="150px" CssClass="validate[required] TextBox" Enabled="false"></asp:TextBox>
                                    Bank Name:<span style="color: red">*</span>
                                    <asp:TextBox ID="txtBankName" runat="server" Width="150px" CssClass="validate[required] TextBox" Enabled="false"></asp:TextBox>
                                    Branch Name:<span style="color: red">*</span>
                                    <asp:TextBox ID="txtBranchName" runat="server" Width="150px" CssClass="validate[required] TextBox" Enabled="false"></asp:TextBox>
                                    IFS Code:<span style="color: red">*</span>
                                    <asp:TextBox ID="txtIFSCode" runat="server" Width="150px" CssClass="validate[required] TextBox" Enabled="false"></asp:TextBox>
                                </div>

                            </div>
                            <div class="clear"></div>
                            <%--  <asp:DataList ID="DataListStudent" runat="server" Width="100%">
                                    <ItemTemplate>--%>
                            <fieldset>
                                <legend>Student Details</legend>
                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Admission No:
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:Label runat="server" ID="lblAdmissionNo"></asp:Label>
                                    </div>
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        GR No:
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:Label runat="server" ID="lblCurrentGrNo"></asp:Label>
                                    </div>
                                </div>

                                <div class="divclasswithfloat">
                                    <div style="width: 20%; float: left" class="label">
                                        Name:
                                    </div>
                                    <div style="width: 80%; float: left">
                                        <%--<span><%# Eval("StudentFirstNameEng") %>+" " +<%# Eval("StudentLastNameEng") %></span></div>--%>
                                        <asp:Label runat="server" ID="lblStudentNameEng"></asp:Label>
                                    </div>
                                </div>
                                <div class="divclasswithfloat">
                                    <div style="width: 20%; float: left" class="label">
                                        Section Name:
                                    </div>
                                    <div style="width: 30%; float: left">
                                        <%-- <span><%# Eval("CurrentSectionID") %></span>--%>
                                        <asp:Label runat="server" ID="lblCurrentSection"></asp:Label>
                                    </div>
                                    <div style="width: 20%; float: left" class="label">
                                        Class-Division:
                                    </div>
                                    <div style="width: 30%; float: left">
                                        <asp:Label runat="server" ID="lblClassDivision"></asp:Label>
                                        &nbsp;(&nbsp;<asp:Label runat="server" ID="lblAcademicYear"></asp:Label>&nbsp;)&nbsp;
                                    </div>
                                </div>
                                <div class="divclasswithfloat">
							        <div style="text-align: left; width: 20%; float: left;" class="label">
								        Budget Category :<span class="Required">*</span>
							        </div>
							        <div style="text-align: left; width: 30%; float: left;">
								        <asp:DropDownList ID="ddlBudgetCategory"  CssClass="Droptextarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetCategory_SelectedIndexChanged" >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
							        </div>                                  
						        </div>
						        <div class="divclasswithfloat">
						             <div style="text-align: left; width: 20%; float: left;" class="label">
								        Budget Heading :<span class="Required">*</span>
							        </div>
							        <div style="text-align: left; width: 30%; float: left;">
								         <asp:DropDownList ID="ddlBudgetHeading"  CssClass="Droptextarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetHeading_SelectedIndexChanged" >
									           <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                         </asp:DropDownList>
							        </div>
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
								        Budget SubHeading :<span class="Required">*</span>
							        </div>
							        <div style="text-align: left; width: 30%; float: left;">
								        <asp:DropDownList ID="ddlBudgetSubHeading"  CssClass="Droptextarea" runat="server" AutoPostBack="True"    >
                                              <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>    
							        </div>
						        </div>
                            </fieldset>


                            <%-- </ItemTemplate>
                                </asp:DataList>--%>

                            <fieldset>
                                <legend>Past Fees Details</legend>


                                <div id="divPastFees" style="width: 100%" runat="server">
                                    <asp:Panel ID="pnlClick" runat="server" CssClass="pnlCSS">
                                        <div style="height: 30px; vertical-align: middle">
                                            <div style="float: left; color: black; padding: 5px 5px 0 0">
                                                Fee Panel
                                            </div>
                                            <div style="float: right; color: Black; padding: 5px 5px 0 0">
                                                <asp:Label ID="lblMessage" runat="server" Text="Label" />
                                                <asp:Image ID="imgArrows" runat="server" />
                                            </div>
                                            <div style="clear: both"></div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlCollapsable" runat="server" Height="0" CssClass="pnlCSS">
                                        <asp:Label runat="server" ID="lblmsg" CssClass="message"></asp:Label>
                                        <br />
                                        <asp:GridView ID="gvPastFees" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="gvPastFees_RowDataBound" OnRowCommand="gvPastFees_OnRowCommand">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ReceiptNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRcNo" Text='<%# Eval("FYear") + "/" + Eval("ReceiptNo") %> ' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FeesName" HeaderText="Fees Name">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Date" HeaderText="Date">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Paid Fees Amount In ₹">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFeeAmount" runat="server" Text='<%# Eval("FeesAmount") %>' CssClass="label"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalPaidFees" runat="server" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ScholarShip In ₹">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount") %>' CssClass="label"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalGivenDiscount" runat="server" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Re-Print">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/Print.png"
                                                            CommandName="Print1" CommandArgument='<%# Eval("ReceiptNo") %>' Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                </asp:TemplateField>

                                            </Columns>

                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" ForeColor="White" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        <br />
                                    </asp:Panel>
                                </div>
                            </fieldset>

                            <div style="width: 100%" id="divFeeVisibility" runat="server">
                                <fieldset>
                                    <legend>Fees Details</legend>
                                    <asp:Label runat="server" ID="lblFee" ForeColor="Red"></asp:Label>
                                    <div style="width: 100%">
                                        <asp:GridView ID="gvFees" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="gvFees_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="ClassWiseFeesTemplateTID" HeaderText="ClassWiseFeesTemplateTID ID">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                    <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                </asp:BoundField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<input type="checkbox" id="chkChild" />--%>
                                                        <asp:CheckBox ID="chkChild" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FeesName" HeaderText="Fees Name">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FeesType" HeaderText="Fees Category">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Academic Year">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblAcademicYear" runat="server" ReadOnly="true" Width="100px" Text='<%# Eval("AcademicYear") %>' CssClass="txtFeeAmount TextBox" BorderWidth="0px" ForeColor="Black">0</asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="ScholarShip In ₹">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDiscountAmount" runat="server" Width="100px" CssClass="txtDiscount TextBox"
                                                            onchange='<%# "feesCalc(" + 2 + ",this.value"+ "," + Eval("FeesAmount") + "," + (Container.DataItemIndex + 1) +");" %>' onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;">0</asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotalDiscount" runat="server" CssClass="lblDiscount" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fee Amount In ₹">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFeeAmount" runat="server" Width="100px" Text='<%# Eval("FeesAmount") %>' CssClass="txtFeeAmount TextBox">0</asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFeeAmount" runat="server" CssClass="lblFeeAmount" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount In ₹">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotalAmount" runat="server" Width="100px"
                                                            onchange='<%# "feesCalc(" + 1 + ",this.value"+ "," + Eval("FeesAmount") + "," + (Container.DataItemIndex + 1) +");" %>'
                                                            onkeypress="return PreventDecimalPoint(event)" Text='<%# Eval("FeesAmount") %>' CssClass="txtTotalAmount TextBox">0</asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server" CssClass="lblTotalAmount" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remaining Amount.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemainingAmount" runat="server" Width="100px" CssClass="txtRemainingAmount TextBox">0</asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblRemainingAmount" runat="server" CssClass="lblRemainingAmount" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FeeGroupID" HeaderText="FeesGroup ID">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                    <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LedgerID" HeaderText="Ledger ID">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                    <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ClassMID" HeaderText="ClassMID">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                    <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DivisionTID" HeaderText="DivisionTID">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                    <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                </asp:BoundField>
                                            </Columns>

                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" ForeColor="White" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                    <div id="pnlFees" runat="server">
                                        <%--<div style="text-align: right" class="divclasswithfloat">
                                            <button id="btnCalculate" type="button" class="btn-blue-new btn-blue-medium">Calculate</button>
                                        </div>--%>
                                        <div class="divclasswithfloat">
                                            <div style="width: 20%; float: left" class="label">
                                                Sum Of Amount To Be Paid:
                                            </div>
                                            <div style="width: 30%; float: left">
                                                <asp:TextBox ID="txtAmountToBePaid" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox" Enabled="false">0</asp:TextBox>
                                            </div>
                                            <div style="width: 20%; float: left" class="label">
                                                Scholarship Amount:
                                            </div>
                                            <div style="width: 30%; float: left;">
                                                <asp:TextBox ID="txtDiscountedAmount" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox" Enabled="false">0</asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="clear"></div>
                                        <div class="divclasswithfloat">
                                            <div style="width: 20%; float: left" class="label">
                                                Amount  Paid:
                                            </div>
                                            <div style="width: 30%; float: left">
                                                <asp:TextBox ID="txtAmountPaid" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox">0</asp:TextBox>
                                                <asp:TextBox ID="txtFullAmount" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox hidden">0</asp:TextBox>
                                            </div>
                                            <div style="width: 20%; float: left" class="label">
                                                Remaining Amount:
                                            </div>
                                            <div style="width: 30%; float: left;">
                                                <div style="width: 60%; float: left;">
                                                    <asp:TextBox ID="txtRemainingAmountFoot" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox" Enabled="false">0</asp:TextBox>
                                                </div>
                                                <div style="width: 40%; float: left; text-align: right">
                                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach" Enabled="false" OnClick="btnSave_Click" />&nbsp;
                                                </div>
                                            </div>

                                        </div>
                                        <div class="clear"></div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
                <ajax:CollapsiblePanelExtender
                    ID="CollapsiblePanelExtender1"
                    runat="server"
                    CollapseControlID="pnlClick"
                    Collapsed="true"
                    ExpandControlID="pnlClick"
                    TextLabelID="lblMessage"
                    CollapsedText="Show"
                    ExpandedText="Hide"
                    ImageControlID="imgArrows"
                    CollapsedImage="../Images/plus.png"
                    ExpandedImage="../Images/minus-icon.png"
                    ExpandDirection="Vertical"
                    TargetControlID="pnlCollapsable"
                    ScrollContents="false">
                </ajax:CollapsiblePanelExtender>
                <div id="divFeeCollectionPrint" style="width: 100%; display: none; text-align: center; padding: 0 20px 0 20px">
                    <table style="width: 100%; text-align: center; padding: 0 10px 0 10px;display:none;">
                        <tr>
                            <td style="text-align: center; width: 100%;">
                                <table style="width: 100%; text-align: center; font-family: Verdana; font-size: 10px; border: solid 1px black;">
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%; border-bottom: 1px solid black">
                                                <tr>
                                                    <td colspan="4" style="text-align: center;">
                                                        <b style="font-size: 20px;"><%=Session["SCHOOLNAME"] %></b><br />
                                                        <b style="font-size: 13px;"></b>
                                                        <br />

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="4" align="center" style="padding: 10px 0 10px 0">
                                                        <b style="font-size: 13px;">RECEIPT</b><br />
                                                        <b style="font-size: 9px;">(FY:<asp:Label runat="server" ID="lblFinancialYear"></asp:Label>)</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left">
                                            <span style="font-weight: bold">Receipt No. :</span>
                                            <asp:Label runat="server" ID="lblVoucherNo"></asp:Label>
                                        </td>
                                        <td style="width: 34%; text-align: left">
                                            <span style="font-weight: bold">Date :</span>
                                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left">
                                            <span style="font-weight: bold">Student Name :</span>
                                            <asp:Label runat="server" ID="lblStudentName"></asp:Label>
                                        </td>
                                        <td style="width: 34%; text-align: left">
                                            <span style="font-weight: bold">Std :</span>
                                            <asp:Label runat="server" ID="lblStd"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="border-top: 1px solid Black"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center; vertical-align: top; height: 270px">
                                            <asp:GridView ID="gvReport" runat="server" BackColor="White" Width="100%" BorderColor="Black" AutoGenerateColumns="False" OnRowDataBound="gvReport_RowDataBound"
                                                CellPadding="4" Font-Names="Verdana" Font-Size="10px" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="FeeGroupName" HeaderText="FeeGroup Name">
                                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="FeesAmount(₹)" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTotalAmount" runat="server" Width="10%" Text='<%# Eval("FeesAmount") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Paid Fees(₹)" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTotalAmount" runat="server" Width="10%" Text='<%# Eval("PaidFees") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPaidAmount" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scholarship(₹)" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtDiscount" runat="server" Width="10%" Text='<%# Eval("ScholarShip") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblDiscount" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currently Paid(₹)" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCurrentAmount" runat="server" Width="10%" Text='<%# Eval("CurrentlyPaid") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCurrentAmount" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pending Fees(₹)" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTotalAmount" runat="server" Width="10%" Text='<%# Eval("PendingFees") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPendingAmount" runat="server" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle BackColor="White" />
                                                <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                                    BorderWidth="1px" BorderStyle="Solid" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Recieved With Thanks Rupees : </b>&nbsp;
                                        <asp:Label ID="lblcurAmount1" runat="server" Font-Bold="true" />
                                            &nbsp;<b>(Rs).</b>
                                            <asp:Label ID="lblCurAmountInt1" runat="server" Font-Bold="true" /><b>/-)</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold; height: 10px;" colspan="2"></td>
                                        <td style="text-align: right; font-weight: bold" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold" colspan="2">Authorized Person Sign
                                        </td>
                                        <td style="text-align: right; font-weight: bold" colspan="2">Parent/Guardian Sign
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold; height: 10px;" colspan="2"></td>
                                        <td style="text-align: right; font-weight: bold" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold; height: 10px;" colspan="2"></td>
                                        <td style="text-align: right; font-weight: bold" colspan="2"></td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                 <%--   <div style="height: 20px"></div>--%>
                    <table style="width: 100%; text-align: center; padding: 0 10px 0 10px;display:none;">
                        <tr>
                            <td style="text-align: center; width: 100%;">
                                <table style="width: 100%; text-align: center; font-family: Verdana; font-size: 10px; border: solid 1px black;">
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%; border-bottom: 1px solid black">
                                                <tr>
                                                    <td colspan="4" style="text-align: center;">
                                                        <b style="font-size: 20px;"><%=Session["SCHOOLNAME"] %></b><br />
                                                        <b style="font-size: 13px;"></b>
                                                        <br />

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="4" align="center" style="padding: 10px 0 10px 0">
                                                        <b style="font-size: 13px;">RECEIPT</b><br />
                                                        <b style="font-size: 9px;">(FY:<asp:Label runat="server" ID="lblFYear"></asp:Label>)</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left">
                                            <span style="font-weight: bold">Receipt No. :</span>
                                            <asp:Label runat="server" ID="lblVoucherNo1"></asp:Label>
                                        </td>
                                        <td style="width: 34%; text-align: left">
                                            <span style="font-weight: bold">Date :</span>
                                            <asp:Label runat="server" ID="lblDate1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left">
                                            <span style="font-weight: bold">Student Name :</span>
                                            <asp:Label runat="server" ID="lblStudentName1"></asp:Label>
                                        </td>
                                        <td style="width: 34%; text-align: left">
                                            <span style="font-weight: bold">Std :</span>
                                            <asp:Label runat="server" ID="lblStd1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="border-top: 1px solid Black"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center; vertical-align: top; height: 270px">
                                            <asp:GridView ID="gvReport1" runat="server" BackColor="White" Width="100%" BorderColor="Black" AutoGenerateColumns="False" OnRowDataBound="gvReport1_RowDataBound"
                                                CellPadding="4" Font-Names="Verdana" Font-Size="10px" ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField DataField="FeeGroupName" HeaderText="FeeGroup Name">
                                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="FeesAmount(₹)" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTotalAmount1" runat="server" Width="10%" Text='<%# Eval("FeesAmount") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalAmount1" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Paid Fees(₹)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPaidAmount1" runat="server" Width="10%" Text='<%# Eval("PaidFees") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPaidAmount1" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scholarship(₹)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtDiscount1" runat="server" Width="10%" Text='<%# Eval("ScholarShip") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblDiscount1" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currently Paid(₹)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCurrentAmount1" runat="server" Width="10%" Text='<%# Eval("CurrentlyPaid") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCurrentAmount1" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pending Fees(₹)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPendingAmount1" runat="server" Width="10%" Text='<%# Eval("PendingFees") %>' CssClass="TextBox">0</asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPendingAmount1" runat="server" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle BackColor="White" />
                                                <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                                    BorderWidth="1px" BorderStyle="Solid" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Recieved With Thanks Rupees :</b> &nbsp;
                                        <asp:Label ID="lblcurAmount" runat="server" Font-Bold="true" />
                                            &nbsp;<b>(₹</b>
                                            <asp:Label ID="lblCurAmountInt" runat="server" Font-Bold="true" /><b>/-)</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold; height: 10px;" colspan="2"></td>
                                        <td style="text-align: right; font-weight: bold" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold" colspan="2">Authorized person Sign
                                        </td>
                                        <td style="text-align: right; font-weight: bold" colspan="2">Parent/Guardian Sign
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold; height: 10px;" colspan="2"></td>
                                        <td style="text-align: right; font-weight: bold" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; font-weight: bold; height: 10px;" colspan="2"></td>
                                        <td style="text-align: right; font-weight: bold" colspan="2"></td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>




                    <%-- Print Fees Receipts --%>


                    <%--1st recipt --%>
                    <table border="0.5" style="font-family: Verdana; font-size: 10px; margin-left: 230px; font-weight: normal; width: 110px; float: left; border-color: lightgray"
                        cellspacing="0px" cellpadding="1px">
                        <tr>
                            <td colspan="3" style="height: 90px;"></td>
                        </tr>


                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                        
                                     
                                        <%=Session["SCHOOLNAME"] %>

                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                            <%= lblCurrentSection.Text %>     <br />
                                      Student Copy
                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                      BANK OF BARODA<br />
                                        FertilizerNagar Township Branch,
                                        FertilizerNagar,Dist.Vadodara.
                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                   Account No :       <b> <%= ViewState["AccountNumber"]   %> </b>

                                    </b>
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Rec No. </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <b> <%= lblVoucherNo.Text   %> </b>
                                 
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Date:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                 
                                  <%=  lblDate1.Text   %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">GR No. :
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblCurrentGrNo.Text %>
                        
								

                                </font></td>

                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">STD-Div :
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblStd.Text %>
                        
								

                                </font></td>

                        </tr>

                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Name:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblStudentName.Text %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Contact No.:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                   <%=  ViewState["StudentContactNumber"]   %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Fees for the year:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                 <%=   lblAcademicYear.Text  %>
                                </font>
                            </td>
                        </tr>


                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Name of ClassTeacher:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <asp:DataList ID="dlBindFeesGrid" runat="server" OnItemDataBound="dlBindFeesGrid_ItemDataBound" BorderWidth="1">
                                    <HeaderTemplate>
                                        <table id="perYear" width="320px" style="font-family: Verdana; font-size: 10px; font-weight: normal;"
                                            cellspacing="0px" cellpadding="0px">
                                            <tr>
                                                <td colspan="2" align="left" width="180px" style="font-weight: bold;">FeeGroupName
                                                
                                                </td>
                                                <td colspan="3" align="center" style="font-weight: bold; text-align: right;">Amount
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2" align="left" width="180px">
                                                <%# Eval("FeeGroupName")%>
                                            </td>
                                            <td colspan="3" align="right" width="100px">
                                                <%# Eval("CurrentlyPaid")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                        </tr>

                          <tr>
                            <td class="auto-style4" style="font-weight:bold;">
                                <font size="1.5">Total:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="right">
                                <font size="1.5">
                             <%=   ViewState["TotalPaidAmount"]   %>
                                    <%--  <%=   lblTotalAmount.Text  %>--%>
                                </font>
                            </td>
                        </tr>



                        <tr>

                            <td class="auto-style5" colspan="2" align="left">

                                <font size="1.5">Payment Type:</font>
                            </td>
                            <td align="right" class="auto-style4">
                                <font size="1.5">       <%=    ViewState["ModeOfPayment"]   %>  </font>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" align="right">
                                <br />
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style4" style="height: 70px;">
                                <font size="1.5">


                                    <b>Rupees in words:
                                    </b>
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="center">
                                <font size="1.5">

                                    <b><%=ViewState["AMTinWords"] %></b></font>
                            </td>
                        </tr>

                        <tr>
                            <td rowspan="8" colspan="3">

                                <font size="1.5" color="red">
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       Deposited By
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       Cashier sign
                                </font>
                            </td>
                        </tr>


                    </table>
                    <%--1st recipt --%>

                    <%-- border --%>
                    <table style="border-right: 1px dotted black; margin-right: 5px; font-family: Verdana; font-size: 10px; font-weight: normal; width: 10px; float: left"
                        cellspacing="0px" align="center">
                        <tr>

                            <td style="height: 790px" align="center"></td>


                        </tr>
                    </table>
                    <%-- border --%>

                    <%--2nd recipt --%>
                    <table border="0.5" style="font-family: Verdana; font-size: 10px; margin-left: 5px; font-weight: normal; width: 110px; float: left; border-color: lightgray"
                        cellspacing="0px" cellpadding="1px">
                        <tr>
                            <td colspan="3" style="height: 90px;"></td>
                        </tr>


                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                        
                                     
                                        <%=Session["SCHOOLNAME"] %>

                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                            <%= lblCurrentSection.Text %>     <br />
                                     SCHOOL OFFICE COPY
                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                      BANK OF BARODA<br />
                                        FertilizerNagar Township Branch,
                                        FertilizerNagar,Dist.Vadodara.
                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                   Account No : <%= ViewState["AccountNumber"]   %>

                                    </b>
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Rec No. </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <b> <%= lblVoucherNo.Text   %> </b>
                                 
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Date:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                 
                                  <%=  lblDate1.Text   %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">GR No. :
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblCurrentGrNo.Text %>
                        
								

                                </font></td>

                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">STD-Div :
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblStd.Text %>
                        
								

                                </font></td>

                        </tr>

                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Name:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblStudentName.Text %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Contact No.:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%=  ViewState["StudentContactNumber"]   %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Fees for the year:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                 <%=   lblAcademicYear.Text  %>
                                </font>
                            </td>
                        </tr>


                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Name of ClassTeacher:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <asp:DataList ID="dlBindFeesGridForSchool" runat="server" OnItemDataBound="dlBindFeesGrid_ItemDataBound" BorderWidth="1">
                                    <HeaderTemplate>
                                        <table id="perYear" width="320px" style="font-family: Verdana; font-size: 10px; font-weight: normal;"
                                            cellspacing="0px" cellpadding="0px">
                                            <tr>
                                                <td colspan="2" align="left" width="180px" style="font-weight: bold;">FeeGroupName
                                                
                                                </td>
                                                <td colspan="3" align="center" style="font-weight: bold; text-align: right;">Amount
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2" align="left" width="180px">
                                                <%# Eval("FeeGroupName")%>
                                            </td>
                                            <td colspan="3" align="right" width="100px">
                                                <%# Eval("CurrentlyPaid")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                        </tr>


                          <tr>
                            <td class="auto-style4" style="font-weight:bold;">
                                <font size="1.5">Total:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="right">
                                <font size="1.5">
                             <%=   ViewState["TotalPaidAmount"]   %>
                                    <%--  <%=   lblTotalAmount.Text  %>--%>
                                </font>
                            </td>
                        </tr>


                        <tr>

                            <td class="auto-style5" colspan="2" align="left">

                                <font size="1.5">Payment Type:</font>
                            </td>
                            <td align="right" class="auto-style4">
                                <font size="1.5">      <%=    ViewState["ModeOfPayment"]   %>    </font>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" align="right">
                                <br />
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style4" style="height: 70px;">
                                <font size="1.5">


                                    <b>Rupees in words:
                                    </b>
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="center">
                                <font size="1.5">

                                    <b><%=ViewState["AMTinWords"] %></b></font>
                            </td>
                        </tr>

                        <tr>
                            <td rowspan="8" colspan="3">

                                <font size="1.5" color="red">
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       Deposited By
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       Cashier sign
                                </font>
                            </td>
                        </tr>


                    </table>
                    <%--2nd recipt --%>

                    <%-- border --%>
                    <table style="border-right: 1px dotted black; margin-right: 5px; font-family: Verdana; font-size: 10px; font-weight: normal; width: 10px; float: left"
                        cellspacing="0px" align="center">
                        <tr>

                            <td style="height: 790px" align="center"></td>


                        </tr>
                    </table>
                    <%-- border --%>

          

                

                    <%--4th recipt--%>
                    <table border="0.5" style="font-family: Verdana; font-size: 10px; margin-left: 5px; font-weight: normal; width: 110px; float: left; border-color: lightgray"
                        cellspacing="0px" cellpadding="1px">
                        <tr>
                            <td colspan="3" style="height: 90px;"></td>
                        </tr>


                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                        
                                     
                                        <%=Session["SCHOOLNAME"] %>

                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                            <%= lblCurrentSection.Text %>     <br />
                                   BANK COPY
                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                      BANK OF BARODA<br />
                                        FertilizerNagar Township Branch,
                                        FertilizerNagar,Dist.Vadodara.
                                    </b>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style6" style="font-size: 18px;">
                                <font color="Darkblue" size="2.5">
                                    <b>
                                   Account No : <%= ViewState["AccountNumber"]   %>

                                    </b>
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Rec No. </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <b> <%= lblVoucherNo.Text   %> </b>
                                 
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Date:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                 
                                  <%=  lblDate1.Text   %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">GR No. :
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblCurrentGrNo.Text %>
                        
								

                                </font></td>

                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">STD-Div :
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblStd.Text %>
                        
								

                                </font></td>

                        </tr>

                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Name:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%= lblStudentName.Text %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Contact No.:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                    <%=  ViewState["StudentContactNumber"]   %>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Fees for the year:
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                 <%=   lblAcademicYear.Text  %>
                                </font>
                            </td>
                        </tr>


                        <tr>
                            <td class="auto-style4">
                                <font size="1.5">Name of ClassTeacher:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="left">
                                <font size="1.5">
                                
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <asp:DataList ID="dlBindFeesGridForBank" runat="server" OnItemDataBound="dlBindFeesGrid_ItemDataBound" BorderWidth="1">
                                    <HeaderTemplate>
                                        <table id="perYear" width="320px" style="font-family: Verdana; font-size: 10px; font-weight: normal;"
                                            cellspacing="0px" cellpadding="0px">
                                            <tr>
                                                <td colspan="2" align="left" width="180px" style="font-weight: bold;">FeeGroupName
                                                
                                                </td>
                                                <td colspan="3" align="center" style="font-weight: bold; text-align: right;">Amount
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2" align="left" width="180px">
                                                <%# Eval("FeeGroupName")%>
                                            </td>
                                            <td colspan="3" align="right" width="100px">
                                                <%# Eval("CurrentlyPaid")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                        </tr>


                          <tr>
                            <td class="auto-style4" style="font-weight:bold;">
                                <font size="1.5">Total:</font>
                            </td>
                            <td colspan="2" class="auto-style5" align="right">
                                <font size="1.5">
                             <%=   ViewState["TotalPaidAmount"]   %>
                                    <%--  <%=   lblTotalAmount.Text  %>--%>
                                </font>
                            </td>
                        </tr>


                        <tr>

                            <td class="auto-style5" colspan="2" align="left">

                                <font size="1.5">Payment Type:</font>
                            </td>
                            <td align="right" class="auto-style4">
                                <font size="1.5">       <%=    ViewState["ModeOfPayment"]   %>   </font>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" align="right">
                                <br />
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style4" style="height: 70px;">
                                <font size="1.5">


                                    <b>Rupees in words:
                                    </b>
                                </font>
                            </td>
                            <td colspan="2" class="auto-style5" align="center">
                                <font size="1.5">

                                    <b><%=ViewState["AMTinWords"] %></b></font>
                            </td>
                        </tr>

                        <tr>
                            <td rowspan="8" colspan="3">

                                <font size="1.5" color="red">
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       Deposited By
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       Cashier sign
                                </font>
                            </td>
                        </tr>


                    </table>
                    <%--4th recipt--%>
                    </form>
                        
                 
                    <%-- Print Tuition Fees Receipts --%>
                </div>
            </div>
        </div>





        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
    </div>

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        var AcademicYear;
        $('[id*=chkHeader]').click(function () {

            if ($(this).is(":checked")) {

                $('[id*=chkChild]').prop("checked", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkChild]').prop("checked", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });
        $("[id*=chkChild]").click(function () {
            if ($(this).is(":checked")) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            } else {
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                    $('[id*=chkHeader]').prop("checked", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                }
            }
            // alert("chkChild");
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                $('[id*=chkHeader]').prop("checked", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkHeader]').removeAttr("checked");
            }
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });


        //var totalAmount = 0, TotalDiscount = 0, Total = 0, TotalAmountnotCheck = 0;
        $('#<%=gvFees.ClientID %>').find("input:checkbox").click(function () {

            if ($(this).is(":checked")) {
                //  alert('Yes');
                AcademicYear = $(this).closest("tr").find($("[id*=lblAcademicYear]")).val();
                // alert(AcademicYear);
                //$(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);
                var Discount = parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
                var Amount = parseFloat($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
                var Total = parseFloat(Amount - Discount);
                if (Discount > Amount) {
                    alert("Scholarship amount can not greater then Fees amount");
                    $(this).closest("tr").find($("[id*=txtDiscountAmount]")).val(0);
                    $(this).closest("tr").find($("[id*=txtTotalAmount]")).val(Amount);
                    $(this).closest("tr").find($("[id*=txtRemainingAmount]")).val(0);
                }
                if ($(this).closest("tr").find($("[id*=txtTotalAmount]")).val() > Amount) {
                    alert('Total Amount should not greater then fees Amount.');
                    $(this).closest("tr").find($("[id*=txtTotalAmount]")).val(Amount);
                    $(this).closest("tr").find($("[id*=txtRemainingAmount]")).val(0);
                }
                if ($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val() == '') {
                    $(this).closest("tr").find($("[id*=txtDiscountAmount]")).val(0)
                    $(this).closest("tr").find($("[id*=txtTotalAmount]")).val(Amount);
                    $(this).closest("tr").find($("[id*=txtRemainingAmount]")).val(0);
                }
                if ($(this).closest("tr").find($("[id*=txtTotalAmount]")).val() == '') {
                    $(this).closest("tr").find($("[id*=txtTotalAmount]")).val(Amount);
                    $(this).closest("tr").find($("[id*=txtRemainingAmount]")).val(0);
                }
                var Count = parseFloat(parseFloat($(this).closest("tr").find($("[id*=txtTotalAmount]")).val()) + Discount);
                //alert(Count);
                if (Amount < Count) {
                    $(this).closest("tr").find($("[id*=txtDiscountAmount]")).val(0);
                    $(this).closest("tr").find($("[id*=txtTotalAmount]")).val(Amount);
                    $(this).closest("tr").find($("[id*=txtRemainingAmount]")).val(0);
                }
                calculatefooter();

                $(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('readonly', true);
                $(this).closest("tr").find($("[id*=txtTotalAmount]")).prop('readonly', true);
                // $(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('disabled', true);
                CalculateFee();
                //alert(Discount + Amount + Total);
            } else {
                $(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('readonly', false);
                $(this).closest("tr").find($("[id*=txtTotalAmount]")).prop('readonly', false);
                CalculateFee();
            }

        });

        function feesCalc(param, paramval, feesAmount, index) {

            var Discount, TotalAmount;
            var TotalAmountId, DiscountAmountId, getIndex, RemainingAmt, RemainingAmtId;
            getIndex = parseInt(index + 1);
            if (getIndex < 10) {
                TotalAmountId = '#ctl00_ContentPlaceHolder1_gvFees_ctl0' + getIndex.toString() + '_txtTotalAmount';
                DiscountAmountId = '#ctl00_ContentPlaceHolder1_gvFees_ctl0' + getIndex.toString() + '_txtDiscountAmount';
                RemainingAmtId = '#ctl00_ContentPlaceHolder1_gvFees_ctl0' + getIndex.toString() + '_txtRemainingAmount';
            } else {
                TotalAmountId = '#ctl00_ContentPlaceHolder1_gvFees_ctl' + getIndex.toString() + '_txtTotalAmount';
                DiscountAmountId = '#ctl00_ContentPlaceHolder1_gvFees_ctl' + getIndex.toString() + '_txtDiscountAmount';
                RemainingAmtId = '#ctl00_ContentPlaceHolder1_gvFees_ctl' + getIndex.toString() + '_txtRemainingAmount';
            }


            if (param == 2) {
                //paramId = 
                //var k = $(paramId).val();
                //alert(k);

                Discount = parseFloat(paramval);
                TotalAmount = parseFloat(feesAmount - Discount);
                $(TotalAmountId).val(TotalAmount.toString());
            } else {
                Discount = parseFloat($(DiscountAmountId).val());
                TotalAmount = parseFloat(paramval);
            }

            RemainingAmt = parseFloat(feesAmount - (TotalAmount + Discount));
            $(RemainingAmtId).val(RemainingAmt);

            validateFees(Discount, FeesAmount, TotalAmount, TotalAmountId, DiscountAmountId, RemainingAmtId);

            //var rowIndex = $(this).closest("tr").prevAll("tr").length;
            //rowIndex++;
            //$(this).closest("tr").find($("[id=ctl00_ContentPlaceHolder1_gvFees_ctl0" + rowIndex + "_txtRemainingAmount]")).val(RemainingAmt);
            calculatefooter();
        }

        function validateFees(Discount, FeesAmount, TotalAmount, TotalAmountId, DiscountAmountId, RemainingAmtId) {

            var Total = parseFloat(parseFloat(Discount) + parseFloat(TotalAmount));
            if (Total > FeesAmount) {
                alert("Sum of Scholarship and Total Amount is not larger than Fees Amount.");
                $(DiscountAmountId).val(0);
                $(TotalAmountId).val(0);
                $(RemainingAmtId).val(FeesAmount);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            } else {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
            }
        }

        //$('[id*=txtDiscountAmount]').change(function () {

        //  var Discount = parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
        //var Amount = parseFloat($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
        //var Total = parseFloat(Amount - Discount);
        //alert($(this).closest("tr").find($("[id*=txtRemainingAmount]")).val());

        // feesCalc(Discount, Amount, Total);

        //});

        //$('[id*=txtTotalAmount]').change(function () {
        //  var Discount = parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
        //var Amount = parseFloat($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
        //var Total = parseFloat($(this).closest("tr").find($("[id*=txtTotalAmount]")).val());
        //feesCalc(Discount, Amount, Total);
        //calculatefooter();
        //});

        $(document.getElementById('<%= txtAmountPaid.ClientID %>')).change(function () {
            calculatefooter();
            var RemainingAmont = parseFloat($('.lblTotalAmount').text()) - parseFloat($(document.getElementById('<%= txtAmountPaid.ClientID %>')).val());
            $(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(RemainingAmont);
        });

        function EnableCheckBox() {
            $('#<%=gvFees.ClientID %> tr').each(function () {
                if (AcademicYear == $(this).closest("tr").find($("[id*=lblAcademicYear]")).val()) {
                    alert('Yup');
                }
                else {
                    alert('Nope');
                }
            });
        }

        function CalculateFee() {
            var totalAmount = 0.0, FeesAmount = 0.0, TotalDiscount = 0.0, Total = 0.0, RemainingAmount = 0.0, TotalAmountnotCheck = 0.0;
            var i = 0;
            var RowCount = $('#<%=gvFees.ClientID %> tr').length;

            $('#<%=gvFees.ClientID %> tr').each(function () {

                if (i != 0) {
                    // alert('RowCount' + (RowCount - 1));
                    if (i != RowCount - 1) {
                        if ($(this).find('input:checkbox').is(":checked")) {
                            // alert("Checked");
                            //alert($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
                            TotalDiscount += parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
                            $('.lblDiscount').text(TotalDiscount);

                            FeesAmount += parseFloat($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
                            $('.lblFeeAmount').text(FeesAmount);

                            totalAmount += parseFloat($(this).closest("tr").find($("[id*=txtTotalAmount]")).val());
                            RemainingAmount += parseFloat($(this).closest("tr").find($("[id*=txtRemainingAmount]")).val());
                            $('.lblRemainingAmount').text(RemainingAmount);

                            $(document.getElementById('<%= txtAmountToBePaid.ClientID %>')).val(FeesAmount);
                            $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(totalAmount);
                            $(document.getElementById('<%= txtDiscountedAmount.ClientID %>')).val(TotalDiscount);
                            $(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(RemainingAmount);
                            $(document.getElementById('<%= btnSave.ClientID %>')).prop('disabled', false);
                            //$(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('disabled', true);
                        }
                        ////else {
                        ////$(this).closest("tr").find($("[id*=txtDiscountAmount]")).val(0);
                        ////$(this).closest("tr").find($("[id*=txtTotalAmount]")).val($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
                        ////$(this).closest("tr").find($("[id*=txtRemainingAmount]")).val(0);
                        ////var c = $(this).closest("tr").find($("[id*=txtFeeAmount]")).val();
                        // totalAmount += parseFloat(c);
                        //TotalAmountnotCheck += parseFloat($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
                        //$(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(TotalAmountnotCheck);
                        ////}
                    }
                }
                i = i + 1;

            });

            var Amount = parseFloat(totalAmount) + parseFloat(TotalAmountnotCheck);
            $('.lblTotalAmount').text(Amount);
            $(document.getElementById('<%= txtFullAmount.ClientID %>')).val(Amount);
            if (Amount == TotalAmountnotCheck) {
                $(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(0);
                $(document.getElementById('<%= txtAmountToBePaid.ClientID %>')).val(0);
                $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);
                $(document.getElementById('<%= txtDiscountedAmount.ClientID %>')).val(0);
                $('.lblDiscount').text(0);
            }
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                //$(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(0);
            }


        }

        function calculatefooter() {
            var a = 0;
            var x = $('.txtDiscount');
            for (var i = 0; i < x.length; i++) {
                a += parseFloat(x[i].value);
            }
            $('.lblDiscount').text(a);

            var b = 0;
            var y = $('.txtFeeAmount');
            for (var i = 0; i < y.length; i++) {
                b += parseFloat(y[i].value);
            }
            $('.lblFeeAmount').text(b);

            var c = 0;
            var z = $('.txtTotalAmount');
            for (var i = 0; i < z.length; i++) {
                c += parseFloat(z[i].value);
            }
            $('.lblTotalAmount').text(c);

            var d = 0;
            var e = $('.txtRemainingAmount');
            for (var i = 0; i < e.length; i++) {
                d += parseFloat(e[i].value);
            }
            $('.lblRemainingAmount').text(d);
        }
        $("#btnCalculate").click(function () {
            CalculateFee();

        });
        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();


        });

        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });

        //function GetSelectedRow(chk) {
        //    var row = chk.parentNode.parentNode;
        //    var rowIndex = row.rowIndex - 1;
        //    var Amount = row.cells[6].getElementsByTagName("input")[0].value;
        //    alert(Amount);
        //    return false;
        //}
      <%-- $(document.getElementById('<%= ddlModeOfPayment.ClientID %>')).change(function () {
            debugger;

            if ($(document.getElementById('<%= ddlModeOfPayment.ClientID %>')).val() == 2 || $(document.getElementById('<%= ddlModeOfPayment.ClientID %>')).val() == 3) {

                $('#DivBankData').show();
                //  alert(2);
            } else {

                $('#DivBankData').hide();

            }

        });--%>



    </script>

</asp:Content>
