using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GEIMS.Client.UI
{
    public partial class SchoolReports : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "SchoolGeneralReports")
                {
                    divGeneralReports.Visible = true;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolStudentReports")
                {
                    divStatutory.Visible = true;
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;

                }
                else if (Request.QueryString["Mode"] == "SchoolFeesReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = true;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolInventoryReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = true;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolPayrollReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = true;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolAccountingReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolStatutoryReports")
                {
                    divStatutory.Visible = true;
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divTimeTable.Visible = false;
                    divAccounting.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolTimeTableReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = true;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "SchoolDEOReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divResultReports.Visible = false;
                    divDEOReports.Visible = true;
                }
                else if (Request.QueryString["Mode"] == "ResultReports")
                {
                    divGeneralReports.Visible = false;
                    divPayRoll.Visible = false;
                    divFees.Visible = false;
                    divInventory.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divTimeTable.Visible = false;
                    divDEOReports.Visible = false;
                    divResultReports.Visible = true;
                }
                else
                {
                    divGeneralReports.Visible = true;
                    divPayRoll.Visible = false;
                    divFees.Visible = true;
                    divInventory.Visible = true;
                    divAccounting.Visible = true;
                    divStatutory.Visible = true;
                    divTimeTable.Visible = true;
                    divDEOReports.Visible = true;
                    divResultReports.Visible = true;
                }
            }
        }
    }
}