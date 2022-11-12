using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI; 
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using System.Web.Script.Serialization;
using System.Data;

namespace GEIMS.ReportUI
{
    public partial class EmployeePayrollReport : PageBase
    {
        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                bindYear();
                divReport.Visible = false;
                divEmployee.Visible = true;
                btnPrintDetail.Visible = false;
                //hfTrustMID.Value = Session[ApplicationSession.TRUSTID].ToString();
                //hfSchoolMID.Value = Session[ApplicationSession.SCHOOLID].ToString();
            }

        }
        #endregion

        #region Bind Year
        public void bindYear()
        {
            string[] strYear;
            int intacYear = 0;
            #region Get Accounting Start Date
            ApplicationResult objResults = new ApplicationResult();
            TrustBL objTrustBl = new TrustBL();
           
            objResults = objTrustBl.Trust_Select(Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString()));

            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    string strACStartDate = objResults.resultDT.Rows[0][TrustBO.TRUST_ACCOUNTSTARTDATE].ToString();
                    strYear = strACStartDate.ToString().Split(new char[] { '/' });
                    intacYear = Convert.ToInt32(strYear[2]);
                }

            }
            #endregion
           

            for (int i = intacYear; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        #endregion

        #region btnPrintDetail Click Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divSchool1');", true);
            lblTrust.Text = "Fertilizer Nagar English and Gujarati Medium School.";
            lblYear1.Text = ddlYear.SelectedItem.Text;
            lblMonth1.Text = ddlMonth.SelectedItem.Text;
            
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        #region BtnGo Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlMonth.SelectedValue != "" && ddlYear.SelectedValue != "-1")
            {
                BindgvReport();
                //divReport.Visible = true;
                //divEmployee.Visible = false;
            }
            else
            {
                divReport.Visible = false;
                divEmployee.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Dropdowns.');", true);
            }

        }
    #endregion

        #region Bind GridView
        /// <summary>
        /// to show employee payroll report for selected month and year
        /// </summary>
        public void BindgvReport()
        {
            PaySlipBl objPaySlipBl = new PaySlipBl();
            ApplicationResult objResult = new ApplicationResult();

            //objResult = objPaySlipBl.Select_Employee_ForPaySlip_Report((Session[ApplicationSession.TRUSTID]).ToString(),
            //    ddlMonth.SelectedValue, ddlYear.SelectedValue, 1);
            objResult = objPaySlipBl.Select_Employee_ForPaySlip_Report((Session[ApplicationSession.TRUSTID]).ToString(), ddlMonth.SelectedValue, ddlYear.SelectedValue, 1, (Session[ApplicationSession.SCHOOLID]).ToString());
            if (objResult != null)
            {
                gvReport1.DataSource = null;
                gvReport.DataSource = null;
               
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvReport1.Visible = true;
                    gvReport.Visible = true;
                    divReport.Visible = true;
                    lblTrustName.Text = "Fertilizer Nagar English and Gujarati Medium School.";
                    lblYear.Text = ddlYear.SelectedItem.Text;
                    lblMonth.Text = ddlMonth.SelectedItem.Text;
                    DataRow newrow1 = objResult.resultDT.NewRow();
                    newrow1[1] = "Total";
                    objResult.resultDT.Rows.Add(newrow1);
                    int i = 0;
                    foreach (DataColumn col in objResult.resultDT.Columns)
                    {

                        if (i != 0 && i != 1 && i != 2 && i != 3)
                        {
                            object sumObject;
                            sumObject = objResult.resultDT.Compute("Sum([" + col.ColumnName + "])", "");
                            objResult.resultDT.Rows[objResult.resultDT.Rows.Count - 1][i] = sumObject;
                        }
                        i++;
                    }

                    gvReport1.DataSource = objResult.resultDT;
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();
                    gvReport1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record Found.');", true);
                   //Response.Redirect("../PayRoll/EmployeePayRollReport.aspx");
                    ClearAll();
                }
            }

      


        }
        #endregion

        #region btnBack Click Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
             divEmployee .Visible = true;
             btnPrintDetail.Visible = false;
        }
        #endregion

        #region gvReport row bound Event
        protected void gvReport1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            
        }

      
        protected void gvReport_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        #endregion

        #region BtnBackToreport Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolPayrollReports");
            //Response.Redirect("GeneratePaySlip.aspx");
        }
        #endregion

    }
}