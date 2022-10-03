<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EmployeeCategoryWise.aspx.cs" Inherits="GEIMS.ReportUI.EmployeeCategoryWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Employee Category Wise
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" /> &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                 OnClick="btnReport_Click" />
                </div>
                <div id="divContent" style="height: 100%; font-family: Verdana;">
                    <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
                    <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                        <div style="text-align: center; width: 100%;">
                            <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                        </div>

                        <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                            <div id="tabs-1" style="min-height: 150px;">
                                <%--<asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="Student Details">
                                   
                                </asp:Panel>--%>
                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>
                                                    શૈક્ષણિક તથા બિન-શૈક્ષણિક સ્ટાફ ની વિગત </b>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                શાળા :
                                                <asp:Label runat="server" ID="lblSchoolName"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="false"
                                            CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="true" Width="100%" OnRowCreated="gvReport_RowCreated">
                                            <RowStyle BackColor="White" />
                                            <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                                BorderWidth="1px" BorderStyle="Solid" />
                                            <Columns>
                                                <asp:BoundField DataField="MaleSCCount" HeaderText="પુરૂષ">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FemaleSCCount" HeaderText="સ્ત્રી">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MaleSTCount" HeaderText="પુરૂષ">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FemaleSTCount" HeaderText="સ્ત્રી">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MaleOBCCount" HeaderText="પુરૂષ">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FemaleOBCCount" HeaderText="સ્ત્રી">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MaleOtherCount" HeaderText="પુરૂષ">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FemaleOtherCount" HeaderText="સ્ત્રી">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MaleTotalCount" HeaderText="પુરૂષ">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FemaleTotalCount" HeaderText="સ્ત્રી">
                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
                    </div>
                </div>

                <div id="divPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="padding: 10px; padding-right: 30px;">
                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                <b>
                                   શૈક્ષણિક તથા બિન-શૈક્ષણિક સ્ટાફ ની વિગત </b>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="padding: 10px; padding-right: 30px;">
                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                શાળા :
                                                <asp:Label runat="server" ID="lblSchoolName1"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; float: left; padding-top: 0px 10px 0 10px;" class="label">
                        <asp:GridView ID="gvReport1" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="false"
                            CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="true" Width="97%" OnRowCreated="gvReport1_RowCreated">
                            <RowStyle BackColor="White" />
                            <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                BorderWidth="1px" BorderStyle="Solid" />
                            <Columns>
                                <asp:BoundField DataField="MaleSCCount" HeaderText="પુરૂષ">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FemaleSCCount" HeaderText="સ્ત્રી">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaleSTCount" HeaderText="પુરૂષ">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FemaleSTCount" HeaderText="સ્ત્રી">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaleOBCCount" HeaderText="પુરૂષ">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FemaleOBCCount" HeaderText="સ્ત્રી">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaleOtherCount" HeaderText="પુરૂષ">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FemaleOtherCount" HeaderText="સ્ત્રી">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaleTotalCount" HeaderText="પુરૂષ">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FemaleTotalCount" HeaderText="સ્ત્રી">
                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
    </script>
</asp:Content>

