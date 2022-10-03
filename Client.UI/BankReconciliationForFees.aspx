<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="BankReconciliationForFees.aspx.cs" Inherits="GEIMS.Client.UI.BankReconciliationForFees" %>
<%@ Import Namespace="GEIMS.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../CSS/ajaxCalender.css" rel="stylesheet" />
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(document).ready(function () {

          <%--  $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $(document.getElementById('<%= txtRemainingAmountFoot.ClientID %>')).val(0);
            $(document.getElementById('<%= txtAmountToBePaid.ClientID %>')).val(0);
            $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);--%>
            $(".autosuggest").autocomplete({
                source: function (request, response) {

                },
                select: function (e, i) {
                  <%--  // $("#<%=hfSearchName.ClientID %>").val(i.item.val);
                    $("#<%=hfSearchName.ClientID %>").val(i.item.label);--%>
                }
            });
           <%-- $('#<%=gvFees.ClientID %> tr').each(function () {
                $(this).closest("tr").find($("[id*=txtFeeAmount]")).prop('readonly', true);
                $(this).closest("tr").find($("[id*=lblTotalAmount]")).prop('readonly', true);
                $(this).closest("tr").find($("[id*=txtRemainingAmount]")).prop('readonly', true);
            });--%>

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

    <script type="text/javascript">
        function calendarShown1(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <style type="text/css">
        .pnlCSS {
            font-weight: bold;
            cursor: pointer;
            border: solid 1px #c0c0c0;
            width: 99%;
        }
    </style>
      <script>
          $(function () {
              $(document.getElementById('<%= tabs.ClientID %>')).tabs();
          });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


       <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            <%--Reconciliation of fee directly deposited in bank--%>
            Fees Reco
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabBankReconciliation" href="#tabs-1">Fees Reco</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">                      
                         <div id="divStudentPanel" style="width: 100%">
                            <fieldset>
                                <legend>Search Fees Reco</legend>
                                <div class="divclasswithfloat">
                                    
                                    <div style="width: 17%; float: left;">
                                        <p>Select Shcool</p>
                                        <asp:DropDownList ID="ddlSearchBySchool" Width="150px" CssClass="textarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBySchool_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                       
                                    </div>
                                      <div style="width: 17%; float: left;">
                                          <p>Select Section</p>
                                        <asp:DropDownList ID="ddlSearchBySection" Width="150px" CssClass="textarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBySection_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                       
                                    </div>
                                    
                                     <div style="width: 17%; float: left;">
                                          <p>Select Class</p>
                                        <asp:DropDownList ID="ddlSearchByClass" Width="150px" CssClass="textarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByClass_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                       
                                    </div>
                                    <div style="width: 17%; float: left;">
                                        <p>Select Division</p>
                                        <asp:DropDownList ID="ddlSearchByDivision" Width="150px" CssClass="textarea" runat="server">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                       
                                    </div>
                                     <div style="width: 17%; float: left;">
                                        <p>Select Year</p>
                                        <asp:DropDownList ID="ddlSearchByYear" Width="150px" CssClass="textarea" runat="server">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            
                                        </asp:DropDownList>                                      
                                    </div>

                                     <div style="width: 15%; float: left; text-align: right">
                                        <p>&nbsp;</p>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn-blue-new Attach" Width="50px" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                    
                                </div>                           
                            </fieldset>
                        </div>                  
                        <div id="divFeePanel" runat="server">
                            
                            <div class="clear"></div>                                                   
                            <div style="width: 100%" id="divFeeVisibility" runat="server">
                                <fieldset>
                                    <legend>Fees Reco Details</legend>
                                    <asp:Label runat="server" ID="lblFee" ForeColor="Red"></asp:Label>
                                    <div style="width: 100%">
                                        <asp:GridView ID="gvBankReconciliation" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="True" ShowFooter="True">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />

                                            <Columns>
                                                <%-- <asp:BoundField DataField="AcademicYear" HeaderText="Academic Year">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>--%>                                                                                        
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>                                                    
                                                        <asp:CheckBox ID="chkChild" runat="server" />
                                                        <asp:HiddenField ID="hfFeesCollectionMID" runat="server" Value='<%# Eval("FeesCollectionMID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Student Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("StudentFirstNameEng") + " " + Eval("StudentMiddleNameEng") + " " + Eval("StudentLastNameEng")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="Year">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblAcademicYear" runat="server" ReadOnly="true" Width="80px" Text='<%# Eval("AcademicYear") %>' CssClass="txtFeeAmount TextBox" BorderWidth="0px" ForeColor="Black">0</asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinalAmount" runat="server" Text='<%# Eval("AmountPaid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotalDiscount" runat="server" CssClass="lblDiscount" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Deposit">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtdate" runat="server" Width="80px" CssClass="validate[required] TextBox"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                                        </ajaxToolkit:CalendarExtender>                                                     
                                                    </ItemTemplate>
                                                    <FooterTemplate>                                                        
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Cheque Number">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtCheque" runat="server" Width="80px" CssClass="validate[required] TextBox"></asp:TextBox>
                                                                                                           
                                                    </ItemTemplate>
                                                    <FooterTemplate>                                                        
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtBankName" runat="server" Width="80px" CssClass="validate[required] TextBox"></asp:TextBox>
                                                                                                       
                                                    </ItemTemplate>
                                                    <FooterTemplate>                                                        
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Branch Name">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtBranchName" runat="server" Width="80px" CssClass="validate[required] TextBox"></asp:TextBox>
                                                                                                           
                                                    </ItemTemplate>
                                                    <FooterTemplate>                                                        
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date in Bank Statement">
                                                    <ItemTemplate>                     
                                                        <asp:TextBox ID="txtdate1" runat="server" Width="80px" CssClass="validate[required] TextBox"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" OnClientShown="calendarShown1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate1" TargetControlID="txtdate1">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </ItemTemplate>
                                                    <FooterTemplate>                                                       
                                                    </FooterTemplate>
                                                </asp:TemplateField>                                             
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
                                      
                                        <div class="clear"></div>
                                        <div class="divclasswithfloat">
                                            <div style="width: 100%; float: left;">                     
                                                <div style="width: 100%; float: left; text-align: right">
                                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach"  OnClick="btnSave_Click"  />&nbsp;
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
            </div>
              
            </div>      
        
        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
    </div>

  
</asp:Content>