<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="GEIMS.Accounting.Receipt" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function makeCreditSum() {
            var gridViewID = "<%=gvReceiptsEntry.ClientID %>";
            var gridView = document.getElementById(gridViewID);
            var gridViewControls = gridView.getElementsByTagName("input");
            var sumVal = 0.0;

            for (i = 0; i < gridViewControls.length - 1; i = i + 2) {
                var ctrlVal = gridViewControls[i].value;
                ctrlVal = Math.max(ctrlVal, 0);
                sumVal = eval(sumVal) + eval(ctrlVal);
            }
            var len = gridViewControls.length;
            var CreID = gridViewControls[len - 1].id;
            document.getElementById(CreID).value = sumVal;

            //document.getElementById(gridViewControls[length-2].id).value = sumVal;
            //document.getElementById("txtDebitSum").value = sumVal;
        }
        function colorfocusTextBox(ctrlID) {
            var gridViewID = "<%=gvReceiptsEntry.ClientID %>";
		    var gridView = document.getElementById(gridViewID);
		    var gridViewControls = gridView.getElementsByTagName("input");
		    for (i = 0; i < gridViewControls.length; i++) {

		        if (i == eval(ctrlID)) {
		            gridViewControls[i].style.backgroundColor = "#DDFFAA";
		        }
		        else {
		            gridViewControls[i].style.backgroundColor = "#FFFFFF";
		        }


		    }
		}
		function makeTextBoxFocus(rowID) {
		    var gridViewID = "<%=gvReceiptsEntry.ClientID %>";
		    var gridView = document.getElementById(gridViewID);
		    var gridViewControls = gridView.getElementsByTagName("input");
		    var sel = 2 * rowID;
		    gridViewControls[sel].focus();

		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvReceipts.ClientID%> tbody tr');
        })
    </script>
    <div id="divCurrenTabSelected" class="hidden" visible="false">div4</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Receipt
			 <asp:LinkButton CausesValidation="false" ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnAddNew_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnViewList_Click">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="Div1" align="center">
                    <asp:Label ID="lblDuration" runat="server" CssClass="label"></asp:Label>
                </div>
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                    <asp:GridView ID="gvReceipts" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvReceipts_RowCommand" OnRowEditing="gvReceipts_RowEditing" OnPreRender="gvReceipts_PreRender" OnSelectedIndexChanged="gvReceipts_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="ReceiptPaymentCode" HeaderText="Receipt No">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReceiptPaymentDate" HeaderText="Receipt Date">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="15%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AccountName" HeaderText="Account Name">
                                <HeaderStyle Width="50%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                          <%--  <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>--%>
                             <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" Text='<%# Eval("Amount") + ".00" %>'  runat="server" />
                                </ItemTemplate>
                                 <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("ReceiptPaymentCode")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("ReceiptPaymentCode")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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

                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1">Receipt</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 0 0 0 10px" class="gradientBoxesWithOuterShadows">
                        <%--<fieldset>--%>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Date :<span class="Required">*</span>
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:TextBox runat="server" ID="txtDate" CssClass="validate[required] TextBox" Width="190px" AutoPostBack="True" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hfDate" runat="server" />
                            </div>
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                General Ledger : <span class="Required">*</span>
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:DropDownList ID="ddlGeneralLedger" runat="server" CssClass="validate[required] Droptextarea" OnSelectedIndexChanged="ddlGeneralLedger_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinkButton ID="lbtnCreateNew" runat="server" OnClick="lbtnCreateNew_OnClick" CssClass="button" ForeColor="#FFFFFF" Style="text-decoration: none; font-size: 12px; font-family: Verdana; height: 10px; width: 20px;">Create New</asp:LinkButton>

                            </div>
                            
                        </div>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Mode of Receipt : <span class="Required">*</span>
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:DropDownList ID="ddlReceiptMode" runat="server" CssClass="validate[required] Droptextarea" OnSelectedIndexChanged="ddlReceiptMode_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                    <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                    <asp:ListItem Value="ODCC">Bank OD/CC</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divlblBalance" runat="server">
                                <div style="text-align: left; width: 15%; float: left;" class="label">
                                    Current Balance :
                                </div>
                                <div style="text-align: left; width: 35%; float: left;">
                                    <asp:Label runat="server" ID="lblCurrentBalance"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Receipt No :
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:TextBox runat="server" ID="txtVoucherCode" CssClass="TextBox detach" Width="190px" BackColor="#cae4ff"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Cheque No :
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:TextBox runat="server" ID="txtChequeNo" CssClass="TextBox detach" Width="190px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Bank Name :
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <asp:TextBox runat="server" ID="txtBankName" CssClass="TextBox detach" Width="190px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Branch Name :
                            </div>
                            <div style="text-align: left; width: 35%; float: left;">
                                <%--<asp:TextBox runat="server" ID="txtBranchName" CssClass="TextBox detach" Width="190px" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)"></asp:TextBox>--%>
                                <asp:TextBox runat="server" ID="txtBranchName" CssClass="TextBox detach" Width="190px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Section :
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlSection"  CssClass="validate[required] Droptextarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"  >
                                    <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfSectionID" runat="server" />
							</div>
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Budget Category :
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlBudgetCategory"  CssClass="validate[required] Droptextarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetCategory_SelectedIndexChanged"  >
                                    <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfCategoryID" runat="server" />
							</div>
						</div>
						<div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Budget Heading :
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								 <asp:DropDownList ID="ddlBudgetHeading"  CssClass="validate[required] Droptextarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetHeading_SelectedIndexChanged"   >
									   <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                 </asp:DropDownList>
                                 <asp:HiddenField ID="hfHeadID" runat="server" />
							</div>
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Budget SubHeading :
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<asp:DropDownList ID="ddlBudgetSubHeading"  CssClass="validate[required] Droptextarea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetSubHeading_SelectedIndexChanged" >
                                      <asp:ListItem Value="-1">-Select-</asp:ListItem>                                          
                                </asp:DropDownList>   
                                <asp:HiddenField ID="hfSubHeadID" runat="server" />
							</div>
						</div>
                        <div class="divclasswithfloat">
							<div style="text-align: left; width: 15%; float: left;" class="label">
								Unutilised Budget :<span class="Required"></span>
							</div>
							<div style="text-align: left; width: 35%; float: left;">
								<div style="text-align: left; width: 35%; float: left;">
									<asp:TextBox runat="server" ID="txtUnutilizedBudget" CssClass="TextBox detach" Width="190px" Text="0" BackColor="#cae4ff" ReadOnly="true" Style="text-align:right;" ></asp:TextBox>
								    <asp:HiddenField ID="hfBudgetScreenTID" runat="server" />
                                    <asp:HiddenField ID="hfBudgetScreenID" runat="server" />
                                </div>
							</div>
						</div>
                        <div class="divclasswithfloat" width="100%">
                            <div class="label" align="center">
                                <asp:GridView ID="gvReceiptsEntry" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="776px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Receipt No." DataField="ReceiptPaymentID">
                                            <FooterStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="General Ledger Account " FooterStyle-Font-Size="15px"
                                            FooterText="Total Amount  :  " FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAccountName" runat="server" CssClass="Droptextarea"
                                                    Font-Names="Verdana" Font-Size="12px" Width="640px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="TextBox" onchange="return makeCreditSum()"
                                                    Font-Names="Verdana" onblur="howManyDecimals(this.id,'#FFDFDF')" onkeypress="return NumericKeyPressFraction(event)" Style="text-align: right;" Width="100px" Height="13px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCreditAmount_FilteredTextBoxExtender"
                                                    runat="server" Enabled="True" TargetControlID="txtCreditAmount"
                                                    ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ControlToValidate="txtCreditAmount" CssClass="Required"
                                                    ErrorMessage="Invalid Amount" ValidationExpression="\d{0,8}(.)?(\d{0,2})">*</asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator2_ValidatorCalloutExtender"
                                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator2">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <input id="txtCreditSum" runat="server" class="TextBox" disabled="disabled" style="text-align: right; width: 100px; height: 13px;" type="text" value="0"/>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="JournalID" Visible="False" />
                                    </Columns>
                                    <FooterStyle BackColor="#2b558e" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#2b558e" Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="20px" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="divclasswithfloat" width="100%">
                            <div class="label" align="center">
                                <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" CssClass="btn-blue-new btn-blue-small Detach" Height="25px" OnClick="btnAddRow_Click" />
                            </div>
                        </div>
                        <div class="divclasswithfloat">
                            <div style="text-align: left; width: 15%; float: left;" class="label">
                                Narration :<%--<span class="Required">*</span>--%>
                            </div>
                            <div style="text-align: left; width: 85%; float: left;">
                                <asp:TextBox runat="server" ID="txtNarration" CssClass="TextBox" Width="700px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>

                        </div>
                        <div class="divclasswithoutfloat">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
                        </div>
                        <%--</fieldset>--%>
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
        $(document.getElementById('<%= btnSave.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
</asp:Content>
