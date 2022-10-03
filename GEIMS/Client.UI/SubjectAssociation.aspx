<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SchoolMain.Master" AutoEventWireup="true" CodeBehind="SubjectAssociation.aspx.cs" Inherits="GEIMS.Client.UI.SubjectAssociation" %>

<%@ Import Namespace="GEIMS.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $('#tab-panel').tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divCurrenTabSelected" class="hidden" visible="false">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;padding-right:10px">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Subject Association
               <div style="float: right; text-align: left;">
                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkAddNewClass_OnClick" Text="Add New" />
                                &nbsp;
                                <asp:Button ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkViewList_OnClick" Text="View List" />
                            </div>
            <%--<asp:LinkButton CausesValidation="false" ID="lnkAddNewClass" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkAddNewClass_OnClick">Add New</asp:LinkButton>
            &nbsp;
			 <asp:LinkButton CausesValidation="false" ID="lnkViewList" runat="server" CssClass="btn-blue btn-blue-medium Detach" OnClick="lnkViewList_OnClick">View List</asp:LinkButton>--%>
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
                <fieldset>
                    <legend>Subject Association</legend>
                    <div style="width: 100%; height: 30px;">
                        <div style="text-align: left; width: 20%; float: left;" class="label">
                            Class :<span style="color: red">*</span>
                        </div>
                        <div style="text-align: left; width: 80%; float: left;">
                            <asp:DropDownList runat="server" ID="ddlClass" CssClass="Droptextarea" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_OnSelectedIndexChanged" />
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div style="width: 100%; height: 30px;">
                        <div style="text-align: left; width: 20%; float: left;" class="label">
                            Division :<span style="color: red">*</span>
                        </div>
                        <div style="text-align: left; width: 80%; float: left;">
                            <div style="float: left; text-align: left;">
                                <asp:DropDownList runat="server" ID="ddlDivision" CssClass="Droptextarea" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                            </div>
                           <%-- <div style="float: right; text-align: left;">
                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkAddNewClass_OnClick" Text="Add New" />
                                &nbsp;
                                <asp:Button ID="btnViewList" runat="server" CssClass="btn-blue btn-blue-medium" OnClick="lnkViewList_OnClick" Text="View List" />
                            </div>--%>
                        </div>
                    </div>
                </fieldset>
                <div class="clear"></div>
                <div style="text-align: center; padding-top: 10px; padding-bottom: 10px; width: 100%;">
                    <asp:GridView ID="gvSubjectAssociation" runat="server" AutoGenerateColumns="False"
                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvSubjectAssociation_OnRowCommand">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="NameENG" HeaderText="Subject">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Teacher" HeaderText="Teacher">
                                <HeaderStyle Width="40%" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" Width="40%" VerticalAlign="Top" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" ImageUrl="~/Images/Edit.png"
                                        CommandName="Edit1" CommandArgument='<%# Eval("SubjectMID")%>' Height="20px" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Images/delete-1.png"
                                        CommandName="Delete1" CommandArgument='<%# Eval("SubjectMID")%>' OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
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
                <div id="tabs" runat="server">
                    <div id="tab-panel" class="style-tabs" visible="true">
                        <ul>
                            <li><a id="tabClassDetails" href="#tabs-1">Subject Association Details</a></li>
                        </ul>
                        <div id="tabs-1" style="padding:5px 5px 5px 5px" class="gradientBoxesWithOuterShadows">
                            <div style="width: 100%;">
                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Subject :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 80%; float: left;">
                                        <asp:DropDownList runat="server" ID="ddlSubject" CssClass="Droptextarea">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="divclasswithfloat">
                                    <div style="text-align: left; width: 20%; float: left;" class="label">
                                        Teacher :<span style="color: red">*</span>
                                    </div>
                                    <div style="text-align: left; width: 80%; float: left;">
                                        <asp:TextBox runat="server" ID="txtTeacher" CssClass="TextBox autosuggest"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfEmployeeMID" />
                                        <asp:HiddenField runat="server" ID="hfEmployeeCodeName" />
                                        <asp:Button runat="server" ID="btnAdd" CssClass="btn-blue-new btn-blue-medium" Text="Add" OnClick="btnAdd_OnClick" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="divclasswithoutfloat">
                                    <asp:GridView ID="gvTeacher" runat="server" AutoGenerateColumns="False"
                                        BorderColor="#3B5998" BorderWidth="3px" BorderStyle="Solid" CellPadding="4" GridLines="Both"
                                        Font-Names="verdana" Font-Size="12px" Width="100%" BackColor="White" OnRowCommand="gvTeacher_OnRowCommand">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <RowStyle BackColor="White" Height="20px" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeCodeName" HeaderText="Teacher Name">
                                                <HeaderStyle Width="300px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" Width="80%" VerticalAlign="Top" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="../Images/delete-1.png"
                                                        CommandName="DeleteTeacher" OnClientClick="javascript:return confirm('Are you sure, you want to delete this Record?');"
                                                        Height="20px" Width="20px" CssClass="Detach" CommandArgument='<%# Eval("EmployeeMID")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" />
                                        <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="center" />
                                        <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                                <div style="text-align: right;" class="divclasswithoutfloat">
                                    <asp:Button runat="server" ID="btnSaveClass" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_OnClick" />
                                </div>
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
        <%--$(document.getElementById('<%= btnSaveClass.ClientID %>')).click(function () {
            var valid = $("#aspnetForm").validationEngine('validate');
            var vars = $("#aspnetForm").serialize();
        });--%>

        $(document).ready(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SubjectAssociation.aspx/GetAllEmployeeName",
                        data: "{'prefixText':'" + request.term + "','schoolMID':'" +<%=Session[ApplicationSession.SCHOOLID].ToString()%> +"'}",
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
                    $("#<%=hfEmployeeMID.ClientID %>").val(i.item.val);
                    $("#<%=hfEmployeeCodeName.ClientID %>").val(i.item.label);
                }
            });
        });

        $("#<%= btnAddNew.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").addClass("validate[required]");
            $("#<%=ddlDivision.ClientID%>").addClass("validate[required]");
            $("#<%=ddlSubject.ClientID%>").removeClass("validate[required]");
            $("#<%=txtTeacher.ClientID%>").removeClass("validate[required]");
        });

        $("#<%= btnViewList.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").addClass("validate[required]");
            $("#<%=ddlDivision.ClientID%>").addClass("validate[required]");
            $("#<%=ddlSubject.ClientID%>").removeClass("validate[required]");
            $("#<%=txtTeacher.ClientID%>").removeClass("validate[required]");
        });

        $("#<%= btnAdd.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").removeClass("validate[required]");
            $("#<%=ddlDivision.ClientID%>").removeClass("validate[required]");
            $("#<%=ddlSubject.ClientID%>").removeClass("validate[required]");
            $("#<%=txtTeacher.ClientID%>").addClass("validate[required]");
        });

        $("#<%= btnSaveClass.ClientID%>").click(function () {
            $("#<%=ddlClass.ClientID%>").addClass("validate[required]");
            $("#<%=ddlDivision.ClientID%>").addClass("validate[required]");
            $("#<%=ddlSubject.ClientID%>").addClass("validate[required]");
            $("#<%=txtTeacher.ClientID%>").removeClass("validate[required]");
        });

    </script>
</asp:Content>
