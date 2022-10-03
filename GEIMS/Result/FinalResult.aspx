<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="FinalResult.aspx.cs" Inherits="GEIMS.Result.FinalResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Final Result Master
          <%--  <asp:LinkButton CausesValidation="false" ID="lnkAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Final Result Details</a></li>

                    </ul>
                    <div id="tabs-1" style="height: 500px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">

                        <div style="height: 30px; margin-top: 10px; margin-left: 290px; float: left; width: 100%;">
                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                Class :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 81%; float: left;">
                                <asp:DropDownList runat="server" ID="ddlClass" CssClass="Droptextarea">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                     <asp:ListItem>11</asp:ListItem>
                                     <asp:ListItem>12</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div style="height: 30px; margin-top: 10px; margin-left: 290px; float: left; width: 100%;">
                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                Division:<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 81%; float: right; vertical-align: top;">
                                <asp:DropDownList runat="server" ID="ddlDivision" CssClass="Droptextarea">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; margin-left: 290px; float: left; width: 100%;">
                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                Academic Year :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 81%; float: left;">
                                <asp:DropDownList runat="server" ID="DropDownList1" CssClass="Droptextarea">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>20-21</asp:ListItem>
                                    <asp:ListItem>21-22</asp:ListItem>
                                    <asp:ListItem>22-23</asp:ListItem>
                                    <asp:ListItem>23-24</asp:ListItem>
                                    <asp:ListItem>24-25</asp:ListItem>
                                    <asp:ListItem>25-26</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; margin-left: 290px; float: left; width: 100%;">
                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                Subject :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 81%; float: left;">
                                <asp:DropDownList runat="server" ID="DropDownList2" CssClass="Droptextarea">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Hindi</asp:ListItem>
                                    <asp:ListItem>Maths</asp:ListItem>
                                    <asp:ListItem>Gujarati</asp:ListItem>
                                    <asp:ListItem>English</asp:ListItem>
                                    <asp:ListItem>Physics</asp:ListItem>
                                    <asp:ListItem>History</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div style="height: 30px; margin-top: 10px; margin-left: 290px; float: left; width: 100%;">
                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                Exam :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 81%; float: left;">
                                <asp:DropDownList runat="server" ID="DropDownList8" CssClass="Droptextarea">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>1st Annual Exam</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                            <asp:Button runat="server" ID="Button2" Text="View" CssClass="btn-blue btn-blue-medium" OnClick="Button2_Click" />
                        </div>
                        <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                            <table style="width: 100%; text-align: center" id="tblStudent" runat="server">
                                <tr>
                                    <td align="center">
                                        <table id="Table1" style="width: 100%; text-align: center" runat="server">
                                            <tr>
                                                <td style="width: 45%; color: white; background-color: #3B5998" rowspan="2">StudentName
                                                </td>
                                                <td colspan="3" style="width: 15%; color: white; background-color: #3B5998">Maths</td>
                                                <td colspan="3" style="width: 15%; color: white; background-color: #3B5998">Hindi</td>
                                                <td colspan="3" style="width: 15%; color: white; background-color: #3B5998">English</td>
                                                <td style="width: 10%; color: white; background-color: #3B5998" rowspan="2">Grade</td>
                                            </tr>
                                            <tr>
                                                <td style="color: white; background-color: #3B5998">Total</td>
                                                <td style="color: white; background-color: #3B5998">Passing</td>
                                                <td style="color: white; background-color: #3B5998">Availed</td>
                                                <td style="color: white; background-color: #3B5998">Total</td>
                                                <td style="color: white; background-color: #3B5998">Passing</td>
                                                <td style="color: white; background-color: #3B5998">Availed</td>
                                                <td style="color: white; background-color: #3B5998">Total</td>
                                                <td style="color: white; background-color: #3B5998">Passing</td>
                                                <td style="color: white; background-color: #3B5998">Availed</td>
                                            </tr>
                                            <tr>
                                                <td>Janki P Sharma
                                                </td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>70</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>50</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>80</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlgrade">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Nafisa M Mulla
                                                </td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>50</td>
                                                <td>100</td>
                                                <td>47</td>
                                                <td>50</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>67</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="DropDownList3">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Piyali K Kalyanji
                                                </td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>66</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>78</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>70</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="DropDownList4">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Nakul PiyushBhai Mehta
                                                </td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>71</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>79</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>81</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="DropDownList5">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Keval L Kapdiya
                                                </td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>76</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>55</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>81</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="DropDownList6">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Rupa U Tejavani
                                                </td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>74</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>87</td>
                                                <td>100</td>
                                                <td>40</td>
                                                <td>78</td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="DropDownList7">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>B</asp:ListItem>
                                                        <asp:ListItem>C</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divSave" runat="server" style="height: 30px; margin-top: 180px; float: left; width: 100%;">
                            <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancel_Click"  />--%>&nbsp;&nbsp;
							  <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium" OnClick="btnSave_Click" />
                            &nbsp;&nbsp;&nbsp;
                                                  

                        </div>

                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
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
    </script>
</asp:Content>
