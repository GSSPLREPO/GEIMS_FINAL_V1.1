<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="TimeTableClassWise.aspx.cs" Inherits="GEIMS.ReportUI.TimeTableClassWise" %>
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
                 OnClick="btnBack_Click" />  &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                 OnClick="btnReport_Click" />
            <%--<asp:LinkButton CausesValidation="false" ID="lnkEdit" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkEdit_OnClick">Edit</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <fieldset>
                    <legend>Period Master</legend>
                    <div style="width: 100%; height: 30px;">
                        <div style="text-align: left; width: 20%; float: left;" class="label">
                            Class :<span style="color: red">*</span>
                        </div>
                        <div style="text-align: left; width: 80%; float: left;">
                            <asp:DropDownList runat="server" ID="ddlClass" CssClass="Droptextarea" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_OnSelectedIndexChanged" />
                            <%--<asp:HiddenField runat="server" ID="hfNoOfPeriod" />--%>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div style="width: 100%; height: 30px;">
                        <div style="text-align: left; width: 20%; float: left;" class="label">
                            Division :<span style="color: red">*</span>
                        </div>
                        <div style="text-align: left; width: 80%; float: left;">
                            <div style="float: left; text-align: left;">
                                <asp:DropDownList runat="server" ID="ddlDivision" CssClass="Droptextarea" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                            </div>
                            <div style="float: right; text-align: left;">
                                <%--<asp:Button ID="btnEdit" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkEdit_OnClick" Text="Add New" />--%>
                                &nbsp;
                                <%--<asp:Button ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkViewList_OnClick" Text="GO" />--%>
                            </div>
                        </div>
                    </div>
                </fieldset>
                <div class="clear"></div>
                 <div id="divTimeTable" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                <div id="DisplayPrint" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <table id="timetable" rules="cols" style="border: thin solid #000000; width: 100%;">
                        <tr id="tr00" style="border-bottom:1px solid Black">
                            <th rowspan="2">Period</th>
                            <th colspan="2">    Monday</th>
                            <th colspan="2">Tuesday
                            </th>
                            <th colspan="2">Wednesday</th>
                            <th colspan="2">Thursday</th>
                            <th colspan="2">Friday</th>
                            <th colspan="2">Saturday</th>
                            <th colspan="2">Sunday</th>
                        </tr>
                        <tr style="border-bottom:1px solid Black">
                            <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                             <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                             <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                             <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                             <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                             <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                             <th>
                                Time
                            </th>
                            <th>
                                Subject
                            </th>
                        </tr>
                        <tr id="tr11">
                            <td rowspan="2">1</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom11"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub11"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher11"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom12"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub12"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher12"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom13"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub13"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher13"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom14"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub14"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher14"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom15"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub15"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher15"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom16"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub16"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher16"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom17"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub17"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher17"></asp:Label></td>
                        </tr>
                        <tr id="tr12">
                            <td>
                                <asp:Label runat="server" ID="lblTo11"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo12"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo13"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo14"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo15"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo16"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo17"></asp:Label></td>
                        </tr>
                        <tr id="tr21">
                            <td rowspan="2">2</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom21"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub21"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher21"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom22"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub22"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher22"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom23"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub23"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher23"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom24"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub24"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher24"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom25"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub25"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher25"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom26"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub26"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher26"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom27"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub27"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher27"></asp:Label></td>
                        </tr>
                        <tr id="tr22">
                            <td>
                                <asp:Label runat="server" ID="lblTo21"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo22"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo23"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo24"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo25"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo26"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo27"></asp:Label></td>
                        </tr>
                        <tr id="tr31">
                            <td rowspan="2">3</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom31"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub31"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher31"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom32"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub32"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher32"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom33"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub33"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher33"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom34"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub34"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher34"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom35"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub35"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher35"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom36"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub36"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher36"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom37"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub37"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher37"></asp:Label></td>
                        </tr>
                        <tr id="tr32">
                            <td>
                                <asp:Label runat="server" ID="lblTo31"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo32"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo33"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo34"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo35"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo36"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo37"></asp:Label></td>
                        </tr>
                        <tr id="tr41">
                            <td rowspan="2">4</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom41"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub41"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher41"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom42"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub42"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher42"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom43"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub43"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher43"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom44"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub44"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher44"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom45"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub45"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher45"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom46"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub46"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher46"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom47"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub47"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher47"></asp:Label></td>
                        </tr>
                        <tr id="tr42">
                            <td>
                                <asp:Label runat="server" ID="lblTo41"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo42"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo43"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo44"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo45"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo46"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo47"></asp:Label></td>
                        </tr>
                        <tr id="tr51">
                            <td rowspan="2">5</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom51"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub51"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher51"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom52"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub52"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher52"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom53"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub53"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher53"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom54"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub54"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher54"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom55"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub55"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher55"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom56"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub56"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher56"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom57"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub57"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher57"></asp:Label></td>
                        </tr>
                        <tr id="tr52">
                            <td>
                                <asp:Label runat="server" ID="lblTo51"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo52"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo53"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo54"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo55"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo56"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo57"></asp:Label></td>
                        </tr>
                        <tr id="tr61">
                            <td rowspan="2">6</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom61"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub61"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher61"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom62"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub62"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher62"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom63"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub63"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher63"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom64"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub64"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher64"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom65"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub65"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher65"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom66"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub66"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher66"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom67"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub67"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher67"></asp:Label></td>
                        </tr>
                        <tr id="tr62">
                            <td>
                                <asp:Label runat="server" ID="lblTo61"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo62"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo63"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo64"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo65"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo66"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo67"></asp:Label></td>
                        </tr>
                        <tr id="tr71">
                            <td rowspan="2">7</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom71"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub71"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher71"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom72"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub72"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher72"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom73"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub73"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher73"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom74"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub74"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher74"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom75"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub75"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher75"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom76"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub76"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher76"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom77"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub77"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher77"></asp:Label></td>
                        </tr>
                        <tr id="tr72">
                            <td>
                                <asp:Label runat="server" ID="lblTo71"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo72"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo73"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo74"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo75"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo76"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo77"></asp:Label></td>
                        </tr>
                        <tr id="tr81">
                            <td rowspan="2">8</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom81"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub81"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher81"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom82"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub82"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher82"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom83"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub83"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher83"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom84"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub84"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher84"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom85"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub85"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher85"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom86"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub86"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher86"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom87"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub87"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher87"></asp:Label></td>
                        </tr>
                        <tr id="tr82">
                            <td>
                                <asp:Label runat="server" ID="lblTo81"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo82"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo83"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo84"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo85"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo86"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo87"></asp:Label></td>
                        </tr>
                        <tr id="tr91">
                            <td rowspan="2">9</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom91"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub91"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher91"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom92"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub92"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher92"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom93"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub93"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher93"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom94"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub94"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher94"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom95"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub95"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher95"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom96"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub96"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher96"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom97"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub97"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher97"></asp:Label></td>
                        </tr>
                        <tr id="tr92">
                            <td>
                                <asp:Label runat="server" ID="lblTo91"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo92"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo93"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo94"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo95"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo96"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo97"></asp:Label></td>
                        </tr>
                        <tr id="tr101">
                            <td rowspan="2">10</td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom101"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub101"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher101"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom102"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub102"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher102"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom103"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub103"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher103"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom104"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub104"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher104"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom105"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub105"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher105"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom106"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub106"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher106"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblFrom107"></asp:Label></td>
                            <td rowspan="2">
                                <asp:Label runat="server" ID="lblSub107"></asp:Label><br />
                                <asp:Label runat="server" ID="lblTeacher107"></asp:Label></td>
                        </tr>
                        <tr id="tr102">
                            <td>
                                <asp:Label runat="server" ID="lblTo101"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo102"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo103"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo104"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo105"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo106"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblTo107"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                     </div>
               
                <div id="divContent3" style="width: 10%; float: right;"></div>
            </div>
        </div>
    </div>
      <script>
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

