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
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;

namespace GEIMS.Client.UI
{
    public partial class StudentDataImport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(StudentDataImport));

        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindStatus();
                    GetSchoolName();
                    divGrid.Visible = false;
                    ViewState["Mode"] = "Save";
                    btnSave.Visible = false;
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Data Saved Successfully.');</script>");
            }
        }
        #endregion

        #region View Button Event
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Fetch School

        public void GetSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchoolName, "SchoolNameEng", "SchoolMID");
                if (objResult.resultDT.Rows.Count > 0)
                {


                }
                ddlSchoolName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlSection.Items.Insert(0, new ListItem("--Select--", ""));
                ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }

        #endregion

        #region Section Seclected Change Event
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                ClassBL objClassBl = new ClassBL();

                objResult = objClassBl.Class_SelectAll_SectionWise_ForDropDown(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName, "ClassName", "ClassMID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {


                    }
                    ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region School Selected Change Event
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SectionBL objSectionBl = new SectionBL();

                objResult = objSectionBl.Section_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {


                    }
                    ddlSection.Items.Insert(0, new ListItem("--Select--", ""));

                }

                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 0;

                objResults = objSchoolBl.School_Select(Convert.ToInt32(ddlSchoolName.SelectedValue));

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                    }

                }
                #endregion


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

                objControls.BindDropDown_ListBox(dt, ddlYear, "AcademicYear", "AcademicYear");
                ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Class Selected Cahnge Event
        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DivisionTBL objDivisionTbl = new DivisionTBL();

                objResult = objDivisionTbl.Division_SelectAll_ClassWise_ForDropDown(Convert.ToInt32(ddlClassName.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivisionName, "DivisionName", "DivisionTID");

                    }
                    ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception)
            {

                throw;
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
                    objControls.BindDropDown_ListBox(objResults.resultDT, ddlStatus, "StatusName", "StatusMasterID");
                    if (objResults.resultDT.Rows.Count > 0)
                    {

                    }
                    ddlStatus.Items.Insert(0, new ListItem("-Select-", ""));
                }
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
                        gvExcelFile.DataSource = objResult.resultDT;
                        gvExcelFile.DataBind();
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

        #region btnImport Click Event
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                try
                {
                    string path = string.Concat(Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
                    FileUpload1.SaveAs(path);
                    // Connection String to Excel Workbook
                    string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    using (OleDbConnection con = new OleDbConnection(excelCS))
                    {
                        OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                        con.Open();
                        // Create DbDataReader to Data Worksheet
                        DbDataReader dr = cmd.ExecuteReader();
                        // SQL Server Connection String
                        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                        // Bulk Copy to SQL Server 
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
                        bulkInsert.DestinationTableName = "Employee";
                        bulkInsert.WriteToServer(dr);
                        BindStdentGrid();
                       // lblMessage.Text = "Your file uploaded successfully";
                        //lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception)
                {
                    //lblMessage.Text = "Your file not uploaded";
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

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
                    int rowCount = 20;
                    int Blankcount = 0;
                    for (int j = 1; j <= 114; j++)  //112 new  //97 old
                    {

                        dtExcelResult.Columns.Add(worksheet.Cell(19, j).Value);
                        colCount++;

                    }
                    for (int i = 20; i <= 118; i++)  //102 Old   // 117 new
                    {
                        if (Blankcount == 5)
                        {
                            goto Enter;
                        }
                        if (worksheet.Cell(i, 3).Value != "" && worksheet.Cell(i, 5).Value != "" && worksheet.Cell(i, 6).Value != "" && worksheet.Cell(i, 8).Value != "" && worksheet.Cell(i, 33).Value != "" && worksheet.Cell(i, 34).Value != "")
                        {
                            rowCount++;
                        }
                        else
                        {
                            Blankcount++;
                        }
                    }
                    Enter:;
                    for (int j = 20; j <= rowCount; j++)
                    {
                        if (worksheet.Cell(j, 3).Value != "" && worksheet.Cell(j, 5).Value != "" && worksheet.Cell(j, 6).Value != "" && worksheet.Cell(j, 8).Value != "" && worksheet.Cell(j, 33).Value != "" && worksheet.Cell(j, 34).Value != "")
                        {
                            IFormatProvider yyyymmddFormat = new System.Globalization.CultureInfo("en-GB", false);
                            double d = double.Parse(worksheet.Cell(j, 34).Value);
                            DateTime conv = DateTime.FromOADate(d);
                            string Birthdate = conv.ToString("dd-MM-yyyy", yyyymmddFormat);

                            IFormatProvider yyyymmddFormat2 = new System.Globalization.CultureInfo("en-GB", false);
                            double d2 = double.Parse(worksheet.Cell(j, 22).Value);
                            DateTime conv2 = DateTime.FromOADate(d2);
                            string CurrentRegistrationDate = conv2.ToString("dd-MM-yyyy", yyyymmddFormat2);

                            dtExcelResult.Rows.Add(worksheet.Cell(j, 1).Value, worksheet.Cell(j, 2).Value, worksheet.Cell(j, 3).Value, worksheet.Cell(j, 4).Value, worksheet.Cell(j, 5).Value, worksheet.Cell(j, 6).Value, worksheet.Cell(j, 7).Value, worksheet.Cell(j, 8).Value, worksheet.Cell(j, 9).Value, worksheet.Cell(j, 10).Value, worksheet.Cell(j, 11).Value, worksheet.Cell(j, 12).Value, worksheet.Cell(j, 13).Value, worksheet.Cell(j, 14).Value, worksheet.Cell(j, 15).Value, worksheet.Cell(j, 16).Value, worksheet.Cell(j, 17).Value, worksheet.Cell(j, 18).Value, worksheet.Cell(j, 19).Value, worksheet.Cell(j, 20).Value, worksheet.Cell(j, 21).Value, CurrentRegistrationDate, worksheet.Cell(j, 23).Value, worksheet.Cell(j, 24).Value, worksheet.Cell(j, 25).Value, worksheet.Cell(j, 26).Value, worksheet.Cell(j, 27).Value, worksheet.Cell(j, 28).Value, worksheet.Cell(j, 29).Value, worksheet.Cell(j, 30).Value, worksheet.Cell(j, 31).Value, worksheet.Cell(j, 32).Value, worksheet.Cell(j, 33).Value, worksheet.Cell(j, 34).Value, Birthdate, worksheet.Cell(j, 36).Value, worksheet.Cell(j, 37).Value, worksheet.Cell(j, 38).Value, worksheet.Cell(j, 39).Value, worksheet.Cell(j, 40).Value, worksheet.Cell(j, 41).Value, worksheet.Cell(j, 42).Value, worksheet.Cell(j, 43).Value, worksheet.Cell(j, 44).Value, worksheet.Cell(j, 45).Value, worksheet.Cell(j, 46).Value, worksheet.Cell(j, 47).Value, worksheet.Cell(j, 48).Value, worksheet.Cell(j, 49).Value, worksheet.Cell(j, 50).Value, worksheet.Cell(j, 51).Value, worksheet.Cell(j, 52).Value, worksheet.Cell(j, 53).Value, worksheet.Cell(j, 54).Value, worksheet.Cell(j, 55).Value, worksheet.Cell(j, 56).Value, worksheet.Cell(j, 57).Value, worksheet.Cell(j, 58).Value, worksheet.Cell(j, 59).Value, worksheet.Cell(j, 60).Value, worksheet.Cell(j, 61).Value, worksheet.Cell(j, 62).Value, worksheet.Cell(j, 63).Value, worksheet.Cell(j, 64).Value, worksheet.Cell(j, 65).Value, worksheet.Cell(j, 66).Value, worksheet.Cell(j, 67).Value, worksheet.Cell(j, 68).Value, worksheet.Cell(j, 69).Value, worksheet.Cell(j, 70).Value, worksheet.Cell(j, 71).Value, worksheet.Cell(j, 72).Value, worksheet.Cell(j, 73).Value, worksheet.Cell(j, 74).Value, worksheet.Cell(j, 75).Value, worksheet.Cell(j, 76).Value, worksheet.Cell(j, 77).Value, worksheet.Cell(j, 78).Value, worksheet.Cell(j, 79).Value, worksheet.Cell(j, 80).Value, worksheet.Cell(j, 81).Value, worksheet.Cell(j, 82).Value, worksheet.Cell(j, 83).Value, worksheet.Cell(j, 84).Value, worksheet.Cell(j, 85).Value, worksheet.Cell(j, 86).Value, worksheet.Cell(j, 87).Value, worksheet.Cell(j, 88).Value, worksheet.Cell(j, 89).Value, worksheet.Cell(j, 90).Value, worksheet.Cell(j, 91).Value, worksheet.Cell(j, 92).Value, worksheet.Cell(j, 93).Value, worksheet.Cell(j, 94).Value, worksheet.Cell(j, 95).Value, worksheet.Cell(j, 96).Value, worksheet.Cell(j, 97).Value, worksheet.Cell(j, 98).Value, worksheet.Cell(j, 99).Value, worksheet.Cell(j, 100).Value, worksheet.Cell(j, 101).Value, worksheet.Cell(j, 102).Value, worksheet.Cell(j, 103).Value, worksheet.Cell(j, 104).Value, worksheet.Cell(j, 105).Value, worksheet.Cell(j, 106).Value, worksheet.Cell(j, 107).Value, worksheet.Cell(j, 108).Value, worksheet.Cell(j, 109).Value, worksheet.Cell(j, 110).Value, worksheet.Cell(j, 111).Value, worksheet.Cell(j, 112).Value, worksheet.Cell(j, 113).Value, worksheet.Cell(j, 114).Value);
                        }
                    }
                    ViewState["Read"] = 1;
                    divGrid.Visible = true;
                    gvExcelFile.Visible = true;
                    btnSave.Visible = true;
                    gvExcelFile.DataSource = dtExcelResult;
                    gvExcelFile.DataBind();
                }
                Exit:;
            }
            catch (Exception ex)
            {

            }
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
                    Extension = Path.GetExtension(FileUpload1.FileName);
                    if (Extension == ".xls" || Extension == ".xlsx")
                    {
                        if (System.IO.File.Exists(Server.MapPath("../_ImportData/" + ddlSchoolName.SelectedItem.Text.Replace(" ", "").ToString() + Extension)))
                        {
                            System.IO.File.Delete(Server.MapPath("../_ImportData/" + ddlSchoolName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                        }
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("../_ImportData/" + ddlSchoolName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                    }
                    else
                    {
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "File format not supported.";
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
                        // lblMsg.Visible = false;


                        // btnSave.Visible = true;
                        //  gvData.visible = true;
                        imgUploder.Visible = false;
                    }
                }
                else
                {
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Select Excel File.";
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

        #region btnSave Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StudentBO objStudentBO = new StudentBO();
                ApplicationResult objResults = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();
                StudentTBO objStudentTBO = new StudentTBO();
                //if ((ddlSchoolName.SelectedItem.Text == "" || ddlSchoolName.SelectedItem.Text == "--Select--")
                //    || (ddlSection.SelectedItem.Text == "" || ddlSection.SelectedItem.Text == "--Select--")
                //    || (ddlClassName.SelectedItem.Text == "" || ddlClassName.SelectedItem.Text == "--Select--")
                //    || (ddlDivisionName.SelectedItem.Text == "" || ddlDivisionName.SelectedItem.Text == "--Select--")
                //    || (ddlYear.SelectedItem.Text == "" || ddlYear.SelectedItem.Text == "--Select--")
                //    || (ddlStatus.SelectedItem.Text == "" || ddlStatus.SelectedItem.Text == "--Select--"))
                //{
                DatabaseTransaction.OpenConnectionTransation();
                for (int i = 0; i < gvExcelFile.Rows.Count; i++)
                {
                    objStudentBO.SchoolMID = Convert.ToInt32(ddlSchoolName.SelectedValue);
                    objStudentBO.StudentFirstNameEng = gvExcelFile.Rows[i].Cells[0].Text.Replace("&nbsp;", "");
                    objStudentBO.StudentFirstNameGuj = gvExcelFile.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                    objStudentBO.StudentMiddleNameEng = gvExcelFile.Rows[i].Cells[1].Text.Replace("&nbsp;", "");
                    objStudentBO.StudentMiddleNameGuj = gvExcelFile.Rows[i].Cells[4].Text.Replace("&nbsp;", "");
                    objStudentBO.StudentLastNameEng = gvExcelFile.Rows[i].Cells[2].Text.Replace("&nbsp;", "");
                    objStudentBO.StudentLastNameGuj = gvExcelFile.Rows[i].Cells[5].Text.Replace("&nbsp;", "");

                    objStudentBO.FatherFirstNameEng = gvExcelFile.Rows[i].Cells[6].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherFirstNameGuj = gvExcelFile.Rows[i].Cells[9].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherMiddleNameEng = gvExcelFile.Rows[i].Cells[7].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherMiddleNameGuj = gvExcelFile.Rows[i].Cells[10].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherLastNameEng = gvExcelFile.Rows[i].Cells[8].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherLastNameGuj = gvExcelFile.Rows[i].Cells[11].Text.Replace("&nbsp;", "");

                    objStudentBO.MotherFirstNameEng = gvExcelFile.Rows[i].Cells[12].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherFirstNameGuj = gvExcelFile.Rows[i].Cells[15].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherMiddleNameEng = gvExcelFile.Rows[i].Cells[13].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherMiddleNameGuj = gvExcelFile.Rows[i].Cells[16].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherLastNameEng = gvExcelFile.Rows[i].Cells[14].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherLastNameGuj = gvExcelFile.Rows[i].Cells[17].Text.Replace("&nbsp;", "");

                    objStudentBO.AdmissionNo = gvExcelFile.Rows[i].Cells[18].Text.Replace("&nbsp;", "");
                    objStudentBO.CurrentDate = gvExcelFile.Rows[i].Cells[19].Text.Replace("&nbsp;", "");
                    objStudentBO.CurrentYear = ddlYear.SelectedItem.ToString();
                    objStudentBO.CurrentSectionID = Convert.ToInt32(ddlSection.SelectedValue);

                    objStudentBO.CurrentClassID = Convert.ToInt32(ddlClassName.SelectedValue);
                    // objStudentBO.CurrentClassID = Convert.ToInt32(ddlClass.SelectedValue);
                    objStudentBO.CurrentDivisionTID = Convert.ToInt32(ddlDivisionName.SelectedValue);

                    objStudentBO.CurrentGrNo = gvExcelFile.Rows[i].Cells[20].Text.Replace("&nbsp;", "");

                    objStudentBO.AdmittedClassID = -1;

                    objStudentBO.AdmittedDivisionTID = -1;

                    objStudentBO.AdmittedYear = "-1";

                    objStudentBO.StudentPhoto = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");


                    objStudentBO.GenderEng = gvExcelFile.Rows[i].Cells[22].Text;
                    objStudentBO.GenderGuj = gvExcelFile.Rows[i].Cells[21].Text;
                    //txtDateOfBirth.ReadOnly = false;
                    objStudentBO.DateOfBirth = gvExcelFile.Rows[i].Cells[23].Text.Replace("&nbsp;", "");
                    //txtDateOfBirth.ReadOnly = true;
                    objStudentBO.BirthDistrictEng = gvExcelFile.Rows[i].Cells[24].Text.Replace("&nbsp;", "");
                    objStudentBO.BirthDistrictGuj = gvExcelFile.Rows[i].Cells[25].Text.Replace("&nbsp;", "");
                    objStudentBO.NationalityEng = gvExcelFile.Rows[i].Cells[26].Text.Replace("&nbsp;", "");
                    objStudentBO.NationalityGuj = gvExcelFile.Rows[i].Cells[27].Text.Replace("&nbsp;", "");
                    objStudentBO.ReligionEng = gvExcelFile.Rows[i].Cells[28].Text.Replace("&nbsp;", "");
                    objStudentBO.CasteEng = gvExcelFile.Rows[i].Cells[29].Text.Replace("&nbsp;", "");
                    objStudentBO.CasteGuj = gvExcelFile.Rows[i].Cells[30].Text.Replace("&nbsp;", "");
                    objStudentBO.SubCasteEng = gvExcelFile.Rows[i].Cells[31].Text.Replace("&nbsp;", "");
                    objStudentBO.SubCasteGuj = gvExcelFile.Rows[i].Cells[32].Text.Replace("&nbsp;", "");
                    objStudentBO.CategoryEng = gvExcelFile.Rows[i].Cells[33].Text.Replace("&nbsp;", "");
                    objStudentBO.CategoryGuj = gvExcelFile.Rows[i].Cells[34].Text.Replace("&nbsp;", "");
                    objStudentBO.SubCategory = "";
                    objStudentBO.HandicapPrecent = gvExcelFile.Rows[i].Cells[35].Text.Replace("&nbsp;", "");
                    objStudentBO.OtherDefect = gvExcelFile.Rows[i].Cells[36].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentAddressEng = gvExcelFile.Rows[i].Cells[37].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentAddressGuj = gvExcelFile.Rows[i].Cells[38].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentCityEng = gvExcelFile.Rows[i].Cells[39].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentCityGuj = gvExcelFile.Rows[i].Cells[40].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentStateEng = gvExcelFile.Rows[i].Cells[41].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentStateGuj = gvExcelFile.Rows[i].Cells[42].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentPinCode = gvExcelFile.Rows[i].Cells[43].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentContactNo = gvExcelFile.Rows[i].Cells[44].Text.Replace("&nbsp;", "");
                    objStudentBO.PresentEmailId = "";
                    objStudentBO.PermanentAddressEng = gvExcelFile.Rows[i].Cells[45].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentAddressGuj = gvExcelFile.Rows[i].Cells[46].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentCityEng = gvExcelFile.Rows[i].Cells[47].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentCityGuj = gvExcelFile.Rows[i].Cells[48].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentStateEng = gvExcelFile.Rows[i].Cells[49].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentStateGuj = gvExcelFile.Rows[i].Cells[50].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentPinCode = gvExcelFile.Rows[i].Cells[51].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentContactNo = gvExcelFile.Rows[i].Cells[52].Text.Replace("&nbsp;", "");
                    objStudentBO.PermanentEmailId = "";
                    objStudentBO.FatherOccupation = gvExcelFile.Rows[i].Cells[53].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherOccupation = gvExcelFile.Rows[i].Cells[54].Text.Replace("&nbsp;", "");
                    objStudentBO.GardianOccupation = gvExcelFile.Rows[i].Cells[55].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherQualification = gvExcelFile.Rows[i].Cells[56].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherQualification = gvExcelFile.Rows[i].Cells[57].Text.Replace("&nbsp;", "");
                    objStudentBO.GardianQualification = gvExcelFile.Rows[i].Cells[58].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherMobileNo = gvExcelFile.Rows[i].Cells[59].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherMobileNo = gvExcelFile.Rows[i].Cells[60].Text.Replace("&nbsp;", "");
                    objStudentBO.GardianMobileNo = gvExcelFile.Rows[i].Cells[61].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherEmailID = gvExcelFile.Rows[i].Cells[62].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherEmailID = gvExcelFile.Rows[i].Cells[63].Text.Replace("&nbsp;", "");
                    objStudentBO.GardianEmailID = gvExcelFile.Rows[i].Cells[64].Text.Replace("&nbsp;", "");
                    objStudentBO.Height = gvExcelFile.Rows[i].Cells[65].Text.Replace("&nbsp;", "");
                    objStudentBO.Weight = gvExcelFile.Rows[i].Cells[66].Text.Replace("&nbsp;", "");
                    objStudentBO.Hobbies = gvExcelFile.Rows[i].Cells[67].Text.Replace("&nbsp;", "");
                    objStudentBO.StatusMasterID = Convert.ToInt32(ddlStatus.SelectedValue);   //68
                    objStudentBO.LeftDate = "";  //69
                    objStudentBO.LeftReason = "";  //70
                    objStudentBO.LeftYear = "-Select-";  //71
                    objStudentBO.LeftStd = "";   //72
                    objStudentBO.LcNo = "";   //73
                    objStudentBO.LcDate = "";  //74
                    objStudentBO.LcRemarks = "";  //75
                    objStudentBO.LcCopy = "";  //76
                    objStudentBO.RegisteredYear = "-Select-";  //77
                    objStudentBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objStudentBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objStudentBO.AdmissionDate = "";
                    //objStudentBO.UNIQUENO = gvExcelFile.Rows[i].Cells[68].Text.Replace("&nbsp;", "");
                    //objStudentBO.UNIQUEId = gvExcelFile.Rows[i].Cells[69].Text.Replace("&nbsp;", "");
                    objStudentBO.GvUniqueNo = gvExcelFile.Rows[i].Cells[68].Text.Replace("&nbsp;", "");
                    objStudentBO.GVUniqueID = gvExcelFile.Rows[i].Cells[69].Text.Replace("&nbsp;", "");
                    objStudentBO.MotherTongue = gvExcelFile.Rows[i].Cells[71].Text.Replace("&nbsp;", "");
                    objStudentBO.PreviousSchoolDetails = gvExcelFile.Rows[i].Cells[72].Text.Replace("&nbsp;", "");
                    objStudentBO.PhysicalIdentification = gvExcelFile.Rows[i].Cells[73].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherOrganisationName = gvExcelFile.Rows[i].Cells[74].Text.Replace("&nbsp;", "");
                    objStudentBO.FatherOrganisationContactNumber = gvExcelFile.Rows[i].Cells[75].Text.Replace("&nbsp;", "");
                    objStudentBO.BloodGroup = gvExcelFile.Rows[i].Cells[76].Text.Replace("&nbsp;", "");
                    objStudentBO.BankAccount = gvExcelFile.Rows[i].Cells[77].Text.Replace("&nbsp;", "");
                    objStudentBO.IFSCCode = gvExcelFile.Rows[i].Cells[78].Text.Replace("&nbsp;", "");
                    objStudentBO.BranchName = gvExcelFile.Rows[i].Cells[79].Text.Replace("&nbsp;", "");
                    objStudentBO.AccountNumber = gvExcelFile.Rows[i].Cells[80].Text.Replace("&nbsp;", "");
                    objStudentBO.TypeOfVehicle = gvExcelFile.Rows[i].Cells[81].Text.Replace("&nbsp;", "");
                    objStudentBO.VehicleNo = gvExcelFile.Rows[i].Cells[82].Text.Replace("&nbsp;", "");
                    objStudentBO.DriverName = gvExcelFile.Rows[i].Cells[83].Text.Replace("&nbsp;", "");
                    objStudentBO.DriverContactNo = gvExcelFile.Rows[i].Cells[84].Text.Replace("&nbsp;", "");
                    objStudentBO.AadharCardNo = gvExcelFile.Rows[i].Cells[85].Text.Replace("&nbsp;", "");
                    objStudentBO.RollNumber = gvExcelFile.Rows[i].Cells[86].Text.Replace("&nbsp;", "");


                    #region RollBack Transaction Starts


                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objResults = objStudentBL.Student_Insert(objStudentBO);
                        if (objResults != null)
                        {
                            if (objResults.resultDT.Rows.Count > 0)
                            {
                                ViewState["StudentMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                            }
                            if (Convert.ToInt32(ViewState["StudentMID"]) != 0)
                            {
                                objStudentTBO.StudentMID = Convert.ToInt32(ViewState["StudentMID"].ToString());
                                objStudentTBO.ClassMID = Convert.ToInt32(ddlClassName.SelectedValue);
                                objStudentTBO.Year = ddlYear.SelectedItem.ToString();
                                objStudentTBO.DivisionTID = Convert.ToInt32(ddlDivisionName.SelectedValue);
                                objStudentTBO.StatusMasterID = Convert.ToInt32(ddlStatus.SelectedValue);
                                objStudentTBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objStudentTBO.StatusName = ddlStatus.SelectedItem.ToString();
                                objStudentTBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objStudentTBO.GrNo = objStudentBO.CurrentGrNo;

                                objResults = objStudentBL.StudentT_Insert(objStudentTBO);
                            }

                            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {

                                // Response.Redirect("StudentDataImport.aspx");

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Student GRNo Already Exist.');", true);
                                ClearAll();
                            }

                        }
                    }



                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Record Saved Successfully.');", true);
                string Extension = string.Empty;
                Extension = Path.GetExtension(FileUpload1.FileName);
                System.IO.File.Delete(Server.MapPath("../_ImportData/" + ddlSchoolName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                #endregion
                ClearAll();
                DatabaseTransaction.CommitTransation();



                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select Every Dropdown.');", true);
                //}

            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                string Extension = string.Empty;
                Extension = Path.GetExtension(FileUpload1.FileName);
                System.IO.File.Delete(Server.MapPath("../_ImportData/" + ddlSchoolName.SelectedItem.Text.Replace(" ", "").ToString() + Extension));
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops!Contact to your Administrator.');", true);
            }

        }
        #endregion

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
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divGrid.Visible = false;
        }
        #endregion
    }
}