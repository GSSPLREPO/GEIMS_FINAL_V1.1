<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="BudgetHeadingMaster.aspx.cs" Inherits="GEIMS.Client.UI.BudgetHeadingMaster"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
          $(function () {
              $(document.getElementById('<%= tabs.ClientID %>')).tabs();
          });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvBudgetHeading.ClientID%> tbody tr');
        })
    </script>
     <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>

    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
      Budget Heading
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
                    <asp:GridView ID="gvBudgetHeading" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvBudgetHeading_RowCommand"
                        OnPreRender="gvBudgetHeading_PreRender">
                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                        <Columns>
                             <asp:BoundField DataField="CategoryName" HeaderText="Category Name">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HeadingName" HeaderText="Heading Name">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("BudgetHeadingMID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="true" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("BudgetHeadingMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Budget Heading</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 195px;padding:5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%;">
                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                   Category :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 80%; float: left;">
                                    <asp:DropDownList ID="ddlBudgetCategory" runat="server" CssClass="validate[required] Droptextarea">
                                      <asp:ListItem Value="">--Select--</asp:ListItem>
                                  </asp:DropDownList>
                                </div>
                            </div>
                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                   Heading :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:TextBox ID="txtDepartmentNameGUJ" runat="server" CssClass="validate[required] TextBox" Width="50%" ></asp:TextBox>
                                </div>
                            </div>                         
                            <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" />
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
        $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
</asp:Content>
