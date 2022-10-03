<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentAttendence.aspx.cs" Inherits="GEIMS.Client.UI.StudentAttendence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />

    <script type="text/javascript">

        function BindCheckBOX() {

            $("[id$=chkChild]").click(function () {
                if ($('[id$=chkChild]').length == $('[id$=chkChild]:checked').length) {
                    if ($(this).is(":checked")) {
                       // alert("chkChild");
                        $(this).closest("tr").find($("[id*=chkSms]")).prop("checked", false);
                        $(this).closest("tr").find($("[id*=chkSms]")).prop('disabled', false);
                    }
                    else {
                        $(this).closest("tr").find($("[id*=chkSms]")).prop("checked", true);
                        $(this).closest("tr").find($("[id*=chkSms]")).prop('disabled', true);
                    }
                    $('[id$=chkHeader]').attr("checked", "checked");
                }
                else {
                    $('[id$=chkHeader]').removeAttr("checked");
                }
            });
        }


        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        function CheckAll() {
            $('[id*=chkChild]').attr("checked", "checked");
            ($("[id*=chkSms]")).prop('disabled', true);
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
            Student Attendance
           
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">

            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divEmployee" runat="server">
                    <div id="tabs" runat="server">
                        <div id="tab-panel" class="style-tabs" visible="true">
                            <ul>
                                <li><a id="tabClassDetails" href="#tabs-1">Student Attendance</a></li>
                            </ul>
                            <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <asp:HiddenField ID="hfCLassMID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hfDivisionTID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hfEmployeeID" runat="server" ClientIDMode="Static" />


                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 19%; float: left;" class="label">
                                            Date :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 81%; float: left;">
                                            <asp:TextBox ID="txtdate" runat="server" Width="150px" CssClass="validate[required] TextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="Calendar1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 19%; float: left;" class="label">
                                            Class :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 81%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlClass" Width="150px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 19%; float: left;" class="label">
                                            Division :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 81%; float: left; vertical-align: top;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDivision" Width="150px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 19%; float: left;" class="label">
                                            Academic Year :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 81%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Enabled="false" Width="150px">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div id="divLoading" align="center" class="divclasswithfloat">
                                           Loading. Please wait.<br />
                                        <br />
                                        <img src="../Images/loading.gif" alt="" />
                                    </div>


                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <asp:Button runat="server" ID="btnViewGrid" Text="View" CssClass="btn-blue btn-blue-medium" OnClick="btnViewGrid_Click" />
                                    </div>

                                    <div class="divclasswithfloat">
                                        <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="StudentMID" HeaderText="Student ID">
                                                    <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Attendance">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkChild" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentName" Text='<%#Eval("StudentFirstNameEng") + " " + Eval("StudentLastNameEng") %> ' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CurrentGrNo" HeaderText="Gr No">
                                                    <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Send SMS">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSms" runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                            <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                            <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        <div style="width: 100%; text-align: right;" class="divclasswithfloat">
                                            <asp:Button runat="server" ID="btnSMS" Text="Send SMS" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSMS_Click" />
                                        </div>
                                    </div>
                                    <div style="width: 100%; text-align: right;" class="divclasswithOutfloat">

                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />

                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right;"></div>
        </div>
    </div>

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        function BindDorpdownOnButtonClick() {
            //alert("BindDorpdownOnButtonClick");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentAttendence.aspx/LoadClass",
                data: "{'intEmployeeMID':'" +<%=Session["UserID"] %> + "'}",
                dataType: "json",
                success: function (data) {

                    $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
                    // alert("Suceess");
                    var temp = $.parseJSON(data.d);
                    var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                    $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);
                    $("#divLoading").show();
                    $.each(temp, function (i) {

                        var optionhtml = '<option value="' +
                         temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
                        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

                    });
                    $("#divLoading").hide();
                    $(document.getElementById('<%= ddlClass.ClientID %>')).val($(document.getElementById('<%= hfCLassMID.ClientID %>')).val());

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "StudentAttendence.aspx/LoadDivision",
                        data: "{'intClassMID':'" + $(document.getElementById('<%= hfCLassMID.ClientID %>')).val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                        dataType: "json",
                        success: function (data) {
                            $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
                            var temp = $.parseJSON(data.d);
                            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
                            $("#divLoading").show();
                            $.each(temp, function (i) {

                                var optionhtml = '<option value="' +
                                 temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                                $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);
                            });
                            $("#divLoading").hide();
                            $(document.getElementById('<%= ddlDivision.ClientID %>')).val($(document.getElementById('<%= hfDivisionTID.ClientID %>')).val());
                        },
                    });
                },
            });

        }
        $("[id*=chkChild]").click(function () {
            //alert("chkChild");
            if ($(this).is(":checked")) {
                $(this).closest("tr").css("background-color", "White");
                $(this).closest("tr").find($("[id*=chkSms]")).prop("checked", false);
                $(this).closest("tr").find($("[id*=chkSms]")).prop('disabled', true);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                // alert("unchkChild");
                $(this).closest("tr").css("background-color", "#F799A3");
                $(this).closest("tr").find($("[id*=chkSms]")).prop("checked", true);
                $(this).closest("tr").find($("[id*=chkSms]")).prop('disabled', false);
              
            }
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });


        $(document.getElementById('<%= ddlClass.ClientID %>')).change(function () {
            $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UpgradeStudent.aspx/LoadDivision",
                data: "{'intClassMID':'" + $('#<%= ddlClass.ClientID %> option:selected').val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                dataType: "json",
                success: function (data) {
                    var count = -1;
                    var temp = $.parseJSON(data.d);
                    $("#divLoading").show();
                    $.each(temp, function (i) {
                        count = i;
                        var optionhtml = '<option value="' +
                         temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                        $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);
                    });
                    $("#divLoading").hide();
                    if (count == "-1") {
                        alert("Division is not Created");
                    }
                },
                error: function (error) {
                }

            });
        });

        function BindClass() {
            $(document.getElementById('<%= ddlClass.ClientID %>')).empty();

            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);
            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
            // alert(<%=Session["UserID"] %>);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentAttendence.aspx/LoadClass",
                data: "{'intEmployeeMID':'" + <%=Session["UserID"] %> + "' }",
                dataType: "json",
                success: function (data) {
                    var temp = $.parseJSON(data.d);
                    $("#divLoading").show();
                    $.each(temp, function (i) {
                        count = i;
                        var optionhtml = '<option value="' +
                         temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
                        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

                    });
                    $("#divLoading").hide();
                    if (count == "-1") {
                        alert("Class is not Created");
                    }
                },
                error: function (error) {
                    //  alert("Error" + error);
                }

            });
        }

        $(document.getElementById('<%= btnViewGrid.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });


    </script>

</asp:Content>
