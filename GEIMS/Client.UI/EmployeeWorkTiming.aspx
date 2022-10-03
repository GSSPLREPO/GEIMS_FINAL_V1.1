<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EmployeeWorkTiming.aspx.cs" Inherits="GEIMS.Client.UI.EmployeeWorkTiming" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../CSS/TabPanel.css" rel="stylesheet" />
    <link href="../CSS/screen.css" rel="stylesheet" />        
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        function BindCheckBOX() {
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
        }
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Employee Working Time         
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 0%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 100%; float: left; height: 100%;">
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Employee Working Time</a></li>
                        </ul>
                        <div id="tabs-1" style="padding: 10px 10px 10px 10px;" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;">
                                <asp:HiddenField ID="hfCLassMID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfDivisionTID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfEmployeeID" runat="server" ClientIDMode="Static" />


                                <%--<div style="width: 100%;" class="divclasswithfloat">
                                    <div style="width: 19%; float: left;" class="label">
                                        Date :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 81%; float: left;">
                                        <asp:TextBox ID="txtdate" runat="server" Width="150px" CssClass="validate[required] TextBox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="Calendar1" OnClientShown="calendarShown" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtdate" TargetControlID="txtdate">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                   <%-- <div style="padding-top: 40px;">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Style="vertical-align: top" />
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="true" CssClass="button" Style="vertical-align: top" OnClick="btnUpload_OnClick" />
                                    </div>--%>
                                </div>
                                   <div style="width: 100%;" >
                               <div style="width: 100%;"  class="divclasswithfloat">
                                       <div style="text-align: left; width: 19%; float: left;" class="label">
                                           School Name : <span style="color: red">*</span>
                                       </div>
                                       <div style="text-align: left; width: 80%; float: left;">
                                           <asp:DropDownList ID="ddlSchoolName" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolName_SelectedIndexChanged">
                                           </asp:DropDownList>
                                       </div>
                                   </div>
                                      <div style="width: 100%;"  class="divclasswithfloat">
                                       <div style="text-align: left; width: 19%; float: left;" class="label">
                                           Department Name : <span style="color: red">*</span>
                                       </div>
                                       <div style="text-align: left; float: left; width: 30%;">
                                           <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                           </asp:DropDownList>
                                       </div>
                                  
                                   </div>
                                        <div style="width: 100%;"  class="divclasswithfloat">
                                       <div style="text-align: left; width: 19%; float: left;" class="label">
                                           Days Of Week :<span style="color: red">*</span>
                                       </div>
                                       <div style="text-align: left; float: left; width: 30%;">
                                           <asp:DropDownList ID="ddlDaysofweek" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDaysofweek_SelectedIndexChanged">
                                             <%--<asp:ListItem Value="-1">-Select-</asp:ListItem>
                                            <asp:ListItem Value="2">Mon</asp:ListItem>
                                            <asp:ListItem Value="3">Tue</asp:ListItem>
                                               <asp:ListItem Value="4">Wed</asp:ListItem>
                                            <asp:ListItem Value="5">Thu</asp:ListItem>
                                               <asp:ListItem Value="6">Fri</asp:ListItem>
                                            <asp:ListItem Value="7">Sat</asp:ListItem>--%>
                                           </asp:DropDownList>
                                       </div>
                                   </div>
                                 </div>
                                <div style="width: 100%;" class="divclasswithfloat">
                                    <asp:Button runat="server" ID="btnViewGrid" Text="View" CssClass="btn-blue btn-blue-medium" OnClick="btnViewGrid_Click" />
                                </div>
                                <div id="divNote" runat="server" style="color: red">Note *: Start Time and End Time must be in 24 hour format.</div>
                                <div id="divGrid" runat="server" class="divclasswithfloat">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                             <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                                                BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                                                Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvEmployee_SelectedIndexChanged1" OnRowDataBound="gvEmployee_RowDataBound" >
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="EmployeeMID" HeaderText="Employee ID">
                                                        <HeaderStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" CssClass="hidden" />
                                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" CssClass="hidden" />
                                                    </asp:BoundField>
                                                     <asp:TemplateField HeaderText="Select Employee">
                                                                    <HeaderTemplate>

                                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%--<input type="checkbox" id="chkChild" />--%>
                                                                        <asp:CheckBox ID="chkChild" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="Select Employee">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkChild" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code">
                                                        <HeaderStyle Width="10%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="10%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentName" Text='<%#Eval("EmployeeFNameENG") + " " + Eval("EmployeeLNameENG") %> ' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="30%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Department" HeaderText="Department">
                                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                        <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                                                    </asp:BoundField>

                                                    <%--  Change  --%>

                                                    <asp:TemplateField HeaderText="Select Shift">
                                                       <%-- <HeaderTemplate>
                                                                <asp:DropDownList runat="server" CssClass=" TextBox" ID="ddlShiftNameMaster" AutoPostBack="true" OnSelectedIndexChanged="ddlShiftNameMaster_SelectedIndexChanged" Width="100px">
                                                                </asp:DropDownList>
                                                        </HeaderTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlShiftName" runat="server" CssClass="Droptextarea" OnSelectedIndexChanged="ddlShiftName_SelectedIndexChanged" 
                                                                Font-Names="Verdana" Font-Size="12px" Width="160px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <%--<HeaderStyle HorizontalAlign="Center" Width="100px" />--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time (24 hour)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIntime" runat="server" Width="75px" CssClass="txtIntime TextBox" ReadOnly="true">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time (24 hour)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOuttime" runat="server" Width="75px" CssClass="txtOuttime TextBox" ReadOnly="true">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTime" runat="server" Width="75px" CssClass="lblTime TextBox">0:0</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Recess Start Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRecessStartTime" runat="server" Width="100px" CssClass="lblRecessStartTime TextBox">0:0</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Recess End Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRecessEndTime" runat="server" Width="100px" CssClass="lblRecessEndTime TextBox">0:0</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="FirstHalfTime">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFirstHalfTime" runat="server" Width="100px" CssClass="lblFirstHalfTime TextBox">0:0</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SecondHalfTime">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSecondHalfTime" runat="server" Width="100px" CssClass="lblSecondHalfTime TextBox">0:0</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <%--  <asp:TemplateField HeaderText="Holiday">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlHoliday" runat="server" Width="100px" CssClass="ddlHoliday Droptextarea" >
                                                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>               
                                                         <asp:ListItem Text="Holiday" Value="1"></asp:ListItem>
                                                         <asp:ListItem Text="Weekoff" Value="2"></asp:ListItem>
                                                         </asp:DropDownList>--%>
                                                        <%--</ItemTemplate>--%>
                                                    <%--</asp:TemplateField>--%>
                                                </Columns>
                                                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </ContentTemplate>                                     
                                    </asp:UpdatePanel>
                                </div>
                                <div style="width: 100%; text-align: right;" class="divclasswithOutfloat;">
                                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" Enabled="false" />
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
         function calendarShown(sender, args) {
             sender._popupBehavior._element.style.zIndex = 10005;
         }
         jQuery("#aspnetForm").validationEngine('attach', {
             promptPosition: "bottomRight",
             validationEventTrigger: "submit",
             validateNonVisibleFields: false,
             updatePromptsPosition: true
         });

         function CallConfirmBox() {
             javascript: return confirm('Are you sure, you want to delete this Record?');
         }

        <%-- $("[id*=chkChild]").click(function () {
             //alert("chkChild");
             if ($(this).is(":checked")) {
                 $(this).closest("tr").css("background-color", "White");
                 $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
               

            }
            else {
                // alert("unchkChild");
                $(this).closest("tr").css("background-color", "#F799A3");


            }

            if ($('[id*=chkChild]').length == $('[id*=chkChild]:not(:checked)').length) {
                $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
            }
        });--%>
         function jqry() { };
         $("[id*=chkHeader]").click(function () {
             //alert("chkHeader");
             //  alert($("[id$=chkChild]").attr('checked', this.checked));
             // $("[id*=chkChild]").attr('checked', this.checked);
             if ($(this).is(":checked")) {
                 $('[id*=chkChild]').prop("checked", true);
                 $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
                } else {
                    $('[id*=chkChild]').prop("checked", false);
                    $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", true);
                    $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#848484");
                }
            });
         $("[id*=chkChild]").click(function () {
             //   alert("chkChild");
             if ($(this).is(":checked")) {
                 //    alert("CHecked");
                 $(document.getElementById('<%= btnSave.ClientID %>')).prop("disabled", false);
                $(document.getElementById('<%= btnSave.ClientID %>')).css("background-color", "#3b5998");
               }
               if ($('[id*=chkChild]').length == $('[id*=chkChild]:checked').length) {
                   // alert("In");

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

        $('#<%=gvEmployee.ClientID %>').find("[id*=txtIntime]").focusout(function () {
            var intime = $(this).closest("tr").find($("[id*=txtIntime]")).val();
            //if (!intime.test("/\d{1,2}:\d{1,2}")) {
            //    alert('Time is not in correct formate');
            //}
            if (!intime.contains(':')) {
                alert('Time is not in correct formate');
            }
        });
        $('#<%=gvEmployee.ClientID %>').find("[id*=txtOuttime]").focusout(function () {
             var intime = $(this).closest("tr").find($("[id*=txtIntime]")).val();
             var outtime = $(this).closest("tr").find($("[id*=txtOuttime]")).val();
             //var Recouttime = $(this).closest("tr").find($("[id*=txtRecOuttime]")).val();
             //var Recintime = $(this).closest("tr").find($("[id*=txtRecIntime]")).val();

            

             var hour1 = intime.split(':')[0];
             var min1 = intime.split(':')[1];
             var hour2 = outtime.split(':')[0];
             var min2 = outtime.split(':')[1];

         
             //var hour3 = Recintime.split(':')[0];
             //var min3 = Recintime.split(':')[1];
             //var hour4 = Recouttime.split(':')[0];
             //var min4 = Recouttime.split(':')[1];


       
             if (parseInt(intime) >= parseInt(outtime)) {
             
                 alert('Time Format Should be 24Hours Or OutTime Must be greater then InTime.');
                 $(this).closest("tr").find($("[id*=lblTime]")).text('0:0');
             } else {
                 var hourDiff = (parseInt(hour2) - parseInt(hour1));
                 var minDiff = (parseInt(min2) - parseInt(min1));
                 //var hourDiff1 = (parseInt(hour4) - parseInt(hour3));
                 //var minDiff1 = (parseInt(min4) - parseInt(min3));
              
               
                 if (minDiff < 0) {
                     minDiff = 60 + minDiff;
                     hourDiff -= 1;
                 }
           
                 //if (minDiff1 < 0) {
                 //    minDiff1 = 60 + minDiff1;
                 //    hourDiff1 -= 1;
                 //}

                // hourDiff = ((hourDiff) + (hourDiff1))
             
                 //minDiff = ((minDiff) + (minDiff1))


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
            
            
         }); (function () {
             var valid = $("#aspnetForm").validationEngine('validate');
             var vars = $("#aspnetForm").serialize();
         });

         function CheckAll() {
             $('[id*=chkHeader]').attr("checked", "checked");
         }     
     </script>
</asp:Content>
