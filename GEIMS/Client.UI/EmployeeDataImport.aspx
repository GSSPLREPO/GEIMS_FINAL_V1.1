<%@ Page Title="" Language="C#" MasterPageFile="~/Master/TrustMain.Master" AutoEventWireup="true" CodeBehind="EmployeeDataImport.aspx.cs" Inherits="GEIMS.Client.UI.EmployeeDataImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 0 0 0 10px; width: 100%">
        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Select :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlOrganisation" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlOrganisation_SelectedIndexChanged">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>Trust</asp:ListItem>
                        <asp:ListItem>School</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
        </div>

        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Trust/School Name :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlOrgName" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

            </div>
        </div>

        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Department Name :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true">
                    </asp:DropDownList>
                </div>

            </div>
        </div>

        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Designation :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddldesignation" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true">
                    </asp:DropDownList>
                </div>

            </div>
        </div>

        <div style="width: 100%; float: left;" class="label">
            <div style="padding: 10px;">
                <div style="float: left; width: 15%;">
                    Employee Role :<span style="color: red">*</span>
                </div>
                <div style="float: left; width: 85%;">
                    <asp:DropDownList ID="ddlEmployeeRole" runat="server" CssClass=" Droptextarea" Width="260px" Enabled="true">
                    </asp:DropDownList>
                </div>

            </div>
        </div>

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
        <div id="divGrid" runat="server" visible="false" style="float: left; text-align: center; width: 100%; padding-bottom: 10px; overflow: scroll; width: 970px">
            <asp:GridView ID="gvExcelFile" Visible="false" runat="server" AutoGenerateColumns="false" BorderColor="#3B5998" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" Style="font-size: 11px; font-family: Verdana;" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="EmployeeCode" DataField="EmployeeCode" />
                    <asp:BoundField HeaderText="First Name" DataField="First Name" />
                    <asp:BoundField HeaderText="નામ" DataField="નામ" />
                    <asp:BoundField HeaderText="Middle Name" DataField="Middle Name" />
                    <asp:BoundField HeaderText="પિતાનુંનામe" DataField="પિતાનુંનામ" />
                    <asp:BoundField HeaderText="Last Name" DataField="Last Nmae" />
                    <asp:BoundField HeaderText="અટક" DataField="અટક" />

                    <asp:BoundField HeaderText="Gender" DataField="Gender" />
                    <asp:BoundField HeaderText="જાતિ" DataField="જાતિ" />
                    <asp:BoundField HeaderText="DateOfBirth" DataField="DateOfBirth" />
                    <asp:BoundField HeaderText="Birth District" DataField="Birth District" />
                    <asp:BoundField HeaderText="જન્મજિલ્લા" DataField="જન્મજિલ્લા" />
                    <asp:BoundField HeaderText="Birth Taluka" DataField="Birth Taluka" />

                    <asp:BoundField HeaderText="જન્મ તાલુકા" DataField="જન્મતાલુકા" />
                    <asp:BoundField HeaderText="Birth City/Village" DataField="Birth City/Village" />
                    <asp:BoundField HeaderText="જન્મ શહેર" DataField="જન્મશહેર" />
                    <asp:BoundField HeaderText="Nationality" DataField="Nationality" />
                    <asp:BoundField HeaderText="રાષ્ટ્રીયતા" DataField="રાષ્ટ્રીયતા" />
                    <asp:BoundField HeaderText="Religion" DataField="Religion" />

                    <asp:BoundField HeaderText="ધર્મ" DataField="ધર્મ" />
                    <asp:BoundField HeaderText="Caste" DataField="Caste" />
                    <asp:BoundField HeaderText="Marital Status" DataField="Marital Status" />
                    <asp:BoundField HeaderText="Blood Group" DataField="Blood Group" />
                    <asp:BoundField HeaderText="Mother Language" DataField="Mother Language" />
                    <asp:BoundField HeaderText="Present Address" DataField="Address" />

                    <asp:BoundField HeaderText="સરનામું" DataField="સરનામું" />
                    <asp:BoundField HeaderText="Land Mark" DataField="Land Mark" />
                    <asp:BoundField HeaderText="સીમાચિહ્ન" DataField="સીમાચિહ્ન" />
                    <asp:BoundField HeaderText="City" DataField="City" />
                    <asp:BoundField HeaderText="શહેર" DataField="શહેર" />
                    <asp:BoundField HeaderText="State" DataField="State" />

                    <asp:BoundField HeaderText="રાજ્ય" DataField="રાજ્ય" />
                    <asp:BoundField HeaderText="Pin Code" DataField="PinCode" />
                    <asp:BoundField HeaderText="Permenant Address" DataField="Address1" />

                    <asp:BoundField HeaderText="સરનામું" DataField="સરનામું1" />
                    <asp:BoundField HeaderText="Land Mark" DataField="Land Mark1" />
                    <asp:BoundField HeaderText="સીમાચિહ્ન" DataField="સીમાચિહ્ન1" />
                    <asp:BoundField HeaderText="City" DataField="City1" />
                    <asp:BoundField HeaderText="શહેર" DataField="શહેર1" />
                    <asp:BoundField HeaderText="State" DataField="State1" />

                    <asp:BoundField HeaderText="રાજ્ય" DataField="રાજ્ય1" />
                    <asp:BoundField HeaderText="Pin Code" DataField="PinCode1" />
                    <asp:BoundField DataField="Telephone No" HeaderText="Telephone No" />
                    <asp:BoundField DataField="Mobile No" HeaderText="Mobile No" />
                    <asp:BoundField DataField="Email Id" HeaderText="Email Id" />
                    <asp:BoundField DataField="Hobbies" HeaderText="Hobbies" />

                    <asp:BoundField DataField="RightVision" HeaderText="RightVision" />
                    <asp:BoundField DataField="LeftVision" HeaderText="LeftVision" />                  
                    <asp:BoundField DataField="Height" HeaderText="Height" />
                    <asp:BoundField DataField="Weight" HeaderText="Weight" />

                    <asp:BoundField DataField="Physical Identification" HeaderText="Physical Identification" />
                    <asp:BoundField DataField="શારીરિક ઓળખ" HeaderText="શારીરિક ઓળખ" />
                    <asp:BoundField HeaderText="MFirst Name" DataField="First Name1" />
                    <asp:BoundField HeaderText="MFirst Name" DataField="નામ1" />
                    <asp:BoundField HeaderText="MMiddle Name" DataField="Middle Name1" />
                    <asp:BoundField HeaderText="MiddleName" DataField="પિતાનુંનામ1" />
                    <asp:BoundField HeaderText="MLast Name" DataField="Last Name1" />
                    <asp:BoundField HeaderText="MLast Name" DataField="અટક1" />

                    <asp:BoundField DataField="Bank Name" HeaderText="Bank Name" />
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name" />
                    <asp:BoundField DataField="Branch Code" HeaderText="Branch Code" />
                    <asp:BoundField DataField="Account No" HeaderText="Account No" />
                    <asp:BoundField DataField="PF No" HeaderText="PF No" />
                    <asp:BoundField DataField="PAN No" HeaderText="PAN No" />
                    <asp:BoundField DataField="ESIC No" HeaderText="ESIC No" />
                    <asp:BoundField DataField="IFSC Code" HeaderText="IFSC Code" />
                    <asp:BoundField DataField="GPF Account No" HeaderText="GPF Account No" />
                    <asp:BoundField DataField="CPF Account No" HeaderText="CPF Account No" />
                    <asp:BoundField DataField="Department Joining Date" HeaderText="Department Joining Date" />
                    <asp:BoundField DataField="Organisation Joining Date" HeaderText="Organisation Joining Date" />

                    <asp:BoundField DataField="ReplacementSchoolInfoENG" HeaderText="ReplacementSchoolInfoENG" />
                    <asp:BoundField DataField="ReplacementSchoolInfoGUJ" HeaderText="ReplacementSchoolInfoGUJ" />

                    <asp:BoundField DataField="Retirement Date" HeaderText="Retirement Date" />
                    <asp:BoundField DataField="Term End Retirement Date" HeaderText="TermEnd Retirement Date" />

                    <asp:BoundField DataField="Break Info" HeaderText="Break Info" />
                    <asp:BoundField DataField="કારકિર્દી બ્રેક માહિતી" HeaderText="કારકિર્દી બ્રેક માહિતી" />

                    <asp:BoundField DataField="Other Achivement Details" HeaderText="Other Achivement Details" />
                    <asp:BoundField DataField="સિદ્ધિ માહિતી" HeaderText="સિદ્ધિ માહિતી" />
                    <asp:BoundField DataField="IsUser" HeaderText="IsUser" />

                    <asp:BoundField DataField="User Name" HeaderText="User Name" />
                    <asp:BoundField DataField="password" HeaderText="password" />
                    <asp:BoundField DataField="IsTeacher" HeaderText="IsTeacher" />

                    <asp:BoundField DataField="IsPrincipal" HeaderText="IsPrincipal" />
                    <asp:BoundField DataField="Allow Account Access" HeaderText="Allow Account Access" />

                    <asp:BoundField DataField="TypeOfAppointment" HeaderText="TypeOfAppointment" />
                    <asp:BoundField DataField="TypeOfAppointmentCode" HeaderText="TypeOfAppointmentCode" />
                    <asp:BoundField DataField="AadarCardNo" HeaderText="AadarCardNo" />
                    <asp:BoundField DataField="ElectionCardNo" HeaderText="ElectionCardNo" />
                    <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                    <asp:BoundField DataField="PortalID" HeaderText="PortalID" />
                    <asp:BoundField DataField="PRANNo" HeaderText="PRANNo" />
                    <asp:BoundField DataField="TANNO" HeaderText="TANNO" />


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
</asp:Content>
