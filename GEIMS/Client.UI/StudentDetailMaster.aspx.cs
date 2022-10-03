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
    public partial class StudentDetailMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentDetailMaster));

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
          //  fileToUpload.Attributes.Add("onchange", "javascript:return ajaxFileUpload();");
            if (fuImage.HasFile)
            {
                string Extension;
                Extension = Path.GetExtension(fuImage.FileName);
                if (Extension == ".jpg" || Extension == ".JPG" || Extension == ".jpeg" || Extension == ".JPEG" || Extension == ".bmp" || Extension == ".BMP" || Extension == ".gif" || Extension == ".GIF" || Extension == ".png" || Extension == ".PNG")
                {
                    if (System.IO.File.Exists(Server.MapPath("../Logo/StudentPhoto/") + txtStudentFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension))
                    {
                        System.IO.File.Delete(Server.MapPath("../Logo/StudentPhoto/") + txtStudentFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension);
                    }
                    fuImage.PostedFile.SaveAs(Server.MapPath("../Logo/StudentPhoto/") + txtStudentFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension);
                    //objSchoolBo.SchoolLogo = txtSchoolName.Text.Replace(" ", "").ToString() + Extension;
                    imgphoto.ImageUrl = "../Logo/StudentPhoto/" + txtStudentFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension;
                    //  ViewState["strPhoto"] = "../Logo/StudentPhoto/" + txtStudentFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension;
                    Byte[] bytes = (byte[])ImageToByteArrayFromFilePath("../Logo/StudentPhoto/" + txtStudentFirstName.Text.Replace(" ", "").ToString() + System.DateTime.Now.ToLongDateString() + Extension);
                    ViewState["Bytes"] = bytes;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('File format not supported');</script>");
                }
            }
            else
            {
                imgphoto.ImageUrl = "~/images/noimage-big.jpg";
                // ViewState["Bytes"] = null;
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    divModal.Visible = false;
                    Manage_Student(2);
                    BindYear();
                    BindAcademicYear();
                    BindStatus();
                    BindSection();
                    ViewState["Mode"] = "Save";
                    ViewState["StudentMID"] = 0;
                    PanelGrid_VisibilityMode(1);
                    txtDateOfBirth.Attributes.Add("readonly", "readonly");
                    txtCurAdmissionDate.Attributes.Add("readonly", "readonly");
                    txtAdmittednDate.Attributes.Add("readonly", "readonly");
                    txtLeftDate.Attributes.Add("readonly", "readonly");
                    txtJoiningDate.Attributes.Add("readonly", "readonly");
                    string Serverpath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                    string sDirPath = Server.MapPath(Serverpath);
                    hdnUploadFilePath.Value = sDirPath;
                    hfTab.Value = "0";
                    if (Session[ApplicationSession.USERID] != null || Session[ApplicationSession.USERID] != "")
                    {

                        hdnLastUserID.Value = Session[ApplicationSession.USERID].ToString();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
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
                    objControls.BindDropDown_ListBox(dtYear, ddlLeftYear, "AcademicYear", "AcademicYear");
                }
                //ddlAdmittedYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                //ddlCurAdmissionYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                ddlLeftYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindAcademicYear
        public void BindAcademicYear()
        {
            try
            {
                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;

                objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                    }

                }
                #endregion

                Controls objControls = new Controls();
                int month = System.DateTime.Now.Month;
                int Year = System.DateTime.Now.Year;
                int lastTwoDigit = Year % 100;
                string yr = string.Empty;
                if (month >= intMonth)
                    yr = (lastTwoDigit.ToString() + (lastTwoDigit + 1).ToString()).ToString();
                else
                    yr = ((lastTwoDigit - 1).ToString() + lastTwoDigit.ToString()).ToString();

                int f = (Convert.ToInt32(yr.Substring(0, 2)));
                int l = (Convert.ToInt32(yr.Substring(2, 2)));

                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));

                for (int i = 0; i < 5; i++)
                {
                    dr = dt.NewRow();
                    if (i == 0)
                    {
                        dr["AcademicYear"] = Convert.ToString(f.ToString() + "-" + l.ToString());
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if ((f - 1).ToString().Length < 2)
                        {
                            if (f.ToString().Length == 2)
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                            }
                            else
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
                            }
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["AcademicYear"] = Convert.ToString((f - 1).ToString() + "-" + (f).ToString());
                            dt.Rows.Add(dr);
                        }
                        f = f - 1;
                        l = f;
                    }
                }

                objControls.BindDropDown_ListBox(dt, ddlAdmittedYear, "AcademicYear", "AcademicYear");
                ddlAdmittedYear.Items.Insert(0, new ListItem("-Select-", ""));

                objControls.BindDropDown_ListBox(dt, ddlCurAdmissionYear, "AcademicYear", "AcademicYear");
                ddlCurAdmissionYear.Items.Insert(0, new ListItem("-Select-", ""));

                objControls.BindDropDown_ListBox(dt, ddlRegisterdYear, "AcademicYear", "AcademicYear");
                ddlRegisterdYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            }
        }
        #endregion

        #region Bind Student Grid
        private void BindStdentGrid()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();

                objResult = objStudentBL.Student_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvStudent.DataSource = objResult.resultDT;
                        gvStudent.DataBind();
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

        #region Bind Status
        private void BindStatus()
        {
            try
            {
                StatusBL objStatusBL = new StatusBL();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                objResults = objStatusBL.Status_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResults.resultDT, ddlStatus, "StatusName", "StatusMasterID");
                    }
                    //ddlStatus.Items.Insert(0, new ListItem("-Select-", ""));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Bind Section
        public void BindSection()
        {
            try
            {
                SectionBL ObjSectionBl = new SectionBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjSectionBl.Section_SelectAll_SectionMID(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection1, "SectionName", "SectionMID");
                        ddlSection1.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Bind Class
        public void BindClass()
        {
            try
            {
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int SchoolID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int SectionID = Convert.ToInt32(ddlSection1.SelectedValue.ToString());

                objResult = objClassBL.Find_Class_SectionWise(SchoolID, SectionID);

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName1, "ClassName", "ClassMID");
                        ddlClassName1.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName1, "ClassName", "ClassMID");
                        ddlDivisionName1.Items.Insert(0, new ListItem("-Select-", "0"));
                        ddlDivisionName1.ClearSelection();
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

        #region Bind Division
        public void BindDivision()
        {
            try
            {
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int ClassID = Convert.ToInt32(ddlClassName1.SelectedValue.ToString());

                objResult = objClassBL.Find_Division_ClassWise(ClassID);

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName1, "DivisionName", "DivisionTID");
                        ddlDivisionName1.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName1, "DivisionName", "DivisionTID");
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

        #region AddStudent
        protected void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            chkIsLate.Checked = false;
            PanelGrid_VisibilityMode(2);
            Manage_Student(2);
            lblMessage.Visible = false;
            hfTab.Value = "0";
        }
        #endregion

        #region ViewList
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["StudentMID"] = null;
            PanelGrid_VisibilityMode(1);
            GridPanel.Visible = true;
        }
        #endregion

        #region Save Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StudentBO objStudentBO = new StudentBO();
                ApplicationResult objResults = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();
                StudentTBO objStudentTBO = new StudentTBO();

                objStudentBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objStudentBO.StudentFirstNameEng = txtStudentFirstName.Text;
                objStudentBO.StudentFirstNameGuj = txtStudentFirstNameGuj.Text.Trim();
                objStudentBO.StudentMiddleNameEng = txtStudentMiddleName.Text;
                objStudentBO.StudentMiddleNameGuj = txtStudentMiddleNameGuj.Text.Trim();
                objStudentBO.StudentLastNameEng = txtStudentLastName.Text;
                objStudentBO.StudentLastNameGuj = txtStudentLastNameGuj.Text.Trim();

                objStudentBO.FatherFirstNameEng = txtFatherFirstName.Text;
                objStudentBO.FatherFirstNameGuj = txtFatherFirstNameGuj.Text.Trim();
                objStudentBO.FatherMiddleNameEng = txtFatherMiddleName.Text;
                objStudentBO.FatherMiddleNameGuj = txtFatherMiddleNameGuj.Text.Trim();
                objStudentBO.FatherLastNameEng = txtFatherLastName.Text;
                objStudentBO.FatherLastNameGuj = txtFatherLastNameGuj.Text.Trim();

                objStudentBO.MotherFirstNameEng = txtMotherFirstName.Text;
                objStudentBO.MotherFirstNameGuj = txtMotherFirstNameGuj.Text.Trim();
                objStudentBO.MotherMiddleNameEng = txtMotherMiddleName.Text;
                objStudentBO.MotherMiddleNameGuj = txtMotherMiddleNameGuj.Text.Trim();
                objStudentBO.MotherLastNameEng = txtMotherLastName.Text;
                objStudentBO.MotherLastNameGuj = txtMotherLastNameGuj.Text.Trim();

                objStudentBO.AdmissionNo = txtCurAdmissionNo.Text;
                objStudentBO.CurrentDate = txtCurAdmissionDate.Text;
                objStudentBO.GVUniqueID = txtGovUniqueID.Text;
                objStudentBO.BankAccount = txtBankAccount.Text;
                objStudentBO.JoiningDate = txtJoiningDate.Text;
                objStudentTBO.IsLate = Convert.ToInt32(chkIsLate.Checked);
                objStudentBO.CurrentYear = ddlCurAdmissionYear.SelectedItem.ToString();
                objStudentBO.CurrentSectionID = Convert.ToInt32(Request.Form[ddlSection.UniqueID]);

                objStudentBO.CurrentClassID = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                // objStudentBO.CurrentClassID = Convert.ToInt32(ddlClass.SelectedValue);
                objStudentBO.CurrentDivisionTID = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);

                objStudentBO.CurrentGrNo = txtCurGrNo.Text;

                objStudentBO.AdmittedClassID = Convert.ToInt32(Request.Form[ddlAdmittedClass.UniqueID]);

                objStudentBO.AdmittedDivisionTID = Convert.ToInt32(Request.Form[ddlAdmittedDivision.UniqueID]);
                if (ddlAdmittedYear.SelectedItem.ToString() == "-Select-")
                {
                    objStudentBO.AdmittedYear = "-1";
                }
                else
                {
                    objStudentBO.AdmittedYear = ddlAdmittedYear.SelectedItem.ToString();
                }
                objStudentBO.AdmittedGrNo = txtAdmittedGr.Text;
                if (ViewState["Bytes"] != null)
                {
                    objStudentBO.StudentPhoto = (byte[])ViewState["Bytes"];
                }
                else if (Session["PicsName"] != null)
                {
                    objStudentBO.StudentPhoto = ImageToByteArrayFromFilePath("../Logo/StudentPhoto/" + Session["PicsName"].ToString());
                }
                else
                {
                    objStudentBO.StudentPhoto = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                }
                objStudentBO.GenderEng = rblGender.SelectedItem.ToString();
                objStudentBO.GenderGuj = rblGenderGuj.SelectedItem.ToString();
                //txtDateOfBirth.ReadOnly = false;
                objStudentBO.DateOfBirth = txtDateOfBirth.Text;
                //txtDateOfBirth.ReadOnly = true;
                objStudentBO.BirthDistrictEng = txtBirthDistrict.Text;
                objStudentBO.BirthDistrictGuj = txtBirthDistrictGuj.Text.Trim();
                objStudentBO.NationalityEng = txtNationality.Text;
                objStudentBO.NationalityGuj = txtNationalityGuj.Text.Trim();
                objStudentBO.ReligionEng = txtReligion.Text;
                objStudentBO.CasteEng = txtCaste.Text;
                objStudentBO.CasteGuj = txtCasteGuj.Text.Trim();
                objStudentBO.SubCasteEng = txtSubcasteEng.Text;
                objStudentBO.SubCasteGuj = txtSubCasteGuj.Text.Trim();
                objStudentBO.CategoryEng = rblCategory.SelectedItem.ToString();
                objStudentBO.CategoryGuj = rblCategoryGuj.Text.Trim();
                string cbspecialities = string.Empty;
                foreach (ListItem li in chkSubCategory.Items)
                {
                    if (li.Selected)
                    {
                        cbspecialities += li.Value + ",";

                    }
                    objStudentBO.SubCategory = cbspecialities;


                }
                objStudentBO.HandicapPrecent = txtHandicapePercentage.Text;
                objStudentBO.PresentAddressEng = txtCurAddress.Text.Trim();
                objStudentBO.PresentAddressGuj = txtCorrespondenceAddressGuj.Text.Trim();
                objStudentBO.PresentCityEng = txtCurCity.Text;
                objStudentBO.PresentCityGuj = txtCorrespondenceCityGuj.Text.Trim();
                objStudentBO.PresentStateEng = txtCurState.Text;
                objStudentBO.PresentStateGuj = txtCorrespondenceStateGuj.Text.Trim();
                objStudentBO.PresentPinCode = txtCurPinCode.Text;
                objStudentBO.PresentContactNo = txtCurContactNo.Text;
                objStudentBO.PresentEmailId = txtCurEmailID.Text;
                objStudentBO.PermanentAddressEng = txtPermenantAddress.Text;
                objStudentBO.PermanentAddressGuj = txtPermenantAddressGuj.Text.Trim();
                objStudentBO.PermanentCityEng = txtPermenantCity.Text;
                objStudentBO.PermanentCityGuj = txtPermenantCityGuj.Text.Trim();
                objStudentBO.PermanentStateEng = txtPermenantCity.Text;
                objStudentBO.PermanentStateGuj = txtPermenantCityGuj.Text.Trim();
                objStudentBO.PermanentPinCode = txtPermenantPinCode.Text;
                objStudentBO.PermanentContactNo = txtPermenantContactNo.Text;
                objStudentBO.PermanentEmailId = txtPermenantEmailID.Text;
                objStudentBO.FatherOccupation = txtFatherOccupation.Text;
                objStudentBO.MotherOccupation = txtFatherOccupation.Text;
                objStudentBO.GardianOccupation = txtFatherOccupation.Text;
                objStudentBO.FatherQualification = txtFatherQuali.Text;
                objStudentBO.MotherQualification = txtMotherQuali.Text;
                objStudentBO.GardianQualification = txtGardianQuali.Text;
                objStudentBO.FatherMobileNo = txtFatherMobileNo.Text;
                objStudentBO.MotherMobileNo = txtMotherMobileNo.Text;
                objStudentBO.GardianMobileNo = txtGardianMobileNo.Text;
                objStudentBO.FatherEmailID = txtFatherEmailID.Text;
                objStudentBO.MotherEmailID = txtMotherEmailID.Text;
                objStudentBO.GardianEmailID = txtGardianEmailID.Text;
                objStudentBO.Height = txthight.Text;
                objStudentBO.Weight = txtweight.Text;
                objStudentBO.Hobbies = txtHobbies.Text;
                objStudentBO.StatusMasterID = Convert.ToInt32(ddlStatus.SelectedValue);
                objStudentBO.LeftDate = txtLeftDate.Text;
                objStudentBO.LeftReason = txtLeftReason.Text;
                if (ddlLeftYear.SelectedItem.ToString() == "-Select-")
                {
                    objStudentBO.LeftYear = "-1";
                }
                else
                {
                    objStudentBO.LeftYear = ddlLeftYear.SelectedItem.ToString();
                }
                objStudentBO.LeftStd = txtLeftClass.Text;
                objStudentBO.LcNo = txtLcNo.Text;
                objStudentBO.LcDate = txtLCDate.Text;
                objStudentBO.LcRemarks = txtLCRemark.Text;
                objStudentBO.LcCopy = txtLCCopy.Text;
                objStudentBO.RegisteredYear = ddlRegisterdYear.SelectedItem.ToString();
                objStudentBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objStudentBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objStudentBO.AdmissionDate = txtAdmittednDate.Text;
                objStudentBO.BankAccount = txtBankAccount.Text;
                objStudentBO.GVUniqueID = txtGovUniqueID.Text;
                objStudentBO.MotherTongue = txtMotherTongue.Text;
                objStudentBO.PreviousSchoolDetails = txtPreviousSchoolDetail.Text;
                objStudentBO.PhysicalIdentification = txtphysicalIdentification.Text;
                objStudentBO.FatherOrganisationName = txtFatherOrganisationName.Text;
                objStudentBO.FatherOrganisationContactNumber = txtFatherOrganisationContactNo.Text;
                objStudentBO.BloodGroup = txtBloodGroup.Text;
                objStudentBO.IFSCCode = txtIFSCCode.Text;
                objStudentBO.BranchName = txtBranchName.Text;
                objStudentBO.AccountNumber = txtAccountNumber.Text;
                objStudentBO.TypeOfVehicle = txtTypeOfVehicle.Text;
                objStudentBO.VehicleNo = txtVehicleNo.Text;
                objStudentBO.DriverName = txtDriverName.Text;
                objStudentBO.DriverContactNo = txtDriverContactNo.Text;
                objStudentBO.AadharCardNo = txtAadharCardNo.Text;
                objStudentBO.RollNumber = txtRollNumber.Text;
                objStudentBO.GvUniqueNo = txtGovUniqueNo.Text;

                if (ValidateName() == true)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Student GR No " + txtCurGrNo.Text + " Already Exists.');</script>");
                    goto Exit;
                }
                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();
                if (ViewState["Mode"].ToString() == "Save")
                {
                    objStudentBO.IsLeavingCerti = 0;
                    objStudentBO.IsLeavingGujaratiCerti = 0;
                    objResults = objStudentBL.Student_Insert(objStudentBO);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ViewState["StudentMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                            hdnStudentID.Value = ViewState["StudentMID"].ToString();
                        }
                        objStudentTBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                        objStudentTBO.ClassMID = Convert.ToInt32(Request.Form[ddlClass.UniqueID]);
                        objStudentTBO.Year = ddlCurAdmissionYear.SelectedItem.ToString();
                        objStudentTBO.DivisionTID = Convert.ToInt32(Request.Form[ddlDivision.UniqueID]);
                        objStudentTBO.StatusMasterID = Convert.ToInt32(ddlStatus.SelectedValue);
                        objStudentTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objStudentTBO.StatusName = ddlStatus.SelectedItem.ToString(); 
                        objStudentTBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objStudentTBO.GrNo = txtCurGrNo.Text;

                        objResults = objStudentBL.StudentT_Insert(objStudentTBO);
                        if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Student Created Successfully.";
                            ClearAll();
                            hfTab.Value = "5";
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Student GRNo Already Exist.";
                            ClearAll();
                            hfTab.Value = "5";
                        }

                    }
                }
                else
                {
                    objStudentBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                    objStudentBO.CurrentClassID = Convert.ToInt32(ViewState["CurrentClass"].ToString());
                    objStudentBO.CurrentDivisionTID = Convert.ToInt32(ViewState["CurrentDivision"].ToString());
                    objStudentBO.CurrentSectionID = Convert.ToInt32(ViewState["CurrentSection"].ToString());
                    objStudentBO.AdmittedClassID = Convert.ToInt32(Request.Form[ddlAdmittedClass.UniqueID]);
                    objStudentTBO.IsLate = (chkIsLate.Checked == true) ? 1 : 0 ;
                    
                    objStudentBO.AdmittedDivisionTID = Convert.ToInt32(Request.Form[ddlAdmittedDivision.UniqueID]);
                    objResults = objStudentBL.Student_Update(objStudentBO, objStudentTBO);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        //objResults = objStudentBL.StudentT_Update(objStudentTBO);
                        //if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                        //{
                        hfTab.Value = "4";
                        hdnStudentID.Value = ViewState["StudentMID"].ToString();
                        lblMessage.Visible = true;
                        lblMessage.Text = "Student updated successfully.";
                        ClearAll();
                        ViewState["Mode"] = "Save";
                        btnSave.Text = "Save";
                        //}
                    }
                    else
                    {
                        hfTab.Value = "4";
                        hdnStudentID.Value = ViewState["StudentMID"].ToString();
                        lblMessage.Visible = true;
                        lblMessage.Text = "Student GRNo Already Exists.";
                        ClearAll();
                        ViewState["Mode"] = "Save";
                        btnSave.Text = "Save";
                    }
                }
                DatabaseTransaction.CommitTransation();
                #endregion
            Exit: ;

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Go for Searching Student
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                StudentBL objStudentBL = new StudentBL();
                StudentBO objStudentBO = new StudentBO();

                ApplicationResult objResultProgram = new ApplicationResult();
                objResultProgram = objStudentBL.Student_Search_StudentName(txtSearchName.Text.Trim(), Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
                if (objResultProgram != null)
                {
                    if (objResultProgram.resultDT.Rows.Count > 0)
                    {
                        gvStudent.Visible = true;
                        gvStudent.DataSource = objResultProgram.resultDT;
                        gvStudent.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                        gvStudent.Visible = false;
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
        public static string LoadSection(int intSchoolID)
        {
            try
            {
                StudentDetailMaster objStudentDetailMaster = new StudentDetailMaster();
                DataTable dtStudent = new DataTable();
                dtStudent = objStudentDetailMaster.FetchSection(intSchoolID);
                string res = "";
                res = DataSetToJSON(dtStudent);
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadClass(int intSectionID, int intSchoolMID)
        {
            try
            {
                #region Bind Class
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objClassBL.Class_SelectAll_SectionWise_ForDropDown(intSectionID, intSchoolMID);
                if (objResultSection != null)
                {
                    if (objResultSection.resultDT.Rows.Count > 0)
                    {

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
        public static string LoadAdmittedClass(int intSchoolMID)
        {
            try
            {
                #region Bind Class
                ClassBL objClassBL = new ClassBL();
                ClassBO objClassBO = new ClassBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objClassBL.Class_SelectAll(intSchoolMID);
                if (objResultSection != null)
                {
                    if (objResultSection.resultDT.Rows.Count > 0)
                    {

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
        public static string LoadDivision(int intClassMID, int intSchoolMID)
        {
            try
            {
                #region Bind Division
                DivisionTBL objDivisionBL = new DivisionTBL();
                DivisionTBO objDivisionBO = new DivisionTBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
                if (objResultSection != null)
                {
                    if (objResultSection.resultDT.Rows.Count > 0)
                    {

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

        #endregion

        #region FetchSection
        private DataTable FetchSection(int intSchoolID)
        {
            // DataTable dtStudent = new DataTable();
            SectionBL objSectionBL = new SectionBL();
            SectionBO objSectionBO = new SectionBO();
            ApplicationResult objResults = new ApplicationResult();
            objResults = objSectionBL.Section_SelectAll_ForDropDown(intSchoolID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchClass
        private DataTable FetchClass(int intSectionID, int intSchoolMID)
        {
            // DataTable dtClass = new DataTable();
            ClassBL objClassBL = new ClassBL();
            ClassBO objClassBO = new ClassBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objClassBL.Class_SelectAll_SectionWise_ForDropDown(intSectionID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region FetchAdmittedClass
        private DataTable FetchAdmittedClass(int intSchoolMID)
        {
            // DataTable dtClass = new DataTable();
            ClassBL objClassBL = new ClassBL();
            ClassBO objClassBO = new ClassBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objClassBL.Class_SelectAll(intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch Division
        private DataTable FetchDivision(int intClassMID, int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();
            DivisionTBL objDivisionBL = new DivisionTBL();
            DivisionTBO objDivisionBO = new DivisionTBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objDivisionBL.Division_SelectAll_ClassWise_ForDropDown(intClassMID, intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Row Command
        protected void gvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                Controls objControls = new Controls();
                StudentBL objStudentBL = new StudentBL();
                ApplicationResult objResults = new ApplicationResult();

                ViewState["StudentMID"] = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName.ToString() == "Edit1")
                {
                    Manage_Student(1);
                    dt = FetchSection(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (dt.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(dt, ddlSection, "SectionName", "SectionMID");
                    }
                    ddlSection.Items.Insert(0, new ListItem("-Select-", "-1"));


                    objResults = objStudentBL.Student_Select(Convert.ToInt32(ViewState["StudentMID"].ToString()), 0);

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            txtStudentFirstName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTFIRSTNAMEENG].ToString();
                            txtStudentFirstNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTFIRSTNAMEGUJ].ToString();
                            txtStudentMiddleName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTMIDDLENAMEENG].ToString();
                            txtStudentMiddleNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTMIDDLENAMEGUJ].ToString();
                            txtStudentLastName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTLASTNAMEENG].ToString();
                            txtStudentLastNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTLASTNAMEGUJ].ToString();

                            txtFatherFirstName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERFIRSTNAMEENG].ToString();
                            txtFatherFirstNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERFIRSTNAMEGUJ].ToString();
                            txtFatherMiddleName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERMIDDLENAMEENG].ToString();
                            txtFatherMiddleNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERMIDDLENAMEGUJ].ToString();
                            txtFatherLastName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERLASTNAMEENG].ToString();
                            txtFatherLastNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERLASTNAMEGUJ].ToString();

                            txtMotherFirstName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERFIRSTNAMEENG].ToString();
                            txtMotherFirstNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERFIRSTNAMEGUJ].ToString();
                            txtMotherMiddleName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERMIDDLENAMEENG].ToString();
                            txtMotherMiddleNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERMIDDLENAMEGUJ].ToString();
                            txtMotherLastName.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERLASTNAMEENG].ToString();
                            txtMotherLastNameGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERLASTNAMEGUJ].ToString();

                            txtCurAdmissionNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMISSIONNO].ToString();
                            txtCurAdmissionDate.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDATE].ToString();
                            txtGovUniqueID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GVUNIQUEID].ToString();
                            txtBankAccount.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_BANKACCOUNT].ToString();
                            txtJoiningDate.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_JOININGDATE].ToString();
                            if (objResults.resultDT.Rows[0][StudentTBO.STUDENTT_ISLATE].ToString() == "0")
                            {
                                chkIsLate.Checked = false;
                            }
                            else
                            {
                                chkIsLate.Checked = true;
                            }
                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString() != "-Select-")
                            {
                                ddlCurAdmissionYear.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTYEAR].ToString();
                            }
                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_REGISTERDYEAR].ToString() != "-Select-")
                            {
                                ddlRegisterdYear.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_REGISTERDYEAR].ToString();
                            }
                            // ddlAdmittedYear.SelectedValue = dtStudent.Rows[0][StudentBO.STUDENT_ADMITTEDYEAR].ToString();
                            ddlSection.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTSECTIONID].ToString();
                            ViewState["CurrentSection"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTSECTIONID].ToString();
                            dt = FetchClass(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlClass, "ClassName", "ClassMID");
                                // objControls.BindDropDown_ListBox(dt, ddlAdmittedClass, "ClassName", "ClassMID");
                            }

                            ddlClass.Items.Insert(0, new ListItem("-Select-", "-1"));

                            ddlClass.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTCLASSID].ToString();
                            ViewState["CurrentClass"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTCLASSID].ToString();
                            dt = FetchDivision(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlDivision, "DivisionName", "DivisionTID");
                                // objControls.BindDropDown_ListBox(dt, ddlAdmittedDivision, "DivisionName", "DivisionTID");
                            }
                            ddlDivision.Items.Insert(0, new ListItem("-Select-", "-1"));

                            ddlDivision.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDIVISIONTID].ToString();
                            ViewState["CurrentDivision"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTDIVISIONTID].ToString();
                            txtCurGrNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CURRENTGRNO].ToString();
                            txtAdmittedGr.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDGRNO].ToString();

                            dt = FetchAdmittedClass(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (dt.Rows.Count > 0)
                            {
                                // objControls.BindDropDown_ListBox(dt, ddlClass, "ClassName", "ClassMID");
                                objControls.BindDropDown_ListBox(dt, ddlAdmittedClass, "ClassName", "ClassMID");
                            }
                            ddlAdmittedClass.Items.Insert(0, new ListItem("-Select-", "-1"));
                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDCLASSID].ToString() != "-1")
                            {
                                ddlAdmittedClass.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDCLASSID].ToString();
                                ViewState["AdmittedClass"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDCLASSID].ToString();
                            }
                            else
                            {
                                ViewState["AdmittedClass"] = -1;
                            }
                            dt = FetchDivision(Convert.ToInt32(ddlAdmittedClass.SelectedValue), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (dt.Rows.Count > 0)
                            {
                                // objControls.BindDropDown_ListBox(dt, ddlDivision, "DivisionName", "DivisionTID");
                                objControls.BindDropDown_ListBox(dt, ddlAdmittedDivision, "DivisionName", "DivisionTID");
                            }
                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDDIVISIONTID].ToString() != "-1")
                            {
                                ddlAdmittedDivision.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDDIVISIONTID].ToString();
                                ViewState["AdmittedDivision"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDDIVISIONTID].ToString();
                            }
                            else
                            {
                                ViewState["AdmittedDivision"] = -1;
                            }
                            ddlAdmittedDivision.Items.Insert(0, new ListItem("-Select-", "-1"));

                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDYEAR].ToString() != "-Select-" && objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDYEAR].ToString() != "-1")
                            {
                                ddlAdmittedYear.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMITTEDYEAR].ToString();
                            }
                            ViewState["Bytes"] = objResults.resultDT.Rows[0][StudentBO.STUDENT_STUDENTPHOTO];
                            if (ViewState["Bytes"].ToString() != "")
                            {
                                imgphoto.ImageUrl = "GetImage.ashx?StudentMID=" + ViewState["StudentMID"];
                            }
                            else
                            {
                                imgphoto.ImageUrl = "~/images/noimage-big.jpg";
                                // lbtnRemovePhoto.Visible = false;
                            }

                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_GENDERENG].ToString().ToUpper() == "Male".ToUpper())
                            {
                                rblGender.Items[0].Selected = true;
                                if (ViewState["Bytes"].ToString() != "")
                                {
                                    imgphoto.ImageUrl = "GetImage.ashx?StudentMID=" + ViewState["StudentMID"];
                                }
                                else {
                                    imgphoto.ImageUrl = "../Images/MALE_ICON.png";
                                }
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_GENDERENG].ToString().ToUpper() == "Female".ToUpper())
                            {
                                rblGender.Items[1].Selected = true;
                                if (ViewState["Bytes"].ToString() != "")
                                {
                                    imgphoto.ImageUrl = "GetImage.ashx?StudentMID=" + ViewState["StudentMID"];
                                }
                                else {
                                    imgphoto.ImageUrl = "../Images/FEMALE_ICON.png";
                                }
                            }

                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_GENDERGUJ].ToString() == "પુરૂષ")
                            {
                                rblGenderGuj.Items[0].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_GENDERGUJ].ToString() == "સ્ત્રી")
                            {
                                rblGenderGuj.Items[1].Selected = true;
                            }

                            txtDateOfBirth.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_DATEOFBIRTH].ToString();
                            txtBirthDistrict.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_BIRTHDISTRICTENG].ToString();
                            txtBirthDistrictGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_BIRTHDISTRICTGUJ].ToString();
                            txtNationality.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_NATIONALITYENG].ToString();
                            txtNationalityGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_NATIONALITYGUJ].ToString();
                            txtReligion.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_RELIGIONENG].ToString();
                            txtCaste.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CASTEENG].ToString();
                            txtCasteGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_CASTEGUJ].ToString();
                            txtSubcasteEng.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_SUBCASTEENG].ToString();
                            txtSubCasteGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_SUBCASTEGUJ].ToString();

                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYENG].ToString() == "Open")
                            {
                                rblCategory.Items[0].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYENG].ToString() == "OBC")
                            {
                                rblCategory.Items[1].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYENG].ToString() == "SC")
                            {
                                rblCategory.Items[2].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYENG].ToString() == "ST")
                            {
                                rblCategory.Items[3].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYENG].ToString() == "Other")
                            {
                                rblCategory.Items[4].Selected = true;
                            }

                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYGUJ].ToString() == "સામાન્ય")
                            {
                                rblCategoryGuj.Items[0].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYGUJ].ToString() == "બક્ષીપંચ")
                            {
                                rblCategoryGuj.Items[1].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYGUJ].ToString() == "અનુસૂચિત જાતિ")
                            {
                                rblCategoryGuj.Items[2].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYGUJ].ToString() == "અનુસૂચિત જન જાતિ")
                            {
                                rblCategoryGuj.Items[3].Selected = true;
                            }
                            else if (objResults.resultDT.Rows[0][StudentBO.STUDENT_CATEGORYGUJ].ToString() == "અન્ય")
                            {
                                rblCategoryGuj.Items[4].Selected = true;
                            }

                            //SubCategory

                            string SubCategory = objResults.resultDT.Rows[0][StudentBO.STUDENT_SUBCATEGORY].ToString();
                            string[] words = SubCategory.Split(',');
                            foreach (string word in words)
                            {
                                if (word == "Deaf")
                                {
                                    chkSubCategory.Items[0].Selected = true;
                                }
                                else if (word == "Blind")
                                {
                                    chkSubCategory.Items[1].Selected = true;
                                }
                                else if (word == "Physically Challenged")
                                {
                                    chkSubCategory.Items[2].Selected = true;
                                }
                                else if (word == "OverSease")
                                {
                                    chkSubCategory.Items[3].Selected = true;
                                }
                                else if (word == "Other Defects")
                                {
                                    chkSubCategory.Items[4].Selected = true;
                                }
                            }

                            txtHandicapePercentage.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_HANDICAPPRECENT].ToString();
                            txtOtherDefects.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_OTHERDEFECT].ToString();
                            txtCurAddress.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTADDRESSENG].ToString();
                            txtCorrespondenceAddressGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTADDRESSGUJ].ToString();
                            txtCurCity.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTCITYENG].ToString();

                            txtCorrespondenceCityGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTCITYGUJ].ToString();
                            txtCorrespondenceStateGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTSTATEGUJ].ToString();
                            txtCurState.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTSTATEENG].ToString();
                            txtCurPinCode.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTPINCODE].ToString();
                            txtCurContactNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTCONTACTNO].ToString();
                            txtCurEmailID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTEMAILID].ToString();

                            txtPermenantAddress.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTADDRESSENG].ToString();
                            txtPermenantAddressGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTADDRESSGUJ].ToString();
                            txtPermenantCity.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTCITYENG].ToString();
                            txtPermenantCityGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTCITYGUJ].ToString();
                            txtPermenantState.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTSTATEENG].ToString();
                            txtPermenantStateGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTSTATEGUJ].ToString();
                            txtPermenantPinCode.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTPINCODE].ToString();
                            txtPermenantContactNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTCONTACTNO].ToString();
                            txtPermenantEmailID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PERMANENTEMAILID].ToString();

                            txtFatherOccupation.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHEROCCUPATION].ToString();
                            txtMotherOccupation.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHEROCCUPATION].ToString();
                            txtGardianOccupation.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GARDIANOCCUPATION].ToString();

                            txtFatherQuali.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERQUALIFICATION].ToString();
                            txtMotherQuali.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERQUALIFICATION].ToString();
                            txtGardianQuali.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GARDIANQUALIFICATION].ToString();
                            txtFatherMobileNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERMOBILENO].ToString();
                            txtMotherMobileNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERMOBILENO].ToString();
                            txtGardianMobileNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GARDIANMOBILENO].ToString();
                            txtFatherEmailID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHEREMAILID].ToString();
                            txtMotherEmailID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHEREMAILID].ToString();
                            txtGardianEmailID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GARDIANEMAILID].ToString();
                            txthight.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_HEIGHT].ToString();
                            txtweight.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_WEIGHT].ToString();
                            txtHobbies.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_HOBBIES].ToString();
                            //isLeft
                            ddlStatus.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_STATUSMASTERID].ToString();
                            txtLeftDate.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LEFTDATE].ToString();
                            txtLeftReason.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LEFTREASON].ToString();
                            if (objResults.resultDT.Rows[0][StudentBO.STUDENT_LEFTYEAR].ToString() != "-Select-")
                            {
                                ddlLeftYear.SelectedValue = objResults.resultDT.Rows[0][StudentBO.STUDENT_LEFTYEAR].ToString();
                            }
                            txtLeftClass.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LEFTSTD].ToString();
                            txtLcNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LCNO].ToString();
                            txtLCDate.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LCDATE].ToString();
                            txtLCRemark.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LCREMARKS].ToString();
                            txtLCCopy.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_LCCOPY].ToString();


                            txtCorrespondenceAddressGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTADDRESSGUJ].ToString();
                            txtCurCity.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTCITYENG].ToString();

                            txtCorrespondenceCityGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTCITYGUJ].ToString();
                            txtCorrespondenceStateGuj.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTSTATEGUJ].ToString();
                            txtCurState.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTSTATEENG].ToString();
                            txtCurPinCode.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTPINCODE].ToString();
                            txtCurContactNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTCONTACTNO].ToString();
                            txtCurEmailID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_PRESENTEMAILID].ToString();
                            txtAdmittednDate.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_ADMISSIONDATE].ToString();
                            txtGovUniqueID.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GVUNIQUEID].ToString();
                            txtBankAccount.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_BANKACCOUNT].ToString();
                            txtMotherTongue.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_MOTHERTONGUE].ToString();
                            txtPreviousSchoolDetail.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_PREVIOUSSCHOOLDETAILS].ToString();
                            txtphysicalIdentification.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_PHYSICALIDENTIFICATION].ToString();
                            txtIFSCCode.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_IFSCCODE].ToString();
                            txtBranchName.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_BRANCHName].ToString();
                            txtAccountNumber.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_ACCOUNTNUMBER].ToString();
                            txtAadharCardNo.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_AADHARCARDNO].ToString();
                            txtBloodGroup.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_BloodGroup].ToString();
                            txtRollNumber.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_ROLLNUMBER].ToString();
                            txtTypeOfVehicle.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_TYPEOFVEHICLE].ToString();
                            txtVehicleNo.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_VEHICLENo].ToString();
                            txtDriverName.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_DRIVERNAME].ToString();
                            txtDriverContactNo.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_DRIVERCONTACTNO].ToString();
                            txtFatherOrganisationName.Text= objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERORGANISATIONNAME].ToString();
                            txtFatherOrganisationContactNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_FATHERORGANISATIONCONTACTNUMBER].ToString();
                            txtGovUniqueNo.Text = objResults.resultDT.Rows[0][StudentBO.STUDENT_GVUNIQUENO].ToString();
                            //Vishal Modified
                            //objResults = objStudentBL.StudentT_Select(Convert.ToInt32(ViewState["StudentMID"].ToString()));
                            //if (objResults != null)
                            //{
                            //    if (objResults.resultDT.Rows.Count > 0)
                            //    {
                            //        //txtDivisionName.Text = dtStudentT.Rows[0][DivisionTBO.DIVISIONT_DIVISIONNAME].ToString();
                            //        ddlClass.SelectedValue = objResults.resultDT.Rows[0][StudentTBO.STUDENTT_CLASSMID].ToString();
                            //        ddlCurAdmissionYear.SelectedValue = objResults.resultDT.Rows[0][StudentTBO.STUDENTT_YEAR].ToString();

                            //    }
                            //}

                            ViewState["Mode"] = "Edit1";
                            hdnStudentID.Value = ViewState["StudentMID"].ToString();
                            if (Session[ApplicationSession.USERID] != null || Session[ApplicationSession.USERID] != "")
                            {

                                hdnLastUserID.Value = Session[ApplicationSession.USERID].ToString();
                            }
                            hfTab.Value = "1";
                            PanelGrid_VisibilityMode(2);
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

        #region ValidateName
        public bool ValidateName()
        {
            ApplicationResult objResults = new ApplicationResult();
            StudentBL objStudentBL = new StudentBL();
            StudentBO objStudentBO = new StudentBO();

            if (ViewState["Mode"].ToString() == "Save")
            {
                objResults = objStudentBL.Student_ValidateName(txtCurGrNo.Text, objStudentBO.CurrentSectionID, -1);
            }
            else
            {
                objResults = objStudentBL.Student_ValidateName(txtCurGrNo.Text, objStudentBO.CurrentSectionID, Convert.ToInt32(ViewState["StudentMID"].ToString()));
            }

            if (objResults.resultDT.Rows.Count > 0)

                return true;
            return false;

        }
        #endregion

        #region PanelGrid_VisibilityMode
        private void PanelGrid_VisibilityMode(int intMode)
        {
            ApplicationResult objResultProgram = new ApplicationResult();
            StudentBL objStudentBL = new StudentBL();
            objResultProgram = objStudentBL.Student_Search_StudentName(txtSearchName.Text.Trim(), Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
            if (objResultProgram != null)
            {
                if (intMode == 1)
                {
                    GridPanel.Visible = true;
                    divStudent.Visible = false;
                    lnkAddNewStudent.Visible = true;
                }
                else if (intMode == 2)
                {
                    GridPanel.Visible = false;
                    divStudent.Visible = true;
                    lnkAddNewStudent.Visible = true;
                }
            }
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

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            ViewState["Mode"] = "Save";
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            rblGender.SelectedIndex = -1;
            rblGenderGuj.SelectedIndex = -1;
            Session["PicsName"] = null;
            Session["ImageNumber"] = null;
            ViewState["Bytes"] = null;
            gvStudent.DataSource = null;
            gvStudent.DataBind();
        }
        #endregion

        #region Manage Student
        private void Manage_Student(int intMode)
        {
            if (intMode == 1)
            {


                //Student Detail

                ddlSection.Enabled = false;
                //txtAdmittedGr.Enabled = false;
                txtCurAdmissionDate.Enabled = false;
                ddlClass.Enabled = false;
                ddlDivision.Enabled = false;
                txtCurAdmissionDate.Enabled = false;
                ddlCurAdmissionYear.Enabled = false;
                ddlRegisterdYear.Enabled = false;
            }
            else if (intMode == 2)
            {


                //Student Detail

                ddlSection.Enabled = true;
                //txtAdmittedGr.Enabled = true;
                txtCurAdmissionDate.Enabled = true;
                ddlClass.Enabled = true;
                ddlDivision.Enabled = true;
                txtCurAdmissionDate.Enabled = true;
                ddlCurAdmissionYear.Enabled = true;
                ddlRegisterdYear.Enabled = true;
                //ddlAdmittedClass.Enabled = true;
                //ddlAdmittedDivision.Enabled = true;
                //txtAdmittednDate.Enabled = true;
                //ddlAdmittedYear.Enabled = true;
            }
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
                    imgphoto.ImageUrl = "../Logo/StudentPhoto/" + Session["PicsName"].ToString(); // + Session["ImageNumber"] + ".jpg";
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
            Session["PicsName"] = (txtStudentFirstName.Text + System.DateTime.Now.ToLongDateString()).ToString().Trim();
            ModalPopupExtender1.Show();
            //foreach (var file in Directory.GetFiles(Server.MapPath("../VisitorPics/")))
            //{
            //    File.Delete(file);
            //}
            imgphoto.ImageUrl = "~/Images/NoImage-big.jpg";
        }
        #endregion

        protected void lnkCancelPhoto_Click(object sender, EventArgs e)
        {
            divModal.Visible = false;
        }

        #region Get Student Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllStudentNameForReport(string prefixText, int SearchType, int SchoolMID, int SectionType, int ClassType, int DivisionType)
        {
            StudentBL objEmployeeMbl = new StudentBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Student_ForAutocomplete(strSearchText, SearchType, SchoolMID, SectionType, ClassType, DivisionType);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    if (SearchType == 1)
                    {
                        string strStudentName = objResult.resultDT.Rows[i]["StudentName"].ToString();
                        string strStudentMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentName, strStudentMID));
                    }
                    else if (SearchType == 2)
                    {
                        string strStudentGRNo = objResult.resultDT.Rows[i]["CurrentGrNo"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strStudentGRNo, strEmployeeMID));
                    }
                    else if (SearchType == 3)
                    {
                        string strAdmission = objResult.resultDT.Rows[i]["AdmissionNo"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strAdmission, strEmployeeMID));
                    }
                    else if (SearchType == 4)
                    {
                        string strUniqueID = objResult.resultDT.Rows[i]["UniqueID"].ToString();
                        string strEmployeeMID = objResult.resultDT.Rows[i][StudentBO.STUDENT_STUDENTMID].ToString();
                        result.Add(string.Format("{0}~{1}", strUniqueID, strEmployeeMID));
                    }
                }
            }
            return result.ToArray();
        }


        #endregion

        #region Section Wise Class     
        protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClass();
        }
        #endregion

        #region Division Details Class Wise
        protected void ddlClassName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDivision();
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

       
    }
}