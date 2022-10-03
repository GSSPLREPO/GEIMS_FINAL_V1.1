<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="FeesCategoryMaster.aspx.cs" Inherits="GEIMS.Client.UI.FeesCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Fee Category Master
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewFeeCategory" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewFeeCategory_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>
                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                    <asp:GridView ID="gvFeesCategory" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvFeesCategory_RowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="FeesName" HeaderText="Fee Category Name">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FeesType" HeaderText="Fee Type">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FeeAbbreviation" HeaderText="Fee Abbreviation">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                             <asp:BoundField DataField="OutStandingMonthName" HeaderText="Outstnading Month">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("FeesCategoryMID")%>' CssClass="Detach" Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("FeesCategoryMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                        <li><a id="tabClassDetails" href="#tabs-1">Fees Category Details</a></li>

                    </ul>
                    <div id="tabs-1" style="height: 230px;padding:5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Fee Type: <span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 27%; float: left;">
                                <asp:RadioButtonList ID="rblFeeType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Compulsory">Compulsory</asp:ListItem>
                                    <asp:ListItem Value="Optional">Optional</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Fees Category Name :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtFeesCategoryName" runat="server" CssClass="validate[required] TextBox" Width="250px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Outstanding Month :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:DropDownList ID="ddlOutstandingMonth" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Fees Abbreviation :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtAbbreviation" runat="server" CssClass="validate[required] TextBox" Width="250px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Fees Group :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:DropDownList ID="ddlFeeGroup" runat="server" CssClass="validate[required] Droptextarea" Width="260px">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div style="height: 30px; float: right; width: 100%;padding:0 10px 0px 0px">
                            <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancel_Click"  />--%>&nbsp;&nbsp;
							<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium" OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
					
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
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

        $(function () {
            $("table[id*=rblFeeType]").validationEngine('attach', { promptPosition: "bottomRight" });
            $("table[id*=rblFeeType] input").addClass("validate[required]");
            $("[id*=btnFeesCategory]").bind("click", function () {
                if (!$("table[id*=rblFeeType]").validationEngine('validate')) {
                    return false;
                }
                return true;
            });

        });


    </script>
</asp:Content>
