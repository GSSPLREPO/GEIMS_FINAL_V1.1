<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GEIMS.Client.UI.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ddl
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            
            -webkit-appearance: none; 
            background-image:url('../Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            -moz-appearance:none;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvStudentFees" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true" OnRowDataBound="gvStudentFees_RowDataBound">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Choose Category">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkChild" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                       <%-- <asp:BoundField  >
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField >
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>--%>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
    <div style="width:200px; overflow:hidden" >
    <asp:DropDownList ID="DropDownList1" runat="server" style="width:220px">
      <asp:ListItem Text="text1" />
            <asp:ListItem Text="text2" />
    </asp:DropDownList>
</div>    
</asp:Content>
