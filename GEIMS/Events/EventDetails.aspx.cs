using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Configuration;
using Image = System.Drawing.Image;
using System.Globalization;

namespace GEIMS.Events

{
    public partial class EventDetails : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ScheduleEvents));
        int intMonth = DateTime.Today.Month;
        int intYear = DateTime.Today.Year;

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            gvEventDetails.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
            try
            {
                lnkAddNewClass.Visible = false;
                if (!IsPostBack)
                {
                    bindMothYear(ddlYear, ddlMonth);
                    ViewState["Mode"] = "Save";
                    BindEventDetails(intMonth, intYear);
                    tabs.Visible = false;
                    divImagesPnl.Visible = false;
                    lnkViewList.Visible = false;
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

        #region Button Save Click Event Update data
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {

                ScheduledEventBO objScheduledEventBO = new ScheduledEventBO();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();


                objScheduledEventBO.ScheduledEventID = Convert.ToInt32(ViewState["ScheduledEventID"]);
                objScheduledEventBO.EventDetailsDescription = txtEventDescription.Text;
                objResult = objScheduledEventBL.ScheduledEvetn_UpdateForEventDestailsDescription(objScheduledEventBO);
                if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record not updated.');</script>");
                }
                //Code to redirect to display events view list after saving
                BindEventDetails(intMonth, intYear);
                divImagesPnl.Visible = false;
                PanelVisibility(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Bind Images With DataList
        private void PopulateImage()
        {
            try
            {
                ApplicationResult objEventName = new ApplicationResult();
                EventDetailsBL objEventDetailsBL = new EventDetailsBL();

                objEventName = objEventDetailsBL.EventDetails_Images_Select(Convert.ToInt32(ViewState["ScheduledEventID"]));
                
                if (objEventName.resultDT.Rows.Count > 0)
                {
                    divImagesPnl.Visible = true;
                   
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
                                images1.Add(new MyImageFiles { FileName = objEventName.resultDT.Rows[i]["FileName"].ToString() });

                            }
                        }

                    }
                    DataList1.DataSource = images1;
                    DataList1.DataBind();
                }
                else
                {
                    divImagesPnl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Delete Images
        protected void imgDelete(object source, DataListCommandEventArgs e)
        {
            try
            {
                string filename = e.CommandArgument.ToString();
                File.Delete(Server.MapPath("~/EventImages/" + filename));
                File.Delete(Server.MapPath("~/Thumbnil/" + filename));
                FileInfo finfo = new FileInfo(filename);
                finfo.Delete();

                ApplicationResult objResult = new ApplicationResult();
                DataTable dt = new DataTable();
                EventDetailsBL objEventDetailsBL = new EventDetailsBL();
                EventDetailsBO objEventDetailsBO = new EventDetailsBO();


                objResult = objEventDetailsBL.EventDetails_Delete(filename);
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Image Delete Succefful.');</script>");
                }

                PopulateImage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region File upload control code
        protected void UploadFile(object sender, EventArgs e)
        {
            try
            {
                EventDetailsBO objEventDetailsBO = new EventDetailsBO();
                EventDetailsBL objEventDetailsBL = new EventDetailsBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                //int intEventDetailsID = 0;

                if (FileUpload1.HasFile == false)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "<script>alert('No File Select for Upload.')</script>", false);
                }
                else
                {
                    foreach (var httpPostedFile in FileUpload1.PostedFiles)
                    {
                        string Uploadfilename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                        string ext = Path.GetExtension(Uploadfilename);

                        if (ext.ToLower() != ".jpg"
                              && ext.ToLower() != ".png"
                              && ext.ToLower() != ".gif"
                              && ext.ToLower() != ".jpeg")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Only Images Format .jpg, .png, .gif, .jpeg allowed ');</script>");
                        }
                        else
                        {
                            Image img = Image.FromStream(FileUpload1.PostedFile.InputStream);
                            string uploadFilePath = DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                            string imageFile = uploadFilePath + ext;
                            string folderPath = Server.MapPath("~/EventImages/");
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            FileUpload1.PostedFile.SaveAs(Path.Combine(Server.MapPath("~/EventImages"), imageFile));
                            var ration = (double)100 / img.Height;
                            int imageHeight = (int)(img.Height * ration);
                            int imageWidth = (int)(img.Width * ration);

                            Image.GetThumbnailImageAbort dCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                            Image thumbnilImg = img.GetThumbnailImage(imageWidth, imageHeight, dCallback, IntPtr.Zero);

                            string folderPathThumbnil = Server.MapPath("~/Thumbnil/");
                            if (!Directory.Exists(folderPathThumbnil))
                            {
                                Directory.CreateDirectory(folderPathThumbnil);
                            }
                            thumbnilImg.Save(Path.Combine(Server.MapPath("~/Thumbnil"), imageFile), ImageFormat.Jpeg);
                            thumbnilImg.Dispose();

                            // code for insert data in table

                            objEventDetailsBO.ScheduledEventID = Convert.ToInt32(ddlEventName.SelectedValue);
                            objEventDetailsBO.ImageName = imageFile;

                            objResult = objEventDetailsBL.EventDetailsImages_Insert(objEventDetailsBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Image saved.');</script>");
                            }

                            PopulateImage();

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
        public bool ThumbnailCallback()
        {
            return false;
        }

        protected void gvEventDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Gridview row command for Edit and Delete
        protected void gvEventDetails_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    objResult = objScheduledEventBL.ScheduledEvent_Select(Convert.ToInt32(e.CommandArgument.ToString()));

                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {

                            dt = FetchEventName(Convert.ToInt32(dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SCHEDULEDEVENTID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlEventName, "EventName", "ScheduledEventID");
                            }
                            ddlEventName.SelectedValue = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SCHEDULEDEVENTID].ToString();

                            dt = FetchSection(Convert.ToInt32(dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SCHOOLMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlSectionName, "DepartmentNameENG", "DepartmentID");
                            }

                            ddlSectionName.SelectedValue = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_SECTIONMID].ToString();
                            txtFromDate.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATE].ToString();
                            ViewState["Date"] = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATE].ToString();
                            txtFromDateFromTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATEFROMTIME].ToString();
                            txtFromDateToTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTFROMDATETOTIME].ToString();
                            txtToDate.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTTODATE].ToString();
                            txtToDateFromTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTTODATEFROMTIME].ToString();
                            txtToDateToTime.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTTODATETOTIME].ToString();
                            txtEventDescription.Text = dtResult.Rows[0][ScheduledEventBO.SCHEDULEDEVENT_EVENTDETAILSDESCRIPTION].ToString();

                            PopulateImage();
                            PanelVisibility(2);

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
                divImagesPnl.Visible = false;
                BindEventDetails(intMonth, intYear);
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

        #region Add New Button Click Event
        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {
            try
            {
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

        #region Bind Event Details For GridView
        public void BindEventDetails(int intMonth, int intYear)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ScheduleEventsBL objScheduledEventBL = new ScheduleEventsBL();

                objResult = objScheduledEventBL.ScheduledEvent_Select_By_Trust_School(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), intMonth, intYear);
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvEventDetails.Visible = true;
                    btnUpload.Visible = true;
                    FileUpload1.Visible = true;
                    gvEventDetails.DataSource = objResult.resultDT;
                    gvEventDetails.DataBind();
                    PanelVisibility(1);
                }
                else
                {
                    FileUpload1.Visible = false;
                    btnUpload.Visible = false;
                    PanelVisibility(2);
                    gvEventDetails.Visible = false;
                   

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion  

        #region Clear ALL Method
        public void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";

        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intCode)
        {
            if (intCode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            //else if (intCode == 2 && ViewState["Mode"].ToString() == "Save")
            //{
            //    btnSaveClass.Text = "Save";
            //    divGrid.Visible = false;
            //    tabs.Visible = true;
            //    lnkViewList.Visible = true;
            //}
            else if (intCode == 2 && ViewState["Mode"].ToString() == "Edit")
            {
                btnSaveClass.Text = "Update";
                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
            else
            {
               
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = true;
            }
        }
        #endregion

        #region Fetch Section data For DropDown When click on Edit
        private DataTable FetchSection(int intSchoolMID)
        {

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

        #region Fetch EventName For DropDown When click on Edit
        private DataTable FetchEventName(int intScheduledEventID)
        {
            ScheduledEventBO objScheduledEventBO = new ScheduledEventBO();
            ScheduleEventsBL ObjScheduleEventsBL = new ScheduleEventsBL();
            ApplicationResult objResults = new ApplicationResult();

            objResults = ObjScheduleEventsBL.EventName_Select_ForDropDown(intScheduledEventID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Dropdown Month and Year Selected Index Change Event
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            intYear = Convert.ToInt32(ddlYear.SelectedValue);
            BindEventDetails(intMonth, intYear);
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            intYear = Convert.ToInt32(ddlYear.SelectedValue);
            BindEventDetails(intMonth, intYear);
        }
        #endregion
    }

    internal class MyImageFiles
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}