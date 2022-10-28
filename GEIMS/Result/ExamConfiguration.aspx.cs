using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Microsoft.Ajax.Utilities;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Client.UI;
using GEIMS.Common;

namespace GEIMS.Result
{
    public partial class Result : System.Web.UI.Page
    {
        #region Declaration
        private static ILog logger = LogManager.GetLogger(typeof(StudentDetailMaster));
        //public static readonly string mstrConnString = System.Configuration.ConfigurationSettings.AppSettings["DBConnString"];
        public static readonly string mstrConnString = Encryption.Decrypt_Static(System.Configuration.ConfigurationSettings.AppSettings["DBConnString"]);

        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();
        public int DivisionTID;
        private string SubjectIDs = null;
        private DataTable dtExam = new DataTable();
        #endregion



        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!Page.IsPostBack)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindClass();", true);
                    ViewState["Mode"] = "Save";
                    BindAcademicYear();
                    //BindSubject1();
                    BindClass();
                    BindExamConfig();
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
                ApplicationResult objResult = new ApplicationResult();
                ClassBL objClassBl = new ClassBL();
                Controls objControl = new Controls();
                objResult = objClassBl.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    objControl.BindDropDown_ListBox(objResult.resultDT, ddlClass, "ClassName", "ClassMID");
                    PanelVisibility(1);
                    ddlClass.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    PanelVisibility(2);
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }

        #endregion

        #region Bind Subject(Not Used)
        public void BindSubject1()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SubjectMBL objSubjectMbl = new SubjectMBL();
                Controls objControl = new Controls();
                objResult = objSubjectMbl.SubjectM_Select_By_School(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    objControl.BindDropDown_ListBox(objResult.resultDT, lbSrcSubject, "NameEng", "SubjectMID");
                    PanelVisibility(1);

                }
                else
                {
                    PanelVisibility(2);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
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

        #region Bind Division
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DivisionTBL objDivisionTbl = new DivisionTBL();

                objResult = objDivisionTbl.DivisionT_SelectAll_For_Exam(Convert.ToInt32(ddlClass.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, lbDivision, "DivisionName", "DivisionTID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        DivisionTID = Convert.ToInt32(objResult.resultDT.Rows[0]["DivisionTID"].ToString());
                        ViewState["DivisionTID"] = DivisionTID;

                    }
                }
                ApplicationResult objResult1 = new ApplicationResult();
                SubjectMBL objSubjectMbl = new SubjectMBL();
                Controls objControl = new Controls();
                objResult1 = objSubjectMbl.SubjectM_Select_By_ClassDivision(Convert.ToInt32(ddlClass.SelectedValue), DivisionTID);
                if (objResult1 != null)
                {
                    objControl.BindDropDown_ListBox(objResult1.resultDT, lbSrcSubject, "NameEng", "SubjectMID");
                    //PanelVisibility(1);

                }
                else
                {
                    //PanelVisibility(2);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ExamConfigurationBO objExamConfigurationBO = new ExamConfigurationBO();
                ExamConfigurationBL objExamConfigurationBL = new ExamConfigurationBL();
                int intExamConfigID = 0;
                objExamConfigurationBO.ClassId = Convert.ToInt32(ddlClass.SelectedValue);
                objExamConfigurationBO.DivisionId = Convert.ToInt32(ViewState["DivisionTID"]);
                objExamConfigurationBO.AcademicYear = ddlAcademicYear.SelectedItem.ToString();
                objExamConfigurationBO.Exam = ddlExam.SelectedItem.ToString();
                objExamConfigurationBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objExamConfigurationBO.IsDeleted = 0;
                foreach (ListItem i in lbDestSubject.Items)
                {
                    SubjectIDs += i.Value + ",";
                }
                SubjectIDs = SubjectIDs.Substring(0, SubjectIDs.Length - 1);
                objExamConfigurationBO.SubjectId = SubjectIDs;
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intExamConfigID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intExamConfigID = Convert.ToInt32(ViewState["ExamConfigID"]);
                }
                objResult =
                    objExamConfigurationBL.ExamConfigurationBL_ValidateName(Convert.ToInt32(ddlClass.SelectedValue),
                        Convert.ToInt32(ViewState["DivisionTID"]), ddlExam.SelectedItem.ToString(),
                        ddlAcademicYear.SelectedItem.ToString(), intExamConfigID, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Exam already Saved.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {

                            objExamConfigurationBO.CreatedByUserId = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objExamConfigurationBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objExamConfigurationBL.ExamConfigurationBL_Insert(objExamConfigurationBO);
                            if (objResult != null)
                            {
                                Controls objControls = new Controls();
                                objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script language='javascript'>alert('Exam Details Saved Successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objExamConfigurationBO.ExamConfigId = Convert.ToInt32(ViewState["ExamConfigID"]);
                            //objExamConfigurationBO.ClassId = Convert.ToInt32(ddlClass.SelectedValue);
                            //objExamConfigurationBO.DivisionId = Convert.ToInt32(ViewState["DivisionTID"]);
                            //objExamConfigurationBO.AcademicYear = ddlAcademicYear.SelectedItem.ToString();
                            //objExamConfigurationBO.Exam = ddlExam.SelectedItem.ToString();
                            //objExamConfigurationBO.IsDeleted = 0;
                            //foreach (ListItem i in lbDestSubject.Items)
                            //{
                            //    SubjectIDs += i.Value + ",";
                            //}
                            //SubjectIDs = SubjectIDs.Substring(0, SubjectIDs.Length - 1);
                            //objExamConfigurationBO.SubjectId = SubjectIDs;
                            objExamConfigurationBO.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objExamConfigurationBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objExamConfigurationBL.ExamConfigurationBL_Update(objExamConfigurationBO);

                            //Changed code on 28/10/2022 Bhandavi
                            //getting oops error as objResult.resultDT.Rows[0][0].ToString() doesnot have any value
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)                               
                            {
                                Controls objControls = new Controls();
                                objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script language='javascript'>alert('Exam Details Updated Successfully.');</script>");
                            }
                            else if (objResult.status == ApplicationResult.CommonStatusType.FAILURE)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                      "<script language='javascript'>alert('Exam Details Can not be Updated.');</script>");
                            }
                            //    if (objResult.resultDT.Rows[0][0].ToString() == "333")
                            //{
                            //    Controls objControls = new Controls();
                            //    objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            //        "<script language='javascript'>alert('Exam Details Updated Successfully.');</script>");
                            //}
                            //        else if (objResult.resultDT.Rows[0][0].ToString() == "222")
                            //{
                            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            //        "<script language='javascript'>alert('Exam Details Can not be Updated.');</script>");
                            //}
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

                            }
                        }
                        ClearAll();
                        BindExamConfig();
                        PanelVisibility(1);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add Button Click Event
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string s = null;
            if (lbSrcSubject.SelectedIndex >= 0)
            {
                ListItemCollection li = new ListItemCollection();
                foreach (ListItem i in lbSrcSubject.Items)
                {
                    if (i.Selected)
                    {
                        lbDestSubject.Items.Add(i);
                        //lbSrcSubject.Items.Remove(i);

                        li.Add(i);
                    }
                    //lbSrcSubject.Items.Remove(lbSrcSubject.Items[i]);
                }
                foreach (ListItem VARIABLE in li)
                {
                    lbSrcSubject.Items.Remove(VARIABLE);
                }
            }
            else
            {
            }
        }
        #endregion

        
        #region Remove Button Click Event
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //if (lbDestSubject.SelectedIndex >= 0)
            //{
            //     foreach (int i in lbDestSubject.GetSelectedIndices())
            //        {
            //            lbSrcSubject.Items.Add(lbDestSubject.Items[i]);
            //            lbDestSubject.Items.Remove(lbDestSubject.Items[i]);
            //        }
            //}
            //else
            //{
            //    lbltxt.Visible = true;
            //    lbltxt.Text = "Please select atleast one in Listbox1 to move";
            //}
            if (lbDestSubject.SelectedIndex >= 0)
            {
                for (int i = 0; i < lbDestSubject.Items.Count; i++)
                {
                    if (lbDestSubject.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(lbDestSubject.Items[i]))
                        {
                            arraylist2.Add(lbDestSubject.Items[i]);
                        }                      

                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!lbSrcSubject.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        lbSrcSubject.Items.Add(((ListItem)arraylist2[i]));
                    }
                    lbDestSubject.Items.Remove(((ListItem)arraylist2[i]));
                }
                lbSrcSubject.SelectedIndex = -1;

                //Added on 28/10/2022 Bhandavi
                //to select first item in list of subjects for saving(otherwise validation message is coming)
                if(lbDestSubject.Items.Count > 0)
                    lbDestSubject.SelectedIndex = 0;
            }
        }
        #endregion

        #region Down Button Click Event
        protected void btnDown_Click(object sender, EventArgs e)
        {
            MoveDown(lbDestSubject);
        }
        #endregion

        #region Up Button Click Event
        protected void btnUp_Click(object sender, EventArgs e)
        {
            MoveUp(lbDestSubject);
        }
        #endregion

        #region MoveDown Method for ListBox Destination Subject
        void MoveDown(ListBox myListBox)
        {
            int selectedIndex = myListBox.SelectedIndex;
            if (selectedIndex < myListBox.Items.Count - 1 & selectedIndex != -1)
            {
                myListBox.Items.Insert(selectedIndex + 2, myListBox.Items[selectedIndex]);
                myListBox.Items.RemoveAt(selectedIndex);
                myListBox.SelectedIndex = selectedIndex + 1;

            }
            else
            {

            }
        }
        #endregion

        #region MoveUp Method for ListBox Destination Subject
        void MoveUp(ListBox myListBox)
        {
            int selectedIndex = myListBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                myListBox.Items.Insert(selectedIndex - 1, myListBox.Items[selectedIndex]);
                myListBox.Items.RemoveAt(selectedIndex + 1);
                myListBox.SelectedIndex = selectedIndex - 1;
            }
            else
            {

            }
        }
        #endregion


        #region Button ViewList Click Event
        //protected void btnViewList_Click(object sender, EventArgs e)
        //{
        //    //if (ViewState["Mode"].ToString() == "Save")
        //    //{
        //    //    BindExamConfig();
        //    //    ViewState["Mode"] = "Edit";
        //    //}
        //    //else
        //    //{
        //    //    BindExamConfig();
        //    //    ViewState["Mode"] = "Save";
        //    //}

        //    try
        //    {
        //        ClearAll();
        //        PanelVisibility(1);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        #endregion


        #region Bind Exam Configuration Gridview

        public void BindExamConfig()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ExamConfigurationBL objExamConfigurationBL = new ExamConfigurationBL();

                objResult = objExamConfigurationBL.ExamConfigurationBL_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvExamConfig.DataSource = objResult.resultDT;
                    gvExamConfig.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvExamConfig.Visible = true;
                    }
                    else
                    {
                        gvExamConfig.Visible = false;
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

        #region Gridview Exam Config OnPreRender Event
        protected void gvExamConfig_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvExamConfig.Rows.Count > 0)
                {
                    gvExamConfig.UseAccessibleHeader = true;
                    gvExamConfig.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gridview ExamConfig Row Command Event
        protected void gvExamConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ExamConfigurationBL objExamConfigurationBL = new ExamConfigurationBL();

                ViewState["ExamConfigID"] = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName.ToString() == "Edit1")
                {
                    ApplicationResult objResultsEdit = new ApplicationResult();
                    objResultsEdit = objExamConfigurationBL.ExamConfigurationBL_Select(Convert.ToInt32(ViewState["ExamConfigID"].ToString()));

                    if (objResultsEdit != null)
                    {
                        if (objResultsEdit.resultDT.Rows.Count > 0)
                        {
                            btnsave.Visible = true;
                            ViewState["Mode"] = "Edit";
                            ddlClass.SelectedValue = objResultsEdit.resultDT.Rows[0][ExamConfigurationBO.EXAMCONFIGURATIONBO_CLASSID].ToString();
                            ddlClass_SelectedIndexChanged(sender, e);
                            ddlAcademicYear.Text = objResultsEdit.resultDT.Rows[0][ExamConfigurationBO.EXAMCONFIGURATIONBO_ACADEMICYEAR].ToString();
                            ddlExam.Text = objResultsEdit.resultDT.Rows[0][ExamConfigurationBO.EXAMCONFIGURATIONBO_EXAM].ToString();
                            BindSubject_ForEdit();
                            PanelVisibility(2);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot Update Exam.Marks Already Saved');</script>");
                        }
                    }
                }

                if (e.CommandName.ToString() == "Delete1")
                {
                    Controls objControls = new Controls();

                    ApplicationResult objDelete = new ApplicationResult();
                    int LastModofiedID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    string LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objDelete = objExamConfigurationBL.ExamConfigurationBL_Delete(Convert.ToInt32(e.CommandArgument.ToString()), LastModofiedID, LastModifiedDate);
                    if (objDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        PanelVisibility(1);
                        BindExamConfig();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Exam Details Deleted Successfully.');</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
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

        #region Bind Subject For Edit
        public void BindSubject_ForEdit()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                ExamConfigurationBL objExamConfigurationBL = new ExamConfigurationBL();

                objResult = objExamConfigurationBL.ExamConfiguration_Select_Subject_ForEdit(Convert.ToInt32(ViewState["ExamConfigID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, lbDestSubject, "NameEng", "SubjectMID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        //Added on 28/10/2022 Bhandavi
                        //to select first item in list of subjects for saving(otherwise validation message is coming)
                        lbDestSubject.SelectedIndex = 0;
                        
                    }
                }

                ApplicationResult objResult1 = new ApplicationResult();
                Controls objControls1 = new Controls();
                objResult1 = objExamConfigurationBL.ExamConfiguration_Select_Subject_ForEdit_SourceListBox(Convert.ToInt32(ViewState["ExamConfigID"].ToString()), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                if (objResult1 != null)
                {
                    objControls1.BindDropDown_ListBox(objResult1.resultDT, lbSrcSubject, "NameEng", "SubjectMID");
                    if (objResult1.resultDT.Rows.Count > 0)
                    {

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

        #region Add New Link Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(2);
                lbDestSubject.Items.Clear();
                lbDivision.Items.Clear();
                Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }
        #endregion

        #region ViewList LinkButton Click Event
        protected void lnkViewList_Click1(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);
                BindExamConfig();
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