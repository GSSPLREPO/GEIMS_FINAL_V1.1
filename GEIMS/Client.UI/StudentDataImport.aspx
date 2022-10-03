<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="StudentDataImport.aspx.cs" Inherits="GEIMS.Client.UI.StudentDataImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; padding: 0 0 0 10px">
        <%-- <asp:UpdatePanel ID="upGridSchool" UpdateMode="Conditional" runat="server">
            <ContentTemplate>--%>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    School Name :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlSchoolName" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Section Name :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Class Name :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlClassName" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Division Name :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlDivisionName" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Year :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Status :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="validate[required] Droptextarea" Width="260px" Enabled="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
        </div>
        <%--  </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSchoolName" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlSection" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlClassName" EventName="SelectedIndexChanged" />             
            </Triggers>
        </asp:UpdatePanel>--%>
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Upload<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:FileUpload ID="FileUpload1" runat="server" Style="vertical-align: top" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="true" CssClass="button" Style="vertical-align: top" OnClick="btnUpload_Click" />
                    &nbsp;
					<asp:Image ID="imgUploder" runat="server" Visible="false" ImageUrl="../Images/loading.gif" />
                </div>
            </div>
        </div>
        <div id="divGrid" runat="server" style="float: left; text-align: right; width: 100%; padding-bottom: 10px; padding-top: 10px; overflow: scroll; width: 970px">
            <asp:GridView ID="gvExcelFile" runat="server" AutoGenerateColumns="false" BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" Style="font-size: 11px; font-family: Verdana;" Width="100%">
                <Columns>
                    <asp:BoundField DataField="First Name" HeaderText="First Name" />
                    <asp:BoundField DataField="Middle Name" HeaderText="Middle Name" />
                    <asp:BoundField DataField="Last Name" HeaderText="Last Name" />
                    <asp:BoundField DataField="નામ" HeaderText="પ્રથમ નામ" />
                    <asp:BoundField DataField="પિતાનુંનામ" HeaderText="પપ્પા નું નામ" />
                    <asp:BoundField DataField="અટક" HeaderText="અટક" />

                    <asp:BoundField DataField="Father's First Name" HeaderText="First Name" />
                    <asp:BoundField DataField="Father's Middle Name" HeaderText="Middle Name" />
                    <asp:BoundField DataField="Father's Last Name" HeaderText="Last Name" />
                    <asp:BoundField DataField="નામ1" HeaderText="પિતા નું પ્રથમ નામ" />
                    <asp:BoundField DataField="પિતાનુંનામ1" HeaderText="પિતા ના પિતા નું નામ" />
                    <asp:BoundField DataField="અટક1" HeaderText="પિતા ની અટક" />

                    <asp:BoundField DataField="Mother's First Name" HeaderText="First Name" />
                    <asp:BoundField DataField="Mother's Middle Name" HeaderText="Middle Name" />
                    <asp:BoundField DataField="Mother's Last Name" HeaderText="Last Name" />
                    <asp:BoundField DataField="નામ2" HeaderText="માતા નું પ્રથમ નામ" />
                    <asp:BoundField DataField="પિતાનુંનામ2" HeaderText="માતા ના પિતા નું નામ" />
                    <asp:BoundField DataField="અટક2" HeaderText="માતા ની અટક" />

                    <asp:BoundField DataField="Admission No" HeaderText="Admission No" />
                    <asp:BoundField DataField="Registration Date" HeaderText="Registration Date" />
                    <asp:BoundField DataField="Gr No" HeaderText="GR No" />
                    <asp:BoundField DataField="જાતિ" HeaderText="જાતિ" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:BoundField DataField="Date Of Birth" HeaderText="Date Of Birth" />

                    <asp:BoundField DataField="Birth District" HeaderText="Birth District" />
                    <asp:BoundField DataField="જન્મજિલ્લા" HeaderText="જન્મ શહેર" />
                    <asp:BoundField DataField="Nationality" HeaderText="Nationality" />
                    <asp:BoundField DataField="રાષ્ટ્રીયતા" HeaderText="રાષ્ટ્રીયતા" />
                    <asp:BoundField DataField="Religion" HeaderText="Religion" />
                    <asp:BoundField DataField="Caste" HeaderText="Caste" />

                    <asp:BoundField DataField="જ્ઞાતિ" HeaderText="જ્ઞાતિ" />
                    <asp:BoundField DataField="Sub Caste" HeaderText="Sub Caste" />
                    <asp:BoundField DataField="પેટા જ્ઞાતિ" HeaderText="પેટા જ્ઞાતિ" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="શ્રેણી" HeaderText="શ્રેણી" />
                    <asp:BoundField DataField="Handicap Percentage" HeaderText="Handicap Percentage" />

                    <asp:BoundField DataField="Other Defect" HeaderText="Other Defect" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="સરનામું" HeaderText="સરનામુ" />
                    <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:BoundField DataField="શહેર" HeaderText="શહેર" />
                    <asp:BoundField DataField="State" HeaderText="State" />

                    <asp:BoundField DataField="રાજ્ય" HeaderText="રાજ્ય" />
                    <asp:BoundField DataField="Pin Code" HeaderText="Pin Code" />
                    <asp:BoundField DataField="Contact No" HeaderText="Contact No" />
                    <asp:BoundField DataField="Permenant address" HeaderText="Permenant address" />
                    <asp:BoundField DataField="સરનામું1" HeaderText="કાયમી સરનામું" />
                    <asp:BoundField DataField="Permenant City" HeaderText="Permenant City" />
                    <asp:BoundField DataField="શહેર1" HeaderText="કાયમી શહેર" />


                    <asp:BoundField DataField="Permenant State" HeaderText="Permenant State" />
                    <asp:BoundField DataField="રાજ્ય1" HeaderText="કાયમી રાજ્ય" />
                    <asp:BoundField DataField="Permenant Pin Code" HeaderText="Permenant Pin Code" />

                    <asp:BoundField DataField="Permenant Contact No" HeaderText="Permenant Contact No" />
                    <asp:BoundField DataField="Father's Occupation" HeaderText="Father's Occupation" />
                    <asp:BoundField DataField="Mother's Occupation" HeaderText="Mother's Occupation" />
                    <asp:BoundField DataField="Guardian's Occupation" HeaderText="Guardian's Occupation" />

                    <asp:BoundField DataField="Father's Qualification" HeaderText="Father's Qualification" />
                    <asp:BoundField DataField="Mother's Qualification" HeaderText="Mother's Qualification" />
                    <asp:BoundField DataField="Guardian's Qualification" HeaderText="Guardian's Qualification" />


                    <asp:BoundField DataField="Father's Mobile" HeaderText="Father's Mobile" />
                    <asp:BoundField DataField="Mother's Mobile" HeaderText="Mother's Mobile" />
                    <asp:BoundField DataField="Guardian's Mobile" HeaderText="Guardian's Mobile" />

                    <asp:BoundField DataField="Father's EmailID" HeaderText="Father's EmailID" />
                    <asp:BoundField DataField="Mother's EmailID" HeaderText="Mother's EmailID" />
                    <asp:BoundField DataField="Guardian's EmailID" HeaderText="Guardian's EmailID" />
                    <asp:BoundField DataField="Height" HeaderText="Height" />
                    <asp:BoundField DataField="Weight" HeaderText="Weight" />
                    <asp:BoundField DataField="Hobbies" HeaderText="Hobbies" />
                      <asp:BoundField DataField="UniqueNo" HeaderText="UniqueNo" />
                      <asp:BoundField DataField="UniqueID" HeaderText="UniqueID" />

                    <asp:BoundField DataField="Registered Year" HeaderText="Registered Year" />

                    <asp:BoundField DataField="Mother tongue" HeaderText="Mother tongue" />
                    <asp:BoundField DataField="Previouse school detail" HeaderText="Previouse school detail" />
                    <asp:BoundField DataField="physical identification" HeaderText="physical identification" />
                    <asp:BoundField DataField="Father's Occupation" HeaderText="FatherOrganisationName" />
                    <asp:BoundField DataField="Mother's Occupation" HeaderText="FatherOrganisationContactNumber" />
                    <asp:BoundField DataField="Blood Group" HeaderText="Blood Group" />
                    <asp:BoundField DataField="Bank Name" HeaderText="Bank Name" />
                    <asp:BoundField DataField="IFSC code" HeaderText="IFSC code" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                    <asp:BoundField DataField="A/C No." HeaderText="A/C No." />
                    <asp:BoundField DataField="TypeOfVehicle" HeaderText="TypeOfVehicle" />
                    <asp:BoundField DataField="Vehicle no." HeaderText="Vehicle no." />
                    <asp:BoundField DataField="Driver name" HeaderText="Driver name" />
                    <asp:BoundField DataField="DriverContactNo" HeaderText="DriverContactNo" />
                    <asp:BoundField DataField="AadharCardNo" HeaderText="Aadhar card no." />
                    <asp:BoundField DataField="RollNumber" HeaderText="Roll Number" />
                </Columns>
                <FooterStyle BackColor="#3B5998" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle BackColor="#3B5998" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#3B5998" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" Height="20px" />
                <SelectedRowStyle BackColor="#2B558E" Font-Bold="True" ForeColor="White" />
            </asp:GridView>

        </div>

        <div style="width: 100%; float: left; padding-top: 0px;" class="label">
            <div style="padding: 10px; padding-right: 30px;">
                <div style="float: left; text-align: right; width: 100%; padding-bottom: 10px;">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn-blue-new btn-blue-medium" OnClick="btnSave_Click" />

                </div>
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
       //var totalAmount = 0, TotalDiscount = 0, Total = 0, TotalAmountnotCheck = 0;
       $('.Detach').click(function () {
           $("#aspnetForm").validationEngine('detach');
       });
      </script>
</asp:Content>
