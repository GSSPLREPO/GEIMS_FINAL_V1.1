    using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;    
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class Employee : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(Employee));
        string GENDER = "";

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                //  ViewState["Bytes"] = null;
                if(!IsPostBack)
                {
                    ddlSearchBy.SelectedIndex = 1;
                }

                if (fuImage.HasFile)
                {
                    // Comments added on 04/10/2022 by Bhandavi
                    //if image selected, show in image photo field
                    string Extension;
                    Extension = Path.GetExtension(fuImage.FileName);
                    if (Extension == ".jpg" || Extension == ".JPG" || Extension == ".jpeg" || Extension == ".JPEG" || Extension == ".bmp" || Extension == ".BMP" || Extension == ".gif" || Extension == ".GIF" || Extension == ".png" || Extension == ".PNG")
                    {
                        if (System.IO.File.Exists(Server.MapPath("../Logo/EmployeePhoto/") + txtEmployeeFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension))
                        {
                            System.IO.File.Delete(Server.MapPath("../Logo/EmployeePhoto/") + txtEmployeeFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension);
                        }
                        fuImage.PostedFile.SaveAs(Server.MapPath("../Logo/EmployeePhoto/") + txtEmployeeFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension);
                        //objSchoolBo.SchoolLogo = txtSchoolName.Text.Replace(" ", "").ToString() + Extension;
                        imgphoto.ImageUrl = "../Logo/EmployeePhoto/" + txtEmployeeFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension;
                        // ViewState["strPhoto"] = "../Logo/EmployeePhoto/" + txtEmployeeFirstName.Text.Replace(" ", "").ToString() +System.DateTime.Now.ToLongDateString()+ Extension;
                        Byte[] bytes = (byte[])ImageToByteArrayFromFilePath("../Logo/EmployeePhoto/" + txtEmployeeFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension);
                        ViewState["Bytes"] = bytes;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('File format not supported');</script>");
                    }
                }
                else
                {
                    imgphoto.ImageUrl = "../Images/NoImage-big.jpg";
                }
                BindReportingTo();
                if (!Page.IsPostBack)
                {
                    //Manage_Student(2);
                    divModal.Visible = false;
                    gvEmpExpirence.Visible = false;
                    gvEmpFamily.Visible = false;
                    gvEmployeeQualification.Visible = false;
                    BindYear();
                    ViewState["Mode"] = "Save";
                    ViewState["EmployeeMID"] = 0;
                    PanelGrid_VisibilityMode(1);
                    txtDateOfBirth.Attributes.Add("readonly", "readonly");
                    txtDeptJoinDate.Attributes.Add("readonly", "readonly");
                    txtOrgDate.Attributes.Add("readonly", "readonly");
                    txtRetirementDate.Attributes.Add("readonly", "readonly");
                    txtTermRetireDate.Attributes.Add("readonly", "readonly");
                    txtResignDate.Attributes.Add("readonly", "readonly");
                    txtLastWorkingDate.Attributes.Add("readonly", "readonly");
                    txtRemakrsDate.Attributes.Add("readonly", "readonly");
                    string Serverpath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                    string sDirPath = Server.MapPath(Serverpath);
                    hdnUploadFilePath.Value = sDirPath;
                    hfTab.Value = "0";

                    if (Session[ApplicationSession.USERID] != null || Session[ApplicationSession.USERID] != "")
                    {

                        hdnLastUserID.Value = Session[ApplicationSession.USERID].ToString();
                    }
                }     
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));

                ViewState["EmployeeMID"] = null;

                PanelGrid_VisibilityMode(1);

                divGridPanel.Visible = true;

                ddlSearchBy.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region AddNew Button
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                PanelGrid_VisibilityMode(2);
                ClearAll();
                //Manage_Student(2);
                //lblMessage.Visible = false;
                hfTab.Value = "0";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Save Click
        /// <summary>
        /// Update code(Added Condition To check gender) for Gender selection match with Gujarti Gender selection
        /// </summary>
        /// <Developer Name="Yogesh Patel"></param>
        /// <Date="17-04-2022"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                EmployeeMBO objEmployeeBo = new EmployeeMBO();
                ApplicationResult objResults = new ApplicationResult();
                EmployeeMBL objEmployeeBl = new EmployeeMBL();

                EmployeeDeptTransferHistoryTBO objEmployeeDeptTransferHistoryBO = new EmployeeDeptTransferHistoryTBO();
                ApplicationResult objEmpDeptTransferHistoryResults = new ApplicationResult();

                Controls objControls = new Controls();
                
                objEmployeeBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                if (rblSelect.SelectedValue == "0")
                {
                    objEmployeeBo.SchoolMID = Convert.ToInt32(Request.Form[ddlSchool.UniqueID]);
                }
                else
                {
                    objEmployeeBo.SchoolMID = 0;
                }
                objEmployeeBo.DepartmentID = Convert.ToInt32(Request.Form[ddlDepartment.UniqueID]);
                objEmployeeBo.DesignationID = Convert.ToInt32(Request.Form[ddlDesignation.UniqueID]);
                objEmployeeBo.ReportingTo = Convert.ToInt32(Request.Form[ddlReportingTo.UniqueID]);
                objEmployeeBo.EmployeeCode = txtEmployeeCode.Text;
                objEmployeeBo.EmployeeFNameENG = txtEmployeeFirstName.Text;
                objEmployeeBo.EmployeeFNameGUJ = txtEmployeeFirstNameGuj.Text;
                objEmployeeBo.EmployeeLNameENG = txtEmployeeLastName.Text;
                objEmployeeBo.EmployeeLNameGUJ = txtEmployeeLastNameGuj.Text;
                objEmployeeBo.EmployeeMNameENG = txtEmployeeMiddleName.Text;
                objEmployeeBo.EmployeeMNameGUJ = txtEmployeeMiddleNameGuj.Text;
                if (ViewState["Bytes"] != null)
                {
                    objEmployeeBo.Photo = (byte[])ViewState["Bytes"];
                }
                else if (Session["PicsName"] != null)
                {
                    objEmployeeBo.Photo = ImageToByteArrayFromFilePath("../Logo/EmployeePhoto/" + Session["PicsName"].ToString());
                }
                else
                {
                    objEmployeeBo.Photo = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                }
                
                //if (rblGender.SelectedValue=="MALE")


                //if (rblGender.SelectedValue != "MALE" && rblGenderGuj.SelectedValue != "પુરૂષ" || (rblGender.SelectedValue != "FEMALE" && rblGenderGuj.SelectedValue != "સ્ત્રી"))
                //{
                //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Gender should not be diffrent');</script>");

                //}
                //else 
                //{
                 
                    objEmployeeBo.Gender = rblGender.SelectedItem.ToString();
                    objEmployeeBo.GenderGuj = rblGenderGuj.SelectedItem.ToString();
                    objEmployeeBo.DateOfBirth = txtDateOfBirth.Text;
                    objEmployeeBo.BirthDistrictENG = txtBirthDistrict.Text;
                    objEmployeeBo.BirthDistrictGUJ = txtBirthDistrictGuj.Text;
                    objEmployeeBo.BirthTalukaENG = txtBirthTaluka.Text;
                    objEmployeeBo.BirthTalukaGUJ = txtBirthTalukaGuj.Text;
                    objEmployeeBo.BirthCityVillageENG = txtBirthCity.Text;
                    objEmployeeBo.BirthCityVillageGUJ = txtBirthCityGuj.Text;
                    objEmployeeBo.NationalityENG = txtNationality.Text;
                    objEmployeeBo.NationalityGUJ = txtNationalityGuj.Text;
                    objEmployeeBo.ReligionENG = txtReligion.Text;
                    objEmployeeBo.ReligionGUJ = txtReligionGuj.Text;
                    objEmployeeBo.Caste = txtCaste.Text;
                    objEmployeeBo.MaritalStatus = txtMaritalStatus.Text;
                    objEmployeeBo.BloodGroup = txtBloodGroup.Text;
                    objEmployeeBo.MotherLanguage = txtMotherToungue.Text;
                    objEmployeeBo.CurrentAddressENG = txtCurAddress.Text;
                    objEmployeeBo.CurrentAddressGUJ = txtCorrespondenceAddressGuj.Text;
                    objEmployeeBo.CurrentLandmarkENG = txtCurLandMark.Text;
                    objEmployeeBo.CurrentLandmarkGUJ = txtCurLandMarkGuj.Text;
                    objEmployeeBo.CurrentCityENG = txtCurCity.Text;
                    objEmployeeBo.CurrentCityGUJ = txtCorrespondenceCityGuj.Text;
                    objEmployeeBo.CurrentStateENG = txtCurState.Text;
                    objEmployeeBo.CurrentStateGUJ = txtCorrespondenceStateGuj.Text;
                    objEmployeeBo.CurrentPinCode = txtCurPinCode.Text;
                    objEmployeeBo.PermenantAddressEng = txtPermenantAddress.Text;
                    objEmployeeBo.PermenantAddressGuj = txtPermenantAddressGuj.Text;
                    objEmployeeBo.PermenantCityEng = txtPermenantCity.Text;
                    objEmployeeBo.PermenantCityGuj = txtPermenantCityGuj.Text;
                    objEmployeeBo.PermenantLandmarkEng = txtPermenantLandMark.Text;
                    objEmployeeBo.PermenantLandmarkGuj = txtPermenantLandMarkGuj.Text;
                    objEmployeeBo.PermenantStateEng = txtPermenantState.Text;
                    objEmployeeBo.PermenantStateGuj = txtPermenantStateGuj.Text;
                    objEmployeeBo.PermenantPincode = txtPermenantPinCode.Text;
                    objEmployeeBo.TelephoneNo = txtPhoneNo.Text;
                    objEmployeeBo.MobileNo = txtMobileNo.Text;
                    objEmployeeBo.EmailId = txtEmailID.Text;
                    objEmployeeBo.Hobbies = txtHobbies.Text;
                    objEmployeeBo.RightVision = txtRightVision.Text;
                    objEmployeeBo.LeftVision = txtLeftVision.Text;
                    objEmployeeBo.RectificationDevice = txtRectificationDevice.Text;
                    objEmployeeBo.Height = txtHeight.Text;
                    objEmployeeBo.Weight = txtWeight.Text;
                    objEmployeeBo.PhysicalIdentificationENG = txtPhysicalIdentification.Text;
                    objEmployeeBo.PhysicalIdentificationGUJ = txtPhysicalIdentificationGuj.Text;
                    objEmployeeBo.MotherMaidenFNameENG = txtMMFirstName.Text;
                    objEmployeeBo.MotherMaidenFNameGUJ = txtMotherFirstNameGuj.Text;
                    objEmployeeBo.MotherMaidenLNameENG = txtMMLastName.Text;
                    objEmployeeBo.MotherMaidenLNameGUJ = txtMotherLastNameGuj.Text;
                    objEmployeeBo.MotherMaidenMNameENG = txtMMMiddleName.Text;
                    objEmployeeBo.MotherMaidenMNameGUJ = txtMotherMiddleNameGuj.Text;
                    objEmployeeBo.BankName = txtBankName.Text;
                    objEmployeeBo.BranchName = txtBranchName.Text;
                    objEmployeeBo.BranchCode = txtBranchCode.Text;
                    objEmployeeBo.AccountNo = txtAccNo.Text;
                    objEmployeeBo.PFNo = txtPFNo.Text;
                    objEmployeeBo.PANNo = txtPanNo.Text;
                    objEmployeeBo.ESICNo = txtEsicNo.Text;
                    objEmployeeBo.IFSCCode = txtIfscCode.Text;
                    objEmployeeBo.GPFAccountNo = txtGpfNo.Text;
                    objEmployeeBo.CPFAccountNo = txtCpfNo.Text;
                    objEmployeeBo.DepartmentJoiningDate = txtDeptJoinDate.Text;
                    objEmployeeBo.OrganisationJoiningDate = txtOrgDate.Text;
                    objEmployeeBo.TypeOfAppointment = rblAppointmentType.SelectedItem.ToString();
                    objEmployeeBo.ReplacementSchoolInfoENG = txtSchoolInfo.Text;
                    objEmployeeBo.ReplacementSchoolInfoGUJ = txtSchoolReplacementInfoGuj.Text;
                    objEmployeeBo.RetirementDate = txtRetirementDate.Text;
                    objEmployeeBo.TermEndRetirementDate = txtTermRetireDate.Text;
                    objEmployeeBo.LASTWORKINGDATE = txtLastWorkingDate.Text;


                    if (chkIsResign.Checked == true)
                    {

                        objEmployeeBo.IsResigned = 1;

                        objEmployeeBo.IsDeleted = 1;
                    }
                    //else if(chkIsResign.Checked == true && txtLastWorkingDate.Text != DateTime.Now.ToString("dd/MM/yyyy"))
                    // {
                    //    objEmployeeBo.IsResigned = 1;

                    //     objEmployeeBo.IsDeleted = 0;
                    // }

                    else
                    {
                        objEmployeeBo.IsResigned = 0;

                    }
                    objEmployeeBo.ResignedDate = txtResignDate.Text;
                    objEmployeeBo.LASTWORKINGDATE = txtLastWorkingDate.Text;
                    objEmployeeBo.ResignReasonEng = txtResignReason.Text;
                    objEmployeeBo.ResignReasonGuj = txtResignReasonGuj.Text;
                    objEmployeeBo.BreakInfoENG = txtBreakInfo.Text;
                    objEmployeeBo.BreakInfoGUJ = txtBreakInfoGuj.Text;
                    objEmployeeBo.OtherAchivementDetailsENG = txtAchieveMentInfo.Text;
                    objEmployeeBo.OtherAchivementDetailsGUJ = txtAchievementInfoGuj.Text;
                    if (chkIsUser.Checked == true)
                    {
                        objEmployeeBo.IsUser = 1;

                    }
                    else
                    {
                        objEmployeeBo.IsUser = 0;
                    }
                    objEmployeeBo.UserName = txtUserName.Text;
                    objEmployeeBo.Password = txtPassWord.Text;
                    objEmployeeBo.RoleID = Convert.ToInt32(Request.Form[ddlRole.UniqueID]);
                    if (rblTeacher.SelectedItem.Text == "Teacher")
                    {
                        objEmployeeBo.IsTeacher = 1;
                        objEmployeeBo.IsPrincipal = 0;
                    }
                    else if (rblTeacher.SelectedItem.Text == "Principal")
                    {
                        objEmployeeBo.IsTeacher = 0;
                        objEmployeeBo.IsPrincipal = 1;
                    }
                    else
                    {
                        objEmployeeBo.IsTeacher = 0;
                        objEmployeeBo.IsPrincipal = 0;
                    }
                    if (chkAlowance.Checked == true)
                    {
                        objEmployeeBo.AllowAccountAccess = 1;
                    }
                    else
                    {
                        objEmployeeBo.AllowAccountAccess = 0;
                    }
                    objEmployeeBo.CategoryEng = rblCategory.SelectedItem.ToString();
                    objEmployeeBo.CategoryGuj = rblCategoryGuj.SelectedItem.ToString();
                    objEmployeeBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objEmployeeBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objEmployeeBo.TypeOfAppointmentCode = txtAppointmentTypeCode.Text;
                    objEmployeeBo.AaharCardNo = txtAadharCardNo.Text;
                    objEmployeeBo.ElectionCardNo = txtElectionCardNo.Text;
                    objEmployeeBo.VehicleNo = txtVehicleNo.Text;
                    objEmployeeBo.PortalID = txtPortalID.Text;
                    objEmployeeBo.PRANNo = txtPRANNO.Text;
                    objEmployeeBo.TANNO = txtTANNo.Text;

                if (ViewState["Mode"].ToString() == "Save")
                {
                    if (objEmployeeBo.GenderGuj == "પુરૂષ")
                    {
                        GENDER = "Male";
                    }
                    else if (objEmployeeBo.GenderGuj == "સ્ત્રી")
                    {

                        GENDER = "Female";
                    }

                    if ((objEmployeeBo.Gender) == (GENDER))
                    {
                        objEmployeeBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objEmployeeBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objResults = objEmployeeBl.EmployeeM_Insert(objEmployeeBo);
                        if (objResults != null)
                        {
                            if (objResults.resultDT.Rows.Count > 0)
                            {
                                ViewState["EmployeeMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                                hfEmployeeMID.Value = ViewState["EmployeeMID"].ToString();
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Employee Created Successfully.');</script>");
                                ViewState["Mode"] = "Edit";
                            }

                            // ClearAll();


                            hfTab.Value = "1";

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Employee UserName is already Exists.');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Gender Selection must be same As Employee Section Gender Selection');</script>");
                    }

                }
                else
                {
                   
                    if (objEmployeeBo.GenderGuj== "પુરૂષ")
                    {
                        GENDER = "Male";
                    }
                    else if (objEmployeeBo.GenderGuj == "સ્ત્રી")
                    {

                        GENDER = "Female";
                    }

                        if ((objEmployeeBo.Gender) == (GENDER))
                    {
                        objEmployeeBo.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                        //objEmployeeBo.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                        // objEmployeeBo.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                        // objEmployeeBo.DesignationID = Convert.ToInt32(ViewState["DesignationID"].ToString());
                        // objEmployeeBo.RoleID = Convert.ToInt32(ViewState["RoleID"].ToString());

                        objResults = objEmployeeBl.EmployeeM_Update(objEmployeeBo);

                        //Entry in DeptTransferHistory tbl start
                        objEmployeeDeptTransferHistoryBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                        objEmployeeDeptTransferHistoryBO.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                        objEmployeeDeptTransferHistoryBO.DesignationID = Convert.ToInt32(ViewState["DesignationID"].ToString());
                        // objEmployeeDeptTransferHistoryBO.DepartmentID = Convert.ToInt32(Request.Form[ddlDepartment.UniqueID]);
                        // objEmployeeDeptTransferHistoryBO.DesignationID = Convert.ToInt32(Request.Form[ddlDesignation.UniqueID]);
                        objEmployeeDeptTransferHistoryBO.ReportingTo = Convert.ToInt32(Request.Form[ddlReportingTo.UniqueID]);
                        objEmployeeDeptTransferHistoryBO.EmployeeCode = txtEmployeeCode.Text;
                        objEmployeeDeptTransferHistoryBO.DepartmentJoiningDate = txtDeptJoinDate.Text;
                        objEmployeeDeptTransferHistoryBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objEmployeeDeptTransferHistoryBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objEmployeeDeptTransferHistoryBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objEmployeeDeptTransferHistoryBO.ModifiedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                        objEmpDeptTransferHistoryResults = objEmployeeBl.EmployeeDeptTransferHistoryT_Insert(objEmployeeDeptTransferHistoryBO);


                        //Entry in DeptTransferHistory tbl End

                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            hfEmployeeMID.Value = ViewState["EmployeeMID"].ToString();

                            //ClearAll();

                            //ViewState["Mode"] = "Save";
                            hfTab.Value = "5";
                            gvEmployee.Visible = true;


                            GridDataBindForExpirence();
                            GridDataBindForFamily();
                            GridDataBindForQualification();

                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Employee Updated Successfully.');</script>");

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Employee UserName is already Exists.');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Gender Selection must be same As Employee Section Gender Selection.');</script>");
                       // hfTab.Value = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }


        #endregion
       
        #region Save Event For Qualification
        protected void btnAddQualification_Click(object sender, EventArgs e)
        {
            try
            {
                EmpoyeeQualificationTBO objEmpQualiBO = new EmpoyeeQualificationTBO();
                EmployeeTBL objEmpQualiTBL = new EmployeeTBL();
                ApplicationResult objResults = new ApplicationResult();

                objEmpQualiBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                objEmpQualiBO.QualificationNameENG = txtQualificationEng.Text;
                objEmpQualiBO.QualificationNameGUJ = txtQualificationGuj.Text;
                objEmpQualiBO.Year = ddlYear.SelectedItem.ToString();
                objEmpQualiBO.Percentage = txtPercentage.Text;
                objEmpQualiBO.UniversityENG = txtUniversityEng.Text;
                objEmpQualiBO.UniversityGUJ = txtUniversityGuj.Text;
                if (ViewState["Mode"].ToString() == "Edit")
                {
                    objResults = objEmpQualiTBL.EmpoyeeQualificationT_Insert(objEmpQualiBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Qualification Successfully Added.');</script>");
                    }
                }
                else
                {
                    objEmpQualiBO.QualificationTID = Convert.ToInt32(ViewState["QualificationID"].ToString());
                    objResults = objEmpQualiTBL.EmpoyeeQualificationT_Update(objEmpQualiBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Qualification Successfully Updated.');</script>");
                        ViewState["Mode"] = "Edit";
                    }
                }
                GridDataBindForQualification();
                ClearQualification();
                hfTab.Value = "1";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Event For Expirence
        protected void btnAddEmpExp_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeExpirenceTBO objEmpExpBO = new EmployeeExpirenceTBO();
                EmployeeTBL objEmpExpTBL = new EmployeeTBL();
                ApplicationResult objResults = new ApplicationResult();

                objEmpExpBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                objEmpExpBO.OrganisationNameENG = txtOrganisationNameEng.Text;
                objEmpExpBO.OrganisationNameGUJ = txtOrganisationNameGuj.Text;
                objEmpExpBO.OrganisationAddressENG = txtOrgAddressEng.Text;
                objEmpExpBO.OrganisationAddressGUJ = txtOrgAddressGuj.Text;
                objEmpExpBO.DesignationENG = txtDesignationEng.Text;
                objEmpExpBO.DesignationGUJ = txtDesignationGuj.Text;
                objEmpExpBO.DurationMonth = ddlMonth.SelectedItem.ToString();
                objEmpExpBO.DurationYear = ddlDurationYear.SelectedItem.ToString();
                objEmpExpBO.JobResponsibilityENG = txtjobresponsibilityEng.Text;
                objEmpExpBO.JobResponsibilityGUJ = txtjobresponsibilityGuj.Text;
                objEmpExpBO.CTC = txtCTC.Text;
                objEmpExpBO.ReasonOfLeavingENG = txtResignReason.Text;
                objEmpExpBO.ReasonOfLeavingGUJ = txtResignReasonGuj.Text;
                if (ViewState["Mode"].ToString() == "Edit")
                {
                    objResults = objEmpExpTBL.EmployeeExpirenceT_Insert(objEmpExpBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Expirence Successfully Added.');</script>");
                    }
                }
                else
                {
                    objEmpExpBO.EmployeeExperienceTID = Convert.ToInt32(ViewState["EmployeeExperienceTID"].ToString());
                    objResults = objEmpExpTBL.EmployeeExpirenceT_Update(objEmpExpBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Expirence Successfully Updated.');</script>");
                        ViewState["Mode"] = "Edit";
                    }
                }
                GridDataBindForExpirence();
                ClearExpirence();
                hfTab.Value = "2";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Event For Family
        protected void btnAddFamily_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeFamilyTBO objEmpFamilyBO = new EmployeeFamilyTBO();
                EmployeeTBL objEmpFamilyTBL = new EmployeeTBL();
                ApplicationResult objResults = new ApplicationResult();

                objEmpFamilyBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                objEmpFamilyBO.FamilyPersonName = txtEmpFFName.Text;
                objEmpFamilyBO.Occupation = txtFOccupation.Text;
                objEmpFamilyBO.Organisation = txtForgName.Text;
                objEmpFamilyBO.Qualification = txtFQuali.Text;
                objEmpFamilyBO.ContactNo = txtFPhoneNo.Text;
                objEmpFamilyBO.MobileNo = txtFMobileNo.Text;
                objEmpFamilyBO.EmailID = txtFEmailId.Text;
                if (ViewState["Mode"].ToString() == "Edit")
                {
                    objResults = objEmpFamilyTBL.EmployeeFamilyT_Insert(objEmpFamilyBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Family Detail Successfully Added.');</script>");
                    }
                }
                else
                {
                    objEmpFamilyBO.FamilyPersonTID = Convert.ToInt32(ViewState["FamilyPersonTID"].ToString());
                    objResults = objEmpFamilyTBL.EmployeeFamilyT_Update(objEmpFamilyBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Family Detail Successfully Updated.');</script>");
                        ViewState["Mode"] = "Edit";
                    }
                }
                GridDataBindForFamily();
                ClearFamily();
                hfTab.Value = "3";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Webservice
        [System.Web.Services.WebMethod]
        public static string LoadDocument(int intSchoolID, int intTrustMID, int intStudentMID, int intEmployeeMID)
        {
            try
            {
                #region Bind Section
                DocumentBL objDocumentBL = new DocumentBL();
                DocumentBO objDocumentBO = new DocumentBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objDocumentBL.Document_SelectAll_ForDropDown(intSchoolID, intTrustMID, intStudentMID, intEmployeeMID);
                if (objResultSection != null)
                {
                    if (objResultSection.resultDT.Rows.Count > 0)
                    {
                        for (int i = 0; i < objResultSection.resultDT.Rows.Count; i++)
                        {

                        }
                    }

                }
                string res = "";
                res = DataSetToJSON(objResultSection.resultDT);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadSchool(int intTrustID)
        {
            try
            {
                #region Bind Section
                SchoolBL objSchoolBL = new SchoolBL();

                ApplicationResult objResults = new ApplicationResult();
                objResults = objSchoolBL.School_SelectAll_ForDropDOwn(intTrustID);
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        for (int i = 0; i < objResults.resultDT.Rows.Count; i++)
                        {

                        }
                    }

                }
                string res = "";
                res = DataSetToJSON(objResults.resultDT);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadDepartMent(int intTrustID, int intSchoolID)
        {
            try
            {
                #region Bind Section
                DepartmentBL objDocumentBL = new DepartmentBL();


                ApplicationResult objResults = new ApplicationResult();
                objResults = objDocumentBL.Department_Select_By_Trust_School_ForDropDown(intTrustID, intSchoolID);
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        for (int i = 0; i < objResults.resultDT.Rows.Count; i++)
                        {

                        }
                    }

                }
                string res = "";
                res = DataSetToJSON(objResults.resultDT);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadDesignation()
        {
            try
            {
                #region Bind Class
                DesignationBL objDesignationBL = new DesignationBL();


                ApplicationResult objResults = new ApplicationResult();
                objResults = objDesignationBL.Designation_SelectAll_ForDropDown();
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                    }

                }
                string res = "";
                res = DataSetToJSON(objResults.resultDT);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadRole()
        {
            try
            {
                #region Bind Class
                RoleBL objRoleBL = new RoleBL();
                ApplicationResult objResults = new ApplicationResult();
                objResults = objRoleBL.Role_SelectAll_ForDropDown();
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                    }

                }
                string res = "";
                res = DataSetToJSON(objResults.resultDT);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DataSetToJSON(DataTable dt)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            JavaScriptSerializer json = new JavaScriptSerializer();

            return json.Serialize(rows);
        }



        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            EmployeeMBL objEmployeeMbl = new EmployeeMBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.EmployeeM_Select_ForAutoComplete(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #endregion

        #region FetchSchool
        private DataTable FetchSchool(int intTrustID)
        {
            // DataTable dtStudent = new DataTable();
            SchoolBL objSchoolBL = new SchoolBL();

            ApplicationResult objResults = new ApplicationResult();
            objResults = objSchoolBL.School_SelectAll_ForDropDOwn(intTrustID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchDepartment
        private DataTable FetchDepartment(int intTrustID, int intSchoolID)
        {
            DepartmentBL objDocumentBL = new DepartmentBL();


            ApplicationResult objResults = new ApplicationResult();
            objResults = objDocumentBL.Department_Select_By_Trust_School_ForDropDown(intTrustID, intSchoolID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchDesignation
        private DataTable FetchDesignation()
        {
            DesignationBL objDesignationBL = new DesignationBL();

            ApplicationResult objResults = new ApplicationResult();
            objResults = objDesignationBL.Designation_SelectAll_ForDropDown();
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchRole
        private DataTable FetchRole()
        {
            RoleBL objRoleBL = new RoleBL();


            ApplicationResult objResults = new ApplicationResult();
            objResults = objRoleBL.Role_SelectAll_ForDropDown();
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Search Employee Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeMBL objEmployeeBL = new EmployeeMBL();
                EmployeeMBO objEmployeeBo = new EmployeeMBO();

                ApplicationResult objResultProgram = new ApplicationResult();
                objResultProgram = objEmployeeBL.Employee_Search_By_NameAndCode(txtSearchName.Text.Trim(), Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResultProgram != null)
                {
                    if (objResultProgram.resultDT.Rows.Count > 0)
                    {
                        gvEmployee.Visible = true;
                        gvEmployee.DataSource = objResultProgram.resultDT;
                        gvEmployee.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                        gvEmployee.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            rblGender.SelectedIndex = -1;
            rblGenderGuj.SelectedIndex = -1;
            chkIsUser.Checked = false;
            chkIsResign.Checked = false;
            chkAlowance.Checked = false;
            hfTab.Value = "0";
            Session["PicsName"] = null;
            Session["ImageNumber"] = null;
            ViewState["Bytes"] = null;
            gvEmpExpirence.Visible = false;
            gvEmpFamily.Visible = false;
            gvEmployeeQualification.Visible = false;

        }
        #endregion

        #region ClearQualification
        public void ClearQualification()
        {
            txtQualificationEng.Text = "";
            txtQualificationGuj.Text = "";
            ddlYear.SelectedIndex = -1;
            txtPercentage.Text = "";
            txtUniversityEng.Text = "";
            txtUniversityGuj.Text = "";
        }
        #endregion

        #region ClearExpirence
        public void ClearExpirence()
        {
            txtOrganisationNameEng.Text = "";
            txtOrganisationNameGuj.Text = "";
            txtOrgAddressEng.Text = "";
            txtOrgAddressGuj.Text = "";
            txtDesignationEng.Text = "";
            txtDesignationGuj.Text = "";
            ddlMonth.SelectedIndex = -1;
            ddlDurationYear.SelectedIndex = -1;
            ddlYear.SelectedIndex = -1;
            txtjobresponsibilityEng.Text = "";
            txtjobresponsibilityGuj.Text = "";
            txtCTC.Text = "";
            txtResignReason.Text = "";
            txtResignReasonGuj.Text = "";
        }
        #endregion

        #region ClearFamily
        public void ClearFamily()
        {
            txtEmpFFName.Text = "";
            txtFOccupation.Text = "";
            txtForgName.Text = "";
            txtFQuali.Text = "";
            txtFPhoneNo.Text = "";
            txtFMobileNo.Text = "";
            txtFEmailId.Text = "";
        }
        #endregion

        #region BindYear
        public void BindYear()
        {
            try
            {
                Controls objControls = new Controls();
                int currentYear = DateTime.Now.Year;
                CommonFunctions CF = new CommonFunctions();
                DataTable dtYear = CF.CreateDTYear();

                if (dtYear.Rows.Count > 0)
                {
                    //objControls.BindDropDown_ListBox(dtYear, ddlAdmittedYear, "AcademicYear", "AcademicYear");
                    //objControls.BindDropDown_ListBox(dtYear, ddlCurAdmissionYear, "AcademicYear", "AcademicYear");
                    objControls.BindDropDown_ListBox(dtYear, ddlYear, "AcademicYear", "AcademicYear");
                }
                //ddlAdmittedYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                //ddlCurAdmissionYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                ddlYear.Items.Insert(0, new ListItem("-Select-", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindReportingTo
        public void BindReportingTo()
        {
            try
            {
                Controls objControls = new Controls();
                EmployeeBL objEmployeeBl = new EmployeeBL();
                ApplicationResult objResult = new ApplicationResult();
                objResult = objEmployeeBl.Employee_SelectAll_ForReportingTo();
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlReportingTo, "EmployeeName", "EmployeeMID");
                }
                //ddlReportingTo.Items.Insert(0, new ListItem("-Select-", ""));

                
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Bind grid Qualification
        private void GridDataBindForQualification()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                EmployeeTBL objEmpQualiTBL = new EmployeeTBL();

                objResult = objEmpQualiTBL.EmpoyeeQualificationT_SelectAll(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                if (objResult != null)
                {
                    gvEmployeeQualification.DataSource = objResult.resultDT;
                    gvEmployeeQualification.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvEmployeeQualification.Visible = true;
                    }
                    else
                    {
                        gvEmployeeQualification.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind grid Expirence
        private void GridDataBindForExpirence()
        {
            try
            {

                ApplicationResult objResult = new ApplicationResult();
                EmployeeTBL objEmpQualiTBL = new EmployeeTBL();

                objResult = objEmpQualiTBL.EmployeeExpirenceT_SelectAll(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                if (objResult != null)
                {
                    gvEmpExpirence.DataSource = objResult.resultDT;
                    gvEmpExpirence.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvEmpExpirence.Visible = true;
                    }
                    else
                    {
                        gvEmpExpirence.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind grid Family
        private void GridDataBindForFamily()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                EmployeeTBL objEmpQualiTBL = new EmployeeTBL();

                objResult = objEmpQualiTBL.EmployeeFamilyT_SelectAll(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                if (objResult != null)
                {
                    gvEmpFamily.DataSource = objResult.resultDT;
                    gvEmpFamily.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvEmpFamily.Visible = true;
                    }
                    else
                    {
                        gvEmpFamily.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion,

        #region Employee Row Command
        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                Controls objControls = new Controls();
                EmployeeMBL objEmployeeBL = new EmployeeMBL();

                ViewState["EmployeeMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                hnFindEmpId.Value  = Convert.ToString(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objEmployeeBL.EmployeeM_Select(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "UpdateModeIsUserHide();", true);
                                rblSelect.SelectedValue = "0";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "viewForSchool();", true);

                                dt = FetchSchool(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                                if (dt.Rows.Count > 0)
                                {
                                    objControls.BindDropDown_ListBox(dt, ddlSchool, "SchoolNameEng", "SchoolMID");
                                }
                                ddlSchool.Items.Insert(0, new ListItem("-Select-", "-1"));
                                ViewState["SchoolMID"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                ddlSchool.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();

                                dt = FetchDepartment(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchool.SelectedValue));
                                if (dt.Rows.Count > 0)
                                {
                                    objControls.BindDropDown_ListBox(dt, ddlDepartment, "DepartmentNameENG", "DepartmentID");
                                }
                                ddlDepartment.Items.Insert(0, new ListItem("-Select-", "-1"));
                                ViewState["DepartmentID"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DEPARTMENTID].ToString();
                                ddlDepartment.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DEPARTMENTID].ToString();
                                
                                dt = FetchDesignation();

                                if (dt.Rows.Count > 0)
                                {
                                    objControls.BindDropDown_ListBox(dt, ddlDesignation, "DesignationNameENG", "DesignationID");
                                }
                                ddlDesignation.Items.Insert(0, new ListItem("-Select-", "-1"));
                                ViewState["DesignationID"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DESIGNATIONID].ToString();
                                ddlDesignation.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DESIGNATIONID].ToString();
                            }
                            else
                            {
                                rblSelect.SelectedValue = "1";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "viewForTrust();", true);

                                dt = FetchDepartment(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), 0);
                                if (dt.Rows.Count > 0)
                                {
                                    objControls.BindDropDown_ListBox(dt, ddlDepartment, "DepartmentNameENG", "DepartmentID");
                                }
                                ddlDepartment.Items.Insert(0, new ListItem("-Select-", "-1"));
                                ViewState["DepartmentID"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DEPARTMENTID].ToString();
                                ddlDepartment.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DEPARTMENTID].ToString();

                            }
                            if (Convert.ToInt32(objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ReportingTo]) != 0)
                            {
                                ddlReportingTo.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ReportingTo].ToString();
                            }
                            else {
                                ddlReportingTo.SelectedValue = null;
                            }
                          
                           
                            dt = FetchDesignation();
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlDesignation, "DesignationNameENG", "DesignationID");
                            }
                            ddlDesignation.Items.Insert(0, new ListItem("-Select-", "-1"));
                            ViewState["DesignationID"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DESIGNATIONID].ToString();
                            ddlDesignation.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DESIGNATIONID].ToString();

                            dt = FetchRole();
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlRole, "RoleName", "RoleID");
                            }
                            ddlRole.Items.Insert(0, new ListItem("-Select-", "-1"));
                            ViewState["RoleID"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ROLEID].ToString();
                            ddlRole.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ROLEID].ToString();
                            txtEmployeeCode.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEECODE].ToString();
                            txtEmployeeFirstName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEFNAMEENG].ToString();
                            txtEmployeeFirstNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEFNAMEGUJ].ToString();
                            txtEmployeeLastName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEELNAMEENG].ToString();
                            txtEmployeeLastNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEELNAMEGUJ].ToString();
                            txtEmployeeMiddleName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEMNAMEENG].ToString();
                            txtEmployeeMiddleNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEMNAMEGUJ].ToString();
                            ViewState["Bytes"] = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PHOTO];
                            if (ViewState["Bytes"].ToString() != "")
                            {
                                imgphoto.ImageUrl = "GetImage.ashx?EmployeeMID=" + ViewState["EmployeeMID"];
                            }
                            else
                            {
                                imgphoto.ImageUrl = "../Images/noimage-big.jpg";
                                //lbtnRemovePhoto.Visible = false;
                            }
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_GENDER].ToString().ToUpper() == "Male".ToUpper())
                            {
                                rblGender.Items[0].Selected = true;
                                if (ViewState["Bytes"].ToString() != "")
                                {
                                    imgphoto.ImageUrl = "GetImage.ashx?EmployeeMID=" + ViewState["EmployeeMID"];
                                }
                                else
                                {
                                    imgphoto.ImageUrl = "../Images/MALE_ICON.png";
                                }
                               
                               
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_GENDER].ToString().ToUpper() == "Female".ToUpper())
                            {
                                rblGender.Items[1].Selected = true;
                                if (ViewState["Bytes"].ToString() != "")
                                {
                                    imgphoto.ImageUrl = "GetImage.ashx?EmployeeMID=" + ViewState["EmployeeMID"];
                                }
                                else
                                {
                                    imgphoto.ImageUrl = "../Images/FEMALE_ICON.png";
                                }
                            
                            }
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_GENDERGUJ].ToString().ToUpper() == "પુરૂષ")
                            {
                                rblGenderGuj.Items[0].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_GENDERGUJ].ToString().ToUpper() == "સ્ત્રી")
                            {
                                rblGenderGuj.Items[1].Selected = true;
                            }
                            txtDateOfBirth.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DATEOFBIRTH].ToString();
                            txtBirthDistrict.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BIRTHDISTRICTENG].ToString();
                            txtBirthDistrictGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BIRTHDISTRICTGUJ].ToString();
                            txtBirthTaluka.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BIRTHTALUKAENG].ToString();
                            txtBirthTalukaGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BIRTHTALUKAGUJ].ToString();
                            txtBirthCity.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BIRTHCITYVILLAGEENG].ToString();
                            txtBirthCityGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BIRTHCITYVILLAGEGUJ].ToString();
                            txtNationality.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_NATIONALITYENG].ToString();
                            txtNationalityGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_NATIONALITYGUJ].ToString();
                            txtReligion.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RELIGIONENG].ToString();
                            txtReligionGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RELIGIONGUJ].ToString();
                            txtCaste.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CASTE].ToString();
                            txtMaritalStatus.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MARITALSTATUS].ToString();
                            txtBloodGroup.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BLOODGROUP].ToString();
                            txtMotherToungue.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERLANGUAGE].ToString();
                            txtCurAddress.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTADDRESSENG].ToString();
                            txtCorrespondenceAddressGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTADDRESSGUJ].ToString();
                            txtCurLandMark.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTLANDMARKENG].ToString();
                            txtCurLandMarkGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTLANDMARKGUJ].ToString();
                            txtCurCity.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTCITYENG].ToString();
                            txtCorrespondenceCityGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTCITYGUJ].ToString();
                            txtCurState.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTSTATEENG].ToString();
                            txtCorrespondenceStateGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTSTATEGUJ].ToString();
                            txtCurPinCode.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CURRENTPINCODE].ToString();
                            txtPermenantAddress.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTADDRESSENG].ToString();
                            txtPermenantAddressGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTADDRESSGUJ].ToString();
                            txtPermenantCity.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTCITYENG].ToString();
                            txtPermenantCityGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTCITYGUJ].ToString();
                            txtPermenantLandMark.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTLANDMARKENG].ToString();
                            txtPermenantLandMarkGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTLANDMARKGUJ].ToString();
                            txtPermenantState.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTSTATEENG].ToString();
                            txtPermenantStateGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTSTATEGUJ].ToString();
                            txtPermenantPinCode.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PERMENANTPINCODE].ToString();
                            txtPhoneNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TELEPHONENO].ToString();
                            txtMobileNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOBILENO].ToString();
                            txtEmailID.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_EMAILID].ToString();
                            txtHobbies.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_HOBBIES].ToString();
                            txtRightVision.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RIGHTVISION].ToString();
                            txtLeftVision.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_LEFTVISION].ToString();
                            txtRectificationDevice.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RECTIFICATIONDEVICE].ToString();
                            txtHeight.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_HEIGHT].ToString();
                            txtWeight.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_WEIGHT].ToString();
                            txtPhysicalIdentification.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PHYSICALIDENTIFICATIONENG].ToString();
                            txtPhysicalIdentificationGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PHYSICALIDENTIFICATIONGUJ].ToString();
                            txtMMFirstName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERMAIDENFNAMEENG].ToString();
                            txtMotherFirstNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERMAIDENFNAMEGUJ].ToString();
                            txtMMLastName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERMAIDENLNAMEENG].ToString();
                            txtMotherLastNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERMAIDENLNAMEGUJ].ToString();
                            txtMMMiddleName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERMAIDENMNAMEENG].ToString();
                            txtMotherMiddleNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_MOTHERMAIDENMNAMEGUJ].ToString();
                            txtBankName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BANKNAME].ToString();
                            txtBranchName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BRANCHNAME].ToString();
                            txtBranchCode.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BRANCHCODE].ToString();
                            txtAccNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ACCOUNTNO].ToString();
                            txtPFNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PFNO].ToString();
                            txtPanNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PANNO].ToString();
                            txtEsicNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ESICNO].ToString();
                            txtIfscCode.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_IFSCCODE].ToString();
                            txtGpfNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_GPFACCOUNTNO].ToString();
                            txtCpfNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CPFACCOUNTNO].ToString();
                            txtDeptJoinDate.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_DEPARTMENTJOININGDATE].ToString();
                            txtOrgDate.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ORGANISATIONJOININGDATE].ToString();

                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "GovDEO")
                            {
                                rblAppointmentType.Items[0].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoShow();", true);
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "GovPravasi")
                            {
                                rblAppointmentType.Items[1].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoHide();", true);
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "MgtPermanent")
                            {
                                rblAppointmentType.Items[2].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoHide();", true);
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "MgtTemporary")
                            {
                                rblAppointmentType.Items[3].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoHide();", true);
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "MgtDailyWages")
                            {
                                rblAppointmentType.Items[4].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoHide();", true);
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "Direct")
                            {
                                rblAppointmentType.Items[5].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoShow();", true);
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TYPEOFAPPOINTMENT].ToString() == "Replacement")
                            {
                                rblAppointmentType.Items[6].Selected = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SchoolInfoHide();", true);
                            }
                            txtSchoolInfo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_REPLACEMENTSCHOOLINFOENG].ToString();
                            txtSchoolReplacementInfoGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_REPLACEMENTSCHOOLINFOGUJ].ToString();
                            txtRetirementDate.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RETIREMENTDATE].ToString();
                            txtTermRetireDate.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TERMENDRETIREMENTDATE].ToString();
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ISRESIGNED].ToString() == "1")
                            {
                                chkIsResign.Checked = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divResignshow();", true);
                            }
                            else
                            {
                                chkIsResign.Checked = false;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "divResignhide();", true);

                            }
                            txtResignDate.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RESIGNEDDATE].ToString();
                            txtLastWorkingDate.Text= objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_LASTWORKINGDATE].ToString();
                            txtResignReason.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RESIGNREASONENG].ToString();
                            txtResignReasonGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_RESIGNREASONGUJ].ToString();
                            txtBreakInfo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BREAKINFOENG].ToString();
                            txtBreakInfoGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_BREAKINFOGUJ].ToString();
                            txtAchieveMentInfo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_OTHERACHIVEMENTDETAILSENG].ToString();
                            txtAchievementInfoGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_OTHERACHIVEMENTDETAILSGUJ].ToString();
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ISUSER].ToString() == "1")
                            {
                                chkIsUser.Checked = true;
                            //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "UpdateModeIsUserShow();", true);
                               ScriptManager.RegisterStartupScript(this, GetType(), "alert", "UpdateModeIsUserShow();", true);
                            }
                            else
                            {
                                chkIsUser.Checked = false;
                               // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction","UpdateModeIsUserHide();", true);
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "UpdateModeIsUserHide();", true);
                            }
                            txtUserName.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_USERNAME].ToString();
                            txtPassWord.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PASSWORD].ToString();
                            txtPassWord.Attributes.Add("value", objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PASSWORD].ToString());
                            txtRePassword.Attributes.Add("value", objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PASSWORD].ToString());

                            //txtUserName.Text = "";
                            //txtPassWord.Text = "";
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ISTEACHER].ToString() == "1")
                            {
                                rblTeacher.Items[0].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ISTEACHER].ToString() == "1")
                            {
                                rblTeacher.Items[1].Selected = true;
                            }
                            else
                            {
                                rblTeacher.Items[2].Selected = true;
                            }
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ALLOWACCOUNTACCESS].ToString() == "1")
                            {
                                chkAlowance.Checked = true;
                            }
                            else
                            {
                                chkAlowance.Checked = false;
                            }
                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYENG].ToString() == "Open")
                            {
                                rblCategory.Items[0].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYENG].ToString() == "OBC")
                            {
                                rblCategory.Items[1].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYENG].ToString() == "SC")
                            {
                                rblCategory.Items[2].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYENG].ToString() == "ST")
                            {
                                rblCategory.Items[3].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYENG].ToString() == "Other")
                            {
                                rblCategory.Items[4].Selected = true;
                            }

                            if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYGUJ].ToString() == "સામાન્ય")
                            {
                                rblCategoryGuj.Items[0].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYGUJ].ToString() == "બક્ષીપંચ")
                            {
                                rblCategoryGuj.Items[1].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYGUJ].ToString() == "અનુસૂચિત જાતિ")
                            {
                                rblCategoryGuj.Items[2].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYGUJ].ToString() == "અનુસૂચિત જન જાતિ")
                            {
                                rblCategoryGuj.Items[3].Selected = true;
                            }
                            else if (objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_CATEGORYGUJ].ToString() == "અન્ય")
                            {
                                rblCategoryGuj.Items[4].Selected = true;
                            }

                            txtAppointmentTypeCode.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TypeOfAppointmentCode].ToString();
                            txtAadharCardNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_AaharCardNo].ToString();
                            txtElectionCardNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_ElectionCardNo].ToString();
                            txtVehicleNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_VehicleNo].ToString();
                            txtPortalID.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PortalID].ToString();
                            txtPRANNO.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_PRANNo].ToString();
                            txtTANNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeMBO.EMPLOYEEM_TANNO].ToString();
                            GridDataBindForQualification();
                            GridDataBindForExpirence();
                            GridDataBindForFamily();
                            GridDataBindForEmployeeRemakrs();
                            hfEmployeeMID.Value = ViewState["EmployeeMID"].ToString();
                            ViewState["Mode"] = "Edit";
                            PanelGrid_VisibilityMode(2);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "UpdateModeIsUserHide();", true);
                            hfTab.Value = "4";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Qualification GridView Row Command
        protected void gvEmployeeQualification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                EmployeeTBL objEmployeeBL = new EmployeeTBL();

                ViewState["QualificationID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objEmployeeBL.EmpoyeeQualificationT_Select(Convert.ToInt32(ViewState["QualificationID"].ToString()));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            txtQualificationEng.Text = objResultsEdit.resultDT.Rows[0][EmpoyeeQualificationTBO.EMPOYEEQUALIFICATIONT_QUALIFICATIONNAMEENG].ToString();
                            txtQualificationGuj.Text = objResultsEdit.resultDT.Rows[0][EmpoyeeQualificationTBO.EMPOYEEQUALIFICATIONT_QUALIFICATIONNAMEGUJ].ToString();
                            ddlYear.SelectedValue = objResultsEdit.resultDT.Rows[0][EmpoyeeQualificationTBO.EMPOYEEQUALIFICATIONT_YEAR].ToString();
                            txtPercentage.Text = objResultsEdit.resultDT.Rows[0][EmpoyeeQualificationTBO.EMPOYEEQUALIFICATIONT_PERCENTAGE].ToString();
                            txtUniversityEng.Text = objResultsEdit.resultDT.Rows[0][EmpoyeeQualificationTBO.EMPOYEEQUALIFICATIONT_UNIVERSITYENG].ToString();
                            txtUniversityGuj.Text = objResultsEdit.resultDT.Rows[0][EmpoyeeQualificationTBO.EMPOYEEQUALIFICATIONT_UNIVERSITYGUJ].ToString();
                            ViewState["Mode"] = "Edit";
                            hfTab.Value = "1";
                        }
                    }
                }

                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();
                    ApplicationResult objDelete = new ApplicationResult();


                    objDelete = objEmployeeBL.EmpoyeeQualificationT_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearQualification();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Qualification deleted successfully.');</script>");
                        GridDataBindForQualification();
                        hfTab.Value = "1";
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region GridView Employee Expirence RowCommand
        protected void gvEmpExpirence_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                EmployeeTBL objEmployeeBL = new EmployeeTBL();

                ViewState["EmployeeExperienceTID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objEmployeeBL.EmployeeExpirenceT_Select(Convert.ToInt32(ViewState["EmployeeExperienceTID"].ToString()));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            txtOrganisationNameEng.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_ORGANISATIONNAMEENG].ToString();
                            txtOrganisationNameGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_ORGANISATIONNAMEGUJ].ToString();
                            txtOrgAddressEng.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_ORGANISATIONADDRESSENG].ToString();
                            txtOrgAddressGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_ORGANISATIONADDRESSGUJ].ToString();
                            txtDesignationEng.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_DESIGNATIONENG].ToString();
                            txtDesignationGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_DESIGNATIONGUJ].ToString();
                            ddlMonth.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_DURATIONMONTH].ToString();
                            ddlDurationYear.SelectedValue = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_DURATIONYEAR].ToString();
                            txtjobresponsibilityEng.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_JOBRESPONSIBILITYENG].ToString();
                            txtjobresponsibilityGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_JOBRESPONSIBILITYGUJ].ToString();
                            txtCTC.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_CTC].ToString();
                            txtResignReason.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_REASONOFLEAVINGENG].ToString();
                            txtResignReasonGuj.Text = objResultsEdit.resultDT.Rows[0][EmployeeExpirenceTBO.EMPLOYEEEXPIRENCET_REASONOFLEAVINGGUJ].ToString();
                            ViewState["Mode"] = "Edit";

                            hfTab.Value = "21";
                        }
                    }
                }

                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();
                    ApplicationResult objDelete = new ApplicationResult();


                    objDelete = objEmployeeBL.EmployeeExpirenceT_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearExpirence();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Expirence deleted successfully.');</script>");
                        GridDataBindForExpirence();
                        hfTab.Value = "2";
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gridview Family RowCommand
        protected void gvEmpFamily_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                EmployeeTBL objEmployeeBL = new EmployeeTBL();

                ViewState["FamilyPersonTID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objEmployeeBL.EmployeeFamilyT_Select(Convert.ToInt32(ViewState["FamilyPersonTID"].ToString()));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            txtEmpFFName.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_FAMILYPERSONNAME].ToString();
                            txtFOccupation.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_OCCUPATION].ToString();
                            txtForgName.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_ORGANISATION].ToString();
                            txtFQuali.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_QUALIFICATION].ToString();
                            txtFPhoneNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_CONTACTNO].ToString();
                            txtFMobileNo.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_MOBILENO].ToString();
                            txtFEmailId.Text = objResultsEdit.resultDT.Rows[0][EmployeeFamilyTBO.EMPLOYEEFAMILYT_EMAILID].ToString();
                            ViewState["Mode"] = "Edit";
                            hfTab.Value = "3";
                        }
                    }
                }

                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();
                    ApplicationResult objDelete = new ApplicationResult();


                    objDelete = objEmployeeBL.EmployeeFamilyT_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearFamily();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Family Detail deleted successfully.');</script>");
                        GridDataBindForFamily();
                        hfTab.Value = "3";
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region PanelGrid_VisibilityMode
        private void PanelGrid_VisibilityMode(int intMode)
        {
            ApplicationResult objResultProgram = new ApplicationResult();
            StudentBL objStudentBL = new StudentBL();
            //objResultProgram = objStudentBL.Student_Search_StudentName(txtSearchName.Text.Trim(), Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()), txtSearchName.Text, txtSearchName.Text);
            //if (objResultProgram != null)
            //{
            if (intMode == 1)
            {
                divGridPanel.Visible = true;
                divEmployee.Visible = false;
                btnAddEmployee.Visible = true;
                gvEmployee.Visible = false;
            }
            else if (intMode == 2)
            {
                divGridPanel.Visible = false;
                divEmployee.Visible = true;
                btnAddEmployee.Visible = true;
                gvEmployee.Visible = true;
            }
            //}
        }

        #endregion PanelGrid_VisibilityMode

        #region Function for convert Image To ByteArray For FilePath
        public byte[] ImageToByteArrayFromFilePath(string imagefilePath)
        {
            try
            {
                byte[] imageArray = File.ReadAllBytes(Server.MapPath(imagefilePath));
                return imageArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Upload Button Event
        protected void btnUploadDocument_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Save";
            hfTab.Value = "1";


           

        }
        #endregion

        #region Go to Employee Detail Button
        protected void btnGoToEmployee_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Edit";
            hfTab.Value = "4";

        }
        #endregion

        #region OK button Click Event of WebCam Model Popup
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PicsName"] != null && Session["ImageNumber"] != null)
                {

                    Session["PicsName"] = Session["PicsName"].ToString() + Session["ImageNumber"] + ".jpg";
                    imgphoto.ImageUrl = "../Logo/EmployeePhoto/" + Session["PicsName"].ToString(); // + Session["ImageNumber"] + ".jpg";
                }
                else
                {
                    imgphoto.ImageUrl = "~/Images/NoImage-big.jpg";
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Take Proper Photo.');</script>");
                }
                ModalPopupExtender1.Hide();
                divModal.Visible = false;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }

        }
        #endregion

        #region ClickPhoto Button Click Event of WebCam
        protected void btnClickPhoto_Click(object sender, EventArgs e)
        {
            divModal.Visible = true;
            Session["PicsName"] = (txtEmployeeFirstName.Text + System.DateTime.Now.ToLongDateString()).ToString().Trim();
            ModalPopupExtender1.Show();
            //foreach (var file in Directory.GetFiles(Server.MapPath("../VisitorPics/")))
            //{
            //    File.Delete(file);
            //}
            imgphoto.ImageUrl = "~/Images/NoImage-big.jpg";
        }
        #endregion

        #region cancel Button Event in Family tab
        protected void btnCancelFamily_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Save";
            hfTab.Value = "3";
        }
        #endregion

        #region Cancel event in expirence tab
        protected void btnCancelExpirence_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Save";
            hfTab.Value = "2";
        }
        #endregion

        #region Cancel event inQualification tab
        protected void btnCancelQualification_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Save";
            hfTab.Value = "1";
        }
        #endregion

        #region cancel Photo Button Event
        protected void lnkCancelPhoto_Click(object sender, EventArgs e)
        {
            divModal.Visible = false;
        }
        #endregion

        protected void rblGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblGender.SelectedValue == "MALE")
            {
                imgphoto.ImageUrl = "../Images/MALE_ICON.png";
            }
            else if (rblGender.SelectedValue == "FEMALE")
            {
                imgphoto.ImageUrl = "../Images/FEMALE_ICON.png";
            }
            else {
                imgphoto.ImageUrl = "../Images/noimage-big.jpg";
            }
        }
        protected void gvEmpRemakrs_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void btnCancelEmpTRemakrs_Click(object sender, EventArgs e)
        {
            txtRemakrs.Text = "";
            txtRemakrsDate.Text = "";          
            hfTab.Value = "6";

        }

        #region Save Event For Employee Remakrs
        protected void bntAddEmpTRemakrs_Click(object sender, EventArgs e)
        {
            try
            {             
                EmployeeRemarksTBO objEmpRemakrsTBO = new EmployeeRemarksTBO();
                EmployeeRemarksTBL objEmpRemakrsBL = new EmployeeRemarksTBL();
                ApplicationResult objResults = new ApplicationResult();
              
                objEmpRemakrsTBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                objEmpRemakrsTBO.RemarksDate =Convert.ToDateTime(txtRemakrsDate.Text);
                objEmpRemakrsTBO.Remarks = txtRemakrs.Text;
                if (objEmpRemakrsTBO.EmployeeMID != 0 && objEmpRemakrsTBO.RemarksDate != null && objEmpRemakrsTBO.Remarks != "")
                {

                    objResults = objEmpRemakrsBL.EmployeeRemarksT_Insert(objEmpRemakrsTBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Employee Remakrs Successfully Added.');</script>");
                    }
                }
                else {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }             

                GridDataBindForEmployeeRemakrs();
                txtRemakrs.Text = "";
                txtRemakrsDate.Text = "";
               // ClearQualification();
                hfTab.Value = "6";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind grid Employee Remarks
        private void GridDataBindForEmployeeRemakrs()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
            
                EmployeeRemarksTBL objEmpRemakrsBL = new EmployeeRemarksTBL();

                objResult = objEmpRemakrsBL.EmployeeRemarksT_SelectALL(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                if (objResult != null)
                {
                    gvEmpRemakrs.DataSource = objResult.resultDT;
                    gvEmpRemakrs.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvEmpRemakrs.Visible = true;
                    }
                    else
                    {
                        gvEmpRemakrs.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        
    }
}