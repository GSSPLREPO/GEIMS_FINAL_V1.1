<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="YearlyEmployeeReport.aspx.cs" Inherits="GEIMS.PayRoll.YearlyEmployeeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
            <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
            <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
                <div id="divTitle" class="pageTitle" style="width: 100%;">
                    Pay slip (Yearly)
            <asp:Button ID="btnPrintDetail" runat="server" CssClass="btn-blue btn-blue-medium" Text="Print Detail" OnClick="btnPrintDetail_Click" />
                    &nbsp;
             <asp:Button ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Cancel"
                 OnClick="btnBack_Click" />&nbsp;
                    <asp:Button ID="btnReport" runat="server" CssClass="btn-blue btn-blue-medium Detach" Text="Back To Menu"
                 OnClick="btnReport_Click" />
                  <%--  <a href="../Client.UI/TrustReports.aspx">../Client.UI/TrustReports.aspx</a>--%>
                </div>
                <div id="divContent" style="height: 100%; font-family: Verdana;">
                    <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
                    <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                        <div style="text-align: center; width: 100%;">
                            <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                        </div>

                        <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                            <div id="tabs-1" style="min-height: 150px;padding:5px;">
                                <asp:Panel ID="pnlEmployeePayrollInfo" runat="server" GroupingText="Payroll Details">

                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Employee Name :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="TextBox autosuggest" Width="50%" Height="100%"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfSchoolMID" />
                                                <asp:HiddenField runat="server" ID="hfTrustMID" />
                                                <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                                <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left;" class="label">
                                        <div style="padding: 10px;">
                                            <div style="float: left; width: 15%;">
                                                Year :<span style="color: red">*</span>
                                            </div>
                                            <div style="float: left; width: 25%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="Droptextarea" Width="210px">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: Left; width: 15%;">
                                                IsApproved :
                                            </div>
                                            <div style="float: Left; width: 25%;">
                                                <asp:CheckBox runat="server" ID="chkApproved" Checked="true"></asp:CheckBox>
                                            </div>

                                            <div style="float: left; text-align: right; width: 20%;">
                                                <asp:Button runat="server" ID="btnGo" Text="Go" CssClass="btn-blue-new btn-blue-medium" OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="divReport" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">


                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 20px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>Trust :</b>
                                                <asp:Label runat="server" ID="lblTrustName"></asp:Label>
                                                &nbsp; &nbsp; &nbsp;
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                                        <div style="padding: 10px; padding-right: 30px;">
                                            <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                                <b>Employee Name:</b>
                                                <asp:Label runat="server" ID="lblName"></asp:Label>
                                                &nbsp; &nbsp; &nbsp;
                                               
                                                
                                                <b>Year :</b>
                                                <asp:Label runat="server" ID="lblYear"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 10px; padding-right: 30px; overflow: scroll; width: 1100px" runat="server" id ="dvReport">
                                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="red" Style="text-align: center"></asp:Label>
                                        <asp:GridView ID="gvReport" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                            CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%" OnRowDataBound="gvReport_RowDataBound">
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

                <div id="divReport1" style="width: 100%; padding-top: 0px; display: none;">
                    <div id="div1" runat="server" style="width: 100%; float: left; padding-top: 0px;" class="label">
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 20px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <b>Report : Employee Payroll Report</b>
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 20px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <b>Trust :</b>
                                    <asp:Label runat="server" ID="lblTrust"></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
                            <div style="padding: 10px; padding-right: 30px;">
                                <div style="float: left; text-align: center; width: 100%; padding-bottom: 10px;">
                                    <b>Employee Name:</b>
                                    <asp:Label runat="server" ID="lblName1"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;
                                               
                                    <b>Year :</b>
                                    <asp:Label runat="server" ID="lblYear1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="padding: 10px; padding-right: 30px; width: 100%; float: left">
                            <asp:GridView ID="gvReport1" Visible="true" runat="server" BackColor="White" BorderColor="Black" AutoGenerateColumns="true"
                                CellPadding="4" Font-Names="Verdana" Font-Size="11px" AllowSorting="false" Width="100%" OnRowDataBound="gvReport1_RowDataBound">
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
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "YearlyEmployeeReport.aspx/GetAllEmployeeNameForReport",
                        data: "{'prefixText':'" + request.term + "','TrustMID':'" + $(document.getElementById('<%= hfTrustMID.ClientID %>')).val() + "','SchoolMID':'" + $(document.getElementById('<%= hfSchoolMID.ClientID %>')).val() + "'}",
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

        $(document.getElementById('<%= btnBack.ClientID %>')).click(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "PaySlip.aspx/GetAllEmployeeNameForReport",
                        data: "{'prefixText':'" + request.term + "','TrustMID':'" + $(document.getElementById('<%= hfTrustMID.ClientID %>')).val() + "','SchoolMID':'" + $(document.getElementById('<%= hfSchoolMID.ClientID %>')).val() + "'}",
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
        });

        $(document.getElementById('<%= btnGo.ClientID %>')).click({
         
         });
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });

        $('.Attach').click(function () {
            $("#aspnetForm").validationEngine('attach');
        });
    </script>
</asp:Content>
