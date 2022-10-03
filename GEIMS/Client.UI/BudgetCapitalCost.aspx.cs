using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Web.UI;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class BudgetCapitalCost1 : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClassMaster));

        #region Declaration
        int ms; 
        decimal p, tot;
        int id = 0;
        string FAYEAR = "";
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindAcademicYear();
                    BindSection();
                    BindBudgetCategory();
                    bindHeading();
                    BindBudgetUOM();
                    bindgrid();
                    getTotalSum();
                   
                    ViewState["Mode"] = "Save";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

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

                /*Split FA YEAR and set Pattern eg. "21-22" to set "2122" this value store in database*/
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

        #region Save
        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            //SAVE
            try
            {
                BudgetCapitalCostBO objBudgetCapitalCostBO = new BudgetCapitalCostBO();
                BudgetCapitalCostBL objBudgetCapitalCostBL = new BudgetCapitalCostBL();
                ApplicationResult objResult = new ApplicationResult();
                DataTable dtResult = new DataTable();
                int intCapitalCostId = 0;

                objBudgetCapitalCostBO.SectionMID = Convert.ToInt32(ddlSection.SelectedValue);  
                int intSectionMID = Convert.ToInt32(ddlSection.SelectedValue);
                objBudgetCapitalCostBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                objBudgetCapitalCostBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                objBudgetCapitalCostBO.BudgetCategoryMID = Convert.ToInt32(hfBudgetCategory.Value);
                int catMID = Convert.ToInt32(hfBudgetCategory.Value);
                objBudgetCapitalCostBO.BudgetHeadingMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                int headMID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                objBudgetCapitalCostBO.BudgetSubHeadingMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                int subheadMID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);

                objBudgetCapitalCostBO.Quantity = Convert.ToInt32(txtQty.Text.ToString());
                objBudgetCapitalCostBO.UOMID = Convert.ToInt32(ddlUOM.SelectedValue);
                objBudgetCapitalCostBO.RatePerUnit = Convert.ToDecimal(txtRate.Text.ToString());
                objBudgetCapitalCostBO.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text.ToString());
                //objBudgetCapitalCostBO.CurrentYear = Convert.ToString(txtAcademicYear.Text.ToString());
                objBudgetCapitalCostBO.CurrentYear = Convert.ToString(hfAcademicYear.Value);

                objBudgetCapitalCostBO.IsDeleted = 0;

                //Code For Validate SubHeadingM Name
                if (ViewState["Mode"].ToString() == "Save")
                {
                    intCapitalCostId = -1;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    intCapitalCostId = Convert.ToInt32(ViewState["CapitalCostId"].ToString());
                }

                objResult = objBudgetCapitalCostBL.BudgetCapitalCostSubHeadingM_ValidateName(intCapitalCostId, intTrustMID, intSchoolMID, intSectionMID, catMID, headMID, subheadMID, Convert.ToString(hfAcademicYear.Value));

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
                            if(txtAcademicYear.Text != "" )
                            {
                                //if (ddlBudgetCategory != null && ddlBudgetHeading != null && ddlBudgetSubHeading !=null && ddlUOM != null)                          
                                if (txtBudgetCategory.Text != "" && ddlBudgetHeading.SelectedIndex != 0 && ddlBudgetSubHeading.SelectedIndex != 0 && ddlUOM.SelectedIndex != 0)
                                {
                                    objBudgetCapitalCostBO.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                    objBudgetCapitalCostBO.CreatedDate = DateTime.UtcNow.AddHours(5.5);
                                    objResult = objBudgetCapitalCostBL.BudgetCapitalCost_Insert(objBudgetCapitalCostBO);
                                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Save Successfully.');</script>");                 
                                        ClearAll();
                                        BindAcademicYear();
                                        bindgrid();
                                        getTotalSum();
                                        PanelGrid_VisibilityMode(1);
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Not Save.');</script>");
                                        ClearAll();
                                        BindAcademicYear();
                                        bindgrid();
                                        getTotalSum();
                                        PanelGrid_VisibilityMode(1);
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
                            objBudgetCapitalCostBO.CapitalCostId = Convert.ToInt32(intCapitalCostId);

                            objBudgetCapitalCostBO.ModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objBudgetCapitalCostBO.ModifiedDate = DateTime.UtcNow.AddHours(5.5);

                            objResult = objBudgetCapitalCostBL.BudgetCapitalCost_Update(objBudgetCapitalCostBO);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
                                ClearAll();
                                BindAcademicYear();
                                bindgrid();
                                getTotalSum();
                                PanelGrid_VisibilityMode(1);
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

        #region PreRender
        protected void gvBudgetCapitalCost_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvBudgetCapitalCost.Rows.Count > 0)
                {
                    gvBudgetCapitalCost.UseAccessibleHeader = true;
                    gvBudgetCapitalCost.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator11.');</script>");
            }
        }
        #endregion

        #region Add New Button Click Event
        protected void lnkAddNewClass_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                ClearAll();
                BindAcademicYear();
                BindBudgetCategory();
                bindHeading();
                bindSubHeading();
                ddlSection.Enabled = true;
                txtBudgetCategory.Enabled = true;
                ddlBudgetHeading.Enabled = true;
                ddlBudgetSubHeading.Enabled = true;
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
                clear();
                ClearAll();
                PanelGrid_VisibilityMode(1);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindGrid
        public void bindgrid()
        {
            try
            {
                BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                int TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                string CurrentYear = FAYEAR.ToString();

                //objResult = ObjBudgetCapitalCostBL.BudgetCapital_SelectAll();
                objResult = ObjBudgetCapitalCostBL.BudgetCapital_SelectAll(SchoolMID, TrustMID, CurrentYear);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvBudgetCapitalCost.EmptyDataText = "No Data Fetch";
                        gvBudgetCapitalCost.DataSource = objResult.resultDT;
                        gvBudgetCapitalCost.DataBind();
                        PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        gvBudgetCapitalCost.EmptyDataText = "No Data Fetch";
                        PanelGrid_VisibilityMode(2);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        protected void ddlBudgetSubHeading_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Bind Section
        public void BindSection()
        {
            try
            {
                SectionBL objSectionBL = new SectionBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = objSectionBL.Section_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                        ddlSection.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DataTable dt = new DataTable();

                objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
                if (objResult != null)
                {
                    //if (objResult.resultDT.Rows.Count > 0)
                    //{
                    //    objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                    //    ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));
                    //}
                    DataTable dtResult = objResult.resultDT;
                    if (dtResult.Rows.Count > 0)
                    {
                        txtBudgetCategory.Text = dtResult.Rows[4][1].ToString();
                        id = Convert.ToInt32(dtResult.Rows[4][0].ToString());
                        hfBudgetCategory.Value = id.ToString();
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

        #region Category Wise Heading
        public void bindHeading()
        {
            try
            {
                BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetSubHeadingMBL.BudgetHeading_SelectDropDownByCapId(Convert.ToInt32(id));

                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                        ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                    else
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                        ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                        ddlBudgetHeading.ClearSelection();
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

        #region SubHeading Bind
        public void bindSubHeading()
        {
            try
            {
                BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                int hid = Convert.ToInt32(ddlBudgetHeading.SelectedValue);

                if (hid > 0)
                {
                    objResult = ObjBudgetCapitalCostBL.BudgetSubHeading_SelectDropdown(hid);
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                            ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                        }
                        else
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                            ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            ddlBudgetSubHeading.ClearSelection();
                        }
                    }
                }
                else
                {
                    ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                    ddlBudgetSubHeading.ClearSelection();
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
            bindSubHeading();
        }
        #endregion

        #region Bind Budget UOM
        public void BindBudgetUOM()
        {
            try
            {
                BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetCapitalCostBL.BudgetUOM_DropDown();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlUOM, "UOMName", "UOMID");
                        ddlUOM.Items.Insert(0, new ListItem("-Select-", "0"));
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

        #region Gridview Column Total
        public void getTotalSum()
        {
            try
            {
                double tamount = 0.0;
                Label TotalAmount = new Label();

                if (gvBudgetCapitalCost.Rows.Count > 0)
                {
                    for (int i = 0; i < gvBudgetCapitalCost.Rows.Count; i++)
                    {

                        TotalAmount = (Label)(gvBudgetCapitalCost.Rows[i].Cells[1].FindControl("TotalAmount"));
                        if (TotalAmount.Text.Length > 0)
                        {
                            tamount += Convert.ToDouble(TotalAmount.Text.ToString());                 
                        }
                    }
                    ((System.Web.UI.HtmlControls.HtmlInputText)(gvBudgetCapitalCost.FooterRow.Cells[1].FindControl("txtTotalSum"))).Value = tamount.ToString();
                }
                else
                {
                    bindgrid();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Rate * Quantity
        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQty.Text != "" && txtRate.Text != "")
                {
                    ms = Convert.ToInt32(txtQty.Text);
                    p = Convert.ToDecimal(txtRate.Text);
                    tot = ms * p;
                    txtTotalAmount.Text = tot.ToString();
                }
                else
                {
                    p = 0;
                    tot = ms * p;
                    txtTotalAmount.Text = tot.ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Error for Calculation(Rate * Quantity)!!!!!.');</script>");
            }
        }
        #endregion

        #region Quantity * Rate
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQty.Text != "" && txtRate.Text != "")
                {
                    ms = Convert.ToInt32(txtQty.Text);
                    p = Convert.ToDecimal(txtRate.Text);
                    tot = ms * p;
                    txtTotalAmount.Text = tot.ToString();
                }
                else
                {
                    ms = 0;
                    tot = ms * p;
                    txtTotalAmount.Text = tot.ToString();
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Error for Calculation(Quantity * Rate)!!!!!.');</script>");
            }
        }
        #endregion

        #region Edit / Delete Details for Capital Cost
        protected void gvBudgetCapitalCost_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResult1 = new ApplicationResult();

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                BudgetCapitalCostBO objBudgetCapitalCostBO = new BudgetCapitalCostBO();
                BudgetCapitalCostBL objBudgetCapitalCostBL = new BudgetCapitalCostBL();

                Controls objControls = new Controls();
                Controls objControls1 = new Controls();

                if (e.CommandName.ToString() == "Edit1")
                {
                    ViewState["Mode"] = "Edit";
                    ViewState["CapitalCostId"] = e.CommandArgument.ToString();

                    objResult = objBudgetCapitalCostBL.BudgetCapitalCost_M_SelectById(Convert.ToInt32(ViewState["CapitalCostId"].ToString()));

                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            ddlSection.Enabled = false;
                            txtBudgetCategory.Enabled = false;
                            ddlBudgetHeading.Enabled = false;
                            ddlBudgetSubHeading.Enabled = false;
                            
                            txtAcademicYear.Text = dtResult.Rows[0][BudgetCapitalCostBO.BudgetCapitalCost_CURRENTYEAR].ToString();
                            ddlSection.Text = dtResult.Rows[0][BudgetCapitalCostBO.BudgetSectionMID_SECTIONMID].ToString();
                            //txtBudgetCategory.Text = dtResult.Rows[0][BudgetSubHeadingMBO.BudgetHeading_BUDGETCATEGORYMID].ToString();

                            BindBudgetCategory();
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
                            txtQty.Text = dtResult.Rows[0]["Quantity"].ToString();
                            ddlUOM.Text = dtResult.Rows[0]["UOMID"].ToString();
                            txtRate.Text = dtResult.Rows[0]["RatePerUnit"].ToString();
                            txtTotalAmount.Text = dtResult.Rows[0]["TotalAmount"].ToString();
                            
                            PanelGrid_VisibilityMode(2);
                        }
                    }

                }
                else if (e.CommandName.ToString() == "Delete1")
                {
                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    objResult = objBudgetCapitalCostBL.BudgetCapitalCost_Delete(id);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record deleted successfully.');</script>");
                        PanelGrid_VisibilityMode(1);
                        ClearAll();
                        BindAcademicYear();
                        bindgrid();
                        getTotalSum();                      
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

        protected void gvBudgetCapitalCost_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
 
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
            ViewState["CapitalCostId"] = null;
            ViewState["Mode"] = "Save";
        }
      
        #endregion

        #region Clear All Form Data
        public void clear()
        {
            //ddlBudgetCategory.SelectedIndex = 0;
            ddlBudgetHeading.SelectedIndex = 0;
            ddlBudgetSubHeading.SelectedIndex = 0;
            ddlUOM.SelectedIndex = 0;
            txtQty.Text = "0";
            txtRate.Text ="0";
            txtTotalAmount.Text = "0";
        }
        #endregion
    }
}