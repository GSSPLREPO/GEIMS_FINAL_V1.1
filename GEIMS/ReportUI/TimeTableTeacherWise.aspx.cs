using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using System.Web.Script.Serialization;

namespace GEIMS.ReportUI
{
    public partial class TimeTableTeacherWise : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TimeTableTeacherWise));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    divTimeTable.Visible = false;
                    btnPrintDetail.Visible = false;
                    btnBack1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region Bind Time Table
        public void BindTimeTable()
        {
            ApplicationResult objResult = new ApplicationResult();
            TimeTableBL objTimeTableBl = new TimeTableBL();
            objResult = objTimeTableBl.TimeTable_Select_TeacherWise(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]),
                Convert.ToInt32((hfEmployeeMID.Value)));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvReport.DataSource = objResult.resultDT;
                    gvReport.DataBind();
                    //for (int i = 1; i <= objResult.resultDT.Rows.Count; i++)
                    //    {
                    //        //int intCount = Convert.ToInt32(objResult.resultDT.Rows[i][0].ToString());
                    //        DataTable dtTemp = objResult.resultDT.AsEnumerable().Where(r => r.Field<int>("DayNo") == Convert.ToInt32(objResult.resultDT.Rows[i][0].ToString()))
                    //        .CopyToDataTable();
                    //        if (dtTemp.Rows.Count > 0)
                    //        {
                    //            ViewState["MaxNo"] = objResult.resultDT.Rows[0][10].ToString();
                    //            for (int j = 1; j <= Convert.ToInt32(objResult.resultDT.Rows[i][9].ToString()); j++)
                    //            {
                    //                Label lbl1 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblFrom" + j.ToString() + "" + i.ToString()) as Label;
                    //                lbl1.Text = dtTemp.Rows[j - 1]["FromTime"].ToString();
                    //                Label lbl2 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblTo" + j.ToString() + "" + i.ToString()) as Label;
                    //                lbl2.Text = dtTemp.Rows[j - 1]["ToTime"].ToString();
                    //                Label lbl3 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblClass" + j.ToString() + "" + i.ToString()) as Label;
                    //                lbl3.Text = dtTemp.Rows[j - 1]["ClassName"].ToString();
                    //                Label lbl4 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblDivision" + j.ToString() + "" + i.ToString()) as Label;
                    //                lbl4.Text = dtTemp.Rows[j - 1]["DivisionName"].ToString();
                    //                Label lbl5 = Page.Master.FindControl("ContentPlaceHolder1").FindControl("lblSUbject" + j.ToString() + "" + i.ToString()) as Label;
                    //                lbl5.Text = dtTemp.Rows[j - 1]["SubjectName"].ToString();
                    //            }
                    //        }

                    //}
                }
            }
            //  ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>BindTimeTable(" + Convert.ToInt32(ViewState["MaxNo"].ToString()) + ");</script>");
        }
        #endregion

        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            divTimeTable.Visible = true;
            BindTimeTable();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('DisplayPrint');", true);
        }

      
        #region Get Employee Web Service Method
        [System.Web.Services.WebMethod]
        public static string[] GetAllEmployeeNameForReport(string prefixText, int TrustMID, int SchoolMID)
        {
            EmployeeMBL objEmployeeMbl = new EmployeeMBL();
            ApplicationResult objResult = new ApplicationResult();
            string strSearchText = prefixText + "%";
            List<string> result = new List<string>();
            objResult = objEmployeeMbl.EmployeeM_Select_ForAutoComplete(strSearchText, TrustMID, SchoolMID);
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

        protected void btnGo_Click(object sender, EventArgs e)
        {
            divTimeTable.Visible = true;
            BindTimeTable();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}