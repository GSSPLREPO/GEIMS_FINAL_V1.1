<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="BackUp.aspx.cs" Inherits="GEIMS.Client.UI.BackUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">
        1
    </div>
    <div id="divActiveTab" class="hidden" visible="false">
        Database Backup
    </div>
    <div style="min-height:500px">
        <table width="100%" align="center" cellpadding="3px" style="padding-top: 5px;">
            <tr>
                <td>
                    <div class="pageTitle">
                        database Backup
                        <%--<asp:Label ID="lblTitle" runat="server"></asp:Label>--%>
                    </div>
                    <div class="buttonPanel">
                        <%-- <asp:LinkButton CausesValidation="false" ID="btnAddConnection" runat="server" class="btn-blue btn-blue-medium"
                            OnClick="btnAddConnection_Click">Add
                                New</asp:LinkButton>--%>
                    </div>
                    <%--  <div class="heading">
                            Heading</div>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center" valign="top">
                                <asp:GridView ID="gvbackup" runat="server" CssClass="label" AutoGenerateColumns="False"
                                    BorderColor="#67A3D1" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" AllowPaging="false"
                                    GridLines="Both" Font-Names="verdana" Font-Size="11px" BackColor="White">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="Text" HeaderText="File Name" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>'
                                                    runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Value") %>'
                                                    runat="server" OnClick="DeleteFile" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#67A3D1" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblMsg" runat="server" CssClass="message"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Button runat="server" ID="btnbackup" Text="Start Backup" Font-Size="13px" Width="150px" class="btn-blue btn-blue-medium"
                                     OnClick="btnbackup_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
