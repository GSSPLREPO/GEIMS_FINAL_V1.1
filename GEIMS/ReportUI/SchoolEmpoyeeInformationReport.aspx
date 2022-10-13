<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SchoolEmpoyeeInformationReport.aspx.cs" Inherits="GEIMS.ReportUI.SchoolEmpoyeeInformationReport" %>
<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--  <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Employee Information
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail"
                OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack1" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />  &nbsp;
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
                                <asp:Panel ID="pnlEmployeeInfo" runat="server" GroupingText="Employee Details">
                                  
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Employee Name :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 65%;">
                                                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="validate[required] TextBox mytext" Width="50%" Height="100%"></asp:TextBox>
                                                <%--<input type="text" class="autosuggest" />--%>
                                               <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                                <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                 <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="divEmployee" runat="server"  style="width: 100%; float: left; padding-top: 0px;">
                                    <asp:DataList ID="dlEmployee" Width="100%" runat="server" align="center" Style="font-family: Verdana;height: 1000px; font-size: 8px; border: solid 2px black" OnItemDataBound="dlEmployee_ItemDataBound">
                                        <ItemTemplate>
                                            <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%;">
                                                <tr>
                                                    <td style="text-align: center" colspan="4"><b style="font-size: 18px; font-weight: bold">શાળા માહિતીનું સંકલન</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center" colspan="4"><b style="font-size: 15px; font-weight: bold">સ્ટાફ યાદી</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; font-weight: bold;" colspan="4">
                                                        <%# Eval("SchoolNameGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;"  colspan="4">ગામ : <%# Eval("TownGuj") %>&nbsp;&nbsp;જિલ્લો : <%# Eval("DistrictGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; border-bottom: 1px solid black;" colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">કર્મચારી નો નંબર :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("EmployeeCode") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">કર્મચારીનું પુરુ નામ  :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("EmployeeFNameGuj") %>&nbsp; <%# Eval("EmployeeMNameGuj") %>&nbsp; <%# Eval("EmployeeLNameGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">હોદ્દો :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("DesignationNameGuj") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td valign="top" align="left" colspan="4">શૈક્ષણિક લાયકાત :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <asp:GridView ID="gvEducationalDetail" runat="server" Width="80%" Font-Names="verdana" AutoGenerateColumns="false"
                                                            Font-Size="12px">
                                                            <Columns>
                                                                <asp:BoundField DataField="QualificationNameGUJ" HeaderText="લાયકાત">
                                                                    <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="UniversityGUJ" HeaderText="યુનિવર્સિટી">
                                                                    <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Year" HeaderText="વર્ષ">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Percentage" HeaderText="ટકાવારી">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="left" colspan="4">વ્યવસાયીક  લાયકાત :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <asp:GridView ID="gvExperience" runat="server" Width="80%" Font-Names="verdana" AutoGenerateColumns="false"
                                                            Font-Size="12px">
                                                            <Columns>
                                                                <asp:BoundField DataField="OrganisationNameGUJ" HeaderText="સંસ્થાનુ નામ">
                                                                    <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DesignationGUJ" HeaderText="હોદ્દો">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DurationYear" HeaderText="અવધિ વર્ષે">
                                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DurationMonth" HeaderText="અવધિ મહિનો">
                                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="JobResponsibilityGUJ" HeaderText="નોકરી ની જવાબદારીઓ">
                                                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CTC" HeaderText="સીટીસી">
                                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ReasonOfLeavingGUJ" HeaderText="છોડવાનુ કારણ ">
                                                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">જન્મ માહિતી
                                                    </td>
                                                    <td style="text-align: left" colspan="3">ગામ :  <%# Eval("BirthCityVillageGuj") %> ,  તાલુકો :  <%# Eval("BirthTalukaGuj") %> , જિલ્લો :  <%# Eval("BirthTalukaGuj") %> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">જાતિ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("CategoryGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">હાલ નોકરી નો વિભાગ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("DepartmentNameGUJ") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">ખાતા જોડાયા તારીખ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("DepartmentJoiningDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">શાળા માં જોડાયા તારીખ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("OrganisationJoiningDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">સીધી નિમણુક કે ફાજલ નિમણુક :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("TypeOfAppointment") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">ફાજલ આવ્યા તારીખ:
                                                    </td>
                                                    <td style="text-align: left" colspan="3"></td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left">ફાજલ આવ્યા શાળા ની મહિતી:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("ReplacementSchoolInfoGUJ") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">નિવૃત્તિ તારીખ:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("RetirementDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">સત્ર અંતે નિવૃત્તિ તારીખ:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("TermEndRetirementDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">પાન કાર્ડ નંબર:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("PANNo") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">બેન્ક એકાઉન્ટ નંબર:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("AccountNo") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left; width:27%">બેન્ક નુ નામ :
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%# Eval("BankName") %>
                                                    </td>
                                                    <td style="text-align: left; width:15%">શાખા નુ નામ :
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%# Eval("BankName") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                        </div>
                        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
                         <div id="divEmployee1"  style="width: 100%; float: left; padding-top: 0px;display:none">
                                    <asp:DataList ID="dlEmployee1" Width="100%" runat="server" align="center" Style="font-family: Verdana;height: 1000px; font-size: 8px; border: solid 2px black" OnItemDataBound="dlEmployee1_ItemDataBound">
                                        <ItemTemplate>
                                            <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%;">
                                                <tr>
                                                    <td style="text-align: center" colspan="4"><b style="font-size: 18px; font-weight: bold">શાળા માહિતીનું સંકલન</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center" colspan="4"><b style="font-size: 15px; font-weight: bold">સ્ટાફ યાદી</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; font-weight: bold;" colspan="4">
                                                        <%# Eval("SchoolNameGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;"  colspan="4">ગામ : <%# Eval("TownGuj") %>&nbsp;&nbsp;જિલ્લો : <%# Eval("DistrictGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; border-bottom: 1px solid black;" colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">કર્મચારી નો નંબર :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("EmployeeCode") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">કર્મચારીનું પુરુ નામ  :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("EmployeeFNameGuj") %>&nbsp; <%# Eval("EmployeeMNameGuj") %>&nbsp; <%# Eval("EmployeeLNameGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">હોદ્દો :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("DesignationNameGuj") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td valign="top" align="left" colspan="4">શૈક્ષણિક લાયકાત :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <asp:GridView ID="gvEducationalDetail1" runat="server" Width="80%" Font-Names="verdana" AutoGenerateColumns="false"
                                                            Font-Size="12px">
                                                            <Columns>
                                                                <asp:BoundField DataField="QualificationNameGUJ" HeaderText="લાયકાત">
                                                                    <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="UniversityGUJ" HeaderText="યુનિવર્સિટી">
                                                                    <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Year" HeaderText="વર્ષ">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Percentage" HeaderText="ટકાવારી">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="left" colspan="4">વ્યવસાયીક  લાયકાત :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <asp:GridView ID="gvExperience1" runat="server" Width="80%" Font-Names="verdana" AutoGenerateColumns="false"
                                                            Font-Size="12px">
                                                            <Columns>
                                                                <asp:BoundField DataField="OrganisationNameGUJ" HeaderText="સંસ્થાનુ નામ">
                                                                    <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DesignationGUJ" HeaderText="હોદ્દો">
                                                                    <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DurationYear" HeaderText="અવધિ વર્ષે">
                                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DurationMonth" HeaderText="અવધિ મહિનો">
                                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="JobResponsibilityGUJ" HeaderText="નોકરી ની જવાબદારીઓ">
                                                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CTC" HeaderText="સીટીસી">
                                                                    <HeaderStyle Width="5%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ReasonOfLeavingGUJ" HeaderText="છોડવાનુ કારણ ">
                                                                    <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" Font-Bold="true" />
                                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">જન્મ માહિતી
                                                    </td>
                                                    <td style="text-align: left" colspan="3">ગામ :  <%# Eval("BirthCityVillageGuj") %> ,  તાલુકો :  <%# Eval("BirthTalukaGuj") %> , જિલ્લો :  <%# Eval("BirthTalukaGuj") %> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">જાતિ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("CategoryGuj") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">હાલ નોકરી નો વિભાગ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("DepartmentNameGUJ") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">ખાતા જોડાયા તારીખ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("DepartmentJoiningDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">શાળા માં જોડાયા તારીખ :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("OrganisationJoiningDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">સીધી નિમણુક કે ફાજલ નિમણુક :
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("TypeOfAppointment") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">ફાજલ આવ્યા તારીખ:
                                                    </td>
                                                    <td style="text-align: left" colspan="3"></td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left">ફાજલ આવ્યા શાળા ની મહિતી:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("ReplacementSchoolInfoGUJ") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">નિવૃત્તિ તારીખ:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("RetirementDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">સત્ર અંતે નિવૃત્તિ તારીખ:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("TermEndRetirementDate") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">પાન કાર્ડ નંબર:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("PANNo") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">બેન્ક એકાઉન્ટ નંબર:
                                                    </td>
                                                    <td style="text-align: left" colspan="3">
                                                        <%# Eval("AccountNo") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left; width:27%">બેન્ક નુ નામ :
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%# Eval("BankName") %>
                                                    </td>
                                                    <td style="text-align: left; width:15%">શાખા નુ નામ :
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%# Eval("BankName") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                    <div style="padding: 10px; padding-right: 30px;">
                        <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                        </div>
                    </div>
                </div>
            </div>
       <%-- </ContentTemplate>
      
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $(document).ready(function () {
            AutoComplete();
        });
        function AutoComplete() {
            $(".mytext").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SchoolEmpoyeeInformationReport.aspx/GetAllEmployeeNameForReport",
                        data: "{'prefixText':'" + request.term + "','TrustMID':'" +  <%=Session[ApplicationSession.TRUSTID] %> + "','SchoolMID':'" + <%=Session[ApplicationSession.SCHOOLID] %> + "'}",
                        dataType: "json",
                        success: function (data) {
                          
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('~')[0],
                                    val: item.split('~')[1]
                                };
                            }));
                           
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfEmployeeMID.ClientID %>").val(i.item.val);
                    $("#<%=hfEmployeeCodeName.ClientID %>").val(i.item.label);
                }
            });
        }

        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });

        $('.Attach').click(function () {
            $("#aspnetForm").validationEngine('attach');
        });
    </script>
</asp:Content>