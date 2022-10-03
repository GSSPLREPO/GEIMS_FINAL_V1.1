<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="StockUpdation.aspx.cs" Inherits="GEIMS.Client.UI.StockUpdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="subDiv" style="width: 100%; float: left;" class="label">
        <div style="padding: 10px;">
            <fieldset>
                <legend style="font-size: 13px;"><b>Material Details</b></legend>
               <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <div style="width: 100%; float: left;" class="label">
                            <div style="padding: 10px;">
                                <div style="float: left; width: 15%;">
                                    Year :<span style="color: red">*</span>
                                </div>
                                <div style="float: left; width: 35%;">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass=" Droptextarea" AutoPostBack="True" Width="260px" Enabled="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                        ErrorMessage="Select Year." SetFocusOnError="true" ValidationGroup="g1" InitialValue=""
                                        ForeColor="red">*</asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; width: 15%;">
                                    Material Group :<span style="color: red">*</span>
                                </div>
                                <div style="float: left; width: 35%;">
                                    <asp:DropDownList ID="ddlMaterialGroupName" runat="server" AutoPostBack="True" CssClass=" Droptextarea" Width="260px" Enabled="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlMaterialGroupName"
                                        ErrorMessage="Enter Material Group Name." SetFocusOnError="true" ValidationGroup="g1" 
                                        ForeColor="red">*</asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </div>
                        <asp:HiddenField ID="hfMaterialID" runat="server" />
                        <asp:HiddenField ID="hf" runat="server" />
                        <div style="width: 100%; float: left;" class="label">
                            <div style="padding: 10px;">
                                <%--<div style="float: left; width: 15%; display: none;">
                                                    Delivery Note :
                                                </div>
                                                <div style="float: left; width: 35%; display: none;">
                                                    <asp:TextBox ID="txtDeliveryNote" runat="server" CssClass="TextArea" TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox>
                                                </div>--%>

                                <div style="text-align: right; float: left; vertical-align: bottom; width: 100%;">
                                    <asp:Button runat="server" ID="btnUpdateMaterial" Text="Show Material" ValidationGroup="g1" CssClass="btn-blue-new btn-blue-medium" OnClick="btnUpdateMaterial_OnClick" />
                                </div>
                            </div>
                        </div>

                        <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; float: left; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="MaterialTID" HeaderText="Id">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="hidden" Width="30%" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" CssClass="hidden" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MaterialName" HeaderText="Material Name">
                                        <HeaderStyle HorizontalAlign="Center" Width="50%" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Remaining Amount.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStock" runat="server" Width="100px" CssClass="txtRemainingAmount TextBox">0</asp:TextBox>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblRemainingAmount" runat="server" CssClass="lblRemainingAmount" />
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" ForeColor="White" Font-Size="12px" />
                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                   <%-- </ContentTemplate>
                    <Triggers>--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="ddlsection" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlClass" EventName="SelectedIndexChanged" />--%>
                   <%-- </Triggers>
                </asp:UpdatePanel>--%>
            </fieldset>
        </div>
        <div id="divPurchaseSave" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
            <div style="padding: 10px; padding-right: 30px;">
                <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_OnClick" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfUOM" runat="server" />
        <asp:HiddenField ID="hfMaterial" runat="server" />
        <asp:HiddenField ID="hfValidate" runat="server" />
        <asp:HiddenField ID="hfTab" runat="server" />
        <asp:HiddenField ID="hfMode" runat="server" />
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


