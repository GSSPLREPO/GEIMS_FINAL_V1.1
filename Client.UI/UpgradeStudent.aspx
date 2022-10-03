<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="UpgradeStudent.aspx.cs" Inherits="GEIMS.Client.UI.UpgradeStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />

    <script type="text/javascript">


        function BindDorpdownOnButtonClick() {
            //alert("BindDorpdownOnButtonClick");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UpgradeStudent.aspx/LoadSection",
                data: "{'intSchoolID':'" + <%=Session["SchoolID"] %> + "'}",
                dataType: "json",
                success: function (data) {
                    var temp = $.parseJSON(data.d);
                    $(document.getElementById('<%= ddlSection.ClientID %>')).empty();
                    var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                    $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml1);
                   $("#divLoading").show();
                   $.each(temp, function (i) {
                       var optionhtml = '<option value="' +
                        temp[i].SectionMID + '">' + temp[i].SectionName + '</option>';
                       $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml);
                        $(document.getElementById('<%= ddlSection.ClientID %>')).val($(document.getElementById('<%= hfSectionID.ClientID %>')).val());
                    });
                    $("#divLoading").hide();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "UpgradeStudent.aspx/LoadClass",
                        data: "{'intSectionID':'" + $(document.getElementById('<%= hfSectionID.ClientID %>')).val() + "'" + ",'intSchoolMID':'" +  <%=Session["SchoolID"] %> + "'}",
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
                                url: "UpgradeStudent.aspx/LoadDivision",
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
                },

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
            Student Upgrade
           
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">

            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">

                <div id="divEmployee" runat="server">
                    <div id="tabs" runat="server">
                        <div id="tab-panel" class="style-tabs" visible="true">
                            <ul>
                                <li><a id="tabClassDetails" href="#tabs-1">Student Upgrade</a></li>

                            </ul>
                            <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <asp:HiddenField ID="hfCLassMID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hfDivisionTID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hfSectionID" runat="server" ClientIDMode="Static" />


                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 19%; float: left;" class="label">
                                            Section :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 81%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlSection" Width="150px">
                                            </asp:DropDownList>
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
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicYear" Enabled="True" Width="150px">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 19%; float: left;" class="label">
                                            Status :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 81%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlStatus" Width="150px">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 100%; float: left;">
                                            <asp:RadioButtonList ID="rblAcademicOrStatus" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Student Upgradation</asp:ListItem>
                                                <asp:ListItem Value="2">Student Status Change</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <%--<div id="divLoading" align="center" class="divclasswithfloat">
                                        Loading. Please wait.<br />
                                        <br />
                                        <img src="../Images/loading.gif" alt="" />
                                    </div>--%>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <asp:Button runat="server" ID="btnViewGrid" Text="View" CssClass="btn-blue btn-blue-medium" OnClick="btnViewGrid_Click" />
                                    </div>

                                    <div class="divclasswithfloat">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true" OnRowDataBound="gvStudent_RowDataBound">
                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SrNo" HeaderText="Sr No.">
                                                            <HeaderStyle Width="300px" HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="True"></ItemStyle>
                                                            </asp:BoundField>
                                                        <asp:BoundField DataField="StudentMID" HeaderText="Student ID">
                                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="StudentTID" HeaderText="Student ID">
                                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Choose Category">
                                                            <HeaderTemplate>

                                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--<input type="checkbox" id="chkChild" />--%>
                                                                <asp:CheckBox ID="chkChild" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gr No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGridGrNo" runat="server" Text='<%#Eval("CurrentGrNo")%>' Width="80px" CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AdmissionNo" HeaderText="Admission No">
                                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStudentName" Text='<%# Eval("StudentLastNameEng")  + " " + Eval("StudentFirstNameEng") + " "+Eval("StudentMiddleNameEng")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Choose Status">
                                                            <HeaderTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlStatusHeader" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusHeader_SelectedIndexChanged" Width="100px">
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlStatus" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Choose Class">
                                                            <HeaderTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlClassHeader" AutoPostBack="true" OnSelectedIndexChanged="ddlClassHeader_SelectedIndexChanged" Width="100px">
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlGridviewClass" AutoPostBack="true" OnSelectedIndexChanged="ddlGridviewClass_SelectedIndexChanged" Width="100px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Choose Class">
                                                            <HeaderTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlDivisionHeader" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisionHeader_SelectedIndexChanged" Width="100px">
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlGridDivision" Width="100px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Choose Year">
                                                            <HeaderTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlYearHeader" AutoPostBack="true" OnSelectedIndexChanged="ddlYearHeader_SelectedIndexChanged" Width="100px">
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlGridYear" Width="100px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Left Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDate" runat="server"  Width="80px" CssClass="TextBox" Text='<%#Eval("LeftDate")%>'></asp:TextBox>
                                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDate" TargetControlID="txtDate">
                                                                 </ajaxToolkit:CalendarExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div style="width: 100%; text-align: right;" class="divclasswithOutfloat">

                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click"  OnClientClick="javascript:return confirm('Are you sure, you want to Update Status of Selected Student?');" />

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
        $("#<%=btnSave.ClientID%>").on("click", function (e) {
            if ($('[id*=chkChild]:checked').length == 0) {
                alert("Select atleast one Student.");
                e.preventDefault();

            }
        });
        function jqry() { };
            $("[id*=chkHeader]").click(function () {
                //alert("chkHeader");
                //  alert($("[id$=chkChild]").attr('checked', this.checked));
                // $("[id*=chkChild]").attr('checked', this.checked);
                if ($(this).is(":checked")) {
                    $('[id*=chkChild]').prop("checked", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                } else {
                    $('[id*=chkChild]').prop("checked", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
                }
            });
       
        $("[id*=chkChild]").click(function () {
            //   alert("chkChild");
            if ($(this).is(":checked")) {
                //    alert("CHecked");
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
             if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                 // alert("In");

                 $('[id*=chkHeader]').prop("checked", true);
                 $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            }
            else {
                $('[id*=chkHeader]').removeAttr("checked");
            }
             if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                 $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
         });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jqry);
        function BindSection() {

            $(document.getElementById('<%= ddlSection.ClientID %>')).empty();
            $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UpgradeStudent.aspx/LoadSection",
                data: "{'intSchoolID':'" + <%=Session["SchoolID"] %> + "'}",
                dataType: "json",
                success: function (data) {
                    var count = -1;
                    var temp = $.parseJSON(data.d);

                    var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                    $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml1);
                    $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);
                    $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
                    $("#divLoading").show();
                    $.each(temp, function (i) {
                        count = i;
                        var optionhtml = '<option value="' +
                         temp[i].SectionMID + '">' + temp[i].SectionName + '</option>';
                        $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml);
                    });
                    $("#divLoading").hide();
                    if (count == "-1") {
                        alert("Section is not Created");
                    }
                },
                error: function (error) {
                }

            });

        }

       
       
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

        $(document.getElementById('<%= ddlSection.ClientID %>')).change(function () {
            $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            $(document.getElementById('<%= ddlClass.ClientID %>')).empty();

            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "UpgradeStudent.aspx/LoadClass",
                data: "{'intSectionID':'" + $(document.getElementById('<%= ddlSection.ClientID %>')).val() + "'" + ",'intSchoolMID':'" +  <%=Session["SchoolID"] %> + "'}",
                dataType: "json",
                success: function (data) {
                    var count = -1;
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
        });

        $(document.getElementById('<%= btnViewGrid.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
            $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
        });


    </script>

</asp:Content>
