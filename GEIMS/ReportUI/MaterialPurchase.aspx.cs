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
using GEIMS.DataAccess;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace GEIMS.ReportUI
{
    public partial class MaterialPurchase : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {

        }

        #region Bind Vendor Name
        public void getVendorName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            VendorBL objVendorBL = new VendorBL();

            objResult = objVendorBL.Vendor_SelectAll();
            if (objResult != null)
            {
                objControls.BindDropDown_ListBox(objResult.resultDT, ddlVendor, "VendorName", "VendorID");
                if (objResult.resultDT.Rows.Count > 0)
                {
                }
                ddlVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
            }
        }
        #endregion
    }
}