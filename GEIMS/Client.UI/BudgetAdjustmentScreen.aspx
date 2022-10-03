<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="BudgetAdjustmentScreen.aspx.cs" Inherits="GEIMS.Client.UI.BudgetAdjustmentScreen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
          $(function () {
              $(document.getElementById('<%= tabs.ClientID %>')).tabs();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
       <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
           Adjustment Screen      
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabBankReconciliation" href="#tabs-1">Adjustment Screen</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">                      
                         <div id="divStudentPanel" style="width: 100%">                          
                            <fieldset>
                                <legend>Transfer From</legend> 
                                 <div class="divclasswithfloat"> 
                                      <div style="width: 15%; float: left;">
                                        <p>Select School</p>                                      
                                         <asp:DropDownList ID="ddlSchool"  CssClass="TextBox" runat="server"  Width="280px" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                     <div style="width: 15%; float: left; margin-left:150px;">
                                        <p>Section</p>                                      
                                         <asp:DropDownList ID="ddlSection"  CssClass="TextBox" runat="server"  Width="180%" Height="25px" AutoPostBack="True"  >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>                                  
                                  </div>
                                <div class="divclasswithfloat">   
                                    <div style="width: 15%; float: left;">
                                        <p>Category</p>                                      
                                         <asp:DropDownList ID="ddlBudgetCategory"  CssClass="TextBox" runat="server"  Width="80%" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetCategory_SelectedIndexChanged"  >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 15%; float: left;">
                                        <p>Heading</p>                                      
                                         <asp:DropDownList ID="ddlBudgetHeading"  CssClass="TextBox" runat="server"  Width="80%" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetHeading_SelectedIndexChanged"  >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                      <div style="width: 15%; float: left;">
                                          <p>SubHeading</p>
                                        <asp:DropDownList ID="ddlBudgetSubHeading"  CssClass="TextBox" runat="server" AutoPostBack="True"  Width="80%" Height="25px" OnSelectedIndexChanged="ddlBudgetSubHeading_SelectedIndexChanged" >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>                                      
                                    </div>                                                                                                                                          
                                    <div style="width: 15%; float: left;">
                                        <p>Amount</p>
                                        <asp:TextBox ID="txtAvailableAmount" runat="server" CssClass="validate[required] TextBox" placeholder="0" Width="80%" Height="18px" ReadOnly="true" style="text-align: right" AutoPostBack="True" >0.00</asp:TextBox>                                                                                
                                        <asp:HiddenField ID="hfAvailableAmount" runat="server" />
                                    </div>
                                     <div style="width: 15%; float: left;">
                                        <p>Adjust Amount</p>
                                        <asp:TextBox ID="txtAdjustmentAmt" runat="server" CssClass="validate[required] TextBox" onkeypress="return numeric(event)" placeholder="0" Width="80%" Height="18px" ReadOnly="false" style="text-align: right" OnTextChanged="txtAdjustmentAmt_TextChanged" AutoPostBack="True"  >0.00</asp:TextBox>                                                                                
                                    </div>
                                     <div style="width: 15%; float: left;">
                                        <p>Total Amount</p>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="validate[required] TextBox" placeholder="0" Width="80%" Height="18px" ReadOnly="true" style="text-align: right" >0.00</asp:TextBox>                                                                                
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>                                             
                                </div>                               
                            </fieldset>
                        </div>                  
                        <div id="divFeePanel" runat="server">                            
                            <div class="clear"></div>                                                   
                            <div style="width: 100%" id="divFeeVisibility" runat="server">
                               <fieldset>
                                <legend>Transfer To</legend> 
                                  <%--<div class="divclasswithfloat"> 
                                     <div style="width: 15%; float: left;">
                                        <p>Section</p>                                      
                                         <asp:DropDownList ID="ddlSection1"  CssClass="TextBox" runat="server"  Width="80%" Height="25px" AutoPostBack="True"  >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>                                   
                                  </div>--%>
                                <div class="divclasswithfloat">   
                                    <div style="width: 15%; float: left;">
                                        <p>Category</p>                                      
                                         <asp:DropDownList ID="ddlBudgetCategory1"  CssClass="TextBox" runat="server"  Width="80%" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetCategory1_SelectedIndexChanged1" >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 15%; float: left;">
                                        <p>Heading</p>                                      
                                         <asp:DropDownList ID="ddlBudgetHeading1"  CssClass="TextBox" runat="server"  Width="80%" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetHeading1_SelectedIndexChanged" >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                      <div style="width: 15%; float: left;">
                                          <p>SubHeading</p>
                                        <asp:DropDownList ID="ddlBudgetSubHeading1"  CssClass="TextBox" runat="server" AutoPostBack="True"  Width="80%" Height="25px" OnSelectedIndexChanged="ddlBudgetSubHeading1_SelectedIndexChanged" >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>                                      
                                    </div>                                                                                                                 
                                    <div style="width: 15%; float: left;">
                                        <p>Amount</p>
                                        <asp:TextBox ID="txtAvailableAmount1" runat="server" CssClass="validate[required] TextBox" placeholder="0" Width="80%" Height="18px" ReadOnly="true" style="text-align: right" >0.00</asp:TextBox>                                                                                
                                    </div>  
                                    <div style="width: 15%; float: left;">
                                        <p>Adjust Amount</p>
                                        <asp:TextBox ID="txtAdjustmentAmt1" runat="server" CssClass="validate[required] TextBox" placeholder="0" Width="80%" Height="18px" ReadOnly="true" style="text-align: right" >0.00</asp:TextBox>                                                                                
                                    </div>
                                     <div style="width: 15%; float: left;">
                                        <p>Total Amount</p>
                                        <asp:TextBox ID="txtTotalAmount1" runat="server" CssClass="validate[required] TextBox" placeholder="0" Width="80%" Height="18px" ReadOnly="true" style="text-align: right" >0.00</asp:TextBox>                                                                                
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </div>
                                </div> 
                                   <div class="divclasswithfloat">
                                     <div style="width: 10%; float: left; text-align: right">
                                        <p>&nbsp;</p>
                                        <asp:Button ID="btnAdustment" runat="server" CssClass="btn-blue-new Attach" Width="50px" Text="Transfer" style="width:80px; height:28px; left: 0px; top: -3px;" OnClick="btnAdustment_Click" />
                                    </div>   
                                </div>
                            </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>              
            </div>      
        </div>
        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>   

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $(document.getElementById('<%=btnAdustment.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
</asp:Content>
