<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="TeacherWiseTimeTable.aspx.cs" Inherits="GEIMS.ReportUI.TeacherWiseTimeTable" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />
	<style type="text/css"> 
 
     .VertiColumn th { 
      writing-mode: tb-rl; 
      filter: fliph()flipV(); 
 
} 
 
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Time Table Information
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
            &nbsp;
             <asp:Button ID="btnBack1" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />
              &nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                 OnClick="btnReport_Click" />
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <div id="tabs-1" style="min-height: 50px;">
                        <div id="divtimeTable" runat="server">
                            <fieldset>
                                <legend>Time Table</legend>


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
                                            <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnGo_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="divReport" runat="server" style="width: 100%; padding-top: 0px;">
                            <div style="padding: 10px; padding-right: 30px; width: 1100px; float: left">
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
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
            </div>
        </div>
        <div id="divReport1"  style="width: 100%; padding-top: 0px;display: none">
            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                        <b>Report : Time Table (For Teachers)</b>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                        Trust Name :
                                                <asp:Label runat="server" ID="lblTrust"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                        School Name :
                                                <asp:Label runat="server" ID="lblSchool"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                <div style="padding: 10px; padding-right: 30px;">
                    <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                        Employee Name :
                                                <asp:Label runat="server" ID="lblName"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="padding: 10px; padding-right: 30px; width: 1100px; float: left">
                <asp:GridView ID="gvReport1" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                    CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%">
                    <RowStyle BackColor="White" />
                    <FooterStyle BackColor="White" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="Black"
                        BorderWidth="1px" BorderStyle="Solid" />
                </asp:GridView>
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
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });

        $('.Attach').click(function () {
            $("#aspnetForm").validationEngine('attach');
        });
    </script>
</asp:Content>
