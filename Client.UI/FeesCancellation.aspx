<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="FeesCancellation.aspx.cs" Inherits="GEIMS.Client.UI.FeesCancellation" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/jquery-1.8.3.js"></script>--%>
    <link href="../CSS/ajaxCalender.css" rel="stylesheet" />
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(document).ready(function () {

            $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $(document.getElementById('<%= txtTotalPaidAmount.ClientID %>')).val(0);
            $(document.getElementById('<%= txtTotalScholerShip.ClientID %>')).val(0);
            $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);
            $(document.getElementById('<%= txtRemainingPaidFees.ClientID %>')).val(0);
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "FeesCancellation.aspx/GetAllStudentNameForReport",
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Fees Cancellation
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1">Fees Cancellation</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                        <%--<ajax:ToolkitScriptManager ID="ScriptManager1" runat="server">
                        </ajax:ToolkitScriptManager>--%>
                        <div id="divStudentPanel" style="width: 100%">
                            <fieldset>
                                <legend>Search Student</legend>
                                <div class="divclasswithfloat">
                                    <div style="width: 80%; float: left;">
                                        <asp:DropDownList ID="ddlSearchBy" Width="150px" CssClass="textarea" runat="server">
                                            <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Student Name</asp:ListItem>
                                            <asp:ListItem Value="2">Student GR NO</asp:ListItem>
                                            <asp:ListItem Value="3">Student Form No</asp:ListItem>
                                            <asp:ListItem Value="4">Student Unique ID</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="validate[required] textarea autosuggest"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfSearchName" />
                                        &nbsp;&nbsp;&nbsp;
                                       
                                    </div>

                                </div>
                                <div class="divclasswithfloat">
                                    <div style="width: 80%; float: left;">
                                        Academic Year:
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 20%; float: left; text-align: right">
                                        <asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new Attach" Width="50px" Text="Search" CausesValidation="false"
                                            OnClick="btnGo_Click" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvStudent_RowCommand">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <Columns>

                                        <%-- <asp:BoundField DataField="StudentFirstNameEng + ' ' + StudentLastNameEng" HeaderText="Student Name">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" Text='<%#Eval("StudentFirstNameEng") + " " + Eval("StudentLastNameEng") %> ' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class/Division">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" Text='<%#Eval("CLassName") + "-" + Eval("DivisionName") %> ' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CurrentGrNo" HeaderText="Current GR No">
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AdmissionNo" HeaderText="AdmissionNo">
                                            <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Add Fee">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/Edit.png"
                                                    CommandName="Edit1" CommandArgument='<%# Eval("StudentMID") %>' Width="20px" />
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
                            </fieldset>
                        </div>
                        <div id="divFeePanel" runat="server">
                            <fieldset>
                                <legend>Student Details</legend>
                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Admission No:
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:Label runat="server" ID="lblAdmissionNo"></asp:Label>
                                    </div>
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        GR No:
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:Label runat="server" ID="lblCurrentGrNo"></asp:Label>
                                    </div>
                                </div>

                                <div class="divclasswithfloat">
                                    <div style="width: 20%; float: left" class="label">
                                        Name:
                                    </div>
                                    <div style="width: 80%; float: left">
                                        <%--<span><%# Eval("StudentFirstNameEng") %>+" " +<%# Eval("StudentLastNameEng") %></span></div>--%>
                                        <asp:Label runat="server" ID="lblStudentNameEng"></asp:Label>
                                    </div>
                                </div>
                                <div class="divclasswithfloat">
                                    <div style="width: 20%; float: left" class="label">
                                        Section Name:
                                    </div>
                                    <div style="width: 30%; float: left">
                                        <%-- <span><%# Eval("CurrentSectionID") %></span>--%>
                                        <asp:Label runat="server" ID="lblCurrentSection"></asp:Label>
                                    </div>
                                    <div style="width: 20%; float: left" class="label">
                                        Class-Division:
                                    </div>
                                    <div style="width: 30%; float: left">

                                        <asp:Label runat="server" ID="lblClassDivision"></asp:Label>
                                        &nbsp;(&nbsp;<asp:Label runat="server" ID="lblAcademicYear"></asp:Label>&nbsp;)&nbsp;
                                    </div>

                                </div>
                            </fieldset>


                            <%-- </ItemTemplate>
                                </asp:DataList>--%>

                            <fieldset runat="server" id="aa">
                                <legend>Past Fees Details</legend>


                                <div id="divPastFees" style="width: 100%; float: left" runat="server">
                                    <asp:GridView ID="gvPastFees" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="gvPastFees_RowDataBound">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="FeesCollectionMID" HeaderText="FeesCollectionMID">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FeesCollectionTID" HeaderText="FeesCollectionTID">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                <FooterStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>

                                                    <asp:CheckBox ID="chkHeader" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChild" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ReceiptNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" Text='<%#Eval("FYear") + "/" + Eval("ReceiptNo") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FeesName" HeaderText="Fees Name">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Date" HeaderText="Date">
                                                <HeaderStyle Width="100px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Paid Fees Amount In Rs.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotalAmount" runat="server" Width="100px" Text='<%# Eval("FeesAmount") %>' CssClass="txtAmount TextBox" BorderColor="White" ForeColor="Black">0</asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalPaidFees" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ScholarShip In Rs.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDiscountAmount" runat="server" Width="100px" Text='<%# Eval("Discount") %>' CssClass="txtDiscount TextBox" BorderColor="White" ForeColor="Black">0</asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalGivenDiscount" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" ForeColor="White" Font-Size="12px" />
                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                                <div id="pnlFees" runat="server" style="width: 100%; float: left">
                                    <div class="divclasswithfloat">
                                        <div style="width: 20%; float: left" class="label">
                                            Cancelled Amount:
                                        </div>
                                        <div style="width: 30%; float: left">
                                            <asp:TextBox ID="txtTotalPaidAmount" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox" Enabled="false">0</asp:TextBox>
                                        </div>
                                        <div style="width: 20%; float: left" class="label">
                                            Cancelled ScholarShip:
                                        </div>
                                        <div style="width: 30%; float: left;">
                                            <asp:TextBox ID="txtTotalScholerShip" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox" Enabled="false">0</asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="clear"></div>
                                    <div class="divclasswithfloat">
                                        <div style="width: 20%; float: left" class="label">
                                            Total Amount Cancelled:
                                        </div>
                                        <div style="width: 30%; float: left">
                                            <asp:TextBox ID="txtAmountPaid" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox">0</asp:TextBox>
                                            <asp:TextBox ID="txtFullAmount" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox hidden">0</asp:TextBox>
                                        </div>
                                        <div style="width: 20%; float: left" class="label">
                                            Remaining Paid Fees:
                                        </div>
                                        <div style="width: 30%; float: left">
                                            <asp:TextBox ID="txtRemainingPaidFees" runat="server" Width="150px" CssClass="validate[custom[onlyNumberSp]] TextBox">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="divclasswithfloat">
                                        <div style="width: 20%; float: left" class="label">
                                            Cancellation Remarks:
                                        </div>
                                        <div style="width: 80%; float: left" class="label">
                                            <asp:TextBox ID="txtCancellationReason" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%; text-align: right; margin-bottom: 0px;" class="divclasswithoutfloat">
                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium Attach" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $('[id*=chkHeader]').click(function () {

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
            if ($(this).is(":checked")) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
            } else {
                if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                    $('[id*=chkHeader]').prop("checked", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                }
            }
            // alert("chkChild");
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
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


        //var totalAmount = 0, TotalDiscount = 0, Total = 0, TotalAmountnotCheck = 0;
        $('#<%=gvPastFees.ClientID %>').find("input:checkbox").click(function () {
            $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);
            $(document.getElementById('<%= txtTotalPaidAmount.ClientID %>')).val(0);
            $(document.getElementById('<%= txtTotalScholerShip.ClientID %>')).val(0);
            $(document.getElementById('<%= txtRemainingPaidFees.ClientID %>')).val(0);
            CalculateFee();

        });
        function CalculateFee() {
            var totalAmount = 0.0, TotalDiscount = 0.0, Total = 0.0, TotalAmountnotCheck = 0.0, TotalDisCountnotCheck = 0.0;
            var i = 0;
            var RowCount = $('#<%=gvPastFees.ClientID %> tr').length;

            $('#<%=gvPastFees.ClientID %> tr').each(function () {

                if (i != 0) {
                    //alert(i);
                    //alert('RowCount' + (RowCount - 1));
                    if (i != RowCount - 1) {
                        if ($(this).find('input:checkbox').is(":checked")) {
                            //alert($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
                            TotalDiscount += parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
                            $(document.getElementById('<%= txtTotalScholerShip.ClientID %>')).val(TotalDiscount);
                            totalAmount += parseFloat($(this).closest("tr").find($("[id*=txtTotalAmount]")).val());

                            $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(totalAmount);
                            $(document.getElementById('<%= txtTotalPaidAmount.ClientID %>')).val(totalAmount);
                            $(document.getElementById('<%= btnSave.ClientID %>')).prop('disabled', false);
                            //$(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('disabled', true);
                        }
                        else {
                            $(this).closest("tr").find($("[id*=txtDiscountAmount]")).val($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
                            $(this).closest("tr").find($("[id*=txtTotalAmount]")).val($(this).closest("tr").find($("[id*=txtTotalAmount]")).val());
                            TotalAmountnotCheck += parseFloat($(this).closest("tr").find($("[id*=txtTotalAmount]")).val());
                            TotalDisCountnotCheck += parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());

                            $(document.getElementById('<%= txtRemainingPaidFees.ClientID %>')).val(parseFloat(TotalAmountnotCheck));
                       }
                   }
               }
                i = i + 1;
            });
            
           var Amount = parseFloat(totalAmount) + parseFloat(TotalAmountnotCheck);
           $('.lblTotalAmount').text(Amount);
           $(document.getElementById('<%= txtFullAmount.ClientID %>')).val(Amount);
            if (Amount == TotalAmountnotCheck) {
                 $(document.getElementById('<%= txtRemainingPaidFees.ClientID %>')).val(0);
                $(document.getElementById('<%= txtAmountPaid.ClientID %>')).val(0);
                $(document.getElementById('<%= txtTotalPaidAmount.ClientID %>')).val(0);
                $(document.getElementById('<%= txtTotalScholerShip.ClientID %>')).val(0);
            }
            if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                $(document.getElementById('<%= txtRemainingPaidFees.ClientID %>')).val(0);
            }
        }
        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();


        });
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });
    </script>
</asp:Content>
