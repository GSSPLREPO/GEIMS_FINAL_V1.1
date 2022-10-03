<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="SchoolAcoountingInfoReport.aspx.cs" Inherits="GEIMS.ReportUI.SchoolAcoountingInfoReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
           School Accounting Information
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
                        <asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="School Details">
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        શાળા :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:DropDownList ID="ddlSchool" runat="server" CssClass="validate[required] Droptextarea" Width="300px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        ઍસ.વી.ઍસનું નામ :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:TextBox ID="txtSVSName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        ક્યૂડીસી શાળાનું નામ :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:TextBox ID="txtQDCName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        પ્રમુખનું નામ :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:TextBox ID="txtPramukhName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; width: 15%;">
                                        કારકુનનું નામ :<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 85%;">
                                        <asp:TextBox ID="txtClerkName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        પ્રમુખ નો નંબર:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtPramukhNo" runat="server" CssClass="validate[required,custom[onlyNumberSp],maxSize[15],minSize[10]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        કારકુન નો નંબર:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtClerkNo" runat="server" CssClass="validate[required,custom[onlyNumberSp],maxSize[15],minSize[10]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        કમ્પ્યુટરની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtComputerNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        મેદાનનું માપ:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtGroundSize" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        મુતરડીની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtUrinaryRoomNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ફાયર સુરક્ષા સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtFireSaftyNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        ટીવી છે?<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:CheckBox runat="server" ID="chkTv" AutoPostBack="true" OnCheckedChanged="chkTv_CheckedChanged" />
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ઈકો ક્લબ છે?<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:CheckBox runat="server" ID="chkEco" AutoPostBack="true" OnCheckedChanged="chkEco_CheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        NSS ચાલે છે?<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:CheckBox runat="server" ID="chkNSS" AutoPostBack="true" OnCheckedChanged="chkNSS_CheckedChanged" />
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        કેરિયર કોર્નર ચાલે છે?<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:CheckBox runat="server" ID="chkCareer" AutoPostBack="true" OnCheckedChanged="chkCareer_CheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div id="divTv" runat="server" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        ટીવીની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtTV" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ઍલ.સી.ડીની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtLCDNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divEco" runat="server" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        ઈકો ક્લબની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtEcoNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ઇન્ચાર્જ શિક્ષકનું નામ:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtEcoName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divNSS" runat="server" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        NSSની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtNSSNO" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ઇન્ચાર્જ શિક્ષકનું નામ:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtNSSName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divCareer" runat="server" style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        કેરિયર કોર્નરની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtCareerNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ઇન્ચાર્જ શિક્ષકનું નામ:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtCareerName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        મકાનની વિગત:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 85%;">
                                        <asp:TextBox ID="txtBuildingInfo" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        ઓરડાની સંખ્યા:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtRoomNo" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        ઓરડાનું માપ:<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtRoomSize" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        આચાર્ય(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtPrincipalOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        આચાર્ય(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtPrincipalClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        જૂના શિક્ષક(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtTeacherOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        જૂના શિક્ષક(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtTeacherClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        શિ. સહાયક(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtSahayakOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        શિ. સહાયક(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtSahayakClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        કારકુન(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtClerkOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        કારકુન(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtClerkClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        વહીવટી સહાયક(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtVahivatiOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        વહીવટી સહાયક(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtVahivatiClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        પટાવાળા(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtPatavalaOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        પટાવાળા(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtPatavalaClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: Left; width: 15%;">
                                        સાથી સહાયક(ભરાયેલ):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: Left; width: 35%;">
                                        <asp:TextBox ID="txtFSahayakOpen" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        સાથી સહાયક(ખાલી):<span style="color: red">*</span>
                                    </div>
                                    <div style="float: left; width: 35%;">
                                        <asp:TextBox ID="txtFSahayakClose" runat="server" CssClass="validate[required,custom[onlyNumberSp]] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left;" class="label">
                                <div style="padding: 10px;">
                                    <div style="float: left; text-align: right; width: 100%;">
                                        <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                <div style="padding: 10px; padding-right: 30px;">
                                </div>
                            </div>
                        </asp:Panel>
                        <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div id="divTransferGujarati" runat="server" style="padding: 10px; padding-right: 10px; width: 98%; float: left; border: solid 2px black; padding-left: 2px">
                                <asp:DataList ID="dlSchoolInfoGujarati" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;" OnItemDataBound="dlSchoolInfoGujarati_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse">
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width: 100%; border-bottom: 1px solid Black">
                                                        <tr>
                                                            <td style="width: 10%;" align="center">
                                                                <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                            </td>

                                                            <td style="width: 85%; padding-left: 5px;" align="center">
                                                                <h3><%# Eval("TrustName") %></h3>
                                                                <strong style="font-size: 22px">શાળાકીય આંકડાકીય  માહિતી પત્રક</strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">ઍસ.વી.ઍસનું નામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblSVSName"></asp:Label></strong></td>
                                                <td style="width: 25%; padding-left: 7px">ક્યૂડીસી શાળાનું નામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblQdcName"></asp:Label></strong></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">શાળાનું નામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("SchoolName") %></strong></td>
                                                <td style="width: 25%; padding-left: 7px">ફોન નંબર :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("MobileNo") %></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">ગામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("TownGuj") %></strong></td>
                                                <td style="width: 25%; padding-left: 7px">તાલુકો: </td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("TalukaGuj") %></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">સ્થાપના વર્ષ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("ApprovalYear") %></strong></td>
                                                <td style="width: 25%; padding-left: 7px">રજીસ્ટર નંબર : </td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("ApprovalNo") %></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">પ્રમુખનું નામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblPramukhName"></asp:Label></strong></td>
                                                <td style="width: 25%; padding-left: 7px">પ્રમુખ નો નંબર: </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblPramukhNo"></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">આચાર્ય નું નામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("Principal") %></strong></td>
                                                <td style="width: 25%; padding-left: 7px">આચાર્ય નો નંબર: </td>
                                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("PrincipalMobileNo") %></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">કારકુનનું નામ :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblClerkName"></asp:Label></strong></td>
                                                <td style="width: 25%; padding-left: 7px">કારકુન નો નંબર: </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblClerkNo"></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">શાળામાં આવેલ કમ્પ્યુટર ની સંખ્યા :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblComputerNo"></asp:Label></strong></td>
                                                <td style="width: 25%; padding-left: 7px">મેદાન નું માપ : </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblGroundSize"></asp:Label></strong> ચો.મી.</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">શાળામાં મુતરડીની સંખ્યા :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblUrinaryRoom"></asp:Label></strong></td>
                                                <td style="width: 25%; padding-left: 7px">ફાયર સુરક્ષા સંખ્યા : </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblFireNo"></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">ટીવી / ડીટીઍચ છે? :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblTv"></asp:Label></strong></td>
                                                <td colspan="2">
                                                    <table style="width: 100%; font-size: 11px;">
                                                        <tr>
                                                            <td style="width: 30%;">ટીવી ની સંખ્યા :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblTVNo"></asp:Label></strong>
                                                            </td>
                                                            <td style="width: 30%;">ઍલ.સી.ડીની સંખ્યા :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblLCDNo"></asp:Label></strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">ઈકો ક્લબ છે? :</td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblEco"></asp:Label></strong></td>
                                                <td colspan="2">
                                                    <table style="width: 100%; font-size: 11px;">
                                                        <tr>
                                                            <td style="width: 30%;">ક્લબમાં સંખ્યા :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblEcoNo"></asp:Label></strong>
                                                            </td>
                                                            <td style="width: 30%;">ઇન્ચાર્જ શિક્ષકનું નામ :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="llbEcoName"></asp:Label></strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">NSS ચાલે છે? </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblNSS"></asp:Label></strong></td>
                                                <td colspan="2">
                                                    <table style="width: 100%; font-size: 11px;">
                                                        <tr>
                                                            <td style="width: 30%;">NSSમાં સંખ્યા :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblNSSNo"></asp:Label></strong>
                                                            </td>
                                                            <td style="width: 30%;">ઇન્ચાર્જ શિક્ષકનું નામ :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblNssName"></asp:Label></strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">કેરિયર કોર્નર ચાલે છે? </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblCareer"></asp:Label></strong></td>
                                                <td colspan="2">
                                                    <table style="width: 100%; font-size: 11px;">
                                                        <tr>
                                                            <td style="width: 30%;">કેરિયર કોર્નરમાં સંખ્યા :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblCareerCorner"></asp:Label></strong>
                                                            </td>
                                                            <td style="width: 30%;">ઇન્ચાર્જ શિક્ષકનું નામ :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblCareerCornerName"></asp:Label></strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; padding-left: 7px">મકાનની વિગત : </td>
                                                <td style="width: 25%; padding-left: 7px"><strong>
                                                    <asp:Label runat="server" ID="lblBuldingInfo"></asp:Label></strong></td>
                                                <td colspan="2">
                                                    <table style="width: 100%; font-size: 11px;">
                                                        <tr>
                                                            <td style="width: 30%;">ઓરડાની સંખ્યા :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="lblRoomNo"></asp:Label></strong>
                                                            </td>
                                                            <td style="width: 30%;">ઓરડાનું માપ :
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <strong>
                                                                    <asp:Label runat="server" ID="llbRoomSize"></asp:Label>
                                                                    ચો.ફુટ.</strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>


                                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="center">
                                            <strong>શાળામાં આવેલ વર્ગોની સંખ્યા</strong><br />
                                            પૂર્વ પ્રાથમિક વિભાગ
                                            <asp:DataList ID="dlPrePrimary" Width="40%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold; padding-left: 7px;">ધોરણ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">LKG</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">UKG</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વર્ગ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("DivisionLkg")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("DivisionUkg")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વિષય સંખ્યા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>

                                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="center">પ્રાથમિક વિભાગ
                                            <asp:DataList ID="dlPrimary" Width="80%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold; padding-left: 7px;">ધોરણ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">1</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">2</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">3</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">4</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">5</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">6</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">7</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">8</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">9</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વર્ગ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division1")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division2")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division3")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division4")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division5")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division6")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division7")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division8")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division9")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વિષય સંખ્યા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>


                                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="center">માધ્યમિક વિભાગ
                                            <asp:DataList ID="dlSecondary" Width="80%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold; padding-left: 7px;">ધોરણ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">10</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">11 વિજ્ઞાન પ્રવાહ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">11 સામાન્ય પ્રવાહ </td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">12 વિજ્ઞાન પ્રવાહ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">12 સામાન્ય પ્રવાહ </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વર્ગ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division12")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division13")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division14")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division15")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division16")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વિષય સંખ્યા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>


                                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="center">શૈક્ષણિક તથા બિન-શૈક્ષણિક સ્ટાફ ની વિગત
                                            <asp:DataList ID="dlStaffInfo" Width="80%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;" OnItemDataBound="dlStaffInfo_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold;">ક્રમ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">આચાર્ય</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">જૂના શિક્ષક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">શિ.સહાયક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">કારકૂન </td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વહીવટી સહાયક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">પટાવાળા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">સાથી સહાયક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">કુલ</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px">ભરાયેલ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblaacharyOpendl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblJSOpendl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblSSOpendl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblKarkunOpen"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblVSOpendl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblpatavalaOpendl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblFSOpendl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblTotalPrincipalOpendl"></asp:Label></strong></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px">ખાલી</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblaacharyClosedl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblJSClosedl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblSSClosedl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblKarkunClose"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblVSClosedl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblpatavalaClosedl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblFSClosedl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblTotalPrincipalClosedl"></asp:Label></strong></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px">કુલ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblaacharyTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblJSTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblSSTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblKarkunTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblVSTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblpatavalaTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblFSTotaldl"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblTotalPrincipalTotaldl"></asp:Label></strong></td>
                                                        </tr>

                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
            </div>
        </div>

        <div id="divTransferGujaratiPrint" style="width: 100%; display: none;">
            <div id="divTransferGujarati1" runat="server" style="padding-left: 10px; padding-right: 10px; width: 97%; float: left; border: solid 2px black; padding-left: 2px;">
                <asp:DataList ID="dlSchoolInfoGujarati1" Width="98%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;" OnItemDataBound="dlSchoolInfoGujarati1_ItemDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse">
                            <tr>
                                <td colspan="4">
                                    <table style="width: 100%; border-bottom: 1px solid Black">
                                        <tr>
                                            <td style="width: 10%;" align="center">
                                                <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                            </td>

                                            <td style="width: 85%; padding-left: 5px;" align="center">
                                                <h3><%# Eval("TrustName") %></h3>
                                                <strong style="font-size: 22px">શાળાકીય આંકડાકીય  માહિતી પત્રક</strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">ઍસ.વી.ઍસનું નામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblSVSName1"></asp:Label></strong></td>
                                <td style="width: 25%; padding-left: 7px">ક્યૂડીસી શાળાનું નામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblQdcName1"></asp:Label></strong></td>
                            </tr>

                            <tr>
                                <td style="width: 25%; padding-left: 7px">શાળાનું નામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("SchoolName") %></strong></td>
                                <td style="width: 25%; padding-left: 7px">ફોન નંબર :</td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("MobileNo") %></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">ગામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("TownGuj") %></strong></td>
                                <td style="width: 25%; padding-left: 7px">તાલુકો: </td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("TalukaGuj") %></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">સ્થાપના વર્ષ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("ApprovalYear") %></strong></td>
                                <td style="width: 25%; padding-left: 7px">રજીસ્ટર નંબર : </td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("ApprovalNo") %></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">પ્રમુખનું નામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblPramukhName1"></asp:Label></strong></td>
                                <td style="width: 25%; padding-left: 7px">પ્રમુખ નો નંબર: </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblPramukhNo1"></asp:Label></strong></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">આચાર્ય નું નામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("Principal") %></strong></td>
                                <td style="width: 25%; padding-left: 7px">આચાર્ય નો નંબર: </td>
                                <td style="width: 25%; padding-left: 7px"><strong><%# Eval("PrincipalMobileNo") %></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">કારકુનનું નામ :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblClerkName1"></asp:Label></strong></td>
                                <td style="width: 25%; padding-left: 7px">કારકુન નો નંબર: </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblClerkNo1"></asp:Label></strong></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">શાળામાં આવેલ કમ્પ્યુટર ની સંખ્યા :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblComputerNo1"></asp:Label></strong></td>
                                <td style="width: 25%; padding-left: 7px">મેદાન નું માપ : </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblGroundSize1"></asp:Label></strong> ચો.મી.</td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">શાળામાં મુતરડીની સંખ્યા :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblUrinaryRoom1"></asp:Label></strong></td>
                                <td style="width: 25%; padding-left: 7px">ફાયર સુરક્ષા સંખ્યા : </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblFireNo1"></asp:Label></strong></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">ટીવી / ડીટીઍચ છે? :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblTv1"></asp:Label></strong></td>
                                <td colspan="2">
                                    <table style="width: 100%; font-size: 11px;">
                                        <tr>
                                            <td style="width: 30%;">ટીવી ની સંખ્યા :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblTVNo1"></asp:Label></strong>
                                            </td>
                                            <td style="width: 30%;">ઍલ.સી.ડીની સંખ્યા :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblLCDNo1"></asp:Label></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">ઈકો ક્લબ છે? :</td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblEco1"></asp:Label></strong></td>
                                <td colspan="2">
                                    <table style="width: 100%; font-size: 11px;">
                                        <tr>
                                            <td style="width: 30%;">ક્લબમાં સંખ્યા :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblEcoNo1"></asp:Label></strong>
                                            </td>
                                            <td style="width: 30%;">ઇ. શિક્ષકનું નામ :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="llbEcoName1"></asp:Label></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">NSS ચાલે છે? </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblNSS1"></asp:Label></strong></td>
                                <td colspan="2">
                                    <table style="width: 100%; font-size: 11px;">
                                        <tr>
                                            <td style="width: 30%;">NSSમાં સંખ્યા :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblNSSNo1"></asp:Label></strong>
                                            </td>
                                            <td style="width: 30%;">ઇ. શિક્ષકનું નામ :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblNssName1"></asp:Label></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">કેરિયર કોર્નર ચાલે છે? </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblCareer1"></asp:Label></strong></td>
                                <td colspan="2">
                                    <table style="width: 100%; font-size: 11px;">
                                        <tr>
                                            <td style="width: 30%;">કેરિયર કોર્નર સંખ્યા :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblCareerCorner1"></asp:Label></strong>
                                            </td>
                                            <td style="width: 30%;">ઇ. શિક્ષકનું નામ :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblCareerCornerName1"></asp:Label></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding-left: 7px">મકાનની વિગત : </td>
                                <td style="width: 25%; padding-left: 7px"><strong>
                                    <asp:Label runat="server" ID="lblBuldingInfo1"></asp:Label></strong></td>
                                <td colspan="2">
                                    <table style="width: 100%; font-size: 11px;">
                                        <tr>
                                            <td style="width: 30%;">ઓરડાની સંખ્યા :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="lblRoomNo1"></asp:Label></strong>
                                            </td>
                                            <td style="width: 30%;">ઓરડાનું માપ :
                                            </td>
                                            <td style="width: 20%;">
                                                <strong>
                                                    <asp:Label runat="server" ID="llbRoomSize1"></asp:Label>
                                                    ચો.ફુટ.</strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>

                <br />
                <br />
                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                    <tr>
                        <td align="center">
                            <strong>શાળામાં આવેલ વર્ગોની સંખ્યા</strong><br />
                            પૂર્વ પ્રાથમિક વિભાગ
                                            <asp:DataList ID="dlPrePrimary1" Width="40%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold; padding-left: 7px;">ધોરણ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">LKG</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">UKG</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વર્ગ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("DivisionLkg")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("DivisionUkg")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વિષય સંખ્યા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                    <tr>
                        <td align="center">પ્રાથમિક વિભાગ
                                            <asp:DataList ID="dlPrimary1" Width="80%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold; padding-left: 7px;">ધોરણ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">1</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">2</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">3</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">4</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">5</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">6</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">7</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">8</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">9</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વર્ગ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division1")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division2")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division3")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division4")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division5")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division6")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division7")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division8")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division9")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વિષય સંખ્યા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                        </td>
                    </tr>
                </table>

                <br />
                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                    <tr>
                        <td align="center">માધ્યમિક વિભાગ
                                            <asp:DataList ID="dlSecondary1" Width="80%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold; padding-left: 7px;">ધોરણ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">10</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">11 વિજ્ઞાન પ્રવાહ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">11 સામાન્ય પ્રવાહ </td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">12 વિજ્ઞાન પ્રવાહ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">12 સામાન્ય પ્રવાહ </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વર્ગ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division12")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division13")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division14")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division15")%></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><%# Eval("Division16")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વિષય સંખ્યા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                        </td>
                    </tr>
                </table>

                <br />
                <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border-collapse: collapse;">
                    <tr>
                        <td align="center">શૈક્ષણિક તથા બિન-શૈક્ષણિક સ્ટાફ ની વિગત
                                            <asp:DataList ID="dlStaffInfo1" Width="80%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px;" OnItemDataBound="dlStaffInfo1_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%; font-size: 11px; vertical-align: top; height: 100%; border: solid 1px black; border-collapse: collapse">
                                                        <tr>
                                                            <td style="width: 17%; border: solid 1px black; font-weight: bold;">ક્રમ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">આચાર્ય</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">જૂના શિક્ષક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">શિ.સહાયક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">કારકૂન </td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">વહીવટી સહાયક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">પટાવાળા</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">સાથી સહાયક</td>
                                                            <td style="border: solid 1px black; padding-left: 7px; font-weight: bold;">કુલ</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px">ભરાયેલ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblaacharyOpendl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblJSOpendl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblSSOpendl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblKarkunOpen1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblVSOpendl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblpatavalaOpendl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblFSOpendl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblTotalPrincipalOpendl1"></asp:Label></strong></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px">ખાલી</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblaacharyClosedl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblJSClosedl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblSSClosedl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblKarkunClose1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblVSClosedl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblpatavalaClosedl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblFSClosedl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblTotalPrincipalClosedl1"></asp:Label></strong></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border: solid 1px black; padding-left: 7px">કુલ</td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblaacharyTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblJSTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblSSTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblKarkunTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblVSTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblpatavalaTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblFSTotaldl1"></asp:Label></strong></td>
                                                            <td style="border: solid 1px black; padding-left: 7px"><strong>
                                                                <asp:Label runat="server" ID="lblTotalPrincipalTotaldl1"></asp:Label></strong></td>
                                                        </tr>

                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </div>
        </div>
    </div>
    <%--   </ContentTemplate>
       
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
    </script>
</asp:Content>
