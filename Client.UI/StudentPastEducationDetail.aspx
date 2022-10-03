<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="StudentPastEducationDetail.aspx.cs" Inherits="GEIMS.Client.UI.StudentPastEducationDetail" %>

<%@ Import Namespace="GEIMS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script type="text/javascript">
        $(document).ready(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "StudentPastEducationDetail.aspx/GetAllStudentNameForReport",
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
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Pre-Education Detail
            <%--<asp:LinkButton CausesValidation="false" ID="btnAddClassTemplate" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="bbtnAddClassTemplate_Click">Add New</asp:LinkButton>--%>
			&nbsp;
			 <%--<asp:LinkButton CausesValidation="false" ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="btnViewList_Click">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                    <ul>
                        <li><a id="tabStudentTemplateDetails" href="#tabs-1">Pre-Education</a></li>
                    </ul>
                    <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">

                        <div style="width: 100%">
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
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="textarea autosuggest"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:HiddenField runat="server" ID="hfSearchName" />
                                    </div>
                                    <div style="width: 20%; float: left; text-align: right">
                                        <asp:Button ID="btnGo" runat="server" CssClass="btn-blue-new Detach" Width="50px" Text="Search" CausesValidation="false"
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
                                        <asp:TemplateField HeaderText="Add">
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
                        <div id="divStudentPanel" runat="server">
                            <div style="width: 100%;">
                                <fieldset>
                                    <legend>Student Details</legend>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Admission No:
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:Label runat="server" ID="lblAdmissionNo"></asp:Label>&nbsp;
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
                            </div>
                            <div style="width: 100%;" id="divDetails" runat ="server">
                                <fieldset>
                                    <legend>Add Details</legend>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left; height: 14px;" class="label">
                                            School Name:<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 80%; float: left;">
                                            <asp:TextBox ID="txtSchoolName" runat="server" CssClass="validate[required] TextBox" Width="60%" Height="100%"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="divclasswithfloat">
                                        <div style="width: 20%; float: left" class="label">
                                            School Address:
                                        </div>
                                        <div style="width: 80%; float: left">
                                            <asp:TextBox ID="txtSchoolAddress" runat="server" CssClass="TextArea" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Board Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtBoardName" runat="server" CssClass="validate[required] TextBox" Width="60%"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Medium Name :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtMediumName" runat="server" CssClass="validate[required] TextBox" Width="60%" Height="100%"></asp:TextBox>
                                        </div>
                                    </div>
									  <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Town :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtTown" runat="server" CssClass="validate[required] TextBox" Width="60%"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Taluka :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtTaluka" runat="server" CssClass="validate[required] TextBox" Width="60%" Height="100%"></asp:TextBox>
                                        </div>
                                    </div>
									  <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                         District :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="validate[required] TextBox" Width="60%"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            State :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtState" runat="server" CssClass="validate[required] TextBox" Width="60%" Height="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="divclasswithfloat">
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Passed Exam :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:TextBox ID="txtPassedExam" runat="server" CssClass="validate[required] TextBox" Width="60%"></asp:TextBox>
                                        </div>
                                        <div style="text-align: left; width: 20%; float: left;" class="label">
                                            Passing Year :<span style="color: red">*</span>
                                        </div>
                                        <div style="text-align: left; width: 30%; float: left;">
                                            <asp:DropDownList ID="ddlPassingYear" runat="server" CssClass="Droptextarea" Width="160px" Enabled="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="width: 100%" class="divclasswithoutfloat" id="divgvEducation" runat="server">
                                <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="False"
                                    BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both" OnRowCommand="gvEducation_RowCommand"
                                    Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="SchoolName" HeaderText="School Name">
                                            <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="50%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PassedExam" HeaderText="Passed Exam">
                                            <HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PassingYear" HeaderText="Passing Year">
                                            <HeaderStyle Width="200px" HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" Width="25%" VerticalAlign="Top" Wrap="true" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                                    CommandName="Edit1" CommandArgument='<%# Eval("StudentEducationDetailTID")%>' CssClass="Detach" Height="20px" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CssClass="Detach" ImageUrl="~/Images/delete-1.png"
                                                    CommandName="Delete1" CommandArgument='<%# Eval("StudentEducationDetailTID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                            <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">
                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />
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
        $("#<%=ddlSearchBy.ClientID %>").change(function () {
            $("#<%=txtSearchName.ClientID %>").val(null);
        });
        $('.Detach').click(function () {
            $("#aspnetForm").validationEngine('detach');
        });
    </script>
</asp:Content>


