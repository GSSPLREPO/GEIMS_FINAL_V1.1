using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GEIMS.Client.UI
{
    public partial class TrustReports : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "GeneralReports")
                {
                    divGeneralReports.Visible = true;
                    divInventory.Visible = false;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = false;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "InventoryReports")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = true;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = false;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "PayRollReports")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = false;
                    divPayRoll.Visible = true;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = false;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "AccountingReports")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = false;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = true;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = false;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "StatutoryReports")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = false;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = true;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = false;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "FeesReport")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = false;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = true;
                    divMeeting.Visible = false;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "MeetingReports")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = false;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = true;
                    divEvent.Visible = false;
                    divLibrary.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "EventReports")
                {
                    divGeneralReports.Visible = false;
                    divInventory.Visible = false;
                    divPayRoll.Visible = false;
                    divAccounting.Visible = false;
                    divStatutory.Visible = false;
                    divFeesCollection.Visible = false;
                    divMeeting.Visible = false;
                    divEvent.Visible = true;
                    divLibrary.Visible = false;
                }
                //else if (Request.QueryString["Mode"] == "LibraryReports")
                //{
                //    divGeneralReports.Visible = false;
                //    divInventory.Visible = false;
                //    divPayRoll.Visible = false;
                //    divAccounting.Visible = false;
                //    divStatutory.Visible = false;
                //    divFeesCollection.Visible = false;
                //    divMeeting.Visible = false;
                //    divEvent.Visible = false;
                //    divLibrary.Visible = true;
                //}
                else
                {
                    divGeneralReports.Visible = true;
                    divInventory.Visible = true;
                    divPayRoll.Visible = true;
                    divAccounting.Visible = true;
                    divStatutory.Visible = true;
                    divFeesCollection.Visible = true;
                    divMeeting.Visible = true;
                    divEvent.Visible = true;
                    divLibrary.Visible = false;
                }
            }
        }
    }
}