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
    public partial class BudgetAdjustmentScreen : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClassMaster));

        #region Declaration
        int SchoolID = 0;
        int CategoryID = 0;
        int HeadID = 0;
        int SubheadID = 0;
        int SectionMID = 0;
        int SchoolID1 = 0;
        int CategoryID1 = 0;
        int HeadID1 = 0;
        int SubheadID1 = 0;
        int SectionMID1 = 0;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindSchool();
                BindSection();
                BindBudgetCategory();
                //BindSection1();
                BindBudgetCategory1();
                ViewState["Mode"] = "Save";
            }
        }
        #endregion

        #region Save
        protected void btnAdustment_Click(object sender, EventArgs e)
        {
            //Add Adjustment (Transfer)
            try
            {
                if(ddlSection.SelectedIndex !=0 && ddlBudgetCategory.SelectedIndex !=0 && ddlBudgetHeading.SelectedIndex != 0 && ddlBudgetSubHeading.SelectedIndex!=0 )
                {
                    if(ddlSection.SelectedIndex != 0 && ddlBudgetCategory1.SelectedIndex != 0 && ddlBudgetHeading1.SelectedIndex != 0 && ddlBudgetSubHeading1.SelectedIndex != 0)
                    {
                        BudgetEntryScreenTBO ObjBudgetEntryScreenTBO = new BudgetEntryScreenTBO();
                        BudgetEntryScreenTBL ObjBudgetEntryScreenTBL = new BudgetEntryScreenTBL();
                        ApplicationResult objResult = new ApplicationResult();
                        DataTable dt = new DataTable();
                        Controls objControls = new Controls();

                        int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                        CategoryID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                        HeadID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                        SubheadID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                        SectionMID = Convert.ToInt32(ddlSection.SelectedValue);

                        objResult = ObjBudgetEntryScreenTBL.BudgetEntry_FindId(intTrustMID, SchoolID, CategoryID, HeadID, SubheadID, SectionMID);
                        if (objResult != null)
                        {
                            BudgetEntryScreenTBO ObjBudgetEntryScreenTBO1 = new BudgetEntryScreenTBO();
                            BudgetEntryScreenTBL ObjBudgetEntryScreenTBL1 = new BudgetEntryScreenTBL();
                            ApplicationResult objResult1 = new ApplicationResult();
                            DataTable dtResult = objResult.resultDT;
                            if (dtResult.Rows.Count > 0)
                            {
                                int BudgetScreenTID = Convert.ToInt32(dtResult.Rows[0]["BudgetScreenTID"]);
                                decimal BudgetSectionAmount = Convert.ToDecimal(dtResult.Rows[0]["BudgetSectionAmount"]);
                                //hfAvailableAmount.Value = Convert.ToString(BudgetSectionAmount);
                                decimal BudgetAdjustAmt = Convert.ToDecimal(txtAdjustmentAmt.Text);

                                if (BudgetAdjustAmt > 0)
                                {
                                    if (BudgetSectionAmount >= BudgetAdjustAmt)
                                    {
                                        //Find
                                        int intTrustMID1 = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                                        SchoolID1 = Convert.ToInt32(ddlSchool.SelectedValue);
                                        CategoryID1 = Convert.ToInt32(ddlBudgetCategory1.SelectedValue);
                                        HeadID1 = Convert.ToInt32(ddlBudgetHeading1.SelectedValue);
                                        SubheadID1 = Convert.ToInt32(ddlBudgetSubHeading1.SelectedValue);
                                        SectionMID1 = Convert.ToInt32(ddlSection.SelectedValue);

                                        objResult1 = ObjBudgetEntryScreenTBL.BudgetEntry_FindId(intTrustMID1, SchoolID1, CategoryID1, HeadID1, SubheadID1, SectionMID1);
                                        if (objResult1 != null)
                                        {
                                            DataTable dtResult1 = objResult1.resultDT;
                                            if (dtResult1.Rows.Count > 0)
                                            {
                                                //if (SectionMID != SectionMID1)
                                                if (SubheadID != SubheadID1)
                                                {
                                                    int BudgetScreenTID1 = Convert.ToInt32(dtResult1.Rows[0]["BudgetScreenTID"]);
                                                    decimal BudgetSectionAmount1 = Convert.ToDecimal(dtResult1.Rows[0]["BudgetSectionAmount"]);

                                                    decimal tamt = BudgetSectionAmount - BudgetAdjustAmt;
                                                    decimal tamt1 = BudgetSectionAmount1 + BudgetAdjustAmt;

                                                    //From Update
                                                    ObjBudgetEntryScreenTBO.BudgetScreenTId = BudgetScreenTID;
                                                    ObjBudgetEntryScreenTBO.BudgetSectionAmount = tamt;

                                                    //To Update
                                                    ObjBudgetEntryScreenTBO1.BudgetScreenTId = BudgetScreenTID1;
                                                    ObjBudgetEntryScreenTBO1.BudgetSectionAmount = tamt1;

                                                    try
                                                    {

                                                        objResult = ObjBudgetEntryScreenTBL.BudgetEntryScreenFromT_Update(ObjBudgetEntryScreenTBO);
                                                        objResult1 = ObjBudgetEntryScreenTBL1.BudgetEntryScreenToT_Update(ObjBudgetEntryScreenTBO1);
                                                        if (objResult1.status == ApplicationResult.CommonStatusType.SUCCESS)
                                                        {
                                                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Amount Transfer successfully.');</script>");
                                                            ClearAll();
                                                            clear();
                                                            Response.AppendHeader("Refresh", "0");
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        logger.Error("Error", ex);
                                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                                                    }
                                                }
                                                else
                                                {
                                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Same SubHeading Amount Not Transfer!');</script>");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Insufficient  Budget Amount!');</script>");
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Put Budget Amount!');</script>");
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Budget Amount Not Fetch!');</script>");
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select All DropDown Transfer To.');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select All DropDown Transfer From.');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind School
        private void BindSchool()
        {
            try
            {                
                SchoolBL objSchoolBL = new SchoolBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = objSchoolBL.School_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");
                        ddlSchool.Items.Insert(0, new ListItem("-Select-", "0"));
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
                        ddlSection.Items.Insert(0, new ListItem("-Select-", "0"));                     
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

        #region Bind School wise Section
        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SchoolID = Convert.ToInt32(ddlSchool.SelectedValue.ToString());

            try
            {
                ApplicationResult objResult = new ApplicationResult();
                SectionBL objSectionBL = new SectionBL();
                Controls objControls = new Controls();

                objResult = objSectionBL.Section_SelectAll(SchoolID);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");
                        ddlSection.Items.Insert(0, new ListItem("-Select-", "0"));
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

        //#region Bind Budget Category
        //public void BindBudgetCategory()
        //{
        //    try
        //    {
        //        BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
        //        ApplicationResult objResult = new ApplicationResult();
        //        Controls objControls = new Controls();

        //        objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
        //        if (objResult != null)
        //        {
        //            if (objResult.resultDT.Rows.Count > 0)
        //            {
        //                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
        //                ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", ""));                      
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        //#endregion

        #region Bind Budget Category
        public void BindBudgetCategory()
        {
            try
            {
                BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();

                objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory, "CategoryName", "BudgetCategoryMID");
                        ddlBudgetCategory.Items.Insert(0, new ListItem("-Select-", "0"));
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
                            ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            ddlBudgetSubHeading.ClearSelection();
                            //txtUnutilizedBudget.Text = "0";
                        }
                        else
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading, "HeadingName", "BudgetHeadingMID");
                            //ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            //ddlBudgetHeading.ClearSelection();
                            ddlBudgetHeading.ClearSelection();
                            ddlBudgetSubHeading.ClearSelection();
                            //txtUnutilizedBudget.Text = "0";
                        }
                    }
                }
                else if (ddlBudgetCategory.SelectedValue == "0")
                {
                    ddlBudgetHeading.ClearSelection();
                    ddlBudgetSubHeading.ClearSelection();
                    //txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Heading wise Subheading
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
                                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                                ddlBudgetSubHeading.ClearSelection();
                                //txtUnutilizedBudget.Text = "0";
                            }
                            else
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading, "SubHeadingName", "BudgetSubHeadingMID");
                                ddlBudgetSubHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                                ddlBudgetSubHeading.ClearSelection();
                                //txtUnutilizedBudget.Text = "0";
                            }
                        }
                    }
                    else
                    {
                        ddlBudgetSubHeading.ClearSelection();
                        //txtUnutilizedBudget.Text = "0";
                    }
                }
                else
                {
                    ddlBudgetSubHeading.ClearSelection();
                    //txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Available Budget Amount
        protected void ddlBudgetSubHeading_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(ddlSchool.SelectedIndex!=0 && ddlSection.SelectedIndex!=0)
                {
                    BudgetEntryScreenTBL ObjBudgetEntryScreenTBL = new BudgetEntryScreenTBL();
                    ApplicationResult objResult = new ApplicationResult();
                    DataTable dt = new DataTable();
                    Controls objControls = new Controls();



                    int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    //int intSchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    int SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                    int CategoryID = Convert.ToInt32(ddlBudgetCategory.SelectedValue);
                    int HeadID = Convert.ToInt32(ddlBudgetHeading.SelectedValue);
                    int SubheadID = Convert.ToInt32(ddlBudgetSubHeading.SelectedValue);
                    int SectionMID = Convert.ToInt32(ddlSection.SelectedValue);

                    objResult = ObjBudgetEntryScreenTBL.BudgetEntry_FindId(intTrustMID, SchoolID, CategoryID, HeadID, SubheadID, SectionMID);
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtAvailableAmount.Text = Convert.ToString(dtResult.Rows[0]["BudgetSectionAmount"]);

                            decimal no1, no2;
                            no1 = Convert.ToDecimal(dtResult.Rows[0]["BudgetSectionAmount"]);
                            no2 = Convert.ToDecimal(txtAdjustmentAmt.Text);
                            txtTotalAmount.Text = Convert.ToString(no1 - no2);
                        }
                        else
                        {
                            txtAvailableAmount.Text = "0.00";
                            txtAdjustmentAmt.Text = "0.00";
                            txtTotalAmount.Text = "0.00";
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Fund Not Available!!!!!.');</script>");
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

        #region Find Total Amount and Total Amount1
        protected void txtAdjustmentAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {          
                txtAdjustmentAmt1.Text = txtAdjustmentAmt.Text;
                //Find Total Amount = Adjustment Amount (substract) to Availalbe Amount
                decimal no1, no2, ans;
                no1 = Convert.ToDecimal(txtAvailableAmount.Text);
                no2 = Convert.ToDecimal(txtAdjustmentAmt.Text);
                ans = (no1 - no2);
                txtTotalAmount.Text = Convert.ToString(ans);

                //Find Total Amount1 = Adjustment Amount1 (addition) to Availalbe Amount1
                decimal no11, no21, ans1;
                no11 = Convert.ToDecimal(txtAvailableAmount1.Text);
                no21 = Convert.ToDecimal(txtAdjustmentAmt1.Text);
                ans1 = (no11 + no21);
                txtTotalAmount1.Text = Convert.ToString(ans1);
            }
            catch(Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        //#region Bind Section1
        //public void BindSection1()
        //{
        //    try
        //    {
        //        SectionBL objSectionBL = new SectionBL();
        //        ApplicationResult objResult = new ApplicationResult();
        //        Controls objControls = new Controls();

        //        objResult = objSectionBL.Section_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
        //        if (objResult != null)
        //        {
        //            if (objResult.resultDT.Rows.Count > 0)
        //            {
        //                objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection1, "SectionName", "SectionMID");
        //                ddlSection1.Items.Insert(0, new ListItem("-Select-", ""));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        //#endregion

        //#region Bind Budget Category1
        //public void BindBudgetCategory1()
        //{
        //    try
        //    {
        //        BudgetCategoryMBL ObjBudgetHeadingSelectDropDownBL = new BudgetCategoryMBL();
        //        ApplicationResult objResult = new ApplicationResult();
        //        Controls objControls = new Controls();

        //        objResult = ObjBudgetHeadingSelectDropDownBL.BudgetHeading_SelectDropDown();
        //        if (objResult != null)
        //        {
        //            if (objResult.resultDT.Rows.Count > 0)
        //            {                     
        //                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory1, "CategoryName", "BudgetCategoryMID");
        //                ddlBudgetCategory1.Items.Insert(0, new ListItem("-Select-", ""));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        //#endregion

        #region Bind Budget Category1
        public void BindBudgetCategory1()
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
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetCategory1, "CategoryName", "BudgetCategoryMID");
                        ddlBudgetCategory1.Items.Insert(0, new ListItem("-Select-", "0"));

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

        #region Bind Category Wise Heading1
        protected void ddlBudgetCategory1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (ddlBudgetCategory1.SelectedIndex != 0)
                {
                    BudgetSubHeadingMBL ObjBudgetSubHeadingMBL = new BudgetSubHeadingMBL();
                    ApplicationResult objResult = new ApplicationResult();
                    Controls objControls = new Controls();

                    objResult = ObjBudgetSubHeadingMBL.BudgetHeading_SelectDropDownByCapId(Convert.ToInt32(ddlBudgetCategory1.SelectedValue));
                    if (objResult != null)
                    {
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading1, "HeadingName", "BudgetHeadingMID");
                            ddlBudgetHeading1.Items.Insert(0, new ListItem("-Select-", "0"));
                            ddlBudgetSubHeading1.ClearSelection();
                            //txtUnutilizedBudget.Text = "0";
                        }
                        else
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetHeading1, "HeadingName", "BudgetHeadingMID");
                            //ddlBudgetHeading.Items.Insert(0, new ListItem("-Select-", "0"));
                            //ddlBudgetHeading.ClearSelection();
                            ddlBudgetHeading1.ClearSelection();
                            ddlBudgetSubHeading1.ClearSelection();
                            //txtUnutilizedBudget.Text = "0";
                        }
                    }
                }
                else if (ddlBudgetCategory1.SelectedValue == "0")
                {
                    ddlBudgetHeading1.ClearSelection();
                    ddlBudgetSubHeading1.ClearSelection();
                    //txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Heading Wise Subheading1
        protected void ddlBudgetHeading1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBudgetHeading1.SelectedIndex != 0)
                {
                    BudgetCapitalCostBL ObjBudgetCapitalCostBL = new BudgetCapitalCostBL();
                    ApplicationResult objResult = new ApplicationResult();
                    Controls objControls = new Controls();

                    int id = Convert.ToInt32(ddlBudgetHeading1.SelectedValue);

                    if (id > 0)
                    {
                        objResult = ObjBudgetCapitalCostBL.BudgetSubHeading_SelectDropdown(id);
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading1, "SubHeadingName", "BudgetSubHeadingMID");
                                ddlBudgetSubHeading1.Items.Insert(0, new ListItem("-Select-", "0"));
                                ddlBudgetSubHeading1.ClearSelection();
                                //txtUnutilizedBudget.Text = "0";
                            }
                            else
                            {
                                objControls.BindDropDown_ListBox(objResult.resultDT, ddlBudgetSubHeading1, "SubHeadingName", "BudgetSubHeadingMID");
                                ddlBudgetSubHeading1.Items.Insert(0, new ListItem("-Select-", "0"));
                                ddlBudgetSubHeading1.ClearSelection();
                                //txtUnutilizedBudget.Text = "0";
                            }
                        }
                    }
                    else
                    {
                        ddlBudgetSubHeading1.ClearSelection();
                        //txtUnutilizedBudget.Text = "0";
                    }
                }
                else
                {
                    ddlBudgetSubHeading1.ClearSelection();
                    //txtUnutilizedBudget.Text = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Available Budget Amount1
        protected void ddlBudgetSubHeading1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSchool.SelectedIndex != 0 && ddlSection.SelectedIndex != 0)
                {
                    BudgetEntryScreenTBL ObjBudgetEntryScreenTBL = new BudgetEntryScreenTBL();
                    ApplicationResult objResult = new ApplicationResult();
                    DataTable dt = new DataTable();
                    Controls objControls = new Controls();

                    int intTrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    int SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                    int CategoryID = Convert.ToInt32(ddlBudgetCategory1.SelectedValue);
                    int HeadID = Convert.ToInt32(ddlBudgetHeading1.SelectedValue);
                    int SubheadID = Convert.ToInt32(ddlBudgetSubHeading1.SelectedValue);
                    int SectionMID = Convert.ToInt32(ddlSection.SelectedValue);

                    objResult = ObjBudgetEntryScreenTBL.BudgetEntry_FindId(intTrustMID, SchoolID, CategoryID, HeadID, SubheadID, SectionMID);
                    if (objResult != null)
                    {
                        DataTable dtResult = objResult.resultDT;
                        if (dtResult.Rows.Count > 0)
                        {
                            txtAvailableAmount1.Text = Convert.ToString(dtResult.Rows[0]["BudgetSectionAmount"]);
                            txtAdjustmentAmt1.Text = txtAdjustmentAmt.Text;
                            decimal no1, no2;
                            no1 = Convert.ToDecimal(dtResult.Rows[0]["BudgetSectionAmount"]);
                            no2 = Convert.ToDecimal(txtAdjustmentAmt1.Text);
                            txtTotalAmount1.Text = Convert.ToString(no1 + no2);
                        }
                        else
                        {
                            txtAvailableAmount1.Text = "0.00";
                            txtAdjustmentAmt1.Text = "0.00";
                            txtTotalAmount1.Text = "0.00";
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Fund Not Available!!!!!.');</script>");
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
            //SchoolID = 0;
            //CategoryID = 0;
            //HeadID = 0;
            //SubheadID = 0;
            //SectionMID = 0;
            //SchoolID1 = 0;
            //CategoryID1 = 0;
            //HeadID1 = 0;
            //SubheadID1 = 0;
            //SectionMID1 = 0;
        }
        #endregion
    }
}