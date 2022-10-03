<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="DocumentMaster.aspx.cs" Inherits="GEIMS.Client.UI.DocumentMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script src="../JS/AjaxFileupload.js"></script>
    <link href="../CSS/Site.css" rel="stylesheet" />
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <script type="text/javascript">


        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
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
                //  alert("ajaxFileUpload");
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

                            var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val();
                            var TrustID = $('#<%=hdnTrustID.ClientID%>').val();
                            var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
                            var EmployeeMID = 0;


                            $.ajaxFileUpload({

                                url: 'FileUpload.ashx?id=' + FileFolder + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&UserID=" + UserID+"&EmployeeMID="+EmployeeMID,
                                secureuri: false,
                                fileElementId: 'fileToUpload',
                                dataType: 'json',
                                success: function (data, status) {

                                    if (typeof (data.error) != 'undefined') {
                                      //  alert("undefined");
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
                //count = parseInt($("#hdnCountFiles").val()) + 1;         
                count = hdnid;
                var hdnid = 'hdnDocId_' + count;
                //var hdnid = 'hdnDocId_' + $("#hdnCountFiles").val();
                var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val();
                var TrustID = $('#<%=hdnTrustID.ClientID%>').val();
                var EmployeeMID = 0;
                var txtDocDescId = 'txtDocDesc_' + count;
                var lblfilename = 'lblfilename_' + count;
                var path = $('#hdnUploadFilePath').val();
                //alert("uploadedDiv");
                $("#uploadedDiv").append("<div id='" + hdnid + "' style='clear:both; background-color:#d2e9ff; padding-top:5px; height:25px; width:500px'><span id='" + lblfilename + "' style='width:175px;float:left;margin-left:40px;overflow:hidden;'>" + fileName +
                    "</span><span style='width:170px;float:left;margin-left:0px;'><input type='text' id='" + txtDocDescId + "' value='" + fileName +
                    "' /><input name='" + hdnid + "' id='" + hdnid + "' value='" + count + "' type='hidden'></span><span style='float:left; margin-left:10px; width:40px;' >" +
                    "<a href='#' class='dellink' onclick='deleterow(\"#" + hdnid + "," + file + "\")'>Delete</a></span>" + // for deleting file
                    "<span style='float:left; margin-left:10px; width:40px;' ><a class='dellink' href='FileUpload.ashx?filepath=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&EmployeeMID="+0+"' >View</a></span></div>" // for downloading file

                    );
                //update file counter
                $("#hdnCountFiles").val(count);
                return false;

            }
            // delete existing file
            function deleterow(divrow) {
                var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val();

            var TrustID = $('#<%=hdnTrustID.ClientID%>').val();
            var str = divrow.split(",");
            var row = str[0];
            var file = str[1];
            var docID = row.substr(10, str[0].length);

            var path = $('#hdnUploadFilePath').val();
            if (confirm('Are you sure to delete?')) {

                $.ajax({
                    url: "FileUpload.ashx?path=" + path + "&file=" + file + "&SchoolID=" + SchoolID + "&TrustID=" + TrustID + "&DocMID=" + docID+"&EmployeeMID="+0,
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Document Master
            <%--<asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_Click">Add New</asp:LinkButton>
			&nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>
                </div>

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Upload Documents</a></li>

                    </ul>
                    <div id="tabs-1" style="padding:5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <div style="width: 100%;" class="mydiv">
                            <div style="width: 100%">
                                <div style=" width: 100%;" class="divclasswithfloat">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Select :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 79%; float: left;">

                                        <asp:RadioButtonList runat="server" ID="rblSelect" RepeatDirection="Horizontal">
                                            <asp:ListItem>School</asp:ListItem>
                                            <asp:ListItem>Trust</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div id="divSchool" style="width: 100%" class="divclasswithfloat">
                                    <div style=" width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            School Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: right;">
                                            <asp:DropDownList runat="server" CssClass="TextBox" ID="ddlSchoolID" Width="45%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div id="divTrust" style="width: 100%" class="divclasswithfloat">
                                    <div style="width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Trust Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: right;">
                                            <asp:DropDownList runat="server" CssClass="TextBox" ID="ddlTrustID" Width="45%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div>
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
                                <asp:HiddenField ID="hdnSchoolID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTrustID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastUserID" runat="server" ClientIDMode="Static" />

                                <div id="divDocument" style="padding-top: 10px; clear: both">
                                    <div >
                                        <%--Show wrapper on above of fileUpload Control--%>
                                        <label class="file-upload">
                                            <%-- Set Text To be displayed inplace of Browse button--%>
                                            <span><strong>Select file</strong></span>
                                            <%--Make clientID static if you are using Master Page--%>
                                            <asp:FileUpload ID="fileToUpload" runat="server" ClientIDMode="Static" onchange="javascript:return ajaxFileUpload();" />

                                        </label>
                                        <br />
                                        <span class="label">*Select File To Upload</span>
                                        <br />
                                        <span class="label">*Only rar,Image,Word File and .txt File</span>

                                    </div>
                                    <%--  <div style="width: 100%">
                                    <asp:Button runat="server" ID="btnUpload" Text="Upload" CssClass="btn-blue btn-blue-medium" OnClick="btnUpload_Click" />
                                </div>--%>
                                    <div style="float: left; padding-left: 10px">
                                        <span style="padding-left: 10px">
                                            <%--Progress bar--%>
                                            <img id="loading" src="images/loading.gif" style="display: none;"></span>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 14px;"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("#divGrid").hide();
        $("#divSchool").hide();
        $("#divTrust").hide();
        $("#divDocument").hide();


        $('#<%=rblSelect.ClientID%>').find('input[type=radio]').click(function () {
           
            if ($(this).is(':checked')) {
                var selectedValue = $(this).val();
                if (selectedValue == "School") {
                    $("#uploadedDiv").empty();
                    $("#divSchool").show();
                    $("#divTrust").hide();

                    $("#<%=ddlTrustID.ClientID%>").val("-1");
                }
                else {
                    $("#uploadedDiv").empty();
                    $("#divTrust").show();
                    $("#divSchool").hide();

                    $("#<%=ddlSchoolID.ClientID%>").val("-1");
                }
            }
        });

        $('#<%=ddlSchoolID.ClientID%>').change(function () {
            $("#divGrid").show();
            $("#divDocument").show();
            $("#uploadedDiv").empty();

            $('#<%=hdnSchoolID.ClientID%>').val($('#<%=ddlSchoolID.ClientID%>').val());
            var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val();
            var TrustID = $('#<%=hdnTrustID.ClientID%>').val("0");
            var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
            var EmployeeMID = 0;
            var StudentID = 0;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DocumentMaster.aspx/LoadDocument",
                data: "{'intSchoolID':'" + parseInt(SchoolID) + "','intTrustMID':'" + 0 + "','intStudentMID':'" + 0 + "','intEmployeeMID':'" + 0 + "'}",
                dataType: "json",
                success: function (data) {

                    var temp = $.parseJSON(data.d);

                    //alert(temp[1].DocumentMID);

                    $.each(temp, function (i) {

                        // count = parseInt(temp[i].DocumentMID) + 1;
                        var hdnid = temp[i].DocumentMID;
                        var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val();
                        var TrustID = $('#<%=hdnTrustID.ClientID%>').val("0");
                        var StudentID = 0;

                        var txtDocDescId = 'txtDocDesc_' + hdnid;
                        //  alert(txtDocDescId);
                        var lblfilename = temp[i].DocumentName + hdnid;
                        var path = temp[i].DocumentPath;
                        var file = temp[i].DocumentName;
                        // alert(file);
                        var fileName = file.substr(0, (file.lastIndexOf('.')));
                       // alert(fileName)
                        //var fileName = lblfilename;
                        // alert("in");
                        ShowUploadedFiles(file, fileName, hdnid);
                    });
                },

                error: function (error) {
                   // alert("Error" + error);
                }

            });

        });

        $('#<%=ddlTrustID.ClientID%>').change(function () {
            $("#divGrid").show();
            $("#divDocument").show();

            $('#<%=hdnTrustID.ClientID%>').val($('#<%=ddlTrustID.ClientID%>').val());
            var TrustID = $('#<%=hdnTrustID.ClientID%>').val();
            var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val("0");
            var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
            var StudentID = 0;
            var EmployeeMID = 0;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DocumentMaster.aspx/LoadDocument",
                data: "{'intSchoolID':'" + 0 + "','intTrustMID':'" + parseInt(TrustID) + "','intStudentMID':'" + 0 + "','intEmployeeMID':'" + 0 + "'}",
                dataType: "json",
                success: function (data) {

                    var temp = $.parseJSON(data.d);

                    //alert(temp[1].DocumentMID);

                    $.each(temp, function (i) {

                        // count = parseInt(temp[i].DocumentMID) + 1;
                        var hdnid = temp[i].DocumentMID;
                        var SchoolID = $('#<%=hdnSchoolID.ClientID%>').val("0");
                        var TrustID = $('#<%=hdnTrustID.ClientID%>').val();
                        var StudentID = 0;
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

        });
        var UserID = $('#<%=hdnLastUserID.ClientID%>').val();
      
    </script>
</asp:Content>
