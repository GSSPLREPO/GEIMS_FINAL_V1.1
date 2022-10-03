<%@ Page EnableEventValidation="false"  Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SchoolEmployee.aspx.cs" Inherits="GEIMS.Client.UI.SchoolEmployee" %>
<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/ModalPopupWindow.js"></script>
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script src="../JS/AjaxFileupload.js"></script>
    <link href="../CSS/Site.css" rel="stylesheet" />
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        var modalWin = new CreateModalPopUpObject();
        modalWin.SetCloseButtonImagePath("../Images/remove.gif");


        function AlertMessage() {
            alert('Please Take Photo.');
        }
        function closePopup() {
            $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1').hide();
            document.getElementById('ContentPlaceHolder1_ModalPopupExtender1').value = "";
        }

        $(document).ready(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SchoolEmployee.aspx/GetAllEmployeeNameForReport",
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

                }
            });
            var tab = $(document.getElementById('<%= hfTab.ClientID %>')).val();
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $('#<%=btnUploadDocument.ClientID%>').hide();
            $('#<%=btnGoToEmployee.ClientID%>').hide();
            if (tab == "0") {

                $("#divUser").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });


                $(document.getElementById('<%= ddlDesignation.ClientID %>')).empty();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SchoolEmployee.aspx/LoadDesignation",
                    data: "{}",
                    dataType: "json",
                    success: function (data) {
                        var temp = $.parseJSON(data.d);
                        var count = -1;
                        var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlDesignation.ClientID %>')).append(optionhtml1);

                        $.each(temp, function (i) {
                            count = i;
                            var optionhtml = '<option value="' +
                             temp[i].DesignationID + '">' + temp[i].DesignationNameENG + '</option>';
                            $(document.getElementById('<%= ddlDesignation.ClientID %>')).append(optionhtml);
                        });
                        if (count == "-1") {
                            alert("Designation is not Created");
                        }
                    },
                    error: function (error) {
                    }

                });

                $(document.getElementById('<%= ddlRole.ClientID %>')).empty();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SchoolEmployee.aspx/LoadRole",
                    data: "{}",
                    dataType: "json",
                    success: function (data) {
                        var temp = $.parseJSON(data.d);
                        var count = -1;
                        var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlRole.ClientID %>')).append(optionhtml1);

                        $.each(temp, function (i) {
                            count = i;
                            var optionhtml = '<option value="' +
                             temp[i].RoleID + '">' + temp[i].RoleName + '</option>';
                            $(document.getElementById('<%= ddlRole.ClientID %>')).append(optionhtml);
                        });
                        if (count == "-1") {
                            alert("Class is not Created");
                        }
                    },
                    error: function (error) {
                    }

                });

                $(document.getElementById('<%= ddlDepartment.ClientID %>')).empty();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SchoolEmployee.aspx/LoadDepartMent",
                    data: "{'intTrustID':'" + <%=Session["TrustID"] %> + "','intSchoolID':'" + <%=Session["SchoolID"] %> + "'}",
                    dataType: "json",
                    success: function (data) {
                        var temp = $.parseJSON(data.d);
                        var count = -1;
                        var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlDepartment.ClientID %>')).append(optionhtml1);

                    $.each(temp, function (i) {
                        count = i;
                        var optionhtml = '<option value="' +
                            temp[i].DepartmentID + '">' + temp[i].DepartmentNameENG + '</option>';
                        $(document.getElementById('<%= ddlDepartment.ClientID %>')).append(optionhtml);
                    });
                    if (count == "-1") {
                        alert("Department is not Created");
                    }
                },
                    error: function (error) {
                    }

                });

        }

        else if (tab == "1") {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 3, 2, 4, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });

                var EmployeeID = $('#<%=hfEmployeeMID.ClientID%>').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SchoolEmployee.aspx/LoadDocument",
                    data: "{'intSchoolID':'" + 0 + "','intTrustMID':'" + 0 + "','intStudentMID':'" + 0 + "','intEmployeeMID':'" + EmployeeID + "'}",
                    dataType: "json",
                    success: function (data) {
                        var temp = $.parseJSON(data.d);

                        $.each(temp, function (i) {
                            var hdnid = temp[i].DocumentMID;
                            var StudentID = $('#<%=hfEmployeeMID.ClientID%>').val();
                            var SchoolID = 0;
                            var TrustID = 0;
                            var EmployeeMID = 0;
                            var txtDocDescId = 'txtDocDesc_' + hdnid;

                            var lblfilename = temp[i].DocumentName + hdnid;
                            var path = temp[i].DocumentPath;
                            var file = temp[i].DocumentName;

                            var fileName = file.substr(0, (file.lastIndexOf('.')));
                            ShowUploadedFiles(file, fileName, hdnid);
                        });
                    },

                    error: function (error) {
                        // alert("Error" + error);
                    }

                });
                function ShowUploadedFiles(file, fileName, hdnid) {
                    count = hdnid;
                    var hdnid = 'hdnDocId_' + count;
                    //var hdnid = 'hdnDocId_' + $("#hdnCountFiles").val();
                    var SchoolID = 0;
                    var TrustID = 0;
                    var StudentMID = 0;
                    var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();
                    var txtDocDescId = 'txtDocDesc_' + count;
                    var lblfilename = 'lblfilename_' + count;
                    var path = $('#hdnUploadFilePath').val();
                    $("#uploadedDiv").append("<div id='" + hdnid + "' style='clear:both; background-color:#d2e9ff; padding-top:5px; height:25px; width:500px'><span id='" + lblfilename + "' style='width:175px;float:left;margin-left:40px;overflow:hidden;'>" + fileName +
                        "</span><span style='width:170px;float:left;margin-left:0px;'><input type='text' id='" + txtDocDescId + "' value='" + fileName +
                        "' /><input name='" + hdnid + "' id='" + hdnid + "' value='" + count + "' type='hidden'></span><span style='float:left; margin-left:10px; width:40px;' >" +
                        "<a href='#' class='dellink' onclick='deleterow(\"#" + hdnid + "," + file + "\")'>Delete</a></span>" + // for deleting file
                        "<span style='float:left; margin-left:10px; width:40px;' ><a class='dellink' href='FileUpload.ashx?filepath=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&StudentMID=" + StudentMID + "&EmployeeMID=" + EmployeeMID + "' >View</a></span></div>" // for downloading file
                        );
                    //update file counter
                    $("#hdnCountFiles").val(count);
                    return false;

                }
                // delete existing file
                function deleterow(divrow) {
                    var SchoolID = 0;
                    var StudentMID = 0;
                    var TrustID = 0;
                    var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();
                    var str = divrow.split(",");
                    var row = str[0];
                    var file = str[1];
                    var docID = row.substr(10, str[0].length);

                    var path = $('#hdnUploadFilePath').val();
                    if (confirm('Are you sure to delete?')) {

                        $.ajax({
                            url: "FileUpload.ashx?path=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&DocMID=" + docID + "&StudentMID=" + StudentMID + "&EmployeeMID=" + EmployeeMID,
                            type: "GET",
                            cache: false,
                            async: true,
                            success: function (html) {

                            }
                        });
                        $(row).remove();
                    }
                    return false;
                }
            }
            else if (tab == "2") {

                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });
            }
            else if (tab == "21") {

                if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() != "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() != "0") {
                  //  alert("hi");
                    $("#divEmpExpirence").show();
                    $("#divAddEmpExpirence").show();
                 
                }
                else if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() != "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() == "0") {
                    $("#divEmpExpirence").show();
                    $("#divAddEmpExpirence").show();
                   
                }
                else if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() == "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() != "0") {
                    $("#divEmpExpirence").show();
                    $("#divAddEmpExpirence").show();
                   
                }
                else {
                  
                    $("#divEmpExpirence").hide();
                    $("#divAddEmpExpirence").hide();
                   

                }
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });
            }
            else if (tab == "3") {

                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 5);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 4, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 5 });
            }
            else if (tab == "4") {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });
                $('#<%=btnUploadDocument.ClientID%>').show();

                // $("#SchoolReplacementInfo").show();
                // $('#divUser').show();
                // $("#divResignGuj").show();
            }
            else if (tab == "5") {
                $(document.getElementById('<%= btnGoToEmployee.ClientID %>')).hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });

                var EmployeeID = $('#<%=hfEmployeeMID.ClientID%>').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SchoolEmployee.aspx/LoadDocument",
                    data: "{'intSchoolID':'" + 0 + "','intTrustMID':'" + 0 + "','intStudentMID':'" + 0 + "','intEmployeeMID':'" + EmployeeID + "'}",
                    dataType: "json",
                    success: function (data) {
                        var temp = $.parseJSON(data.d);

                        $.each(temp, function (i) {
                            var hdnid = temp[i].DocumentMID;
                            var StudentID = $('#<%=hfEmployeeMID.ClientID%>').val();
                            var SchoolID = 0;
                            var TrustID = 0;
                            var EmployeeMID = 0;
                            var txtDocDescId = 'txtDocDesc_' + hdnid;

                            var lblfilename = temp[i].DocumentName + hdnid;
                            var path = temp[i].DocumentPath;
                            var file = temp[i].DocumentName;

                            var fileName = file.substr(0, (file.lastIndexOf('.')));
                            ShowUploadedFiles(file, fileName, hdnid);
                        });
                    },

                    error: function (error) {
                        // alert("Error" + error);
                    }

                });
                function ShowUploadedFiles(file, fileName, hdnid) {
                    count = hdnid;
                    var hdnid = 'hdnDocId_' + count;
                    //var hdnid = 'hdnDocId_' + $("#hdnCountFiles").val();
                    var SchoolID = 0;
                    var TrustID = 0;
                    var StudentMID = 0;
                    var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();
                    var txtDocDescId = 'txtDocDesc_' + count;
                    var lblfilename = 'lblfilename_' + count;
                    var path = $('#hdnUploadFilePath').val();
                    $("#uploadedDiv").append("<div id='" + hdnid + "' style='clear:both; background-color:#d2e9ff; padding-top:5px; height:25px; width:500px'><span id='" + lblfilename + "' style='width:175px;float:left;margin-left:40px;overflow:hidden;'>" + fileName +
                        "</span><span style='width:170px;float:left;margin-left:0px;'><input type='text' id='" + txtDocDescId + "' value='" + fileName +
                        "' /><input name='" + hdnid + "' id='" + hdnid + "' value='" + count + "' type='hidden'></span><span style='float:left; margin-left:10px; width:40px;' >" +
                        "<a href='#' class='dellink' onclick='deleterow(\"#" + hdnid + "," + file + "\")'>Delete</a></span>" + // for deleting file
                        "<span style='float:left; margin-left:10px; width:40px;' ><a class='dellink' href='FileUpload.ashx?filepath=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&StudentMID=" + StudentMID + "&EmployeeMID=" + EmployeeMID + "' >View</a></span></div>" // for downloading file
                        );
                    //update file counter
                    $("#hdnCountFiles").val(count);
                    return false;

                }
                // delete existing file
                function deleterow(divrow) {
                    var SchoolID = 0;
                    var StudentMID = 0;
                    var TrustID = 0;
                    var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();
                    var str = divrow.split(",");
                    var row = str[0];
                    var file = str[1];
                    var docID = row.substr(10, str[0].length);

                    var path = $('#hdnUploadFilePath').val();
                    if (confirm('Are you sure to delete?')) {

                        $.ajax({
                            url: "FileUpload.ashx?path=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&DocMID=" + docID + "&StudentMID=" + StudentMID + "&EmployeeMID=" + EmployeeMID,
                            type: "GET",
                            cache: false,
                            async: true,
                            success: function (html) {

                            }
                        });
                        $(row).remove();
                    }
                    return false;
                }
            }
        });


    </script>
    <style type="text/css">
        .RadWModalImage {
            filter: Alpha(Opacity=80);
            -moz-opacity: 0.4;
            opacity: 0.4;
            background-color: #666699;
        }

        .pnlWeather {
            background-color: black;
            padding: 0;
        }

        .popup_Container {
            background-color: white;
            border: 0px solid #000000;
            padding: 0;
            height: 470px;
            width: 780px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Employee Master
            <asp:LinkButton CausesValidation="false" ID="btnAddEmployee" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnAddEmployee_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnViewList_Click">View List</asp:LinkButton>
            &nbsp;<%--<button id="btnUploadDocument" type="button"  class="btn-blue btn-blue-medium">Other Document</button>--%>
            <asp:LinkButton CausesValidation="false" ID="btnUploadDocument" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnUploadDocument_Click">Other Document</asp:LinkButton>
            <asp:LinkButton CausesValidation="false" ID="btnGoToEmployee" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnGoToEmployee_Click">Go to Employee</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">

            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGridPanel" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px;padding-right:10px; width: 100%;">
                    <fieldset>
                        <legend>Search Employee</legend>
                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server" Enabled="false" >
                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">Employee Name</asp:ListItem>
                            <asp:ListItem Value="2">Employee Code</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] textarea autosuggest"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGo" runat="server" CssClass="btn-blue Attach" Width="50px" Text="Go" CausesValidation="false"
                        OnClick="btnGo_Click" />
                        <br />
                        <br />
                        <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvEmployee_RowCommand">
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                <Columns>
                                     <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code">
                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name">
                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Trust/School Name">
                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Designation" HeaderText="Designation">
                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Department" HeaderText="Department">
                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No">
                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                CommandName="Edit1" CommandArgument='<%# Eval("EmployeeMID")%>' Height="20px" Width="20px" />
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
                    </fieldset>
                </div>
                <div id="divEmployee" runat="server" >
                    <div id="tabs" runat="server">
                        <div id="tab-panel" class="style-tabs" visible="true" style="padding:5px 5px 5px 5px">
                            <ul>
                                <li><a id="tabClassDetails" href="#tabs-1">Employee</a></li>
                                <li><a id="tabSchoolDetail" href="#tabs-2">School</a></li>
                                <li><a id="tabGujarati" href="#tabs-3">Gujarati</a></li>
                                <li><a id="tabEducationDetail" href="#tabs-4">Qualification</a></li>
                                <li><a id="tabExperienceDetail" href="#tabs-5">Experience</a></li>
                                <li><a id="tabFamilyDetail" href="#tabs-6">Family</a></li>
                                <li><a id="tabDocument" href="#tabs-7">Document</a></li>
                            </ul>
                            <div id="tabs-1" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <asp:HiddenField runat="server" ID="hfTab" />
                                    <asp:HiddenField runat="server" ID="hfSchoolMID" />
                                    <asp:HiddenField runat="server" ID="hfDesignationID" />
                                    <asp:HiddenField runat="server" ID="hfDepartmentID" />
                                    <asp:HiddenField runat="server" ID="hfRoleID" />

                                    <div style="width: 100%" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Employee Name : <span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtEmployeeFirstName" runat="server" CssClass="validate[required,custom[onlyLetterSp]] TextBox" placeholder="First Name" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtEmployeeMiddleName" runat="server" CssClass="validate[custom[onlyLetterSp]] TextBox" placeholder="Middle Name" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtEmployeeLastName" runat="server" CssClass="validate[required,custom[onlyLetterSp]] TextBox" placeholder="Last Name" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Mother Maiden Name :<%-- <span style="color: red">*</span>--%>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtMMFirstName" runat="server" CssClass="validate[custom[onlyLetterSp]] TextBox" placeholder="First Name" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtMMMiddleName" runat="server" CssClass="validate[custom[onlyLetterSp]] TextBox" placeholder="Middle Name" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtMMLastName" runat="server" CssClass="validate[custom[onlyLetterSp]] TextBox" placeholder="Last Name" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Upload Photo :
                                        </div>
                                        <div style="text-align: left; width: 79%; float: right; vertical-align: top;">
                                            <asp:FileUpload ID="fuImage" runat="server" CssClass="TextBox Detach" Height="25px" onchange="UploadFileNow()" />
                                        </div>
                                    </div>

                                    <div style="width: 100%;">
                                        <div style="width: 70%; float: left">
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 30%; float: left;" class="label">
                                                    Employee Code:<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 70%; float: right; vertical-align: top;">
                                                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 30%; float: left;" class="label">
                                                    Gender:<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 70%; float: left;">
                                                    <%--<asp:RadioButton runat="server" CssClass="validate[required]" ValidationGroup="Gender" Text="Male" ID="rdMale" />
										<asp:RadioButton runat="server" CssClass="validate[required]" ValidationGroup="Gender" Text="Female" ID="rdFemale" />--%>
                                                    <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="MALE">Male</asp:ListItem>
                                                        <asp:ListItem Value="FEMALE">Female</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 30%; float: left;" class="label">
                                                    Email ID:
                                                </div>
                                                <div style="text-align: left; width: 70%; float: right; vertical-align: top;">
                                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 30%; float: left;" class="label">
                                                    Nationality:
                                                </div>
                                                <div style="text-align: left; width: 70%; float: right; vertical-align: top;">
                                                    <asp:TextBox ID="txtNationality" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div style="width: 30%; float: left; text-align: left;">
                                            <asp:Image ImageUrl="~/Images/NoImage-big.jpg" runat="server" ID="imgphoto"
                                                Width="60%" Height="120px" />
                                            <div style="width: 100%; float: left; padding-left: 20px" class="divclasswithfloat">
                                                <%-- <asp:LinkButton ID="lbtnRemovePhoto" CssClass="label" Visible="false" runat="server"
                                                    Text="Remove Photo" CausesValidation="False"></asp:LinkButton><br />
                                                <span style="color: Blue; font-size: 10px;">.jpg,jpeg,.gif,.bmp size 120 X 120</span><br />--%>
                                                <asp:Button runat="server" ID="btnShowModal" Text="Take Photo" OnClick="btnClickPhoto_Click"
                                                    CausesValidation="false" CssClass="btn-blue-new btn-blue-medium Detach" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnClickPhoto" runat="server" Text="Done" CssClass="btn-blue-new btn-blue-medium"
                                                                    Width="50" Style="display: none" />
                                            </div>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <fieldset>
                                                <legend>Employee Birth Detail</legend>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Date Of Birth:  
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateOfBirth" TargetControlID="txtDateOfBirth">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Birth District :
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtBirthDistrict" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Birth Taluka :
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtBirthTaluka" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Birth City/Village:  
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtBirthCity" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </fieldset>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <fieldset>
                                                <legend>Personal Detail</legend>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Mother Tongue:  
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtMotherToungue" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Religion:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtReligion" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Caste:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtCaste" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Marital Status: 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtMaritalStatus" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Blood Group:  
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <!--<div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Category: 
                                                    </div>

                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtCategoryEng" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>-->
                                                     <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Category: <span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:RadioButtonList ID="rblCategory" CssClass="CheckBoxList" runat="server" RepeatDirection="Horizontal" Font-Bold="false">
                                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                                            <asp:ListItem Value="OBC">OBC</asp:ListItem>
                                                            <asp:ListItem Value="SC">SC</asp:ListItem>
                                                            <asp:ListItem Value="ST">ST</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Hobbies:  
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtHobbies" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Physical Identification:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: right; vertical-align: top;">
                                                        <asp:TextBox ID="txtPhysicalIdentification" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Aadhar Card No:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtAadharCardNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Election Card No: 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtElectionCardNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Vehicle No:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Portal ID: 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtPortalID" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        PRAN No:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtPRANNO" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        TAN NO: 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtTANNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <fieldset>
                                                <legend>Physical Detail</legend>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Height(In cms): 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtHeight" runat="server" CssClass="validate[custom[number]] TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Weight(In Kgs):
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="validate[custom[number]] TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Rectification Device :
                                                    </div>
                                                    <div style="text-align: left; width: 80%; float: left;">
                                                        <asp:TextBox ID="txtRectificationDevice" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">

                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Left Vision : 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtLeftVision" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Right Vision :  
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtRightVision" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <fieldset>
                                                <legend>Contact Detail</legend>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Mobile No: 
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                    </div>
                                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                                        Phone No:
                                                    </div>
                                                    <div style="text-align: left; width: 30%; float: left;">
                                                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </fieldset>
                                        </div>

                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 47%; float: left;" class="divclasswithfloat">
                                                <fieldset>
                                                    <legend>Correspondence Address</legend>

                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 41%; float: left;" class="label">
                                                            Address :<span style="color: red">*</span>
                                                        </div>
                                                        <div style="text-align: left; width: 59%; float: left;">
                                                            <asp:TextBox ID="txtCurAddress" runat="server" CssClass="validate[required] SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 41%; float: left;" class="label">
                                                            City/Town :  
                                                        </div>
                                                        <div style="text-align: left; width: 59%; float: left;">
                                                            <asp:TextBox ID="txtCurCity" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 41%; float: left;" class="label">
                                                            Landmark :  
                                                        </div>
                                                        <div style="text-align: left; width: 59%; float: left;">
                                                            <asp:TextBox ID="txtCurLandMark" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 41%; float: left;" class="label">
                                                            State :  
                                                        </div>
                                                        <div style="text-align: left; width: 59%; float: left;">
                                                            <asp:TextBox ID="txtCurState" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 41%; float: left;" class="label">
                                                            Pin Code :  
                                                        </div>
                                                        <div style="text-align: left; width: 59%; float: left;">
                                                            <asp:TextBox ID="txtCurPinCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </fieldset>

                                            </div>
                                            <div style="vertical-align: middle; width: 6%; float: left; padding-top: 120px; text-align: center">
                                                <button id="btnForward" type="button" class="button" style="width: 30px">>></button>
                                                <%--<asp:Button ID="btnForward" CssClass="button" Text=">>" Font-Bold="true" runat="server"
									Width="30px" OnClick="btnForward_Click" />--%>
                                            </div>
                                            <div style="float: left; width: 46%;" class="divclasswithfloat">
                                                <fieldset>
                                                    <legend>Permanent Address</legend>
                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 33%; float: left;" class="label">
                                                            Address :  <span style="color: red">*</span>
                                                        </div>
                                                        <div style="text-align: left; width: 67%; float: left;">
                                                            <asp:TextBox ID="txtPermenantAddress" runat="server" CssClass="validate[required] SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 33%; float: left;" class="label">
                                                            City/Town :  
                                                        </div>
                                                        <div style="text-align: left; width: 67%; float: left;">
                                                            <asp:TextBox ID="txtPermenantCity" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 33%; float: left;" class="label">
                                                            Landmark :  
                                                        </div>
                                                        <div style="text-align: left; width: 67%; float: left;">
                                                            <asp:TextBox ID="txtPermenantLandMark" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 33%; float: left;" class="label">
                                                            State :  
                                                        </div>
                                                        <div style="text-align: left; width: 67%; float: left;">
                                                            <asp:TextBox ID="txtPermenantState" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>



                                                    <div style="width: 100%;" class="divclasswithfloat">
                                                        <div style="text-align: left; width: 33%; float: left;" class="label">
                                                            Pin Code :  
                                                        </div>
                                                        <div style="text-align: left; width: 67%; float: left;">
                                                            <asp:TextBox ID="txtPermenantPinCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; text-align: right; margin-bottom: 0px;" class="divclasswithoutfloat">
                                        <%--<button id="btnNextPersonal" type="button" class="btn-blue-new btn-blue-medium">Next</button>--%>
                                        <button id="btnNextPersonal" type="button" class="btn-blue-new btn-blue-medium">Next</button>
                                    </div>
                                </div>

                            </div>

                            <div id="tabs-2" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <div style="width: 100%;">
                                        <fieldset>
                                            <legend>School Detail</legend>
                                           <%-- <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 21%; float: left;" class="label">
                                                    Select :<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 79%; float: left;">

                                                    <asp:RadioButtonList runat="server" ID="rblSelect" RepeatDirection="Horizontal" Enabled ="false">
                                                        <asp:ListItem Selected="True" Value="0">School</asp:ListItem>
                                                        <asp:ListItem Value="1">Trust</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>--%>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                               <%-- <div id="divSchoolDetail">
                                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                                        School:  <span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 31%; float: left;">
                                                        <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlSchool" Width="65%">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>--%>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Department :<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDepartment" Width="65%">
                                                    </asp:DropDownList>
                                                </div>
                                                 <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Reporting To:<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlReportingTo" Width="65%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Designation:<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDesignation" Width="65%">
                                                    </asp:DropDownList>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Employee Role:<span style="color: red">*</span>
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlRole" Width="65%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div style="width: 100%;">
                                        <fieldset>
                                            <legend>Joining Detail</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Dept. Join Date 
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtDeptJoinDate" runat="server" CssClass="TextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtDeptJoinDate" TargetControlID="txtDeptJoinDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Org. Join Date :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtOrgDate" runat="server" CssClass="TextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtOrgDate" TargetControlID="txtOrgDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Appointment Type 
                                                </div>
                                                <div style="text-align: left; width: 81%; float: left;">
                                                    <asp:RadioButtonList ID="rblAppointmentType" runat="server" RepeatDirection="Horizontal">
                                                         <asp:ListItem Value="0" Text="Gov DEO">GovDEO</asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Gov Pravasi">GovPravasi</asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Mgt Permanent">MgtPermanent</asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Mgt Temporary">MgtTemporary</asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Mgt Daily Wages">MgtDailyWages</asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Direct">Direct</asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Replacement">Replacement</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div id="SchoolReplacementInfo" style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Replacement School Info :
                                                </div>
                                                <div style="text-align: left; width: 81%; float: left;">
                                                    <asp:TextBox ID="txtSchoolInfo" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Retirement Date 
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtRetirementDate" runat="server" CssClass="TextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender4" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtRetirementDate" TargetControlID="txtRetirementDate">
                                                    </ajaxToolkit:CalendarExtender>--%>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Term Retirement Date :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtTermRetireDate" runat="server" CssClass="TextBox" Width="150px" ReadOnly="True" ></asp:TextBox>
                                                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTermRetireDate" TargetControlID="txtTermRetireDate">
                                                    </ajaxToolkit:CalendarExtender>--%>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Break Info:  
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtBreakInfo" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Achievement Info:
                                                </div>
                                                <div style="text-align: left; width: 31%; float: right; vertical-align: top;">
                                                    <asp:TextBox ID="txtAchieveMentInfo" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div style="width: 100%;">
                                        <fieldset>
                                            <legend>Bank Detail</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Bank Name :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Branch Name :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Branch Code :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtBranchCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Account No :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    PF No :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtPFNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Pan No : 
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtPanNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    ESIC No :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtEsicNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    IFSC Code : 
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtIfscCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>PF Account Detail</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    GPF Account No :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtGpfNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    CPF Account No :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:TextBox ID="txtCpfNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div style="width: 100%;">
                                        <fieldset>
                                            <legend>Other Detail</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    As Teacher :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:RadioButtonList ID="rblTeacher" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Selected="True">Teacher</asp:ListItem>
                                                        <asp:ListItem Value="1">Principal</asp:ListItem>
                                                        <asp:ListItem Value="2">None</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Allowance Allowed :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:CheckBox runat="server" ID="chkAlowance" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div style="width: 100%;">
                                        <fieldset>
                                            <legend>Account Detail</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Is User :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:CheckBox runat="server" ID="chkIsUser" />
                                                </div>
                                            </div>
                                            <div id="divUser" style="width: 100%;">
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                                        Username :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 81%; float: left;">
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="validate[required] TextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                                        Password :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 31%; float: left;">
                                                        <asp:TextBox ID="txtPassWord" runat="server" CssClass="validate[required] TextBox" Width="150px" TextMode="PassWord" ReadOnly="True"></asp:TextBox>
                                                         <asp:HiddenField ID="hfPassword" runat="server" />
                                                    </div>
                                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                                        Retype-Password :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 31%; float: left;">
                                                        <asp:TextBox ID="txtRePassword" runat="server" CssClass="validate[required, equals[ctl00_ContentPlaceHolder1_txtPassWord]] TextBox" Width="150px" TextMode="PassWord" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                    <div style="width: 100%;">
                                        <fieldset>
                                            <legend>Resignation Detail</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                                    Has Resigned :
                                                </div>
                                                <div style="text-align: left; width: 31%; float: left;">
                                                    <asp:CheckBox runat="server" ID="chkIsResign" />
                                                </div>
                                            </div>
                                            <div id="divResign" style="width: 100%;">
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                                        Resign Date :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 81%; float: left;">
                                                        <asp:TextBox ID="txtResignDate" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtResignDate" TargetControlID="txtResignDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                                        Resign Reason :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 81%; float: left;">
                                                        <asp:TextBox ID="txtResignReason" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div style="width: 100%; text-align: right; margin-bottom: 0px;" class="divclasswithoutfloat">
                                        <div style="float: left">
                                            <button id="btnBackSchoolDetail" type="button" class="btn-blue-new btn-blue-medium">Back</button>
                                        </div>
                                        <div>
                                            <button id="btnNextSchoolDetail" type="button" class="btn-blue-new btn-blue-medium">Next</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="tabs-3" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <div style="width: 100%" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            કર્મચારીનું નામ : <span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtEmployeeFirstNameGuj" runat="server" CssClass="validate[required] TextBox" placeholder="નામ" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtEmployeeMiddleNameGuj" runat="server" CssClass="TextBox" placeholder="પિતા નુ નામ" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtEmployeeLastNameGuj" runat="server" CssClass="validate[required] TextBox" placeholder="અટક" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            માતાનું લગ્ન પહેલાંનું નામ : <span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtMotherFirstNameGuj" runat="server" CssClass="validate[required] TextBox" placeholder="નામ" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtMotherMiddleNameGuj" runat="server" CssClass="TextBox" placeholder="પિતા નુ નામ" Width="150px"></asp:TextBox>
                                            &nbsp;
								<asp:TextBox ID="txtMotherLastNameGuj" runat="server" CssClass="TextBox" placeholder="અટક" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            જાતિ :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:RadioButtonList ID="rblGenderGuj" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="પુરૂષ">પુરૂષ</asp:ListItem>
                                                <asp:ListItem Value="સ્ત્રી">સ્ત્રી</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            રાષ્ટ્રીયતા : 
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtNationalityGuj" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            ધર્મ  : 
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtReligionGuj" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            શારીરિક ઓળખ : 
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtPhysicalIdentificationGuj" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>

                                     <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            જાતિ :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:RadioButtonList ID="rblCategoryGuj" CssClass="CheckBoxList" runat="server" RepeatDirection="Horizontal" Font-Bold="false">
                                                <asp:ListItem Value="સામાન્ય">સામાન્ય</asp:ListItem>
                                                <asp:ListItem Value="બક્ષીપંચ">બક્ષીપંચ</asp:ListItem>
                                                <asp:ListItem Value="SC">અનુસૂચિત જાતિ</asp:ListItem>
                                                <asp:ListItem Value="અનુસૂચિત જન જાતિ">અનુસૂચિત જન જાતિ</asp:ListItem>
                                                <asp:ListItem Value="અન્ય">અન્ય</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                   <!-- <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            જાતિ : 
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtCategoryGuj" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                        </div>-->

                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            કારકિર્દી બ્રેક માહિતી :  
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtBreakInfoGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            સિદ્ધિ માહિતી :
                                        </div>
                                        <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                            <asp:TextBox ID="txtAchievementInfoGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            શાળા બદલિ માહિતી :  
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtSchoolReplacementInfoGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div id="divResignGuj">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                રાજીનામું નું કારણ :
                                            </div>
                                            <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                                <asp:TextBox ID="txtResignReasonGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <fieldset>
                                            <legend>કર્મચારીનું જન્મ વિગતવાર</legend>
                                            <div style="width: 100%;" class="divclasswithfloat">
                                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                                    જન્મ જિલ્લા :
                                                </div>
                                                <div style="text-align: left; width: 30%; float: left;">
                                                    <asp:TextBox ID="txtBirthDistrictGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>
                                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                                    જન્મ તાલુકા :
                                                </div>
                                                <div style="text-align: left; width: 30%; float: left;">
                                                    <asp:TextBox ID="txtBirthTalukaGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div style="width: 100%;" class="divclasswithfloat">

                                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                                    જન્મ શહેર/ગામ:  
                                                </div>
                                                <div style="text-align: left; width: 80%; float: left;">
                                                    <asp:TextBox ID="txtBirthCityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>

                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 47%; float: left;" class="divclasswithfloat">
                                            <fieldset>
                                                <legend>પત્રવ્યવહાર સરનામું</legend>

                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 40%; float: left;" class="label">
                                                        સરનામું :<span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 60%; float: left;">
                                                        <asp:TextBox ID="txtCorrespondenceAddressGuj" runat="server" CssClass="validate[required] SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 40%; float: left;" class="label">
                                                        સીમાચિહ્ન :   
                                                    </div>
                                                    <div style="text-align: left; width: 60%; float: left;">
                                                        <asp:TextBox ID="txtCurLandMarkGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 40%; float: left;" class="label">
                                                        શહેર/નગર :  
                                                    </div>
                                                    <div style="text-align: left; width: 60%; float: left;">
                                                        <asp:TextBox ID="txtCorrespondenceCityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 40%; float: left;" class="label">
                                                        રાજ્ય :  
                                                    </div>
                                                    <div style="text-align: left; width: 60%; float: left;">
                                                        <asp:TextBox ID="txtCorrespondenceStateGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </fieldset>

                                        </div>
                                        <div style="vertical-align: middle; width: 6%; float: left; padding-top: 120px; text-align: center">
                                            <button id="btnForwardGuj" type="button" class="button" style="width: 30px">>></button>
                                            <%--<asp:Button ID="btnForward" CssClass="button" Text=">>" Font-Bold="true" runat="server"
									Width="30px" OnClick="btnForward_Click" />--%>
                                        </div>
                                        <div style="float: left; width: 46%;" class="divclasswithfloat">
                                            <fieldset>
                                                <legend>કાયમી સરનામું</legend>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 33%; float: left;" class="label">
                                                        સરનામું :  <span style="color: red">*</span>
                                                    </div>
                                                    <div style="text-align: left; width: 67%; float: left;">
                                                        <asp:TextBox ID="txtPermenantAddressGuj" runat="server" CssClass="validate[required] SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 33%; float: left;" class="label">
                                                        સીમાચિહ્ન :  
                                                    </div>
                                                    <div style="text-align: left; width: 67%; float: left;">
                                                        <asp:TextBox ID="txtPermenantLandMarkGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 33%; float: left;" class="label">
                                                        શહેર/નગર :  
                                                    </div>
                                                    <div style="text-align: left; width: 67%; float: left;">
                                                        <asp:TextBox ID="txtPermenantCityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="width: 100%;" class="divclasswithfloat">
                                                    <div style="text-align: left; width: 33%; float: left;" class="label">
                                                        રાજ્ય :  
                                                    </div>
                                                    <div style="text-align: left; width: 67%; float: left;">
                                                        <asp:TextBox ID="txtPermenantStateGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div style="width: 100%; text-align: right; margin-bottom: 0px;" class="divclasswithoutfloat">
                                        <div style="float: left">
                                            <button id="btnBackGujarati" type="button" class="btn-blue-new btn-blue-medium">Back</button>
                                        </div>
                                        <div>
                                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                    <%-- <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                    </div>--%>
                                </div>
                            </div>

                            <div id="tabs-4" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">

                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Qualification :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtQualificationEng" runat="server" CssClass="validate[required] TextBox" Width="350px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            શૈક્ષણિક લાયકાત :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtQualificationGuj" runat="server" CssClass="validate[required] TextBox" Width="350px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Percentage : <span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtPercentage" runat="server" CssClass="validate[required] TextBox" Width="150px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Year : <span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlYear" Width="65%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            University :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtUniversityEng" runat="server" CssClass="validate[required] TextBox" Width="350px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            યુનિવર્સિટી : 
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtUniversityGuj" runat="server" CssClass=" TextBox" Width="350px"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <asp:GridView ID="gvEmployeeQualification" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" OnRowCommand="gvEmployeeQualification_RowCommand"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="QualificationNameEng" HeaderText="Qualification Name">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Year" HeaderText="Year">
                                                    <HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Percentage" HeaderText="%">
                                                    <HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                            CommandName="Edit1" CommandArgument='<%# Eval("QualificationTID")%>' CssClass="Detach" Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                                            CommandName="Delete1" CommandArgument='<%# Eval("QualificationTID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                                    <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                        <div style="text-align: right; width: 10%; float: right;">
                                            <%--<button id="btnCancelQualification" type="button" class="btn-blue btn-blue-medium Detach">Cancel</button>--%>
                                            <asp:Button runat="server" ID="btnCancelQualification" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancelQualification_Click" />
                                        </div>
                                        <div style="text-align: right; width: 90%; float: right;">
                                            <asp:Button runat="server" ID="btnAddQualification" Text="Add" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnAddQualification_Click" />
                                        </div>
                                    </div>
                                    <%--  <div class="clear"></div>--%>
                                    <div style="width: 100%; padding-top: 10px; text-align: right;" class="divclasswithoutfloat">
                                         <div style="float: left">
                                            <button onclick="gvEmployee_RowCommand" id="btnBackQualification" type="button" class="btn-blue-new btn-blue-medium">Back</button>
                                        </div>
                                        <button id="btnContinueQualification" type="button" class="btn-blue-new btn-blue-medium Detach">Continue</button>
                                    </div>
                                    <%-- <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                    </div>--%>
                                </div>

                            </div>

                            <div id="tabs-5" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Duration Year :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDurationYear" Width="50%">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="31">31</asp:ListItem>
                                                <asp:ListItem Value="32">32</asp:ListItem>
                                                <asp:ListItem Value="33">33</asp:ListItem>
                                                <asp:ListItem Value="34">34</asp:ListItem>
                                                <asp:ListItem Value="35">35</asp:ListItem>
                                                <asp:ListItem Value="36">36</asp:ListItem>
                                                <asp:ListItem Value="37">37</asp:ListItem>
                                                <asp:ListItem Value="38">38</asp:ListItem>
                                                <asp:ListItem Value="39">39</asp:ListItem>
                                                <asp:ListItem Value="40">40</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Duration Month :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlMonth" Width="50%">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divEmpExpirence">
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                Organisation Name :<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 79%; float: left;">
                                                <asp:TextBox ID="txtOrganisationNameEng" runat="server" CssClass="validate[required] TextBox" Width="350px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                સંસ્થાનુ નામ : <span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 79%; float: left;">
                                                <asp:TextBox ID="txtOrganisationNameGuj" runat="server" CssClass="validate[required] TextBox" Width="350px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                Org. Address :  
                                            </div>
                                            <div style="text-align: left; width: 29%; float: left;">
                                                <asp:TextBox ID="txtOrgAddressEng" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                સંસ્થાનુ સરનામુ :
                                            </div>
                                            <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                                <asp:TextBox ID="txtOrgAddressGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                Designation :<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 29%; float: left;">
                                                <asp:TextBox ID="txtDesignationEng" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                હોદ્દો :
                                            </div>
                                            <div style="text-align: left; width: 29%; float: left;">
                                                <asp:TextBox ID="txtDesignationGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                CTC :<span style="color: red">*</span>
                                            </div>
                                            <div style="text-align: left; width: 79%; float: left;">
                                                <asp:TextBox ID="txtCTC" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" onkeypress="return NumericKeyPressFraction(event)" Width="150px"></asp:TextBox>
                                            </div>


                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                Job Responsibility :  
                                            </div>
                                            <div style="text-align: left; width: 29%; float: left;">
                                                <asp:TextBox ID="txtjobresponsibilityEng" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                કામ ની જવાબદારી :
                                            </div>
                                            <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                                <asp:TextBox ID="txtjobresponsibilityGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="width: 100%;" class="divclasswithfloat">
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                Reason Of Leaving :  
                                            </div>
                                            <div style="text-align: left; width: 29%; float: left;">
                                                <asp:TextBox ID="txtLeavingReasonEng" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div style="text-align: left; width: 21%; float: left;" class="label">
                                                છોડવાનુ કારણ :
                                            </div>
                                            <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                                <asp:TextBox ID="txtLeavingReasonGuj" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="width: 100%;" class="divclasswithfloat">
                                        <asp:GridView ID="gvEmpExpirence" runat="server" AutoGenerateColumns="False"
                                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" OnRowCommand="gvEmpExpirence_RowCommand"
                                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                            <Columns>
                                                <%-- <asp:BoundField DataField="OrganisationNameEng" HeaderText="Organisation Name">
                                                    <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="DurationYear" HeaderText="Year">
                                                    <HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DurationMonth" HeaderText="Month">
                                                    <HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                            CommandName="Edit1" CommandArgument='<%# Eval("EmployeeExperienceTID")%>' CssClass="Detach" Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                                            CommandName="Delete1" CommandArgument='<%# Eval("EmployeeExperienceTID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                                    <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                        <div style="text-align: right; width: 10%; float: right;">
                                            <%--<button id="btnCancelExpirence" type="button" class="btn-blue btn-blue-medium Detach">Cancel</button>--%>
                                            <asp:Button runat="server" ID="btnCancelExpirence" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancelExpirence_Click" />
                                        </div>
                                        <div id="divAddEmpExpirence" style="text-align: right; width: 90%; float: right;">
                                            <asp:Button runat="server" ID="btnAddEmpExp" Text="Add" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnAddEmpExp_Click" />
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                    <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                        <button id="btnContinueExpirence" type="button" class="btn-blue-new btn-blue-medium Detach">Continue</button>
                                        <button id="btnBackExpirence" type="button" class="btn-blue-back btn-blue-medium Detach">Back</button>
                                    </div>
                                    <%-- <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                                    </div>--%>
                                </div>
                            </div>

                            <div id="tabs-6" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;" class="divclasswithfloat">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Name : <span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 79%; float: left;">
                                        <asp:TextBox ID="txtEmpFFName" runat="server" CssClass="validate[required,custom[onlyLetterSp]] TextBox" placeholder="Name" Width="350px"></asp:TextBox>

                                    </div>
                                </div>

                                <div style="width: 100%;" class="divclasswithfloat">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Occupation :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtFOccupation" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Organisation :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtForgName" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%;" class="divclasswithfloat">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Highest Qualification :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtFQuali" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Email ID :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtFEmailId" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%;" class="divclasswithfloat">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Phone No :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtFPhoneNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Mobile No :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtFMobileNo" runat="server" CssClass="[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="width: 100%;" class="divclasswithfloat">
                                    <asp:GridView ID="gvEmpFamily" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" OnRowCommand="gvEmpFamily_RowCommand"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="FamilyPersonName" HeaderText="Name">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="100%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                        CommandName="Edit1" CommandArgument='<%# Eval("FamilyPersonTID")%>' CssClass="Detach" Height="20px" Width="20px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                                        CommandName="Delete1" CommandArgument='<%# Eval("FamilyPersonTID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                                <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                    <div style="text-align: right; width: 10%; float: right;">
                                        <%-- <button id="btnCancelFamily" type="button" class="btn-blue btn-blue-medium Detach">Cancel</button>--%>
                                        <asp:Button runat="server" ID="btnCancelFamily" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancelFamily_Click" />
                                    </div>
                                    <div style="text-align: right; width: 90%; float: right;">
                                        <asp:Button runat="server" ID="btnAddFamily" Text="Add" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnAddFamily_Click" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                    <button id="btnContinueFamily" type="button" class="btn-blue-new btn-blue-medium Detach">Continue</button>
                                    <button id="btnBackFamily" type="button" class="btn-blue-back btn-blue-medium Detach">Back</button>
                                </div>

                            </div>

                            <div id="tabs-7" style="padding:0 0 0 10px;" class="gradientBoxesWithOuterShadows">
                                <div style="width: 100%;">
                                    <div id="divGrid" style="width: 500px; background-color: #3b5998; height: 25px; clear: both; color: white;">
                                        <div style="width: 175px; float: left; margin-left: 40px; margin-top: 4px;">
                                            File Name
                                        </div>
                                        <div style="width: 180px; float: left; margin-top: 4px;">
                                            Description
                                        </div>
                                        <div style="width: 50px; float: left; margin-top: 4px;">
                                            Action
                                        </div>
                                    </div>
                                    <div id="uploadedDiv" runat="server" style="width: 500px; clear: both" clientidmode="Static">
                                    </div>
                                    <asp:HiddenField ID="hfEmployeeMID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnLastUserID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnFileFolder" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnCountFiles" runat="server" Value="0" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnUploadFilePath" runat="server" ClientIDMode="Static" />
                                    <div id="divDocument" style="width: 100%; padding-top: 10px; clear: both">
                                        <div style="float: left; padding-top: 5px;">
                                            <label class="file-upload">
                                                <span><strong>Select file</strong></span>
                                                <asp:FileUpload ID="fileToUpload" runat="server" ClientIDMode="Static" onchange="javascript:return ajaxFileUpload();" />
                                            </label>
                                        </div>
                                        <div style="width: 100%">
                                        </div>
                                        <div style="width: 100%; float: left; padding-left: 10px">
                                            <span style="padding-left: 10px">
                                                <img id="loading" src="images/loading.gif" style="display: none;"></span>
                                        </div>
                                    </div>

                                    <div style="width: 100%; margin-top: 10px; height: 30px;">
                                        <div style="text-align: left; width: 20%; float: left;"
                                            class="label">
                                        </div>
                                        <div style="text-align: right; width: 80%; float: right;">
                                        </div>
                                    </div>
                                    <div style="width: 100%; margin-top: 10px; height: 30px;">
                                        <button id="btnBackDocument" type="button" class="btn-blue-back btn-blue-medium Detach">Back</button>
                                    </div>
                                    <%-- <div style="width: 100%;" class="divclasswithfloat">
                                       
                                    </div>--%>
                                </div>
                            </div>

                            <div id="divModal" runat="server" visible="false">
                                <div>
                                    <table width="500px" style="height: auto; background-color: #FFFFFF; border: outset 2px White;"
                                        align="center" cellpadding="3px">
                                        <tr>
                                            <td align="left" valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" class="pageTitle">Click Employee&#39;s Photo
                                                            <asp:LinkButton CausesValidation="false" ID="lnkCancelPhoto" runat="server" class="btn-blue btn-blue-medium btn-right"
                                                                OnClick="lnkCancelPhoto_Click">Cancel</asp:LinkButton>
                                                            <asp:LinkButton CausesValidation="false" ID="btnOK" runat="server" class="btn-blue btn-blue-medium btn-right"
                                                                OnClick="btnOK_Click">OK</asp:LinkButton>



                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top">
                                                <object width="410" height="190">
                                                    <param name="movie" value="~/WebcamResources/save_picture.swf">
                                                    <embed id="embm" runat="server" src="~/WebcamResources/save_picture.swf" width="410" height="190" />
                                                </object>
                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DropShadow="false"
                                                    Drag="True" Enabled="true" PopupControlID="divModal" TargetControlID="btnClickPhoto"
                                                    BackgroundCssClass="RadWModalImage">
                                                </ajaxToolkit:ModalPopupExtender>
                                            </td>
                                        </tr>
                                    </table>
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
            //var totalAmount = 0, TotalDiscount = 0, Total = 0, TotalAmountnotCheck = 0;
            $('.Detach').click(function () {
                $("#aspnetForm").validationEngine('detach');
       });
   
        function UploadFileNow() {

            var value = $(document.getElementById('<%= fuImage.ClientID %>')).val();
            if (value != '') {
                $("#aspnetForm").submit();
            }

        };

        $("#btnNextPersonal").click(function () {

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
            // alert(valid);

            if (valid == true) {
                // alert("true")

                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });
            }
        });

        $("#btnForward").click(function () {

            $(document.getElementById('<%= txtPermenantAddress.ClientID %>')).val($(document.getElementById('<%= txtCurAddress.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantState.ClientID %>')).val($(document.getElementById('<%= txtCurState.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantCity.ClientID %>')).val($(document.getElementById('<%= txtCurCity.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantLandMark.ClientID %>')).val($(document.getElementById('<%= txtCurLandMark.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantPinCode.ClientID %>')).val($(document.getElementById('<%= txtCurPinCode.ClientID %>')).val());
        });

        $(function () {
            $("table[id*=rblGender]").validationEngine('attach', { promptPosition: "bottomRight" });
            $("table[id*=rblGender] input").addClass("validate[required]");
            $("[id*=btnNextPersonal]").bind("click", function () {
                if (!$("table[id*=rblGender]").validationEngine('validate')) {
                    return false;
                }
                return true;
            });
        });

        $("#SchoolReplacementInfo").hide();
        $('#<%=rblAppointmentType.ClientID%>').click(function () {
            $('#<%=rblAppointmentType.ClientID%> input:radio:checked').each(function () {
                if (($(this).val()) == "0") {
                    $("#SchoolReplacementInfo").hide();
                }
                else {
                    $("#SchoolReplacementInfo").show();
                }
            });
        });
        function SchoolInfoShow() {
            $("#SchoolReplacementInfo").show();
        }
        function SchoolInfoSHide() {
            $("#SchoolReplacementHide").show();
        }


        $(document.getElementById('<%= chkIsUser.ClientID %>')).click(function () {
            if ($(document.getElementById('<%= chkIsUser.ClientID %>')).prop("checked")) {
                $('#divUser').show();
            }
            else {
                $("#divUser").hide();
            }
        });
        function divResignHide() {
            $("#divResign").hide();
            $("#divResignGuj").hide();
        }
        function divResignShow() {
            $("#divResign").show();
            $("#divResignGuj").show();
        }
        $("#divResign").hide();
        $("#divResignGuj").hide();

        $(document.getElementById('<%= chkIsResign.ClientID %>')).click(function () {
            if ($(document.getElementById('<%= chkIsResign.ClientID %>')).prop("checked")) {
                $('#divResign').show();
                $("#divResignGuj").show();
            }
            else {
                $("#divResign").hide();
                $("#divResignGuj").hide();
            }
        });

        $("#btnNextSchoolDetail").click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4, 5, 6] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });
            }
        });
        $("#btnBackSchoolDetail").click(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4, 5, 6] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });
        });

        $("#btnForwardGuj").click(function () {
            $(document.getElementById('<%= txtPermenantAddressGuj.ClientID %>')).val($(document.getElementById('<%= txtCorrespondenceAddressGuj.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantLandMarkGuj.ClientID %>')).val($(document.getElementById('<%= txtCurLandMarkGuj.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantStateGuj.ClientID %>')).val($(document.getElementById('<%= txtCorrespondenceStateGuj.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantCityGuj.ClientID %>')).val($(document.getElementById('<%= txtCorrespondenceCityGuj.ClientID %>')).val());
        });
        $("#btnBackGujarati").click(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4, 5, 6] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });
        });

        $("#btnBackQualification").click(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);
             $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4, 5, 6] });
             $(document.getElementById('<%= tabs.ClientID  %>')).tabs({ active: 2 });
        });

        $("#btnContinueQualification").click(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 5, 6] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });
        });
        $("#divEmpExpirence").hide();
        $("#divAddEmpExpirence").hide();
        
        $(document.getElementById('<%= ddlDurationYear.ClientID %>')).change(function () {
            if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() != "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() != "0") {
                $("#divEmpExpirence").show();
                $("#divAddEmpExpirence").show();
            }
            else if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() != "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() == "0") {
                $("#divEmpExpirence").show();
                $("#divAddEmpExpirence").show();
            }
            else if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() == "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() != "0") {
                $("#divEmpExpirence").show();
                $("#divAddEmpExpirence").show();
            }
            else {
                $("#divEmpExpirence").hide();
                $("#divAddEmpExpirence").hide();
            }
        });
    $(document.getElementById('<%= ddlMonth.ClientID %>')).change(function () {
            if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() != "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() != "0") {
                $("#divEmpExpirence").show();
                $("#divAddEmpExpirence").show();
        }
        else if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() != "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() == "0") {
                $("#divEmpExpirence").show();
                $("#divAddEmpExpirence").show();
            }
            else if ($('#<%= ddlDurationYear.ClientID %> option:selected').text() == "0" && $('#<%= ddlMonth.ClientID %> option:selected').text() != "0") {
                $("#divEmpExpirence").show();
                $("#divAddEmpExpirence").show();
        }
        else {
                $("#divEmpExpirence").hide();
                $("#divAddEmpExpirence").hide();
        }
    });
