<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentDetailMaster.aspx.cs" Inherits="GEIMS.Client.UI.StudentDetailMaster" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/ModalPopupWindow.js"></script>
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script src="../JS/AjaxFileupload.js"></script>
    <link href="../CSS/Site.css" rel="stylesheet" />
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <script type="text/javascript">
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
                        url: "StudentDetailMaster.aspx/GetAllStudentNameForReport",
                        data: "{'prefixText':'" + request.term + "','SearchType':'" + $(document.getElementById('<%= ddlSearchBy.ClientID %>')).val() + "','SchoolMID':'" +<%=Session[ApplicationSession.SCHOOLID] %> + "'}",
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
                    // $("#<%=hfSearchName.ClientID %>").val(i.item.val);
                    $("#<%=hfSearchName.ClientID %>").val(i.item.label);
                }
            });


            $("#btnUploadDocument").hide();
            $("#btnStudentDetail").hide();
            var tab = $(document.getElementById('<%= hfTab.ClientID %>')).val();

            if (tab == "0") {
                $("#btnUploadDocument").hide();
                $("#btnStudentDetail").hide();
                $("#divGridPanel").show();

                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });
                $("#tabs").hide();
                $(document.getElementById('<%= ddlSection.ClientID %>')).empty();
                $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
                $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
                $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).empty();
                $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).empty();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "StudentDetailMaster.aspx/LoadSection",
                    data: "{'intSchoolID':'" + <%=Session["SchoolID"] %> + "'}",
                    dataType: "json",
                    success: function (data) {
                        var count = -1;
                        var temp = $.parseJSON(data.d);

                        var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml1);

                        //var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

                        //var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);

                        var optionhtml2 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).append(optionhtml2);

                        //var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                        $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).append(optionhtml2);
                        var SectionID = $('#<%=hdnClassID.ClientID%>').val();

                        $.each(temp, function (i) {
                            //alert("Success");
                            count = i;
                            var optionhtml = '<option value="' +
                                temp[i].SectionMID + '">' + temp[i].SectionName + '</option>';
                            $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml);
                        });
                        if (count == "-1") {
                            alert("Section is not Created");
                        }
                    },
                    error: function (error) {
                    }

                });
            }
            else if (tab == "4") {
                //alert("4");
                $("#btnUploadDocument").hide();
                $("#btnStudentDetail").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });

                var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
                //alert(StudentID);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "StudentDetailMaster.aspx/LoadDocument",
                    data: "{'intSchoolID':'" + 0 + "','intTrustMID':'" + 0 + "','intStudentMID':'" + StudentID + "',,'intEmployeeMID':'" + 0 + "'}",
                    dataType: "json",
                    success: function (data) {
                        //  alert("Success");
                        var temp = $.parseJSON(data.d);

                        // alert(temp[1].DocumentMID);

                        $.each(temp, function (i) {

                            // count = parseInt(temp[i].DocumentMID) + 1;
                            var hdnid = temp[i].DocumentMID;
                            var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
                            var SchoolID = 0;
                            var TrustID = 0;
                            var EmployeeMID = 0;
                            var txtDocDescId = 'txtDocDesc_' + hdnid;
                            //  alert(txtDocDescId);
                            var lblfilename = temp[i].DocumentName + hdnid;
                            var path = temp[i].DocumentPath;
                            var file = temp[i].DocumentName;
                            // alert(file);
                            var fileName = file.substr(0, (file.lastIndexOf('.')));

                            // alert("in");
                            ShowUploadedFiles(file, fileName, hdnid);
                        });
                    },

                    error: function (error) {
                        // alert("Error" + error);
                    }

                });
            }
            else if (tab == "1") {
                $("#btnUploadDocument").show();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });

                var SchoolID = 0;
                var TrustID = 0;
                var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
                var UserID = $('#<%=hdnLastUserID.ClientID%>').val();

            }
            else if (tab == "5") {
                //alert("4");
                $("#btnUploadDocument").hide();
                $("#btnStudentDetail").hide();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });

                var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
                //alert(StudentID);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "StudentDetailMaster.aspx/LoadDocument",
                    data: "{'intSchoolID':'" + 0 + "','intTrustMID':'" + 0 + "','intStudentMID':'" + StudentID + "',,'intEmployeeMID':'" + 0 + "'}",
                    dataType: "json",
                    success: function (data) {
                        //  alert("Success");
                        var temp = $.parseJSON(data.d);

                        // alert(temp[1].DocumentMID);

                        $.each(temp, function (i) {

                            // count = parseInt(temp[i].DocumentMID) + 1;
                            var hdnid = temp[i].DocumentMID;
                            var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
                            var SchoolID = 0;
                            var TrustID = 0;
                            var EmployeeMID = 0;
                            var txtDocDescId = 'txtDocDesc_' + hdnid;
                            //  alert(txtDocDescId);
                            var lblfilename = temp[i].DocumentName + hdnid;
                            var path = temp[i].DocumentPath;
                            var file = temp[i].DocumentName;
                            // alert(file);
                            var fileName = file.substr(0, (file.lastIndexOf('.')));

                            // alert("in");
                            ShowUploadedFiles(file, fileName, hdnid);
                        });
                    },

                    error: function (error) {
                        // alert("Error" + error);
                    }

                });
            }
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

                        var SchoolID = 0;
                        var TrustID = 0;
                        var EmployeeMID = 0;
                        var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
                        var StudentMID = $('#<%=hdnStudentID.ClientID%>').val();
                        //  alert(StudentMID);

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
                        alert('file ' + filename + ' already exist');
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
            var EmployeeMID = 0;
            var StudentMID = $('#<%=hdnStudentID.ClientID%>').val();
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
            var EmployeeMID = 0;
            var TrustID = 0;
            var StudentMID = $('#<%=hdnStudentID.ClientID%>').val();
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
            Student Master
             <button id="btnStudentDetail" type="button" class="btn-blue btn-blue-medium" style="height: 28px">Go To Student Detail</button>
            <button id="btnUploadDocument" type="button" class="btn-blue btn-blue-medium" style="height: 28px">Go To Upload Document</button>&nbsp;&nbsp;
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewStudent" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnAddNewStudent_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnViewList_Click">View List</asp:LinkButton>
            <%--<button id="lnkViewList" type="button" class="btn-blue btn-blue-medium">View List</button>--%>&nbsp;&nbsp;
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; padding-bottom: 20px; padding-right: 10px; height: 100%;">
                <%-- <div style="text-align: center; width: 100%;">
                    <asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>
                </div>--%>
                <div id="divGridPanel" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <asp:Panel ID="GridPanel" runat="server" Font-Names="Verdana" Font-Size="11px"
                        GroupingText="Search Student">
                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server">
                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                            <asp:ListItem Value="1">Student Name</asp:ListItem>
                            <asp:ListItem Value="2">Student GR NO</asp:ListItem>
                            <asp:ListItem Value="3">Student Form No</asp:ListItem>
                            <asp:ListItem Value="4">Student Unique ID</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtSearchName" runat="server" CssClass="textarea autosuggest"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:HiddenField runat="server" ID="hfSearchName" />
                        <asp:Button ID="btnGo" runat="server" CssClass="btn-blue Detach" Width="50px" Text="Go" CausesValidation="false"
                            OnClick="btnGo_Click" />
                        <br />
                        <br />
                        <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                            BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                            Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvStudent_RowCommand">
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                            <Columns>
                                <%-- <asp:BoundField DataField="StudentTID" HeaderText="Student ID">
								<HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden"/>
								<ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
							</asp:BoundField>--%>
                                <%-- <asp:BoundField DataField="StudentFirstNameEng" HeaderText="Student Name">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Name">
                                    <HeaderStyle Width="60%" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentName" Text='<%#Eval("StudentFirstNameEng") + " " + Eval("StudentLastNameEng") %> ' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="60%" VerticalAlign="Top" Wrap="true"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CurrentGrNo" HeaderText="GR No">
                                    <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AdmissionNo" HeaderText="Form No">
                                    <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/Edit.png"
                                            CommandName="Edit1" CommandArgument='<%# Eval("StudentMID")%>' Height="20px" Width="20px" />
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

                    </asp:Panel>
                </div>
                <div id="divStudent" runat="server">
                    <div id="tabs" runat="server" class="style-tabs" style="width: 100%;">
                        <ul>

                            <li><a id="tabPersonalDetails" href="#tabs-1">Personal Details</a></li>
                            <li><a id="tabStudentDetails" href="#tabs-2">Student Details</a></li>
                            <li><a id="tabGujaratiDetails" href="#tabs-3">Details in Gujarati</a></li>
                            <li><a id="tabParentDetails" href="#tabs-4">Parent Details</a></li>
                            <li><a id="tabLeft Detail" href="#tabs-5">Upload Document</a></li>

                        </ul>
                        <div id="tabs-1" style="height:1121px;padding:5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">  <%--Height 995px--%>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Student Name : <span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtStudentFirstName" runat="server" CssClass="validate[required] TextBox" placeholder="First Name" Width="150px"></asp:TextBox>
                                    &nbsp;
								<asp:TextBox ID="txtStudentMiddleName" runat="server" CssClass=" TextBox" placeholder="Middle Name" Width="150px"></asp:TextBox>
                                    &nbsp;
								<asp:TextBox ID="txtStudentLastName" runat="server" CssClass="validate[required] TextBox" placeholder="Last Name" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Father's Name : 
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtFatherFirstName" runat="server" placeholder="First Name" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    &nbsp;
									<asp:TextBox ID="txtFatherMiddleName" runat="server" placeholder="Middle Name" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    &nbsp;
									<asp:TextBox ID="txtFatherLastName" runat="server" placeholder="Last Name" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Mother's Name : 
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtMotherFirstName" placeholder="First Name" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                    &nbsp;
									<asp:TextBox ID="txtMotherMiddleName" placeholder="Middle Name" runat="server" CssClass="va TextBox" Width="150px"></asp:TextBox>
                                    &nbsp;
									<asp:TextBox ID="txtMotherLastName" placeholder="Last Name" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Gaurdian's Name :
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtGardianFirstName" placeholder="First Name" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    &nbsp;
									<asp:TextBox ID="txtGardianMiddleName" placeholder="Middle Name" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    &nbsp;
									<asp:TextBox ID="txtGardianLastName" placeholder="Last Name" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Upload Photo :
                                </div>
                                <div style="text-align: left; width: 81%; float: right;">
                                    <asp:FileUpload ID="fuImage"  runat="server" CssClass="TextBox Detach" Height="25px" onchange="UploadFileNow()" />
                                </div>
                            </div>
                            <div style="width: 100%; height: 120px;">
                                <div style="margin-top: 10px; width: 70%; float: left">
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 27%; float: left;" class="label">
                                            Gender:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 73%; float: left;">
                                            <%--<asp:RadioButton runat="server" CssClass="validate[required]" ValidationGroup="Gender" Text="Male" ID="rdMale" />
										<asp:RadioButton runat="server" CssClass="validate[required]" ValidationGroup="Gender" Text="Female" ID="rdFemale" />--%>
                                            <asp:RadioButtonList ID="rblGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblGender_SelectedIndexChanged">
                                                <asp:ListItem Value="MALE">Male</asp:ListItem>
                                                <asp:ListItem Value="FEMALE">Female</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 27%; float: left;" class="label">
                                            Date Of Birth:
                                        </div>
                                        <div style="text-align: left; width: 73%; float: right; vertical-align: top;">
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateOfBirth" TargetControlID="txtDateOfBirth">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 27%; float: left;" class="label">
                                            Birth District:  
                                        </div>
                                        <div style="text-align: left; width: 73%; float: left;">
                                            <asp:TextBox ID="txtBirthDistrict" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 27%; float: left;" class="label">
                                            Nationality:
                                        </div>
                                        <div style="text-align: left; width: 73%; float: right; vertical-align: top;">
                                            <asp:TextBox ID="txtNationality" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                       <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 27%; float: left;" class="label">
                                            Mother Tongue:
                                        </div>
                                        <div style="text-align: left; width: 73%; float: right; vertical-align: top;">
                                            <asp:TextBox ID="txtMotherTongue" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                       <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 27%; float: left;" class="label">
                                           Physical Identification:
                                        </div>
                                        <div style="text-align: left; width: 73%; float: right; vertical-align: top;">
                                            <asp:TextBox ID="txtphysicalIdentification" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                     
                                </div>

                                <div style="width: 30%; height: 190px; float: left; text-align: left;">
                                    <asp:Image ImageUrl="~/Images/NoImage-big.jpg" runat="server" ID="imgphoto"
                                        Width="60%" Height="120px" />
                                    <div style="width: 100%; float: left; padding-left: 20px" class="divclasswithfloat">
                                        <asp:Button runat="server" ID="btnShowModal" Text="Take Photo" OnClick="btnClickPhoto_Click"
                                            CausesValidation="false" CssClass="btn-blue-new btn-blue-medium" Visible="false" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnClickPhoto" runat="server" Text="Done" CssClass="btn-blue-new btn-blue-medium Detach"
                                                                    Width="50" Style="display: none" />
                                    </div>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Sub Category:  
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">

                                    <asp:CheckBoxList ID="chkSubCategory" CssClass="CheckBoxList" runat="server" RepeatDirection="Horizontal" Font-Bold="false">
                                        <asp:ListItem Value="Deaf">Deaf</asp:ListItem>
                                        <asp:ListItem Value="Blind">Blind</asp:ListItem>
                                        <asp:ListItem Value="Physically Challenged">Physically Challenged</asp:ListItem>
                                        <asp:ListItem Value="OverSease">OverSease</asp:ListItem>
                                        <asp:ListItem Value="Other Defects">Other Defects</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Sub Category: <span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:RadioButtonList ID="rblCategory" CssClass="CheckBoxList" runat="server" RepeatDirection="Horizontal" Font-Bold="false">
                                        <asp:ListItem Value="Open">Open</asp:ListItem>
                                        <asp:ListItem Value="OBC">OBC</asp:ListItem>
                                        <asp:ListItem Value="SC">SC</asp:ListItem>
                                        <asp:ListItem Value="ST">ST</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="divPercentage" style="height: 30px; float: left; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Handicapped Percentage(In %):  
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtHandicapePercentage" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
                                </div>

                            </div>
                            <div id="divDefects" style="height: 30px; float: left; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Other Defects:  
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtOtherDefects" runat="server" CssClass="TextBox" Width="50%" Height="100%"></asp:TextBox>
                                </div>

                            </div>
                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Religion:  
                                </div>
                                <div style="text-align: left; width: 31%; float: left;">
                                    <asp:TextBox ID="txtReligion" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Caste:
                                </div>
                                <div style="text-align: left; width: 31%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtCaste" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Sub Caste:  
                                </div>
                                <div style="text-align: left; width: 31%; float: left;">
                                    <asp:TextBox ID="txtSubcasteEng" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                 Blood Group :
                                </div>
                                <div style="text-align: left; width: 31%; float: right;">
                                    <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>


                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Height(In cms):  
                                </div>
                                <div style="text-align: left; width: 31%; float: left;">
                                    <asp:TextBox ID="txthight" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Weight(In Kgs):
                                </div>
                                <div style="text-align: left; width: 31%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtweight" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Hobbies:  
                                </div>
                                <div style="text-align: left; width: 31%; float: left;">
                                    <asp:TextBox ID="txtHobbies" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                 <div style="text-align: left; width: 19%; float: left;" class="label">
                                 Previous School Details:
                                </div>
                                <div style="text-align: left; width: 31%; float: right;vertical-align: top;"">
                                    <asp:TextBox ID="txtPreviousSchoolDetail" runat="server" CssClass="SmallTextArea" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                           
                            <div style="height: 235px; margin-top: 44px; float: left; width: 100%;">
                                <div style="text-align: left; width: 47%; float: left;" class="label">
                                    <asp:Panel ID="pnlPresentAddress" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        GroupingText="Correspondence Address">
                                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Address :
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurAddress" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                City/Town :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurCity" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                State :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurState" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Pin Code :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurPinCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Contact No :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurContactNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Mobile No :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurMobileNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                EmailID :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCurEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                                <div style="height: 235px; vertical-align: middle; width: 4%; float: left; padding-top: 150px">
                                    <button id="btnForward" type="button" class="button" style="width: 30px">>></button>
                                    <%--<asp:Button ID="btnForward" CssClass="button" Text=">>" Font-Bold="true" runat="server"
									Width="30px" OnClick="btnForward_Click" />--%>
                                </div>
                                <div style="text-align: left; width: 47%; height: 235px; float: left;" class="label">
                                    <asp:Panel ID="pnlPermenantAddress" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        GroupingText="Permenant Address">

                                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Address :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantAddress" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                City/Town :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantCity" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                State :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantState" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Pin Code :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantPinCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Contact No :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantContactNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--<div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                Mobile No :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantMobileNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                EmailID :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>


                            </div>
                            <div style="width: 100%; height: 30px;">

                                <div style="text-align: left; width: 20%; float: left; margin-top: 108px;" class="label">
                                </div>
                                <div style="text-align: left; width: 80%; float: right; margin-top: 108px;">

                                    <button id="btnNextAddress" type="button" class="btn-blue btn-blue-medium Attach">Next</button>

                                </div>

                            </div>


                        </div>

                        <div id="tabs-2" style="height:693px; padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%">

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Section Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 79%; float: right;">
                                        <asp:DropDownList runat="server" CssClass="CSS_BindSection validate[required] TextBox" ID="ddlSection" Width="27%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; margin-top: 10px;">
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Gov. UniqueID :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtGovUniqueID" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                      <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Gov. UniqueNo :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtGovUniqueNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                  
                                </div>
                                 <div style="height: 30px; margin-top: 10px; width: 100%;">
                                       <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Bank Account :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtBankAccount" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                     IFSC Code:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right;">
                                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                   <div style="height: 30px; margin-top: 10px; width: 100%;">
                                       
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                     Branch Name:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                   A/C No. :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right;">
                                        <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                             
                                </div>
                                  <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                         Roll Number :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtRollNumber" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                         </div>  
                                      <div style="text-align: left; width: 21%; float: left;" class="label">
                                  Aadhar Card No. :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtAadharCardNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                              
                                </div>
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Form No:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtCurAdmissionNo" runat="server" CssClass="validate[groupRequired[UniqueNo]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Current GR NO :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtCurGrNo" runat="server" CssClass="validate[groupRequired[UniqueNo]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Current Class :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlClass" Width="100px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Current Division :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlDivision" Width="100px">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Registration Date:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtCurAdmissionDate" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtCurAdmissionDate" TargetControlID="txtCurAdmissionDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Registration Year:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:DropDownList ID="ddlRegisterdYear" runat="server" CssClass="validate[required] TextBox" Width="100px">
                                            <%--<asp:ListItem>2014</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Admitted GR NO :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtAdmittedGr" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Academic Year: <span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:DropDownList ID="ddlCurAdmissionYear" runat="server" CssClass="validate[required] TextBox" Width="100px">
                                            <%--<asp:ListItem>2014</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Admitted Class :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:DropDownList runat="server" CssClass="TextBox" ID="ddlAdmittedClass" Width="100px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Admitted Division :
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:DropDownList runat="server" CssClass="TextBox" ID="ddlAdmittedDivision" Width="100px">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left; height: 14px;" class="label">
                                        Admission Date:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtAdmittednDate" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtAdmittednDate" TargetControlID="txtAdmittednDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Admission Year:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left; vertical-align: top;">
                                        <asp:DropDownList ID="ddlAdmittedYear" runat="server" CssClass="validate[required] Droptextarea" Width="100px">
                                            <%--<asp:ListItem>2014</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Status:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">

                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox" Width="100px">
                                            <%--<asp:ListItem>2014</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left; height: 14px;" class="label">
                                        Joining Date:<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtJoiningDate" TargetControlID="txtJoiningDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>

                                          <asp:Panel ID="Panel1" runat="server" Font-Names="Verdana" Font-Size="11px"
                                GroupingText="Mode of Transport">


                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                     Type Of Vehicle :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtTypeOfVehicle" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                     Vehicle No. :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                    Driver Name :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtDriverName" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                      Driver Contact No:
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtDriverContactNo" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>


                                 <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">

                                       
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left; height: 14px;" class="label">
                                        IsLate:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:CheckBox ID="chkIsLate" runat="server" />
                                    </div>
                                </div>
                                <div id="divLeft" style="width: 100%">
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Left Date :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtLeftDate" runat="server" CssClass="validate[required] Droptextarea" Width="150px" ReadOnly="true"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtLeftDate" TargetControlID="txtLeftDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Left Year :
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left; vertical-align: top;">

                                            <asp:DropDownList ID="ddlLeftYear" runat="server" CssClass="TextBox" Width="150px">
                                                <%--<asp:ListItem>2014</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Left Class :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtLeftClass" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>

                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Left Division :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left; vertical-align: top;">

                                            <asp:TextBox ID="txtDivision" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            LC No :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtLcNo" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>

                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            LC Date :
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left; vertical-align: top;">

                                            <asp:TextBox ID="txtLCDate" runat="server" CssClass="TextBox" Width="150px" ReadOnly="true"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtLCDate" TargetControlID="txtLCDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>

                                    </div>

                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            No. OF LC Copy :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtLCCopy" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>

                                        </div>

                                    </div>

                                    <div style="height: 55px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Left Reason :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtLeftReason" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <div style="height: 55px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            LC Remark :
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtLCRemark" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <%--<div style="width: 100%; margin-top: 10px; height: 30px;">
							<div style="text-align: left; width: 20%; float: left;" class="label">
								<button id="btnBackLeft" type="button" class="btn-blue-back btn-blue-medium">Back</button>
							</div>
							<div style="text-align: left; width: 80%; float: right;">
								<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnCancel_Click" />&nbsp;&nbsp;
								<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium" OnClick="btnSave_Click" />
					&nbsp;&nbsp;
								
							</div>
						</div>--%>
                                </div>

                            </div>

                            <%--<div style="width: 100%; float: right; text-align: right; height: 16%;">
							<asp:ImageButton ID="ImgNextStudentDetails" ImageUrl="~/Images/continue-button.gif" ImageAlign="Right" runat="server" Width="120px" Height="35px" />
						</div>--%>
                            <div style="width: 100%; margin-top: 10px; height: 30px;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    <button id="btnBackStudentDetails" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                                </div>
                                <div style="text-align: left; width: 80%; float: right;">
                                    <button id="btnNextStudentDetails" type="button" class="btn-blue btn-blue-medium">Next</button>
                                </div>
                            </div>
                        </div>


                        <div id="tabs-3" style="padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    વિદ્યાર્થી નુ નામ :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 27%; float: left;">
                                    <asp:TextBox ID="txtStudentFirstNameGuj" runat="server" placeholder="નામ" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    <asp:TextBox ID="txtStudentMiddleNameGuj" runat="server" placeholder="પિતા નુ નામ" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 28%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtStudentLastNameGuj" runat="server" placeholder="અટક" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>

                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    પિતા નુ નામ :
                                </div>
                                <div style="text-align: left; width: 27%; float: left;">
                                    <asp:TextBox ID="txtFatherFirstNameGuj" runat="server" placeholder="નામ" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    <asp:TextBox ID="txtFatherMiddleNameGuj" placeholder="પિતા નુ નામ" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 28%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtFatherLastNameGuj" placeholder="અટક" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>

                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    માતા નુ નામ :
                                </div>
                                <div style="text-align: left; width: 27%; float: left;">
                                    <asp:TextBox ID="txtMotherFirstNameGuj" placeholder="નામ" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    <asp:TextBox ID="txtMotherMiddleNameGuj" placeholder="પિતા નુ નામ" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 28%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtMotherLastNameGuj" placeholder="અટક" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>

                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    જાતિ :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 27%; float: left;">
                                    <asp:RadioButtonList ID="rblGenderGuj" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="પુરૂષ">પુરૂષ</asp:ListItem>
                                        <asp:ListItem Value="સ્ત્રી">સ્ત્રી</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    જન્મ સ્થળ :
                                </div>
                                <div style="text-align: left; width: 28%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtBirthDistrictGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>

                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    રાષ્ટ્રીયતા :  
                                </div>
                                <div style="text-align: left; width: 27%; float: left;">
                                    <asp:TextBox ID="txtNationalityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    જ્ઞાતિ:
                                </div>
                                <div style="text-align: left; width: 28%; float: right; vertical-align: top;">
                                    <asp:TextBox ID="txtCasteGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    પેટા જ્ઞાતિ :  
                                </div>
                                <div style="text-align: left; width: 81%; float: left;">
                                    <asp:TextBox ID="txtSubCasteGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </div>

                            </div>
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 19%; float: left;" class="label">
                                    જાતિ : <span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 81%; float: right; vertical-align: top;">
                                    <asp:RadioButtonList ID="rblCategoryGuj" CssClass="CheckBoxList" runat="server" RepeatDirection="Horizontal" Font-Bold="false">
                                        <asp:ListItem Value="સામાન્ય">સામાન્ય</asp:ListItem>
                                        <asp:ListItem Value="બક્ષીપંચ">બક્ષીપંચ</asp:ListItem>
                                        <asp:ListItem Value="SC">અનુસૂચિત જાતિ</asp:ListItem>
                                        <asp:ListItem Value="અનુસૂચિત જન જાતિ">અનુસૂચિત જન જાતિ</asp:ListItem>
                                        <asp:ListItem Value="અન્ય">અન્ય</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div style="height: 180px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 47%; float: left;" class="label">
                                    <asp:Panel ID="pnlCorrespondenceAddressGuj" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        GroupingText="પત્રવ્યવહાર સરનામું">
                                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                સરનામું :
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCorrespondenceAddressGuj" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                શહેર :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCorrespondenceCityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                રાજ્ય :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtCorrespondenceStateGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>



                                    </asp:Panel>
                                </div>
                                <div style="height: 110px; vertical-align: middle; width: 4%; float: left; padding-top: 70px">
                                    <button id="btnForwardGuj" type="button" class="button" style="width: 30px">>></button>
                                    <%--<asp:Button ID="btnForwardGuj" CssClass="button" Text=">>" Font-Bold="true" runat="server"
									Width="30px" />--%>
                                </div>
                                <div style="text-align: left; width: 47%; float: left;" class="label">
                                    <asp:Panel ID="pnlPermenantAddressGuj" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        GroupingText="કાયમી સરનામું">

                                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                સરનામું: 
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantAddressGuj" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                શહેર :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantCityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                                            <div style="text-align: left; width: 40%; float: left;" class="label">
                                                રાજ્ય :  
                                            </div>
                                            <div style="text-align: left; width: 60%; float: left;">
                                                <asp:TextBox ID="txtPermenantStateGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                            </div>
                                        </div>


                                    </asp:Panel>
                                </div>


                            </div>
                            <div style="width: 100%; margin-top: 10px; height: 30px;">
                                <div style="text-align: left; width: 19%; float: left; height: 28px;" class="label">
                                    <button id="btnBackGuj" type="button" class="btn-blue-back btn-blue-medium">Back</button>

                                </div>
                                <div style="text-align: left; width: 81%; float: right;">
                                    <button id="btnNextGuj" type="button" class="btn-blue btn-blue-medium">Next</button>
                                </div>
                            </div>

                        </div>

                        <div id="tabs-4" style="padding:5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                            <asp:Panel ID="pnlFatherDetail" runat="server" Font-Names="Verdana" Font-Size="11px"
                                GroupingText="Father's Detail">


                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                        Occupation :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtFatherOccupation" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                        Qualification :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtFatherQuali" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                        Mobile No :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtFatherMobileNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                        EmailID :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtFatherEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                      Organisation Name :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtFatherOrganisationName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                      Organisation Mobile No:
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtFatherOrganisationContactNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlMotherDetail" runat="server" Font-Names="Verdana" Font-Size="11px"
                                GroupingText="Mother's Detail">

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                        Occupation :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtMotherOccupation" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                        Qualification :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtMotherQuali" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                        Mobile No :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtMotherMobileNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                        EmailID :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtMotherEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlGardianDetail" runat="server" Font-Names="Verdana" Font-Size="11px"
                                GroupingText="Guardian's Detail">

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                        Occupation :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtGardianOccupation" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                        Qualification :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtGardianQuali" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 19%; float: left;" class="label">
                                        Mobile No :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left;">
                                        <asp:TextBox ID="txtGardianMobileNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 26%; float: left;" class="label">
                                        EmailID :
                                    </div>
                                    <div style="text-align: left; width: 27%; float: left; vertical-align: top;">
                                        <asp:TextBox ID="txtGardianEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div style="width: 100%; height: 30px; margin-top: 10px">

                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    <button id="btnBackParentDetail" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                                </div>
                                <div style="text-align: left; width: 80%; float: right;">
                                    <%--<button id="Button1" type="button" class="btn-blue btn-blue-medium">Next</button>--%>
                                    <%--<asp:Button
                                    runat="server" ID="btnCancel" Text="Cancel"
                                    CssClass="btn-blue btn-blue-medium Detach"
                                    OnClientClick="myFunction()" />--%>

                                &nbsp;&nbsp;
                                 <asp:Button runat="server" ID="btnSave" Text="Save"
                                     CssClass="btn-blue btn-blue-medium"
                                     OnClick="btnSave_Click" />

                                    <%--  <button id="btnNextParentDetail" type="button" class="btn-blue btn-blue-medium">Next</button>--%>
                                </div>
                            </div>
                        </div>

                        <div id="tabs-5" style="height: 300px; padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                            <div style="width: 100%;">
                                <%--  <p style="font-size: 15px; margin-bottom: 10px; margin-left: 10px; font-weight: bold">
            Upload Files</p>--%>
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
                                <asp:HiddenField ID="hdnFileFolder" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCountFiles" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnUploadFilePath" runat="server" ClientIDMode="Static" />
                                <%--<asp:HiddenField ID="hdnSchoolID" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnTrustID" runat="server" ClientIDMode="Static" />--%>
                                <asp:HiddenField ID="hdnStudentID" runat="server" ClientIDMode="Static" />
                                <%--<asp:HiddenField ID="hdnSchoolMID" runat="server" ClientIDMode="Static" />--%>
                                <asp:HiddenField ID="hdnClassID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastUserID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ID="hfTab" />

                                <div id="divDocument" style="width: 100%; padding-top: 10px; clear: both">
                                    <div style="float: left; padding-top: 5px;">
                                        <%--Show wrapper on above of fileUpload Control--%>
                                        <label class="file-upload">
                                            <%-- Set Text To be displayed inplace of Browse button--%>
                                            <span><strong>Select file</strong></span>
                                            <%--Make clientID static if you are using Master Page--%>
                                            <asp:FileUpload ID="fileToUpload" runat="server" ClientIDMode="Static" onchange="javascript:return ajaxFileUpload();" />

                                        </label>

                                    </div>
                                    <div style="width: 100%">
                                    </div>
                                    <div style="width: 100%; float: left; padding-left: 10px">
                                        <span style="padding-left: 10px">
                                            <%--Progress bar--%>
                                            <img id="loading" src="images/loading.gif" style="display: none;"></span>
                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%; margin-top: 10px; height: 30px;">
                                <div style="text-align: left; width: 20%; float: left;"
                                    class="label">
                                    <%--<button id="btnBackLeft"
                                    type="button" class="btn-blue-back btn-blue-medium">
                                    Back</button>--%>
                                </div>
                                <div style="text-align: left; width: 80%; float: right;">
                                </div>
                            </div>

                        </div>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DropShadow="false"
                            Drag="True" Enabled="true" PopupControlID="divModal" TargetControlID="btnClickPhoto"
                            BackgroundCssClass="RadWModalImage">
                        </ajaxToolkit:ModalPopupExtender>
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
    </div>
    <script type="text/javascript">

        function UploadFileNow() {
            //alert("Upload");
            var value = $(document.getElementById('<%= fuImage.ClientID %>')).val();
            if (value != '') {
                //alert("Upload1");
                $("#aspnetForm").submit();
            }

        };

        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });

        $("#btnNextStudentDetails").click(function () {

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });
            }
        });

        $("#btnBackStudentDetails").click(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });
        });

        $(function () {
            $("table[id*=rblGender]").validationEngine('attach', { promptPosition: "bottomRight" });
            $("table[id*=rblGender] input").addClass("validate[required]");
            $("[id*=btnNextAddress]").bind("click", function () {
                if (!$("table[id*=rblGender]").validationEngine('validate')) {
                    return false;
                }
                return true;
            });
            $("table[id*=rblCategory]").validationEngine('attach', { promptPosition: "bottomRight" });
            $("table[id*=rblCategory] input").addClass("validate[required]");
            $("[id*=btnNextAddress]").bind("click", function () {
                if (!$("table[id*=rblCategory]").validationEngine('validate')) {
                    return false;
                }
                return true;
            });
            $("table[id*=rblCategoryGuj]").validationEngine('attach', { promptPosition: "bottomRight" });
            $("table[id*=rblCategoryGuj] input").addClass("validate[required]");
            $("[id*=btnNextGuj]").bind("click", function () {
                if (!$("table[id*=rblCategoryGuj]").validationEngine('validate')) {
                    return false;
                }
                return true;
            });
        });

        $("#divPercentage").hide();
        $("#divDefects").hide();

        //alert("CheckBox");

        $('#<%=chkSubCategory.ClientID%>').click(
            function () {

                $("#divPercentage").hide();
                $("#divDefects").hide();
                var i = 0;
                $('#<%=chkSubCategory.ClientID%> input[type=checkbox]:checked').each(function () {

                    if (($(this).next().text()) == "Physically Challenged") {
                        i++;
                        $("#divPercentage").show();
                    }
                    if (($(this).next().text()) == "Other Defects") {
                        i++;
                        $("#divDefects").show();
                    }
                });
                if (i == 1) {
               //     $('#tabs-1').height(1035);
                    $('#tabs-1').height(1162);
                }
                else if (i == 2) {
                    $("#divPercentage").show();
                    $("#divDefects").show();
                //    $('#tabs-1').height(1075);
                    $('#tabs-1').height(1161);
                }
                else if (i == 0) {
                   // $('#tabs-1').height(995);
                    $('#tabs-1').height(1121);
                }
            });

        $("#btnNextAddress").click(function () {

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });
            }
        });
        $("#btnBackParentDetail").click(function () {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });

        });


        $("#btnBackLeft").click(function () {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });

        });
        $("#btnNextGuj").click(function () {

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });
            }
        });
        $("#btnBackGuj").click(function () {



            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });

        });


        $("#btnForward").click(function () {

            $(document.getElementById('<%= txtPermenantAddress.ClientID %>')).val($(document.getElementById('<%= txtCurAddress.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantState.ClientID %>')).val($(document.getElementById('<%= txtCurState.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantCity.ClientID %>')).val($(document.getElementById('<%= txtCurCity.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantPinCode.ClientID %>')).val($(document.getElementById('<%= txtCurPinCode.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantContactNo.ClientID %>')).val($(document.getElementById('<%= txtCurContactNo.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantEmailID.ClientID %>')).val($(document.getElementById('<%= txtCurEmailID.ClientID %>')).val());
        });

        $("#btnForwardGuj").click(function () {

            $(document.getElementById('<%= txtPermenantAddressGuj.ClientID %>')).val($(document.getElementById('<%= txtCorrespondenceAddressGuj.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantStateGuj.ClientID %>')).val($(document.getElementById('<%= txtCorrespondenceStateGuj.ClientID %>')).val());
            $(document.getElementById('<%= txtPermenantCityGuj.ClientID %>')).val($(document.getElementById('<%= txtCorrespondenceCityGuj.ClientID %>')).val());

        });

        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            //$("#btnSave").click(function () {
            //var valid = $("#aspnetForm").validationEngine('validate');
            //var vars = $("#aspnetForm").serialize();

            var tab = $(document.getElementById('<%= hfTab.ClientID %>')).val();
            // alert("Save");
            if (tab == "0") {
                // alert("0");
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });


                }
                else if (tab == "4") {
                    // alert("4");
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3] });
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });


                }
                else if (tab == "1") {
                    $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            }
        });


        $("#divLeft").hide();
        $(document.getElementById('<%= ddlStatus.ClientID %>')).change(function () {
            if ($('#<%= ddlStatus.ClientID %> option:selected').val() == 3) {
                $("#divLeft").show();
              //  $('#tabs-2').height(693);
                $('#tabs-2').height(978);
            }
            else {
                $("#divLeft").hide();
              ///  $('#tabs-2').height(355);
                $('#tabs-2').height(693);

            }
        });


        $(document.getElementById('<%= lnkAddNewStudent.ClientID %>')).click(function () {
            $(document.getElementById('<%= ddlSection.ClientID %>')).empty();
            $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
            $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
            $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).empty();
            $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).empty();
            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentDetailMaster.aspx/LoadSection",
                data: "{'intSchoolID':'" + <%=Session["SchoolID"] %> + "'}",
            dataType: "json",
            success: function (data) {
                var count = -1;
                // alert("ajax");
                var temp = $.parseJSON(data.d);

                var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml1);

                //  var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

                // var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);

                var optionhtml2 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).append(optionhtml2);

                // var optionhtml1 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).append(optionhtml2);

                $.each(temp, function (i) {
                    count = i;
                    var optionhtml = '<option value="' +
                        temp[i].SectionMID + '">' + temp[i].SectionName + '</option>';
                    $(document.getElementById('<%= ddlSection.ClientID %>')).append(optionhtml);
                });
                if (count == "-1") {
                    alert("Section is not Created");
                }
            },
            error: function (error) {
                // alert("Error" + error);
            }

        });
        });
        $(document.getElementById('<%= ddlSection.ClientID %>')).change(function () {
            $(document.getElementById('<%= ddlClass.ClientID %>')).empty();
            $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).empty();
            var optionhtml2 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).append(optionhtml2);

            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml1);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentDetailMaster.aspx/LoadClass",
                data: "{'intSectionID':'" + $(document.getElementById('<%= ddlSection.ClientID %>')).val() + "'" + ",'intSchoolMID':'" +  <%=Session["SchoolID"] %> + "'}",
            dataType: "json",
            success: function (data) {
                var count = -1;
                var temp = $.parseJSON(data.d);
                $.each(temp, function (i) {
                    count = i;
                    var optionhtml = '<option value="' +
                        temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';
                    $(document.getElementById('<%= ddlClass.ClientID %>')).append(optionhtml);

                    });
                if (count == "-1") {
                    alert("Class is not Created");
                }
            },
            error: function (error) {
                //  alert("Error" + error);
            }

        });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentDetailMaster.aspx/LoadAdmittedClass",
                data: "{'intSchoolMID':'" +  <%=Session["SchoolID"] %> + "'}",
            dataType: "json",
            success: function (data) {
                $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).empty();
                    var optionhtml2 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
                    $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).append(optionhtml2);
                    var count = -1;
                    var temp = $.parseJSON(data.d);

                    $.each(temp, function (i) {
                        count = i;
                        var optionhtml = '<option value="' +
                            temp[i].ClassMID + '">' + temp[i].ClassName + '</option>';

                        $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).append(optionhtml);
                    });
                if (count == "-1") {
                    alert("Class is not Created");
                }
            },
            error: function (error) {
                //  alert("Error" + error);
            }

        });
        });



        $(document.getElementById('<%= ddlClass.ClientID %>')).change(function () {

            $(document.getElementById('<%= ddlDivision.ClientID %>')).empty();
            var optionhtml1 = '<option value="">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml1);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentDetailMaster.aspx/LoadDivision",
                data: "{'intClassMID':'" + $('#<%= ddlClass.ClientID %> option:selected').val() + "'" + ",'intSchoolMID':'" + <%=Session["SchoolID"] %> + "'}",
            dataType: "json",
            success: function (data) {
                var temp = $.parseJSON(data.d);
                var count = -1;
                $.each(temp, function (i) {
                    count = i;
                    var optionhtml = '<option value="' +
                        temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                    $(document.getElementById('<%= ddlDivision.ClientID %>')).append(optionhtml);
                    });
                if (count == "-1") {
                    alert("Division is not Created");
                }

            },
            error: function (error) {
                // alert("Error" + error);
            }

        });
        });

        $(document.getElementById('<%= ddlAdmittedClass.ClientID %>')).change(function () {
            $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).empty();
            var optionhtml2 = '<option value="' + -1 + '">' + "-Select-" + '</option>';
            $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).append(optionhtml2);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentDetailMaster.aspx/LoadDivision",
                data: "{'intClassMID':'" + $('#<%= ddlAdmittedClass.ClientID %> option:selected').val() + "'" + ",'intSchoolMID':'" +  <%=Session["SchoolID"] %> + "'}",
                  dataType: "json",
                  success: function (data) {
                      var count = -1;
                      var temp = $.parseJSON(data.d);
                      // alert("Division");
                      $.each(temp, function (i) {
                          count = i;
                          var optionhtml = '<option value="' +
                              temp[i].DivisionTID + '">' + temp[i].DivisionName + '</option>';
                          $(document.getElementById('<%= ddlAdmittedDivision.ClientID %>')).append(optionhtml);
                    });
                      if (count == "-1") {
                          alert("Division is not Created");
                      }
                  },
                  error: function (error) {
                      //alert("Error" + error);
                  }

              });
        });

        $("#btnStudentDetail").click(function () {
            $("#btnStudentDetail").hide();
            $("#btnUploadDocument").show();
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });

        });

        $("#btnUploadDocument").click(function () {
            // alert("hi");
            $("#btnStudentDetail").show();
            $("#btnUploadDocument").hide();
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });

            var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
            //alert(StudentID);
            $("#uploadedDiv").empty();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "StudentDetailMaster.aspx/LoadDocument",
                data: "{'intSchoolID':'" + 0 + "','intTrustMID':'" + 0 + "','intStudentMID':'" + StudentID + "','intEmployeeMID':'" + 0 + "'}",
                dataType: "json",
                success: function (data) {
                    // alert("Success");
                    var temp = $.parseJSON(data.d);

                    //  alert(temp[1].DocumentMID);

                    $.each(temp, function (i) {

                        // count = parseInt(temp[i].DocumentMID) + 1;
                        var hdnid = temp[i].DocumentMID;
                        var StudentID = $('#<%=hdnStudentID.ClientID%>').val();
                        var SchoolID = 0;
                        var TrustID = 0;

                        var txtDocDescId = 'txtDocDesc_' + hdnid;
                        //  alert(txtDocDescId);
                        var lblfilename = temp[i].DocumentName + hdnid;
                        var path = temp[i].DocumentPath;
                        var file = temp[i].DocumentName;
                        // alert(file);
                        var fileName = file.substr(0, (file.lastIndexOf('.')));

                        // alert("in");
                        ShowUploadedFiles(file, fileName, hdnid);
                    });
                },

                error: function (error) {
                    // alert("Error" + error);
                }

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

                            var SchoolID = 0;
                            var TrustID = 0;
                            var EmployeeMID = 0;
                            var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
                            var StudentMID = $('#<%=hdnStudentID.ClientID%>').val();
                            //  alert(StudentMID);

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
                                    alert(e);
                                }
                            });
                        }
                        else {
                            alert('file ' + filename + ' already exist');
                            return false;
                        }
                    }
                }
                else {
                    alert('You can upload only jpg,jpeg,pdf,doc,docx,txt,zip,rar extensions files.');
                }
                return false;

            }
            // alert("hi");
            //show uploaded file 
            function ShowUploadedFiles(file, fileName, hdnid) {
                //   alert("hi");
                //count = parseInt($("#hdnCountFiles").val()) + 1; 
                //hdnid = 1;
                count = hdnid;
                var hdnid = 'hdnDocId_' + count;
                //var hdnid = 'hdnDocId_' + $("#hdnCountFiles").val();
                var SchoolID = 0;
                var TrustID = 0;
                var EmployeeMID = 0;
                var StudentMID = $('#<%=hdnStudentID.ClientID%>').val();
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
                var EmployeeMID = 0;
                var TrustID = 0;
                var StudentMID = $('#<%=hdnStudentID.ClientID%>').val();
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

        });

        $(document.getElementById('<%= lnkViewList.ClientID %>')).click(function () {
            $("#btnUploadDocument").hide();
            $("#btnStudentDetail").hide();
        });

        $(document.getElementById('<%= btnGo.ClientID %>')).click(function () {


        });
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
</asp:Content>
