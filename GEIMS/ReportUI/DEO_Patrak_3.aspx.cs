using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.Common;

namespace GEIMS.ReportUI
{
    public partial class DEO_Patrak_3 : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(DEO_Patrak_3));

        public override void VerifyRenderingInServerForm(Control control) { return; }

        #region  pageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!IsPostBack)
                {
                    divReport.Visible = true;
                    BindDEOReport();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind DEO Report
        public void BindDEOReport()
        {
            ApplicationResult objResult = new ApplicationResult();
            SchoolBL objSchoolBL = new SchoolBL();

            objResult = objSchoolBL.SchoolWiseDEOReport_Patrak3(Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString()));
            if (objResult.resultDT.Rows.Count > 0)
            {
                gvReport.DataSource = objResult.resultDT;
                gvReport.DataBind();
                gvReport1.DataSource = objResult.resultDT;
                gvReport1.DataBind();
                divReport.Visible = true;
                lblTrustName.Text = Session[ApplicationSession.TRUSTNAME].ToString();
                lblSchoolName.Text = Session[ApplicationSession.SCHOOLNAME].ToString();
                lblTrust.Text = Session[ApplicationSession.TRUSTNAME].ToString();
                lblSchool.Text = Session[ApplicationSession.SCHOOLNAME].ToString();

            }
            else
            {
                divReport.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Records Found.');", true);
            }
        }
        #endregion

        #region btnBack Click
        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region btnPrintDetail
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            divReport.Visible = true;
            BindDEOReport();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divReport1');", true);
        }
        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/SchoolReports.aspx?Mode=SchoolDEOReports");
        }
        #endregion

        #region Export Excel Button Click Event
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.AddHeader("content-disposition", "attachment;filename=DEO_Patrak_3.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvReport.AllowPaging = false;
                gvReport.RenderControl(hw);
                //string strTitle = "Office of The Commissioner of Police, Vadodara City";
                //string strSubTitle = lblTitle.Text;
                string Date = DateTime.UtcNow.AddHours(5.5).ToString();
                // string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : Compact Report</span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><br/><span align='center' style='font-family:verdana;font-size:11px'></span><br/><span align='center' style='font-family:verdana;font-size:11px'></div><br/><br/>" + sw.ToString() + "<br/>";
                string content = "<div align='center' style='font-family:verdana;font-size:13px'><span style='font-size:16px:font-weight:bold;color:Maroon;'>Report : DEO Report Patrak-3 </span><br/><span style='font-size:13px:font-weight:bold'></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Date :</strong>" + System.DateTime.Now.ToShortDateString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>Trust Name :</strong>" + Session[ApplicationSession.TRUSTNAME].ToString() + "</span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong>School Name :</strong>" + Session[ApplicationSession.SCHOOLNAME].ToString() + "</span><br/><br/><span align='center' style='font-family:verdana;font-size:11px'><strong></strong></span><br/><span align='center' style='font-family:verdana;font-size:11px'><strong></strong></div><br/><br/>" + sw.ToString() + "<br/>";
                Response.Output.Write(content);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical Problem. Contact to your Administrator.');</script>");
            }
        }
        #endregion
    }
}