$("#btnContinueExpirence").click(function () {

    $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 5);
    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 4, 6] });
    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 5 });
});
$("#btnContinueFamily").click(function () {

    $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 6);
    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 4, 5] });
    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 6 });
});
$(document.getElementById('<%= btnUploadDocument.ClientID %>')).click(function () {
            $(document.getElementById('<%= btnUploadDocument.ClientID %>')).hide();
    $(document.getElementById('<%= btnGoToEmployee.ClientID %>')).show();

});
$(document.getElementById('<%= btnGoToEmployee.ClientID %>')).click(function () {
            $(document.getElementById('<%= btnGoToEmployee.ClientID %>')).hide();
    $(document.getElementById('<%= btnUploadDocument.ClientID %>')).show();

});

function checkFileExtension(file) {
    var flag = true;
    var extension = file.substr((file.lastIndexOf('.') + 1));

    switch (extension) {
        case 'jpg':
        case 'jpeg':
        case 'png':
        case 'gif':
        case 'zip':
        case 'rar':
        case 'pdf':
        case 'doc':
        case 'docx':
        case 'txt':

        case 'JPG':
        case 'JPEG':
        case 'PNG':
        case 'GIF':
        case 'ZIP':
        case 'RAR':
        case 'PDF':
        case 'DOC':
        case 'DOCX':
        case 'TXT':
            flag = true;
            break;
        default:
            flag = false;
    }

    return flag;
}

