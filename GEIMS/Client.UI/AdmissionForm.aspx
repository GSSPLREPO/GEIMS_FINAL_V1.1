    <%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="AdmissionForm.aspx.cs" Inherits="GEIMS.Client.UI.AdmissionForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {
            $(document.getElementById('<%= tabs.ClientID %>')).tabs();
        });
    </script>
   
 
   <script type="text/javascript">
       function newSrc(newlink) {
           //var e = document.getElementById("MySelectMenu");
           //var newSrc = e.options[e.selectedIndex].value;
           document.getElementById("MyFrame").src = newlink;
       
       }
   </script>

    <style>
        .TextArea {
            max-width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

    <div id="divCurrenTabSelected" class="hidden" visible="true">div1</div>
    <div id="divMain" style="width: 100%; text-align: left; padding-top: 5px;">
        <div id="divTitle" class="pageTitle" style="width: 100%;">
            Admission Form        
        </div>
        <div id="divContent" style="height: 100%; font-family: Verdana;">
            <div id="divContent1" style="width: 10%; float: left; height: 100%; color: white;">1</div>
            <div id="divContent2" style="width: 80%; float: left; height: 100%;">
             <%--<div id="tabs" runat="server">--%>
                <div id="tabs" runat="server" class="style-tabs" visible="true" style="width: 100%;">
                   <ul>
                        <li><a id="tabClassDetails" href="#tabs-1">Admission Form</a></li>
                   </ul>
                   <div id="tabs-1" style="height: 150px; padding: 5px 5px 5px 5px;" class="gradientBoxesWithOuterShadows">
                               <%--Height : 559Px--%>
                            <div style="width: 100%;">
                               <div style="width: 100%;" class="divclasswithfloat">
                                       <div style="text-align: left; width: 20%; float: left;" class="label">
                                           School Name :<span style="color: red">*</span>
                                       </div>
                                       <div style="text-align: left; width: 30%; float: left;">
                                           <asp:DropDownList ID="ddlSchoolName" runat="server" CssClass=" validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolName_SelectedIndexChanged">
                                           </asp:DropDownList>
                                       </div>
                                       <div style="text-align: left; width: 20%; float: left;" class="label">
                                           Section Name :<span style="color: red">*</span>
                                       </div>
                                       <div style="text-align: left; float: right; width: 30%;">
                                           <asp:DropDownList ID="ddlSection" runat="server" CssClass=" validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                           </asp:DropDownList>
                                       </div>
                                   </div>
                                 </div>
                                   <%--  <div class="divclasswithfloat">
                               <div style="text-align: left; width: 20%; float: left;" class="label" visible="False">
                                     Select Language :<span style="color: red">*</span>
                                </div>
                                <div style="text-align: left; width: 30%; float: left;">
                                    <asp:DropDownList ID="ddlMedium" runat="server" CssClass=" validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" Visible="false">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">English</asp:ListItem>
                                        <asp:ListItem Value="2">Gujarati</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                    
                                </div>--%>
                               </div>
                               <div style="width: 100%; text-align: right; margin:-35px;" class="divclasswithoutfloat">

                                   <asp:Button runat="server" ID="BtnPreview" Text="Preview & Print" CssClass="btn-blue-new btn-blue-medium" OnClick="btnPreview_Click" />
                                   <%--</script>--%>
                                   <%--<asp:Button runat="server" ID="btnSaveClass" Text="Print" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" />--%>
                               </div>

                               <%--<div class="divclasswithfloat">
                                <div style="text-align: left; width: 20%; float: left;" class="label">
                                   Print Preview:<span style="color: red">*</span>
                                  <iframe src="" style="width:500%;height:450px;overflow:scroll;" id="MyFrame"></iframe>
                                    
                                        
                                     
                                </div>
                                
                            </div>--%>

                               <div style="width: 100%; text-align: right;" class="divclasswithoutfloat">

                                   <asp:Button runat="server" ID="btnSaveClass" Text="Print" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSaveClass_Click" Visible="False" />
                               </div>
                
             </div>
                 <%--</div>--%>
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
