<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="Membership.aspx.cs" Inherits="GEIMS.Library.Membership" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function calendarShown(sender, args) {
             sender._popupBehavior._element.style.zIndex = 10005;
         }
     </script>
     <script>
         $(function () {
             $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvBudgetSubHeading.ClientID%> tbody tr');
        })
    </script>
     <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>

    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
           Library Membership
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);" />
                    </div><br/>
                    <asp:GridView ID="gvBudgetSubHeading" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvBudgetSubHeading_RowCommand"
                        OnPreRender="gvBudgetSubHeading_PreRender">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                           <asp:BoundField DataField="CategoryName" HeaderText="Category Name">
                                <HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HeadingName" HeaderText="Heading Name">
                                <HeaderStyle Width="25%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SubHeadingName" HeaderText="SubHeading Name">
                                <HeaderStyle Width="50%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%--<%# Eval("BudgetSubHeadingMID")%>--%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%--<%# Eval("BudgetSubHeadingMID")%>--%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
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
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Membership</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 200px;padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%; float: left;">
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                   Membership Type :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 80%; float: right;">
                                   <%-- <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlBudgetCategory" Width="50%" Height="100%" AutoPostBack="True"  />--%>
                                    <asp:RadioButtonList ID="rblMemberType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Employee</asp:ListItem>
                                        <asp:ListItem>Students</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Member Code :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 30%;">
                                    <%--<asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlBudgetHeading" Width="50%" Height="100%" />--%>
                                    <asp:TextBox ID="txtCode" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                </div>

                                 <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Registration Date :<span style="color: red">*</span>
                                </div>                           
                                 <div style="float: right; text-align: left; width: 30%; float: left;" class="label">
                                    <asp:TextBox ID="txtdate" runat="server" Width="150px" CssClass="validate[required] TextBox"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Member Name :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">                                 
                                    <asp:TextBox ID="txtMemberName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                             <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Member Address :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">                                 
                                    <asp:TextBox ID="txtMemberAddress" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="height: 30px; float: right; width: 100%; padding:0 0 0 10px;">
                            <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue btn-blue-medium"  />
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
