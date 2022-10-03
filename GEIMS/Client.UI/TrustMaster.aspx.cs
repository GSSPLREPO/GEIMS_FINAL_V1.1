using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using log4net;
using GEIMS.DataAccess;


namespace GEIMS.Client.UI
{
    public partial class TrustMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(TrustMaster));

        #region Pageload Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            GetImage();
            if (!Page.IsPostBack)
            {
                try
                {
                    txtAccStartDate.Enabled = true;
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    ViewState["TrustMID"] = 0;
                    hfTab.Value = "0";
                    txtApprovalDate.Attributes.Add("readonly", "readonly");
                    txtAccStartDate.Attributes.Add("readonly", "readonly");
                    RestrictNewTrust();
                    BindYear();
                    txtAbbreviation.Enabled = true;

                  

                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion

        #region BindYear
        public void BindYear()
        {
            try
            {
                Controls objControls = new Controls();

                int currentYear = DateTime.Now.Year;
                CommonFunctions CF = new CommonFunctions();
                DataTable dtYear = CF.CreateDTYear();

                if (dtYear.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(dtYear, ddlApprovalYear, "AcademicYear", "AcademicYear");
                }
                ddlApprovalYear.Items.Insert(0, new ListItem("-Select-", "-1"));
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Get Image
        public void GetImage()
        {
            if (fuImage.HasFile)
            {
                string Extension;
                Extension = Path.GetExtension(fuImage.FileName);
                if (Extension == ".jpg" || Extension == ".JPG" || Extension == ".jpeg" || Extension == ".JPEG" || Extension == ".bmp" || Extension == ".BMP" || Extension == ".gif" || Extension == ".GIF" || Extension == ".png" || Extension == ".PNG")
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Logo/TrustLogo/") + txtTrustName.Text.Replace(" ", "").ToString() + Extension))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Logo/TrustLogo/") + txtTrustName.Text.Replace(" ", "").ToString() + Extension);
                    }
                    fuImage.PostedFile.SaveAs(Server.MapPath("~/Logo/TrustLogo/") + txtTrustName.Text.Replace(" ", "").ToString() + Extension);
                    //objSchoolBo.SchoolLogo = txtSchoolName.Text.Replace(" ", "").ToString() + Extension;
                    imgphoto.ImageUrl = "~/Logo/TrustLogo/" + txtTrustName.Text.Replace(" ", "").ToString() + Extension;
                    // ViewState["strPhoto"] = "../Logo/TrustLogo/" + txtTrustName.Text.Replace(" ", "").ToString() + Extension;
                    Byte[] bytes = (byte[])ImageToByteArrayFromFilePath("~/Logo/TrustLogo/" + txtTrustName.Text.Replace(" ", "").ToString() + Extension);
                    ViewState["Bytes"] = bytes;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('File format not supported');</script>");

                }
            }
            else
            {
                imgphoto.ImageUrl = "~/images/noimage-big.jpg";
            }
        }
        #endregion

        #region Bind grid For Trust
        private void GridDataBind()
        {
            try
            {
                TrustBL objTrustBl = new TrustBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objTrustBl.Trust_SelectAll();
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvTrust.DataSource = objResult.resultDT;
                        gvTrust.DataBind();
                        PanelGrid_VisibilityMode(1);

                    }
                    else
                    {
                        PanelGrid_VisibilityMode(2);
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




        #region ViewList Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["TrustMID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Save Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TrustBO objTrustBo = new TrustBO();
                TrustBL objTrustBl = new TrustBL();
                BankAssociationBL objBankBl = new BankAssociationBL();
                BankAssociationBO objBankBo = new BankAssociationBO();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();

                //objTrustBo.TrustMID = Convert.ToInt32(ViewState["TrustMID"].ToString());
                objTrustBo.TrustNameEng = txtTrustName.Text;
                objTrustBo.TrustNameGuj = txtTrustNameGuj.Text;
                objTrustBo.RegistrationCode = txtRegCode.Text;
                objTrustBo.TrustAbbreviation = txtAbbreviation.Text;

                //Changed on 27/09/2022 by Bhandavi
                //Getting error while saving if image is not there (It is not null but empty in viewstate)
                // if (ViewState["Bytes"] != null) 
                if (ViewState["Bytes"].ToString() != "")
                {
                    objTrustBo.TrustLogo = (byte[])ViewState["Bytes"];
                    //objTrustBo.TrustLogo = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                }
                else
                {
                    //objTrustBo.TrustLogo = (byte[])ViewState["Bytes"];
                    objTrustBo.TrustLogo = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                }
                objTrustBo.ApprovalNo = txtApprovalNo.Text;
                objTrustBo.ApprovalDate = txtApprovalDate.Text;
                objTrustBo.AccountStartDate = txtAccStartDate.Text;
                objTrustBo.ApprovalYear = ddlApprovalYear.SelectedItem.ToString();
                objTrustBo.AddressEng = txtTrustAddress.Text;
                objTrustBo.AddressGuj = txtTrustAddressGuj.Text;
                objTrustBo.CountryEng = txtCountry.Text;
                objTrustBo.CountryGuj = txtCountryGuj.Text;
                objTrustBo.StateEng = txtState.Text;
                objTrustBo.StateGuj = txtStateGuj.Text;
                objTrustBo.DistrictEng = txtDistrict.Text;
                objTrustBo.DistrictGuj = txtDistrictGuj.Text;
                objTrustBo.TownEng = txtCity.Text;
                objTrustBo.TownGuj = txtcityGuj.Text;
                objTrustBo.Pincode = txtPincode.Text;
                objTrustBo.TelephoneNo = txtTelephone.Text;
                objTrustBo.MobileNo = txtMobileNo.Text;
                objTrustBo.EmailId = txtEmailID.Text;
                objTrustBo.AlternateEmailId = txtAlterID.Text;
                objTrustBo.FaxNo = txtFax.Text;
                objTrustBo.Website = txtWebsite.Text;
                objTrustBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objTrustBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);

                objBankBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objBankBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                if (ValidateName() == true)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert(Trust Name " + txtTrustName.Text + " Already Exists.');</script>");
                    goto Exit;
                }
                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();
                if (ViewState["Mode"].ToString() == "Save")
                {

                    objResults = objTrustBl.Trust_Insert(objTrustBo);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ViewState["TrustMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                            InsertBank();
                        }
                        ClearAll();
                        GridDataBind();
                        PanelGrid_VisibilityMode(1);
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Trust Created Successfully');</script>");
                    }
                }
                else
                {
                    objResults = objBankBl.BankAssociation_Delete_TrustMID(Convert.ToInt32(ViewState["TrustMID"].ToString()), 0);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                    }

                    InsertBank();
                    objTrustBo.TrustMID = Convert.ToInt32(ViewState["TrustMID"].ToString());
                    objResults = objTrustBl.Trust_Update(objTrustBo);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Trust updated successfully');</script>");


                        ClearAll();
                        objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                        ViewState["Mode"] = "Save";
                        btnSave.Text = "Save";
                        txtAccStartDate.Enabled = true;
                    }
                }
                DatabaseTransaction.CommitTransation();
                GridDataBind();
                #endregion
            Exit: ;
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region btnAddTrust Event
        protected void lnkAddNewTrust_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
            txtAbbreviation.Enabled = true;
            txtAccStartDate.Enabled = true;
        }
        #endregion

        #region btnBank Event For Add Bank
        protected void btnBank_Click(object sender, EventArgs e)
        {
            try
            {
                int intflagsave = 0;
                
                DataTable dtTrust = new DataTable();
                if (ViewState["dtBank"] == null)
                {

                    DataColumn column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.AutoIncrement = true;
                    column.AutoIncrementSeed = 1000;
                    column.AutoIncrementStep = 10;

                    dtTrust.Columns.Add("BankAssociationMID", typeof(int));
                    dtTrust.Columns.Add("TrustMID", typeof(int));
                    dtTrust.Columns.Add("SchoolMID", typeof(int));
                    dtTrust.Columns.Add("BankName", typeof(string));
                    dtTrust.Columns.Add("BranchName", typeof(string));
                    dtTrust.Columns.Add("AccountNameEng", typeof(string));
                    dtTrust.Columns.Add("AccountNameGuj", typeof(string));
                    dtTrust.Columns.Add("AccountNo", typeof(string));
                    dtTrust.Columns.Add("AccountType", typeof(string));
                    dtTrust.Columns.Add("IfscCode", typeof(string));
                    dtTrust.Columns.Add("PanNo", typeof(string));
                }
                else
                {
                    dtTrust = (DataTable)ViewState["dtBank"];
                    if (dtTrust.Rows.Count > 0)
                    {
                        string strFilter = "AccountNo = '" + txtAccountNo.Text.Trim() + "'";
                        DataRow[] results = dtTrust.Select(strFilter);
                        if (results.Length > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('AccountNo name already exist.');</script>");
                            txtAccountNo.Text = "";
                            intflagsave = 1;
                        }
                    }

                }
                if (intflagsave == 0)
                {
                dtTrust.Rows.Add(null, null, null, txtBankName.Text, txtBranchName.Text, txtAccountName.Text, txtAccountNameGuj.Text, txtAccountNo.Text, txtAccountType.Text, txtISFCCodes.Text, txtPanNo.Text);
                ViewState["dtBank"] = dtTrust;
                    if (dtTrust.Rows.Count > 0)
                    {
                            gvbankAssociation.DataSource = dtTrust;
                            gvbankAssociation.DataBind();

                            ViewState["dtBank"].ToString();
                    }
                }
                hfTab.Value = "4";
                clearBank();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "onBankDetailClick();", true);

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region Trust gridview RowCommand Event
        protected void gvTrust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                BindYear();
                ApplicationResult objResults = new ApplicationResult();
                TrustBL objTrustBl = new TrustBL();
                BankAssociationBL objBankBl = new BankAssociationBL();
                Controls objControls = new Controls();
                ViewState["TrustMID"] = Convert.ToInt32(e.CommandArgument.ToString());
                objResults = objTrustBl.Abbreviation_Validation(Convert.ToInt32(ViewState["TrustMID"].ToString()), 0, 0);

                TabGrid_VisibilityMode(true, false, false, false, false);

                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        txtAbbreviation.Enabled = false;
                    }
                    else
                    {
                        txtAbbreviation.Enabled = true;
                    }

                }


                if (e.CommandName.ToString() == "Edit1")
                {
                    // txtAccStartDate.Enabled = false;
                    objResults = objTrustBl.Trust_Select(Convert.ToInt32(ViewState["TrustMID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            txtTrustName.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_TRUSTNAMEENG].ToString();
                            txtTrustNameGuj.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_TRUSTNAMEGUJ].ToString();
                            txtRegCode.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_REGISTRATIONCODE].ToString();
                            txtAbbreviation.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_TRUSTABBREVIATION].ToString();
                            ViewState["Bytes"] = objResults.resultDT.Rows[0][TrustBO.TRUST_TRUSTLOGO];
                            if (ViewState["Bytes"].ToString() != "")
                            {
                                imgphoto.ImageUrl = "GetImage.ashx?TrustMID=" + ViewState["TrustMID"];

                            }
                            else
                            {
                                imgphoto.ImageUrl = "~/images/noimage-big.jpg";
                                //lbtnRemovePhoto.Visible = false;
                            }
                            txtApprovalNo.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_APPROVALNO].ToString();
                            txtApprovalDate.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_APPROVALDATE].ToString();
                            txtAccStartDate.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_ACCOUNTSTARTDATE].ToString();
                            if (objResults.resultDT.Rows[0][TrustBO.TRUST_APPROVALYEAR].ToString() != "-Select-" && objResults.resultDT.Rows[0][TrustBO.TRUST_APPROVALYEAR].ToString() != "")
                            {
                                ddlApprovalYear.SelectedValue = objResults.resultDT.Rows[0][TrustBO.TRUST_APPROVALYEAR].ToString();
                            }
                            txtTrustAddress.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_ADDRESSENG].ToString();
                            txtTrustAddressGuj.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_ADDRESSGUJ].ToString();
                            txtCountry.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_COUNTRYENG].ToString();
                            txtCountryGuj.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_COUNTRYGUJ].ToString();
                            txtState.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_STATEENG].ToString();
                            txtStateGuj.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_STATEGUJ].ToString();
                            txtDistrict.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_DISTRICTENG].ToString();
                            txtDistrictGuj.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_DISTRICTGUJ].ToString();
                            txtCity.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_TOWNENG].ToString();
                            txtcityGuj.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_TOWNGUJ].ToString();
                            txtPincode.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_PINCODE].ToString();
                            txtTelephone.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_TELEPHONENO].ToString();
                            txtMobileNo.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_MOBILENO].ToString();
                            txtEmailID.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_EMAILID].ToString();
                            txtAlterID.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_ALTERNATEEMAILID].ToString();
                            txtFax.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_FAXNO].ToString();
                            txtWebsite.Text = objResults.resultDT.Rows[0][TrustBO.TRUST_WEBSITE].ToString();

                            ApplicationResult objResultsEditBank = new ApplicationResult();
                            DataTable dtBankAssociation = new DataTable();
                            objResultsEditBank = objBankBl.BankAssociation_Select_TrustMID(Convert.ToInt32(ViewState["TrustMID"].ToString()), 0);
                            if (objResults != null)
                            {

                                dtBankAssociation = objResultsEditBank.resultDT;
                                if (dtBankAssociation.Rows.Count > 0)
                                {
                                    gvbankAssociation.Visible = true;

                                    gvbankAssociation.DataSource = dtBankAssociation;
                                    gvbankAssociation.DataBind();
                                }
                                ViewState["dtBank"] = dtBankAssociation;
                            }
                            ViewState["Mode"] = "Edit1";
                            PanelGrid_VisibilityMode(2);
                        }
                    }
                }

                else if ((e.CommandName.ToString() == "Delete"))
                {
                    objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));

                    objResults = objTrustBl.Validate_Trust_Delete(Convert.ToInt32(ViewState["TrustMID"].ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(objResults.resultDT.Rows[0]["TrustMID"]) == Convert.ToInt32(ViewState["TrustMID"].ToString()))
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('There are School(s) associated with this Trust. To delete this Trust you need to delete School(s) first.');</script>");
                                goto Exit;
                            }
                        }
                    }
                    ApplicationResult objResultsDelete = new ApplicationResult();
                    ApplicationResult objResultsBankDelete = new ApplicationResult();

                    objResultsDelete = objTrustBl.Trust_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('Trust deleted successfully.');</script>");
                        GridDataBind();
                    }
                }
            Exit: ;

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region BankAssociation Gridview RowDeleting Event
        protected void gvbankAssociation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        #endregion

        #region BankAssociation Gridview RowCommand Event
        protected void gvbankAssociation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.ToString() == "DeleteBank"))
                {
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    DataTable dtBank = (DataTable)ViewState["dtBank"];

                    dtBank.Rows.RemoveAt(row.RowIndex);

                    ViewState["dtBank"] = dtBank;
                    gvbankAssociation.DataSource = dtBank;
                    gvbankAssociation.DataBind();
                    clearBank();
                    hfTab.Value = "4";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region BankAssociation Gridview RowDataBound Event
        protected void gvbankAssociation_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        #endregion

        #region BankAssociation Gridview RowEditing Event
        protected void gvbankAssociation_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        #endregion

        #region Function for convert Image To ByteArray For FilePath
        public byte[] ImageToByteArrayFromFilePath(string imagefilePath)
        {
            try
            {
                byte[] imageArray = File.ReadAllBytes(Server.MapPath(imagefilePath));
                return imageArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region ValidateName
        public bool ValidateName()
        {
            TrustBL objTrustBl = new TrustBL();
            ApplicationResult objResults = new ApplicationResult();

            if (ViewState["Mode"].ToString() == "Save")
            {
                objResults = objTrustBl.Trust_ValidateName(txtTrustName.Text, txtAbbreviation.Text, -1);
            }
            else
            {
                objResults = objTrustBl.Trust_ValidateName(txtTrustName.Text, txtAbbreviation.Text, Convert.ToInt32(ViewState["TrustMID"].ToString()));
            }

            if (objResults.resultDT.Rows.Count > 0)

                return true;
            return false;

        }
        #endregion

        #region InsertBank
        public void InsertBank()
        {
            BankAssociationBO objBankBo = new BankAssociationBO();
            ApplicationResult objResultsBankInsert = new ApplicationResult();
            BankAssociationBL objBankBl = new BankAssociationBL();

            if (ViewState["dtBank"] != null)
            {
                DataTable dt = (DataTable)ViewState["dtBank"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objBankBo.TrustMID = Convert.ToInt32(ViewState["TrustMID"].ToString());
                    objBankBo.SchoolMID = 0;
                    objBankBo.BankName = dt.Rows[i]["BankName"].ToString();
                    objBankBo.AccountNameEng = dt.Rows[i]["AccountNameEng"].ToString();
                    objBankBo.AccountNameGuj = dt.Rows[i]["AccountNameGuj"].ToString();
                    objBankBo.AccountType = dt.Rows[i]["AccountType"].ToString();
                    objBankBo.AccountNo = dt.Rows[i]["AccountNo"].ToString();
                    objBankBo.BranchName = dt.Rows[i]["BranchName"].ToString();
                    objBankBo.PanNO = dt.Rows[i]["PanNO"].ToString();
                    objBankBo.IfscCode = dt.Rows[i]["IfscCode"].ToString();
                    objBankBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objBankBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    objResultsBankInsert = objBankBl.BankAssociation_Insert(objBankBo);
                    if (objResultsBankInsert.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {


                    }

                }
            }
        }
        #endregion

        #region Clear Bank textboxes
        public void clearBank()
        {
            txtAccountName.Text = "";
            txtAccountNameGuj.Text = "";
            txtAccountType.Text = "";
            txtBankName.Text = "";
            txtAccountNo.Text = "";
            txtBranchName.Text = "";
            txtPanNo.Text = "";
            txtISFCCodes.Text = "";

        }
        #endregion

        #region PanelGrid_VisibilityMode

        //panel grid true & false according to condition
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                gvTrust.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
                //  lnkAddNewTrust.Visible = false;
            }
            else if (intMode == 2)
            {
                hfTab.Value = "0";
                gvTrust.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                // lnkAddNewTrust.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Tab Grid_VisibilityMode

        //panel grid true & false according to condition
        private void TabGrid_VisibilityMode(bool b1, bool b2, bool b3, bool b4, bool b5)
        {
            //tabTrustDetails.Visible = b1;
            //tabAddress.Visible = b2;
            //tabGujaratiDetails.Visible = b3;
            //tabContactDetails.Visible = b4;
            //tabBankDetails.Visible = b5;
        }

        #endregion Tab Grid_VisibilityMode

        #region Restrict New Trust
        public void RestrictNewTrust()
        {
            try
            {
                TrustBL objTrustBl = new TrustBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objTrustBl.Trust_SelectAll();

                if (objResult.resultDT.Rows.Count > 0)
                {
                    //lnkAddNewTrust.Visible = false;
                }
                else
                {
                    //lnkAddNewTrust.Visible = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear all
        private void ClearAll()
        {
            ViewState["Bytes"] = null;
            ViewState["strPhoto"] = null;
            ViewState["Mode"] = "Save";
            ViewState["dtBank"] = null;
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
           
        }

        #endregion

        protected void ConfirmDeleteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            if (chk.Checked)
            {
                foreach (GridViewRow row in gvbankAssociation.Rows)
                {
                    var deleteButton = row.FindControl("ImageButton4") as ImageButton;
                    deleteButton.Visible = true;
                    TabGrid_VisibilityMode(false,false,false,false,true);
                }
             
            }
            else {
                foreach (GridViewRow row in gvbankAssociation.Rows)
                {
                    var deleteButton = row.FindControl("ImageButton4") as ImageButton;
                    deleteButton.Visible = false;
                    TabGrid_VisibilityMode(false, false, false, false, true);
                }
            
            }
           
        }

        protected void gvbankAssociation_RowCreated(object sender, GridViewRowEventArgs e)
        {

            //foreach (GridViewRow row in gvbankAssociation.Rows)
            //{
            //    var deleteButton = row.FindControl("ImageButton4") as ImageButton;
            //    deleteButton.Visible = false;
            //}


        }
    }
}