<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="SchoolMasterDetail.aspx.cs" Inherits="GEIMS.Client.UI.SchoolMasterDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/screen.css" rel="stylesheet" />
    <script src="../JS/AjaxFileupload.js"></script>
    <link href="../CSS/Site.css" rel="stylesheet" />
    <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <script src="../JS/ModalPopupWindow.js" type="text/javascript"></script>
    <script type="text/javascript">
        //function stopRKey(evt) {
        //	var evt = (evt) ? evt : ((event) ? event : null);
        //	var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        //	if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        //}
        //document.onkeypress = stopRKey;
        function myFunction() {

            document.getElementById("aspnetForm").reset();
        }
        function myFunctionBank() {

            document.getElementById("tabs-5").reset();
        }
        function UploadFileNow() {

            var value = $(document.getElementById('<%= fuImage.ClientID %>')).val();

            if (value != '') {

                $("#aspnetForm").submit();

            }

        };


        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(document).ready(function () {

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
		});

    </script>
    <%--<script>
		$(function () {
			$(document.getElementById('<%= tabs.ClientID %>')).tabs();
		});

		$().ready(function () {

			$(document.getElementById('<%= imgNextSchool.ClientID %>')).click(function () {

			});



			$(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });

			$(document.getElementById('<%= imgNextAddress.ClientID %>')).click(function () {
				alert("NextAddress");
				$("#aspnetForm").validate({
					debug: true,
					errorElement: "em1",
					errorPlacement: function (error1, element1) {
						alert("Error");
						error1.appendTo(element1.parent("div").next("div"));
					},
					success: function (label1) {

						label1.remove();
					},
					rules: {
						'<%= txtSchoolAddress.UniqueID %>': {
						required: true
					}

					}, messages: {
						'<%= txtSchoolAddress.UniqueID %>': {
							required: "Required Field",
						}

					},
					submitHandler: function (form1) {

						$(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);
						$(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4] });
						$(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });
					}
				});
			});


		});

	</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle">
            School Master
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewSchool" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewSchool_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>

        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div style="text-align: center;">
                    <%--<asp:Label ID="lblMsg" runat="server" CssClass="message" Visible="false"></asp:Label>--%>
                </div>
                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px;">
                    <asp:GridView ID="gvSchool" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvSchool_RowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="SchoolNameEng" HeaderText="School Name">
                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("SchoolMID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("SchoolMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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

                <div id="tabs" runat="server" style="width: 100%;">
                    <ul>
                        <li><a id="tabSchoolDetails" href="#tabs-1">School Details</a></li>
                        <li><a id="tabAddress" href="#tabs-2">Address</a></li>
                        <li><a id="tabGujaratiDetails" href="#tabs-3">Details in Gujarati</a></li>
                        <li><a id="tabContactDetails" href="#tabs-4">Contact Details</a></li>
                        <li><a id="tabBankDetails" href="#tabs-5">Bank Details</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 555px; padding: 10px 10px 10px 10px" class="gradientBoxesWithOuterShadows">

                        <div style="width: 100%">
                            <div style="width: 70%; float: left;">
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 30%; float: left;" class="label">
                                        Name of School :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 70%; float: left;">
                                        <asp:TextBox ID="txtSchoolName" runat="server" CssClass="validate[required] TextBox" Width="94%"></asp:TextBox>

                                    </div>
                                </div>
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 30%; float: left;" class="label">
                                        DEO. School Code :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 70%; float: right;">
                                        <asp:TextBox ID="txtSchoolCode" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 30%; float: left;" class="label">
                                        School Timings :
                                    </div>
                                    <div style="text-align: left; width: 70%; float: right;">
                                        <asp:TextBox ID="txtSchoolTiming" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="height: 30px; margin-top: 10px; width: 100%;">
                                    <div style="text-align: left; width: 30%; float: left;" class="label">
                                        Upload Logo :
                                    </div>
                                    <div style="text-align: left; width: 70%; float: right;">
                                        <asp:FileUpload ID="fuImage" runat="server" CssClass="TextBox Detach" Height="25px" onchange="UploadFileNow()" />
                                    </div>
                                </div>

                            </div>
                            <div style="width: 30%; height: 130px; text-align: center; float: right;">
                                <asp:Image ImageUrl="~/images/noimage-big.jpg" runat="server" ID="imgphoto"
                                    Width="120px" Height="120px" />

                            </div>
                        </div>
                        <div style="width: 100%; float: left; margin-top: 10px;">
                            <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                <div style="text-align: left; width: 21%; float: left;" class="label">
                                    Month For Academic Year: <span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 79%; float: left;">
                                    <asp:DropDownList runat="server" CssClass="validate[required] TextBox" ID="ddlAcademicMonth" Width="150px">
                                        <asp:ListItem Value="">-Select-</asp:ListItem>
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div style="width: 100%; float: left; margin-top: 10px;">
                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        School Abbrev. <span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 79%; float: left;">
                                        <asp:TextBox ID="txtAbbreviation" runat="server" CssClass="validate[required,maxSize[2],minSize[2]]] TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        A/C Start Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 79%; float: left;">
                                        <asp:TextBox ID="txtAccStartDate" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtAccStartDate" TargetControlID="txtAccStartDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        SSC Index No.: 
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtSSCIndexNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        HSC Science Index No.:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtScienceIndexNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        HSC Commerce Index No.:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtCommerceIndexNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        HSC Arts Index No.:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtArtsIndexNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Registration Code:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtRegistrationCode" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Registration Name:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtRegistrationName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                    </div>
                                </div>

                                <%--<div style="height: 55px; margin-top: 10px; float: left; width: 100%; visibility:hidden">
								<div style="text-align: left; width: 21%; float: left;" class="label">
									Registered Address :
								</div>
								<div style="text-align: left; width: 79%; float: right; height: 50px;">
									<asp:TextBox ID="txtRegisteredAddress" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
								</div>
							</div>--%>

                                <div style="height: 55px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        School Motto:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: left;">
                                        <asp:TextBox ID="txtSchoolMotto" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        School Vision:
                                    </div>
                                    <div style="text-align: left; width: 29%; float: right; vertical-align: top;">
                                        <asp:TextBox ID="txtSchoolVision" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="height: 30px; margin-top: 10px; float: left; width: 100%;">
                                    <div style="text-align: left; width: 21%; float: left;" class="label">
                                        Is On Rent :
                                    </div>
                                    <div style="text-align: left; width: 79%; float: right;">
                                        <asp:CheckBox runat="server" ID="chkIsRent" />
                                    </div>

                                </div>

                                <div id="divRent" style="float: right; margin-top: 10px; width: 100%;">
                                    <div style="height: 30px; width: 100%; margin-top: 10px">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Owner Name:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtOwnerName" runat="server" CssClass="validate[required] custom[onlyLetterSp] TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            માલિકનું નામ :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: right;">
                                            <asp:TextBox ID="txtOwnerNameGuj" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div style="height: 55px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Owner's Address:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: right; height: 50px;">
                                            <asp:TextBox ID="txtOwnerAddress" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="height: 55px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            માલિકનું સરનામું:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 79%; float: right; height: 50px;">
                                            <asp:TextBox ID="txtOwnerAddressGuj" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Ward Name:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtWordName" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            વોર્ડ નુ નામ :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 29%; float: right;">
                                            <asp:TextBox ID="txtWardNameGuj" runat="server" CssClass="validate[required] TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Ward No:
                                        </div>
                                        <div style="text-align: left; width: 79%; float: left;">
                                            <asp:TextBox ID="txtWordNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Plot/Block No:
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:TextBox ID="txtPlotNo" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Plot Area:
                                        </div>
                                        <div style="text-align: left; width: 29%; float: right;">
                                            <asp:TextBox ID="txtPlotArea" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div style="height: 30px; margin-top: 10px; width: 100%;">
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            Construction Year:
                                        </div>
                                        <div style="text-align: left; width: 29%; float: left;">
                                            <asp:DropDownList ID="ddlConstructionYear" runat="server" CssClass="TextBox">
                                                <%--<asp:ListItem>2014</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="text-align: left; width: 21%; float: left;" class="label">
                                            No of Floor:
                                        </div>
                                        <div style="text-align: left; width: 29%; float: right;">
                                            <asp:TextBox ID="txtNoOfFloor" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div style="width: 100%; margin-top: 10px; padding-bottom: 10px; height: 30px;">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        &nbsp;
                                    </div>
                                    <div style="text-align: left; width: 80%; float: right;">

                                        <button id="btnNextSchoolDetails" type="button" class="btn-blue btn-blue-medium">Next</button>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div id="tabs-2" style="padding: 10px 10px 10px 10px" class="gradientBoxesWithOuterShadows">
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Area Type :
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <asp:RadioButtonList ID="rblAreaType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>Village Area</asp:ListItem>
                                    <asp:ListItem>Town Area</asp:ListItem>
                                    <asp:ListItem>City Area</asp:ListItem>
                                    <asp:ListItem>Notified Area</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Area SubType :
                            </div>
                            <div style="text-align: left; width: 80%; float: right;">
                                <asp:RadioButtonList ID="rblAreaSubType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>Non Adivasi Area</asp:ListItem>
                                    <asp:ListItem>Economically Backward Area</asp:ListItem>
                                    <asp:ListItem>Frontier Area</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Address of School :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 80%; float: right; height: 50px;">
                                <asp:TextBox ID="txtSchoolAddress" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="hi" ErrorMessage="Error" ControlToValidate="txtSchoolAddress"><img src="../Images/unchecked.gif" >Pls Enter Address</img> </asp:RequiredFieldValidator>--%>
                            </div>

                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;">

                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Town/City/Village :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtTown" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                AT & PO. :
                            </div>
                            <div style="text-align: left; width: 30%; float: right;">
                                <asp:TextBox ID="txtATPOEng" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Taluka :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtTalukaEng" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                District :
                            </div>
                            <div style="text-align: left; width: 30%; float: right;">
                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                State :
                            </div>
                            <div style="text-align: left; width: 30%; float: left;">
                                <asp:TextBox ID="txtState" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Country :
                            </div>
                            <div style="text-align: left; width: 30%; float: right;">
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Pincode :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtPinCode" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>

                            </div>
                        </div>
                        <div style="width: 100%; margin-top: 10px; padding-bottom: 10px; height: 30px;">

                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                <button id="btnBackAddress" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                            </div>
                            <div style="text-align: left; width: 81%; float: right;">
                                <button id="btnNextAddress" type="button" class="btn-blue btn-blue-medium" onclick="UploadFileNow()">Next</button>
                            </div>

                        </div>
                    </div>

                    <div id="tabs-3" style="padding: 10px 10px 10px 10px" class="gradientBoxesWithOuterShadows">
                        <div style="height: 30px; margin-top: 10px;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                શાળા નુ નામ : <span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 82.5%; float: right;">
                                <asp:TextBox ID="txtSchoolNameGuj" runat="server" CssClass="validate[required] TextBox" Width="300px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                શાળા નુ સરનામુ :<span style="color: red">*</span>
                            </div>
                            <div style="text-align: left; width: 82.5%; float: right;">
                                <asp:TextBox ID="txtSchoolAddressGuj" runat="server" CssClass="validate[required] TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                શહેર/ગામડુ :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: left;">
                                <asp:TextBox ID="txtTownGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                મુકામ પોસ્ટ :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: right;">
                                <asp:TextBox ID="txtATPOGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                તાલુકા :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: left;">
                                <asp:TextBox ID="txttalukaGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                જીલ્લો :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: right;">
                                <asp:TextBox ID="txtDistrictGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                રાજ્ય :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: left;">
                                <asp:TextBox ID="txtStateGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                દેશ :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: right;">
                                <asp:TextBox ID="txtCountryGuj" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 30px; margin-top: 10px;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                નોધણીક્રુત નામ :
                            </div>
                            <div style="text-align: left; width: 82.5%; float: right;">
                                <asp:TextBox ID="txtRegistrationCodeGuj" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 55px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                નોધણીક્રુત સરનામુ :
                            </div>
                            <div style="text-align: left; width: 82.5%; float: right;">
                                <asp:TextBox ID="txtRegisteredddressGuj" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 55px; margin-top: 10px;">
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                શાળા નુ સૂત્ર :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: left;">
                                <asp:TextBox ID="txtSchoolMottoGuj" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                <%--<asp:TextBox ID="txtSchoolMottoGuj" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>--%>
                            </div>
                            <div style="text-align: left; width: 17.5%; float: left;" class="label">
                                શાળા ની દ્રષ્ટિ :
                                શાળા ની દર્શન શક્તિ :
                            </div>
                            <div style="text-align: left; width: 32.5%; float: right;">
                                <asp:TextBox ID="txtSchoolVisionGuj" runat="server" CssClass="SmallTextArea" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>--%>
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

                    <div id="tabs-4" style="padding: 10px 10px 10px 10px" class="gradientBoxesWithOuterShadows">
                        <div style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Telephone No :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="validate[custom[onlyNumberSp]] TextBox" Width="150px"></asp:TextBox>
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
                        <div id="divAltEmail" runat="server" visible="false" style="height: 30px; margin-top: 10px; width: 100%;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                Alternet Email ID :
                            </div>
                            <div style="text-align: left; width: 80%; float: left;">
                                <asp:TextBox ID="txtAlternateEmail" runat="server" CssClass="validate[custom[email]] TextBox" Width="250px"></asp:TextBox>
                            </div>
                        </div>
                        <div style="height: 30px; margin-top: 10px; width: 100%;display:none;">
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

                            <div style="text-align: left; width: 19%; float: left;" class="label">
                                <button id="btnBackContact" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                            </div>
                            <div style="text-align: left; width: 81%; float: right;">
                                <button id="btnNextContact" type="button" class="btn-blue btn-blue-medium">Next</button>
                            </div>

                        </div>
                    </div>

                    <div id="tabs-5" style="padding: 10px 10px 10px 10px" class="gradientBoxesWithOuterShadows">
                        <asp:Panel ID="pnlBankDetail" runat="server" Font-Names="Verdana" Font-Size="11px"
                            GroupingText="BankAssociation Detail">
                            <div style="height: 30px; margin-top: 10px; width: 100%;">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    <asp:HiddenField runat="server" ID="hfTab" />
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
                            <div style="height: 30px; margin-top: 10px; padding-bottom: 10px; float: right; width: 100%;">
                                <div style="text-align: right; width: 90px; float: right;">
                                    <button id="btnCancelBank" type="button" class="btn-blue btn-blue-medium Detach">Cancel</button>
                                </div>
                                <div style="text-align: right; width: 110px; float: right;">
                                    <asp:Button runat="server" ID="btnBank" Text="Add" CssClass="btn-blue btn-blue-medium Attach" OnClick="btnBank_Click" />
                                </div>


                                <%--<asp:Button runat="server" ID="Cancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" OnClientClick="myFunction()" />--%>
                            </div>
                        </asp:Panel>
                        <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                            <asp:GridView ID="gvbankAssociation" runat="server" AutoGenerateColumns="False"
                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvbankAssociation_RowCommand" OnRowDataBound="gvbankAssociation_RowDataBound" OnRowDeleting="gvbankAssociation_RowDeleting" OnRowEditing="gvbankAssociation_RowEditing">
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

                                        <HeaderStyle Width="30%" HorizontalAlign="left" VerticalAlign="Top"  />
                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true"  />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AccountNameGuj" HeaderText="Account Name">

                                        <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden"/>
                                        <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" CssClass="hidden"/>
                                        <FooterStyle CssClass="hidden" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AccountType" HeaderText="Account Type">

                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AccountNo" HeaderText="Account No">

                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top"  />
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
                        <div style="width: 100%; margin-top: 10px; padding-bottom: 10px; height: 30px;">
                            <div style="text-align: left; width: 20%; float: left;" class="label">
                                <button id="btnBackBank" type="button" class="btn-blue-back btn-blue-medium">Back</button>
                            </div>
                            <div style="text-align: right; width: 80%; float: right;">
                                <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn-blue btn-blue-medium Detach" />--%>

								&nbsp;&nbsp;
								<asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue btn-blue-medium Detach" OnClick="btnSave_Click" />&nbsp;&nbsp;
					&nbsp;&nbsp;
								
                            </div>
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

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }



        $(document.getElementById('<%=btnSave.ClientID %>')).click(function () {
		        $("#aspnetForm").validationEngine('detach');
		    });

		    $("#btnNextSchoolDetails").click(function () {

		        var valid = $("#aspnetForm").validationEngine('validate');
		        var vars = $("#aspnetForm").serialize();

		        if (valid == true) {
		            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);

			        $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4] });
			        $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });
			    }
			});

            $("#btnBackAddress").click(function () {


                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 0);

            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [1, 2, 3, 4] });
            $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 0 });

        });
        $("#btnNextAddress").click(function () {

            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();

            if (valid == true) {
                $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 2);

		        $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 3, 4] });
		        $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 2 });
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


        $("#btnBackGuj").click(function () {


            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 1);

		    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 2, 3, 4] });
		    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 1 });

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
        $("#divRent").hide();

        $(document.getElementById('<%= chkIsRent.ClientID %>')).click(function () {
            if ($(document.getElementById('<%= chkIsRent.ClientID %>')).prop("checked")) {
                $('#divRent').show();
                $('#tabs-1').height(895);
            }
            else {
                $("#divRent").hide();
                $('#tabs-1').height(555);
            }
        });
        function onBankDetailClick() {


            $(document.getElementById('<%= tabs.ClientID %>')).tabs("enable", 3);

		    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ disabled: [0, 1, 2, 4] });
		    $(document.getElementById('<%= tabs.ClientID %>')).tabs({ active: 3 });
		}
		$("#btnBank").click(function () {
		    alert("btnbank");

		    var valid = $("#aspnetForm").validationEngine('validate');
		    var vars = $("#aspnetForm").serialize();
		});

		$(".gvImageButton").click(function () {
		    $("#aspnetForm").validationEngine('detach');
		});

		$("#btnCancelBank").click(function () {

		    $('#tabs-5 .TextBox').val('');
		});
    </script>
</asp:Content>
