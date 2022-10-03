<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="WorkingDays.aspx.cs" Inherits="GEIMS.Client.UI.WorkingDays" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Working Days
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabWorkingDaysDetails" href="#tabs-1">Working Days Details</a></li>

                    </ul>
                    <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%;">
                            <div style="width: 100%;" class="divclasswithfloat">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Academic Year :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Width="150px">
                                    </asp:DropDownList>
                                </div>
                                <asp:Button runat="server" ID="btnViewGrid" Text="View Months" CssClass="btn-blue btn-blue-medium" OnClick="btnViewGrid_Click" />
                            </div>
                            <div class="clear"></div>
                            <div style="margin-top: 10px; width: 100%;" class="divclasswithoutfloat">
                                <asp:GridView ID="gvWorkingDays" HorizontalAlign="center" runat="server" AutoGenerateColumns="false"
                                    BorderColor="#3b5998" BorderWidth="3px" BorderStyle="solid" CellPadding="4" GridLines="both"
                                    Font-Names="verdana" Font-Size="12px" BackColor="white" ShowHeaderWhenEmpty="true">
                                    <FooterStyle BackColor="white" ForeColor="#333333" />
                                    <RowStyle BackColor="white" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="MonthNo" HeaderText="MonthID">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                            <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                            <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MonthName" HeaderText="Month Name">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Total Working Days.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalDays" runat="server" Width="150px" CssClass="TextBox" onkeypress="return NumericKeyPressFraction(event)">0</asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3b5998" Font-Bold="true" Font-Names="verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3b5998" ForeColor="white" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2b558e" Font-Bold="true" ForeColor="white" />
                                    <HeaderStyle BackColor="#3b5998" Font-Bold="true" ForeColor="white" />
                                </asp:GridView>
                            </div>

                            <div class="clear"></div>
                            <div id="divSave" runat="server" style="text-align: right;" class="divclasswithoutfloat">
                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                            </div>

                        </div>
                    </div>
                    <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
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
    </script>
</asp:Content>

