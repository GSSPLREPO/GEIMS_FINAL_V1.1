<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="TimeTableTeacherWise.aspx.cs" Inherits="GEIMS.ReportUI.TimeTableTeacherWise" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Time Table
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
            &nbsp;
             <asp:Button ID="btnBack1" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />
            <%--<asp:LinkButton CausesValidation="false" ID="lnkEdit" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkEdit_OnClick">Edit</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <fieldset>
                    <legend>Period Master</legend>


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
                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium" OnClick="btnGo_Click" />
                            </div>
                        </div>
                    </div>
                </fieldset>
                <div class="clear"></div>
                <div id="divTimeTable" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <div id="DisplayPrint" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                        <table id="timetable" rules="cols" style="border: thin solid #000000; width: 100%;">
                            <tr id="tr00" style="border-bottom: 1px solid Black">
                                <th>Period</th>
                                <th>Monday</th>
                                <th>Tuesday
                                </th>
                                <th>Wednesday</th>
                                <th>Thursday</th>
                                <th>Friday</th>
                                <th>Saturday</th>
                               
                            </tr>
                            <tr id="tr11">
                                <td>1</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom11"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo11"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass11"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision11"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject11"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom12"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo12"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass12"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision12"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject12"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom13"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo13"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass13"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision13"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject13"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom14"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo14"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass14"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision14"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject14"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom15"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo15"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass15"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision15"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject15"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom16"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo16"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass16"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision16"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject16"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr id="tr21">
                                <td>2</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom21"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo21"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass21"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision21"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject21"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom22"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo22"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass22"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision22"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject22"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom23"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo23"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass23"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision23"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject23"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom24"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo24"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass24"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision24"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject24"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom25"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo25"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass25"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision25"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject25"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom26"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo26"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass26"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision26"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject26"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr id="tr31">
                                <td>3</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom31"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo31"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass31"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision31"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject31"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom32"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo32"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass32"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision32"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject32"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom33"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo33"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass33"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision33"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject33"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom34"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo34"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass34"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision34"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject34"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom35"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo35"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass35"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision35"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject35"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom36"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo36"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass36"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision36"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject36"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr id="tr41">
                                <td>4</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom41"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo41"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass41"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision41"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject41"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom42"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo42"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass42"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision42"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject42"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom43"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo43"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass43"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision43"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject43"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom44"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo44"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass44"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision44"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject44"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom45"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo45"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass45"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision45"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject45"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom46"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo46"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass46"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision46"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject46"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr id="tr51">
                                <td>5</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom51"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo51"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass51"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision51"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject51"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom52"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo52"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass52"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision52"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject52"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom53"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo53"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass53"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision53"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject53"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom54"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo54"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass54"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision54"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject54"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom55"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo55"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass55"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision55"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject55"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom56"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo56"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass56"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision56"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject56"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr id="tr61">
                                <td>6</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom61"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo61"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass61"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision61"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject61"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom62"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo62"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass62"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision62"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject62"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom63"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo63"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass63"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision63"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject63"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom64"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo64"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass64"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision64"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject64"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom65"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo65"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass65"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision65"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject65"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom66"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo66"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass66"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision66"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject66"></asp:Label>
                                </td>
                               
                            </tr>
                             <tr id="tr71">
                                <td>7</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom71"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo71"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass71"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision71"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject71"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom72"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo72"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass72"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision72"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject72"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom73"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo73"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass73"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision73"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject73"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom74"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo74"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass74"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision74"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject74"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom75"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo75"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass75"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision75"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject75"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom76"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo76"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass76"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision76"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject76"></asp:Label>
                                </td>
                               
                            </tr>
                               <tr id="tr81">
                                <td>8</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom81"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo81"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass81"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision81"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject81"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom82"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo82"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass82"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision82"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject82"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom83"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo83"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass83"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision83"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject83"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom84"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo84"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass84"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision84"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject84"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom85"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo85"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass85"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision85"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject85"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom86"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo86"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass86"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision86"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject86"></asp:Label>
                                </td>
                               
                            </tr>
                               <tr id="tr91">
                                <td>9</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom91"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo91"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass91"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision91"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject91"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom92"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo92"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass92"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision92"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject92"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom93"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo93"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass93"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision93"></asp:Label>
                                    <asp:Label runat="server" ID="lblSubject93"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom94"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo94"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass94"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision94"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject94"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom95"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo95"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass95"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision95"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject95"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom96"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo96"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass96"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision96"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject96"></asp:Label>
                                </td>
                               
                            </tr>
                               <tr id="tr101">
                                <td>10</td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom101"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo101"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass101"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision101"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject101"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom102"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo102"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass102"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision102"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject102"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom103"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo103"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass103"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision103"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject103"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom104"></asp:Label>
                                   -
                                    <asp:Label runat="server" ID="lblTo104"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass104"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision104"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject104"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom105"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo105"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass105"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision105"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject105"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblFrom106"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblTo106"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblClass106"></asp:Label>
                                    -
                                    <asp:Label runat="server" ID="lblDivision106"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSubject106"></asp:Label>
                                </td>
                               
                            </tr>
                        </table>

                                    <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%">
                                    <RowStyle BackColor="White" />
                                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                                        BorderWidth="1px" BorderStyle="Solid" />
                                </asp:GridView>

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
        function BindTimeTable(i) {
            hideTimeTable();
            $("#timetable").show();
            $("#tr00").show();
            for (var j = 0; j <= i; j++) {
                var k = 'tr' + j + "1";
                $("#" + k + "").show();
                var l = 'tr' + j + "2";
                $("#" + l + "").show();
            }
        }
        hideTimeTable();
        function hideTimeTable() {
            $("#timetable").hide();
            $("#tr00").hide();
            for (var l = 0; l <= 10; l++) {
                var m = 'tr' + l + "1";
                $("#" + m + "").hide();
                var n = 'tr' + l + "1";
                $("#" + n + "").hide();
            }
        }
    </script>
</asp:Content>
