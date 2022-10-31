using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Client.UI;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Result
{
    public partial class StudentExamMarks : System.Web.UI.Page
    {
        #region Declaration
        private static ILog logger = LogManager.GetLogger(typeof(StudentDetailMaster));
        //public static readonly string mstrConnString = System.Configuration.ConfigurationSettings.AppSettings["DBConnString"];
        public static readonly string mstrConnString = Encryption.Decrypt_Static(System.Configuration.ConfigurationSettings.AppSettings["DBConnString"]);

        #endregion



        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClass();
                BindAcademicYear();
                btnsave.Visible = false;
            }
        }
        #endregion

        #region Bind Class
        /// <summary>
        /// To bind classes to dropdownlist
        /// </summary>
        protected void BindClass()
        {
            ApplicationResult objResult = new ApplicationResult();
            ClassBL objClassBl = new ClassBL();
            Controls objControl = new Controls();
            objResult = objClassBl.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlClass, "ClassName", "ClassMID");
            }
            ddlClass.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Division
        protected void ddlClass_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            DivisionTBL objDivisionTbl = new DivisionTBL();

            objResult = objDivisionTbl.DivisionT_SelectAll_For_Exam(Convert.ToInt32(ddlClass.SelectedValue));
            if (objResult != null)
            {
                //objControls.BindDropDown_ListBox(objResult.resultDT, ddlDesignation, "DesignationNameENG", "DesignationID");
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlDivision, "DivisionName", "DivisionTID");
                if (objResult.resultDT.Rows.Count > 0)
                {

                }

                //Added below code on 31/10/2022 Bhandavi
                //to clear total marks and passing marks text boxes, to hide save button and marks griview, to select first option in academic year and subject drop down
                //when class changed
                ddlAcademicYear.SelectedIndex = 0;
                ddlExam.SelectedIndex = 0;
                if(ddlSubject.Items.Count > 0)
                    ddlSubject.SelectedIndex = 0;
                txtTotalMarks.Text = "";
                txtPassingMarks.Text = "";
                gvStudentMarks.DataSource = null;
                gvStudentMarks.Visible = false;
                btnsave.Visible = false;
                //ddlDesignation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select All--", "-1"));
            }
        }
        #endregion

        #region Bind Academic Year
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

                objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");

            }
            catch (Exception ex)
            {
                //logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

            }
        }
        #endregion

        #region Bind Subject
        public void BindSubject()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            ExamConfigurationBL objExamConfigurationBL = new ExamConfigurationBL();

            objResult = objExamConfigurationBL.ExamConfiguration_Select_Subject(
                Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue),
                ddlAcademicYear.SelectedItem.ToString(),ddlExam.SelectedItem.ToString());

            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSubject, "NameEng", "SubjectMID");
                    ddlSubject.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    //Added below code on 31/10/2022 Bhandavi
                    //When subjects are not availabe then clear sbuject drop down
                    ddlSubject.Items.Clear();
                    ddlSubject.Items.Insert(0, new ListItem("--Select--", ""));
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No Subjects To be Bound.');</script>");
                }
            }
        }

        #endregion

        #region BindGrid of Student
        protected void BindStudentGrid()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                StudentBL objStudentBL = new StudentBL();
                ViewState["ClassMID"] = Convert.ToInt32(ddlClass.SelectedValue);
                ViewState["DivisionID"] = Convert.ToInt32(ddlDivision.SelectedValue);
                ViewState["AcademicYear"] = ddlAcademicYear.SelectedItem.ToString();
                objResult = objStudentBL.Student_Select_ClassDivisionWise_ForExam(Convert.ToInt32(ViewState["ClassMID"].ToString()), Convert.ToInt32(ViewState["DivisionID"].ToString()), ddlAcademicYear.SelectedItem.ToString(), ddlExam.SelectedItem.ToString(),Convert.ToInt32(ddlSubject.SelectedValue),Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    btnsave.Visible = true;
                    if (objResult.resutlDS.Tables[1].Rows.Count > 0)
                    {
                        hfExamConfigId.Value = objResult.resutlDS.Tables[0].Rows[0][0].ToString();
                        gvStudentMarks.Visible = true;

                        if (objResult.resutlDS.Tables[1].Columns.Contains("ObtainedMarks"))
                        {
                            DataTable dt = objResult.resutlDS.Tables[1];
                            gvStudentMarks.DataSource = objResult.resutlDS.Tables[1];
                            gvStudentMarks.DataBind();
                            foreach (GridViewRow gvr in gvStudentMarks.Rows)
                            {
                                TextBox type = ((TextBox)gvr.FindControl("txtTotal"));
                                //type.ReadOnly = true;
                                type.Attributes.Add("readonly", "readonly");
                                TextBox type1 = ((TextBox)gvr.FindControl("txtPassing"));
                               // type1.ReadOnly = true;
                                type1.Attributes.Add("readonly", "readonly");

                            }
                            txtTotalMarks.Text = dt.Rows[0]["TotalMarks"].ToString();
                            txtPassingMarks.Text = dt.Rows[0]["PassingMarks"].ToString();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string s = gvStudentMarks.Rows[i].Cells[3].ToString();
                                TextBox box1 = (TextBox)gvStudentMarks.Rows[i].Cells[1].FindControl("txtTotal");
                                TextBox box2 = (TextBox)gvStudentMarks.Rows[i].Cells[2].FindControl("txtPassing");
                                TextBox box3 = (TextBox)gvStudentMarks.Rows[i].Cells[3].FindControl("txtObtained");

                                box1.Text = dt.Rows[i]["TotalMarks"].ToString();
                                box2.Text = dt.Rows[i]["PassingMarks"].ToString();
                                box3.Text = dt.Rows[i]["ObtainedMarks"].ToString();

                            }
                            ViewState["Mode"] = "Edit";
                        }
                        else
                        {
                            gvStudentMarks.DataSource = objResult.resutlDS.Tables[1];
                            gvStudentMarks.DataBind();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FillMarks();", true);
                            foreach (GridViewRow gvr in gvStudentMarks.Rows)
                            {
                                TextBox type = ((TextBox)gvr.FindControl("txtTotal"));
                                //type.ReadOnly = true;
                                type.Attributes.Add("readonly", "readonly");
                                TextBox type1 = ((TextBox)gvr.FindControl("txtPassing"));
                                //type1.ReadOnly = true;
                                type1.Attributes.Add("readonly", "readonly");
                            }
                            ViewState["Mode"] = "Save";
                        }
                        
                        //TextBox txtObtainedMarks1 = (TextBox)gvStudentMarks.HeaderRow.FindControl("Obtained Marks");
                    }
                    else
                    {
                        gvStudentMarks.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No record Found.');</script>");
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



        #region Save Button Click Event
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FillObtainedMarks();", true);
                StudentMarksBO objStudentMarksBO = new StudentMarksBO();
                StudentMarksBL objStudentMarksBL = new StudentMarksBL();
                ApplicationResult objResult = new ApplicationResult();
                int ObtainedMarks, TotalMarks, PassingMarks;
                DatabaseTransaction.OpenConnectionTransation();

                foreach (GridViewRow row in gvStudentMarks.Rows)
                {
                    objStudentMarksBO.ExamConfigId = Convert.ToInt32(hfExamConfigId.Value);
                    objStudentMarksBO.SubjectId = Convert.ToInt32(ddlSubject.SelectedValue);
                    objStudentMarksBO.StudentId = Convert.ToInt32(row.Cells[1].Text);
                    objStudentMarksBO.Exam = ddlExam.SelectedItem.ToString();

                    TextBox txtTotalMarks = (TextBox)row.FindControl("txtTotal");
                    //txtTotalMarks.Enabled = true;
                    TotalMarks = Convert.ToInt32(txtTotalMarks.Text);
                    objStudentMarksBO.TotalMarks = TotalMarks;

                    TextBox txtObtainedMarks = (TextBox)row.FindControl("txtObtained");
                    //txtObtainedMarks.Enabled = true;
                    ObtainedMarks = Convert.ToInt32(txtObtainedMarks.Text);
                    objStudentMarksBO.ObtainedMarks = ObtainedMarks;

                    TextBox txtPassingMarks = (TextBox)row.FindControl("txtPassing");
                    PassingMarks = Convert.ToInt32(txtPassingMarks.Text);
                    objStudentMarksBO.PassingMarks = PassingMarks;


                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objStudentMarksBO.CreatedById = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objStudentMarksBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                        objResult = objStudentMarksBL.StudentMarks_Insert(objStudentMarksBO);
                    }
                    else
                    {
                        objStudentMarksBO.LastModifiedById = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objStudentMarksBO.LastModifiedByDate = DateTime.UtcNow.AddHours(5.5).ToString();

                        objResult = objStudentMarksBL.StudentMarks_Update(objStudentMarksBO);
                    }
                }

                if (objResult != null)
                {
                    if (objResult.status.ToString() == "SUCCESS")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script language='javascript'>alert('Data Saved Successfully.');</script>");
                        DatabaseTransaction.CommitTransation();
                        Controls objControl = new Controls();
                        objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                        gvStudentMarks.DataSource = null;
                        gvStudentMarks.Visible = false;
                        btnsave.Visible = false;
                        
                        //Added below code on 31/10/2022 Bhandavi
                        //to clear fields after saving 
                        //Bug No 149
                       
                        ddlDivision.Items.Clear();
                        ddlSubject.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion
        
        #region View Button Click Event
        protected void btnView_Click(object sender, EventArgs e)
        {
            gvStudentMarks.DataSource = null;
            BindStudentGrid();
        }
        #endregion



        #region  Exam DropDownliast Selected Index Changed Event
        protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubject();
            //Added below code on 31/10/2022 Bhandavi
            //to clear total marks and passing marks text boxes when exam changed

            txtTotalMarks.Text = "";
            txtPassingMarks.Text = "";
        }

        #endregion

        #region gvClass OnRowDataBound Event
        protected void gvClass_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int intClassMID = Convert.ToInt32(e.Row.Cells[0].Text);
                GridView gvChild = (GridView)e.Row.FindControl("gvChild");

                DivisionTBL objDivisionTbl = new DivisionTBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objDivisionTbl.Division_SelectAll_ClassWise(intClassMID,
                    Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvChild.DataSource = objResult.resultDT;
                    gvChild.DataBind();
                }
            }
        }
        #endregion

        #region Gridview gvStudenMarks RowDataBound Method
        protected void gvStudentMarks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    if (gvRow.Cells[1].Text == "Student Name")
            //    {
            //        //gvRow.Cells.Remove(gvRow.Cells[0]);
            //        GridViewRow gvHeader = new GridViewRow(1, 1, DataControlRowType.Header,
            //            DataControlRowState.Insert);
            //        TableCell headerCell0 = new TableCell()
            //        {
            //            Text = "Total",
            //            HorizontalAlign = HorizontalAlign.Center,
            //        };
            //        TableCell headerCell1 = new TableCell()
            //        {
            //            Text = "Passing",
            //            HorizontalAlign = HorizontalAlign.Center,
            //        };
            //        gvHeader.Cells.Add(headerCell0);
            //        gvHeader.Cells.Add(headerCell1);
            //        gvStudentMarks.Controls[0].Controls.AddAt(1, gvHeader);
            //    }
            //}

        }
        #endregion
    }
}