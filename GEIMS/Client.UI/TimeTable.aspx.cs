using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.Client.UI
{
    public partial class TimeTable : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TimeTable));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["Mode"] = "Save";
                    BindClass();
                    PanelVisibility(1);
                    divTimeTable.Visible = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion



        #region Bind Class
        public void BindClass()
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
            ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Division
        public void BindDivision(int intClassMID)
        {
            ApplicationResult objResult = new ApplicationResult();
            DivisionTBL objDivisionTbl = new DivisionTBL();
            Controls objControl = new Controls();
            objResult = objDivisionTbl.DivisionT_Select_By_Class(intClassMID);
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlDivision, "DivisionName", "DivisionTID");
            }
            ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Days
        public void BindDays(int intClassMID, int intMode)
        {

            ApplicationResult objResult = new ApplicationResult();
            PeriodBL objPeriodBl = new PeriodBL();
            Controls objControl = new Controls();
            objResult = objPeriodBl.Period_SelectAll_Days(intClassMID, intMode);
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlDay, "DayName", "DayNo");
            }
            ddlDay.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Time Table
        public void BindTimeTable()
        {
            ApplicationResult objResult = new ApplicationResult();
            TimeTableBL objTimeTableBl = new TimeTableBL();
            objResult = objTimeTableBl.TimeTable_Select_By_Class_Division(Convert.ToInt32(ddlClass.SelectedValue),
                Convert.ToInt32(ddlDivision.SelectedValue));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        DataTable dtTemp = objResult.resultDT.AsEnumerable().Where(r => r.Field<int>("DayNo") == i)
                        .CopyToDataTable();
                        if (dtTemp.Rows.Count > 0)
                        {
                            for (int j = 1; j <= Convert.ToInt32(hfNoOfPeriod.Value); j++)
                            {
                                Label lbl1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblFrom" + j.ToString() + "" + i.ToString()) as Label;
                                lbl1.Text = dtTemp.Rows[j - 1]["FromTime"].ToString();
                                Label lbl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblTo" + j.ToString() + "" + i.ToString()) as Label;
                                lbl2.Text = dtTemp.Rows[j - 1]["ToTime"].ToString();
                                Label lbl3 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblSub" + j.ToString() + "" + i.ToString()) as Label;
                                lbl3.Text = dtTemp.Rows[j - 1]["SubjectName"].ToString();
                                Label lbl4 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblTeacher" + j.ToString() + "" + i.ToString()) as Label;
                                lbl4.Text = "(" + dtTemp.Rows[j - 1]["Teacher"].ToString() + ")";
                            }
                        }
                    }
                    divTimeTable.Visible = true;
                }
                else
                {
                    divTimeTable.Visible = false;
                }
            }
            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindTimeTable(" + Convert.ToInt32(hfNoOfPeriod.Value) + ");</script>");
        }
        #endregion

        #region Bind Subject
        public void BindSubject()
        {
            ApplicationResult objResult = new ApplicationResult();
            SubjectTBL objSubjectTbl = new SubjectTBL();
            Controls objControl = new Controls();
            int intCount = Convert.ToInt32(hfNoOfPeriod.Value);
            objResult = objSubjectTbl.SubjectT_SelectAll_By_Class_Division(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue));
            if (objResult != null)
            {
                for (int i = 1; i <= intCount; i++)
                {
                    DropDownList ddl = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlSubject" + i.ToString()) as DropDownList;
                    objControl.BindDropDown_ListBox(objResult.resultDT, ddl, "Subject", "SubjectMID");
                }
            }
            for (int j = 1; j <= intCount; j++)
            {
                DropDownList ddl1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlSubject" + j.ToString()) as DropDownList;
                DropDownList ddl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlTeacher" + j.ToString()) as DropDownList;
                ddl1.Items.Insert(0, new ListItem("None", "-1"));
                ddl2.Items.Insert(0, new ListItem("None", "-1"));
            }
        }
        #endregion



        #region Class DropDown Selected Index Change Event
        protected void ddlClass_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClass.SelectedValue != "")
                {
                    ApplicationResult objResult = new ApplicationResult();
                    ClassBL objClassBl = new ClassBL();
                    TimeTableBL objTimeTableBl = new TimeTableBL();
                    objResult = objTimeTableBl.TimeTable_Validate_Period(Convert.ToInt32(ddlClass.SelectedValue));

                    if (objResult.resultDT.Rows[0][0].ToString() == "0")
                    {
                        objResult = objClassBl.Class_Select(Convert.ToInt32(ddlClass.SelectedValue));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                hfNoOfPeriod.Value = objResult.resultDT.Rows[0][ClassBO.CLASS_NOOFPERIOD].ToString();
                            }
                        }
                        BindDivision(Convert.ToInt32(ddlClass.SelectedValue));
                    }
                    else
                    {
                        ddlDivision.Items.Clear();
                        ddlDivision.Items.Insert(0, new ListItem("--Select--", ""));
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('First you have to insert data in period.');</script>");
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

        #region Division DropDown  Selected Index Change Event
        protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDivision.SelectedValue != "")
                {
                    if (ViewState["DropDownMode"].ToString() == "Load")
                    {
                        BindTimeTable();
                    }
                    else if (ViewState["DropDownMode"].ToString() == "New")
                    {
                        BindSubject();
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

        #region Subject DropDown Selected Index Change Event
        protected void ddlSubject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl1 = (sender as DropDownList);
                string strname = ddl1.ID;
                string i = strname.Substring(strname.Length - 1, 1);
                if (i == "0")
                {
                    i = "10";
                }
                DropDownList ddl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlTeacher" + i.ToString()) as DropDownList;
                ddl2.Items.Clear();
                ApplicationResult objResult = new ApplicationResult();
                SubjectTBL objSubjectTbl = new SubjectTBL();
                Controls objControls = new Controls();
                objResult = objSubjectTbl.SubjectT_Select_Teacher(Convert.ToInt32(ddlClass.SelectedValue),
                    Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddl1.SelectedValue));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddl2, "EmployeeFNameENG", "EmployeeMID");
                    }
                }
                ddl2.Items.Insert(0, new ListItem("None", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Day DropDown Selected Index Change Event
        protected void ddlDay_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDay.SelectedValue != "")
                {
                    ApplicationResult objResult = new ApplicationResult();
                    TimeTableBL objTimeTableBl = new TimeTableBL();
                    objResult = objTimeTableBl.TimeTable_Select_By_Class_Division_Day(Convert.ToInt32(ddlClass.SelectedValue),
                             Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            for (int i = 0, j = 1; i < Convert.ToInt32(hfNoOfPeriod.Value); i++, j++)
                            {
                                Label lbl = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblPeriod" + j.ToString()) as Label;
                                DropDownList ddl1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlSubject" + j.ToString()) as DropDownList;
                                DropDownList ddl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlTeacher" + j.ToString()) as DropDownList;
                                lbl.Text = objResult.resultDT.Rows[i]["PeriodID"].ToString();
                                ddl1.SelectedValue = objResult.resultDT.Rows[i]["SubjectMID"].ToString();
                                ddlSubject_OnSelectedIndexChanged(ddl1, e);
                                ddl2.SelectedValue = objResult.resultDT.Rows[i]["TeacherID"].ToString();
                            }
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



        #region Save Button Click Event
        protected void btnSaveClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                TimeTableBO objTimeTableBo = new TimeTableBO();
                TimeTableBL objTimeTableBl = new TimeTableBL();
                for (int i = 1; i <= Convert.ToInt32(hfNoOfPeriod.Value); i++)
                {
                    // Label lbl = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblPeriod" + i.ToString()) as Label;
                    Label lbl = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblPeriod" + i.ToString()) as Label;

                    DropDownList ddl1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlSubject" + i.ToString()) as DropDownList;
                    DropDownList ddl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlTeacher" + i.ToString()) as DropDownList;
                    objTimeTableBo.PeriodID = Convert.ToInt32(lbl.Text);
                    objTimeTableBo.SubjectMID = Convert.ToInt32(ddl1.SelectedValue);
                    objTimeTableBo.EmployeeMID = Convert.ToInt32(ddl2.SelectedValue);
                    objTimeTableBo.EmployeeMID = Convert.ToInt32(ddl2.SelectedValue);
                    objResult = objTimeTableBl.TimeTable_Insert(objTimeTableBo, Convert.ToInt32(ddlClass.SelectedValue),
                        Convert.ToInt32(ddlDivision.SelectedValue),
                        Convert.ToInt32(Session[ApplicationSession.USERID].ToString()),
                        DateTime.UtcNow.AddHours(5.5).ToString());
            }
                    BindTimeTable();
                PanelVisibility(1);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Edit Button Click Event
        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {
            try
            {
                HidePeriod();
                ShowPeriod();
                BindDays(Convert.ToInt32(ddlClass.SelectedValue), 1);
                BindSubject();
                ClearAll();
                PanelVisibility(2);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Button Click Event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindTimeTable(" + Convert.ToInt32(hfNoOfPeriod.Value) + ");</script>");
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
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1").FindControl("tabs"));
            ViewState["Mode"] = "Save";
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divTimeTable.Visible = true;
                tabs.Visible = false;
                ViewState["DropDownMode"] = "Load";
                btnViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                divTimeTable.Visible = false;
                tabs.Visible = true;
                ViewState["DropDownMode"] = "New";
                btnViewList.Visible = true;
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                ddlClass.Enabled = false;
                ddlDivision.Enabled = false;
            }
            else if (ViewState["Mode"].ToString() == "Save")
            {
                ddlClass.Enabled = true;
                ddlDivision.Enabled = true;
            }
        }
        #endregion

        #region Hide Period
        public void HidePeriod()
        {
            for (int i = 1; i <= 10; i++)
            {
                Control MyList = Page.Master.FindControl("ContentPlaceHolder1").FindControl("tr" + i);
                MyList.Visible = false;
            }
        }
        #endregion

        #region Show Period
        public void ShowPeriod()
        {
            for (int i = 1; i <= Convert.ToInt32(hfNoOfPeriod.Value); i++)
            {
               
                    Control MyList = Page.Master.FindControl("ContentPlaceHolder1").FindControl("tr" + i);
                    MyList.Visible = true;
               
            }
        }
        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            divTimeTable.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('timetable');", true);
        }
    }
}