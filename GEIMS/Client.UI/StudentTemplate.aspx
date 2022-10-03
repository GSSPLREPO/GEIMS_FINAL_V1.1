<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentTemplate.aspx.cs" Inherits="GEIMS.Client.UI.StudentTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />

    <script type="text/javascript">


        function BindDorpdownOnButtonClick() {
            $(document.getElementById('<%= ddlClass.ClientID %>')).empty();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Class_Template.aspx/LoadClass",
                data: "{'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                dataType: "json",
                success: function (data) {
                    // alert("Suceess");
                    var temp = $.parseJSON(data.d);
                    var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                    $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

                    $.each(temp, function (i) {
                        var optionhtml = '<option value="' +
                            temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
                        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

                    });
                    $(document.getElementById('<%= ddlClass.ClientID %>')).val($(document.getElementById('<%= hfCLassMID.ClientID %>')).val());
                    $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Class_Template.aspx/LoadDivision",
                        data: "{'intClassMID':'" + $(document.getElementById('<%= hfCLassMID.ClientID %>')).val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                        dataType: "json",
                        success: function (data) {
                            var temp = $.parseJSON(data.d);
                            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
                            $.each(temp, function (i) {

                                var optionhtml = '<option value="' +
                                    temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                                $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);
                            });
                            $(document.getElementById('<%= ddlDivision.ClientID %>')).val($(document.getElementById('<%= hfDivisionTID.ClientID %>')).val());
                            $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).empty();
                            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                            $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml1);
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "StudentTemplate.aspx/LoadFeesWithAmount",
                                data: "{'intClassMID':'" + $(document.getElementById('<%= hfCLassMID.ClientID %>')).val() + "'" + ",'intDivisionTID':'" + $(document.getElementById('<%= hfDivisionTID.ClientID %>')).val() + "'" + ",'strAcademicYear':'" + $(document.getElementById('<%= hfAcademicYear.ClientID %>')).val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                                dataType: "json",
                                success: function (data) {
                                    var temp = $.parseJSON(data.d);

                                    $.each(temp, function (i) {

                                        var optionhtml = '<option value="' +
                                         temp[i].FeesCategoryMID + '">' + temp[i].FeesNameWithAmount + '</option>';
                                        $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml);
                                    });
                                    $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).val($(document.getElementById('<%= hfFees.ClientID %>')).val());
                                },
                                error: function (error) {
                                    // alert("Error" + error);
                                }
                            });
                        },
                        error: function (error) {
                            // alert("Error" + error);
                        }
                    });
                },
                error: function (error) {
                    // alert("Error" + error);
                }
            });
        }
        function bindFees() {
            $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).empty();
             var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
             $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml1);
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "StudentTemplate.aspx/LoadFeesWithAmount",
                 data: "{'intClassMID':'" + $('#<%= ddlClass.ClientID %> option:selected').val() + "'" + ",'intDivisionTID':'" + $('#<%= ddlDivision.ClientID %> option:selected').val() + "'" + ",'strAcademicYear':'" + $('#<%= ddlAcademicYear.ClientID %> option:selected').text() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
        dataType: "json",
                 success: function (data) {
            //alert("{'intClassMID':'" + $('#<%= ddlClass.ClientID %> option:selected').val() + "'" + ",'intDivisionTID':'" + $('#<%= ddlDivision.ClientID %> option:selected').val() + "'" + ",'strAcademicYear':'" + $('#<%= ddlAcademicYear.ClientID %> option:selected').text() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}");
            var count = -1;
            var temp = $.parseJSON(data.d); 
            //alert(temp);
            $.each(temp, function (i) {               
                count = i;
                var optionhtml = '<option value="' +
                 temp[i].FeesCategoryMID + '">' + temp[i].FeesNameWithAmount + '</option>';
                $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml);
            });
                     alert(optionhtml);
                     if (count == "-1") {
                         alert("FeeCategory or ClassTemplate is not Created");
                     }
                 },
        error: function (error) {
            //alert("Error" + error);
        }

    });
         }

         function calendarShown(sender, args) {
             sender._popupBehavior._element.style.zIndex = 10005;
         }
         $(function () {

             $(document.getElementById('<%= tabs.ClientID %>')).tabs();
});

    function bindClass() {
        $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Class_Template.aspx/LoadClass",
            data: "{'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                    dataType: "json",
                    success: function (data) {
                        var temp = $.parseJSON(data.d);
                        //alert("Success");
                        var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

                        //var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);

                        $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml1);
                        $.each(temp, function (i) {
                            count = i;
                            var optionhtml = '<option value="' +
                             temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
                            $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

                        });
                if (count == "-1") {
                    alert("Class is not Created");
                }
                // bindDivision();
            },
            error: function (error) {
                // alert("Error" + error);
            }

        });
        }


        function BindCheckBOX() {
            $('[id$=chkHeader]').click(function () {

                $("[id$=chkChild]").attr('checked', this.checked);
            });
            $("[id$=chkChild]").click(function () {
                if ($('[id$=chkChild]').length == $('[id$=chkChild]:checked').length) {
                    $('[id$=chkHeader]').attr("checked", "checked");
                }
                else {
                    $('[id$=chkHeader]').removeAttr("checked");
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Student Fee Template
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1">Student Fee Template Details</a></li>

                    </ul>
                    <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <asp:HiddenField ID="hfCLassMID" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfDivisionTID" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfFees" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfAcademicYear" runat="server" ClientIDMode="Static" />
                        <div style="width: 100%;">
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
                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Width="150px" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="width: 100%;" class="divclasswithfloat">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Fee Category :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 31%; float: left;">
                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlFeesCategoryName" Width="150px" AutoPostBack="False">
                                    </asp:DropDownList>
                                </div>
                                <div style="text-align: left; float: right; width: 50%;" class="label">
                                    <asp:Button runat="server" ID="btnViewAssignFee" Text="View Fees" CssClass="btn-blue btn-blue-medium" OnClick="btnViewAssignFee_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnViewGrid" Text="View Students" CssClass="btn-blue btn-blue-medium" OnClick="btnViewGrid_Click" />

                                    <%-- <button id="btnViewGrid" type="button" class="btn-blue btn-blue-medium">View</button>--%>
                                </div>
                            </div>

                            <div class="clear"></div>
                            <div style="margin-top: 10px; width: 100%;" class="divclasswithoutfloat">
                                <asp:HiddenField ID="hfTab" runat="server" ClientIDMode="Static" />
                                <asp:GridView ID="gvStudentFees" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" BackColor="White" ShowHeaderWhenEmpty="true">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="StudentMID" HeaderText="Student ID">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                            <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Choose Category">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <HeaderStyle Width="30px"></HeaderStyle>
                                            <ItemTemplate>
                                                <%--<input type="checkbox" id="chkChild" />--%>
                                                <asp:CheckBox ID="chkChild" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStudentName" Text='<%#  Eval("StudentLastNameEng") + " " +Eval("StudentFirstNameEng") %> ' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="350px" HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>

                            <div class="clear"></div>
                            <div style="margin-top: 10px; width: 100%;">
                                <asp:GridView ID="gvAssignFees" runat="server" AutoGenerateColumns="false"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>
                            <div style="text-align: right;" class="divclasswithoutfloat">
                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;
							
                               <%-- <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue-new btn-blue-medium" OnClick="btnCancel_Click" />--%>
                                <%--<button id="btnSave" class="btn-blue btn-blue-medium" type="button" height="20px">Save</button>--%>
                                <%--&nbsp;&nbsp;--%>
                            </div>

                        </div>
                    </div>
                    <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
                </div>
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
        function checkForFees() {
            if ($('#<%= ddlClass.ClientID %> option:selected').val() != "" && $('#<%= ddlDivision.ClientID %> option:selected').val() != "" && $('#<%= ddlAcademicYear.ClientID %> option:selected').val() != "") {
                bindFees();
            }
        }

        function Clear() {
            bindClass();
            $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).empty();
                var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml1);
                //  bindFees();
            }

            $('#btnSave').click(function () {
                var valid = $("#aspnetForm").validationEngine('validate');
                var vars = $("#aspnetForm").serialize();

                if (valid == true) {
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [2, 3, 4] });
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });
                }

                $(".gv tr").each(function () {
                    // alert("gv");
                    var checkBox = $(this).find("input[type='checkbox']");
                    // var textBox = $(this).find("input[type='text']");
                    if ($(checkBox).is(':checked')) {
                        //alert("Checked");
                    }
                });
            });
            $(document.getElementById('<%= ddlClass.ClientID %>')).change(function () {
            $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
                $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).empty();
                var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
                var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlFeesCategoryName.ClientID %>')).append(optionhtml1);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "StudentTemplate.aspx/LoadDivision",
                    data: "{'intClassMID':'" + $('#<%= ddlClass.ClientID %> option:selected').val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
                    dataType: "json",
                    success: function (data) {
                        var count = -1;
                        var temp = $.parseJSON(data.d);

                        $.each(temp, function (i) {
                            count = i;
                            var optionhtml = '<option value="' +
                             temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);
                        });
                        checkForFees();
                        if (count == "-1") {
                            alert("Division is not Created");
                        }
                    },
                    error: function (error) {
                        // alert("Error" + error);
                    }

                });
            });
            $(document.getElementById('<%= ddlAcademicYear.ClientID %>')).change(function () {
            // alert("D");
            checkForFees();
        });
        $(document.getElementById('<%= ddlDivision.ClientID %>')).change(function () {
            checkForFees();
        });

        $('[id*=chkHeader]').click(function () {
            // alert("chkHeader");
            //  alert($("[id$=chkChild]").attr('checked', this.checked));
            // $("[id*=chkChild]").attr('checked', this.checked);
            if ($(this).is(":checked")) {
                $('[id*=chkChild]').prop("checked", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkChild]').prop("checked", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });
        $("[id*=chkChild]").click(function () {
            // alert("chkChild");
            if ($(this).is(":checked")) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                }

                // alert("chkChild");
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                    $('[id*=chkHeader]').prop("checked", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                }
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
                }
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                    // alert("In");

                    $('[id*=chkHeader]').prop("checked", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                } else {
                    $('[id*=chkHeader]').removeAttr("checked");
                }
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
                }
            });

    </script>
</asp:Content>
