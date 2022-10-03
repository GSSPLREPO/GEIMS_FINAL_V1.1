<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptBalanceSheet.aspx.cs"  MasterPageFile="~/Master/TrustMain.Master" Inherits="GEIMS.Accounting.RptBalanceSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Balance Sheet
			<asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="../Images/excel.PNG" Width="25px" Height="25px"
                CssClass="btn-blue" ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />
            &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
			<asp:LinkButton CausesValidation="false" ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnBack_Click">Back</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;" runat="server">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="Div1" align="center">
                    <asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%; height: 100%; float: left;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1">Report</a></li>
                    </ul>
                    <div id="tabs-1" class="gradientBoxesWithOuterShadows">
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Date Range: <span class="Required">*</span>
                            </div>
                            <div style="text-align: left; width: 85%; float: left;">
                                <asp:TextBox ID="txtFromDate" name="date" runat="server" CssClass="textbox validate[required] " Width="80px"></asp:TextBox>
                                &nbsp; to &nbsp;
								<asp:TextBox ID="txtToDate" name="date" runat="server" CssClass="textbox validate[required] " Width="80px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 50%; float: left; padding: 2px;" class="label">
                                <asp:GridView ID="gvAsset" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="AccountGroupID" HeaderText="AccountGroupID">
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" CssClass="hidden" />
                                            <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AccountGroupName" HeaderText="Asset Account Group">
                                            <HeaderStyle Width="85%" HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="85%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sequence">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSequence" runat="server" Width="100px" Text='<%# Eval("Sequence") %>' CssClass="txtTotalAmount TextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" HorizontalAlign="Right" ForeColor="White" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>
                            <div style="text-align: left; width: 48%; float: left; padding: 2px;">
                                <asp:GridView ID="gvLiability" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="AccountGroupID" HeaderText="AccountGroupID">
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" CssClass="hidden" />
                                            <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AccountGroupName" HeaderText="Liability Account Group">
                                            <HeaderStyle Width="85%" HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="85%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sequence">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSequence1" runat="server" Width="100px" Text='<%# Eval("Sequence") %>' CssClass="txtTotalAmount TextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" HorizontalAlign="Right" ForeColor="White" />
                                    <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="divclasswithoutfloat">
                            <asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new btn-blue-medium" Text="Get Report" OnClick="btnGo_OnClick" />
                        </div>
                    </div>
                </div>

                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <div id="search">
                        <%--<input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">--%>
                    </div>
                    <br />
                    <div id="Div2">
                        <asp:Label ID="lblHeading" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="divclasswithfloat">
                        <div style="text-align: left; width: 15%; float: left;" class="label">
                            <%--<b>Opening Balance : </b>--%>
                        </div>
                        <div style="text-align: right; width: 85%; float: right;">
                            <%--<asp:Label runat="server" ID="lblOpening" CssClass="label"></asp:Label>--%>
                        </div>
                    </div>
                    <div style="text-align: center;">
                        <div id="div3" style="font-family: Verdana; font-size: 9px; text-align: center; width: 100%; float: left;"
                            class="">
                            <asp:DataList ID="dlBalanceSheet" runat="server" RepeatDirection="Horizontal" Visible="False"
                                OnItemDataBound="dlBalanceSheet_OnItemDataBound" Height="100%" Width="1050px" HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table style="font-family: Verdana; font-size: 10px; text-align: center; font-weight: normal; width: 100%; float: left"
                                        cellspacing="0px" cellpadding="1px">
                                        <tr>
                                            <td style="text-align: center; font-size: 13px; font-weight: bold;" colspan="2">
                                                <asp:Label ID="lblTrustName" runat="server" Text='<%#Eval("TrustNameEng") %>'></asp:Label>
                                            </td>
                                            <br />
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; font-size: 12px;" colspan="2">
                                                <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                            </td>
                                            <br />
                                        </tr>
                                        <tr>
                                             <%-- Funds & Liabilities--%>
                                            <td>
                                                <table style="font-family: Verdana; font-size: 10px; font-weight: normal; width: 525px; float: left"
                                                    cellspacing="0px" cellpadding="1px">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:DataList ID="dlLiabilityTransaction" runat="server" RepeatDirection="Vertical" OnItemDataBound="dlExpenseTransaction_OnItemDataBound"
                                                                 Height="100%" Width="100%" HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <table width="100%" style="font-family: Verdana; font-size: 13px; font-weight: bold;">
                                                                        <tr>
                                                                            <td align="center" style="border:1px solid black;">
                                                                                <b>Funds & Liabilities</b>
                                                                            </td>
                                                                               <td align="center" style="border:1px solid black;">
                                                                                <b></b>
                                                                            </td>
                                                                            <td style="width: 15%;border:1px solid black;" align="center">
                                                                               <b>Amount</b>
                                                                           </td> 
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td runat="server" id="tdAccountName" ClientIDMode="Static">
                                                                                <%# Eval("AccountName") %>
                                                                            </td>
                                                                               <td style="width: 15%; border:1px solid black; height:25px;">
                                                                                <%# Eval("SubDebit") %>
                                                                            </td>
                                                                            <td style="width: 15%; border:1px solid black;height:25px;">
                                                                               <%# Eval("Debit") %>
                                                                           </td> 
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <%--Property & Assets--%>
                                            <td>
                                                <table style="font-family: Verdana; font-size: 10px; font-weight: normal; width: 525px; float: left"
                                                    cellspacing="0px" cellpadding="1px">
                                                    <tr>
                                                        <td>
                                                            <asp:DataList ID="dlAssetTransaction" runat="server" RepeatDirection="Vertical" onItemDataBound="dlIncomeTransaction_OnItemDataBound"
                                                                 Height="100%" Width="100%" HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <table width="100%" style="font-family: Verdana; font-size: 13px; font-weight: bold;">
                                                                        <tr>
                                                                            <td align="center" style="border:1px solid black;">
                                                                                <b>Property & Asset</b>
                                                                            </td>
                                                                             <td align="center" style="border:1px solid black;">
                                                                                <b></b>
                                                                            </td>
                                                                            <td style="width: 15%;border:1px solid black;" align="center">
                                                                               <b>Amount</b>
                                                                           </td> 
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td runat="server" id="tdiAccountName" ClientIDMode="Static">
                                                                                <%# Eval("AccountName") %>
                                                                            </td>
                                                                                <td style="width: 15%; border:1px solid black; height:25px;">
                                                                               <%# Eval("SubDebit") %>
                                                                           </td> 
                                                                            <td style="width: 15%; border:1px solid black; height:25px;">
                                                                               <%# Eval("Debit") %>
                                                                           </td> 
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right;"></div>
            </div>
        </div>

    </div>

    
    <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender>

    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        $(document.getElementById('<%= btnGo.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });

        $(document).ready(function () {
            var gvIncomeRowsCount = $('#<%=gvAsset.ClientID %> tr').length;
            var gvExpenseRowsCount = $('#<%=gvLiability.ClientID %> tr').length;
            var oldVal;

            $('#<%=gvAsset.ClientID %>').find("input:text").click(function () {
                oldVal = $(this).val();
            });

            $('[id$=txtSequence]').change(function () {
                var i = 0;
                var newVal = $(this).val();

                var row = this.parentNode.parentNode;
                var rowIndex = row.rowIndex;
                //alert('RI' + rowIndex);
                //alert('nv' + newVal);
                if (newVal != '' && newVal != 0 && newVal != null && newVal < gvIncomeRowsCount && newVal > 0) {

                    $('#<%=gvAsset.ClientID %> tr').each(function () {

                        if (i != 0 && i != rowIndex) {
                            //alert('i' + i);
                            var orignalVal = $(this).find('input:text').val();
                            //alert(oldVal);
                            //alert(newVal);
                            //alert('orgval' + orignalVal);
                            //if () {
                            if (newVal == orignalVal) {
                                $(this).find('input:text').val(oldVal);
                            }
                            //}
                            //oldVal = $(this).find('input:text').val();
                        }

                        i = i + 1;
                    });
                    $('#<%=btnGo.ClientID %>').prop("disabled", false);
                } else {
                    alert('Please enter correct sequence no.');
                    $('#<%=btnGo.ClientID %>').prop("disabled", true);
                }
            });


            var oldValExp;
            $('#<%=gvLiability.ClientID %>').find("input:text").click(function () {
                oldValExp = $(this).val();
            });

            $('[id$=txtSequence1]').change(function () {
                var iExp = 0;
                var newValExp = $(this).val();

                var rowExp = this.parentNode.parentNode;
                var rowIndexExp = rowExp.rowIndex;
                //alert('RI' + rowIndex);
                //alert('nv' + newVal);
                if (newValExp != '' && newValExp != 0 && newValExp != null && newValExp < gvExpenseRowsCount && newValExp > 0) {
                    $('#<%=gvLiability.ClientID %> tr').each(function () {

                        if (iExp != 0 && iExp != rowIndexExp) {
                            //alert('i' + i);
                            var orignalValExp = $(this).find('input:text').val();
                            //alert(oldVal);
                            //alert(newVal);
                            //alert('orgval' + orignalVal);
                            //if () {
                            if (newValExp == orignalValExp) {
                                $(this).find('input:text').val(oldValExp);
                            }
                            //}
                            //oldVal = $(this).find('input:text').val();
                        }

                        iExp = iExp + 1;
                    });
                    $('#<%=btnGo.ClientID %>').prop("disabled", false);
                } else {
                    alert('Please enter correct sequence no.');
                    $('#<%=btnGo.ClientID %>').prop("disabled", true);
                }
            });

            //var Discount = parseFloat($(this).closest("tr").find($("[id*=txtDiscountAmount]")).val());
            //var Amount = parseFloat($(this).closest("tr").find($("[id*=txtFeeAmount]")).val());
            //var Total = parseFloat(Amount - Discount);
            //if (Discount > Amount) {
            //    alert("Discount is not large than Total Amount");
            //    $(this).closest("tr").find($("[id*=txtDiscountAmount]")).val(0);
            //}
            //else {
            //    $(this).closest("tr").find($("[id*=txtTotalAmount]")).val(Total);
            //}
            //calculatefooter();

            //$(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('readonly', true);
            //// $(this).closest("tr").find($("[id*=txtDiscountAmount]")).prop('disabled', true);

            //CalculateFee();
        });
    </script>
</asp:Content>