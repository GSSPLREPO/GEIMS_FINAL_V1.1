using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.Accounting
{
    public partial class SerialNo : PageBase
    {
        #region declaration
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SerialNo));
        readonly Controls _objControl = new Controls();
        #endregion

        #region Pre-Init Method
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] == "TU")
            {
                MasterPageFile = "~/Master/TrustMain.Master";
            }
        }
        #endregion

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session[ApplicationSession.HASACCESSACCOUNTUSERID]) > 0)
            {
                if (IsPostBack) return;
                if (Request.QueryString["mode"] == "TU")
                {
                    lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                }
                else
                {
                    if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                    {
                        lblDuration.Text = Session[ApplicationSession.TRUSTNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                    }
                    else
                        lblDuration.Text = Session[ApplicationSession.SCHOOLNAME].ToString() + ". Account Duration : " + Session[ApplicationSession.ACCOUNTFROMDATE].ToString() + " To " + Session[ApplicationSession.ACCOUNTTODATE].ToString();
                }
                PanelVisibility(1);
                ViewState["Mode"] = "Save";
                BindYear();
                BindGrid();
                BindTrust();
            }
            else
            {
                if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) == 0)
                    Response.Redirect("../Accounting/AccountLogin.aspx?mode=TU", false);
                else
                    Response.Redirect("../Accounting/AccountLogin.aspx", false);
            }
        }
        #endregion

        #region Button Click Events

        #region Viewlist Button
        protected void btnViewList_Click(object sender, EventArgs e)
        {
            PanelVisibility(1);
            Clear();
        }
        #endregion

        #region Button Add New
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            PanelVisibility(2);
            Clear();
            BindTrust();
            ddlTrust.SelectedValue = Session[ApplicationSession.TRUSTID].ToString();
            ddlTrust_SelectedIndexChanged(sender,e);
            if (Convert.ToInt32(Session[ApplicationSession.SCHOOLID]) > 0)
                ddlSchool.SelectedValue = Session[ApplicationSession.SCHOOLID].ToString();
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var objSerialNoBo = new SerialNoBo();
                var objSerialNoBl = new SerialNoBl();
                int intId = 0;

                objSerialNoBo.TrustMID = Convert.ToInt32(ddlTrust.SelectedValue);
                objSerialNoBo.SchoolMID = ddlSchool.SelectedValue != "" ? Convert.ToInt32(ddlSchool.SelectedValue) : 0;
                objSerialNoBo.Year = Convert.ToInt32(ddlYear.SelectedValue);
                objSerialNoBo.EntryType = ddlType.SelectedValue;
                objSerialNoBo.StartNo = Convert.ToInt32(txtStartNo.Text);
                objSerialNoBo.CreatedBy = Convert.ToInt32(Session[ApplicationSession.USERID]);
                objSerialNoBo.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString(CultureInfo.InvariantCulture);
                objSerialNoBo.IsDeleted = 0;
                objSerialNoBo.LastModifideDate = DateTime.UtcNow.AddHours(5.5).ToString(CultureInfo.InvariantCulture);
                objSerialNoBo.LastModifideUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                switch (ViewState["Mode"].ToString())
                {
                    case "Save":
                        intId = -1;
                        break;
                    case "Edit":
                        intId = Convert.ToInt32(ViewState["Id"].ToString());
                        break;
                }
                var objResultValidate = objSerialNoBl.SerialNo_ValidateName(intId, Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]), Convert.ToInt32(ddlYear.SelectedValue), ddlType.SelectedValue);

                if (objResultValidate != null)
                {
                    var dtResultValidate = objResultValidate.resultDT;
                    if (dtResultValidate.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('This record already exists.');</script>");
                    }
                    else
                    {
                        if (ViewState["Mode"].ToString() == "Save")
                        {
                            var objResult = objSerialNoBl.SerialNo_Insert(objSerialNoBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record Saved Successfully.');</script>");
                            }
                        }
                        else
                        {
                            objSerialNoBo.Id = Convert.ToInt32(ViewState["Id"]);
                            var objResult = objSerialNoBl.SerialNo_Update(objSerialNoBo);
                            ClientScript.RegisterStartupScript(typeof (Page), "MessagePopUp",objResult.status == ApplicationResult.CommonStatusType.SUCCESS
                                    ? "<script>alert('Record Updated Successfully.');</script>"
                                    : "<script>alert('You can not changed the record. It is already in use.');</script>");
                        }
                        Clear();
                        BindGrid();
                        PanelVisibility(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #endregion

        #region Dropdowns Events
        protected void ddlTrust_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSchool();
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gridview events
        #region gvSerialNo Events
        protected void gvSerialNo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var objSerialBl = new SerialNoBl();
                ViewState["Id"] = e.CommandArgument;
                if (e.CommandName == "Edit1")
                {
                    var objResult = objSerialBl.SerialNo_Select(Convert.ToInt32(e.CommandArgument));
                    if (objResult == null) return;
                    var dtresult = objResult.resultDT;
                    if (dtresult.Rows.Count <= 0) return;
                    BindTrust();
                    ddlTrust.SelectedValue = dtresult.Rows[0]["TrustMID"].ToString();
                    ddlTrust_SelectedIndexChanged(sender, e);
                    if (dtresult.Rows[0]["SchoolMID"].ToString() != "0")
                        ddlSchool.SelectedValue = dtresult.Rows[0]["SchoolMID"].ToString();
                    ddlType.SelectedValue = dtresult.Rows[0]["EntryType"].ToString();
                    ddlYear.SelectedValue = dtresult.Rows[0]["Year"].ToString();
                    txtStartNo.Text = dtresult.Rows[0]["StartNo"].ToString();

                    ViewState["Mode"] = "Edit";
                    ddlType.Enabled = false;
                    ddlYear.Enabled = false;
                    PanelVisibility(2);

                }
                else if (e.CommandName == "Delete1")
                {
                    var objResult = objSerialBl.SerialNo_Delete(Convert.ToInt32(e.CommandArgument));
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", objResult.status == ApplicationResult.CommonStatusType.SUCCESS
                            ? "<script>alert('Record deleted successfully.');</script>"
                            : "<script>alert('You can not delete this record. It is already in use.');</script>");
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        protected void gvSerialNo_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvSerialNo.Rows.Count <= 0) return;
                gvSerialNo.UseAccessibleHeader = true;
                gvSerialNo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion
        #endregion

        #region User Define Events

        #region Bind Grid
        public void BindGrid()
        {
            try
            {
                var objSerialNoBl = new SerialNoBl();

                var objApplicationResult = objSerialNoBl.SerialNo_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objApplicationResult == null) return;
                var dtSelectAll = UpdateDt(objApplicationResult.resultDT);
                gvSerialNo.DataSource = dtSelectAll;
                gvSerialNo.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind Trust

        public void BindTrust()
        {
            var objTrustBl = new TrustBL();

            var objResult = objTrustBl.Trust_SelectAll();
            if (objResult == null) return;
            if (objResult.resultDT.Rows.Count <= 0) return;

            _objControl.BindDropDown_ListBox(objResult.resultDT, ddlTrust, "TrustNameEng", "TrustMID");
            ddlTrust.Items.Insert(0, new ListItem("--Select--", ""));
        }

        #endregion

        #region Bind School

        public void BindSchool()
        {
            var objSchoolBl = new SchoolBL();

            var objResult = objSchoolBl.School_SelectAll(Convert.ToInt32(ddlTrust.SelectedValue));
            if (objResult == null) return;
            if (objResult.resultDT.Rows.Count <= 0) return;

            _objControl.BindDropDown_ListBox(objResult.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");
            ddlSchool.Items.Insert(0, new ListItem("--Select--", ""));
        }

        #endregion

        #region Clear
        public void Clear()
        {
            if (Master != null) _objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
            ViewState["Id"] = "-1";
            ddlType.Enabled = true;
            ddlYear.Enabled = true;
        }

        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            switch (intcode)
            {
                case 1:
                    divGrid.Visible = true;
                    tabs.Visible = false;
                    btnViewList.Visible = false;
                    break;
                case 2:
                    divGrid.Visible = false;
                    tabs.Visible = true;
                    btnViewList.Visible = true;
                    break;
            }
        }

        #endregion

        #region Bind Year

        public void BindYear()
        {
            CommonFunctions cf = new CommonFunctions();
            var dtYear = new DataTable();
            dtYear = cf.CreateFinancialYearDt();

            _objControl.BindDropDown_ListBox(dtYear, ddlYear, "FinancialYear", "Year");
            ddlYear.Items.Insert(0, new ListItem("-Select-", ""));
        }

        #endregion
        
        #endregion


        public DataTable UpdateDt(DataTable dt)
        {
            var dtCloned = dt.Clone();
            dtCloned.Columns["Year"].DataType = typeof(string);
            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }

            foreach (DataRow row in dtCloned.Rows)
            {
                var j = Convert.ToInt32(row["Year"]);
                var k = j.ToString().Insert(2,"-");

                row["Year"] = "20"+k.ToString();
                row.EndEdit();
                dtCloned.AcceptChanges();
            }
            return dtCloned;
        }

    }
}