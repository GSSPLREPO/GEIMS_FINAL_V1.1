using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.BO;
using GEIMS.BL;
using GEIMS.Common;
using System.Data;
using log4net;
using System.IO;
using System.Globalization;

namespace GEIMS.Meeting
{
    public partial class MeetingPoints : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(MeetingPoints));
        int intMonth = DateTime.Today.Month;
        int intYear = DateTime.Today.Year;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CalendarExtender5.StartDate = DateTime.Now;
                if (!IsPostBack)
                {
                    bindMothYear(ddlYear, ddlMonth);
                    ViewState["Mode"] = "Save";
                    pnlDropdown.Visible = true;
                    //BindMeetingPoints();
                    BindMeetingTopic(intMonth, intYear);
                    gvMeetingPoints.Visible = false;
                    tabs.Visible = false;
                    lnkViewList.Visible = false;

                    ddlAssignTo.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlStatus.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlStatus.Items.Insert(1, new ListItem("Assing"));
                    ddlStatus.Items.Insert(2, new ListItem("Pending"));
                    ddlStatus.Items.Insert(3, new ListItem("Complete"));

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
            for (int i = 2021; i <= intYear + 1; i++)
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

        #region Bind Meeting Topic For Dropdown
        public void BindMeetingTopic(int intMonth, int intYear)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                MeetingPointsBL objMeetingPointsBL = new MeetingPointsBL();

                objResult = objMeetingPointsBL.MeetingTopic_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intMonth, intYear);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlMeeting, "Topic", "MeetingID");
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlMeetingName, "Topic", "MeetingID");
                        pnlMeetingName.Visible = true;
                        ddlMeeting.Items.Insert(0, new ListItem("--Select--", ""));
                        ddlMeetingName.Items.Insert(0, new ListItem("----- Select Meeting Name -----", "0"));

                    }

                    else
                    {
                        pnlMeetingName.Visible = false;
                        gvMeetingPoints.Visible = false;
                        ddlMeeting.Items.Clear();
                        ddlMeetingName.Items.Clear();
                        ddlMeeting.Items.Insert(0, new ListItem("--Select--", ""));
                        ddlMeetingName.Items.Insert(0, new ListItem("----- Select Meeting Name -----", "0"));
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Button Save click Event
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                MeetingPointsBO objMeetingPointsBO = new MeetingPointsBO();
                MeetingPointsBL objMeetingPointsBL = new MeetingPointsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();

                int intPointID = 0;

                objMeetingPointsBO.MeetingID = Convert.ToInt32(ddlMeeting.SelectedValue);
                objMeetingPointsBO.ParticipantID = Convert.ToInt32(ddlAssignTo.SelectedValue);
                objMeetingPointsBO.Point = txtPoint.Text.Trim();
                objMeetingPointsBO.AssignTo = ddlAssignTo.SelectedItem.ToString();
                objMeetingPointsBO.Action = txtAction.Text.Trim();
                objMeetingPointsBO.TargetDate = txtTargetDate.Text.Trim();
                objMeetingPointsBO.Status = ddlStatus.SelectedValue.ToString();
                objMeetingPointsBO.Remarks = txtRemarks.Text.Trim();
                objMeetingPointsBO.LastModifiedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objMeetingPointsBO.LastModifiedByDate = DateTime.UtcNow.AddHours(5.5).ToString();

                if (ViewState["Mode"].ToString() == "Save")
                {
                    intPointID = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intPointID = Convert.ToInt32(ViewState["PointID"].ToString());
                }

                objResult = objMeetingPointsBL.MeetingPoint_Validate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlMeeting.SelectedValue), objMeetingPointsBO.Point);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0 && ViewState["Mode"].ToString()=="Save")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Point Already Added');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objMeetingPointsBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objMeetingPointsBO.CreatedByDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objMeetingPointsBL.MeetingPoint_Insert(objMeetingPointsBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objMeetingPointsBO.PointID = Convert.ToInt32(ViewState["PointID"].ToString());
                            objResult = objMeetingPointsBL.MeetingPoints_Update(objMeetingPointsBO);

                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record not updated.');</script>");
                            }
                        }
                        ClearAll();
                        ddlMonth.SelectedValue = intMonth.ToString();
                        BindMeetingPoints(Convert.ToInt32(objMeetingPointsBO.MeetingID));
                        ddlMeetingName.SelectedValue = objMeetingPointsBO.MeetingID.ToString();
                        PanelVisibility(1);
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

        #region Bind Meeting Points Details For GridView
        public void BindMeetingPoints(int intMeeting)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingPointsBL objMeetingPointsBL = new MeetingPointsBL();
                if (intMeeting == 0)
                {
                    gvMeetingPoints.Visible = false;
                    tabs.Visible = false;
                    pnlDropdown.Visible = true;
                }
                else
                {
                    objResult = objMeetingPointsBL.GridviewMeetingPoints_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intMeeting);

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvMeetingPoints.Visible = true;
                        gvMeetingPoints.DataSource = objResult.resultDT;
                        gvMeetingPoints.DataBind();
                        PanelVisibility(1);
                    }
                    else
                    {
                        //ddlMeeting.SelectedValue = ddlMeetingName.SelectedValue;
                        PanelVisibility(2);
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

        #region View list button click
        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {
            try
            {


                intMonth = Convert.ToInt32(ViewState["Month"]);
                intYear = Convert.ToInt32(ViewState["Year"]);
                if (intMonth == 0 || intYear == 0)
                {
                    intMonth = DateTime.Today.Month;
                    intYear = DateTime.Today.Year;
                }
                ClearAll();
                BindMeetingTopic(intMonth, intYear);
                ddlMonth.SelectedValue = intMonth.ToString();
                ddlYear.SelectedValue = intYear.ToString();
                pnlDropdown.Visible = true;
                PanelVisibility(1);
                BindMeetingPoints(ddlMeetingName.SelectedIndex);

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
                //BindMeetingCategory();
                PanelVisibility(2);
                ddlAssignTo.Items.Clear();
                ddlAssignTo.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gridview Row Command for Edit and Delete
        protected void gvMeetingPoints_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                MeetingPointsBL objMeetingPointBL = new MeetingPointsBL();
                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["PointID"] = e.CommandArgument.ToString();

                    objResult = objMeetingPointBL.PointID_Select_For_Edit(Convert.ToInt32(ViewState["PointID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlMeeting.SelectedValue = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_MEETINGID].ToString();
                            txtPoint.Text = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_POINT].ToString();
                            txtAction.Text = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_ACTION].ToString();
                            dt = FetchParticipant(Convert.ToInt32(dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_MEETINGID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlAssignTo, "Name", "ParticipantID");
                            }
                            ddlAssignTo.SelectedValue = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_ParticipantID].ToString();
                            txtTargetDate.Text = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_TARGETDATE].ToString();
                            ddlStatus.SelectedValue = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_STATUS].ToString();
                            txtRemarks.Text = dtResult.Rows[0][MeetingPointsBO.MEETINGPOINTS_REMARKS].ToString();

                            PanelVisibility(2);

                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {

                    ViewState["PointID"] = e.CommandArgument;

                    objResult = objMeetingPointBL.MeetingPoint_Delete(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelVisibility(1);
                        //BindMeetingPoints();
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

        #region Fetch Partcipant For AssingTo DropDown When click on Edit
        private DataTable FetchParticipant(int intMeetingID)
        {
            //DataTable dtDivision = new DataTable();

            MeetingPointsBO objMeetingPointBO = new MeetingPointsBO();
            MeetingPointsBL objMeetingPointBL = new MeetingPointsBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objMeetingPointBL.Participant_SelectAll_ForDropDown(intMeetingID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Clear ALL Method
        public void ClearAll()
        {
            try
            {
                Controls objControl = new Controls();
                objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                ViewState["MeetingID"] = null;
                ViewState["Mode"] = "Save";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = true;
                pnlDropdown.Visible = true;
                gvMeetingPoints.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intcode == 2 && ViewState["Mode"].ToString() == "Edit")
            {

                btnSaveClass.Text = "Update";
                divGrid.Visible = true;
                pnlDropdown.Visible = false;
                gvMeetingPoints.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;

            }
            else if (intcode == 2 && ViewState["Mode"].ToString() == "Save")
            {

                btnSaveClass.Text = "Save";
                divGrid.Visible = true;
                gvMeetingPoints.Visible = false;
                pnlDropdown.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }


        }

        #endregion

        #region Meeting Dropdown selected Index Change event bind Agenda
        protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                MeetingPointsBL objMeetingPointBL = new MeetingPointsBL();

                if (ddlMeeting.SelectedIndex != 0)
                {

                    objResult = objMeetingPointBL.AssignTo_SelectAll_ForDropDown(Convert.ToInt32(ddlMeeting.SelectedValue));

                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlAssignTo, "EmployeeName", "ParticipantID");
                        ddlAssignTo.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                    else
                    {
                        ddlAssignTo.Items.Clear();
                        ddlAssignTo.Items.Insert(0, new ListItem("--Select--", ""));
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You are not Added Any Participant for Assign To');</script>");
                    }

                }
                else
                {
                    ddlAssignTo.Items.Clear();
                    ddlAssignTo.Items.Insert(0, new ListItem("--Select--", ""));
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Code for Generate Serial Number in Meeting Points GridView
        protected void gvMeetingPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrNo") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }
        #endregion

        #region Dropdown Meeting Name Selected Index Change Event
        protected void ddlMeetingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intMeeting = Convert.ToInt32(ddlMeetingName.SelectedValue);
            BindMeetingPoints(intMeeting);
        }
        #endregion

        #region Dropdown Month and Year Selected Index Change Event
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Month"] = Convert.ToInt32(ddlMonth.SelectedValue);
            ViewState["Year"] = Convert.ToInt32(ddlYear.SelectedValue);
            BindMeetingTopic(Convert.ToInt32(ViewState["Month"]), Convert.ToInt32(ViewState["Year"]));
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Month"] = Convert.ToInt32(ddlMonth.SelectedValue);
            ViewState["Year"] = Convert.ToInt32(ddlYear.SelectedValue);
            BindMeetingTopic(Convert.ToInt32(ViewState["Month"]), Convert.ToInt32(ViewState["Year"]));
        }
        #endregion
    }
}