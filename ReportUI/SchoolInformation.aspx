<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="SchoolInformation.aspx.cs" Inherits="GEIMS.Reports.SchoolInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            School Information
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
            &nbsp;
             <asp:Button ID="btnBack1" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />
              &nbsp;
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
                    <div id="tabs-1" style="min-height: 50px;">
                        <asp:Panel ID="pnlSchoolInfo" runat="server" GroupingText="School Details">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 60%;">
                                        School Name :<span style="color: red">*</span>&nbsp;&nbsp;&nbsp;
                                           <asp:DropDownList ID="ddlSchoolName" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true">
                                           </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div id="divSchool" runat="server" style="width: 100%; padding-top: 0px;">
                            <asp:DataList ID="dlSchool" Width="100%" runat="server" align="center" Style="font-family: Verdana; height: 900px; font-size: 10px; border: solid 2px black">
                                <ItemTemplate>
                                    <table style="width: 100%; font-size: 14px; padding-left: 7px; vertical-align: top; height: 100%;">
                                        <tr>
                                            <td style="text-align: center; vertical-align: central; border-bottom: 1px solid black;" colspan="4"><b style="font-size: 18px; font-weight: bold">શાળા ની સામાન્ય માહિતી</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">શાળા નુ નામ :
                                            </td>
                                            <td style="text-align: left; font-weight: bold;" colspan="3">
                                                <%# Eval("SchoolNameGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">શાળા નુ પુરુ સરનામુ :
                                            </td>
                                            <td style="text-align: left" colspan="3">
                                                <%# Eval("AddressGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 32%">અન્ય  માહિતી
                                            </td>
                                            <td style="text-align: left;" colspan="3">ગામ :  <%# Eval("TownGuj") %> , જિલ્લો :  <%# Eval("DistrictGuj") %> , રાજ્ય :  <%# Eval("StateGuj") %>
                                        </tr>

                                        <tr>
                                            <td style="text-align: left">ફોન નંબર :
                                            </td>
                                            <td style="text-align: left; width: 25%">
                                                <%# Eval("TelephoneNo") %>
                                            </td>
                                            <td style="text-align: left">પીન  નંબર :
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <%# Eval("Pincode") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">શાળા નો સમય :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("SchoolTiming") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">શાળા ના આચાર્ય નુ નામ :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("EmployeeName") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">આચાર્ય નુ સરનામુ :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("EmployeeAddressGUJ") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">આચાર્ય નો પીન  નંબર :
                                            </td>
                                            <td style="text-align: left;">
                                                <%# Eval("EmployeePinCode") %>
                                            </td>
                                            <td style="text-align: left">આચાર્ય નો મોબાઇલ નંબર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("EmployeeMobileNo") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">મંજૂરી નંબર :
                                            </td>
                                            <td style="text-align: left;">
                                                <%# Eval("ApprovalNo") %>
                                            </td>
                                            <td style="text-align: left">મંજૂરી ની તારીખ :
                                            </td>
                                            <td style="text-align: left;">
                                                <%# Eval("ApprovalDate") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">શાળા નો વિસ્તાર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("AreaTypeGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">શાળા નો પેટા વિસ્તાર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("AreaSubTypeGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">એસ.એસ.સી. ઇન્ડેક્સ નંબર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("SSCindexNo") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">વિજ્ઞાન પ્રવાહ ઇન્ડેક્સ નંબર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("HSCScienceIndexNo") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">સામાન્ય પ્રવાહ ઇન્ડેક્સ નંબર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("HSCCommerceIndexNO") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">આર્ટસ પ્રવાહ ઇન્ડેક્સ નંબર:
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("HSCArtsIndexNo") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">નોધણીક્રુત નંબર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("RegistrationCode") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">નોધણીક્રુત નામ :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("RegistreredNameGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">નોધણીક્રુત સરનામું :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("RegisteredAddressGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">શાળા નુ સૂત્ર :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("SchoolMottoGuj") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">શાળા ની દ્રષ્ટિ :
                                            </td>
                                            <td style="text-align: left;" colspan="3">
                                                <%# Eval("SchoolVisionGuj") %>
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
            </div>
        </div>
        <div id="divSchool1" style="width: 100%; padding-top: 0px; display: none;">
            <asp:DataList ID="dlSchool1" Width="100%" runat="server" align="center" Style="font-family: Verdana; height: 1000px; font-size: 10px; border: solid 2px black">
                <ItemTemplate>
                    <table style="width: 100%; font-size: 14px; padding-left: 7px; vertical-align: top; height: 100%;">
                        <tr>
                            <td style="text-align: center; vertical-align: central; border-bottom: 1px solid black;" colspan="4"><b style="font-size: 20px; font-weight: bold">શાળા ની સામાન્ય માહિતી</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">શાળા નુ નામ :
                            </td>
                            <td style="text-align: left; font-weight: bold;" colspan="3">
                                <%# Eval("SchoolNameGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">શાળા નુ પુરુ સરનામુ :
                            </td>
                            <td style="text-align: left" colspan="3">
                                <%# Eval("AddressGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 32%">અન્ય  માહિતી
                            </td>
                            <td style="text-align: left;" colspan="3">ગામ :  <%# Eval("TownGuj") %> , જિલ્લો :  <%# Eval("DistrictGuj") %> , રાજ્ય :  <%# Eval("StateGuj") %>
                        </tr>

                        <tr>
                            <td style="text-align: left">ફોન નંબર :
                            </td>
                            <td style="text-align: left; width: 25%">
                                <%# Eval("TelephoneNo") %>
                            </td>
                            <td style="text-align: left">પીન  નંબર :
                            </td>
                            <td style="text-align: left; width: 20%">
                                <%# Eval("Pincode") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">શાળા નો સમય :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("SchoolTiming") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">શાળા ના આચાર્ય નુ નામ :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("EmployeeName") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">આચાર્ય નુ સરનામુ :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("EmployeeAddressGUJ") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">આચાર્ય નો પીન  નંબર :
                            </td>
                            <td style="text-align: left;">
                                <%# Eval("EmployeePinCode") %>
                            </td>
                            <td style="text-align: left">આચાર્ય નો મોબાઇલ નંબર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("EmployeeMobileNo") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">મંજૂરી નંબર :
                            </td>
                            <td style="text-align: left;">
                                <%# Eval("ApprovalNo") %>
                            </td>
                            <td style="text-align: left">મંજૂરી ની તારીખ :
                            </td>
                            <td style="text-align: left;">
                                <%# Eval("ApprovalDate") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">શાળા નો વિસ્તાર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("AreaTypeGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">શાળા નો પેટા વિસ્તાર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("AreaSubTypeGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">એસ.એસ.સી. ઇન્ડેક્સ નંબર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("SSCindexNo") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">વિજ્ઞાન પ્રવાહ ઇન્ડેક્સ નંબર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("HSCScienceIndexNo") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">સામાન્ય પ્રવાહ ઇન્ડેક્સ નંબર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("HSCCommerceIndexNO") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">આર્ટસ પ્રવાહ ઇન્ડેક્સ નંબર:
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("HSCArtsIndexNo") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">નોધણીક્રુત નંબર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("RegistrationCode") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">નોધણીક્રુત નામ :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("RegistreredNameGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">નોધણીક્રુત સરનામું :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("RegisteredAddressGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">શાળા નુ સૂત્ર :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("SchoolMottoGuj") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">શાળા ની દ્રષ્ટિ :
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <%# Eval("SchoolVisionGuj") %>
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

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });

    </script>
</asp:Content>
