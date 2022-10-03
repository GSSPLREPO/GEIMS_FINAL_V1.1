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

namespace GEIMS.PayRoll
{
    public partial class EmployeePayItem : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmployeePayItem));
        double str, str1, sum;

        #region PageLoad Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                ddlSearchBy.SelectedIndex = 1;
                BindTemplateName();
                PanelVisibility(false, false, false, false);
            }
        }
        #endregion

        #region GO button Event
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {
                EmployeeMBL objEmployeeBL = new EmployeeMBL();
                EmployeeMBO objEmployeeBO = new EmployeeMBO();

                ApplicationResult objResultProgram = new ApplicationResult();
                objResultProgram = objEmployeeBL.Employee_Search_By_NameAndCode(txtSearchName.Text, Convert.ToInt32(ddlSearchBy.SelectedValue), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResultProgram != null)
                {
                    if (objResultProgram.resultDT.Rows.Count > 0)
                    {
                        gvEmployee.Visible = true;
                        gvEmployee.DataSource = objResultProgram.resultDT;
                        gvEmployee.DataBind();                     
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('No Record Found.');</script>");
                        gvEmployee.Visible = false;
                    }
                }
                PanelVisibility(false, false, false, false);
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BindTemplateName
        private void BindTemplateName()
        {
            TrustTemplateBl ObjTrustTemplateBl = new TrustTemplateBl();
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();

            //objResult = ObjTrustTemplateBl.TrustTemplate_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            objResult = ObjTrustTemplateBl.TrustTemplate_Select_SchoolWise(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlTemplate, "TrustTemplateName", "TrustTemplateID");
                    ddlTemplate.Items.Insert(0, new ListItem("-Select-", ""));
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Please Applied Designation PI Template');</script>");
                }
            }
        }
        #endregion

        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            UserTemplateTBl objEmployeeMbl = new UserTemplateTBl();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.Employee_Select_AutocomleteForPayroll(strSearchText, TrustMID, SchoolMID);
            if (objResult != null)
            {
                for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                {
                    string strEmployeeCodeName = objResult.resultDT.Rows[i]["EmployeeName"].ToString();
                    string strEmployeeMID = objResult.resultDT.Rows[i][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                    result.Add(string.Format("{0}~{1}", strEmployeeCodeName, strEmployeeMID));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Employee Gridivew RowCommand Event
        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                UserTemplateTBl objUserTemplateBL = new UserTemplateTBl();
                UserPayItemTemplateTBl objUserPayItemBl = new UserPayItemTemplateTBl();
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                ViewState["EmployeeMID"] = commandArgs[0];
                ViewState["SchoolMID"] = commandArgs[1];

                //string a1 = commandArgs[0];
                //string a2 = commandArgs[1];

                if (e.CommandName.ToString() == "Edit1")
                {
                    objResult = objUserTemplateBL.Employee_Select_ForPayItemTemplate(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResult != null)
                    {
                        PanelVisibility(false, false, true, false);
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            int abc = Convert.ToInt32(objResult.resultDT.Rows[0]["Gross"].ToString());
                            ViewState["Gross"] = Convert.ToInt32(objResult.resultDT.Rows[0]["Gross"].ToString());

                            objResult = objUserPayItemBl.Employee_Select_ByEmployeeID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResult != null)
                            {
                                if (objResult.resultDT.Rows.Count > 0)
                                {
                                    PanelVisibility(false, true, true, false);
                                    btnApplyTemplate.Visible = false;
                                    ddlTemplate.Enabled = false;
                                    txtAnnual.Enabled = false;
                                    txtMonthly.Enabled = false;
                                    txtGross.Enabled = false;
                                    gvSelectedPayItem.DataSource = objResult.resultDT;
                                    gvSelectedPayItem.DataBind();

                                    gvSelectedPayItem.Visible = true;
                                    for (int i = 0; i < gvSelectedPayItem.Rows.Count; ++i)
                                    {
                                        sum = sum + Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[7].Text);
                                    }
                                   
                                    txtMonthly.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControls.ConvertToCurrancy(sum.ToString())), 2));
                                    txtAnnual.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControls.ConvertToCurrancy((12 * sum).ToString())), 2));
                                    txtGross.Text = Convert.ToString(ViewState["Gross"].ToString());
                                    int EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                                    objResult = objUserPayItemBl.EmployeeTemplate_Select_ByEmployeeID(EmployeeMID, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                                    if (objResult != null)
                                    {
                                        if (objResult.resultDT.Rows.Count > 0)
                                        {
                                            ddlTemplate.SelectedValue = objResult.resultDT.Rows[0]["TrustTemplateID"].ToString();
                                        }

                                    }
                                }
                            }

                            GridConvertion();
                        }
                        else
                        {
                            ViewState["Gross"] = txtGross.Text;
                            PanelVisibility(false, false, true, false);
                            ddlTemplate.Enabled = true;
                            txtAnnual.Enabled = false;
                            txtMonthly.Enabled = false;
                            txtGross.Enabled = true;
                            btnApplyTemplate.Visible = true;
                            ddlTemplate.SelectedIndex = -1;
                            txtGross.Text = "";
                            txtMonthly.Text = "";
                            txtAnnual.Text = "";
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

        #region Function GridConvertion
        public void GridConvertion()
        {
            for (int i = 0; i < gvSelectedPayItem.Rows.Count; ++i)
            {
                String.Format("{0:##,##0.00}", gvSelectedPayItem.Rows[i].Cells[7].Text).ToString();
                if (gvSelectedPayItem.Rows[i].Cells[3].Text.ToString() == "1")
                {
                    gvSelectedPayItem.Rows[i].Cells[3].Text = "Dependent";
                }
                else
                    if (gvSelectedPayItem.Rows[i].Cells[3].Text.ToString() == "0")
                    {
                        gvSelectedPayItem.Rows[i].Cells[3].Text = "Independent";
                    }
                    else
                        if (gvSelectedPayItem.Rows[i].Cells[3].Text.ToString() == "2")
                        {
                            gvSelectedPayItem.Rows[i].Cells[3].Text = "Depends On Gross";
                        }
                        else
                            if (gvSelectedPayItem.Rows[i].Cells[3].Text.ToString() == "&nbsp;")
                            {
                                gvSelectedPayItem.Rows[i].Cells[3].Text = "";
                            }
            }
        }
        #endregion

        #region button ApplyTemplate Event
        protected void btnApplyTemplate_Click(object sender, EventArgs e)
        {
            if (ddlTemplate.SelectedIndex != 0)
            {
                if (Page.IsValid == true)
                {
                    double BasicAmt = 0;
                    double Earnings = 0;
                    double DA = 0;


                    //if (txtGross.Text == "")   
                    //{
                    //    lblmsg.text = "please enter the gross amount";
                    //    lblmsg.visible = true;
                    //}
                    UserTemplateTBl objUserTemplateBl = new UserTemplateTBl();
                    UserTemplateTBo objUTemplateBo = new UserTemplateTBo();
                    ApplicationResult objResult = new ApplicationResult();
                    Controls objControls = new Controls();
                    try
                    {
                        objUTemplateBo.UserID = Convert.ToInt32(Convert.ToInt32(ViewState["EmployeeMID"].ToString()));
                        objUTemplateBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                        objUTemplateBo.SchoolMID = Convert.ToInt32(Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                        //This logic is created regarding whose employee id pass 0 value in table: tbl_UserTemplate_M, so its no assign template properly
                        //Arpit Shah
                        if (objUTemplateBo.SchoolMID == 0)
                        {
                            objUTemplateBo.SchoolMID = 3;
                        }
                        else
                        {
                            objUTemplateBo.SchoolMID = Convert.ToInt32(Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                        }
                        objUTemplateBo.TrustTemplateID = Convert.ToInt32(ddlTemplate.SelectedValue.ToString());
                        objUTemplateBo.Annual = 0; // Convert.ToDouble(txtAnnual.Text.Trim());
                        objUTemplateBo.Monthly = 0;// Convert.ToDouble(txtMonthly.Text.Trim());
                        objUTemplateBo.Gross = Convert.ToDouble(txtGross.Text == "" ? "0" : txtGross.Text.ToString());
                        objUTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objUTemplateBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objUTemplateBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objUTemplateBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objResult = objUserTemplateBl.UserTemplateT_Insert(objUTemplateBo); //insert this table "tbl_UserTemplate_M"

                        int i;
                        ViewState["UserTemplateID"] = objResult.resultDT.Rows[0][0].ToString();
                        UserPayItemTemplateTBl objUserPayItemTemplateBl = new UserPayItemTemplateTBl();
                        UserPayItemTemplateTBo objUserPayItemTemplateTBo = new UserPayItemTemplateTBo();

                        for (i = 0; i < gvPayItem.Rows.Count; ++i)
                        {
                            objUserPayItemTemplateTBo.UserTemplateID = Convert.ToInt32(ViewState["UserTemplateID"].ToString());
                            objUserPayItemTemplateTBo.PayItem = Convert.ToInt32(gvPayItem.Rows[i].Cells[0].Text);
                            objUserPayItemTemplateTBo.Type = gvPayItem.Rows[i].Cells[2].Text;
                            if (gvPayItem.Rows[i].Cells[5].Text == "&nbsp;")
                            {
                                objUserPayItemTemplateTBo.Percentage = 0;
                            }
                            else
                            {
                                objUserPayItemTemplateTBo.Percentage = Convert.ToDouble(gvPayItem.Rows[i].Cells[5].Text);
                            }
                            //objUserPayItemTemplateTBo.Percentage = Convert.ToDouble(gvPayItem.Rows[i].Cells[5].Text);
                            objUserPayItemTemplateTBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                            objUserPayItemTemplateTBo.SchoolMID = Convert.ToInt32(Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                            //This logic is created regarding whose employee id pass 0[Trust] in table: tbl_UserTemplate_M, so its not assign template properly
                            //Arpit Shah
                            if (objUserPayItemTemplateTBo.SchoolMID == 0)
                            {
                                objUserPayItemTemplateTBo.SchoolMID = 3;
                            }
                            else
                            {
                                objUserPayItemTemplateTBo.SchoolMID = Convert.ToInt32(Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                            }

                            if (gvPayItem.Rows[i].Cells[2].Text == "&nbsp;")
                            {
                                gvPayItem.Rows[i].Cells[2].Text = "";
                                objUserPayItemTemplateTBo.DependsOn = gvPayItem.Rows[i].Cells[3].Text.ToString();
                            }
                            else
                            {
                                objUserPayItemTemplateTBo.DependsOn = gvPayItem.Rows[i].Cells[3].Text.ToString();
                            }

                            #region Riken changes
                            String str;
                            String[] strPatItem;
                            if (gvPayItem.Rows[i].Cells[2].Text.ToString() == "Independent")
                            {
                                objUserPayItemTemplateTBo.Amount = Convert.ToDouble(gvPayItem.Rows[i].Cells[6].Text);
                            }
                            if (gvPayItem.Rows[i].Cells[2].Text.ToString() == "Depends On Gross")
                            {
                                //Note : 
                                //PayItemType depended on Gross is Calculated Based on Amount entered in 
                                //"Gross (Monthly) :" --- of "Employee Pay-Item Template" Screen.
                                //

                                //Note : Changes for new  Professional Tax slab as per kaushik sir mail on 03 June 2022 10:51
                                //Name : Yogesh Patel
                                //Date : 07-06-2022
                                double PTpercentage = Convert.ToDouble(gvPayItem.Rows[i].Cells[6].Text == "" ? "0" : gvPayItem.Rows[i].Cells[6].Text.ToString().Trim());   
                                double PTamt = Convert.ToDouble(gvPayItem.Rows[i].Cells[5].Text == "" ? "0" : gvPayItem.Rows[i].Cells[5].Text.ToString().Trim());
                                double GrossValue = Convert.ToDouble(txtGross.Text == "" ? "0" : txtGross.Text);

                                //Professional Tax Slab wise Calculation
                                if (PTpercentage == 0 && PTamt == 0)
                                {
                                    if (GrossValue < 12000)
                                    {
                                        BasicAmt = 0;
                                        objUserPayItemTemplateTBo.Amount = BasicAmt;
                                        gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    }
                                    //else if (GrossValue > 6000 && GrossValue <= 9000)
                                    //{
                                    //    BasicAmt = 80;
                                    //    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    //    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    //}
                                    //else if (GrossValue > 9000 && GrossValue <= 12000)
                                    //{
                                    //    BasicAmt = 150;
                                    //    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    //    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    //}
                                    else if (GrossValue >= 12000)
                                    {
                                        BasicAmt = 200;
                                        objUserPayItemTemplateTBo.Amount = BasicAmt;
                                        gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    }

                                    //if (GrossValue <= 6000)
                                    //{
                                    //    BasicAmt = 0;
                                    //    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    //    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    //}
                                    //else if (GrossValue > 6000 && GrossValue <= 9000)
                                    //{
                                    //    BasicAmt = 80;
                                    //    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    //    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    //}
                                    //else if (GrossValue > 9000 && GrossValue <= 12000)
                                    //{
                                    //    BasicAmt = 150;
                                    //    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    //    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    //}
                                    //else if (GrossValue > 12000)
                                    //{
                                    //    BasicAmt = 200;
                                    //    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    //    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                    //}
                                }
                                else
                                {
                                    BasicAmt = Convert.ToDouble(txtGross.Text == "" ? "0" : txtGross.Text) * Convert.ToDouble(gvPayItem.Rows[i].Cells[5].Text.ToString()) / 100;
                                    BasicAmt = Math.Round(BasicAmt);
                                    objUserPayItemTemplateTBo.Amount = BasicAmt;
                                    gvPayItem.Rows[i].Cells[6].Text = BasicAmt.ToString();
                                }
                            }
                            if (gvPayItem.Rows[i].Cells[2].Text.ToString() == "Dependent")
                            {
                                Double SumAmount = 0;
                                Double amt = 0;
                                str = gvPayItem.Rows[i].Cells[4].Text.ToString();
                                strPatItem = str.Split(',');
                                // for (int j = 0; j < strPatItem.Length; j++)
                                for (int j = 0; j < gvPayItem.Rows.Count; j++)
                                {
                                    for (int k = 0; k < strPatItem.Length; k++)
                                    {
                                        if (strPatItem[k] == gvPayItem.Rows[j].Cells[1].Text.ToString())
                                        {
                                            SumAmount = SumAmount + Convert.ToDouble(gvPayItem.Rows[j].Cells[6].Text.ToString());
                                        }
                                    }
                                }
                                amt = SumAmount * Convert.ToDouble(gvPayItem.Rows[i].Cells[5].Text.ToString()) / 100;
                                amt = Math.Round(amt);
                                //Testing : Arpit Shah
                                //double grossEarningValue = SumAmount + amt;
                                objUserPayItemTemplateTBo.Amount = amt;
                                gvPayItem.Rows[i].Cells[6].Text = amt.ToString();
                            }
                            #endregion

                            objUserPayItemTemplateTBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objUserPayItemTemplateTBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objUserPayItemTemplateTBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objUserPayItemTemplateTBo.CreatedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            //}
                            //objUserPayItemTemplateBO.Amount = Convert.ToDouble(gdPayItem.Rows[i].Cells[6].Text);
                            objResult = objUserPayItemTemplateBl.UserPayItemTemplateT_Insert(objUserPayItemTemplateTBo); //insert this table "tbl_UserPayItemTemplate_T"

                            // intResult = objUserPayItemTemplateBA.UserPayItemTemplate_Insert(objUserPayItemTemplateBO);
                        }

                        foreach (GridViewRow rowItem in gvPayItem.Rows)
                        {
                            if (rowItem.Cells[1].Text == "SA")
                            {
                                objResult = objUserTemplateBl.EmployeeTemplate_SelectForZero(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));

                                for (int a = 0; a < objResult.resultDT.Rows.Count; a++)
                                {
                                    Earnings = Earnings + (Convert.ToDouble(objResult.resultDT.Rows[a]["Amount"].ToString()));
                                }
                                objUserPayItemTemplateTBo.UserTemplateID = Convert.ToInt32(ViewState["UserTemplateID"].ToString());
                                objUserPayItemTemplateTBo.PayItem = 5;
                                objUserPayItemTemplateTBo.Type = "1";
                                objUserPayItemTemplateTBo.DependsOn = "0";
                                objUserPayItemTemplateTBo.Percentage = 0;
                                objUserPayItemTemplateTBo.Amount = Math.Round(Convert.ToDouble(txtGross.Text.ToString() == "" ? "0" : txtGross.Text.ToString()) - Earnings, 2);
                                objResult = objUserPayItemTemplateBl.UserPayItemTemplateT_Insert(objUserPayItemTemplateTBo); //inert tble "tbl_UserPayItemTemplate_T"
                                break;
                            }
                        }
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Record Inserted Successfully');</script>");

                            objResult = objUserTemplateBl.Employee_Select_ForPayItemTemplate(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                            if (objResult != null)
                            {

                                if (objResult.resultDT.Rows.Count > 0)
                                {
                                    gvSelectedPayItem.Visible = true;
                                    objResult = objUserPayItemTemplateBl.Employee_Select_ByEmployeeID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                                    gvSelectedPayItem.DataSource = objResult.resultDT;
                                    gvSelectedPayItem.DataBind();
                                    for (int j = 0; j < gvSelectedPayItem.Rows.Count; ++j)
                                    {
                                        sum = sum + Math.Round(Convert.ToDouble(gvSelectedPayItem.Rows[j].Cells[7].Text), 2);
                                    }
                                    txtMonthly.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControls.ConvertToCurrancy(sum.ToString())), 2));
                                    txtAnnual.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControls.ConvertToCurrancy((12 * sum).ToString())), 2));
                                    GridConvertion();

                                    PanelVisibility(false, true, true, false);
                                    btnApplyTemplate.Visible = false;
                                    txtMonthly.Enabled = false;
                                    txtAnnual.Enabled = false;
                                    txtGross.Enabled = false;
                                    ddlTemplate.Enabled = false;
                                }
                                else
                                {
                                    PanelVisibility(true, false, false, true);
                                    objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));
                                    ddlDependsOn.Enabled = false;
                                    ddlDependsOn.Items.Clear();
                                    ddlDependsOn.BackColor = System.Drawing.Color.Gainsboro;
                                    ddlPayItemType.Enabled = false;
                                    ddlPayItemType.BackColor = System.Drawing.Color.Gainsboro;
                                    txtAmount.Enabled = false;
                                    txtAmount.BackColor = System.Drawing.Color.Gainsboro;
                                    //txtPayItemName.Enabled = false;
                                    //txtPayItemName.BackColor = System.Drawing.Color.Gainsboro;
                                    txtPercentage.Enabled = false;
                                    txtPercentage.BackColor = System.Drawing.Color.Gainsboro;
                                    // btnApplyOrganisationTemplate.Enabled = true;
                                    txtAnnual.Enabled = false;
                                    txtAnnual.BackColor = System.Drawing.Color.Gainsboro;
                                    txtMonthly.Enabled = false;
                                    txtMonthly.BackColor = System.Drawing.Color.Gainsboro;
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
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Template.');</script>");
            }
        }
        #endregion

        #region gridview SeledtPayItem RowCommand Event
        protected void gvSelectedPayItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                UserPayItemTemplateTBl objUserPayItemtBL = new UserPayItemTemplateTBl();
                UserPayItemTemplateTBo objUserPayItemBO = new UserPayItemTemplateTBo();
                TrustPayItemBl objTrustPayItemBl = new TrustPayItemBl();
                ApplicationResult objResultsPayItem = new ApplicationResult();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                ViewState["UserTemplateID"] = commandArgs[0];
                ViewState["PayItemMID"] = commandArgs[1];
                ViewState["UserPayItemTemplateID"] = commandArgs[2];
                if (e.CommandName.ToString() == "Edit1")
                {
                    //BindTemplateName();
                    PanelVisibility(false, true, true, true);
                    txtPayItemName.Enabled = false;
                    ddlPayItemType.Enabled = false;
                    ddlDependsOn.Enabled = false;
                    btnApplyTemplate.Visible = false;

                    ddlTemplate.Enabled = false;
                    txtAnnual.Enabled = false;
                    txtMonthly.Enabled = false;
                    txtGross.Enabled = true;
                    String[] Depends;

                    //  PayItemType = gdSelectedPayItem.SelectedRow.Cells[3].Text;

                    TrustPayItemTemplateTBl objTrustPayItemTemplateBl = new TrustPayItemTemplateTBl();
                    ApplicationResult objResults = new ApplicationResult();
                    Controls objControls = new Controls();
                    objResults = objTrustPayItemTemplateBl.TrustPayItemTemplate_Select_PayItemWise(Convert.ToInt32(ddlTemplate.SelectedValue));
                    objControls.BindDropDown_ListBox(objResults.resultDT, ddlDependsOn, "Name", "PayItemMID");
                    ddlDependsOn.Items.Insert(0, new ListItem("-Select-", "-1"));

                    objResultsPayItem = objTrustPayItemBl.PayItemTemplate_SelectAll_BothID(Convert.ToInt32(ViewState["UserTemplateID"].ToString()), Convert.ToInt32(ViewState["PayItemMID"].ToString()));
                    if (objResultsPayItem != null)
                    {
                        if (objResultsPayItem.resultDT.Rows.Count > 0)
                        {
                            txtAmount.Text = Convert.ToString(Math.Round(Convert.ToDouble(objControls.ConvertToCurrancy(objResultsPayItem.resultDT.Rows[0]["Amount"].ToString())), 2));
                            txtPercentage.Text = objResultsPayItem.resultDT.Rows[0]["Percentage"].ToString();

                            Depends = objResultsPayItem.resultDT.Rows[0]["DependsOn"].ToString().Split(',');
                            int i = 0;
                            int len = Depends.Length;
                            for (i = 0; i < len; i++)
                            {
                                if (i >= len)
                                    break;
                                else
                                {
                                    if (Depends[i] != "NULL" && Depends[i] != "&nbsp;" && Depends[i] != "" && Depends[i] != "0")
                                    {
                                        //cbDependsOn.SelectedValue = str[i];             
                                        // cbDependsOn.Items.Add(Depends[i]);
                                        ddlDependsOn.SelectedValue = Depends[i];
                                    }
                                    else
                                    {
                                        ddlDependsOn.SelectedValue = null;
                                    }


                                }
                            }

                            txtPayItemName.Text = objResultsPayItem.resultDT.Rows[0]["Name"].ToString();

                            // ddlPayItemType.SelectedValue = gvSelectedPayItem.SelectedRow.Cells[3].Text;

                            if (objResultsPayItem.resultDT.Rows[0]["Type"].ToString() == "Dependent")
                            {
                                ddlPayItemType.SelectedValue = "1";
                                txtPercentage.Enabled = true;
                                txtAmount.Enabled = true; //Change
                                txtPercentage.BackColor = System.Drawing.Color.White;
                                txtAmount.BackColor = System.Drawing.Color.Gainsboro;
                            }
                            else
                                if (objResultsPayItem.resultDT.Rows[0]["Type"].ToString() == "Independent")
                            {
                                ddlPayItemType.SelectedValue = "0";
                                txtAmount.Enabled = true;
                                txtAmount.BackColor = System.Drawing.Color.White;
                                txtPercentage.BackColor = System.Drawing.Color.Gainsboro;
                                txtPercentage.Enabled = false;
                            }
                            else if (objResultsPayItem.resultDT.Rows[0]["Type"].ToString() == "Depends On Gross")
                            {
                                ddlPayItemType.SelectedValue = "2";

                                txtPercentage.Enabled = true;
                                txtPercentage.BackColor = System.Drawing.Color.White;
                                txtAmount.BackColor = System.Drawing.Color.Gainsboro;
                                //txtAmount.Enabled = false;
                                txtAmount.Enabled = true; //Change
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

        #region Template Dropdown SelectedVhange Event
        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Check Employee Name and Template Name [Arpit Shah][02-12-2021]
                string name1 = txtSearchName.Text.Trim();
                string name2 = ddlTemplate.SelectedItem.ToString().Trim();

                if(name1.ToString() == name2.ToString())
                {
                    TrustPayItemBl objTrustPayItemBl = new TrustPayItemBl();
                    ApplicationResult objResult = new ApplicationResult();

                    gvSelectedPayItem.Visible = false;
                    gvPayItem.Visible = true;
                    txtAnnual.Enabled = true;
                    txtMonthly.Enabled = true;

                    objResult = objTrustPayItemBl.PayItemTemplate_SelectAll_TemplateID(Convert.ToInt32(ddlTemplate.SelectedValue));
                    if (objResult != null)
                    {
                        gvPayItem.DataSource = objResult.resultDT;
                        gvPayItem.DataBind();
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            PanelVisibility(true, false, true, false);

                            //Bind txtGrossrv.Text [Total Gross Amout Automatic][Name: Arpit Shah Date: 03-12-2021]


                        }
                        else
                        {
                            PanelVisibility(false, false, true, false);
                        }
                    }
                }
                else
                {
                    gvSelectedPayItem.Visible = false;
                    PanelVisibility(false, false, false, false);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select Employee Name wise Template!!!!!.');</script>");
                }
               
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region button Save Click Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    UserPayItemTemplateTBl objUserPayItemtBL = new UserPayItemTemplateTBl();
            //    UserPayItemTemplateTBo objUserPayItemBO = new UserPayItemTemplateTBo();
            //    ApplicationResult objResultsPayItem = new ApplicationResult();
            //    objUserPayItemBO.UserPayItemTemplateID = Convert.ToInt32(ViewState["UserPayItemTemplateID"].ToString());
            //    objUserPayItemBO.PayItem = Convert.ToInt32(ViewState["PayItemMID"].ToString());
            //    objUserPayItemBO.UserTemplateID = Convert.ToInt32(ViewState["UserTemplateID"].ToString());
            //    objUserPayItemBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
            //    objUserPayItemBO.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
            //    objUserPayItemBO.Amount = Convert.ToDouble(txtAmount.Text.Trim());
            //    objUserPayItemBO.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
            //    objUserPayItemBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
            //    objUserPayItemBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
            //    objResultsPayItem = objUserPayItemtBL.UserPayItemTemplateT_Update(objUserPayItemBO);

            //    if (objResultsPayItem.status == ApplicationResult.CommonStatusType.SUCCESS)
            //    {
            //        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Successfully Updated');</script>");

            //        objResultsPayItem = objUserPayItemtBL.Employee_Select_ByEmployeeID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
            //        if (objResultsPayItem != null)
            //        {
            //            gvSelectedPayItem.DataSource = objResultsPayItem.resultDT;
            //            gvSelectedPayItem.DataBind();
            //            if (objResultsPayItem.resultDT.Rows.Count > 0)
            //            {
            //                gvSelectedPayItem.Visible = true;
            //            }
            //        }

            //        GridConvertion();

            //        for (int i = 0; i < gvSelectedPayItem.Rows.Count; ++i)
            //        {
            //            if (txtPayItemName.Text == gvSelectedPayItem.Rows[i].Cells[5].Text)
            //            {
            //                str = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[6].Text);
            //                double str1 = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[7].Text);
            //                sum = Math.Round((str * Convert.ToDouble(str1)) / 100); 

            //                //sum = Math.Round((str * Convert.ToDouble(gvSelectedPayItem.SelectedRow.Cells[7].Text)) / 100); 
            //                gvSelectedPayItem.Rows[i].Cells[7].Text = sum.ToString();
            //                objUserPayItemBO.UserTemplateID = Convert.ToInt32(gvSelectedPayItem.Rows[i].Cells[8].Text);
            //                objUserPayItemBO.PayItem = Convert.ToInt32(gvSelectedPayItem.Rows[i].Cells[0].Text);
            //                objUserPayItemBO.UserTemplateID = Convert.ToInt32(gvSelectedPayItem.Rows[i].Cells[1].Text);
            //                objUserPayItemBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
            //                objUserPayItemBO.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
            //                objUserPayItemBO.Amount = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[7].Text);
            //                objUserPayItemBO.Percentage = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[6].Text);
            //                objUserPayItemBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
            //                objUserPayItemBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
            //                objResultsPayItem = objUserPayItemtBL.UserPayItemTemplateT_Update(objUserPayItemBO); 
            //            }
            //        }
            //        //PanelVisibility(false, true, true, false);
            //        //btnApplyTemplate.Visible = false;
            //        //ddlTemplate.Enabled = false;
            //        //txtAnnual.Enabled = false;
            //        //txtMonthly.Enabled = false;
            //        //txtGross.Enabled = false;

            //        BindTemplateName();
            //        PanelVisibility(false, false, false, false);
            //    }   
            //}
            //catch (Exception ex)
            //{
            //    logger.Error("Error", ex);
            //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            //}

            try
            {
                UserPayItemTemplateTBl objUserPayItemtBL = new UserPayItemTemplateTBl();
                UserPayItemTemplateTBo objUserPayItemBO = new UserPayItemTemplateTBo();
                ApplicationResult objResultsPayItem = new ApplicationResult();
                objUserPayItemBO.UserPayItemTemplateID = Convert.ToInt32(ViewState["UserPayItemTemplateID"].ToString());
                objUserPayItemBO.PayItem = Convert.ToInt32(ViewState["PayItemMID"].ToString());
                objUserPayItemBO.UserTemplateID = Convert.ToInt32(ViewState["UserTemplateID"].ToString());
                objUserPayItemBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
                objUserPayItemBO.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                objUserPayItemBO.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                objUserPayItemBO.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
                objUserPayItemBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objUserPayItemBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                
                objResultsPayItem = objUserPayItemtBL.UserPayItemTemplateT_Update(objUserPayItemBO, Convert.ToDouble(txtGross.Text.Trim()));

                if (objResultsPayItem.status == ApplicationResult.CommonStatusType.SUCCESS)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Successfully Updated');</script>");

                    objResultsPayItem = objUserPayItemtBL.Employee_Select_ByEmployeeID(Convert.ToInt32(ViewState["EmployeeMID"].ToString()), Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                    if (objResultsPayItem != null)
                    {
                        gvSelectedPayItem.DataSource = objResultsPayItem.resultDT;
                        gvSelectedPayItem.DataBind();
                        if (objResultsPayItem.resultDT.Rows.Count > 0)
                        {
                            gvSelectedPayItem.Visible = true;
                        }
                    }

                    GridConvertion();

                    for (int i = 0; i < gvSelectedPayItem.Rows.Count; ++i)
                    {
                        if (txtPayItemName.Text == gvSelectedPayItem.Rows[i].Cells[5].Text) //[PayemItDependsOn Name
                        //if (txtPayItemName.Text == gvSelectedPayItem.Rows[i].Cells[5].Text) //[PayemItDependsOn Name
                        {
                            str = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[6].Text); //Percentage
                            str1 = Convert.ToDouble(txtAmount.Text.Trim()); //Gorss Amt
                            //str1 = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[7].Text);  //Gorss Amt
                            sum = Math.Round((str * Convert.ToDouble(str1)) / 100); ;
                            //sum = Math.Round((str * Convert.ToDouble(gvSelectedPayItem.SelectedRow.Cells[7].Text)) / 100); ;
                            gvSelectedPayItem.Rows[i].Cells[7].Text = sum.ToString();
                            objUserPayItemBO.UserPayItemTemplateID = Convert.ToInt32(gvSelectedPayItem.Rows[i].Cells[8].Text);
                            objUserPayItemBO.PayItem = Convert.ToInt32(gvSelectedPayItem.Rows[i].Cells[0].Text);
                            objUserPayItemBO.UserTemplateID = Convert.ToInt32(gvSelectedPayItem.Rows[i].Cells[1].Text);
                            objUserPayItemBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
                            objUserPayItemBO.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                            objUserPayItemBO.Amount = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[7].Text);
                            objUserPayItemBO.Percentage = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[6].Text);
                            objUserPayItemBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                            objUserPayItemBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                            objResultsPayItem = objUserPayItemtBL.UserPayItemTemplateT_Update(objUserPayItemBO, Convert.ToDouble(txtGross.Text.Trim()));
                        }
                    }

                    //for (int i = 0; i < gvSelectedPayItem.Rows.Count; ++i)
                    //{
                    //    //Check Independent(Basic)
                    //    if (ddlPayItemType.SelectedItem.ToString() == gvSelectedPayItem.Rows[i].Cells[3].Text)
                    //    {
                    //        if (txtPayItemName.Text == gvSelectedPayItem.Rows[i].Cells[2].Text)
                    //        {
                    //            str = Convert.ToDouble(gvSelectedPayItem.Rows[i].Cells[7].Text);


                    //        }
                    //        else
                    //        {

                    //        }
                    //    }
                    //}
                    PanelVisibility(false, true, true, false);
                    btnApplyTemplate.Visible = false;
                    ddlTemplate.Enabled = false;
                    txtAnnual.Enabled = false;
                    txtMonthly.Enabled = false;
                    txtGross.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Find Percentage Wise Amount (Dependency) Edit Time
        protected void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtPercentage.Text=="")
                {
                    double bamt = 0;
                    double tamt = 0;
                    //Find Basic Salary in gvSelectedPayItem (GridView)
                    for (int i = 0; i < gvSelectedPayItem.Rows.Count; ++i)
                    {
                        bamt = Convert.ToDouble(gvSelectedPayItem.Rows[0].Cells[7].Text);
                    }
                    tamt = bamt * ((Convert.ToDouble(txtPercentage.Text) / 100));
                    txtAmount.Text = tamt.ToString();
                }
                
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region PayItemType Dropdown SelectedChanged Event
        protected void ddlPayItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(bool blPayItem, bool blEmployeePayItem, bool blApplyTemplate, bool blEditTemplate)
        {
            divPayItem.Visible = blPayItem;
            divEmployeePayItem.Visible = blEmployeePayItem;
            divApplyTemplate.Visible = blApplyTemplate;
            divEditTemplate.Visible = blEditTemplate;
        }
        #endregion
 
    }
}