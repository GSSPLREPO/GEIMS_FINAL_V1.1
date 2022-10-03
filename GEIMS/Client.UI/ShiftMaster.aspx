<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="ShiftMaster.aspx.cs" Inherits="GEIMS.Client.UI.ShiftMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        $(function () {
            $('#tab-panel').tabs();
        });

         //TextBox Data valid or not valid Validation
         function validateHhMm(TextBox) {
             var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])?$/.test(TextBox.value);     //23:59
             //var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(TextBox.value);   //2:59:59
             //var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(TextBox.value);   //24:59:59
             
             if (isValid) {
                 TextBox.style.backgroundColor = '#bfa';
                 
             } else {
                 TextBox.style.backgroundColor = '#fba';
             }
             return isValid;
         }

         //Textbox Length Check Validation
         function CheckTextLength(text, long) {
             var maxlength = new Number(long); // Change number to your max length.
             if (text.value.length > maxlength) {
                 text.value = text.value.substring(0, maxlength);
                 alert(" Only " + long + " characters allowed");
             }
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
           Shift Master
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewShift" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewShift_Click">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_Click">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <script type="text/javascript">
                $(function () {
                    $('#id_search').quicksearch('table#<%=gvShiftMaster.ClientID%> tbody tr');
                })
            </script>
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                    <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right:10px; width: 100%;">
                        <asp:GridView ID="gvShiftMaster" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvShiftMaster_SelectedIndexChanged" OnRowCommand="gvShiftMaster_RowCommand">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>

                                            <%--<asp:TemplateField HeaderText="Edit">
									<ItemTemplate>
										<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
											CommandName="Edit1" CommandArgument='<%# Eval("ShiftMID")%>' Height="20px" Width="20px" />
									</ItemTemplate>
									<HeaderStyle HorizontalAlign="center" />
									<ItemStyle HorizontalAlign="center" Width="10%" />
								</asp:TemplateField>--%>
                                           <%--  <asp:TemplateField HeaderText="Holiday">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlHoliday" runat="server" Width="100px" CssClass="ddlHoliday Droptextarea" >
                                                 <asp:ListItem Text="Select" Value="0"></asp:ListItem>               
                                                 <asp:ListItem Text="Holiday" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Weekoff" Value="2"></asp:ListItem>
                                                 </asp:DropDownList>--%>
                                                <%--</ItemTemplate>--%>
                                            <%--</asp:TemplateField>--%>

                                            <asp:BoundField DataField="ShiftMID" HeaderText="Shift MID">
                                                <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                <ItemStyle HorizontalAlign="center" Width="5%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TrustID" HeaderText="TrustMID" Visible="False" />
                                            <asp:TemplateField HeaderText="Shift Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="15%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime" Text='<%#Eval("StartTime") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Recess Start Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecessStartTime" Text='<%#Eval("RecessStartTime") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Recess End Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecessEndTime" Text='<%#Eval("RecessEndTime") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="End Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="EndTime" Text='<%#Eval("EndTime") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="First Half Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstHalfTime" Text='<%#Eval("TotalFirstHalfWH") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Second Half Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSecondHalfTime" Text='<%#Eval("TotalSecondHalfWH") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Total Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTime" Text='<%#Eval("TotalWH") %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="10%" VerticalAlign="Top" Wrap="true" />
                                            </asp:TemplateField>
                                           
                                           
                                           <%-- <asp:TemplateField HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtStartTime"  runat="server" Width="100px" CssClass="txtStartTime TextBox">0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField HeaderText="Recess Start Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRecStartTime"  runat="server" Width="100px" CssClass="txtRecStartTime TextBox">0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recess End Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRecEndTime" runat="server" Width="100px" CssClass="txtRecEndTime TextBox">0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEndTime"  runat="server" Width="100px" CssClass="txtEndTime TextBox">0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField HeaderText="Total Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTime" runat="server" Width="100px" CssClass="lblTime TextBox">0:0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                           <%-- <asp:TemplateField HeaderText="First Half Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstHalfTime" runat="server" Width="100px" CssClass="lblFirstHalfTime TextBox">0:0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Second Half Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSecondHalfTime" runat="server" Width="100px" CssClass="lblSecondHalfTime TextBox">0:0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
								<asp:TemplateField HeaderText="Delete">
									<ItemTemplate>
										<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
											CommandName="Delete1" CommandArgument='<%# Eval("ShiftMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                </div>
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Shift Master</a></li>
                        </ul>
                        <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;">
                               <%-- <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                       Section :<span style="color: red">*</span>
                                    </div>
                                   <div style="text-align: left; float: left; width: 80%;">
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="validate[required] TextBox" Width="155px" Height="25px" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div style="width: 100%" class="divclasswithfloat">
                                    <div style="text-align: left; font-weight:bold; width: 20%; float: left;" class="label">
                                        Shift Name :<span style="color: red">*</span>                                       
                                    </div>
                                    <div style="text-align: left; float: left; width: 80%;">
                                        <asp:TextBox ID="txtShiftName" runat="server" CssClass="validate[required] TextBox" Width="249px" ReadOnly="false" OnTextChanged="txtShiftName_TextChanged" onKeyUp="CheckTextLength(this,25)"></asp:TextBox>                        
                                    </div>
                                </div>
                                 

                               <div style="width: 100%;">
                                    <div id="div1" runat="server" class="divclasswithfloat">
                                         <div id="divNote" runat="server" style="color: red">Note *: InTime and OutTime must be in 24 hour format.</div>
                                        <div id="div2" runat="server" style="color: red">Time Format Example: Minimum 00:01 and Maximum 23:59</div>
                                    </div>
                                
                                    <table style="width: 100%;">
                                        
                                        <tr id="tr0">
                                            <th style="width: 15%;">Start Time <span style="color: red">*</span> </th>
                                            <th style="width: 15%;">Recess Start Time</th>
                                            <th style="width: 15%;">Recess End Time</th>
                                            <th style="width: 15%;">End Time</th>
                                            <%--<th style="width: 20%;">First Half Time</th>
                                            <th style="width: 20%;">Second Half Time</th>
                                            <th style="width: 20%;">Total Time</th>--%>
                                        </tr>
                                        <tr id="tr1">
                                           
                                            <td style="width: 15%">
                                                <asp:TextBox runat="server" ID="TxtStartTime" Width="90%"  placeholder="HH:MM"   CssClass="validate[required] TextBox"  AutoPostBack="False" onchange="validateHhMm(this);" OnTextChanged="TxtStartTime_TextChanged" ></asp:TextBox>

                                                <br />
                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtStartTime"
                                                ValidationExpression="^[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([2-9]{1}\d{3}))$"
                                                ErrorMessage="Invalid format. Valid format is  HH:mm" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="Group1" />--%>
                                           
                                            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="DateTime should be equal or greater than current DateTime"
                                                ForeColor="Red" ControlToValidate="txtDate1" ClientValidationFunction="DateTimeValidation"
                                                Display="Dynamic" ValidationGroup="Group1"></asp:CustomValidator>--%>



                                            </td>
                                            <td style="width: 15%">
                                                <asp:TextBox runat="server" ID="TxtRecessStart" Width="90%" placeholder="HH:MM"  CssClass="validate[required] TextBox"  AutoPostBack="False" onchange="validateHhMm(this);"></asp:TextBox>  
                                                <div>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Check Recess Start Time" ControlToValidate="TxtRecessStart" ControlToCompare="TxtStartTime" Font-Overline="False"  Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True"></asp:CompareValidator>
                                                </div>
                                                    <br/>
                                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtRecessStart"
                                                ValidationExpression="^[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([2-9]{1}\d{3}))$"
                                                ErrorMessage="Invalid format. Valid format is  HH:mm" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="Group1" />--%>
                                            </td>
                                             <td style="width: 15%">
                                                <asp:TextBox runat="server" ID="TxtRecessEnd" Width="90%" placeholder="HH:MM"  CssClass="validate[required] TextBox"  AutoPostBack="False" onchange="validateHhMm(this);"></asp:TextBox>
                                                <div>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Check Recess End Time" ControlToValidate="TxtRecessEnd" ControlToCompare="TxtRecessStart" Font-Overline="False"  Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True"></asp:CompareValidator>
                                                </div>
                                                 <br/>
                                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtRecessEnd"
                                                ValidationExpression="^[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([2-9]{1}\d{3}))$"
                                                ErrorMessage="Invalid format. Valid format is  HH:mm" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="Group1" />--%>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:TextBox runat="server" ID="TxtEndTime" Width="90%" placeholder="HH:MM"  CssClass="validate[required] TextBox"  AutoPostBack="False" onchange="validateHhMm(this);"></asp:TextBox> 
                                                 <div>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Check End Time" ControlToValidate="TxtEndTime" ControlToCompare="TxtRecessEnd" Font-Overline="False"  Operator="GreaterThan" ForeColor="#FF3300" SetFocusOnError="True"></asp:CompareValidator>
                                                </div>
                                                <br/>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtEndTime"
                                                ValidationExpression="^[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([2-9]{1}\d{3}))$"
                                                ErrorMessage="Invalid format. Valid format is  HH:mm" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="Group1" />--%>
                                            </td>
                                            <%-- <td style="width: 20%">
                                                <asp:TextBox runat="server" ID="TxtFirstHalf" Width="131px" placeholder="HH:MM" ReadOnly="true" CssClass="TextBox"></asp:TextBox> 
                                            </td>
                                             <td style="width: 20%">
                                                <asp:TextBox runat="server" ID="TxtSecondHalf" Width="131px" placeholder="HH:MM" ReadOnly="true" CssClass="TextBox"></asp:TextBox> 
                                            </td>
                                             <td style="width: 20%">
                                                <asp:TextBox runat="server" ID="TxtTotalTime" Width="131px" placeholder="HH:MM" ReadOnly="true" CssClass="TextBox"></asp:TextBox> 
                                            </td>--%>
                                        </tr>
                                       
                                       
                                    </table>
                                </div>
                                </div><br/>
                                <div style="width: 100%; text-align: right;padding:0 20px 0px 0px" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" style="left: -8px; top: -3px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divContent3" style="width: 10%; float: right;"></div>
            </div>
        </div>
    <%--</div>--%>
    <script type="text/javascript">
        jQuery("#aspnetForm").validationEngine('attach', {
            promptPosition: "bottomRight",
            validationEventTrigger: "submit",
            validateNonVisibleFields: false,
            updatePromptsPosition: true
        });
        <%--$('#<%=btnSave.ClientID %>').find("[id*=TxtEndTime]").focusout(function () {
            alert("Working")
            var StartTime = $(this).closest("tr").find($("[id*=TxtStartTime]")).val();
            var EndTime = $(this).closest("tr").find($("[id*=TxtEndTime]")).val();
            var RecStarttime = $(this).closest("tr").find($("[id*=TxtRecStart]")).val();
            var RecEndtime = $(this).closest("tr").find($("[id*=TxtRecessEnd]")).val();

            if (parseInt(EndTime) >= parseInt(StartTime)) {
                alert('Time Format Should be 24Hours Or EndTime Must be greater then StartTime.');
            }
            if (parseInt(RecStarttime) >= parseInt(StartTime))
            {
                alert('Time Format Should be 24Hours Or Recess Start Time Must be greater then StartTime.');
            }
            if (parseInt(RecEndtime) >= parseInt(RecStarttime)) {
                alert('Time Format Should be 24Hours Or Recess End Time Must be greater then Recess Start Time.');
            }
            if (parseInt(RecEndtime) >= parseInt(Starttime)) {
                alert('Time Format Should be 24Hours Or Recess End Time Must be greater then Start Time.');
            }
            if (parseInt(Endtime) >= parseInt(RecEndtime)) {
                alert('Time Format Should be 24Hours Or  End Time Must be greater then  Recess End Time.');
            }--%>


        //$(document).ready(function () {
        //    $('#TxtStartTime').focusout(function (event) {
                
        //        var starttime = $("[#txtStarttime]").val();
        //        alert(starttime);
        //        if (!starttime.contains(':')) {
        //            $(this).css({ "background-color": "red", "color": "white" })
        //            alert('Time is not in correct formate');
        //        }
        //       )


        //})
       <%-- $('#<%=gvEmployee.ClientID %>').find("[id*=txtIntime]").focusout(function () {--%>
       <%-- $('#<%=TxtStartTime.ClientID %>').focusout(function () {
            var starttime = $(this).closest("tr").find($("[id*=txtStarttime]")).val();
            //if (!intime.test("/\d{1,2}:\d{1,2}")) {
            //    alert('Time is not in correct formate');
            //}
            if (!starttime.contains(':')) {
                alert('Time is not in correct formate');
            }
        });--%>
        <%--$('#<%=gvEmployee.ClientID %>').find("[id*=txtOuttime]").focusout(function () {
            var intime = $(this).closest("tr").find($("[id*=txtIntime]")).val();
            var outtime = $(this).closest("tr").find($("[id*=txtOuttime]")).val();
            var Recouttime = $(this).closest("tr").find($("[id*=txtRecOuttime]")).val();
            var Recintime = $(this).closest("tr").find($("[id*=txtRecIntime]")).val();

            // alert(intime);
            // alert(outtime);

            var hour1 = intime.split(':')[0];
            var min1 = intime.split(':')[1];
            var hour2 = outtime.split(':')[0];
            var min2 = outtime.split(':')[1];

            var hour3 = Recintime.split(':')[0];
            var min3 = Recintime.split(':')[1];
            var hour4 = Recouttime.split(':')[0];
            var min4 = Recouttime.split(':')[1];



            if (parseInt(intime) >= parseInt(outtime)) {

                alert('Time Format Should be 24Hours Or OutTime Must be greater then InTime.');
                $(this).closest("tr").find($("[id*=lblTime]")).text('0:0');
            } else {
                var hourDiff = (parseInt(hour2) - parseInt(hour1));
                var minDiff = (parseInt(min2) - parseInt(min1));
                var hourDiff1 = (parseInt(hour4) - parseInt(hour3));
                var minDiff1 = (parseInt(min4) - parseInt(min3));


                if (minDiff < 0) {
                    minDiff = 60 + minDiff;
                    hourDiff -= 1;
                }

                if (minDiff1 < 0) {
                    minDiff1 = 60 + minDiff1;
                    hourDiff1 -= 1;
                }

                hourDiff = ((hourDiff) + (hourDiff1))
                minDiff = ((minDiff) + (minDiff1))


                if (minDiff > 59) {
                    hourDiff = hourDiff + 1;
                    minDiff = minDiff - 60;
                }
                //   alert(hourDiff);
                // alert(minDiff);

                var TotalDiff = (hourDiff + ":" + minDiff);


                $(this).closest("tr").find($("[id*=lblTime]")).text(TotalDiff);
                // alert(hourDiff + ":" + minDiff);
            }
            if (!outtime.contains(':')) {
                alert('Time is not in correct formate');
                $(this).closest("tr").find($("[id*=lblTime]")).text('0:0');
            }
            if (outtime.val == '') {
                alert('Time is not in correct formate');
                $(this).closest("tr").find($("[id*=lblTime]")).text('0:0');
            }
        });--%>
        (function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });

    </script>
</asp:Content>

