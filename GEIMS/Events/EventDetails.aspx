<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="GEIMS.Events.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

        function CheckTextLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                alert(" Only " + long + " characters allowed");
            }
        }
    </script>

    <style>
        .TextArea {
            max-width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#id_search').quicksearch('table#<%=gvEventDetails.ClientID%> tbody tr');
        })
    </script>







    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Event Details
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <asp:Panel ID="pnlSearch" runat="server">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                     <div class="divclasswithfloat" style="height: 30px; width: 70%; padding-left: 30%;">
                        <div style="text-align: left; width: 10%; float: left;" class="label">
                            Month :
                        </div>
                        <div style="text-align: left; width: 15%; float: left;">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="Droptextarea" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>

                        </div>


                        <div style="text-align: left; padding-left: 30px; width: 10%; float: left;" class="label">
                            Year :
                        </div>
                        <div style="text-align: left; width: 15%; float: left;">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="Droptextarea" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>

                        </div>
                    </div>
                        </asp:Panel>
                    <asp:GridView ID="gvEventDetails" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvEventDetails_RowCommand" OnSelectedIndexChanged="gvEventDetails_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="EventName" HeaderText="Event Name">
                                <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventFromDate" HeaderText="From Date">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventToDate" HeaderText="To Date">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="5%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventDetailsDescription" HeaderText="Description">
                                <HeaderStyle Width="15%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("ScheduledEventID")%>' Height="20px" Width="20px" />
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
                        <li><a id="tabClassDetails" href="#tabs-1">Events Details</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 320px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <%--Height : 690Px--%>
                        <div style="width: 100%;">
                            <div style="width: 100%;">

                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Event Name :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">

                                        <asp:DropDownList ID="ddlEventName" runat="server" CssClass=" validate[required] Droptextarea" Width="232px" Enabled="False">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="text-align: left; width: 15%; float: left;" class="label">
                                        Event Section :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: right; width: 30%;">
                                        <%--asp:TextBox ID="txtEventSection" runat="server" CssClass="TextBox" Width="225px"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlSectionName" runat="server" CssClass=" validate[required] Droptextarea" Width="232px" Enabled="False">
                                        </asp:DropDownList>

                                    </div>
                                </div>



                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        From Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="225px" Enabled="False"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate">
                                        </ajaxToolkit:CalendarExtender>

                                    </div>

                                    <div style="text-align: left; width: 20%; float: left; height: 13px;" class="label">
                                        From Time :
                                    </div>
                                    <div style="text-align: left; width: 8%; float: left;">
                                        <asp:TextBox ID="txtFromDateFromTime" runat="server" CssClass="TextBox" Width="62px" Enabled="False"></asp:TextBox>

                                    </div>
                                    <div style="text-align: left; width: 7%; float: left; height: 13px;" class="label">
                                        To Time :
                                    </div>

                                    <div style="text-align: left; width: 10%; float: left;">
                                        <asp:TextBox ID="txtFromDateToTime" runat="server" CssClass="TextBox" Width="68px" Enabled="False"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        To Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 30%; float: left;">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Width="225px" Enabled="False"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate">
                                        </ajaxToolkit:CalendarExtender>

                                    </div>

                                    <div style="text-align: left; width: 20%; float: left; height: 13px;" class="label">
                                        From Time :
                                    </div>
                                    <div style="text-align: left; width: 8%; float: left;">
                                        <asp:TextBox ID="txtToDateFromTime" runat="server" CssClass="TextBox" Width="62px" Enabled="False"></asp:TextBox>

                                    </div>
                                    <div style="text-align: left; width: 7%; float: left; height: 13px;" class="label">
                                        To Time :
                                    </div>

                                    <div style="text-align: left; width: 10%; float: left;">
                                        <asp:TextBox ID="txtToDateToTime" runat="server" CssClass="TextBox" Width="68px" Enabled="False"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Event Description :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; float: left; width: 78%;">
                                        <asp:TextBox ID="txtEventDescription" runat="server" onKeyUp="CheckTextLength(this,5000)" CssClass="TextArea validate[required]" Width="93%" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                </div>


                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Event Images :
                                    </div>
                                    <div style="text-align: left; float: left; height: 35px; width: 78%;">
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="TextBox Detach" onchange="UploadFileNow()" />

                                        <asp:Button runat="server" ID="btnUpload" Text="Upload" CssClass="btn-blue-new btn-blue-medium" OnClick="UploadFile" />
                                        <div>
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Text="Note: Images formate .jpg, .jpeg, .png and .gif allow" Style="text-align: center" ForeColor="#FF3300"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="text-align: right; padding-right: 20px;">
                                            <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" />
                                        </div>


                                    </div>
                                    <br />




                                </div>
                            </div>
                        </div>
                        <div id="divContent3" style="width: 10%; float: right;"></div>
                    </div>
                </div>



                <div id="divImagesPnl" runat="server" class="style-tabs" visible="true" style="width: 100%;">

                    <div id="divImagesPnl1" style="height: 340px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                        <asp:Panel ID="pnlImage" runat="server">
                            <div id="divImg" class="pageTitle" style="width: 100%; height: 40px;">
                                <%-- Uploded Images--%>
                                <asp:Label ID="lblUplodedImages" runat="server" Text="Uploded Images" Style="text-align: center"></asp:Label>
                            </div>


                            <div style="overflow: auto; height: 260px; width: 1050px;">

                                <asp:Label ID="lblImage" runat="server" Text="" Style="text-align: center"></asp:Label>

                                <asp:DataList OnDeleteCommand="imgDelete" ID="DataList1" runat="server" RepeatColumns="6"
                                    RepeatDirection="Horizontal" Height="150px" Width="250px">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <div>
                                                <img src='/EventImages/<%#Eval("FileName")%>' width="160px" height="100px" style="border-radius: 10px; margin: 5px" />

                                            </div>
                                            <%-- <div><%#Eval("FileSize")%> KB</div>--%>
                                            <div>
                                                <a target="_blank" href='/EventImages/<%#Eval("FileName")%>'>View Full Size</a>
                                                &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton CommandName="Delete" ID="Delete" CommandArgument='<%#Eval("FileName")%>' runat="server" ForeColor="#FF3300">Delete</asp:LinkButton>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                        </asp:Panel>
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
                $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
                    var valid = $("#aspnetForm").validationEngine('validate');
                    var vars = $("#aspnetForm").serialize();
                });
            </script>
</asp:Content>
