<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EventCategory.aspx.cs" Inherits="GEIMS.Events.EventCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });

         function charName(e) {
             var unicode = e.charCode ? e.charCode : e.keyCode;
             if (unicode == 8 || unicode == 9 || unicode == 32 || unicode == 34 || unicode == 39 || unicode == 95 || (unicode >= 48 && unicode <= 57)
                 || (unicode >= 64 && unicode <= 90) || (unicode >= 97 && unicode <= 122)) {
                 return true;
             }
             else {
                 return false;
             }
         }

         
         function charCategory(e) {
             var unicode = e.charCode ? e.charCode : e.keyCode;
             if (unicode == 8 || unicode == 9 || unicode == 32 || (unicode >= 65 && unicode <= 90) || (unicode >= 97 && unicode <= 122)) {
                 return true;
             }
             else {
                 return false;
             }
         }

         // Charater Length
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
             $('#id_search').quicksearch('table#<%=gvEventCategory.ClientID%> tbody tr');
         })
     </script>



    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Event Category
            <asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="divGrid" runat="server" style="text-align: center; padding-top: 10px; padding-bottom: 10px; padding-right: 10px; width: 100%;">
                    <div id="search">
                        <input id="id_search" type="text" placeholder="Search" onkeydown="return (event.keyCode!=13);">
                    </div>
                    <br />
                    <asp:GridView ID="gvEventCategory" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvEventCategory_RowCommand" OnSelectedIndexChanged="gvEventCategory_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" Width="100px" />
                        <Columns>
                               <asp:BoundField DataField="EventCategoryName"  HeaderText="Event Cateogry Name" ReadOnly="True">
                                <HeaderStyle Width="20%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="20%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EventDescription"  HeaderText="Event Description" ReadOnly="True">
                                <HeaderStyle Width="70%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="70%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                       

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("EventCategoryID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("EventCategoryID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                        Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="6%" />
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
                        <li><a id="tabClassDetails" href="#tabs-1">Event Category</a></li>
                    </ul>
                    <div id="tabs-1" style="height: 150px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                       
                        <div style="width: 100%;">
                            <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Event Category :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 30%; float: left;">
                                   <asp:TextBox ID="txtEventCategory" onKeyUp="CheckTextLength(this,50)" runat="server" onkeypress="return charCategory(event);" CssClass="validate[required] TextBox" Width="150%"></asp:TextBox>
                               
                                </div>
                            </div>

                             <div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                    Description :
                                </div>
                                <div style="text-align: left; float: left; width: 80%;">
                                    <asp:TextBox ID="txtDescription" runat="server" onKeyUp="CheckTextLength(this,5000)" CssClass="TextArea" Width="93%" TextMode="MultiLine"></asp:TextBox>
                                </div>

                            </div><br />

                         

                            <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" />
                            </div>
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
        $(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });
    </script>
</asp:Content>
