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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace GEIMS.ReportUI
{
    public partial class TimeTableClassWise : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TimeTableClassWise));
        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClass();
                divTimeTable.Visible = false;
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
            ddlClass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
            ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
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
            ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
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
                    for (int i = 1; i <= 7; i++)
                    {
                        DataTable dtTemp = objResult.resultDT.AsEnumerable().Where(r => r.Field<int>("DayNo") == i)
                        .CopyToDataTable();
                        if (dtTemp.Rows.Count > 0)
                        {
                            for (int j = 1; j <= Convert.ToInt32(ViewState["NoOfPeriod"].ToString()); j++)
                            {
                                Label lbl1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblFrom" + j.ToString() + "" + i.ToString()) as Label;
                                lbl1.Text = dtTemp.Rows[j - 1]["FromTime"].ToString();
                                Label lbl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblTo" + j.ToString() + "" + i.ToString()) as Label;
                                lbl2.Text = dtTemp.Rows[j - 1]["ToTime"].ToString();
                                Label lbl3 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblSub" + j.ToString() + "" + i.ToString()) as Label;
                                lbl3.Text = dtTemp.Rows[j - 1]["SubjectName"].ToString();
                                Label lbl4 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblTeacher" + j.ToString() + "" + i.ToString()) as Label;
                                lbl4.Text = dtTemp.Rows[j - 1]["Teacher"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
                }
            }
            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindTimeTable(" + Convert.ToInt32(ViewState["NoOfPeriod"].ToString()) + ");</script>");
        }
        #endregion

        #region Bind Subject
        public void BindSubject()
        {
            ApplicationResult objResult = new ApplicationResult();
            SubjectTBL objSubjectTbl = new SubjectTBL();
            Controls objControl = new Controls();
            int intCount = Convert.ToInt32(ViewState["NoOfPeriod"].ToString());
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
                ddl1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("None", "-1"));
                ddl2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("None", "-1"));
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
                                ViewState["NoOfPeriod"] = objResult.resultDT.Rows[0][ClassBO.CLASS_NOOFPERIOD].ToString();
                            }
                        }
                        BindDivision(Convert.ToInt32(ddlClass.SelectedValue));
                    }
                    else
                    {
                        ddlDivision.Items.Clear();
                        ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
                      //  ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('First you have to insert data in period.');</script>");
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
                    divTimeTable.Visible = true;
                        BindTimeTable();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Buttons Click Events
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {

        }

        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            divTimeTable.Visible = true;
            BindTimeTable();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('DisplayPrint');", true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            BindClass();
            ddlDivision.Items.Clear();
        }
        #endregion
        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolTimeTableReports");
        }
        #endregion
    }
}