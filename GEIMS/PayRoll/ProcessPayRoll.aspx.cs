using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.DataAccess;
using log4net;
using System.Collections.Generic;


namespace GEIMS.PayRoll
{
    public partial class ProcessPayRoll : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(ProcessPayRoll));

        #region Declaration
        ApplicationResult objResults = new ApplicationResult();
        ApplicationResult objResultsForTrustWise = new ApplicationResult();
        DataTable dtTrust = new DataTable();
        #endregion

        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                ddlYear.Items.Insert(0, new ListItem("-Select-", " "));
                bindYear();
                divButtons.Visible = false;
                divEmployee.Visible = false;
              
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
            ddlYear.Items.Clear();
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

        #region GeneratePayROll
        public void GeneratePayRoll()
        {
            PaySlipBl objPaySlipBl = new PaySlipBl();
            ApplicationResult objResult = new ApplicationResult();

            objResult = objPaySlipBl.Select_Employee_ForPaySlip((Session[ApplicationSession.TRUSTID]).ToString(), ddlMonth.SelectedValue, ddlYear.SelectedValue,0, (Session[ApplicationSession.SCHOOLID]).ToString());
            if (objResult != null)
            {
                ViewState["Count"] = objResult.resultDT.Columns.Count;

                if (objResult.resultDT.Rows.Count > 0)
                {
                    DataRow newrow1 = objResult.resultDT.NewRow();
                    newrow1[1] = "Total";
                    objResult.resultDT.Rows.Add(newrow1);
                    int i = 0;
                    foreach (DataColumn col in objResult.resultDT.Columns)
                    {                      
                        if (i != 0 && i != 1 && i != 2 && i != 3 )
                        {
                            object sumObject;
                            //Note : Don't Use [] Bracket or Special Character when Inert PayItem other wise generate Error
                            sumObject = objResult.resultDT.Compute("Sum(["+col.ColumnName+"])", ""); 
                            objResult.resultDT.Rows[objResult.resultDT.Rows.Count - 1][i] = sumObject;
                        }
                        i++;
                    }                  
                }
                gvtemp.DataSource = null;
                gvtemp.DataSource = objResult.resultDT;
                gvtemp.DataBind();

                if (objResult.resultDT.Rows.Count > 0)
                {
                    ((CheckBox)gvtemp.Rows[objResult.resultDT.Rows.Count - 1].FindControl("chkChild")).Visible = false;
                    divButtons.Visible = true;
                    divEmployee.Visible = true;
                    gvtemp.Visible = true;
                    lblErrMsg.Visible = false;
                }
                else
                {
                    gvtemp.Visible = false;
                    divButtons.Visible = false;
                    divEmployee.Visible = true;
                    lblErrMsg.Text = "No record Found";
                }
            }
        }
        #endregion

        #region btnApprove Click Event
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DatabaseTransaction.OpenConnectionTransation();
            foreach (GridViewRow gvrow in gvtemp.Rows)
            {
                    PaySlipBl objPaySlipBl = new PaySlipBl();
                    PaySlipBo objPaySlipBo = new PaySlipBo();
                    ApplicationResult objResultsUpdatePaySlip = new ApplicationResult();

                    objPaySlipBo.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    objPaySlipBo.Year = Convert.ToInt32(ddlYear.SelectedValue);
                if (((CheckBox)gvrow.FindControl("chkChild")).Checked == true)
                {
                    

                    int id = Convert.ToInt32( gvrow.Cells[1].Text);
                    objPaySlipBo.UserID = id;
                    objPaySlipBo.PayslipApproved = 1;
                    objPaySlipBo.PaySlipSendforApproval = 1;
                  
                    objPaySlipBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objPaySlipBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResultsUpdatePaySlip = objPaySlipBl.PaySlip_Update_SelectedPart(objPaySlipBo);
                   

                }
            }
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved SuccessFully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Not Approved SuccessFully');", true);
            }
            DatabaseTransaction.CommitTransation();
            GeneratePayRoll();
        }
        #endregion

        #region btnExport Click Event
        /// <summary>
        /// export all payslips to excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            PaySlipBl objPaySlipBl = new PaySlipBl();
            ApplicationResult objResultsExpoert = new ApplicationResult();
            //PayRollProcess_ExportTOExcel
           //objResultsExpoert = objPaySlipBl.PayRollProcess_ExportTOExcel(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue)
             //    ,Convert.ToInt32(Session[ApplicationSession.SCHOOLID])); //Added schoolid parameter 12/11/2022 Bhandavi
            //by calling above data is coming wrong 
            
            objResultsExpoert = objPaySlipBl.Select_Employee_ForPaySlip((Session[ApplicationSession.TRUSTID]).ToString(), ddlMonth.SelectedValue, ddlYear.SelectedValue, 0, (Session[ApplicationSession.SCHOOLID]).ToString());

            if (objResultsExpoert.resultDT.Rows.Count > 0)
            {
                ds.Tables.Add(objResultsExpoert.resultDT);

                DataView dv = new DataView(ds.Tables[0]);

                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                DataTable dt = dv.Table;

                string headerToExport = string.Empty;

                foreach (DataColumn col in dt.Columns)
                    headerToExport += (char)34 + col.ColumnName + (char)34 + (char)44;

                headerToExport.Remove(headerToExport.Length - 1, 1);
                headerToExport = headerToExport + Environment.NewLine + Environment.NewLine;
                builder.Append(headerToExport);

                string body = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    foreach (object obj in row.ItemArray)
                        body = body + obj.ToString() + (char)44;

                    body.Remove(body.Length - 1, 1);

                    body = body + Environment.NewLine;
                }

                builder.Append(body);
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);


                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=Approved_Payslip_For_" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text + ".csv");
                Response.Charset = "";
                Response.Write(builder.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Payslip approved for this Month.');", true);
            }
        }
        #endregion

        #region ddlYera Selected Index Changed
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            divButtons.Visible = false;
            GeneratePayRoll();
        }
        #endregion

        #region 
        protected void gvtemp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Visible = false;
               
                //GrandTotal = GrandTotal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetSalary"));
                //lblTotal.Text = Convert.ToString(GrandTotal);
                //int j = Convert.ToInt32(gvtemp.Rows.Count);
                //((CheckBox)gvtemp.Rows[j].FindControl("chkChild")).Visible = false;
            
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //TotalEarning = TotalEarning + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalEarning"));
                    //TotalDeduction = TotalDeduction + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalDeduction"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                  
                    // Label lblEarning = (Label)e.Row.FindControl("lblTotal");
                    //lblEarning.Text = TotalEarning.ToString();

                    //Label lblDeduction = (Label)e.Row.FindControl("lblTotal");
                    //lblTotal.Text = lblDeduction.ToString();

                }
        }
        #endregion

        #region
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindYear();
        }
        #endregion

    }
}