using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

namespace GEIMS.Events
{
    public partial class ScheduleEvents : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ScheduleEvents));
        int intMonth = DateTime.Today.Month;
        int intYear = DateTime.Today.Year;

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            gvScheduleEvent.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");

            try
            {
                // Backword Date Disable for Selection
                CalendarExtender5.StartDate = DateTime.Now;
                CalendarExtender1.StartDate = DateTime.Now;
                if (!IsPostBack)
                {
                    bindMothYear(ddlYear, ddlMonth);
                    ViewState["Mode"] = "Save";
                    tabs.Visible = false;
                    FetchDataForSchoolDropdown();
                    BindScheduledEvent(intMonth,intYear);
                    BindOrgeniserSection();
                    
                    
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Month and Year
        public void bindMothYear(DropDownList ddly, DropDownList ddlm)
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            ddly.Items.Clear();
            intYear = DateTime.Today.Year;
            string CurYear = intYear.ToString();
            for (int i = 2021; i <= intYear+1; i++)
            {
                ddly.Items.Add(i.ToString());
            }

            ddlm.Items.Clear();
            ddlm.Items.Insert(0, new ListItem("--All--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlm.Items.Add(new System.Web.UI.WebControls.ListItem(info.GetMonthName(i), i.ToString()));
            }
            intMonth = DateTime.Today.Month;

            ddlm.SelectedValue = intMonth.ToString();
            ddly.SelectedValue = intYear.ToString();
        }
        #endregion

        #region View List Button Click Event
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {
               
                if (ViewState["Mode"].ToString() == "Edit")
                {
                    DateTime tempDate = Convert.ToDateTime(ViewState["Date"]);
                    intMonth = tempDate.Month;
                    intYear = tempDate.Year;
                }
                ClearAll();
                BindScheduledEvent(intMonth, intYear);
                ddlMonth.SelectedValue = intMonth.ToString();
                ddlYear.SelectedValue = intYear.ToString();
                PanelVisibility(1);
                
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion
        protected void gvScheduleEvent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                if (ddlSchoolName.SelectedIndex == 0)
                {
                    ddlSectionName.Items.Clear();
                    ddlSectionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
                PanelVisibility(2);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Save Button Click Event
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledEventBO objScheduledEventBO = new ScheduledEventBO();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();

                int intScheduledEventID = 0;

                objScheduledEventBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objScheduledEventBO.SchoolMID = Convert.ToInt32(ddlSchoolName.SelectedValue);
                objScheduledEventBO.SectionMID = Convert.ToInt32(ddlSectionName.SelectedValue);
                objScheduledEventBO.EventName = txtEventName.Text;
                objScheduledEventBO.EventCategoryID = Convert.ToInt32(ddlEventCategory.SelectedValue);
                objScheduledEventBO.EventFromDate = txtFromDate.Text.Trim();
                objScheduledEventBO.EventFromDateFromTime = txtFromDateFromTime.Text.Trim();
                objScheduledEventBO.EventFromDateToTime = txtFromDateToTime.Text.Trim();
                objScheduledEventBO.EventToDate = txtToDate.Text.Trim();
                objScheduledEventBO.EventToDateFromTime = txtToDateFromTime.Text.Trim();
                objScheduledEventBO.EventToDateToTime = txtToDateToTime.Text.Trim();
                objScheduledEventBO.EventPlatform = ddlPlatform.SelectedValue.ToString();
                objScheduledEventBO.EventLocation = txtEventLocation.Text;
                objScheduledEventBO.EventDescription = txtEventDescription.Text;
                objScheduledEventBO.EventOrgeniserName = txtOrganiserName.Text;
                objScheduledEventBO.EventOrgeniserSection = ddlOrganiserSection.SelectedValue.ToString();
                objScheduledEventBO.EventNote = txtEventNote.Text;
                objScheduledEventBO.EventMobileNo = txtMobileNo.Text.Trim();
                objScheduledEventBO.EventEmail = txtEmailID.Text.Trim();
                objScheduledEventBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objScheduledEventBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
                DateTime toDate = Convert.ToDateTime(txtToDate.Text);
                DateTime fromDateFromTime = Convert.ToDateTime(txtFromDateFromTime.Text);
                DateTime toDateFromTime = Convert.ToDateTime(txtToDateFromTime.Text);
                DateTime fromDateToTime = Convert.ToDateTime(txtFromDateToTime.Text);
                int count = 0;

                if (fromDate == toDate)
                {
                    if (fromDateFromTime >= toDateFromTime || fromDateToTime >= toDateFromTime)
                    {
                        count++;
                    }
                }

                if(count==0)
                {

                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        intScheduledEventID = -1;
                    }
                    else if (ViewState["Mode"].ToString() == "Edit")
                    {
                        intScheduledEventID = Convert.ToInt32(ViewState["ScheduledEventID"].ToString());
                    }

                    objResult = objScheduledEventBL.EventName_Validate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), intScheduledEventID, objScheduledEventBO.EventName);
                    if (objResult != null)
                    {
                        dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Event Name Already Exists.');</script>");
                        }
                        else
                        {
                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                objScheduledEventBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objScheduledEventBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objResult = objScheduledEventBL.ScheduledEvent_Insert(objScheduledEventBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                                }
                            }
                            else if (ViewState["Mode"].ToString() == "Edit")
                            {
                                objScheduledEventBO.ScheduledEventID = Convert.ToInt32(ViewState["ScheduledEventID"].ToString());
                                objResult = objScheduledEventBL.ScheduledEvent_Update(objScheduledEventBO);
                                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record not updated.');</script>");
                                }
                            }

                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                DateTime tempDate = Convert.ToDateTime(txtToDate.Text.ToString());
                                intMonth = tempDate.Month;  
                                intYear = tempDate.Year;
                            }

                            ClearAll();
                            
                            ddlMonth.SelectedValue = intMonth.ToString();
                            ddlYear.SelectedValue = intYear.ToString();
                            BindScheduledEvent(intMonth, intYear);
                            PanelVisibility(1);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Enter Valid Time for To Date');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region Scheduled Event Gridview  [Row Command for Edit And Delete , Pre rendar]
        protected void gvScheduleEvent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();
                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["ScheduledEventID"] = e.CommandArgument.ToString();

                    objResult = objScheduledEventBL.ScheduledEvent_Select(Convert.ToInt32(ViewState["ScheduledEventID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlSchoolName.SelectedValue = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SCHOOLMID].ToString();
                            dt = FetchSection(Convert.ToInt32(dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlSectionName, "DepartmentNameENG", "DepartmentID");
                            }
                            ddlSectionName.SelectedValue = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SECTIONMID].ToString();
                            txtEventName.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTNAME].ToString();
                            ddlEventCategory.SelectedValue = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTCATEGORYID].ToString();
                            txtFromDate.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATE].ToString();
                            ViewState["Date"]= dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATE].ToString();
                            txtFromDateFromTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATEFROMTIME].ToString();
                            txtFromDateToTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATETOTIME].ToString();
                            txtToDate.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTTODATE].ToString();
                            txtToDateFromTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTTODATEFROMTIME].ToString();
                            txtToDateToTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTTODATETOTIME].ToString();
                            ddlPlatform.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTPLATFORM].ToString();
                            txtEventLocation.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTLOCATION].ToString();
                            txtEventDescription.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTDESCRIPTION].ToString();
                            txtOrganiserName.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTORGENISERNAME].ToString();
                            ddlOrganiserSection.SelectedValue = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTORGENISERSECTION].ToString();
                            txtEventNote.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTNOTE].ToString();
                            txtMobileNo.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTMOBILENO].ToString();
                            txtEmailID.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTEMAIL].ToString();
                            PanelVisibility(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {

                    ViewState["ScheduledEventID"] = e.CommandArgument;

                    objResult = objScheduledEventBL.ScheduledEvent_Delete(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");

                        PanelVisibility(1);
                        BindScheduledEvent(intMonth, intYear);
                        ImagesDelete();
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

        #region Fetch Section data For DropDown When click on Edit
        private DataTable FetchSection(int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();

            ScheduledEventBO objScheduledEventBO = new ScheduledEventBO();
            ScheduleEventsBL ObjScheduleEventsBL = new ScheduleEventsBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = ObjScheduleEventsBL.Department_SelectAll_ForDropDown(intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch EventCategory and SchoolName For Dropdowm
        public void FetchDataForSchoolDropdown()
        {
            ApplicationResult objResult = new ApplicationResult();
            ApplicationResult objSchoolName = new ApplicationResult();
            Controls objControls = new Controls();
            ScheduleEventsBL objScheduleEventBL = new ScheduleEventsBL();

            objResult = objScheduleEventBL.Select_EventCategory_ForDropdown(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

            objSchoolName = objScheduleEventBL.Select_SchoolName_ForDropdown(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

            if (objResult != null && objSchoolName != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlEventCategory, "EventCategoryName", "EventCategoryID");
                objControls.BindDropDown_ListBox(objSchoolName.resultDT, ddlSchoolName, "SchoolNameEng", "SchoolMID");

            }

            ddlEventCategory.Items.Insert(0, new ListItem("--Select--", ""));
            ddlSchoolName.Items.Insert(0, new ListItem("--Select--", ""));
            ddlSectionName.Items.Insert(0, new ListItem("--Select--", ""));
            ddlPlatform.Items.Insert(0, new ListItem("--Select--", ""));
            ddlPlatform.Items.Insert(1, new ListItem("Online"));
            ddlPlatform.Items.Insert(2, new ListItem("Offline"));
            ddlOrganiserSection.Items.Insert(0, new ListItem("--Select--", ""));

        }
        #endregion

        #region Fetch SectionName for Dropdown Section Name
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DepartmentBL objDepartmentBl = new DepartmentBL();

                if (ddlSchoolName.SelectedIndex != 0)
                {

                    objResult = objDepartmentBl.Department_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {


                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlSectionName, "DepartmentNameENG", "DepartmentID");

                        }

                    }
                    ddlSectionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    ddlSectionName.Items.Clear();
                    ddlSectionName.Items.Insert(0, new ListItem("--Select--", ""));
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Bind ScheduledEvent For GridView
        public void BindScheduledEvent()
        {
           /* try
            {
                ApplicationResult objResult = new ApplicationResult();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();

                objResult = objScheduledEventBL.ScheduledEvent_Select_By_Trust_School(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvScheduleEvent.DataSource = objResult.resultDT;
                    gvScheduleEvent.DataBind();
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
            }*/
        }

        public void BindScheduledEvent(int intMonth, int intYear)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();

                objResult = objScheduledEventBL.ScheduledEvent_Select_By_Trust_School(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intMonth, intYear);

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvScheduleEvent.Visible = true;
                    gvScheduleEvent.DataSource = objResult.resultDT;
                    gvScheduleEvent.DataBind();
                    PanelVisibility(1);
                }
                else
                {
                    gvScheduleEvent.Visible = false;
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

        #region Bind Orgeniser Section For Dropdown
        public void BindOrgeniserSection()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DepartmentBL objDepartmentBl = new DepartmentBL();

                objResult = objDepartmentBl.Department_SelectAll();
                if (objResult != null)
                {
                    ddlOrganiserSection.Enabled = true;
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlOrganiserSection, "DepartmentNameENG", "DepartmentID");

                    }
                    ddlOrganiserSection.Items.Insert(0, new ListItem("--Select--", ""));

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Clear ALL Method
        public void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1").FindControl("ddlSectionName"));
            ViewState["Mode"] = "Save";

        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intCode)
        {
            if (intCode == 1)
            {
                divGrid.Visible = true;
                pnlSearch.Visible = true;
               
                tabs.Visible = false;
                lnkViewList.Visible = false;
               
            }
            else if (intCode == 2 && ViewState["Mode"].ToString() == "Save")
            {
                btnSaveClass.Text = "Save";
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                pnlSearch.Visible = false;
            }
            else if (intCode == 2 && ViewState["Mode"].ToString() == "Edit")
            {
                btnSaveClass.Text = "Update";
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                pnlSearch.Visible = false;
            }
        }
        #endregion

        #region Delete Images
        public void ImagesDelete()
        {
            ApplicationResult objEventName = new ApplicationResult();
            EventDetailsBL objEventDetailsBL = new EventDetailsBL();

            objEventName = objEventDetailsBL.EventDetails_Images_Select(Convert.ToInt32(ViewState["ScheduledEventID"]));

            if (objEventName.resultDT.Rows.Count > 0)
            {

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/EventImages"));
                List<MyImageFiles> images = new List<MyImageFiles>();

                foreach (var l in dir.GetFiles())
                {
                    images.Add(new MyImageFiles { FileName = l.Name, FileSize = (l.Length / 1024).ToString() });
                }
                List<MyImageFiles> images1 = new List<MyImageFiles>();

                for (int i = 0; i < objEventName.resultDT.Rows.Count; i++)
                {
                    DirectoryInfo dir1 = new DirectoryInfo(Server.MapPath("~/EventImages/" + objEventName.resultDT.Rows[i]["FileName"].ToString()));
                    for (int J = 0; J < images.Count; J++)
                    {
                        if (images[J].FileName == objEventName.resultDT.Rows[i]["FileName"].ToString())
                        {
                            File.Delete(Server.MapPath("~/EventImages/" + objEventName.resultDT.Rows[i]["FileName"].ToString()));
                            File.Delete(Server.MapPath("~/Thumbnil/" + objEventName.resultDT.Rows[i]["FileName"].ToString()));
                            FileInfo finfo = new FileInfo(objEventName.resultDT.Rows[i]["FileName"].ToString());
                            finfo.Delete();
                        }

                    }

                }

            }

        }

        #endregion

        #region Textbox organiser name text changed event
        protected void txtOrganiserName_TextChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Dropdown Month and Year Selected Index Change Event
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            intYear = Convert.ToInt32(ddlYear.SelectedValue);
            BindScheduledEvent(intMonth, intYear);
           

        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            intYear = Convert.ToInt32(ddlYear.SelectedValue);
            BindScheduledEvent(intMonth, intYear);
        }
        #endregion
    }
}