//get file path from client system
function getNameFromPath(strFilepath) {

    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }

}
// Asynchronous file upload process
function ajaxFileUpload() {
    // alert("ajaxFileUpload");
    var FileFolder = $('#hdnFileFolder').val();
    var fileToUpload = getNameFromPath($('#fileToUpload').val());
    var filename = fileToUpload.substr(0, (fileToUpload.lastIndexOf('.')));
    if (checkFileExtension(fileToUpload)) {

        var flag = true;
        var counter = $('#hdnCountFiles').val();

        if (filename != "" && filename != null && FileFolder != "0") {
            //Check duplicate file entry
            for (var i = 1; i <= counter; i++) {
                var hdnDocId = "#hdnDocId_" + i;

                if ($(hdnDocId).length > 0) {
                    var mFileName = "#lblfilename_" + i;
                    if ($(mFileName).html() == filename) {
                        flag = false;
                        break;
                    }

                }
            }
            if (flag == true) {
                $("#loading").ajaxStart(function () {
                    $(this).show();
                }).ajaxComplete(function () {
                    $(this).hide();
                    return false;
                });
                var StudentMID = 0;
                var SchoolID = 0;
                var TrustID = 0;
                var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
                var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();

                //alert(EmployeeMID);

                $.ajaxFileUpload({

                    url: 'FileUpload.ashx?id=' + FileFolder + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&StudentMID=" + StudentMID + "&UserID=" + UserID + "&EmployeeMID=" + EmployeeMID,
                    secureuri: false,
                    fileElementId: 'fileToUpload',
                    dataType: 'json',
                    success: function (data, status) {

                        if (typeof (data.error) != 'undefined') {
                            //alert("undefined");
                            if (data.error != '') {
                            } else {
                                ShowUploadedFiles(data.upfile, filename, data.DocMID);
                                $('#fileToUpload').val("");
                            }
                        }
                    },
                    error: function (data, status, e) {
                        // alert(e);
                    }
                });
            }
            else {
                alert('file ' + filename + ' already exist')
                return false;
            }
        }
    }
    else {
        alert('You can upload only jpg,jpeg,pdf,doc,docx,txt,zip,rar extensions files.');
    }
    return false;

}
//show uploaded file 
function ShowUploadedFiles(file, fileName, hdnid) {
    count = hdnid;
    var hdnid = 'hdnDocId_' + count;
    //var hdnid = 'hdnDocId_' + $("#hdnCountFiles").val();
    var SchoolID = 0;
    var TrustID = 0;
    var StudentMID = 0;
    var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();
    var txtDocDescId = 'txtDocDesc_' + count;
    var lblfilename = 'lblfilename_' + count;
    var path = $('#hdnUploadFilePath').val();
    //alert("uploadedDiv");
    $("#uploadedDiv").append("<div id='" + hdnid + "' style='clear:both; background-color:#d2e9ff; padding-top:5px; height:25px; width:500px'><span id='" + lblfilename + "' style='width:175px;float:left;margin-left:40px;overflow:hidden;'>" + fileName +
        "</span><span style='width:170px;float:left;margin-left:0px;'><input type='text' id='" + txtDocDescId + "' value='" + fileName +
        "' /><input name='" + hdnid + "' id='" + hdnid + "' value='" + count + "' type='hidden'></span><span style='float:left; margin-left:10px; width:40px;' >" +
        "<a href='#' class='dellink' onclick='deleterow(\"#" + hdnid + "," + file + "\")'>Delete</a></span>" + // for deleting file
        "<span style='float:left; margin-left:10px; width:40px;' ><a class='dellink' href='FileUpload.ashx?filepath=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&StudentMID=" + StudentMID + "&EmployeeMID=" + EmployeeMID + "' >View</a></span></div>" // for downloading file
        );
    //update file counter
    $("#hdnCountFiles").val(count);
    return false;

}
// delete existing file
function deleterow(divrow) {
    var SchoolID = 0;
    var StudentMID = 0;
    var TrustID = 0;
    var EmployeeMID = $('#<%=hfEmployeeMID.ClientID%>').val();
    var str = divrow.split(",");
    var row = str[0];
    var file = str[1];
    var docID = row.substr(10, str[0].length);

    var path = $('#hdnUploadFilePath').val();
    if (confirm('Are you sure to delete?')) {

        $.ajax({
            url: "FileUpload.ashx?path=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&DocMID=" + docID + "&StudentMID=" + StudentMID + "&EmployeeMID=" + EmployeeMID,
            type: "GET",
            cache: false,
            async: true,
            success: function (html) {

            }
        });
        $(row).remove();
    }
    return false;
}

$(document.getElementById('<%= btnCancelQualification.ClientID %>')).click(function () {
            $('#tabs-4 .TextBox').val('');
        });
        $(document.getElementById('<%= btnCancelExpirence.ClientID %>')).click(function () {
            $('#tabs-5 .TextBox').val('');
        });
        $(document.getElementById('<%= btnCancelFamily.ClientID %>')).click(function () {
            $('#tabs-6 .TextBox').val('');
        });



        $("#btnBackExpirence").click(function () {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4, 5, 6] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });
        });
        $("#btnBackFamily").click(function () {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 5, 6] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });
        });
        $("#btnBackDocument").click(function () {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 5);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3, 4, 6] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 5 });
        });

        function viewForTrust() {
            // alert("divSchoolDetailHide");
            $("#divSchoolDetail").hide();
        }
        function viewForSchool() {
            //alert("divSchoolDetailShow");
            $("#divSchoolDetail").show();
        }

        function UpdateModeIsUserHide() {
            // alert("UpdateModeIsUserHide");
            $("#divUser").hide();
        }
        function UpdateModeIsUserShow() {
            // alert("UpdateModeIsUserShow");
            $("#divUser").show();
        }
    </script>

</asp:Content>


