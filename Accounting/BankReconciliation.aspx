<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="BankReconciliation.aspx.cs" Inherits="GEIMS.Accounting.BankReconciliation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .grd {
            vertical-align: top;
        }
    </style>
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
    <script type="text/javascript">
        /*<![CDATA[*/
        function doAlert(parent) {
            var msg = new DOMAlert(
			{
			    title: 'irlazy.com',
			    text: '<h2>Announcement</h2>This is an end user license aggrement, at least we\'ll pretend it is.<br/><br/>Suspendisse sapien nisi, suscipit at, congue sit amet, sodales vel, nisl. Etiam leo. Nunc eleifend pede ac magna. In venenatis tellus eget ipsum. Fusce eget lectus. Nunc auctor rutrum felis. Nam elit mauris, lacinia ut, cursus sit amet, dapibus vitae, leo. Duis sapien. Mauris mollis ante quis sapien. Pellentesque gravida, justo in pharetra viverra, velit est dictum nisi, non ultrices metus magna at enim. Sed aliquam congue risus. Proin ac dui. In hac habitasse platea dictumst. Sed aliquet gravida enim. Morbi vitae dolor vel sem ultrices feugiat.Nunc mollis massa id ipsum. Morbi elit. Sed accumsan. Ut pulvinar congue eros. Quisque tincidunt, nibh et ultrices molestie, nulla augue pulvinar nunc, in egestas neque neque eget libero. Aenean luctus nisi et sem.Suspendisse sapien nisi, suscipit at, congue sit amet, sodales vel, nisl. Etiam leo. Nunc eleifend pede ac magna. In venenatis tellus eget ipsum. Fusce eget lectus. Nunc auctor rutrum felis. Nam elit mauris, lacinia ut, cursus sit amet, dapibus vitae, leo. Duis sapien. Mauris mollis ante quis sapien. Pellentesque gravida, justo in pharetra viverra, velit est dictum nisi, non ultrices metus magna at enim. Sed aliquam congue risus. Proin ac dui. In hac habitasse platea dictumst. Sed aliquet gravida enim. Morbi vitae dolor vel sem ultrices feugiatNunc mollis massa id ipsum. Morbi elit. Sed accumsan. Ut pulvinar congue eros. Quisque tincidunt, nibh et ultrices molestie, nulla augue pulvinar nunc, in egestas neque neque eget libero. Aenean luctus nisi et sem.Suspendisse sapien nisi, suscipit at, congue sit amet, sodales vel, nisl. Etiam leo. Nunc eleifend pede ac magna. In venenatis tellus eget ipsum. Fusce eget lectus. Nunc auctor rutrum felis. Nam elit mauris, lacinia ut, cursus sit amet, dapibus vitae, leo. Duis sapien. Mauris mollis ante quis sapien. Pellentesque gravida, justo in pharetra viverra, velit est dictum nisi, non ultrices metus magna at enim. Sed aliquam congue risus. Proin ac dui. In hac habitasse platea dictumst. Sed aliquet gravida enim. Morbi vitae dolor vel sem ultrices feugiat.Nunc mollis massa id ipsum. Morbi elit. Sed accumsan. Ut pulvinar congue eros. Quisque tincidunt, nibh et ultrices molestie, nulla augue pulvinar nunc, in egestas neque neque eget libero. Aenean luctus nisi et sem.',
			    skin: 'default',
			    width: 300,
			    height: 300,
			    ok: { value: true, text: 'Yes', onclick: showValue },
			    cancel: { value: false, text: 'No', onclick: showValue }
			});
            msg.show();
        };

        function showValue(sender, value) {
            sender.close();
            var newMsg = new DOMAlert({ skin: 'default', width: 200 });
            newMsg.show("Your response", "You pressed " + value);
        }
        /*]]>*/
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvBankReconciliation.ClientID%> tbody tr');
        })
    </script>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Bank Reconciliation
			<%--<asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="../Images/excel.PNG" Width="25px" Height="25px"
                CssClass="btn-blue" ToolTip="Export to Excel" OnClick="btnExportExcel_Click" />--%>
            &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
			<asp:LinkButton CausesValidation="false" ID="btnBack" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnBack_Click">Back</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <%--<div id="Div1" align="center">
                    <asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
                </div>--%>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%; height: 100%; float: left;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1"> Bank Reconciliation</a></li>
                    </ul>
                    <div id="tabs-1" class="gradientBoxesWithOuterShadows">
                        <div class="divclasswithfloat">
                           
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Bank Name : <span class="Required">*</span>
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:DropDownList ID="ddlGeneralLedger" runat="server" CssClass="validate[required] Droptextarea">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div style="text-align: left; width: 15%; float: left;" class="label">
                                Transaction Type : 
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="Droptextarea detach">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Contra">Contra</asp:ListItem>
                                    <asp:ListItem Value="Receipt">Receipt</asp:ListItem>
                                    <asp:ListItem Value="Payment">Payment</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Period : <span class="Required">*</span>
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
                                &nbsp;
								<asp:TextBox ID="txtToDate" runat="server" CssClass="textbox validate[required]" Width="80px"></asp:TextBox>
                            </div>
                           
                        </div>
                      <%--  <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Narration : 
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:CheckBox ID="chkNarration" runat="server"></asp:CheckBox>
                            </div>
                        </div>--%>
                        <div class="divclasswithoutfloat">
                            <asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new btn-blue-medium" Text="Reconciliation" OnClick="btnGo_Click" />
                        </div>
                    </div>
                </div>

                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
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
                    <div class="divclasswithfloat" style="vertical-align: top;">
                        <div style="text-align: left; width: 1070px; float: left; vertical-align: top;">
                            <asp:GridView ID="gvBankReconciliation" runat="server" AutoGenerateColumns="False" OnPreRender="gvBankReconciliation_PreRender"
								BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" CssClass="grid"
								Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowDataBound="gvBankReconciliation_RowDataBound">
								<FooterStyle BackColor="White" ForeColor="#333333" />
								<RowStyle BackColor="White" Height="100%" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" VerticalAlign="Top" />
								<Columns>
                                     <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
                                   
									<asp:BoundField DataField="OperationDate" HeaderText="Date">
										<HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
                                    <asp:BoundField DataField="Particulars" HeaderText="Ledger Name" HtmlEncode="False">
										<HeaderStyle Width="40%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>									
                                    <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
                                   <%-- <asp:BoundField DataField="BankName" HeaderText="Bank Name">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>--%>
                                    <asp:BoundField DataField="ChequeNo" HeaderText="Instrument No">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
                                   <%-- <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>--%>
									<asp:BoundField DataField="Debit" HeaderText="Debit" HtmlEncode="False">
										<HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="Right" Width="10%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
									<asp:BoundField DataField="Credit" HeaderText="Credit" HtmlEncode="False">
										<HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="Right" Width="10%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>
                                   <%-- <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Balance">
										<HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Top" />
										<ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
									</asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Date in Bank Statement">
                                         <ItemTemplate>    
                                            <asp:TextBox ID="txtdate1" runat="server" Width="80px" CssClass="validate[required] TextBox" ></asp:TextBox>
                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender4" OnClientShown="calendarShown1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate1" TargetControlID="txtdate1">
                                             </ajaxToolkit:CalendarExtender>                                          
                                         </ItemTemplate>
                                        
                                    </asp:TemplateField>   
								</Columns>
								<FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
								<PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
								<SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
								<HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
							</asp:GridView>
                        </div>
                    </div>

                    <br />
                    <div class="divclasswithfloat">
                        <div style="text-align: left; width: 15%; float: left;" class="label">
                            <%--<b>Closing Balance : </b>--%>
                        </div>
                        <div style="text-align: right; width: 85%; float: right;">
                            <%--<asp:Label runat="server" ID="lblClosing" CssClass="label"></asp:Label>--%>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right;"></div>
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
        $(document.getElementById('<%= btnGo.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
    <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
        Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolkit:CalendarExtender>
    <script type="text/javascript">
        function calendarShown1(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
</asp:Content>
