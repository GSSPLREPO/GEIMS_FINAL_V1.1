using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using GEIMS.DataAccess;


namespace GEIMS.Client.UI
{
    public partial class BankReconciliationForFees : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(FeeCollection));

        #region Declaration
        double TotalDiscount = 0;
        double TotalPaidAmount = 0;
        int initialInsert = 0;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
                GetSchoolName();

                gvBankReconciliation.Visible = false;
                btnSave.Visible = false;
            }
        }
        #endregion

        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Save Reocrd
            try
            {
                if (ddlSearchBySchool.SelectedIndex != 0 && ddlSearchBySection.SelectedIndex != 0 && ddlSearchByClass.SelectedIndex != 0 && ddlSearchByDivision.SelectedIndex != 0 && ddlSearchByYear.SelectedIndex != 0)
                {
                    ApplicationResult objResult1 = new ApplicationResult();
                    FeesCollectionBO objFeesCollectionBO = new FeesCollectionBO();
                    BankReconciliationForFeesBL objBankReconciliationForFeesBL = new BankReconciliationForFeesBL();
                    foreach (GridViewRow row in gvBankReconciliation.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkChild")).Checked)
                        {
                            objFeesCollectionBO.FeesCollectionMID = Convert.ToInt32(((HiddenField)row.FindControl("hfFeesCollectionMID")).Value);
                            objFeesCollectionBO.DateOfDeposit = Convert.ToDateTime(((TextBox)row.FindControl("txtdate")).Text);
                            objFeesCollectionBO.RChequeNo = Convert.ToString(((TextBox)row.FindControl("txtCheque")).Text);
                            objFeesCollectionBO.RBankName = Convert.ToString(((TextBox)row.FindControl("txtBankName")).Text);
                            objFeesCollectionBO.RBranchName = Convert.ToString(((TextBox)row.FindControl("txtBranchName")).Text);
                            objFeesCollectionBO.DateInBankStatement = Convert.ToDateTime(((TextBox)row.FindControl("txtdate1")).Text);

                            objResult1 = objBankReconciliationForFeesBL.BankReconciliation_Update(objFeesCollectionBO);
                        }
                        if (objResult1 != null)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Reconciliation Save Successfully.');</script>");
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

        #region Fetch School
        public void GetSchoolName()
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SchoolBL objSchoolBl = new SchoolBL();

                objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSearchBySchool, "SchoolNameEng", "SchoolMID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {


                    }
                    ddlSearchBySchool.Items.Insert(0, new ListItem("--Select--", ""));

                    //ddlSearchByYear.Items.Insert(0, new ListItem("-Select-", "-1"));
                    //ddlDivisionName.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region School wise Section
        protected void ddlSearchBySchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SectionBL objSectionBl = new SectionBL();

                objResult = objSectionBl.Section_SelectAll_ForDropDown(Convert.ToInt32(ddlSearchBySchool.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSearchBySection, "SectionName", "SectionMID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {


                    }
                    ddlSearchBySection.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Section Wise class
        protected void ddlSearchBySection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                ClassBL objClassBl = new ClassBL();

                objResult = objClassBl.Class_SelectAll_SectionWise_ForDropDown(Convert.ToInt32(ddlSearchBySection.SelectedValue), Convert.ToInt32(ddlSearchBySchool.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSearchByClass, "ClassName", "ClassMID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {


                    }
                    ddlSearchByClass.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Class wise Division
        protected void ddlSearchByClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                DivisionTBL objDivisionTbl = new DivisionTBL();

                objResult = objDivisionTbl.Division_SelectAll_ClassWise_ForDropDown(Convert.ToInt32(ddlSearchByClass.SelectedValue), Convert.ToInt32(ddlSearchBySchool.SelectedValue));
                if (objResult != null)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSearchByDivision, "DivisionName", "DivisionTID");
                    if (objResult.resultDT.Rows.Count > 0)
                    {

                    }
                    ddlSearchByDivision.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindYear
        public void BindYear()
        {
            try
            {

                int intMonth = 0;


                Controls objControls = new Controls();
                int month = System.DateTime.Now.Month;
                int Year = System.DateTime.Now.Year;
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
                        dr["AcademicYear"] = Convert.ToString(f.ToString() + "-" + l.ToString());
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if ((f - 1).ToString().Length < 2)
                        {
                            if (f.ToString().Length == 2)
                            {
                                dr["AcademicYear"] = Convert.ToString("0" + (f - 1).ToString() + "-" + (f).ToString());
                            }
                            else
                            {
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

                objControls.BindDropDown_ListBox(dt, ddlSearchByYear, "AcademicYear", "AcademicYear");
                ddlSearchByYear.Items.Insert(0, new ListItem("-Select-", ""));

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }




            //try
            //{
            //    int currentYear = DateTime.Now.Year;
            //    for (int i = 1990; i <= currentYear; i++)
            //    {
            //        ddlYear.Items.Add(Convert.ToString(i));

            //    }
            //    ddlYear.Items.Insert(0, "Select");
            //}
            //catch (Exception ex)
            //{
            //    logger.Error("Error", ex);
            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            //}

        }








        #endregion

        #region Search Record
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Search 
            try
            {
                if (ddlSearchBySchool.SelectedIndex != 0 && ddlSearchBySection.SelectedIndex != 0 && ddlSearchByClass.SelectedIndex != 0 && ddlSearchByDivision.SelectedIndex != 0 && ddlSearchByYear.SelectedIndex != 0)
                {
                    BankReconciliationForFeesBL objBankReconciliationForFeesBL = new BankReconciliationForFeesBL();
                    ApplicationResult objResults = new ApplicationResult();

                    int SchoolID = Convert.ToInt32(ddlSearchBySchool.SelectedValue);
                    int ClassID = Convert.ToInt32(ddlSearchByClass.SelectedValue);
                    int DivisionID = Convert.ToInt32(ddlSearchByDivision.SelectedValue);
                    string YearName = ddlSearchByYear.SelectedItem.ToString();

                    objResults = objBankReconciliationForFeesBL.BankReconcilition_FeesCollection_SearchingWise(SchoolID, ClassID, DivisionID, YearName);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            gvBankReconciliation.DataSource = objResults.resultDT;
                            gvBankReconciliation.DataBind();
                            gvBankReconciliation.Visible = true;
                            btnSave.Visible = true;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('No Record Found.');</script>");
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

    }
}