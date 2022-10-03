﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="BudgetCapitalCostReport.aspx.cs" Inherits="GEIMS.Client.UI.BudgetCapitalCostReport" %>
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
           Capital Cost      
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabBankReconciliation" href="#tabs-1">Capital Cost</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">                      
                         <div id="divStudentPanel" style="width: 100%">
                            <fieldset>
                                <legend>Add Values</legend>
                                
                                <div class="divclasswithfloat">   
                                    <div style="width: 15%; float: left;">
                                        <p>Category</p>                                      
                                        <asp:TextBox ID="txtBudgetCategory" runat="server" CssClass="TextBox" Width="60%" Height="100%" ReadOnly="true" ></asp:TextBox>
                                        <asp:HiddenField ID="hfBudgetCategory" runat="server" />
                                    </div>
                                    <div style="width: 15%; float: left;">
                                        <p>Heading</p>                                      
                                         <asp:DropDownList ID="ddlBudgetHeading"  CssClass="TextBox" runat="server"  Width="80%" Height="25px" OnSelectedIndexChanged="ddlBudgetHeading_SelectedIndexChanged" AutoPostBack="True"  >
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
                                          <p>Quantity</p>
                                         <asp:TextBox ID="txtQty" runat="server" CssClass="validate[required] TextBox" onkeypress="return numeric(event)" placeholder="0" Width="60%" Height="100%" style="text-align: right" AutoPostBack="True" OnTextChanged="txtQty_TextChanged"  ></asp:TextBox>           
                                     </div>
                                    <div style="width: 15%; float: left;">
                                        <p>UOM</p>               
                                       <asp:DropDownList runat="server" CssClass="TextBox" ID="ddlUOM" Width="80%" Height="25px" >
                                           <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                                     <div style="width: 15%; float: left;">
                                        <p>Rate(Unit)</p>
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="validate[required] TextBox" onkeypress="return numeric(event)" placeholder="0" Width="50%" Height="100%" style="text-align: right" AutoPostBack="True" OnTextChanged="txtRate_TextChanged" ></asp:TextBox>                                                                           
                                    </div>
                                     <div style="width: 10%; float: left;">
                                        <p>Total Amount</p>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="validate[required] TextBox" placeholder="0" Width="50%" Height="100%" ReadOnly="true" style="text-align: right" ></asp:TextBox>                                                                                
                                    </div>
                                     <div style="width: 97%; float: left; text-align: right">
                                        <p>&nbsp;</p>
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn-blue-new Attach" Width="50px" Text="ADD" OnClick="btnAdd_Click" style="width:70px; left: 0px;" />
                                    </div>                                    
                                </div>                           
                            </fieldset>
                        </div>                  
                        <div id="divFeePanel" runat="server">
                            
                            <div class="clear"></div>                                                   
                            <div style="width: 100%" id="divFeeVisibility" runat="server">
                                <fieldset>
                                    <legend>Capital Cost</legend>
                                    <asp:Label runat="server" ID="lblFee" ForeColor="Red"></asp:Label>
                                    <div style="width: 100%">
                                        <asp:GridView ID="gvBudgetCapitalCost" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="2"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowCommand="gvBudgetCapitalCost_RowCommand" OnRowCreated="gvBudgetCapitalCost_RowCreated" >
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <Columns>    
                                                <asp:TemplateField HeaderText="Category" FooterText="Total">
											        <ItemTemplate>
                                                        <asp:Label ID="Category" runat="server" Text='<%# Eval("CategoryName") %>'  ></asp:Label>
											        </ItemTemplate>
											        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
										        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Heading">
											        <ItemTemplate>
                                                        <asp:Label ID="HeadingName" runat="server" Text='<%# Eval("HeadingName") %>'  ></asp:Label>
											        </ItemTemplate>
											        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
										        </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SubHeading" >
											        <ItemTemplate>
                                                        <asp:Label ID="SubHeadingName" runat="server" Text='<%# Eval("SubHeadingName") %>'></asp:Label>
											        </ItemTemplate>
											        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
										        </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Qty" >
											        <ItemTemplate>
                                                        <asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>'  CssClass="label"
													Style="text-align: right;" Width="100px" Height="13px"></asp:Label>
											        </ItemTemplate>
											        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
										        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" >
											        <ItemTemplate>
                                                        <asp:Label ID="UOMName" runat="server" Text='<%# Eval("UOMName") %>'></asp:Label>
											        </ItemTemplate>
											        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
										        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate(Unit)" >
											        <ItemTemplate>
                                                       <asp:Label ID="RatePerUnit" runat="server" Text='<%# Eval("RatePerUnit") %>'  CssClass="label"
													Style="text-align: right;" Width="100px" Height="13px"></asp:Label>
											        </ItemTemplate>
											        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
										        </asp:TemplateField>                                             
                                                <asp:TemplateField HeaderText="Total">
											        <ItemTemplate>
                                                         <asp:Label ID="TotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>'  CssClass="label"
													Style="text-align: right;" Width="100px" Height="13px" ></asp:Label>											       								
											        </ItemTemplate>
											        <FooterTemplate>
                                                       <%-- <asp:Label ID="txtSTotal" runat="server" class="TextBox" disabled="disabled" style="text-align: right; width: 100px; height: 13px;" type="text" value="0" Text='<%# Eval("SubTotal") %>'></asp:Label>--%>
												       <input id="txtTotalSum" runat="server" class="TextBox" disabled="disabled" style="text-align: right; width: 100px; height: 13px;" type="text" value="0" />
											        </FooterTemplate>
										        </asp:TemplateField>  
                                                 <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                            CommandName="Edit1" CommandArgument='<%# Eval("CapitalCostId")%>' Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                                            CommandName="Delete1" CommandArgument='<%# Eval("CapitalCostId")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                            Height="20px" Width="20px" />
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
</asp:Content>