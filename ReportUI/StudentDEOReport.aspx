<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentDEOReport.aspx.cs" Inherits="GEIMS.ReportUI.StudentDEOReport" %>

<%@ Import Namespace="GEIMS.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Student Reports
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
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

                            <div id="tabs-1" style="min-height: 150px;">
                                <asp:Panel ID="pnlStudentInfo" runat="server" GroupingText="Student Details">
                                    <asp:HiddenField runat="server" ID="hfSearchName" />
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                <asp:Label runat="server" ID="lblStudentName" Text="StudentName"></asp:Label><asp:Label runat="server" ID="lblStudentNameGujarati" Text="વિદ્યાર્થીનું નામ :" Visible="false"></asp:Label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:TextBox ID="txtStudentName" runat="server" CssClass="validate[required] TextBox autosuggest" Width="50%" Height="100%" AutoPostBack="true" OnTextChanged="txtStudentName_TextChanged"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfSchoolMID" />
                                                <asp:HiddenField runat="server" ID="hfTrustMID" />
                                                <asp:HiddenField runat="server" ID="hfStudentMID" />
                                                <asp:HiddenField runat="server" ID="hfStudentCodeName" />
                                                <asp:HiddenField runat="server" ID="hfMode" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Type:<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 35%;">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="validate[required] Droptextarea" Width="210px" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">Bonafide Certificate</asp:ListItem>
                                                    <asp:ListItem Value="2">Leaving Certificate</asp:ListItem>
                                                    <asp:ListItem Value="3">Attempt Certificate</asp:ListItem>
                                                    <asp:ListItem Value="4">Transfer Certificate</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: Left; width: 15%;">
                                                IsEnglish :
                                            </div>
                                            <div style="float: Left; width: 35%;">
                                                <asp:CheckBox runat="server" ID="chkEngish" Checked="true" AutoPostBack="true" OnCheckedChanged="chkEngish_CheckedChanged"></asp:CheckBox>
                                            </div>


                                        </div>
                                    </div>
                                    <div id="divLeft" runat="server" style="width: 100%; float: left;" class="label">
                                        <div style="width: 100%; float: left;" class="label">
                                            <div style="padding: 10px;">
                                                <div style="float: Left; width: 15%;">
                                                    IsSubmitted :
                                                </div>
                                                <div style="float: Left; width: 35%;">
                                                    <asp:CheckBox runat="server" ID="chkSubmitted"></asp:CheckBox>
                                                </div>
                                                <div style="float: Left; width: 15%;">
                                                    Submission Date :
                                                </div>
                                                <div style="float: Left; width: 35%;">
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDate" TargetControlID="txtDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div id="divTransfer" runat="server" style="width: 100%; float: left;" class="label">
                                        <div style="width: 100%; float: left;" class="label">
                                            <div style="padding: 10px;">
                                                <div style="float: left; width: 15%;">
                                                    આચાર્યનું નામ :<span style="color: red">*</span>
                                                </div>
                                                <div style="float: left; width: 85%;">
                                                    <asp:TextBox ID="txtPrincipalName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="width: 100%; float: left;" class="label">
                                            <div style="padding: 10px;">
                                                <div style="float: Left; width: 15%;">
                                                    શાળનો ડાયસ કોડ:
                                                </div>
                                                <div style="float: Left; width: 35%;">
                                                    <asp:TextBox ID="txtDiesCode" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                                </div>
                                                <div style="float: left; width: 15%;">
                                                    કલસ્ટરનું નામ :<span style="color: red">*</span>
                                                </div>
                                                <div style="float: left; width: 35%;">
                                                    <asp:TextBox ID="txtkalstarName" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div id="divAttempt" runat="server" style="width: 100%; float: left;" class="label">
                                        <div style="width: 100%; float: left;" class="label">
                                            <div style="padding: 10px;">
                                                <div style="float: Left; width: 15%;">
                                                    Percent :
                                                </div>
                                                <div style="float: Left; width: 35%;">
                                                    <asp:TextBox ID="txtPercent" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                                </div>
                                                <div style="float: left; width: 15%;">
                                                    Attempt :<span style="color: red">*</span>
                                                </div>
                                                <div style="float: left; width: 35%;">
                                                    <asp:TextBox ID="txtAttempt" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="width: 100%; float: left;" class="label">
                                            <div style="padding: 10px;">
                                                <div style="float: Left; width: 15%;">
                                                    Year :
                                                </div>
                                                <div style="float: Left; width: 35%;">
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:DropDownList>
                                                </div>
                                                <div style="float: left; width: 15%;">
                                                    Month :<span style="color: red">*</span>
                                                </div>
                                                <div style="float: left; width: 35%;">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="width: 100%; float: left;" class="label">
                                            <div style="padding: 10px;">
                                                <div style="float: Left; width: 15%;">
                                                    Seat No :
                                                </div>
                                                <div style="float: Left; width: 35%;">
                                                    <asp:TextBox ID="txtSeatNo" runat="server" CssClass="validate[required] TextBox" Width="50%" Height="100%"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; text-align: right; width: 100%;">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium" OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                                    <div id="divBonafideEnglish" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left" visible="true">
                                        <asp:DataList ID="dlBonafideEnglish" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlBonafideEnglish_ItemDataBound" Visible="true">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; border-bottom: 1px solid Black">
                                                                <tr>
                                                                    <td style="width: 10%;" align="center">
                                                                        <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                    </td>

                                                                    <td style="width: 85%; padding-left: 5px;" align="center">
                                                                        <h3><%# Eval("TrustName") %></h3>
                                                                        <strong style="font-size: 22px">BONAFIDE CERTIFICATE</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">This is to certify that
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">Mr./Miss <strong><%# Eval("StudentName") %></strong> is a bonafide student of  <strong><%# Eval("SchoolName") %>.</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">According to the general register of the school his/her birthdate is <strong><%# Eval("BirthDate") %>.</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">(In Word):<strong><asp:Label runat="server" ID="lblBirthDateInWords"></asp:Label>.</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">To the best of my knowledge He/She bears good moral character.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">He/She is currently studying in Standard <strong><%# Eval("Class") %>/ <%# Eval("Division") %> ( <%# Eval("Year") %> ).</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">His/Her Place of Birth is <strong><%# Eval("BirthDistict")%>.</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;">Date :<strong>
                                                                        <asp:Label runat="server" ID="lblDate"></asp:Label></strong>
                                                                    </td>

                                                                    <td style="text-align: right; padding-right: 90px">Authorised Signatory.
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">Place : <strong><%# Eval("SchoolDistict") %>.</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div id="divBonafideGujarati" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                                        <asp:DataList ID="dlBonafideGujarati" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlBonafideGujarati_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; border-bottom: 1px solid Black">
                                                                <tr>
                                                                    <td style="width: 10%;" align="center">
                                                                        <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                    </td>

                                                                    <td style="width: 85%; padding-left: 5px;" align="center">
                                                                        <h3><%# Eval("TrustName") %></h3>
                                                                        <strong style="font-size: 22px">બોનાફાઇડ સર્ટિફિકેટ</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">આથી પ્રમાણિત કરવામાં આવે છે કે <strong><%# Eval("StudentName") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">અત્રેની શાળામાં ધોરણ <strong><%# Eval("Class") %> / <%# Eval("Division") %> ( <%# Eval("Year") %> )</strong>
                                                            માં અભ્યાસ કરે છે.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">તેમનું નામ જન્મ પ્રમાણપત્ર ના / ઍલ. સી. ના આધારે છે. તેમની જન્મતારીખ <strong><%# Eval("BirthDate") %></strong>
                                                            અને જાતિ <strong><%# Eval("Category") %></strong> છે.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">વિદ્યાર્થીનું જન્મ સ્થળ <strong><%# Eval("BirthDistict") %></strong>
                                                            છે.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">જે બાબતોની ચકાસણી શાળાના રેકોર્ડ પરથી ખરાઈ કરીને આપવામાં આવેલ છે. જે બરાબર છે.
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;">તારીખ :<strong>
                                                                        <asp:Label runat="server" ID="lblDateGuj"></asp:Label></strong>
                                                                    </td>

                                                                    <td style="text-align: right; padding-right: 90px">અધિકૃત સહી.
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">સ્થળ : <strong><%# Eval("SchoolDistict") %>.</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div id="divLeavingEnglish" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                                        <asp:DataList ID="dlLeavingEnglish" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingEnglish_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%; border-collapse: collapse">
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                                                            <tr>
                                                                                <td style="width: 10%;" align="center">
                                                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                                </td>

                                                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                                                    <strong>
                                                                                        <asp:Label runat="server" ID="lblDuplicateEng"></asp:Label></strong>
                                                                                    <h3><%# Eval("TrustName") %></h3>
                                                                                    <strong style="font-size: 22px">School Leaving Certificate</strong>
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td align="center">School Index No : <%# Eval("IndexNo") %></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 24px">Pupil's Register No:  <%# Eval("Grno") %></td>
                                                                    <td colspan="2">&nbsp;</td>
                                                                    <td>Serial No:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">1. Name of the pupil begining with surname 
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("StudentName") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">2. Mother's Name
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("MotherName") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">3. Caste(With subcaste)
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("Caste") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">4. Place of Birth
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("BirthDistict") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">5. Date of Birth, Month and Year according to christian era in figures & words
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">Figure   :  <%# Eval("BirthDate") %><br />
                                                                        Words   :
                                                                      <asp:Label runat="server" ID="lblDateInWords"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">6. Last School Attended
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2"><%# Eval("SchoolName") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">7. Date of Admission(With Standard)
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("AdmissionDate") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">8. Progress
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">9. Conduct
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">10. Date Of Leaving School
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("LeftDate") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">11. Standrd in which studying and since when?
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("LeftStd") %> Since <%# Eval("AdmittedClass") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">12. Reason Of Leaving
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <%# Eval("LeftReason") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">13. Attendance (No. of Days)
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">In std. <%# Eval("Class") %> During the school Year <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> days out of <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">14. Fees due to the school paid or not?
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                                                        <asp:Label runat="server" ID="lblFeesStatus"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">15. Remarks
                                                                    </td>
                                                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 50px" colspan="4">Certified that the above information is in accordance with the school register.
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 50px" colspan="4">Date: &nbsp;  
                                                            <asp:Label runat="server" ID="lblDateForLeaving"></asp:Label>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 50px" colspan="4">Place: &nbsp;  
                                                            <%# Eval("SchoolDistict") %> 
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">Checked By
                                                                    </td>
                                                                    <td style="text-align: right;">Class Teacher
                                                                    </td>
                                                                    <td style="text-align: right; padding-right: 100px">Clerk
                                                                    </td>
                                                                    <td style="text-align: right; padding-right: 100px">Head Master
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="font-size: 9px; padding-left: 50px">
                                                                        <strong>Note:</strong> No change in entry in this certificate 
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div id="divLeavingGujarati" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                                        <asp:DataList ID="dlLeavingGujarati" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingGujarati_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 14px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%; border-bottom: 1px solid Black">
                                                                <tr>
                                                                    <td style="width: 10%;" align="center">
                                                                        <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                    </td>

                                                                    <td style="width: 85%; padding-left: 5px;" align="center">
                                                                        <strong>
                                                                            <asp:Label runat="server" ID="lblDuplicateGuj"></asp:Label></strong><br />
                                                                        <strong style="font-size: 17px"><%# Eval("TrustName") %></strong><br />
                                                                        <strong>શાળા નો ઇન્ડેક્ષ નંબર.</strong>  <%# Eval("IndexNo") %><br />
                                                                        <strong>શાળા છોડ્યા નું પ્રમાણપત્ર</strong>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">વિદ્યાર્થી નો જનરલ રજીસ્ટર નંબર. <u><%# Eval("Grno") %></u></td>
                                                        <td style="width: 30%; padding-left: 7px">&nbsp;</td>
                                                        <td style="text-align: right; padding-left: 7px">અનુક્રમ નંબર. <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; padding-left: 7px" colspan="2">1. વિદ્યાર્થી નું પુરું નામ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("StudentName") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">2. માતા નું નામ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("MotherName") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">3. ધર્મ અને જાતિ (પેટા જ્ઞાતિ સાથે)
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("Caste") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">4. જન્મ સ્થળ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("BirthDistict") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">5. જન્મ તારીખ (આંકડામાં)
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("BirthDate") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 96px">(શબ્દોમાં)
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">&nbsp;
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">6. છેલ્લી કઈ શાળામાં અભ્યાસ કર્યો
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("SchoolName") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">7. શાળામાં દાખલ થયાની તારીખ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("AdmissionDate") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">કઈ શ્રેણીમાં દાખલ થયા
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("AdmittedClass") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 24%; padding-left: 5px">8 . પ્રગતિ
                                                                    </td>
                                                                    <td style="border-bottom: 1px solid black; width: 25%">&nbsp;
                                                                    </td>
                                                                    <td style="width: 25%">9 . વર્તણુક
                                                                    </td>
                                                                    <td style="border-bottom: 1px solid black; width: 26%">&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">10. શાળા છોડ્યા તારીખ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black">
                                                            <%# Eval("LeftDate") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 24%">11 . કઈ શ્રેણીમાં હતા?(આંકડામાં)
                                                                    </td>
                                                                    <td style="border-bottom: 1px solid black; width: 25%; padding-left: 7px"><%# Eval("LeftStd") %> 
                                                                    </td>
                                                                    <td style="width: 25%; padding-left: 7px">(શબ્દોમાં)
                                                                    </td>
                                                                    <td style="border-bottom: 1px solid black; width: 26%">&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 24%; padding-left: 5px">ક્યારથી (આંકડામાં)
                                                                    </td>
                                                                    <td style="border-bottom: 1px solid black; width: 25%; padding-left: 7px"><%# Eval("AdmittedClass") %>
                                                                    </td>
                                                                    <td style="width: 25%; padding-left: 7px">(શબ્દોમાં)
                                                                    </td>
                                                                    <td style="border-bottom: 1px solid black; width: 26%; padding-left: 7px">&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">12. શાળા છોડ્યા નું કારણ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <%# Eval("LeftReason") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">13. નોંધ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">14. હાજર દિવસ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 7px">15. ફી સ્થિતિ
                                                        </td>
                                                        <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                                            <asp:Label runat="server" ID="lblFeesStatusGuj"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 50px" colspan="4">આથી પ્રમાણપત્ર આપવામાં આવે છે કે ઉપર્યુક્ત માહિતી શાળાના જનરલ રજીસ્ટર પ્રમાણે બરાબર છે.
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 50px" colspan="4">તારીખ. &nbsp;  
                                                            <asp:Label runat="server" ID="lblDateForLeavingGuj"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 50px" colspan="4">સ્થળ. &nbsp;  
                                                            <%# Eval("SchoolDistict") %> 
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">ચકાસણી :
                                                        </td>
                                                        <td style="text-align: right;">વર્ગ શિક્ષક
                                                        </td>
                                                        <td style="text-align: right;">અન્વેષક 
                                                        </td>
                                                        <td style="text-align: right; padding-right: 300px">આચાર્ય
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="font-size: 9px; padding-left: 50px" align="center">
                                                            <strong>નોંધ:</strong> આ પ્રમાણપત્ર આપનાર અધિકારી સિવાય કોઈથી આમાં ફેરફાર થઈ શકશે નહિ. ફેરફાર કરનાર સામે યોગ્ય પગલા લેવામાં આવશે.<br />
                                                            ગ્રાન્ટ ઇન ઍઇડના પ્રકરણ 11 ના વિભાગ 2 ના નિયમ 14 અને 30 પ્રમાણે
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div id="divTransferGujarati" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                                        <asp:DataList ID="dlTransferGujarati" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlTransferGujarati_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; border-bottom: 1px solid Black">
                                                                <tr>
                                                                    <td style="width: 10%;" align="center">
                                                                        <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                    </td>

                                                                    <td style="width: 85%; padding-left: 5px;" align="center">
                                                                        <h3><%# Eval("TrustName") %></h3>
                                                                        <strong style="font-size: 22px">વિદ્યાર્થીનો ટ્રાન્સફર અહેવાલ</strong>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">પ્રતિ, 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">આચાર્યશ્રી, <strong>
                                                            <asp:Label runat="server" ID="lblPrincipal"></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">સવિનય જણાવવાનું કે આપની શાળામાં વર્ષ <strong><%# Eval("Year") %></strong>દરમ્યાન ધોરણ : <strong><%# Eval("Class") %></strong> માં અભ્યાસ કરતો વિદ્યાર્થી / કરતી વિદ્યાર્થીની ની નીચેની વિગત આપવા મહેરબાની કરશો.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">વિદ્યાર્થી નું નામ : <strong><%# Eval("StudentName") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">શાળાનો ડાયસ કોડ : <strong>
                                                            <asp:Label runat="server" ID="lblSchoolDiesCode"></asp:Label></strong>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">કલસ્ટરનું નામ  : <strong>
                                                            <asp:Label runat="server" ID="lblKalstar"></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">વિદ્યાર્થી નો  અનન્ય નંબર  : <strong><%# Eval("GVUniqueID") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">વિદ્યાર્થી નો  જી.આર . નંબર  : <strong><%# Eval("Grno") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;">તારીખ :<strong>
                                                                        <asp:Label runat="server" ID="lblDateGujTransfer"></asp:Label></strong>
                                                                    </td>

                                                                    <td style="text-align: right; padding-right: 90px">આચાર્યશ્રીની સહી.
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div id="divAttemptEnglish" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                                        <asp:DataList ID="dlAttemptEnglish" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingEnglish_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; border-bottom: 1px solid Black">
                                                                <tr>
                                                                    <td style="width: 10%;" align="center">
                                                                        <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                    </td>

                                                                    <td style="width: 85%; padding-left: 5px;" align="center">
                                                                        <strong style="font-size: 22px">ATTEMPT CERTIFICATE</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">G.R.No : &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("GRNO") %> </strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">Name :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("StudentName") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">Standard :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Standard") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong> Division :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Division") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  Percent :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Percent") %> </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">Year :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Year") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; Month : &nbsp;&nbsp;&nbsp;&nbsp;  <strong><%# Eval("Month") %></strong> &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; Attempt :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Attempt") %> </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">Seat :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("SeatNo") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong>Gujarat Boards.</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>

                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div id="divAttemptGujarati" runat="server" style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                                        <asp:DataList ID="dlAttemptGujarati" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingGujarati_ItemDataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%; border-bottom: 1px solid Black">
                                                                <tr>
                                                                    <td style="width: 10%;" align="center">
                                                                        <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                                    </td>

                                                                    <td style="width: 85%; padding-left: 5px;" align="center">
                                                                        <strong style="font-size: 22px">ATTEMPT CERTIFICATE</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">જી.આર.નંબર : &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("GRNO") %> </strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">નામ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("StudentName") %></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">ધોરણ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Standard") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong> વિભાગ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Division") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  Percent :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Percent") %> </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">વર્ષ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Year") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; મહિનો : &nbsp;&nbsp;&nbsp;&nbsp;  <strong><%# Eval("Month") %></strong> &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; Attempt :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Attempt") %> </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px">બેઠક નંબર :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("SeatNo") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong>ગુજરાત બોર્ડ.</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 120px"></td>
                                                    </tr>

                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
                    </div>
                </div>

                <div id="divBonafideEnglishPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlBonafideEnglish1" Width="98%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlBonafideEnglish1_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <h3><%# Eval("TrustName") %></h3>
                                                    <strong style="font-size: 22px">BONAFIDE CERTIFICATE</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">This is to certify that
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">Mr./Miss <strong><%# Eval("StudentName") %></strong> is a bonafide student of  <strong><%# Eval("SchoolName") %>.</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">According to the general register of the school his/her birthdate is <strong><%# Eval("BirthDate") %>.</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">(In Word):<strong><asp:Label runat="server" ID="lblBirthDateInWords1"></asp:Label>.</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">To the best of my knowledge He/She bears good moral character.
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">He/She is currently studying in Standard  <strong><%# Eval("Class") %>/ <%# Eval("Division") %> ( <%# Eval("Year") %> ).</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">His/Her Place of Birth is <strong><%# Eval("BirthDistict") %>.</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%;">Date :<strong>
                                                    <asp:Label runat="server" ID="lblDate1"></asp:Label></strong>
                                                </td>

                                                <td style="text-align: right; padding-right: 90px">Authorised Signatory.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">Place : <strong><%# Eval("SchoolDistict") %></strong></td>
                                </tr>
                                <tr>
                                    <td style="height: 170px">&nbsp;</td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divBonafideGujaratiPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlBonafideGujarati1" Width="98%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlBonafideGujarati1_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <h3><%# Eval("TrustName") %></h3>
                                                    <strong style="font-size: 22px">બોનાફાઇડ સર્ટિફિકેટ</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">આથી પ્રમાણિત કરવામાં આવે છે કે <strong><%# Eval("StudentName") %></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">અત્રેની શાળામાં ધોરણ <strong><%# Eval("Class") %> / <%# Eval("Division") %> ( <%# Eval("Year") %> )</strong>
                                        માં અભ્યાસ કરે છે.
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">તેમનું નામ જન્મ પ્રમાણપત્ર ના / ઍલ. સી. ના આધારે છે. તેમની જન્મતારીખ <strong><%# Eval("BirthDate") %></strong>
                                        અને  જાતિ <strong><%# Eval("Category") %></strong> છે.
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">વિદ્યાર્થીનું જન્મ સ્થળ <strong><%# Eval("BirthDistict") %></strong>
                                        છે.
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">જે બાબતોની ચકાસણી શાળાના રેકોર્ડ પરથી ખરાઈ કરીને આપવામાં આવેલ છે. જે બરાબર છે.
                                    </td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%;">તારીખ :<strong>
                                                    <asp:Label runat="server" ID="lblDateGuj1"></asp:Label></strong>
                                                </td>

                                                <td style="text-align: right; padding-right: 90px">અધિકૃત સહી.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">સ્થળ : <strong><%# Eval("SchoolDistict") %>.</strong></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 240px">&nbsp;</td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divLeavingEngPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlLeavingEnglish1" Width="98%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingEnglish1_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 14px; vertical-align: top; height: 1040px; border-collapse: collapse; border: 1px solid black">
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr id="trDuplicate1" runat="server" visible="false">
                                                <td></td>
                                                <td align="center"><strong>(DUPLICATE)</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <strong>
                                                        <asp:Label runat="server" ID="lblDuplicateEng1"></asp:Label></strong>
                                                    <h3><%# Eval("TrustName") %></h3>

                                                    <strong style="font-size: 22px">School Leaving Certificate</strong>

                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td align="center">School Index No : <%# Eval("IndexNo") %></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 24px">Pupil's Register No:  <%# Eval("Grno") %></td>
                                    <td colspan="2">&nbsp;</td>
                                    <td>Serial No:</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">1. Name of the pupil begining with surname 
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("StudentName") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">2. Mother's Name
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("MotherName") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">3. Caste(With subcaste)
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("Caste") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">4. Place of Birth
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("BirthDistict") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">5. Date of Birth, Month and Year according to christian era in figures & words
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">Figure   :  <%# Eval("BirthDate") %><br />
                                        Words   :
                                                            <asp:Label runat="server" ID="lblDateInWord1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">6. Last School Attended
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">7. Date of Admission(With Standard)
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("AdmissionDate") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">8. Progress
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">9. Conduct
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">10. Date Of Leaving School
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("LeftDate") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">11. Standrd in which studying and since when?
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("LeftStd") %> Since <%# Eval("AdmittedClass") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">12. Reason Of Leaving
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <%# Eval("LeftReason") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">13. Attendance (No. of Days)
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">In std. <%# Eval("Class") %> During the school Year <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> days out of <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">14. Fees due to the school paid or not?
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">
                                        <asp:Label runat="server" ID="lblFeesStatus1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; border: 1px solid black; padding-left: 7px" colspan="2">15. Remarks
                                    </td>
                                    <td style="border: 1px solid black; padding-left: 7px" colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 50px" colspan="4">Certified that the above information is in accordance with the school register.
                                    </td>

                                </tr>
                                <tr>
                                    <td style="padding-left: 50px" colspan="4">Date: &nbsp;  
                                                            <asp:Label runat="server" ID="lblDateForLeaving1"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="padding-left: 50px" colspan="4">Place: &nbsp;  
                                                            <%# Eval("SchoolDistict") %> 
                                    </td>

                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Checked By
                                    </td>
                                    <td style="text-align: right;">Class Teacher
                                    </td>
                                    <td style="text-align: right; padding-right: 100px">Clerk
                                    </td>
                                    <td style="text-align: right; padding-right: 100px">Head Master
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="font-size: 9px; padding-left: 50px">
                                        <strong>Note:</strong> No change in entry in this certificate 
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divLeavingGujPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlLeavingGujarati1" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingGujarati1_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 14px; vertical-align: top; height: 1040px; border-collapse: collapse; border: 1px solid black">
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <strong>
                                                        <asp:Label runat="server" ID="lblDuplicateGuj1"></asp:Label></strong><br />
                                                    <strong style="font-size: 17px"><%# Eval("TrustName") %></strong><br />
                                                    <strong>શાળા નો ઇન્ડેક્ષ નંબર.</strong>  <%# Eval("IndexNo") %><br />
                                                    <strong style="font-size: 22px">શાળા છોડ્યા નું પ્રમાણપત્ર</strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 30%; padding-left: 7px">વિદ્યાર્થી નો જનરલ રજીસ્ટર નંબર. <u><%# Eval("Grno") %></u></td>
                                    <td style="width: 25%; padding-left: 7px">&nbsp;</td>
                                    <td style="text-align: right; padding-left: 7px">અનુક્રમ નંબર. <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></td>
                                </tr>
                                <tr>
                                    <td style="width: 25%; padding-left: 7px" colspan="2">1. વિદ્યાર્થી નું પુરું નામ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("StudentName") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">2. માતા નું નામ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("MotherName") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">3. ધર્મ અને જાતિ (પેટા જ્ઞાતિ સાથે)
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("Caste") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">4. જન્મ સ્થળ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("BirthDistict") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">5. જન્મ તારીખ (આંકડામાં)
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("BirthDate") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 96px">(શબ્દોમાં)
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">&nbsp;
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" style="padding-left: 7px">6. છેલ્લી કઈ શાળામાં અભ્યાસ કર્યો
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("SchoolName") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">7. શાળામાં દાખલ થયાની તારીખ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("AdmissionDate") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">કઈ શ્રેણીમાં દાખલ થયા
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("AdmittedClass") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%; font-size: 14px;">
                                            <tr>
                                                <td style="width: 29%; padding-left: 6px">8 . પ્રગતિ
                                                </td>
                                                <td style="border-bottom: 1px solid black; width: 25%">&nbsp;
                                                </td>
                                                <td style="width: 10%">9. વર્તણુક
                                                </td>
                                                <td style="border-bottom: 1px solid black; width: 37%">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">10. શાળા છોડ્યા તારીખ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black">
                                        <%# Eval("LeftDate") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%; font-size: 14px;">
                                            <tr>
                                                <td style="width: 29%">11 . કઈ શ્રેણીમાં હતા?(આંકડામાં)
                                                </td>
                                                <td style="border-bottom: 1px solid black; width: 25%; padding-left: 6px"><%# Eval("LeftStd") %> 
                                                </td>
                                                <td style="width: 8%; padding-left: 7px">(શબ્દોમાં)
                                                </td>
                                                <td style="border-bottom: 1px solid black; width: 38%">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%; font-size: 14px;">
                                            <tr>
                                                <td style="width: 29%; padding-left: 5px">ક્યારથી (આંકડામાં)
                                                </td>
                                                <td style="border-bottom: 1px solid black; width: 25%; padding-left: 6px"><%# Eval("AdmittedClass") %>
                                                </td>
                                                <td style="width: 8%; padding-left: 7px">(શબ્દોમાં)
                                                </td>
                                                <td style="border-bottom: 1px solid black; width: 38%; padding-left: 7px">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">12. શાળા છોડ્યા નું કારણ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <%# Eval("LeftReason") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">13. નોંધ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">14. હાજર દિવસ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 7px">15. ફી સ્થિતિ
                                    </td>
                                    <td colspan="2" style="border-bottom: 1px solid black; padding-left: 7px">
                                        <asp:Label runat="server" ID="lblFeesStatusGuj1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 50px" colspan="4">આથી પ્રમાણપત્ર આપવામાં આવે છે કે ઉપર્યુક્ત માહિતી શાળાના જનરલ રજીસ્ટર પ્રમાણે બરાબર છે.
                                    </td>

                                </tr>
                                <tr>
                                    <td style="padding-left: 50px" colspan="4">તારીખ. &nbsp;  
                                                            <asp:Label runat="server" ID="lblDateForLeavingGuj1"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="padding-left: 50px" colspan="4">સ્થળ. &nbsp;  
                                                            <%# Eval("SchoolDistict") %> 
                                    </td>

                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">ચકાસણી :
                                    </td>
                                    <td style="text-align: right;">વર્ગ શિક્ષક
                                    </td>
                                    <td style="text-align: right;">અન્વેષક 
                                    </td>
                                    <td style="text-align: right; padding-right: 150px">આચાર્ય
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="font-size: 9px; padding-left: 50px" align="center">
                                        <strong>નોંધ:</strong> આ પ્રમાણપત્ર આપનાર અધિકારી સિવાય કોઈથી આમાં ફેરફાર થઈ શકશે નહિ. ફેરફાર કરનાર સામે યોગ્ય પગલા લેવામાં આવશે.<br />
                                        ગ્રાન્ટ ઇન ઍઇડના પ્રકરણ 11 ના વિભાગ 2 ના નિયમ 14 અને 30 પ્રમાણે
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divTransferGujaratiPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlTransferGujarati1" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlTransferGujarati1_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 16px; vertical-align: top; height: 1040px; border-collapse: collapse; border: 1px solid black">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <h3><%# Eval("TrustName") %></h3>
                                                    <strong style="font-size: 22px">વિદ્યાર્થીનો ટ્રાન્સફર અહેવાલ</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">પ્રતિ, 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">આચાર્યશ્રી, <strong>
                                        <asp:Label runat="server" ID="lblPrincipal1"></asp:Label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">સવિનય જણાવવાનું કે આપની શાળામાં વર્ષ <strong><%# Eval("Year") %></strong> દરમ્યાન ધોરણ : <strong><%# Eval("Class") %></strong> માં અભ્યાસ કરતો વિદ્યાર્થી / કરતી વિદ્યાર્થીની ની નીચેની વિગત આપવા મહેરબાની કરશો.
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">વિદ્યાર્થી નું નામ : <strong><%# Eval("StudentName") %></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">શાળાનો ડાયસ કોડ : <strong>
                                        <asp:Label runat="server" ID="lblSchoolDiesCode1"></asp:Label></strong>

                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">કલસ્ટરનું નામ  : <strong>
                                        <asp:Label runat="server" ID="lblKalstar1"></asp:Label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">વિદ્યાર્થી નો  અનન્ય નંબર  : <strong><%# Eval("GVUniqueID") %></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">વિદ્યાર્થી નો  જી.આર . નંબર  : <strong><%# Eval("Grno") %></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%;">તારીખ :<strong>
                                                    <asp:Label runat="server" ID="lblDateGujTransfer1"></asp:Label></strong>
                                                </td>

                                                <td style="text-align: right; padding-right: 90px">આચાર્યશ્રીની સહી.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divAttemptEngPrint" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlAttemptEnglish1" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingEnglish_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black;">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <strong style="font-size: 22px">ATTEMPT CERTIFICATE</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">G.R.No : &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("GRNO") %>></strong></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">Name :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("StudentName") %></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">Standard :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Standard") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong> Division :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Division") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  Percent :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Percent") %> </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">Year :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Year") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; Month : &nbsp;&nbsp;&nbsp;&nbsp;  <strong><%# Eval("Month") %></strong> &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; Attempt :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Attempt") %> </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">Seat :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("SeatNo") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong>Gujarat Boards.</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>

                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divAttemptGujPrint" runat="server" style="width: 100%; padding: 0 10px 0 10px; display: none;">
                    <asp:DataList ID="dlAttemptGujarati1" Width="100%" runat="server" align="center" Style="font-family: Verdana; font-size: 8px; border: solid 2px black; padding-left: 2px" OnItemDataBound="dlLeavingGujarati_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; font-size: 16px; vertical-align: top; height: 100%; border-collapse: collapse; border: 1px solid black;">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-bottom: 1px solid Black">
                                            <tr>
                                                <td style="width: 10%;" align="center">
                                                    <img src="../Images/NAVCHETAN LOGO COLOUR copy.jpg" width="100" height="100" />
                                                </td>

                                                <td style="width: 85%; padding-left: 5px;" align="center">
                                                    <strong style="font-size: 22px">ATTEMPT CERTIFICATE</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">જી.આર.નંબર : &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("GRNO") %>></strong></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">નામ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("StudentName") %></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">ધોરણ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Standard") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong> વિભાગ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Division") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  Percent :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Percent") %> </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">વર્ષ :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Year") %> </strong>&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; મહિનો : &nbsp;&nbsp;&nbsp;&nbsp;  <strong><%# Eval("Month") %></strong> &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp; Attempt :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("Attempt") %> </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px">બેઠક નંબર :  &nbsp;&nbsp;&nbsp;&nbsp; <strong><%# Eval("SeatNo") %>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</strong>ગુજરાત બોર્ડ.</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 120px"></td>
                                </tr>

                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>


            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtStudentName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkEngish" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $(".autosuggest").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "StudentDEOReport.aspx/GetAllStudentNameForReport",
                    data: "{'prefixText':'" + request.term + "','TrustMID':'" + $(document.getElementById('<%= hfTrustMID.ClientID %>')).val() + "','SchoolMID':'" + $(document.getElementById('<%= hfSchoolMID.ClientID %>')).val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('~')[0],
                                val: item.split('~')[1],
                            };
                        }));
                    },
                    error: function () {
                        alert("Error");
                    }
                });
            },
            select: function (e, i) {

                if (parseInt(i.item.val) > 0 || i.item.val != null || i.item.val != '') {
                    $("#<%=hfStudentMID.ClientID %>").val(i.item.val);
		        }
		        else {
		            $("#<%=hfStudentMID.ClientID %>").val(0);
		        }
		    }
        });
    </script>
</asp:Content>
