<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="RoleRights.aspx.cs" Inherits="GEIMS.Client.UI.RoleRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .droparea {
            margin-top: 1px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Role Rights Details     
           <%-- <asp:LinkButton CausesValidation="false" ID="lnkAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" runat="server" style="height: 100%; font-family: Verdana; padding-bottom: 10px;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left;">
                <div id="divPurchaseTab">
                    <div id="tabs" runat="server" class="style-tabs" visible="True" style="width: 100%; float: left; padding: 5px 5px 5px 5px">
                        <div style="float: left; width: 100%;">
                            <ul>
                                <li><a id="tabPurchasePaySlipDetails" href="#tabs-1">Role Rights Details</a></li>
                            </ul>
                        </div>
                        <div id="tabs-1" class="gradientBoxesWithOuterShadows" style="float: left; width: 99%;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="width: 100%;">
                                <ContentTemplate>
                                    <div id="Div1" style="width: 99%; float: left;" class="label">
                                        <div style="padding: 0px 0 10px 10px;">
                                            <div style="width: 100%; float: left;" class="label">
                                                <div style="padding: 10px;" align="center">
                                                    <div style="float: left; width: 100%;">
                                                        <asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 100%; float: left; padding-bottom: 10px" class="label">
                                                <div style="padding: 0px 0 10px 0px;">
                                                    <div style="float: left; width: 15%;">
                                                        Select :
                                                    </div>
                                                    <div style="float: left; width: 85%;">
                                                        <asp:RadioButtonList runat="server" ID="rblSelect" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblSelect_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Trust</asp:ListItem>
                                                            <asp:ListItem Value="1">School</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                </div>
                                            </div>
                                            <%--<div id="divTrust" runat="server" style="width: 100%; float: left; padding-bottom: 10px" class="label">
                                                <div style="padding: 0px 0 10px 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Trust Name :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="float: left; width: 85%;">
                                                        <asp:DropDownList runat="server" CssClass="droparea" ID="ddlTrust" Width="45%" AutoPostBack="true" OnSelectedIndexChanged="ddlTrust_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTrust"
                                                            ErrorMessage="Select Trust Name." SetFocusOnError="true" ValidationGroup="g1" InitialValue="-1"
                                                            ForeColor="red">*</asp:RequiredFieldValidator>
                                                    </div>

                                                </div>--%>
                                            </div>
                                            <div id="divSchool" runat="server" style="width: 100%; float: left; padding-bottom: 10px" class="label">
                                                <div style="padding: 0px 0 10px 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        School Name :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="float: left; width: 85%;">
                                                        <asp:DropDownList runat="server" CssClass="droparea" ID="ddlSchool" Width="45%" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchool"
                                                            ErrorMessage="Select Trust Name." SetFocusOnError="true" ValidationGroup="g1" InitialValue="-1"
                                                            ForeColor="red">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divRole" runat="server" style="width: 100%; float: left; padding-bottom: 10px" class="label">
                                                <div style="padding: 0px 0 10px 10px;">
                                                    <div style="float: left; width: 15%;">
                                                        Role :
                                                    </div>
                                                    <div style="float: left; width: 85%;">
                                                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True"
                                                            CssClass="droparea" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRole"
                                                            ErrorMessage="Select Role Name." SetFocusOnError="true" ValidationGroup="g1" InitialValue="-1"
                                                            ForeColor="red">*</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divRoleRight" runat="server" style="width: 100%; float: left; padding: 0 0 10px 10px;" visible="false">
                                                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 98%;">
                                                    <asp:Panel ID="pnlSelectRights" runat="server" GroupingText="" Font-Names="Verdana"
                                                        Font-Size="11px" DefaultButton="btnSave">
                                                        <asp:GridView ID="gvSelectRights" runat="server" BackColor="White"
                                                            BorderColor="#4E5557" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                            GridLines="Horizontal" Width="100%" AutoGenerateColumns="False"
                                                            BorderStyle="Solid">
                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ScreenID" HeaderText="RoleScreenID">
                                                                    <HeaderStyle CssClass="hidden " HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle CssClass="hidden " HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SchoolMID" HeaderText="SchoolMID">
                                                                    <HeaderStyle CssClass="hidden " HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle CssClass="hidden " HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DisplayName" HeaderText="Screen Name">
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField>
                                                                     <HeaderTemplate>
                                                                        <asp:CheckBox ID="ChkRightsAll" runat="server" OnCheckedChanged="ChkRightsAll_CheckedChanged" AutoPostBack="true"/>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRights" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div id="divButtons" runat="server" style="width: 100%; float: left;" class="label">
                                                <div style="float: left; text-align: right; width: 100%;" align="center">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-blue btn-blue-medium" Width="75px"
                                                        OnClick="btnSave_Click" ValidationGroup="g1" />&nbsp; &nbsp;
                                                <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="g1" ShowMessageBox="True" ShowSummary="False" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rblSelect" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlRole" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlSchool" EventName="SelectedIndexChanged" />
                                    <%--<asp:AsyncPostBackTrigger ControlID="ddlTrust" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
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

