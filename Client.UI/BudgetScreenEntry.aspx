<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="BudgetScreenEntry.aspx.cs" Inherits="GEIMS.Client.UI.BudgetScreenEntry" %>
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
   <%-- <script type="text/javascript" language="javascript">

        var count = 0;
        function isDecimalNumber(evt, c) {
            count = count + 1;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var dot1 = c.value.indexOf('.');
            var dot2 = c.value.lastIndexOf('.');
            if (count > 10 && dot1 == -1) {
                c.value = "";
                count = 0;
            }
            if (dot1 > 10) {
                c.value = "";
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
                return false;

            return true;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
     <script type="text/javascript">
         $(function () {
             $('#id_search').quicksearch('table#<%=gvBudgetScreen.ClientID%> tbody tr');
        })
     </script>
     <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>

    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
        Budget Entry
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_Click" >Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click" >View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px;padding-right:10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div><br/>
                                    <asp:GridView ID="gvBudgetScreen" runat="server"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="2"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowCreated="gvBudgetScreen_RowCreated" OnRowCommand="gvBudgetScreen_RowCommand" OnPreRender="gvBudgetScreen_PreRender"  >
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                               <Columns>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                                CommandName="Edit1" CommandArgument='<%# Eval("BudgetScreenId")%>' Height="20px" Width="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="center" />
                                                        <ItemStyle HorizontalAlign="center" Width="10%" />
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="true" ImageUrl="~/Images/delete-1.png"
                                                            CommandName="Delete1" CommandArgument='<%# Eval("BudgetScreenId")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                            Height="20px" Width="20px" />
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("BudgetScreenId") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                 </asp:TemplateField>
                                               </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" ForeColor="White" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Budget Entry Details</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 100%;padding:5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%;">
                            <div class="divclasswithfloat" >
                               <%-- <div style="text-align: left; width: 20%; float: left;" class="label">
                                   Category :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 80%; float: left;">
                                    <asp:DropDownList ID="ddlBudgetCategory" runat="server" CssClass="Droptextarea">
                                      <asp:ListItem Value="">--Select--</asp:ListItem>
                                  </asp:DropDownList>
                                </div>--%>
                                <div style="width: 30%; float: left;">
                                        <p>Category</p>                                      
                                         <asp:DropDownList ID="ddlBudgetCategory" CssClass="Droptextarea" runat="server" AutoPostBack="True" Width="80%" OnSelectedIndexChanged="ddlBudgetCategory_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 30%; float: left;">
                                        <p>Heading</p>                                      
                                         <asp:DropDownList ID="ddlBudgetHeading"  CssClass="Droptextarea" runat="server" AutoPostBack="True" Width="80%" OnSelectedIndexChanged="ddlBudgetHeading_SelectedIndexChanged"   >
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 30%; float: left;">
                                          <p>SubHeading</p>
                                        <asp:DropDownList ID="ddlBudgetSubHeading"  CssClass="Droptextarea" runat="server" AutoPostBack="False" Width="80%">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                        </asp:DropDownList>                                      
                                    </div>                    
                            </div>
                            <div class="divclasswithfloat">
                                 <div class="divclasswithfloat" style="margin-top:-18px;">  
                                      <div style="width: 100%; float: left;">
                                            <p><asp:Label ID="lblAdminMGT" runat="server" Text="Admin & General on MGT role"  Visible="false"></asp:Label></p>             
                                            <asp:TextBox ID="txtAdminMGT" runat="server" CssClass="validate[required] TextBox" onkeypress="return numeric(event)" Text="0"  style="text-align: right; width:14%;" Visible="false"  ></asp:TextBox>    
                                      </div>  
                                      <br />
                                      <br />
                                     <div style="width: 100%; float: left; margin-top:30px;">
                                          <asp:DataList ID="dlSection" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
                                              <ItemTemplate>
                                                  <asp:HiddenField ID="hfSectionMID" runat="server" Value='<%# Eval("SectionMID") %>' />
                                                  <asp:Label ID="lblSectionName" runat="server" Text='<%#Eval("SectionName") %>' ></asp:Label>
                                                  <br />
                                                  <br />
                                                  <asp:TextBox ID="txtSectionName" CssClass="validate[required] TextBox" onkeypress="return numeric(event)"  Text="0" runat="server" style="text-align: right; width:70%;"  ></asp:TextBox>  
                                                  <br />
                                                  <br />
                                              </ItemTemplate>
                                          </asp:DataList>
                                      </div>  
                                      <br />    
                                      <br />                                   
                               </div>
                            </div>                          
                            <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">                              
                               <asp:Button ID="btnTotal" runat="server" Text="ADD" CssClass="btn-blue-new btn-blue-medium" Width="50px" OnClick="btnTotal_Click" OnClientClick="return ClearDatalist()" />                                      
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right;"></div>
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
          $(document.getElementById('<%= btnTotal.ClientID %>')).click(function () {
              var valid = $("#aspnetForm").validationEngine('validate');
              var vars = $("#aspnetForm").serialize();
          });
      </script>
</asp:Content>