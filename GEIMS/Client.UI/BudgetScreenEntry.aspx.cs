using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class BudgetScreenEntry : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClassMaster));
        decimal sum = 0, val = 0;
        int sec = 0;
       
        string dt = "";
        string FAYEAR = "";

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                try
                {
                    BindBudgetCategory();
                    bindDataList();
                    BindAcademicYear();
                    bindgrid();
                    
                    //dt = Request.QueryString["Parameter"].ToString();
                    //dt = Server.UrlDecode(Request.QueryString["Parameter"].ToString());
                    ViewState["Mode"] = "Save";
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        //#region BindAcademicYear
        //public void BindAcademicYear()
        //{
        //    try
        //    {
        //        #region Fetch Academic Month from School
        //        SchoolBL objSchoolBl = new SchoolBL();
        //        ApplicationResult objResults = new ApplicationResult();
        //        int intMonth = 4;
        //        string strFromDate = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();

        //        //string strToDate = Session[ApplicationSession.ACCOUNTTODATE].ToString();
        //        //string strYear = Session[ApplicationSession.FINANCIALYEAR].ToString();
        //        DateTime FromDate = Convert.ToDateTime(strFromDate);
        //        //DateTime ToDate = Convert.ToDateTime(strToDate);

        //        //objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

        //        //if (objResults != null)
        //        //{
        //        //    if (objResults.resultDT.Rows.Count > 0)
        //        //    {

        //        //        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
        //        //    }

        //        //}
        //        #endregion

        //        Controls objControls = new Controls();
        //        //int month = System.DateTime.Now.Month;
        //        int month = FromDate.Month;
        //        //int Year = System.DateTime.Now.Year;
        //        int Year = FromDate.Year;
        //        int lastTwoDigit = Year % 100;
        //        string yr = string.Empty;
        //        if (month >= intMonth)
        //            yr = (lastTwoDigit.ToString() + (lastTwoDigit + 1).ToString()).ToString();
        //        else
        //            yr = ((lastTwoDigit - 1).ToString() + lastTwoDigit.ToString()).ToString();

        //        int f = (Convert.ToInt32(yr.Substring(0, 2)));
        //        int l = (Convert.ToInt32(yr.Substring(2, 2)));

        //        DataTable dt = new DataTable();
        //        DataRow dr = null;
        //        dt.Columns.Add(new DataColumn("AcademicYear", typeof(string)));



        //        for (int i = 0; i < 5; i++)
        //        {
        //            dr = dt.NewRow();
        //            if (i == 0)
        //            {
        //                dr["AcademicYear"] = "20"+Convert.ToString(f.ToString() + "-" + l.ToString());
        //                dt.Rows.Add(dr);
        //            }
        //            else
        //            {
        //                if ((f - 1).ToString().Length < 2)
        //                {
        //                    if (f.ToString().Length == 2)
        //                    {
        //                        dr["AcademicYear"] = "20"+Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
        //                    }
        //                    else
        //                    {
        //                        dr["AcademicYear"] = "20"+Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
        //                    }
        //                    dt.Rows.Add(dr);
        //                }
        //                else
        //                {
        //                    dr["AcademicYear"] = "20"+Convert.ToString((f - 1).ToString() + "-" + (f).ToString());
        //                    dt.Rows.Add(dr);
        //                }
        //                f = f - 1;
        //                l = f;
        //            }
        //        }
        //        //objControls.BindDropDown_ListBox(dt, ddlAcademicYear, "AcademicYear", "AcademicYear");
        //        //ddlAcademicYear.Items.Insert(0, new ListItem("-Select-", "-1"));   
        //        txtAcademicYear.Text = dt.Rows[0][0].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('First Login form Accounting Menu Side');</script>");
        //    }
        //}
        //#endregion

        #region BindAcademicYear
        public void BindAcademicYear()
        {
            try
            {
                #region Fetch Academic Month from School
                SchoolBL objSchoolBl = new SchoolBL();
                ApplicationResult objResults = new ApplicationResult();
                int intMonth = 4;
                string strFromDate = Session[ApplicationSession.ACCOUNTFROMDATE].ToString();

                //string strToDate = Session[ApplicationSession.ACCOUNTTODATE].ToString();
                //string strYear = Session[ApplicationSession.FINANCIALYEAR].ToString();
                DateTime FromDate = Convert.ToDateTime(strFromDate);
                //DateTime ToDate = Convert.ToDateTime(strToDate);

                //objResults = objSchoolBl.School_Select(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                //if (objResults != null)
                //{
                //    if (objResults.resultDT.Rows.Count > 0)
                //    {

                //        intMonth = Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString());
                //    }

                //}
                #endregion

                Controls objControls = new Controls();
                //int month = System.DateTime.Now.Month;
                int month = FromDate.Month;
                //int Year = System.DateTime.Now.Year;
                int Year = FromDate.Year;
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
                        //dr["AcademicYear"] = "20" + Convert.ToString(f.ToString() + "-" + l.ToString());
                        dr["AcademicYear"] = Convert.ToString(f.ToString() + "-" + l.ToString());
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if ((f - 1).ToString().Length < 2)
                        {
                            if (f.ToString().Length == 2)
                            {
                                //dr["AcademicYear"] = "20" + Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                            }
                            else
                            {
                                //dr["AcademicYear"] = "20" + Convert.ToString("0" + (f - 1).ToString() + "-" + "0" + (f).ToString());
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
                txtAcademicYear.Text = dt.Rows[0][0].ToString();
                lblDuration.Text = dt.Rows[0][0].ToString();

                /*Split FA YEAR and set Pattern eg. "21-22" to set "2122"*/
                string FAYEAR1 = dt.Rows[0][0].ToString();
                string[] FAList = FAYEAR1.Split(new Char[] { '-' });
                string FAYEARNEW = "";               
                foreach (string fas in FAList)
                {                  
                    FAYEARNEW += fas.ToString();                   
                }
                FAYEAR = FAYEARNEW.ToString();
                hfAcademicYear.Value = FAYEAR.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Login form Accounting Menu in Login Screen');</script>");
                Response.Redirect("../Accounting/AccountLogin.aspx?");
            }
        }
        #endregion

        #region Save Budget Entry Scrren 
        protected void btnTotal_Click(object sender, EventArgs e)
        {
            try
            {
                //txtAcademicYear.Text = FAYEAR.ToString();
                BudgetEntryScreenMBO objBudgetEntryScreenMBO = new BudgetEntryScreenMBO();
                BudgetEntryScreenMBL objBudgetEntryScreenMBL = new BudgetEntryScreenMBL();

                BudgetEntryScreenTBO objBudgetEntryScreenTBO = new BudgetEntryScreenTBO();
                BudgetEntryScreenTBL objBudgetEntryScreenTBL = new BudgetEntryScreenTBL();

                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();

                ApplicationResult objResult1 = new ApplicationResult();
                DataTable dt1 = new DataTable();

                int intBudgetScreenId = 0;

                objBudgetEntryScreenMBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objBudgetEntryScreenMBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                objBudgetEntryScreenMBO.BudgetCategoryMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                int intcatMID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                objBudgetEntryScreenMBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                int intheadMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                objBudgetEntryScreenMBO.BudgetSubHeadingMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                int intsubheadMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);

                //objBudgetEntryScreenMBO.AdminGeneralMGTRole = Convert.ToDecimal(txtAdminMGT.Text.ToString());
                objBudgetEntryScreenMBO.AdminGeneralMGTRole = 0;
                txtAdminMGT.Text = "0";
                //Total
                foreach (DataListItem item in dlSection.Items)
                {
                    sum = sum + Convert.ToDecimal(((TextBox)item.FindControl("txtSectionName")).Text);
                }
                sum = sum + Convert.ToDecimal(txtAdminMGT.Text);
                objBudgetEntryScreenMBO.TotalAmount = sum;
                //objBudgetEntryScreenMBO.CurrentYear = Convert.ToString(txtAcademicYear.Text.ToString());
                objBudgetEntryScreenMBO.CurrentYear = Convert.ToString(hfAcademicYear.Value);
                objBudgetEntryScreenMBO.IsDeleted = 0;

                //Code For Validate SubHeadingM Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intBudgetScreenId = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intBudgetScreenId = Convert.ToInt32(ViewState["BudgetScreenId"].ToString());
                }
                objResult = objBudgetEntryScreenMBL.BudgetEntrySubHeadingM_ValidateName(intBudgetScreenId, intSchoolMID, intTrustMID, intcatMID, intheadMID, intsubheadMID, Convert.ToString(hfAcademicYear.Value));
                if (objResult != null)
                {
                    dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Already Exist.');</script>");
                        clear();
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            //Insert tbl_BudgetEntryScreen_M
                            if(txtAcademicYear.Text != "")
                            {
                                if (ddlBudgetCategory.SelectedIndex != 0 || ddlBudgetHeading.SelectedIndex != 0 || ddlBudgetSubHeading.SelectedIndex != 0)
                                {
                                    objBudgetEntryScreenMBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                    objBudgetEntryScreenMBO.CreatedDate = DateTime.UtcNow.AddHours(5.5);
                                    objResult = objBudgetEntryScreenMBL.BudgetEntryScreenM_Insert(objBudgetEntryScreenMBO);
                                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    {
                                        //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Save successfully.');</script>");
                                        //bindgrid();
                                        //getTotalSum();
                                        //Find Get BudgetScreenId 
                                        ApplicationResult ObjResult2 = new ApplicationResult();
                                        Controls objControls = new Controls();
                                        ObjResult2 = objBudgetEntryScreenMBL.BudgetScreenId_Last();
                                        DataRow Dr = ObjResult2.resultDT.NewRow();
                                        if (ObjResult2.resultDT.Rows.Count > 0)
                                        {
                                            //Insert tbl_BudgetEntryScreen_T
                                            int BudgetScreenId = Convert.ToInt32(ObjResult2.resultDT.Rows[0][0].ToString()); //BudgetScreenId LastId
                                            foreach (DataListItem item in dlSection.Items)
                                            {
                                                sec = Convert.ToInt32(((HiddenField)item.FindControl("hfSectionMID")).Value);
                                                val = Convert.ToDecimal(((TextBox)item.FindControl("txtSectionName")).Text);
                                                objBudgetEntryScreenTBO.BudgetScreenId = BudgetScreenId;
                                                objBudgetEntryScreenTBO.SectionMID = sec;
                                                objBudgetEntryScreenTBO.BudgetSectionAmount = val;
                                                objBudgetEntryScreenTBO.IsDeleted = 0;
                                                objResult = objBudgetEntryScreenTBL.BudgetEntryScreenT_Insert(objBudgetEntryScreenTBO);
                                            }
                                        }
                                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Save Successfully.');</script>");               
                                            clear();
                                            ClearAll();
                                            BindAcademicYear();
                                            bindgrid();
                                            //getTotalSum();
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not Save Successfully.');</script>");
                                        clear();
                                        ClearAll();
                                        BindAcademicYear();
                                        bindgrid();
                                        //getTotalSum();
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select All DropDown List.');</script>");
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Get Accounting Financial Year.');</script>");
                            }                            
                        }
                        else if (ViewState["Mode"].ToString() == "Edit")
                        {
                            objBudgetEntryScreenMBO.BudgetScreenId = Convert.ToInt32(intBudgetScreenId);
                            //Update tbl_BudgetEntryScreen_M
                            objBudgetEntryScreenMBO.ModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objBudgetEntryScreenMBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
                            objResult = objBudgetEntryScreenMBL.BudgetEntryScreenM_Update(objBudgetEntryScreenMBO);

                            //Update tbl_BudgetEntryScreen_T
                            objResult1 = objBudgetEntryScreenTBL.BudgetScreen_T_SelectById(Convert.ToInt32(ViewState["BudgetScreenId"].ToString()));
                            if (objResult1 != null)
                            {
                                DataTable dtResult1 = objResult1.resultDT;
                                foreach (DataListItem item in dlSection.Items)
                                {
                                    objBudgetEntryScreenTBO.BudgetScreenId = Convert.ToInt32(intBudgetScreenId);
                                    sec = Convert.ToInt32(((HiddenField)item.FindControl("hfSectionMID")).Value);
                                    val = Convert.ToDecimal(((TextBox)item.FindControl("txtSectionName")).Text);
                                    objBudgetEntryScreenTBO.SectionMID = sec;
                                    objBudgetEntryScreenTBO.BudgetSectionAmount = val;
                                    objResult = objBudgetEntryScreenTBL.BudgetEntryScreenT_Update(objBudgetEntryScreenTBO);
                                }
                            }
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Update successfully.');</script>");
                                BindAcademicYear();
                                bindgrid();
                                clear();
                                //getTotalSum();
                            }
                        }
                        //else
                        //{
                        //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select All DropDown List.');</script>");
                        //}

                        ClearAll();
                        BindAcademicYear();
                        bindgrid();
                        PanelGrid_VisibilityMode(1);
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

        #region PreRender
        protected void gvBudgetScreen_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvBudgetScreen.Rows.Count > 0)
                {
                    gvBudgetScreen.UseAccessibleHeader = true;
                    gvBudgetScreen.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator11.');</script>");
            }
        }
        #endregion

        #region Bind DropDown
        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetEntryScreenMBL ObjBudgetEntryScreenMBL = new BudgetEntryScreenMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetEntryScreenMBL.BudgetEntry_Heading_SelectDropDown();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                        ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));

                        //ddlBudgetCategory.Items[3].Attributes["disabled"] = "disabled";
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

        #region Bind Category Wise Heading
            protected void ddlBudgetCategory_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    if (ddlBudgetCategory.SelectedIndex != 0)
                    {
                        BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                        ApplicationResult objResult = new ApplicationResult();
                        Controls objControls = new Controls();

                        objResult = ObjBudgetSubHeadingMBL.BudgetHeading_SelectDropDownByCapId(Convert.ToInt32(ddlBudgetCategory.SelectedValue));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                                ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
                                //ddlBudgetCategory.Items[3].Attributes["disabled"] = "disabled";
                            }
                            else
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                                ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
                                ddlBudgetHeading.ClearSelection();
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

        #region Heading Wise SubHeading
            protected void ddlBudgetHeading_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    if (ddlBudgetHeading.SelectedIndex != 0)
                    {
                        BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                        ApplicationResult objResult = new ApplicationResult();
                        Controls objControls = new Controls();

                        int id = Convert.ToInt32(ddlBudgetHeading.SelectedValue);

                        if (id > 0)
                        {
                            objResult = ObjBudgetCapitalCostBL.BudgetSubHeading_SelectDropdown(id);
                            if (objResult != null)
                            {
                                if (objResult.resultDT.Rows.Count > 0)
                                {
                                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                                    ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
                                    //ddlBudgetCategory.Items[3].Attributes["disabled"] = "disabled";
                                }
                                else
                                {
                                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                                    ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
                                    ddlBudgetSubHeading.ClearSelection();
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
        #endregion

        #region Bind Grid
        public void bindgrid()
        {
            try
            {             
                BudgetEntryScreenMBL objBudgetEntryScreenMBL = new BudgetEntryScreenMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                int SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                string CurrentYear = FAYEAR.ToString();

                if (CurrentYear != "")
                {
                    objResult = objBudgetEntryScreenMBL.BudgetEntryScreen_SelectAll(SchoolMID, TrustMID, CurrentYear);
                    //objResult = objBudgetEntryScreenMBL.BudgetEntryScreen_SelectAll_Dynamic(SchoolMID, TrustMID);
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvBudgetScreen.EmptyDataText = "No Data Fetch";
                            gvBudgetScreen.DataSource = objResult.resultDT;
                            gvBudgetScreen.DataBind();
                            bindGridFooterTotal();
                            PanelGrid_VisibilityMode(1);
                        }
                        else
                        {
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Data Not Display First Login form Accounting Menu in Login Screen');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindGridViewFooterTotal
        public void bindGridFooterTotal()
        {
            try
            {
                //here add code for column total sum and show in footer  
                decimal ftotal = 0;
                gvBudgetScreen.FooterRow.Cells[0].Text = "Total";
                gvBudgetScreen.FooterRow.Cells[0].Font.Bold = true;

                ////Position Check
                //gvBudgetScreen.FooterRow.Cells[5].Text = "Total1";
                //gvBudgetScreen.FooterRow.Cells[6].Font.Bold = true;
                //gvBudgetScreen.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                BudgetEntryScreenMBL objBudgetEntryScreenMBL = new BudgetEntryScreenMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                int SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                string CurrentYear = FAYEAR.ToString();
                objResult = objBudgetEntryScreenMBL.BudgetEntryScreen_SelectAll(SchoolMID, TrustMID, CurrentYear);
                if (objResult != null)
                {
                    DataTable dt = objResult.resultDT;
                    for (int k = 4; k < dt.Columns.Count; k++)
                    {
                        ftotal = dt.AsEnumerable().Sum(row => row.Field<decimal>(dt.Columns[k].ToString()));
                        gvBudgetScreen.FooterRow.Cells[k+2].Text = ftotal.ToString();
                        gvBudgetScreen.FooterRow.Cells[k+2].Font.Bold = true;
                        gvBudgetScreen.FooterRow.Cells[k+2].HorizontalAlign = HorizontalAlign.Right;
                        gvBudgetScreen.FooterRow.BackColor = System.Drawing.Color.Black;
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

        #region Hides the first column in GridView AutoGenerateField
        protected void gvBudgetScreen_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //gvBudgetScreen.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Center; //Column [Edit] Align Center
            //gvBudgetScreen.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center; //Column [Delete] Align Center

            //Count All Cell regarding GridView auto Columns
            e.Row.Cells[2].Visible = false;   /* Hides the first column for [BudgetScreenId] */
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left; /* column [CategoryName] */

            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left; /* column [HeadingName] */
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left; /* column [SubHeadingName] */
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right; /* column [Higher Secondary English Medium] */
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right; /* column [Secondary English Section] */
        }
        #endregion

        #region Auto Column GridView Header Alignmnet Center
        protected void gvBudgetScreen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i < NumCells /*- 1*/; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
        #endregion

        #region Bind DataList
        public void bindDataList()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SectionBL objSectionBL = new SectionBL();

                objResult = objSectionBL.Section_SelectAll_ForDropDown(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    dlSection.DataSource = objResult.resultDT;
                    dlSection.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        //PanelGrid_VisibilityMode(1);
                        

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

        #region Add New Button Click Event
        protected void lnkAddNewClass_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                ddlBudgetCategory.Enabled = true;
                ddlBudgetHeading.Enabled = true;
                ddlBudgetSubHeading.Enabled = true;
                BindAcademicYear();
                //ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
                ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", ""));
                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", ""));
                
                PanelGrid_VisibilityMode(2);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region View List Click Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                clear();
                PanelGrid_VisibilityMode(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion   

        #region Edit / Delete
        protected void gvBudgetScreen_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResult1 = new ApplicationResult();

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                BudgetEntryScreenMBL objBudgetEntryScreenMBL = new BudgetEntryScreenMBL();
                BudgetEntryScreenTBL objBudgetEntryScreenTBL = new BudgetEntryScreenTBL();
                BudgetEntryScreenTBO objBudgetEntryScreenTBO = new BudgetEntryScreenTBO();

                Controls objControls = new Controls();
                Controls objControls1 = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["BudgetScreenId"] = e.CommandArgument.ToString();
                                    
                    objResult = objBudgetEntryScreenMBL.BudgetScreen_M_SelectById(Convert.ToInt32(ViewState["BudgetScreenId"].ToString()));

                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlBudgetCategory.Enabled = false;
                            ddlBudgetHeading.Enabled = false;
                            ddlBudgetSubHeading.Enabled = false;
                            
                            txtAcademicYear.Text = dtResult.Rows[0][BudgetEntryScreenMBO.BudgetEntryScreen_CURRENTYEAR].ToString();
                            ddlBudgetCategory.Text = dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID].ToString();

                            dt = FetchHeading(Convert.ToInt32(dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID]), Convert.ToInt32(dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETHEADINGMID]));
                            if (dt.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(dt, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                            }
                            dt1 = FetchSubHeading(Convert.ToInt32(dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID]), Convert.ToInt32(dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETHEADINGMID]), Convert.ToInt32(dtResult.Rows[0][BudgetSubHeadingMBO.BudgetSubHeading_BUDGETSUBHEADINGMID]));
                            if (dt1.Rows.Count > 0)
                            {
                                objControls1.BindDropDown_ListBox(dt1, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                            }
                            txtAdminMGT.Text = dtResult.Rows[0][BudgetEntryScreenMBO.BudgetAdminGeneralMGTRole_ADMINGENERALMGTROLE].ToString();

                            //Fetch BudgetEntryTBL [BudgetSectionAmount]
                            objResult1 = objBudgetEntryScreenTBL.BudgetScreen_T_SelectById(Convert.ToInt32(ViewState["BudgetScreenId"].ToString()));
                            if(objResult1 !=null)
                            {
                                DataTable dtResult1 = objResult1.resultDT;
                                foreach (DataListItem item in dlSection.Items)
                                {
                                    int id1 = sec = Convert.ToInt32(((HiddenField)item.FindControl("hfSectionMID")).Value);
                                   
                                    //Fetch Record BudgetScreenId and SectionMID Wise
                                    ApplicationResult objResultFetch = new ApplicationResult();
                                    objResultFetch = objBudgetEntryScreenTBL.BudgetScreen_T_Fetch(Convert.ToInt32(ViewState["BudgetScreenId"].ToString()),id1);
                                    
                                    DataTable dtResultFetch = objResultFetch.resultDT;

                                    Convert.ToInt32(((HiddenField)item.FindControl("hfSectionMID")).Value = id1.ToString());
                                    Convert.ToDecimal(((TextBox)item.FindControl("txtSectionName")).Text = dtResultFetch.Rows[0]["BudgetSectionAmount"].ToString());
                                }
                            }    
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    objResult = objBudgetEntryScreenMBL.BudgetEntry_M_Delete(id);
                    objResult1 = objBudgetEntryScreenMBL.BudgetEntry_T_Delete(id);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS && objResult1.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        //PanelGrid_VisibilityMode(1);
                        bindgrid();
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

        #region Fetch Heading data For DropDown 
        private DataTable FetchHeading(int intBudgetCategoryMID, int intBudgetHeadingMID)
        {
            //DataTable dtDivision = new DataTable();

            BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
            BudgetSubHeadingMBO objBudgetSubHeadingMBO = new BudgetSubHeadingMBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objBudgetSubHeadingMBL.BudgetHeading_SelectDropDownById(intBudgetCategoryMID, intBudgetHeadingMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion

        #region Fetch SubHeading data For DropDown 
        private DataTable FetchSubHeading(int intBudgetCategoryMID, int intBudgetHeadingMID, int intBudgetSubHeadingMID)
        {
            //DataTable dtDivision = new DataTable();

            BudgetSubHeadingMBL objBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
            BudgetSubHeadingMBO objBudgetSubHeadingMBO = new BudgetSubHeadingMBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objBudgetSubHeadingMBL.BudgetSubHeading_SelectDropDownById(intBudgetCategoryMID, intBudgetHeadingMID, intBudgetSubHeadingMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }
        #endregion
    
        #region PanelGrid_VisibilityMode
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intMode == 2)
            {

                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }
        #endregion PanelGrid_VisibilityMode

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["BudgetScreenId"] = null;
            ViewState["Mode"] = "Save";

        }
        #endregion

        #region Clear All Form Data
        public void clear()
        {
            ddlBudgetCategory.SelectedIndex = 0;
            ddlBudgetHeading.SelectedIndex = 0;
            ddlBudgetSubHeading.SelectedIndex = 0;
            txtAdminMGT.Text = "";
            dlSection.SelectedIndex = 0;

        }
        #endregion       
    }
}