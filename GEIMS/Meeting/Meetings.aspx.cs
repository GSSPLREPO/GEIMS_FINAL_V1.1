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
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ListItem = System.Web.UI.WebControls.ListItem;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net.WebSockets;
using System.Net;

namespace GEIMS.Meeting
{
    public partial class Meetings : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(MeetingCategory));
        int intMonth = DateTime.Today.Month;
        int intYear = DateTime.Today.Year;

        // Code For Meeteing Details 

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            gvMeeting.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
            gvAgenda.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
            gvParticipant.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");


            try
            {
                CalendarExtender5.StartDate = DateTime.Now;

                if (!IsPostBack)
                {
                    bindMothYear(ddlYear, ddlMonth);
                    ViewState["Mode"] = "Save";
                    pnlEmployee.Visible = false;
                    pnlExternal.Visible = false;
                    BindMeetingCategory();
                    BindEmployeeName(ddlMinituesBy);
                    BindEmployeeName(ddlOrganize);
                    intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
                    intYear = Convert.ToInt32(ddlYear.SelectedValue);

                    BindgvMetting(intMonth, intYear);
                    ddlMode.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlMode.Items.Insert(1, new ListItem("Online"));
                    ddlMode.Items.Insert(2, new ListItem("Offline"));
                    ddlMode.Items.Insert(3, new ListItem("Both"));

                    ddlStatus.Items.Insert(0, new ListItem("--Select--", ""));
                    ddlStatus.Items.Insert(1, new ListItem("Create"));
                    ddlStatus.Items.Insert(2, new ListItem("Pending"));
                    ddlStatus.Items.Insert(3, new ListItem("Complete"));
                    pnlAgenda.Visible = false;
                    //UpdatePanelAgenda.Visible = false;
                    pnlParticipant.Visible = false;
                    pnlButton.Visible = false;
                    pnlSendMOM.Visible = false;

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
              
                BindgvMetting(intMonth, intYear);
                ddlMonth.SelectedValue = intMonth.ToString();
                ddlYear.SelectedValue = intYear.ToString();
                //PanelVisibility(1);
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
                btnSaveClass.Visible = true;
                pnlButton.Visible = false;
                //UpdatePanelAgenda.Visible = false;
                pnlAgenda.Visible = false;
                pnlParticipant.Visible = false;
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Button Save Click Event
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                MeetingsBO objMeetingsBO = new MeetingsBO();
                MeetingsBL objMeetingsBL = new MeetingsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intMeetingID = 0;
               
                objMeetingsBO.CategoryID = Convert.ToInt32(ddlMettingCategory.SelectedValue.ToString());
                objMeetingsBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                objMeetingsBO.Topic = txtTopic.Text.Trim();
                objMeetingsBO.MeetingDate = txtDate.Text.Trim();
                objMeetingsBO.FromTime = txtFromTime.Text.Trim();
                objMeetingsBO.Totime = txtToTime.Text.Trim();
                objMeetingsBO.Venue = txtVenue.Text.Trim();
                objMeetingsBO.Mode = ddlMode.SelectedValue.ToString();
                objMeetingsBO.OrganizeBy = Convert.ToInt32(ddlOrganize.SelectedValue.ToString());
                objMeetingsBO.Status = ddlStatus.SelectedValue.ToString();
                objMeetingsBO.MinutesBy = Convert.ToInt32(ddlMinituesBy.SelectedValue.ToString());
                objMeetingsBO.LastModifiedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objMeetingsBO.LastModifiedByDate = DateTime.UtcNow.AddHours(5.5).ToString();

                if (ViewState["Mode"].ToString() == "Save")
                {
                    intMeetingID = -1;

                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intMeetingID = Convert.ToInt32(ViewState["MeetingID"].ToString());
                }

               

                objResult = objMeetingsBL.Meetings_Validate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), objMeetingsBO.CategoryID, objMeetingsBO.Topic, objMeetingsBO.MeetingDate, objMeetingsBO.FromTime, objMeetingsBO.Totime, objMeetingsBO.Venue, objMeetingsBO.Mode, objMeetingsBO.MinutesBy);
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;

                    int temp = 0;

                    

                    if (dtResult.Rows.Count > 0)
                    {

                       

                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            temp = 0;
                            string str1 = txtFromTime.Text;
                            string str2 = txtToTime.Text;
                            string str3 = dtResult.Rows[i]["FromTime"].ToString();
                            string str4 = dtResult.Rows[i]["ToTime"].ToString();
                            str1 = str1.Replace(":", "");
                            str2 = str2.Replace(":", "");
                            str3 = str3.Replace(":", "");
                            str4 = str4.Replace(":", "");

                            int int1 = Convert.ToInt32(str1);
                            int int2 = Convert.ToInt32(str2);
                            int int3 = Convert.ToInt32(str3);
                            int int4 = Convert.ToInt32(str4);

                            int meetingID1 = Convert.ToInt32(ViewState["MeetingID"]);
                            int meetingID2 = Convert.ToInt32(dtResult.Rows[i]["MeetingID"]);


                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                if(txtTopic.Text == dtResult.Rows[i]["Topic"].ToString())
                                {
                                    temp = 1000;
                                }
                                else if(txtTopic.Text != dtResult.Rows[i]["Topic"].ToString())
                                {
                                    if (txtDate.Text.ToString() == dtResult.Rows[i]["MeetingDate"].ToString())
                                    {
                                        if (int1 < int4)
                                        {
                                            if (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"]))
                                            {
                                                temp++;
                                            }

                                            if (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString())
                                            {
                                                temp++;
                                            }
                                            if (ddlMode.SelectedItem.ToString() == "Offline" || ddlMode.SelectedItem.ToString() == "Both")
                                            {
                                                temp++;
                                            }

                                        }

                                    }
                                }
                                
                            }
                            else if(ViewState["Mode"].ToString() == "Edit")
                            {
                                if (
                                    (Convert.ToInt32( ddlMettingCategory.SelectedValue) == Convert.ToInt32( dtResult.Rows[i]["CategoryID"])) &&
                                    (txtTopic.Text==dtResult.Rows[i]["Topic"].ToString()) &&
                                    (txtDate.Text == dtResult.Rows[i]["MeetingDate"].ToString()) &&
                                     (txtFromTime.Text == dtResult.Rows[i]["FromTime"].ToString()) &&
                                     (txtToTime.Text == dtResult.Rows[i]["ToTime"].ToString()) &&
                                     (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString()) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (Convert.ToInt32(ddlOrganize.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["OrganizeBy"])) &&
                                     (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"])) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (ddlStatus.SelectedValue.ToString() == dtResult.Rows[i]["Status"].ToString())
                                    )
                                {
                                    temp = 2000;
                                }
                                else if (
                                    (Convert.ToInt32(ddlMettingCategory.SelectedValue) != Convert.ToInt32(dtResult.Rows[i]["CategoryID"])) &&
                                     (txtDate.Text == dtResult.Rows[i]["MeetingDate"].ToString()) &&
                                     (txtFromTime.Text == dtResult.Rows[i]["FromTime"].ToString()) &&
                                     (txtToTime.Text == dtResult.Rows[i]["ToTime"].ToString()) &&
                                     (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString()) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (Convert.ToInt32(ddlOrganize.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["OrganizeBy"])) &&
                                     (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"])) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (ddlStatus.SelectedValue.ToString() == dtResult.Rows[i]["Status"].ToString())
                                  )
                                {
                                    temp = 0;
                                }
                                else if (
                                     (txtDate.Text == dtResult.Rows[i]["MeetingDate"].ToString()) &&
                                     (txtFromTime.Text == dtResult.Rows[i]["FromTime"].ToString()) &&
                                     (txtToTime.Text == dtResult.Rows[i]["ToTime"].ToString()) &&
                                     (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString()) && 
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (Convert.ToInt32(ddlOrganize.SelectedValue) == Convert.ToInt32( dtResult.Rows[i]["OrganizeBy"])) &&
                                     (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"])) &&
                                     (ddlMode.SelectedValue.ToString()==dtResult.Rows[i]["Mode"].ToString()) &&
                                     (ddlStatus.SelectedValue.ToString()!=dtResult.Rows[i]["Status"].ToString())
                                  )
                                {
                                    temp = 0;
                                }
                                else if(
                                    (txtDate.Text == dtResult.Rows[i]["MeetingDate"].ToString()) &&
                                     (txtFromTime.Text == dtResult.Rows[i]["FromTime"].ToString()) &&
                                     (txtToTime.Text == dtResult.Rows[i]["ToTime"].ToString()) &&
                                     (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString()) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (Convert.ToInt32(ddlOrganize.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["OrganizeBy"])) &&
                                     (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"])) &&
                                     (ddlMode.SelectedValue.ToString() != dtResult.Rows[i]["Mode"].ToString()) &&
                                     (ddlStatus.SelectedValue.ToString() == dtResult.Rows[i]["Status"].ToString())
                                    )
                                {
                                    temp = 0;
                                }
                                else if(
                                     (txtDate.Text == dtResult.Rows[i]["MeetingDate"].ToString()) &&
                                     (txtFromTime.Text == dtResult.Rows[i]["FromTime"].ToString()) &&
                                     (txtToTime.Text == dtResult.Rows[i]["ToTime"].ToString()) &&
                                     (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString()) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (Convert.ToInt32(ddlOrganize.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["OrganizeBy"])) &&
                                     (Convert.ToInt32(ddlMinituesBy.SelectedValue) != Convert.ToInt32(dtResult.Rows[i]["MinutesBy"])) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (ddlStatus.SelectedValue.ToString() == dtResult.Rows[i]["Status"].ToString())
                                    )
                                {
                                    if (txtDate.Text.ToString() == dtResult.Rows[i]["MeetingDate"].ToString())
                                    {
                                        if (int1 < int4)
                                        {
                                            if (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"]))
                                            {
                                                temp++;
                                            }

                                            if (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString())
                                            {
                                                temp++;
                                            }
                                            if (ddlMode.SelectedItem.ToString() == "Offline" || ddlMode.SelectedItem.ToString() == "Both")
                                            {
                                                temp++;
                                            }

                                        }

                                    }

                                }
                                else if (
                                     (txtDate.Text == dtResult.Rows[i]["MeetingDate"].ToString()) &&
                                     (txtFromTime.Text != dtResult.Rows[i]["FromTime"].ToString()) ||
                                     (txtToTime.Text != dtResult.Rows[i]["ToTime"].ToString()) &&
                                     (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString()) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (Convert.ToInt32(ddlOrganize.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["OrganizeBy"])) &&
                                     (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"])) &&
                                     (ddlMode.SelectedValue.ToString() == dtResult.Rows[i]["Mode"].ToString()) &&
                                     (ddlStatus.SelectedValue.ToString() == dtResult.Rows[i]["Status"].ToString())
                                    )
                                {
                                    if (txtDate.Text.ToString() == dtResult.Rows[i]["MeetingDate"].ToString())
                                    {
                                        if (int1 < int4)
                                        {
                                            if (Convert.ToInt32(ddlMinituesBy.SelectedValue) == Convert.ToInt32(dtResult.Rows[i]["MinutesBy"]))
                                            {
                                                temp++;
                                            }

                                            if (txtVenue.Text == dtResult.Rows[i]["Venue"].ToString())
                                            {
                                                temp++;
                                            }
                                            if (ddlMode.SelectedItem.ToString() == "Offline" || ddlMode.SelectedItem.ToString() == "Both")
                                            {
                                                temp++;
                                            }

                                        }

                                    }


                                }
                            }

                        }

                    }

                    if (temp == 1000)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Meeting Name Already Exits');</script>");
                    }
                    else if (temp == 2000)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Change Any Value For Update');</script>");
                    }
                    else if (temp > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Meeting Already Organized');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            objMeetingsBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objMeetingsBO.CreatedByDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objResult = objMeetingsBL.Meetings_Insert(objMeetingsBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                                objResult = objMeetingsBL.fetchRecentMeetingID(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                                dtResult = objResult.resultDT;
                                if (dtResult.Rows.Count > 0)
                                {
                                    btnSaveClass.Visible = false;
                                    ViewState["MeetingID"] = dtResult.Rows[0]["MeetingID"].ToString();
                                    pnlButton.Visible = true;
                                }
                            }
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objMeetingsBO.MeetingID = Convert.ToInt32(ViewState["MeetingID"].ToString());
                            objResult = objMeetingsBL.Meetings_Update(objMeetingsBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");

                                if (ViewState["Mode"].ToString() == "Edit" && ddlStatus.SelectedValue.ToString() == "Complete")
                                {
                                    btnAgendaShow.Visible = false;
                                    btnParticipantShow.Visible = false;
                                    pnlAgenda.Visible = false;
                                    pnlParticipant.Visible = false;
                                }
                                else
                                {
                                    btnAgendaShow.Visible = true;
                                    btnParticipantShow.Visible = true;
                                   // pnlAgenda.Visible = true;
                                    //pnlParticipant.Visible = true;
                                }
                            }
                        }


                        //ClearAll();
                        //BindgvMetting();
                        //PanelVisibility(1);
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

        #region Gridview Meeting Row Command for Edit and Delete
        protected void gvMeeting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                MeetingsBL objMeetingBL = new MeetingsBL();
                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["MeetingID"] = e.CommandArgument.ToString();
                    btnSaveClass.Visible = true;
                    objResult = objMeetingBL.MeetingID_Select_For_Edit(Convert.ToInt32(ViewState["MeetingID"].ToString()));
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            if (ViewState["Mode"].ToString() == "Edit" && dtResult.Rows[0]["Status"].ToString() == "Complete")
                            {
                                btnAgendaShow.Visible = false;
                                btnParticipantShow.Visible = false;
                            }
                            else
                            {
                                btnAgendaShow.Visible = true;
                                btnParticipantShow.Visible = true;
                                btnAgendaShow.Enabled = true;
                                btnParticipantShow.Enabled = true;
                            }

                            ddlMettingCategory.SelectedValue = dtResult.Rows[0][MeetingsBO.MEETINGS_CATEGORYID].ToString();
                            txtTopic.Text = dtResult.Rows[0][MeetingsBO.MEETINGS_TOPIC].ToString();
                            txtDate.Text = dtResult.Rows[0][MeetingsBO.MEETINGS_MEETINGDATE].ToString();
                            ViewState["Date"] = dtResult.Rows[0][MeetingsBO.MEETINGS_MEETINGDATE].ToString();
                            txtFromTime.Text = dtResult.Rows[0][MeetingsBO.MEETINGS_FROMTIME].ToString();
                            txtToTime.Text = dtResult.Rows[0][MeetingsBO.MEETINGS_TOTIME].ToString();
                            txtVenue.Text = dtResult.Rows[0][MeetingsBO.MEETINGS_VENUE].ToString();
                            ddlMode.SelectedValue = dtResult.Rows[0][MeetingsBO.MEETINGS_MODE].ToString();
                            ddlOrganize.SelectedValue = dtResult.Rows[0][MeetingsBO.MEETINGS_ORGANIZEBY].ToString();
                            ddlStatus.SelectedValue = dtResult.Rows[0][MeetingsBO.MEETINGS_STATUS].ToString();
                            ddlMinituesBy.SelectedValue = dtResult.Rows[0][MeetingsBO.MEETINGS_MINUTESBY].ToString();
                            PanelVisibility(2);
                        }
                       // btnAgendaShow.Enabled = true;
                        //btnParticipantShow.Enabled = true;

                        pnlButton.Visible = true;
                        //UpdatePanelAgenda.Visible = false;
                        pnlAgenda.Visible = false;
                        pnlParticipant.Visible = false;
                    }

                }
                else if (e.CommandName.ToString() == "Delete1")
                {

                    ViewState["MeetingID"] = e.CommandArgument;


                    objResult = objMeetingBL.Meetings_Delete(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");

                        PanelVisibility(1);
                        BindgvMetting(intMonth,intYear);
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

        #region Bind Meeting Details For GridView
        public void BindgvMetting()
        {
            /*try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();
                
                objResult = objMeetingsBL.GridviewMeeting_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvMeeting.DataSource = objResult.resultDT;
                    gvMeeting.DataBind();
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

        public void BindgvMetting(int intMonth, int intYear)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                objResult = objMeetingsBL.GridviewMeeting_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intMonth, intYear);

                if (objResult.resultDT.Rows.Count > 0)
                {
                    pnlSearch.Visible = true;
                    gvMeeting.Visible = true;
                    gvMeeting.DataSource = objResult.resultDT;
                    gvMeeting.DataBind();
                    PanelVisibility(1);
                }
                else
                {
                    ViewState["Mode"] = "Save";
                    PanelVisibility(2);
                    gvMeeting.Visible = false;
                    pnlSearch.Visible = false;
                    //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No Record Found');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Meeting Category For Dropdown
        public void BindMeetingCategory()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                objResult = objMeetingsBL.MeetingCategory_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlMettingCategory, "CategoryName", "CategoryID");

                    }
                    ddlMettingCategory.Items.Insert(0, new ListItem("--Select--", ""));

                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No Record Found.');</script>");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Bind MinituesBy and OrganizeBy For Dropdown
        public void BindEmployeeName(DropDownList DropDownName)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                objResult = objMeetingsBL.MinutesBy_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, DropDownName, "EmployeeName", "EmployeeMID");

                    }
                    DropDownName.Items.Insert(0, new ListItem("--Select--", ""));


                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No Record Found.');</script>");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Radio Button Employee
        protected void rdoEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEmployee.Checked == true)
            {
                pnlEmployee.Visible = true;
                BindEmployeeName(ddlEmployeeName);
                BindgvParticipant();
                pnlExternal.Visible = false;
            }
            else
            {
                pnlEmployee.Visible = false;
            }

        }
        #endregion

        #region Radio Button External

        protected void rdoExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExternal.Checked == true)
            {
                pnlExternal.Visible = true;
                pnlEmployee.Visible = false;
                BindgvParticipant();
                txtParticipantName.Text = "";
                txtEmail.Text = "";
                txtOrgName.Text = "";
            }
            else
            {
                pnlExternal.Visible = false;
            }

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
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intcode == 2 && ViewState["Mode"].ToString() == "Edit")
            {

                btnSaveClass.Text = "Update";
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;

            }
            else if (intcode == 2 && ViewState["Mode"].ToString() == "Save")
            {
                btnSaveClass.Text = "Save";
                divGrid.Visible = false;
                tabs.Visible = true;
                btnSaveClass.Visible = true;
                lnkViewList.Visible = true;
                pnlButton.Visible = false;

            }


        }

        #endregion

        #region Agenda Button Show Click Event
        protected void btnAgendaShow_Click(object sender, EventArgs e)
        {
            BindgvAgenda();
            btnAgendaShow.Enabled = false;
            btnParticipantShow.Enabled = true;
            pnlAgenda.Visible = true;
            //UpdatePanelAgenda.Visible = true;
            pnlParticipant.Visible = false;

        }
        #endregion

        #region Participant Button Show Click Event
        protected void btnParticipantShow_Click(object sender, EventArgs e)
        {
            BindgvParticipant();
            btnParticipantShow.Enabled = false;
            btnAgendaShow.Enabled = true;
            pnlParticipant.Visible = true;
            pnlAgenda.Visible = false;
            //UpdatePanelAgenda.Visible = false;
            pnlExternal.Visible = false;
            pnlEmployee.Visible = false;
            rdoEmployee.Checked = false;
            rdoExternal.Checked = false;
        }
        #endregion

        // Code for Meeting Agendas

        #region Code for Generate Serial Number in Agenda GridView
        protected void gvAgenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrNo") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        #endregion

        #region Insert Agenda Detail  Button Add Click Event
        protected void btnAgenda_Click(object sender, EventArgs e)
        {
            try
            {
                MeetingsBO objMeetingsBO = new MeetingsBO();
                MeetingsBL objMeetingsBL = new MeetingsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();

                objMeetingsBO.MeetingID = Convert.ToInt32(ViewState["MeetingID"].ToString());
                objMeetingsBO.AgendaPoint = txtAgendaPoint.Text.Trim();
                objMeetingsBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objMeetingsBO.CreatedByDate = DateTime.UtcNow.AddHours(5.5).ToString();


                objResult = objMeetingsBL.agendaPoint_Validate(Convert.ToInt32(ViewState["MeetingID"]), objMeetingsBO.AgendaPoint);

                dtResult = objResult.resultDT;
                if (dtResult.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Agenda Already Added');</script>");
                }
                else
                {
                    objResult = objMeetingsBL.MeetingAgenda_Insert(objMeetingsBO);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                        txtAgendaPoint.Text = "";
                        BindgvAgenda();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not saved.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Bind GridView Agenda
        public void BindgvAgenda()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                objResult = objMeetingsBL.GridviewAgenda_SelectAll(Convert.ToInt32(ViewState["MeetingID"]));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvAgenda.Visible = true;
                    gvAgenda.DataSource = objResult.resultDT;
                    gvAgenda.DataBind();

                }
                else
                {
                    gvAgenda.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }



        #endregion

        #region GridView Agenda Row Command Delete Button Click Event
        protected void gvAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                MeetingsBL objMeetingBL = new MeetingsBL();
                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "DeleteAgenda")
                {
                    objResult = objMeetingBL.MeetingAgenda_Delete(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");

                        BindgvAgenda();
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('-- Error --.');</script>");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        // Code for Meeting Participant

        #region Code for Generate Serial Number in Participant GridView
        protected void gvParticipant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrNo1") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        #endregion

        #region Insert Detail of Employee Participant on Add button Click Event
        protected void btnAddParticipantEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                MeetingsBO objMeetingsBO = new MeetingsBO();
                MeetingsBL objMeetingsBL = new MeetingsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();

                objMeetingsBO.MeetingID = Convert.ToInt32(ViewState["MeetingID"].ToString());
                objMeetingsBO.EmployeeID = Convert.ToInt32(ddlEmployeeName.SelectedValue.ToString());

                objResult = objMeetingsBL.EmployeeName_Validate(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlEmployeeName.SelectedValue), txtTopic.Text.ToString());
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Paticipant Already Add for this Meeting');</script>");
                    }

                    else
                    {
                        objMeetingsBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objMeetingsBO.CreatedByDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objResult = objMeetingsBL.MeetingParticipantEmployee_Insert(objMeetingsBO);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                            ddlEmployeeName.SelectedIndex = 0;
                            btnSendInvite.Visible = true;
                            btnUpdate.Visible = true;
                            btnSendMOM.Visible = true;
                            BindgvParticipant();

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not saved.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insert Detail of External Participant on Add button Click Event
        protected void btnAddParticipantExternal_Click(object sender, EventArgs e)
        {
            try
            {
                MeetingsBO objMeetingsBO = new MeetingsBO();
                MeetingsBL objMeetingsBL = new MeetingsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();

                objMeetingsBO.MeetingID = Convert.ToInt32(ViewState["MeetingID"].ToString());
                objMeetingsBO.Name = txtParticipantName.Text.Trim();
                objMeetingsBO.OrgName = txtOrgName.Text.Trim();
                objMeetingsBO.Email = txtEmail.Text.Trim();
                objMeetingsBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objMeetingsBO.CreatedByDate = DateTime.UtcNow.AddHours(5.5).ToString();

                objResult = objMeetingsBL.MeetingParticipantExternal_Insert(objMeetingsBO);
                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                    txtParticipantName.Text = "";
                    txtOrgName.Text = "";
                    txtEmail.Text = "";
                    BindgvParticipant();
                    btnSendInvite.Visible = true;
                    btnUpdate.Visible = true;
                    btnSendMOM.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not saved.');</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Bind GridView Participant
        public void BindgvParticipant()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();

                objResult = objMeetingsBL.GridviewParticipant_SelectAll(Convert.ToInt32(ViewState["MeetingID"]));


                if (objResult.resultDT.Rows.Count > 0)
                {
                    btnSendInvite.Visible = true;
                    btnUpdate.Visible = true;
                    btnSendMOM.Visible = true;


                    gvParticipant.Visible = true;
                    gvParticipant.DataSource = objResult.resultDT;
                    gvParticipant.DataBind();


                    for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(objResult.resultDT.Rows[i]["SendInvite"]) == 1)
                        {
                            ((CheckBox)gvParticipant.Rows[i].FindControl("chkSendInvite")).Checked = true;
                            //((CheckBox)gvParticipant.Rows[i].FindControl("chkSendInvite")).Enabled = false;
                        }
                        else
                        {
                            ((CheckBox)gvParticipant.Rows[i].FindControl("chkSendInvite")).Checked = false;
                        }

                        if (Convert.ToInt32(objResult.resultDT.Rows[i]["SendMOM"]) == 1)
                        {
                            ((CheckBox)gvParticipant.Rows[i].FindControl("chkSendMom")).Checked = true;
                            //((CheckBox)gvParticipant.Rows[i].FindControl("chkSendMom")).Enabled = false;
                        }
                        else
                        {
                            ((CheckBox)gvParticipant.Rows[i].FindControl("chkSendMom")).Checked = false;
                        }

                        if (Convert.ToInt32(objResult.resultDT.Rows[i]["Absent"]) == 1)
                        {
                            ((CheckBox)gvParticipant.Rows[i].FindControl("chkAbsent")).Checked = true;
                        }
                        else
                        {
                            ((CheckBox)gvParticipant.Rows[i].FindControl("chkAbsent")).Checked = false;
                        }

                    }



                }
                else
                {
                    gvParticipant.Visible = false;
                    btnSendInvite.Visible = false;
                    btnSendMOM.Visible = false;
                    btnUpdate.Visible = false;
                }


            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #endregion

        #region GridView Participant Row Command Delete Button Click Event
        protected void gvParticipant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                MeetingsBL objMeetingBL = new MeetingsBL();
                Controls objControls = new Controls();

                if (e.CommandName.ToString() == "EditParticipant")
                {
                    /*  objResult = objMeetingBL.MeetingParticipant_Edit(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                      if (objResult != null)
                      {
                          DataTable dtResult = objResult.resultDT;
                          if (dtResult.Rows.Count > 0)
                          {
                              txtParticipantName.Text = dtResult.Rows[0][MeetingsBO.MEETINGPARTICIPANTS_NAME].ToString();
                              txtOrgName.Text = dtResult.Rows[0][MeetingsBO.MEETINGPARTICIPANTS_ORGNAME].ToString();
                              txtEmail.Text = dtResult.Rows[0][MeetingsBO.MEETINGPARTICIPANTS_EMAIL].ToString();
                          }
                      }*/
                }

                else if (e.CommandName.ToString() == "DeleteParticipant")
                {
                    objResult = objMeetingBL.MeetingParticipant_Delete(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        BindgvParticipant();
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('You cannot delete this record because it is in used.');</script>");
                    }
                }

                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('-- Error --.');</script>");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Send Invite Button Click Event
        protected void btnSendInvite_Click(object sender, EventArgs e)
        {
            try
            {
                int temp = CheckInternet();
                if (temp == 1)
                {
                    int count = 0;
                    int SendMailCount = 0;
                    foreach (GridViewRow row in gvParticipant.Rows)
                    {
                        if ((row.FindControl("chkSendInvite") as CheckBox).Checked && (row.FindControl("chkSendInvite") as CheckBox).Enabled.Equals(false))
                        {
                            count++;
                        }
                    }

                    if (count < gvParticipant.Rows.Count)
                    {
                        foreach (GridViewRow row in gvParticipant.Rows)
                        {
                            CheckBox status = (row.FindControl("chkSendInvite") as CheckBox);
                            string strEmail = row.Cells[3].Text;
                            string strName = row.Cells[1].Text;
                            int intMeetingID = Convert.ToInt32(ViewState["MeetingID"]);

                            if (status.Checked && status.Enabled.Equals(true))
                            {
                                UpdateRow(strEmail, 1, 1, intMeetingID);
                                sendMail_Invitation(strEmail, strName, 1);
                                SendMailCount++;
                            }
                            else if (status.Checked && status.Enabled.Equals(false))
                            {
                                UpdateRow(strEmail, 1, 1, intMeetingID);
                            }
                            else
                            {
                                UpdateRow(strEmail, 0, 1, intMeetingID);
                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Already Send Invitation.');</script>");
                    }

                    if (SendMailCount > 0)
                    {

                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Invitation Send successfully.');</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select for Invitation');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Check Internet Connection');</script>");
                }
                
            }
            catch (System.Net.WebException ex)            
            {
                //throw ex;
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Check Internet Connection');</script>");
            }
        }

        #endregion

        #region Update Table Row of Meeting Participant Table for Invite, Send MOM and Abset
        public void UpdateRow(string strEmail, int markStatus, int intSatusID, int intMeetingID)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                MeetingsBL objMeetingBL = new MeetingsBL();
                Controls objControls = new Controls();

                objResult = objMeetingBL.MeetingParticipant_UpdateRow(strEmail, markStatus, intSatusID, intMeetingID);

                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    BindgvParticipant();
                }

                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Invitation Send Fail.');</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Send Email
        private void sendMail_Invitation(string strEmail, string strName, int intMail)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("testgsspl@gmail.com", "Gsspl@12345");
                smtp.EnableSsl = true;
                MailMessage msg = new MailMessage();
                if (intMail == 1)
                {
                    msg.Subject = txtTopic.Text;
                    msg.Body = "Dear " + strName + "," + "\n\n\tYou have Invited for " + txtTopic.Text + " Meeting" + "\n\tDate: " + txtDate.Text + "\n\tTime: " + txtToTime.Text + " To " + txtToTime.Text +
                        "\n\tVenue: " + txtVenue.Text;
                    string toAddress = strEmail;
                    msg.To.Add(toAddress);
                    string formAddress = "G-EIMS <testgsspl@gmail.com>";
                    msg.From = new MailAddress(formAddress);
                }
                else if (intMail == 2)
                {
                    msg.Subject = txtTopic.Text;
                    msg.Body = "Dear " + strName + "," + "\n\n\tPlease Check Attachment for MOM";

                    string toAddress = strEmail;
                    msg.To.Add(toAddress);
                    string formAddress = "G-EIMS <testgsspl@gmail.com>";
                    msg.From = new MailAddress(formAddress);
                }



                smtp.Send(msg);
                //smtp.SendAsync(msg,null);
                //smtp.SendMailAsync(msg);
                //Response.Write("Email Sent to " + strEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Send MOM Button Click Event
        protected void btnSendMOM_Click(object sender, EventArgs e)
        {

            try
            {
                int temp = CheckInternet();
                if (temp == 1)
                {
                    generateMOM();

                    int count = 0;
                    int SendMailCount = 0;
                    int countMOM = 0;

                    foreach (GridViewRow row in gvParticipant.Rows)
                    {
                        if ((row.FindControl("chkSendMom") as CheckBox).Checked && (row.FindControl("chkSendMom") as CheckBox).Enabled.Equals(false))
                        {
                            count++;
                        }
                    }

                    if (count < gvParticipant.Rows.Count)
                    {
                        foreach (GridViewRow row in gvParticipant.Rows)
                        {
                            CheckBox status = (row.FindControl("chkSendMom") as CheckBox);
                            string strEmail = row.Cells[3].Text;
                            string strName = row.Cells[1].Text;
                            int intMeetingID = Convert.ToInt32(ViewState["MeetingID"]);

                            if (status.Checked && status.Enabled.Equals(true))
                            {

                                generatePDF(strEmail, strName, countMOM);
                                UpdateRow(strEmail, 1, 2, intMeetingID);
                                countMOM++;
                                SendMailCount++;
                            }
                            else if (status.Checked && status.Enabled.Equals(false))
                            {
                                UpdateRow(strEmail, 1, 2, intMeetingID);
                            }
                            else
                            {
                                UpdateRow(strEmail, 0, 2, intMeetingID);
                            }

                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Already Send MOM.');</script>");
                    }

                    if (SendMailCount > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('MOM Send successfully.');</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Plese Select for Send MOM');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Check Internet Connection');</script>");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Generate MOM for Send MOM Report
        public void generateMOM()
        {
            try
            {

                ApplicationResult objResult = new ApplicationResult();
                MeetingsBL objMeetingsBL = new MeetingsBL();


                objResult = objMeetingsBL.ScheduleMeeting_Report(Convert.ToInt32(ViewState["MeetingID"]));

                if (objResult.resultDT.Rows.Count > 0)
                {
                    bindAgenda(Convert.ToInt32(ViewState["MeetingID"]));

                    bindParticipant(Convert.ToInt32(ViewState["MeetingID"]));

                    bindMeetingPoint(Convert.ToInt32(ViewState["MeetingID"]));


                    lblTopic.Text = objResult.resultDT.Rows[0]["Topic"].ToString();
                    lblDate.Text = objResult.resultDT.Rows[0]["MeetingDate"].ToString();
                    lblFromTime.Text = objResult.resultDT.Rows[0]["FromTime"].ToString();
                    lblToTime.Text = objResult.resultDT.Rows[0]["ToTime"].ToString();
                    lblPlace.Text = objResult.resultDT.Rows[0]["Venue"].ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Generate PDF for Send MOM

        public void generatePDF(string strEmail, string strName, int countMOM)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string filename = "MOM of " + txtTopic.Text + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);


                StringWriter sw = new StringWriter();
                StringWriter sw1 = new StringWriter();
                StringWriter sw2 = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                HtmlTextWriter hw2 = new HtmlTextWriter(sw2);


                gvReportAgenda.AllowPaging = false;
                gvReportAgenda.GridLines = GridLines.Both;
                gvReportAgenda.RenderControl(hw);

                gvReportPaticipant.AllowPaging = false;
                gvReportPaticipant.GridLines = GridLines.Both;
                gvReportPaticipant.RenderControl(hw1);

                gvReportPoint.AllowPaging = false;
                gvReportPoint.GridLines = GridLines.Both;
                gvReportPoint.RenderControl(hw2);

                string strPath = Server.MapPath("~/Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg");
                string content = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<img height='70px' width='70px' src='" + strPath + "'/> </div> " +
                    "<div align='center' style='font-family:verdana;font-size:16px;margin-top:10px'> " +
                     "<div style='font-size:16px;font-weight:bold;color:Black; margin-top:10px'>" + "MINUTES OF MEETING" + "</div><br/>" +

                    "<div>______________________________________________________________</div><br/>" +
                    "<div align='left' style='font-size:12px;color:Black; margin-top:10px'>" + "Topic : " + lblTopic.Text + "</div><br/>" +
                    "<div align='left' style='font-size:12px;color:Black; margin-top:10px'>" + "Date : " + lblDate.Text + "</div>" +
                    "<div align='right' style='font-size:12px;color:Black; margin-top:10px'>" + "Time From : " + lblFromTime.Text + "</div>" +
                    "<div align='left' style='font-size:12px;color:Black; margin-top:10px'>" + "Place : " + lblPlace.Text + "</div>" +
                    "<div align='right' style='font-size:12px;color:Black; margin-top:10px'>" + "Time To : " + lblToTime.Text + "</div><br/><br/>" +
                    "<div align='left' style='font-size:14px;color:Black;margin-top:30px;margin-bottom:10px;text-decoration:underline'>Agenda</div><br/>"
                                  + sw.ToString() + "<br/></div>";

                string content1 = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                    "<div align='left' style='font-size:14px;color:Black;margin-top:30px;margin-bottom:10px;text-decoration:underline'>Participant</div><br/>"
                                 + sw1.ToString() + "<br/></div>";

                string content2 = "<div align='Left' style='font-family:Bold;font-size:16px;margin-top:10px;'>" +
                   "<div align='left' style='font-size:14px;color:Black;margin-top:30px;margin-bottom:10px;text-decoration:underline'>Meeting Point</div><br/>"
                                + sw2.ToString() + "<br/></div>";

                StringReader sr = new StringReader(content);
                StringReader sr1 = new StringReader(content1);
                StringReader sr2 = new StringReader(content2);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4);
                pdfDoc.SetMargins(20f, 20f, 0f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                htmlparser.Parse(sr1);
                htmlparser.Parse(sr2);
                writer.CloseStream = false;
                pdfDoc.Close();

                memoryStream.Position = 0;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("testgsspl@gmail.com", "Gsspl@12345");
                smtp.EnableSsl = true;
                MailMessage msg = new MailMessage();

                msg.Subject = txtTopic.Text;
                msg.Body = "Dear " + strName + "," + "\n\n\tPlease Check Attachment for MOM";
                msg.Attachments.Add(new Attachment(memoryStream, filename));
                string toAddress = strEmail;
                msg.To.Add(toAddress);
                string formAddress = "G-EIMS <testgsspl@gmail.com>";
                msg.From = new MailAddress(formAddress);

                try
                {
                    smtp.Send(msg);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                memoryStream.Dispose();
                if (countMOM == 0)
                {
                    Response.Write(pdfDoc);
                    gvReportAgenda.GridLines = GridLines.None;
                    gvReportPaticipant.GridLines = GridLines.None;
                    gvReportPoint.GridLines = GridLines.None;

                    //Response.End();
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //log.Error("Button PDF", ex);
                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Oops! There is some technical issue. Please Contact to your administrator.');", true);

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region Bind Agenda for Report
        public void bindAgenda(int intMeetingID)
        {
            ApplicationResult objResult = new ApplicationResult();
            MeetingsBL objMeetingsBL = new MeetingsBL();

            objResult = objMeetingsBL.ScheduleMeeting_FetchAgenda(intMeetingID);
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReportAgenda.DataSource = objResult.resultDT;
                gvReportAgenda.DataBind();
            }
        }
        #endregion

        #region Bind Participant for Report
        public void bindParticipant(int intMeetingID)
        {
            ApplicationResult objResult = new ApplicationResult();
            MeetingsBL objMeetingsBL = new MeetingsBL();

            objResult = objMeetingsBL.ScheduleMeeting_FetchParticipant(intMeetingID);
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReportPaticipant.DataSource = objResult.resultDT;
                gvReportPaticipant.DataBind();
            }
        }

        #endregion

        #region Bind Meeting Point for Report
        public void bindMeetingPoint(int intMeetingID)
        {
            ApplicationResult objResult = new ApplicationResult();
            MeetingsBL objMeetingsBL = new MeetingsBL();

            objResult = objMeetingsBL.SendMOM_bindMeetingPoint(intMeetingID);
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReportPoint.DataSource = objResult.resultDT;
                gvReportPoint.DataBind();
            }
        }

        #endregion

        #region Generate Serial Number for Grid view
        protected void gvReportPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrPoint") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }

        }

        protected void gvReportAgenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrAgenda") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void gvReportPaticipant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lblSrParticipant") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }
        #endregion

        #region Button absent update click event for update absent
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int SendMailCount = 0;

                foreach (GridViewRow row in gvParticipant.Rows)
                {
                    CheckBox status = (row.FindControl("chkAbsent") as CheckBox);
                    string strEmail = row.Cells[3].Text;
                    string strName = row.Cells[1].Text;
                    int intMeetingID = Convert.ToInt32(ViewState["MeetingID"]);

                    if (status.Checked.Equals(true))
                    {
                        UpdateRow(strEmail, 1, 3, intMeetingID);
                        SendMailCount++;
                    }
                    else
                    {
                        UpdateRow(strEmail, 0, 3, intMeetingID);
                    }

                }

                if (SendMailCount > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Absent Update successfully.');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Plese Select for Absent Update');</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Dropdown Month and Year Selected Index Change Event
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            intYear = Convert.ToInt32(ddlYear.SelectedValue);
            BindgvMetting(intMonth, intYear);
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            intYear = Convert.ToInt32(ddlYear.SelectedValue);
            BindgvMetting(intMonth, intYear);
        }
        #endregion

        #region Check Internet Connetion on or off
        protected int CheckInternet()
        {
            WebClient client = new WebClient();
            byte[] datasize = null;
            try
            {
                datasize = client.DownloadData("http://www.google.com");
            }
            catch (Exception ex)
            {
            }
            if (datasize != null && datasize.Length > 0) 
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }
        #endregion
    }
}