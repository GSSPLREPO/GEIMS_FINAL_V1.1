<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/TrustMain.Master" CodeBehind="TrustMaster.aspx.cs" Inherits="GEIMS.Client.UI.TrustMaster" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />

    <script type="text/javascript">
        //function stopRKey(evt) {
        //	var evt = (evt) ? evt : ((event) ? event : null);
        //	var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        //	if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        //}
        //document.onkeypress = stopRKey;
        function UploadFileNow() {

            var value = $(document.getElementById('<%= fuImage.ClientID %>')).val();

            if (value != '') {

                $("#aspnetForm").submit();

            }

        };

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }




        $(function () {

            var tab = $(document.getElementById('<%= hfTab.ClientID %>')).val();
            if (tab == "0") {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });
            }
            else if (tab == "4") {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs();
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });
            }


            //Hide Bank Grid View Delete Button on Check Box Event Code Start
            var th = $("[id*=gvbankAssociation] th:contains('Delete')");
            $("[id*=gvbankAssociation] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

            $("#<%= gvbankAssociation.ClientID %> input[id*='ConfirmDeleteCheckBox']").change(function () {
                debugger;



                var isChecked = $(this).is(":checked");
                if (isChecked == true) {
                    alert("1" + isChecked);
                    var th = $("[id*=gvbankAssociation] th:contains('Delete')");
                    th.css("display", isChecked ? "" : "block");
                    $("[id*=gvbankAssociation] tr").each(function () {
                        $(this).find("td").eq(th.index()).css("display", isChecked ? "" : "block");
                    });
                } else {
                    alert("2" + isChecked);
                    var th = $("[id*=gvbankAssociation] th:contains('Delete')");
                    th.css("display", isChecked ? "" : "none");
                    $("[id*=gvbankAssociation] tr").each(function () {
                        $(this).find("td").eq(th.index()).css("display", isChecked ? "" : "none");
                    });
                }

            });
            //Hide Bank Grid View Delete Button on Check Box Event Code End

        });


   



        function onBankDetailClick() {

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Trust Master
            <%--<asp:LinkButton CausesValidation="false" ID="lnkAddNewTrust" runat="server" Visible="false" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewTrust_Click">Add New</asp:LinkButton>--%>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>

        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center; width: 100%;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>
                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <asp:GridView ID="gvTrust" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvTrust_RowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="TrustNameEng" HeaderText="Trust Name">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png" CssClass="Detach"
                                        CommandName="Edit1" CommandArgument='<%# Eval("TrustMID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png" CssClass="Detach"
                                        CommandName="Delete" CommandArgument='<%# Eval("TrustMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>--%>
                        </Columns>
                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabTrustDetails" href="#tabs-1">Trust Details</a></li>
                        <li><a id="tabAddress" href="#tabs-2">Address</a></li>
                        <li><a id="tabGujaratiDetails" href="#tabs-3">Details in Gujarati</a></li>
                        <li><a id="tabContactDetails" href="#tabs-4">Contact Details</a></li>
                        <li><a id="tabBankDetails" href="#tabs-5">Bank Details</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 300px; padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="width: 80%; float: left; height: 70%;">
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    <asp:HiddenField runat="server" ID="hfTab" />
                                    Name of Trust :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; float: left; width: 40%;">
                                    <asp:TextBox ID="txtTrustName" runat="server" CssClass="validate[required] TextBox" Width="94%" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    Trust Abbrev. :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 75%; float: left;">
                                    <asp:TextBox ID="txtAbbreviation" runat="server" CssClass="validate[required,maxSize[1],minSize[1]]] TextBox" Width="150px"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtApprovalNo" runat="server" CssClass="validate[groupRequired[]] TextBox" Width="150px"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    Registration Code :
                                </div>
                                <div style="text-align: left; float: left; width: 75%;">
                                    <%--<asp:TextBox ID="txtRegCode" runat="server" CssClass="validate[groupRequired[]] TextBox" Width="92%" Height="100%"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtRegCode" runat="server" CssClass="TextBox" Width="150px" Height="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    Upload Logo :
                                </div>
                                <div style="text-align: left; width: 75%; float: right;">
                                    <asp:FileUpload ID="fuImage" runat="server" CssClass="TextBox" Height="25px" onchange="UploadFileNow()" />
                                </div>
                            </div>
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    Approval No :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 75%; float: right;">
                                    <asp:TextBox ID="txtApprovalNo" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtApprovalNo" runat="server" CssClass="validate[groupRequired[]] TextBox" Width="150px"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    Approval Date :
                                </div>
                                <div style="text-align: left; width: 25%; float: left;">
                                    <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtApprovalDate" TargetControlID="txtApprovalDate">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    A/C Start Date :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 25%; float: right;">
                                    <asp:TextBox ID="txtAccStartDate" runat="server" CssClass="TextBox validate[required]" Width="140px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtAccStartDate" TargetControlID="txtAccStartDate">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div style="height: 15%; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 25%; float: left;" class="label">
                                    Approval Year of First Year :
                                </div>
                                <div style="text-align: left; width: 75%; float: right;">
                                    <%--<asp:DropDownList ID="ddlApprovalYear" runat="server"></asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlApprovalYear" runat="server" CssClass="Droptextarea" Width="160px" Enabled="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div style="width: 20%; float: left; text-align: right; height: 84%;">
                            <asp:Image ImageUrl="~/images/noimage-big.jpg" runat="server" ID="imgphoto"
                                Width="60%" Height="35%" />
                        </div>
                        <div class="clear"></div>
                        <div style="width: 100%; float: left; text-align: right; height: 10%;">
                            <button id="btnNextTrustDetails" type="button" class="btn-blue-new btn-blue-medium">Next</button>
                            <%--<asp:Button runat="server" ID="ImgNextTrustDetails" Text="Next" CssClass="btn-blue btn-blue-medium"  />--%>
                            <%--<asp:ImageButton ID="" ImageUrl="~/Images/continue-button.gif" ImageAlign="Right" runat="server" Width="120px" Height="35px" />--%>
                        </div>
                    </div>
                    <div id="tabs-2" style="padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Address of Trust :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: right; height: 50px;">
                                <asp:TextBox ID="txtTrustAddress" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Town/City/Village :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                District :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                State :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtState" runat="server" CssClass=" TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Country :
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 40px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Pincode :
                            </div>
                            <div style="text-align: left; width: 60%; float: left;">
                                <asp:TextBox ID="txtPincode" runat="server" CssClass="TextBox validate[custom[onlyNumberSp],maxSize[6],minSize[6]]" Width="150px"></asp:TextBox>

                            </div>
                        </div>
                        <div style="width: 100%; margin-top: 10px; padding-bottom: 10px; height: 30px;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                <button id="btnBackAddressDetails" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <button id="btnNextAddressDetails" type="button" class="btn-blue btn-blue-medium">Next</button>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-3" style="padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                ટ્રસ્ટનુ નામ :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <asp:TextBox ID="txtTrustNameGuj" runat="server" CssClass="validate[required] TextBox" Width="400px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                ટ્રસ્ટનુ સરનામુ :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: right; height: 50px;">
                                <asp:TextBox ID="txtTrustAddressGuj" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                શહેર/ગામડુ :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtcityGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                જીલ્લો :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtDistrictGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                રાજ્ય :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtStateGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                દેશ :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtCountryGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="width: 100%; margin-top: 10px; padding-bottom: 10px; height: 30px;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                <button id="btnBackGuj" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <button id="btnNextGuj" type="button" class="btn-blue btn-blue-medium">Next</button>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-4" style="padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Telephone No :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtTelephone" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Mobile No :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="validate[custom[onlyNumberSp],maxSize[10],minSize[10]] TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Email ID :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="validate[custom[email]] TextBox" Width="250px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Alternet Email ID :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtAlterID" runat="server" CssClass="validate[custom[email]] TextBox" Width="250px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%; display: none;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Fax :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtFax" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Website :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="TextBox" Width="250px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="width: 100%; margin-top: 10px; padding-bottom: 10px; height: 30px;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                <button id="btnBackContact" type="button" class="btn-blue-back btn-blue-medium">Back</button>

                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <button id="btnNextContact" type="button" class="btn-blue btn-blue-medium">Next</button>

                            </div>
                        </div>
                    </div>
                    <div id="tabs-5" style="padding: 5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">

                        <%--<asp:Panel ID="pnlBankDetail" runat="server" Font-Names="Verdana" Font-Size="11px"
                            GroupingText="BankAssociation Detail">--%>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Account Name :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtAccountName" runat="server" CssClass="validate[required] TextBox" Width="300px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                ખાતા નુ નામ :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <asp:TextBox ID="txtAccountNameGuj" runat="server" CssClass="validate[required] TextBox" Width="300px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Account Type :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtAccountType" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Account No. :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Bank Name :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtBankName" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Branch Name :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                PAN No. :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtPanNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                IFSC Code :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtISFCCodes" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%; text-align: center;">
                            <%--<asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" />--%>

                            <div style="text-align: center; width: 90px; float: right;">
                                <button id="btnCancelBank" type="button" class="btn-blue btn-blue-medium Detach">Cancel</button>
                            </div>
                            <div style="text-align: center; width: 110px; float: right;">
                                <asp:Button runat="server" ID="btnBank" Text="Add" CssClass="btn-blue btn-blue-medium Attach" OnClick="btnBank_Click" />
                            </div>
                        </div>
                        <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvbankAssociation" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvbankAssociation_RowCommand" OnRowDataBound="gvbankAssociation_RowDataBound" OnRowDeleting="gvbankAssociation_RowDeleting" OnRowEditing="gvbankAssociation_RowEditing" OnRowCreated="gvbankAssociation_RowCreated">
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="BankAssociationMID" HeaderText="ID">
                                        <HeaderStyle BackColor="#9097A9" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="hidden" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BankName" HeaderText="Bank Name">

                                        <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AccountNameEng" HeaderText="Account Name">

                                        <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AccountNameGuj" HeaderText="Account Name">

                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                        <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AccountType" HeaderText="Account Type">

                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AccountNo" HeaderText="Account No">

                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name">

                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                        <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="PanNo" HeaderText="Pan No">

                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                        <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="IfscCode" HeaderText="IfscCode">

                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                        <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="">
                                        <%-- <EditItemTemplate>
                                            <asp:CheckBox ID="ConfirmDeleteCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="ConfirmDeleteCheckBox_CheckedChanged" />
                                        </EditItemTemplate>--%>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ConfirmDeleteCheckBox" runat="server" OnCheckedChanged="ConfirmDeleteCheckBox_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                                CommandName="DeleteBank" CommandArgument='<%# Eval("BankAssociationMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                        <div style="width: 100%; margin-top: 5px; padding-bottom: 10px; height: 25px;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                <button id="btnBackBank" type="button" class="btn-blue-back btn-blue-medium">Back</button>

                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <%--<button id="btnNextBank" type="button" class="btn-blue btn-blue-medium">Next</button>--%>
                                <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClientClick="myFunction()" />--%>&nbsp;&nbsp;
							&nbsp;&nbsp;
					        
								<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div id="divContent3" style="width: 10%; float: right; height: 100%;"></div>
        </div>
    </div>
    <script type="text/javascript">
        function Validation() {

            jQuery("#aspnetForm").validationEngine('attach', {
                promptPosition: "bottomRight",
                validationEventTrigger: "submit",
                validateNonVisibleFields: false,
                updatePromptsPosition: true
            });
        }
        $(document.getElementById('<%=btnSave.ClientID %>')).click(function () {
            $("#aspnetForm").validationEngine('detach');
        });

        $("#btnNextTrustDetails").click(function () {
            Validation();
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });
            }
        });
        $("#btnBackAddressDetails").click(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });
        });
        $("#btnNextAddressDetails").click(function () {
            Validation();
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });
            }

        });
        $("#btnBackGuj ").click(function () {


            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });

        });
        $("#btnNextGuj").click(function () {
            Validation();
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == true) {

                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });

            }
        });
        $("#btnBackContact").click(function () {


            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });

        });
        $("#btnNextContact").click(function () {

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 4);

                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 3] });
                $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 4 });
            }

        });
        $("#btnBackBank").click(function () {


            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });

        });

        $("#btnCancelBank").click(function () {

            $('#tabs-5 .TextBox').val('');
        });







    </script>
</asp:Content>
