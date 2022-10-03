<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="TrustPayItemTemplate.aspx.cs" Inherits="GEIMS.PayRoll.TrustPayItemTemplate" %>

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
            School Pay Item Template
			 <%--<asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <script type="text/javascript">
                $(function () {
                    $('#id_search').quicksearch('table#<%=gvPayItem.ClientID%> tbody tr');
                })
            </script>
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                </div>
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1"> School Pay Item</a></li>
                        </ul>
                        

                        <div id="tabs-1" style="padding:5px" class="gradientBoxesWithOuterShadows">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                      Select School :<span style="color: red">*</span>
                                 </div>
                                <div style="text-align: left; float: left; width: 100%;">
                                    <asp:DropDownList ID="ddlSchool" CssClass="TextBox" runat="server"  Width="280px" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" >
                                         <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                    </asp:DropDownList>
                                </div>
                            
                            <div style="width: 100%;">
                                <div style="padding-top: 50px; padding-bottom: 10px; width: 100%;">
                                    <asp:GridView ID="gvPayItem" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="PayItemMID" HeaderText="Pay Item ID">
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Pay Item Name" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxPayItem" runat="server" Enabled="true" Width="5px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                                <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
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
