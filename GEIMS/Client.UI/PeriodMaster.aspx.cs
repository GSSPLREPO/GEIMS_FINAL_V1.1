using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.Client.UI
{
    public partial class PeriodMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(PeriodMaster));

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
                    //BindSubject();
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion



        #region Bind Period
        public void BindPeriod()
        {
            ApplicationResult objResult = new ApplicationResult();
            PeriodBL objPeriodBl = new PeriodBL();

            objResult = objPeriodBl.Period_Select_By_School_Class(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]),Convert.ToInt32(ddlClass.SelectedValue));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvPeriod.DataSource = objResult.resultDT;
                gvPeriod.DataBind();
                PanelVisibility(1);
            }
            else
            {
                PanelVisibility(2);
                BindDays(Convert.ToInt32(ddlClass.SelectedValue), 2);
                int intcount = Convert.ToInt32(hfNoOfPeriod.Value);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindPeriod(" + intcount + ");</script>");
            }
        }
        #endregion

        #region Bind Class
        public void BindClass()
        {
            ApplicationResult objResult = new ApplicationResult();
            ClassBL objClassBl = new ClassBL();
            Controls objControl=new Controls();
            objResult = objClassBl.Class_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT,ddlClass,"ClassName","ClassMID");
            }
            ddlClass.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion

        #region Bind Days
        public void BindDays(int intClassMID, int intMode)
        {

            ApplicationResult objResult = new ApplicationResult();
            PeriodBL objPeriodBl = new PeriodBL();
            Controls objControl = new Controls();
            objResult = objPeriodBl.Period_SelectAll_Days(intClassMID,intMode);
            if (objResult != null)
            {
                objControl.BindDropDown_ListBox(objResult.resultDT, ddlDay, "DayName", "DayNo");
            }
            ddlDay.Items.Insert(0, new ListItem("--Select--", ""));
        }
        #endregion



        #region Class Drop Down Selected Index Change Event
        protected void ddlClass_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClass.SelectedValue != "")
                {
                    ApplicationResult objResult = new ApplicationResult();
                    ClassBL objClassBl = new ClassBL();
                    objResult = objClassBl.Class_Select(Convert.ToInt32(ddlClass.SelectedValue));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            hfNoOfPeriod.Value = objResult.resultDT.Rows[0][ClassBO.CLASS_NOOFPERIOD].ToString();
                        }
                    }
                    if (ViewState["DropDownMode"].ToString() == "Load")
                    {
                        BindPeriod();
                    }
                    else if (ViewState["DropDownMode"].ToString() == "New")
                    {
                        BindDays(Convert.ToInt32(ddlClass.SelectedValue), 2);
                        int intcount = Convert.ToInt32(hfNoOfPeriod.Value);
                        ClientScript.RegisterStartupScript(typeof (Page), "MessagePopUp",
                            "<script>BindPeriod(" + intcount + ");</script>");
                    }
                }
                else
                {
                    gvPeriod.DataSource = null;
                    gvPeriod.DataBind();
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
                PeriodBO objPeriodBo = new PeriodBO();
                PeriodBL objPeriodBl = new PeriodBL();
                objPeriodBo.DayName = ddlDay.SelectedItem.ToString();
                objPeriodBo.DayNo = Convert.ToInt32(ddlDay.SelectedValue);
                objPeriodBo.ClassMID = Convert.ToInt32(ddlClass.SelectedValue);
                objPeriodBo.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString());
                int intCount = Convert.ToInt32(hfNoOfPeriod.Value);
                if (ViewState["Mode"].ToString() == "Save")
                {
                    for (int i = 1; i <= intCount; i++)
                    {
                        objPeriodBo.PeriodNo = i;
                        TextBox txt1 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtFromHH" + i.ToString()) as
                                TextBox;
                        TextBox txt2 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtFromMM" + i.ToString()) as
                                TextBox;
                        string a, b, c, d;
                        a = txt1.Text;
                        b = txt2.Text;
                        if (txt1.Text == "")
                            a = "--";
                        if (txt2.Text == "")
                            b = "--";
                        objPeriodBo.FromTime = a + ":" + b;
                        TextBox txt3 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtToHH" + i.ToString()) as
                                TextBox;
                        TextBox txt4 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtToMM" + i.ToString()) as
                                TextBox;
                        c = txt3.Text;
                        d = txt4.Text;
                        if (c == "")
                            c = "--";
                        if (d == "")
                            d = "--";
                        objPeriodBo.ToTime = c + ":" + d;
                        objPeriodBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objPeriodBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objResult = objPeriodBl.Period_Insert(objPeriodBo);
                    }
                    ClientScript.RegisterStartupScript(typeof (Page), "MessagePopUp",
                        "<script>alert('Record saved successfully.');</script>");
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    for (int i = 1; i <= intCount; i++)
                    {
                        objPeriodBo.PeriodNo = i;
                        TextBox txt1 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtFromHH" + i.ToString()) as
                                TextBox;
                        TextBox txt2 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtFromMM" + i.ToString()) as
                                TextBox;
                        string a, b, c, d;
                        a = txt1.Text;
                        b = txt2.Text;
                        if (txt1.Text == "")
                            a = "--";
                        if (txt2.Text == "")
                            b = "--";
                        objPeriodBo.FromTime = a + ":" + b;
                        TextBox txt3 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtToHH" + i.ToString()) as
                                TextBox;
                        TextBox txt4 =
                            Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtToMM" + i.ToString()) as
                                TextBox;
                        c = txt3.Text;
                        d = txt4.Text;
                        if (c == "")
                            c = "--";
                        if (d == "")
                            d = "--";
                        objPeriodBo.ToTime = c + ":" + d;
                        objPeriodBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objPeriodBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objResult = objPeriodBl.Period_Update(objPeriodBo);
                    }
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                        "<script>alert('Record updated successfully.');</script>");
                }
                ClearAll();
                BindPeriod();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(2);
                BindDays(Convert.ToInt32(ddlClass.SelectedValue),2);
                int intcount = Convert.ToInt32(hfNoOfPeriod.Value);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindPeriod(" + intcount + ");</script>");
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Button Click event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Period GridView Events [Row Command]
        protected void gvPeriod_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                PeriodBL objPeriodBl = new PeriodBL();
                if (e.CommandName.ToString() == "Edit1")
                {
                    BindDays(-1,1);
                    ViewState["Mode"] = "Edit";
                    objResult = objPeriodBl.Period_Select_By_Class(Convert.ToInt32(ddlClass.SelectedValue),
                        Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            ddlDay.SelectedValue = objResult.resultDT.Rows[0][PeriodBO.PERIOD_DAYNO].ToString();
                            int intcount = Convert.ToInt32(objResult.resultDT.Rows.Count);
                            for (int i = 0,j=1; i < intcount; i++,j++)
                            {
                                string[] strFrom =
                                    objResult.resultDT.Rows[i][PeriodBO.PERIOD_FROMTIME].ToString().Split(':');
                                TextBox txt1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtFromHH" + j.ToString()) as TextBox;
                                TextBox txt2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtFromMM" + j.ToString()) as TextBox;
                                txt1.Text = strFrom[0];
                                txt2.Text = strFrom[1];

                                string[] strTo =
                                    objResult.resultDT.Rows[i][PeriodBO.PERIOD_TOTIME].ToString().Split(':');
                                TextBox txt3 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtToHH" + j.ToString()) as TextBox;
                                TextBox txt4 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("txtToMM" + j.ToString()) as TextBox;
                                txt3.Text = strTo[0];
                                txt4.Text = strTo[1];
                            }
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindPeriod("+intcount+");</script>");
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    objResult = objPeriodBl.Period_Delete_By_Class_Day(Convert.ToInt32(ddlClass.SelectedValue),
                        Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session[ApplicationSession.USERID].ToString()), DateTime.UtcNow.AddHours(5.5).ToString());
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        BindPeriod();
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
                gvPeriod.Visible = true;
                tabs.Visible = false;
                ViewState["DropDownMode"] = "Load";
                btnViewList.Visible = false;
            }
            else if (intcode == 2)
            {
                gvPeriod.Visible = false;
                tabs.Visible = true;
                ViewState["DropDownMode"] = "New";
                btnViewList.Visible = true;
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                ddlClass.Enabled = false;
                ddlDay.Enabled = false;
            }
            else if (ViewState["Mode"].ToString() == "Save")
            {
                ddlClass.Enabled = true;
                ddlDay.Enabled = true;
            }
        }
        #endregion
    }
}