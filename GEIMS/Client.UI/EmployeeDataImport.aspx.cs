using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;
using OfficeOpenXml;




namespace GEIMS.Client.UI
{
    public partial class EmployeeDataImport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmployeeDataImport));
        bool isDateTime = false;
        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDesignationName();
                getEmployeeRoleName();
                ddlOrgName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlDepartment.Items.Insert(0, new ListItem("--Select--", ""));
                btnSave.Visible = false;

            }
        }
        #endregion

        #region Organisation Selected Cahnge Event
        protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrganisation.SelectedItem.Text == "Trust")
            {
                getTrustName();
            }
            else
            {
                getSchoolName();
            }
        }
        #endregion

        #region Bind Trust Name
        public void getTrustName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            TrustBL objTrustBl = new TrustBL();

            objResult = objTrustBl.Trust_SelectAll_ForDropDown();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlOrgName, "TrustNameEng", "TrustMID");
                if (objResult.resultDT.Rows.Count > 0)
                {


                }
                ddlOrgName.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        #region Bind School Name
        public void getSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlOrgName, "SchoolNameEng", "SchoolMID");
                if (objResult.resultDT.Rows.Count > 0)
                {


                }
                ddlOrgName.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        #region bind Designation Name
        public void getDesignationName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            DesignationBL objDesignationBl = new DesignationBL();

            objResult = objDesignationBl.Designation_SelectAll_ForDropDown();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddldesignation, "DesignationNameEng", "DesignationID");
                if (objResult.resultDT.Rows.Count > 0)
                {


                }
                ddldesignation.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        #region bind Employee Role Name
        public void getEmployeeRoleName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            RoleBL objRoleBL = new RoleBL();

            objResult = objRoleBL.Role_SelectAll_ForDropDown();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlEmployeeRole, "RoleName", "RoleId");
                if (objResult.resultDT.Rows.Count > 0)
                {


                }
                ddlEmployeeRole.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        #endregion

        #region Bind Department Name
        public void getDepartmentName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            DepartmentBL objDepartmentBl = new DepartmentBL();
            if (ddlOrganisation.SelectedItem.Text == "Trust")
            {
                objResult = objDepartmentBl.Department_Select_By_Trust_School_ForDropDown((Convert.ToInt32(ddlOrgName.SelectedValue)), 0);
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlDepartment, "DepartmentNameEng", "DepartmentID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                    }
                }
            }
            else
            {
                objResult = objDepartmentBl.Department_Select_By_Trust_School_ForDropDown((Convert.ToInt32(Session[ApplicationSession.TRUSTID])), (Convert.ToInt32(ddlOrgName.SelectedValue)));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlDepartment, "DepartmentNameEng", "DepartmentID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                    }
                }
            }
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region ddlOrgName Selected Cahnge Event(School/Trust)
        protected void ddlOrgName_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDepartmentName();
        }
        #endregion

        #region btnUpload Click Event
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Controls objControls = new Controls();
                string Extension = string.Empty;
                if (FileUpload1.HasFile)
                {
                    divGrid.Visible = true;
                    Extension = Path.GetExtension(FileUpload1.FileName);
                    if (Extension == ".xls" || Extension == ".xlsx")
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/_ImportData/" + ddlOrgName.SelectedItem.Text.Replace(" ", "").ToString() + Extension)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/_ImportData/" + ddlOrgName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                        }
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/_ImportData/" + ddlOrgName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('File format not supported');</script>");
                        goto Exit;
                    }
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/_ImportData/" + fileName);
                    FileUpload1.SaveAs(fileLocation);
                    ReadExcel(fileLocation);

                    if (ViewState["Read"].ToString() == "1")
                    {
                        imgUploder.Visible = false;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Select any File for upload.');</script>");
                }
                Exit:;
            }
            catch (Exception ex)
            {
                log4net.LogicalThreadContext.Properties["UserID"] = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                logger.Error("btnUpload_Click", ex);

            }
        }
        #endregion

        public bool IsDateTime(string text)
        {
            DateTime dateTime;


            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDateTime = DateTime.TryParse(text, out dateTime);

            return isDateTime;
        }


        #region ReadExcel
        public void ReadExcel(string filePath)
        {
            try
            {
                DataTable dtExcelResult = new DataTable();
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage xlPackage = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                    //Add Coloums, Checks for tha value in tha worksheet if there is Empty colum then colum is not created
                    int colCount = 0;
                    int rowCount = 19;
                    int Blankcount = 0;
                    for (int j = 1; j <= 104; j++)
                    {

                        dtExcelResult.Columns.Add(worksheet.Cell(18, j).Value);
                        colCount++;

                    }
                    for (int i = 19; i <= 104; i++)
                    {
                        if (Blankcount == 5)
                        {
                            goto Enter;
                        }
                        if (worksheet.Cell(i, 7).Value != "" && worksheet.Cell(i, 8).Value != "" && worksheet.Cell(i, 14).Value != "" && worksheet.Cell(i, 15).Value != "" && worksheet.Cell(i, 31).Value != "" && worksheet.Cell(i, 40).Value != "" && worksheet.Cell(i, 41).Value != "" && worksheet.Cell(i, 60).Value != "" && worksheet.Cell(i, 61).Value != "" && worksheet.Cell(i, 90).Value != "")
                        {
                            rowCount++;
                        }
                        else
                        {
                            Blankcount++;
                        }
                    }
                    Enter:;
                    for (int j = 19; j < rowCount; j++)
                    {
                        ////  if (worksheet.Cell(j, 7).Value != "" && worksheet.Cell(j, 8).Value != "" && worksheet.Cell(j, 14).Value != "" && worksheet.Cell(j, 15).Value != "" && //worksheet.Cell(j, 31).Value != "" && worksheet.Cell(j, 32).Value != "" && worksheet.Cell(j, 40).Value != "" && worksheet.Cell(j, 41).Value != "" && //worksheet.Cell(j, 60).Value != "" && worksheet.Cell(j, 61).Value != "" && worksheet.Cell(j, 91).Value != "")
                        //     {

                        if (worksheet.Cell(j, 7).Value != "" && worksheet.Cell(j, 8).Value != "" && worksheet.Cell(j, 14).Value != "" && worksheet.Cell(j, 15).Value != "" && worksheet.Cell(j, 31).Value != "" && worksheet.Cell(j, 40).Value != "" && worksheet.Cell(j, 41).Value != "" && worksheet.Cell(j, 60).Value != "" && worksheet.Cell(j, 61).Value != "" && worksheet.Cell(j, 90).Value != "")
                        {


                            string Birthdate = string.Empty;
                            string testdata = string.Empty;
                            IFormatProvider yyyymmddFormat = new System.Globalization.CultureInfo("en-GB", false);
                            testdata = worksheet.Cell(j, 11).Value;

                            //Date Verification code Start
                            //DateTime date = Convert.ToDateTime(testdata);
                            //IsDateTime(testdata);
                            //if (isDateTime == true)
                            //{
                            //    Birthdate = testdata;
                            //}

                            //else
                            //{
                            //    double d = double.Parse(testdata);
                            //    DateTime conv = DateTime.FromOADate(d);
                            //    Birthdate = conv.ToString("dd-MM-yyyy", yyyymmddFormat);
                            //}
                            //Date Verification code End


                            double d = double.Parse(testdata);
                            DateTime conv = DateTime.FromOADate(d);
                            Birthdate = conv.ToString("dd-MM-yyyy", yyyymmddFormat);

                            IFormatProvider yyyymmddFormat2 = new System.Globalization.CultureInfo("en-GB", false);
                            string DepartMentJoiningDate = string.Empty;
                            if (worksheet.Cell(j, 71).Value != null && worksheet.Cell(j, 71).Value != "")
                            {
                                double d2 = double.Parse(worksheet.Cell(j, 71).Value);
                                DateTime conv2 = DateTime.FromOADate(d2);
                                DepartMentJoiningDate = conv2.ToString("dd-MM-yyyy", yyyymmddFormat2);
                            }


                            IFormatProvider yyyymmddFormat3 = new System.Globalization.CultureInfo("en-GB", false);
                            string OrganisationJoiningDate = string.Empty;
                            if (worksheet.Cell(j, 72).Value != null && worksheet.Cell(j, 72).Value != "")
                            {
                                double d3 = double.Parse(worksheet.Cell(j, 72).Value);
                                DateTime conv3 = DateTime.FromOADate(d3);
                                OrganisationJoiningDate = conv3.ToString("dd-MM-yyyy", yyyymmddFormat3);
                            }


                            IFormatProvider yyyymmddFormat4 = new System.Globalization.CultureInfo("en-GB", false);
                            string RetirementDate = string.Empty;
                            if (worksheet.Cell(j, 75).Value != null && worksheet.Cell(j, 75).Value != "")
                            {
                                double d4 = double.Parse(worksheet.Cell(j, 75).Value);
                                DateTime conv4 = DateTime.FromOADate(d4);
                                RetirementDate = conv4.ToString("dd-MM-yyyy", yyyymmddFormat4);
                            }


                            IFormatProvider yyyymmddFormat5 = new System.Globalization.CultureInfo("en-GB", false);
                            string TermAndRetirementDate = string.Empty;
                            if (worksheet.Cell(j, 76).Value != null && worksheet.Cell(j, 76).Value != "")
                            {
                                double d5 = double.Parse(worksheet.Cell(j, 76).Value);
                                DateTime conv5 = DateTime.FromOADate(d5);
                                TermAndRetirementDate = conv5.ToString("dd-MM-yyyy", yyyymmddFormat5);
                            }


                            dtExcelResult.Rows.Add(worksheet.Cell(j, 1).Value, worksheet.Cell(j, 2).Value, worksheet.Cell(j, 3).Value, worksheet.Cell(j, 4).Value, worksheet.Cell(j, 5).Value, worksheet.Cell(j, 6).Value, worksheet.Cell(j, 7).Value, worksheet.Cell(j, 8).Value, worksheet.Cell(j, 9).Value, worksheet.Cell(j, 10).Value, Birthdate, worksheet.Cell(j, 12).Value, worksheet.Cell(j, 13).Value, worksheet.Cell(j, 14).Value, worksheet.Cell(j, 15).Value, worksheet.Cell(j, 16).Value, worksheet.Cell(j, 17).Value, worksheet.Cell(j, 18).Value, worksheet.Cell(j, 19).Value, worksheet.Cell(j, 20).Value, worksheet.Cell(j, 21).Value, worksheet.Cell(j, 22).Value, worksheet.Cell(j, 23).Value, worksheet.Cell(j, 24).Value, worksheet.Cell(j, 25).Value, worksheet.Cell(j, 26).Value, worksheet.Cell(j, 27).Value, worksheet.Cell(j, 28).Value, worksheet.Cell(j, 29).Value, worksheet.Cell(j, 30).Value, worksheet.Cell(j, 31).Value, worksheet.Cell(j, 32).Value, worksheet.Cell(j, 33).Value, worksheet.Cell(j, 34).Value, worksheet.Cell(j, 35).Value, worksheet.Cell(j, 36).Value, worksheet.Cell(j, 37).Value, worksheet.Cell(j, 38).Value, worksheet.Cell(j, 39).Value, worksheet.Cell(j, 40).Value, worksheet.Cell(j, 41).Value, worksheet.Cell(j, 42).Value, worksheet.Cell(j, 43).Value, worksheet.Cell(j, 44).Value, worksheet.Cell(j, 45).Value, worksheet.Cell(j, 46).Value, worksheet.Cell(j, 47).Value, worksheet.Cell(j, 48).Value, worksheet.Cell(j, 49).Value, worksheet.Cell(j, 50).Value, worksheet.Cell(j, 51).Value, worksheet.Cell(j, 52).Value, worksheet.Cell(j, 53).Value, worksheet.Cell(j, 54).Value, worksheet.Cell(j, 55).Value, worksheet.Cell(j, 56).Value, worksheet.Cell(j, 57).Value, worksheet.Cell(j, 58).Value, worksheet.Cell(j, 59).Value, worksheet.Cell(j, 60).Value, worksheet.Cell(j, 61).Value, worksheet.Cell(j, 62).Value, worksheet.Cell(j, 63).Value, worksheet.Cell(j, 64).Value, worksheet.Cell(j, 65).Value, worksheet.Cell(j, 66).Value, worksheet.Cell(j, 67).Value, worksheet.Cell(j, 68).Value, worksheet.Cell(j, 69).Value, worksheet.Cell(j, 70).Value, DepartMentJoiningDate, OrganisationJoiningDate, worksheet.Cell(j, 73).Value, worksheet.Cell(j, 74).Value, RetirementDate, TermAndRetirementDate, worksheet.Cell(j, 77).Value, worksheet.Cell(j, 78).Value, worksheet.Cell(j, 79).Value, worksheet.Cell(j, 80).Value, worksheet.Cell(j, 81).Value, worksheet.Cell(j, 82).Value, worksheet.Cell(j, 83).Value, worksheet.Cell(j, 84).Value, worksheet.Cell(j, 85).Value, worksheet.Cell(j, 86).Value, worksheet.Cell(j, 87).Value, worksheet.Cell(j, 88).Value, worksheet.Cell(j, 89).Value, worksheet.Cell(j, 90).Value, worksheet.Cell(j, 91).Value, worksheet.Cell(j, 92).Value, worksheet.Cell(j, 93).Value, worksheet.Cell(j, 94).Value, worksheet.Cell(j, 95).Value, worksheet.Cell(j, 96).Value, worksheet.Cell(j, 97).Value, worksheet.Cell(j, 98).Value, worksheet.Cell(j, 99).Value, worksheet.Cell(j, 100).Value, worksheet.Cell(j, 101).Value, worksheet.Cell(j, 102).Value, worksheet.Cell(j, 103).Value, worksheet.Cell(j, 104).Value);
                        }

                    }
                    ViewState["Read"] = 1;
                    btnSave.Visible = true;
                    divGrid.Visible = true;
                    gvExcelFile.Visible = true;
                    gvExcelFile.DataSource = dtExcelResult;
                    gvExcelFile.DataBind();
                }
                Exit:;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeMBO objEmployeeBo = new EmployeeMBO();
                ApplicationResult objResults = new ApplicationResult();
                EmployeeMBL objEmployeeBl = new EmployeeMBL();
                Controls objControls = new Controls();
                DatabaseTransaction.OpenConnectionTransation();
                for (int i = 0; i < gvExcelFile.Rows.Count; i++)
                {
                    if (ddlOrganisation.SelectedItem.Text == "Trust")
                    {
                        objEmployeeBo.TrustMID = Convert.ToInt32(ddlOrgName.SelectedValue);
                        objEmployeeBo.SchoolMID = 0;
                    }
                    else
                    {
                        objEmployeeBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objEmployeeBo.SchoolMID = Convert.ToInt32(ddlOrgName.SelectedValue);
                    }
                    objEmployeeBo.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    objEmployeeBo.DesignationID = Convert.ToInt32(ddldesignation.SelectedValue);
                    objEmployeeBo.EmployeeCode = gvExcelFile.Rows[i].Cells[0].Text.Replace("&nbsp;", "");
                    objEmployeeBo.EmployeeFNameENG = gvExcelFile.Rows[i].Cells[1].Text.Replace("&nbsp;", "");
                    objEmployeeBo.EmployeeFNameGUJ = gvExcelFile.Rows[i].Cells[2].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.EmployeeLNameENG = gvExcelFile.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.EmployeeLNameGUJ = gvExcelFile.Rows[i].Cells[4].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.EmployeeMNameENG = gvExcelFile.Rows[i].Cells[5].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.EmployeeMNameGUJ = gvExcelFile.Rows[i].Cells[6].Text.Replace("&nbsp;", "");


                    objEmployeeBo.EmployeeMNameENG = gvExcelFile.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                    objEmployeeBo.EmployeeMNameGUJ = gvExcelFile.Rows[i].Cells[4].Text.Replace("&nbsp;", "");
                    objEmployeeBo.EmployeeLNameENG = gvExcelFile.Rows[i].Cells[5].Text.Replace("&nbsp;", "");
                    objEmployeeBo.EmployeeLNameGUJ = gvExcelFile.Rows[i].Cells[6].Text.Replace("&nbsp;", "");

                    objEmployeeBo.Photo = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                    objEmployeeBo.Gender = gvExcelFile.Rows[i].Cells[7].Text.Replace("&nbsp;", "");
                    objEmployeeBo.GenderGuj = gvExcelFile.Rows[i].Cells[8].Text.Replace("&nbsp;", "");
                    objEmployeeBo.DateOfBirth = gvExcelFile.Rows[i].Cells[9].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BirthDistrictENG = gvExcelFile.Rows[i].Cells[10].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BirthDistrictGUJ = gvExcelFile.Rows[i].Cells[11].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BirthTalukaENG = gvExcelFile.Rows[i].Cells[12].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BirthTalukaGUJ = gvExcelFile.Rows[i].Cells[13].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BirthCityVillageENG = gvExcelFile.Rows[i].Cells[14].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BirthCityVillageGUJ = gvExcelFile.Rows[i].Cells[15].Text.Replace("&nbsp;", "");
                    objEmployeeBo.NationalityENG = gvExcelFile.Rows[i].Cells[16].Text.Replace("&nbsp;", "");
                    objEmployeeBo.NationalityGUJ = gvExcelFile.Rows[i].Cells[17].Text.Replace("&nbsp;", "");
                    objEmployeeBo.ReligionENG = gvExcelFile.Rows[i].Cells[18].Text.Replace("&nbsp;", "");
                    objEmployeeBo.ReligionGUJ = gvExcelFile.Rows[i].Cells[19].Text.Replace("&nbsp;", "");
                    objEmployeeBo.Caste = gvExcelFile.Rows[i].Cells[20].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MaritalStatus = gvExcelFile.Rows[i].Cells[21].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BloodGroup = gvExcelFile.Rows[i].Cells[22].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MotherLanguage = gvExcelFile.Rows[i].Cells[23].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentAddressENG = gvExcelFile.Rows[i].Cells[24].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentAddressGUJ = gvExcelFile.Rows[i].Cells[25].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentLandmarkENG = gvExcelFile.Rows[i].Cells[26].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentLandmarkGUJ = gvExcelFile.Rows[i].Cells[27].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentCityENG = gvExcelFile.Rows[i].Cells[28].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentCityGUJ = gvExcelFile.Rows[i].Cells[29].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentStateENG = gvExcelFile.Rows[i].Cells[30].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentStateGUJ = gvExcelFile.Rows[i].Cells[31].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CurrentPinCode = gvExcelFile.Rows[i].Cells[32].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantAddressEng = gvExcelFile.Rows[i].Cells[33].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantAddressGuj = gvExcelFile.Rows[i].Cells[34].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantCityEng = gvExcelFile.Rows[i].Cells[35].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantCityGuj = gvExcelFile.Rows[i].Cells[36].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantLandmarkEng = gvExcelFile.Rows[i].Cells[37].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantLandmarkGuj = gvExcelFile.Rows[i].Cells[38].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantStateEng = gvExcelFile.Rows[i].Cells[39].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantStateGuj = gvExcelFile.Rows[i].Cells[40].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PermenantPincode = gvExcelFile.Rows[i].Cells[41].Text.Replace("&nbsp;", "");
                    objEmployeeBo.TelephoneNo = gvExcelFile.Rows[i].Cells[42].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MobileNo = gvExcelFile.Rows[i].Cells[43].Text.Replace("&nbsp;", "");
                    objEmployeeBo.EmailId = gvExcelFile.Rows[i].Cells[44].Text.Replace("&nbsp;", "");
                    objEmployeeBo.Hobbies = gvExcelFile.Rows[i].Cells[45].Text.Replace("&nbsp;", "");
                    objEmployeeBo.RightVision = gvExcelFile.Rows[i].Cells[46].Text.Replace("&nbsp;", "");
                    objEmployeeBo.LeftVision = gvExcelFile.Rows[i].Cells[47].Text.Replace("&nbsp;", "");
                    objEmployeeBo.RectificationDevice = "";
                    objEmployeeBo.Height = gvExcelFile.Rows[i].Cells[48].Text.Replace("&nbsp;", "");
                    objEmployeeBo.Weight = gvExcelFile.Rows[i].Cells[49].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PhysicalIdentificationENG = gvExcelFile.Rows[i].Cells[50].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PhysicalIdentificationGUJ = gvExcelFile.Rows[i].Cells[51].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MotherMaidenFNameENG = gvExcelFile.Rows[i].Cells[52].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MotherMaidenFNameGUJ = gvExcelFile.Rows[i].Cells[53].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.MotherMaidenLNameENG = gvExcelFile.Rows[i].Cells[54].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.MotherMaidenLNameGUJ = gvExcelFile.Rows[i].Cells[55].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.MotherMaidenMNameENG = gvExcelFile.Rows[i].Cells[56].Text.Replace("&nbsp;", "");
                    //objEmployeeBo.MotherMaidenMNameGUJ = gvExcelFile.Rows[i].Cells[57].Text.Replace("&nbsp;", "");

                    objEmployeeBo.MotherMaidenMNameENG = gvExcelFile.Rows[i].Cells[54].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MotherMaidenMNameGUJ = gvExcelFile.Rows[i].Cells[55].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MotherMaidenLNameENG = gvExcelFile.Rows[i].Cells[56].Text.Replace("&nbsp;", "");
                    objEmployeeBo.MotherMaidenLNameGUJ = gvExcelFile.Rows[i].Cells[57].Text.Replace("&nbsp;", "");


                    objEmployeeBo.BankName = gvExcelFile.Rows[i].Cells[58].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BranchName = gvExcelFile.Rows[i].Cells[59].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BranchCode = gvExcelFile.Rows[i].Cells[60].Text.Replace("&nbsp;", "");
                    objEmployeeBo.AccountNo = gvExcelFile.Rows[i].Cells[61].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PFNo = gvExcelFile.Rows[i].Cells[62].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PANNo = gvExcelFile.Rows[i].Cells[63].Text.Replace("&nbsp;", "");
                    objEmployeeBo.ESICNo = gvExcelFile.Rows[i].Cells[64].Text.Replace("&nbsp;", "");
                    objEmployeeBo.IFSCCode = gvExcelFile.Rows[i].Cells[65].Text.Replace("&nbsp;", "");
                    objEmployeeBo.GPFAccountNo = gvExcelFile.Rows[i].Cells[66].Text.Replace("&nbsp;", "");
                    objEmployeeBo.CPFAccountNo = gvExcelFile.Rows[i].Cells[67].Text.Replace("&nbsp;", "");
                    objEmployeeBo.DepartmentJoiningDate = gvExcelFile.Rows[i].Cells[68].Text.Replace("&nbsp;", "");
                    objEmployeeBo.OrganisationJoiningDate = gvExcelFile.Rows[i].Cells[69].Text.Replace("&nbsp;", "");

                    objEmployeeBo.ReplacementSchoolInfoENG = gvExcelFile.Rows[i].Cells[70].Text.Replace("&nbsp;", "");
                    objEmployeeBo.ReplacementSchoolInfoGUJ = gvExcelFile.Rows[i].Cells[71].Text.Replace("&nbsp;", "");
                    objEmployeeBo.RetirementDate = gvExcelFile.Rows[i].Cells[72].Text.Replace("&nbsp;", "");
                    objEmployeeBo.TermEndRetirementDate = gvExcelFile.Rows[i].Cells[73].Text.Replace("&nbsp;", "");
                    objEmployeeBo.IsResigned = 0;
                    objEmployeeBo.ResignedDate = "";
                    objEmployeeBo.ResignReasonEng = "";
                    objEmployeeBo.ResignReasonGuj = "";
                    objEmployeeBo.BreakInfoENG = gvExcelFile.Rows[i].Cells[74].Text.Replace("&nbsp;", "");
                    objEmployeeBo.BreakInfoGUJ = gvExcelFile.Rows[i].Cells[75].Text.Replace("&nbsp;", "");
                    objEmployeeBo.OtherAchivementDetailsENG = gvExcelFile.Rows[i].Cells[76].Text.Replace("&nbsp;", "");
                    objEmployeeBo.OtherAchivementDetailsGUJ = gvExcelFile.Rows[i].Cells[77].Text.Replace("&nbsp;", "");
                    objEmployeeBo.IsUser = Convert.ToInt32(gvExcelFile.Rows[i].Cells[78].Text.Replace("&nbsp;", ""));
                    objEmployeeBo.UserName = gvExcelFile.Rows[i].Cells[79].Text.Replace("&nbsp;", "");
                    objEmployeeBo.Password = gvExcelFile.Rows[i].Cells[80].Text.Replace("&nbsp;", "");
                    objEmployeeBo.RoleID = Convert.ToInt32(ddlEmployeeRole.SelectedValue);
                    objEmployeeBo.IsTeacher = Convert.ToInt32(gvExcelFile.Rows[i].Cells[81].Text.Replace("&nbsp;", ""));
                    objEmployeeBo.IsPrincipal = Convert.ToInt32(gvExcelFile.Rows[i].Cells[82].Text.Replace("&nbsp;", ""));
                    objEmployeeBo.AllowAccountAccess = Convert.ToInt32(gvExcelFile.Rows[i].Cells[83].Text.Replace("&nbsp;", ""));
                    objEmployeeBo.TypeOfAppointment = gvExcelFile.Rows[i].Cells[84].Text.Replace("&nbsp;", "");
                    objEmployeeBo.TypeOfAppointmentCode = gvExcelFile.Rows[i].Cells[85].Text.Replace("&nbsp;", "");
                    objEmployeeBo.AaharCardNo = gvExcelFile.Rows[i].Cells[86].Text.Replace("&nbsp;", "");
                    objEmployeeBo.ElectionCardNo = gvExcelFile.Rows[i].Cells[87].Text.Replace("&nbsp;", "");
                    objEmployeeBo.VehicleNo = gvExcelFile.Rows[i].Cells[88].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PortalID = gvExcelFile.Rows[i].Cells[89].Text.Replace("&nbsp;", "");
                    objEmployeeBo.PRANNo = gvExcelFile.Rows[i].Cells[90].Text.Replace("&nbsp;", "");
                    objEmployeeBo.TANNO = gvExcelFile.Rows[i].Cells[91].Text.Replace("&nbsp;", "");
                    //          objEmployeeBo.ReportingTo = ;
                    objEmployeeBo.CategoryEng = "";
                    objEmployeeBo.CategoryGuj = "";
                    objEmployeeBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objEmployeeBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objEmployeeBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objEmployeeBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResults = objEmployeeBl.EmployeeM_Insert(objEmployeeBo);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Data Saved Successfully.');</script>");
                        }

                    }
                }
                DatabaseTransaction.CommitTransation();

                string Extension = string.Empty;
                Extension = Path.GetExtension(FileUpload1.FileName);
                System.IO.File.Delete(Server.MapPath("../_ImportData/" + ddlOrgName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                string Extension = string.Empty;
                Extension = Path.GetExtension(FileUpload1.FileName);
                System.IO.File.Delete(Server.MapPath("../_ImportData/" + ddlOrgName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

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
    }
}