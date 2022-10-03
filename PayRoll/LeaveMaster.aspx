<%@ Page Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="LeaveMaster.aspx.cs" Inherits="GEIMS.PayRoll.LeaveMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $('#tab-panel').tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Leave Master Detail
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewLeaveMaster" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewLeaveMaster_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <script type="text/javascript">
                $(function () {
                    $('#id_search').quicksearch('table#<%=gvLeaveMaster.ClientID%> tbody tr');
                })
            </script>
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                    <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                        <asp:GridView ID="gvLeaveMaster" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvLeaveMaster_OnRowCommand"
                            OnPreRender="gvLeaveMaster_OnPreRender">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="LeaveCode" HeaderText="Leave Code">
                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LeaveName" HeaderText="Leave Name">
                                    <HeaderStyle Width="60%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="60%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LeaveOpening" HeaderText="Leave Opening">
                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LeaveCarryForwardLimit" HeaderText="Leave Carry Forwarding">
                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Edit" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                            CommandName="Edit1" CommandArgument='<%# Eval("LeaveID")%>' Height="20px" Width="20px" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                            CommandName="Delete1" CommandArgument='<%# Eval("LeaveID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                </div>
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Leave Master Detail</a></li>
                        </ul>
                        <div id="tabs-1" style="padding:5px" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;">
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Leave Code :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:TextBox ID="txtLeaveCode" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Leave Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 80%; float: left;">
                                        <asp:TextBox ID="txtLeaveName" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Leave Description
                                    </div>
                                    <div style="text-align: left; width: 80%; float: left;">
                                        <asp:TextBox ID="txtLeaveDescription" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Leave Opening :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:TextBox ID="txtLOpening" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Leave Carry Forward :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:TextBox ID="txtCarryForward" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Year :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlYear" Width="33%" Height="26px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSaveLeave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveFeesGroup_Click" />
                                </div>
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

    </script>
</asp:Content>
