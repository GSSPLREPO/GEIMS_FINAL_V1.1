<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="PeriodMaster.aspx.cs" Inherits="GEIMS.Client.UI.PeriodMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $('#tab-panel').tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Period Master
            <%--<asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick" ValidationGroup="g1">Add New</asp:LinkButton>
            <asp:ValidationSummary runat="server" ID="vs1" ShowMessageBox="True" ValidationGroup="g1" ShowSummary="False" />
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <fieldset>
                    <legend>Period Master</legend>
                    <div style="text-align: left; width: 20%; float: left;" class="label">
                        Class :<span style="color: red">*</span>
                    </div>
                    <div style="text-align: left; width: 80%; float: right;">
                        <div style="float: left; text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlClass" CssClass="Droptextarea" OnSelectedIndexChanged="ddlClass_OnSelectedIndexChanged" AutoPostBack="True" />
                        </div>
                        <div style="float: right; text-align: left;">
                            <asp:Button ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkAddNewClass_OnClick" Text="Add New" />
                            &nbsp;
                            <asp:Button ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkViewList_OnClick" Text="View List" />
                        </div>
                    </div>
                </fieldset>
                <br />
                <br />
                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                    <asp:GridView ID="gvPeriod" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvPeriod_OnRowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="DayName" HeaderText="Day">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NoOfPeriod" HeaderText="No Of Period">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("DayNo")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("DayNo")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Period Details</a></li>
                        </ul>
                        <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;" class="mydiv">
                                <div style="width: 100%; padding:10px 0 0 0;">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Day :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 80%; float: left;">
                                        <asp:DropDownList runat="server" ID="ddlDay" CssClass="validate[required] Droptextarea">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hfNoOfPeriod" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div style="width: 100%;">
                                    <table style="width: 60%;">
                                        <tr id="tr0">
                                            <th style="width: 20%;">No</th>
                                            <th style="width: 40%;">From Time</th>
                                            <th style="width: 40%;">To Time</th>
                                        </tr>
                                        <tr id="tr1">
                                            <td style="width: 20%">1</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH1" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','1')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM1" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','1')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH1" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH2','1')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM1" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM2','1')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr2">
                                            <td style="width: 20%">2</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH2" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','2')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM2" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','2')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH2" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH3','2')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM2" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM3','2')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr3">
                                            <td style="width: 20%">3</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH3" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','3')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM3" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','3')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH3" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH4','3')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM3" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM4','3')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr4">
                                            <td style="width: 20%">4</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH4" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','4')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM4" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','4')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH4" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH5','4')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM4" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM5','4')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr5">
                                            <td style="width: 20%">5</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH5" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','5')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM5" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','5')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH5" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH6','5')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM5" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM6','5')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr6">
                                            <td style="width: 20%">6</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH6" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','6')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM6" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','6')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH6" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH7','6')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM6" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM7','6')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr7">
                                            <td style="width: 20%">7</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH7" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','7')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM7" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','7')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH7" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH8','7')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM7" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM8','7')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr8">
                                            <td style="width: 20%">8</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH8" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','8')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM8" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','8')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH8" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH9','8')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM8" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM9','8')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr9">
                                            <td style="width: 20%">9</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH9" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','9')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM9" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','9')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH9" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromHH10','9')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM9" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'txtFromMM10','9')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr10">
                                            <td style="width: 20%">10</td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtFromHH10" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','10')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtFromMM10" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','10')"></asp:TextBox>
                                            </td>
                                            <td style="width: 40%">
                                                <asp:TextBox runat="server" ID="txtToHH10" Width="50px" placeholder="HH" CssClass="TextBox hour" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','10')"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtToMM10" Width="50px" placeholder="MM" CssClass="TextBox minute" MaxLength="2" onkeypress="return PreventDecimalPoint(event)" onchange="timechange($(this).val(),'','10')"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 100%; text-align: right;">
                                    <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_OnClick" />
                                </div>
                            </div>
                        </div>
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
        $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
    <script>

        function BindPeriod(i) {
            hide();
            for (var j = 0; j <= i; j++) {
                var k = 'tr' + j;
                $("#" + k + "").show();
            }
        }
        hide();
        $(document).ready(function () {

        });

        function hide() {
            for (var l = 0; l <= 10; l++) {
                var m = 'tr' + l;
                $("#" + m + "").hide();
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            $('.hour').change(function () {
                var i = parseInt($(this).val());
                if (i > 23) {
                    var j = $(this).val();
                    $(this).val(j.slice(0, -1));
                    alert('Enter Valid Time');
                }
            });
            $('.minute').change(function () {
                var i = parseInt($(this).val());
                if (i > 59) {
                    var j = $(this).val();
                    $(this).val(j.slice(0, -1));
                    alert('Enter Valid Time');
                }
            });
        });
        function timechange(value, controlid) {
            $("input[ID$='" + controlid + "']").val(value);
            validatetime($("#<%=hfNoOfPeriod.ClientID%>").val());
        }

        function validatetime(count) {
            for (var i = 1; i <= count; i++) {
                var flag = 0;
                var control1 = 'txtFromHH' + i;
                var control2 = 'txtFromMM' + i;
                var control3 = 'txtToHH' + i;
                var control4 = 'txtToMM' + i;
                if ($("input[ID$='" + control1 + "']").val() == '') {
                    flag = 1;
                }
                else if ($("input[ID$='" + control2 + "']").val() == '') {
                    flag = 1;
                }
                else if ($("input[ID$='" + control3 + "']").val() == '') {
                    flag = 1;
                }
                else if ($("input[ID$='" + control4 + "']").val() == '') {
                    flag = 1;
                }
                if (flag == 0) {
                    var from = $("input[ID$='" + control1 + "']").val() + ':' + $("input[ID$='" + control2 + "']").val();
                    var to = $("input[ID$='" + control3 + "']").val() + ':' + $("input[ID$='" + control4 + "']").val();

                    var fromtime = "04/03/2014 " + from.toString();
                    var totime = "04/03/2014 " + to.toString();
                    fromtime = new Date(fromtime);
                    totime = new Date(totime);
                    if (fromtime > totime) {
                        alert('Enter valid time for period.');
                        $("input[ID$='" + control3 + "']").val('');
                        $("input[ID$='" + control4 + "']").val('');
                    }
                }
            }
        }

        $("#<%= btnAddNew.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").addClass("validate[required]");
            $("#<%=ddlDay.ClientID%>").removeClass("validate[required]");
        });

        $("#<%= btnViewList.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").addClass("validate[required]");
            $("#<%=ddlDay.ClientID%>").removeClass("validate[required]");
        });

        $("#<%= btnSaveClass.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").addClass("validate[required]");
            $("#<%=ddlDay.ClientID%>").addClass("validate[required]");
        });
    </script>
</asp:Content>
