using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.PayRoll
{
    public partial class TrustPayItemTemplate : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TrustPayItemTemplate));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    ViewState["Mode"] = "Save";
                    //btnSave.Visible = false;
                    //BindGridView();
                    BindSchool();
                    //gvPayItem.Visible = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
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

                objResult = objSchoolBL.School_Select_SchoolMID(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");
                        ddlSchool.Items.Insert(0, new ListItem("-Select-", ""));
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

        //#region BindGrid
        //public void BindGridView()
        //{
        //    try
        //    {
        //        ApplicationResult objResult = new ApplicationResult();
        //        TrustTemplateBl objTrustTemplateBL = new TrustTemplateBl();

        //        objResult = objTrustTemplateBL.TrustTemplate_SelectAll_PayItemWithAsc(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
        //        if (objResult != null)
        //        {
        //            gvPayItem.DataSource = objResult.resultDT;
        //            gvPayItem.DataBind();
        //            //btnSave.Enabled = false;
        //            //if (objResult.resultDT.Rows.Count > 0)
        //            //{
        //            //    gvPayItem.Visible = true;

        //            //    ApplicationResult objTrustTemplateResult = new ApplicationResult();
        //            //    TrustPayItemBl objTrustPayItemBl = new TrustPayItemBl();
        //            //    objTrustTemplateResult = objTrustPayItemBl.TrustPayItem_SelectAll_AscOrder(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
        //            //    foreach (GridViewRow row in gvPayItem.Rows)
        //            //    {

        //            //        if (objTrustTemplateResult.resultDT.Rows.Count > 0)
        //            //        {
        //            //            int i;
        //            //            for (i = 0; i < objTrustTemplateResult.resultDT.Rows.Count; i++)
        //            //            {
        //            //                if (Convert.ToInt32(row.Cells[0].Text) == Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][TrustPayItemBo.TRUSTPAYITEM_PAYITEMID].ToString()))
        //            //                {
        //            //                    ((CheckBox)row.FindControl("CheckBoxPayItem")).Checked = true;
        //            //                    btnSave.Enabled = true;
        //            //                }
        //            //            }
        //            //        }
        //            //    }
        //            //}
        //        }
        //        //btnSave.Visible = false;
        //        //btnCancel.Visible = true;               
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error", ex);
        //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
        //    }
        //}
        //#endregion

        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TrustPayItemBo objTrustPayItemBO = new TrustPayItemBo();
                TrustPayItemBl objTrustPayItemBl = new TrustPayItemBl();
                ApplicationResult objTrustTemplateResult = new ApplicationResult();

                //Cascade Validation
                ApplicationResult objResults = new ApplicationResult();
               

                int SchoolMID = Convert.ToInt32(ddlSchool.SelectedValue.ToString());
                int TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                //Chage Date : 15-11-2021 Arpit Shah
                //Changes : Trust and School wise
                //Display all Deduction or Earning to assing school
                //objTrustTemplateResult = objTrustPayItemBl.TrustPayItem_SelectAll_AscOrder(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                objTrustTemplateResult = objTrustPayItemBl.TrustPayItem_SelectAll_AscOrder(TrustMID, SchoolMID);

                #region RollBack Transaction Starts
                DatabaseTransaction.OpenConnectionTransation();
                ApplicationResult objResultsDelete = new ApplicationResult();

                if (objTrustTemplateResult.resultDT.Rows.Count > 0)
                {
                    for (int i = 0; i < objTrustTemplateResult.resultDT.Rows.Count; i++)
                    {
                        int intTrustPayItemID = Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][0]);
                        int intPayItemID = Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][3]);
                        int intIsDeleted = Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][4]);

                        //objResults = objTrustPayItemBl.TrustPayItemTemplate_Delete_Cascade(intTrustPayItemID, intPayItemID, intIsDeleted);
                        objResults = objTrustPayItemBl.TrustPayItemTemplate_Delete_Cascade(intTrustPayItemID, intPayItemID);
                        DataTable dtResult1 = objResults.resultDT;
                        if (objResults != null)
                        {
                            int col = Convert.ToInt32(dtResult1.Rows[0][0]);
                            if (col == 0)
                            {
                                objResultsDelete = objTrustPayItemBl.TrustPayItem_Delete(Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][TrustPayItemBo.TRUSTPAYITEM_TRUSTPAYITEMID].ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                            }
                            else
                            {
                                //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Check box Checked Item is used to Designantion PI Template after click Save Button.');</script>");
                            }
                        }

                        //Old Code
                        //objResultsDelete = objTrustPayItemBl.TrustPayItem_Delete(Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][TrustPayItemBo.TRUSTPAYITEM_TRUSTPAYITEMID].ToString()), Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());
                    }
                }
                foreach (GridViewRow row in gvPayItem.Rows)
                {
                    //17/10/2022 Bhandavi Inserting into [tbl_TrustPayItem_M] table if checked (saving even already existed in table)
                    //Need to check in table if exists(for checked checkbox) then do not insert
                    //and delete from table if unchecked and if exists in the table
                   
                    if (((CheckBox)row.FindControl("CheckBoxPayItem")).Checked)
                    {
                      
                        //int SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                        objTrustPayItemBO.SchoolMID = Convert.ToInt32(ddlSchool.SelectedValue); 
                        objTrustPayItemBO.PayItemID = Convert.ToInt32(row.Cells[0].Text);
                        objTrustPayItemBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objTrustPayItemBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        ApplicationResult objResultsInsert = new ApplicationResult();
                        objTrustPayItemBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objTrustPayItemBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objTrustPayItemBO.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                        //check if already exists in tbl_TrustPayItem_M table with selected parameters                        
                        int payItemCount = objTrustPayItemBl.CheckTrustPayItem(Convert.ToInt32(Session[ApplicationSession.TRUSTID]),
                                         Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(row.Cells[0].Text));

                        if(payItemCount == 0)
                            objResultsInsert = objTrustPayItemBl.TrustPayItem_Insert(objTrustPayItemBO);

                        if (objResultsInsert != null)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('School PayItem Successfully Saved.');</script>");
                           
                        }

                    }

                    //if unchecked check if exists in tbl_TrustPayItem_M table with selected parameters, if used then do not delete 
                    //delete if not used in tbl_PaySlipUserPayItem_T table for unselected payitem
                    else
                    {
                        //int payItemCount = objTrustPayItemBl.CheckTrustPayItem(Convert.ToInt32(Session[ApplicationSession.TRUSTID]),
                        //                Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(row.Cells[0].Text));                     

                        //if (payItemCount > 0)
                        //{
                        //    objResultsDelete = objTrustPayItemBl.TrustPayItem_DeleteN
                        //        (Convert.ToInt32(Convert.ToInt32(row.Cells[0].Text).ToString()),TrustMID,SchoolMID, Convert.ToInt32(Session[ApplicationSession.USERID]), DateTime.UtcNow.AddHours(5.5).ToString());

                        //}
                        //if (objResultsDelete != null)
                        //{
                        //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('School PayItem Successfully Saved.');</script>");
                        //}
                    }

                }
                DatabaseTransaction.CommitTransation();
                #endregion

                //BindGridView();
                BindSchool();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BindClass();", true);
                //divLoading.Visible = false;
                //Response.Redirect("Class_Template.aspx");
                gvPayItem.Visible = false;
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void lnkViewList_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

        }

        #region School Index Changed
        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchool.SelectedIndex != 0)
            {
                try
                {
                    gvPayItem.Visible = true;
                    //int SchoolID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    int SchoolMID = Convert.ToInt32(ddlSchool.SelectedValue.ToString());
                    int TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);

                    ApplicationResult objResult = new ApplicationResult();
                    TrustTemplateBl objTrustTemplateBL = new TrustTemplateBl();

                    objResult = objTrustTemplateBL.TrustTemplate_SelectAll_PayItemWithAsc(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                    if (objResult != null)
                    {
                        gvPayItem.DataSource = objResult.resultDT;
                        gvPayItem.DataBind();
                        //btnSave.Enabled = true;
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            gvPayItem.Visible = true;

                            ApplicationResult objTrustTemplateResult = new ApplicationResult();
                            TrustPayItemBl objTrustPayItemBl = new TrustPayItemBl();
                            objTrustTemplateResult = objTrustPayItemBl.TrustPayItem_SelectAll_AscOrder(TrustMID,SchoolMID);
                            foreach (GridViewRow row in gvPayItem.Rows)
                            {
                                if (objTrustTemplateResult.resultDT.Rows.Count > 0)
                                {
                                    int i;
                                    for (i = 0; i < objTrustTemplateResult.resultDT.Rows.Count; i++)
                                    {
                                        if (Convert.ToInt32(row.Cells[0].Text) == Convert.ToInt32(objTrustTemplateResult.resultDT.Rows[i][TrustPayItemBo.TRUSTPAYITEM_PAYITEMID].ToString()))
                                        {
                                            ((CheckBox)row.FindControl("CheckBoxPayItem")).Checked = true;
                                            //btnSave.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //btnSave.Visible = false;
                    //btnCancel.Visible = true; 
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
            else
            {
                gvPayItem.Visible = false;
            }          
        }
        #endregion
    }
}