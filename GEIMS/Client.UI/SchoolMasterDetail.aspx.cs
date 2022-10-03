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

    public partial class SchoolMasterDetail : PageBase
    {

        private static ILog logger = LogManager.GetLogger(typeof(SchoolMasterDetail));
        #region PageLoad Event
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
                    txtAbbreviation.Enabled = true;
                    txtAccStartDate.Attributes.Add("readonly", "readonly");
                    BindYear();
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    ViewState["SchoolMID"] = 0;
                    hfTab.Value = "0";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
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
                    if (System.IO.File.Exists(Server.MapPath("~/Logo/SchoolLogo/") + txtSchoolName.Text.Replace(" ", "").ToString() + Extension))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Logo/SchoolLogo/") + txtSchoolName.Text.Replace(" ", "").ToString() + Extension);
                    }
                    fuImage.PostedFile.SaveAs(Server.MapPath("~/Logo/SchoolLogo/") + txtSchoolName.Text.Replace(" ", "").ToString() + Extension);
                    //objSchoolBo.SchoolLogo = txtSchoolName.Text.Replace(" ", "").ToString() + Extension;
                    imgphoto.ImageUrl = "~/Logo/SchoolLogo/" + txtSchoolName.Text.Replace(" ", "").ToString() + Extension;
                   // ViewState["strPhoto"] = "../Logo/SchoolLogo/" + txtSchoolName.Text.Replace(" ", "").ToString() + Extension;
                    Byte[] bytes = (byte[])ImageToByteArrayFromFilePath("~/Logo/SchoolLogo/" + txtSchoolName.Text.Replace(" ", "").ToString() + Extension);
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

        #region BindYear
        public void BindYear()
        {
            try
            {
                int currentYear = DateTime.Now.Year;
                for (int i = 1990; i <= currentYear; i++)
                {
                    ddlConstructionYear.Items.Add(Convert.ToString(i));

                }
                ddlConstructionYear.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region Bind grid School
        private void GridDataBind()
        {
            try
            {

                ApplicationResult objResult = new ApplicationResult();
                SchoolBL objSchoolBl = new SchoolBL();

                objResult = objSchoolBl.School_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                   if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvSchool.DataSource = objResult.resultDT;
                        gvSchool.DataBind();
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

       

        #region Save Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResults = new ApplicationResult();
                SchoolBO objSchoolBo = new SchoolBO();
                SchoolBL objSchoolBl = new SchoolBL();
                BankAssociationBL objBankBl = new BankAssociationBL();
                DataTable dt = new DataTable();
                Controls objControls = new Controls();

                objSchoolBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                //objSchoolBo.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                objSchoolBo.SchoolNameEng = txtSchoolName.Text;
                objSchoolBo.SchoolNameGuj = txtSchoolNameGuj.Text;
                objSchoolBo.SchoolTiming = txtSchoolTiming.Text;
                objSchoolBo.AcademicMonth =Convert.ToInt32(ddlAcademicMonth.SelectedValue);
                objSchoolBo.SchoolCode = txtSchoolCode.Text;
                if (ViewState["Bytes"] != null)
                {
                    objSchoolBo.SchoolLogo = (byte[])ViewState["Bytes"];
                   //objSchoolBo.SchoolLogo = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                }
                else
                {
                    //objSchoolBo.SchoolLogo = (byte[])ViewState["Bytes"];
                    objSchoolBo.SchoolLogo = ImageToByteArrayFromFilePath("../Images/NoImage-big.jpg");
                }
                objSchoolBo.SchoolAbbreviation = txtAbbreviation.Text;
                objSchoolBo.AccountStartDate = txtAccStartDate.Text;
                objSchoolBo.AddressEng = txtSchoolAddress.Text;
                objSchoolBo.AddressGuj = txtSchoolAddressGuj.Text;
                if (rblAreaType.SelectedItem != null)
                {
                    objSchoolBo.AreaType = rblAreaType.SelectedItem.Text;
                    if (objSchoolBo.AreaType == "Village Area")
                    {
                        objSchoolBo.AreaTypeGuj = "ગ્રામ્ય પંચાયત";
                    }
                    else if (objSchoolBo.AreaType == "Town Area")
                    {
                        objSchoolBo.AreaTypeGuj = "નગર પંચાયત";
                    }
                    else if (objSchoolBo.AreaType == "City Area")
                    {
                        objSchoolBo.AreaTypeGuj = "નગર પાલિકા";
                    }
                    else if (objSchoolBo.AreaType == "Notified Area")
                    {
                        objSchoolBo.AreaTypeGuj = "સૂચિત વિસ્તાર";
                    }
                }
                else
                {
                    objSchoolBo.AreaType = "";
                    objSchoolBo.AreaTypeGuj = "";
                }
                if (rblAreaSubType.SelectedItem != null)
                {
                    objSchoolBo.AreaSubType = rblAreaSubType.SelectedItem.ToString();
                    if (objSchoolBo.AreaSubType == "Non Adivasi Area")
                    {
                        objSchoolBo.AreaSubTypeGuj = "બિન આદિવાસી વિસ્તાર";
                    }
                    else if (objSchoolBo.AreaSubType == "Economically Backward Area")
                    {
                        objSchoolBo.AreaSubTypeGuj = "આર્થિક રીતે પછાત વિસ્તાર";
                    }
                    else if (objSchoolBo.AreaSubType == "Frontier Area")
                    {
                        objSchoolBo.AreaSubTypeGuj = "આર્થિક સરહદી વિસ્તાર";
                    }
                    //objSchoolBo.AreaSubTypeGuj = rblAreaSubType.SelectedValue.ToString();
                }
                else
                {
                    objSchoolBo.AreaSubType = "";
                    objSchoolBo.AreaSubTypeGuj = "";
                }

                objSchoolBo.TownEng = txtTown.Text;
                objSchoolBo.TownGuj = txtTownGuj.Text;
                objSchoolBo.AtPoNoEng = txtATPOEng.Text;
                objSchoolBo.AtPoNoGuj = txtATPOGuj.Text;
                objSchoolBo.TalukaEng = txtTalukaEng.Text;
                objSchoolBo.TalukaGuj = txttalukaGuj.Text;
                objSchoolBo.DistrictEng = txtDistrict.Text;
                objSchoolBo.DistrictGuj = txtDistrictGuj.Text;
                objSchoolBo.StateEng = txtState.Text;
                objSchoolBo.StateGuj = txtStateGuj.Text;
                objSchoolBo.CountryEng = txtCountry.Text;
                objSchoolBo.CountryGuj = txtCountryGuj.Text;
                objSchoolBo.Pincode = txtPinCode.Text;
                objSchoolBo.TelephoneNo = txtTelephoneNo.Text;
                objSchoolBo.MobileNo = txtMobileNo.Text;
                objSchoolBo.EmailID = txtEmailID.Text;
                objSchoolBo.AlternateEmailID = txtAlternateEmail.Text;
                objSchoolBo.FaxNo = txtFax.Text;
                objSchoolBo.Website = txtWebsite.Text;
                objSchoolBo.SSCindexNo = txtSSCIndexNo.Text;
                objSchoolBo.HSCScienceIndexNo = txtScienceIndexNo.Text;
                objSchoolBo.HSCCommerceIndexNo = txtCommerceIndexNo.Text;
                objSchoolBo.HSCArtsIndexNo = txtArtsIndexNo.Text;
                objSchoolBo.RegistrationCode = txtRegistrationCode.Text;
                objSchoolBo.RegisteredNameEng = txtRegistrationName.Text;
                objSchoolBo.RegistreredNameGuj = txtRegistrationCodeGuj.Text;
                // objSchoolBo.RegisteredAddressEng = txtRegisteredAddress.Text;
                objSchoolBo.RegisteredAddressGuj = txtRegisteredddressGuj.Text;
                objSchoolBo.SchoolMottoEng = txtSchoolMotto.Text;
                objSchoolBo.SchoolMottoGuj = txtSchoolMottoGuj.Text;
                objSchoolBo.SchoolVisionEng = txtSchoolVision.Text;
                objSchoolBo.SchoolVisionGuj = txtSchoolVisionGuj.Text;
                if (chkIsRent.Checked == true)
                {
                    objSchoolBo.IsOnRent = 1;
                }
                else
                {
                    objSchoolBo.IsOnRent = 0;
                }
                objSchoolBo.OwnerNameEng = txtOwnerName.Text;
                objSchoolBo.OwnerNameGuj = txtOwnerNameGuj.Text;
                objSchoolBo.OwnerAddressEng = txtOwnerAddress.Text;
                objSchoolBo.OwnerAddressGuj = txtOwnerAddressGuj.Text;
                objSchoolBo.WordNo = txtWordNo.Text;
                objSchoolBo.WordNameEng = txtWordName.Text;
                objSchoolBo.WordNameGuj = txtWardNameGuj.Text;
                objSchoolBo.PlotNo = txtPlotNo.Text;
                objSchoolBo.PlotArea = txtPlotArea.Text;
                objSchoolBo.ConstrunctionYear = ddlConstructionYear.SelectedItem.ToString();
                objSchoolBo.NoOfFloors = txtNoOfFloor.Text;
                objSchoolBo.AuditList = "";

                objSchoolBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                objSchoolBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                if (ValidateName() == true)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('School Name " + txtSchoolName.Text + " Already Exists.');</script>");
                    goto Exit;
                }
                #region RollBack Transaction Starts

                DatabaseTransaction.OpenConnectionTransation();
                if (ViewState["Mode"].ToString() == "Save")
                {
                    objResults = objSchoolBl.School_Insert(objSchoolBo);
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            ViewState["SchoolMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                            InsertBank();
                           
                            PanelGrid_VisibilityMode(1);
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('School Created Successfully.');</script>");
                            ClearAll();
                           
                            
                        }

                    }
                }
                else
                {
                  
                    objResults = objBankBl.BankAssociation_Delete_TrustMID(0, Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                    }

                    InsertBank();
                    objSchoolBo.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                    objSchoolBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objResults = objSchoolBl.School_Update(objSchoolBo);
                    if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('School updated successfully');</script>");

                       // GridDataBind();

                        objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
                        ViewState["Mode"] = "Save";
                        btnSave.Text = "Save";
                    }
                }

                DatabaseTransaction.CommitTransation();
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Clear();", true);
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

        #region Add New Event
        protected void lnkAddNewSchool_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            PanelGrid_VisibilityMode(2);
            ClearAll();
            gvbankAssociation.Visible = false;
            txtAbbreviation.Enabled = true;
            txtAccStartDate.Enabled = true;
        }
        #endregion

        #region ViewList Click Event
        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["SchoolMID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region ClearBank Textboxes
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

        #region btnBank Event For Save Bank Detail
        protected void btnBank_Click(object sender, EventArgs e)
        {
            try
            {
                int intflagsave = 0;
                DataTable dtSchool = new DataTable();
                if (ViewState["dtBank"] == null)
                {
                    DataColumn column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.AutoIncrement = true;
                    column.AutoIncrementSeed = 1000;
                    column.AutoIncrementStep = 10;


                    dtSchool.Columns.Add("BankAssociationMID", typeof(int));
                    dtSchool.Columns.Add("TrustMID", typeof(int));
                    dtSchool.Columns.Add("SchoolMID", typeof(int));
                    dtSchool.Columns.Add("BankName", typeof(string));
                    dtSchool.Columns.Add("BranchName", typeof(string));
                    dtSchool.Columns.Add("AccountNameEng", typeof(string));
                    dtSchool.Columns.Add("AccountNameGuj", typeof(string));
                    dtSchool.Columns.Add("AccountNo", typeof(string));
                    dtSchool.Columns.Add("AccountType", typeof(string));
                    dtSchool.Columns.Add("IfscCode", typeof(string));
                    dtSchool.Columns.Add("PanNo", typeof(string));
                }
                else
                {
                    dtSchool = (DataTable)ViewState["dtBank"];
                    if (dtSchool.Rows.Count > 0)
                    {
                        string strFilter = "AccountNo = '" + txtAccountNo.Text.Trim() + "'";
                        DataRow[] results = dtSchool.Select(strFilter);
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
                    dtSchool.Rows.Add(null, null, null, txtBankName.Text, txtBranchName.Text, txtAccountName.Text, txtAccountNameGuj.Text, txtAccountNo.Text, txtAccountType.Text, txtISFCCodes.Text, txtPanNo.Text);

                    ViewState["dtBank"] = dtSchool;


                    if (dtSchool.Rows.Count > 0)
                    {
                        gvbankAssociation.Visible = true;
                        gvbankAssociation.DataSource = dtSchool;
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



        #region School RowCommand Event
        protected void gvSchool_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                SchoolBO objSchoolBo = new SchoolBO();
                SchoolBL objSchoolBl = new SchoolBL();
                TrustBL objTrustBl = new TrustBL();
                ApplicationResult objResults = new ApplicationResult();
                BankAssociationBL objBankBl = new BankAssociationBL();
                Controls objControls = new Controls();

                ViewState["SchoolMID"] = Convert.ToInt32(e.CommandArgument.ToString());

                objResults = objTrustBl.Abbreviation_Validation(0,Convert.ToInt32(ViewState["SchoolMID"].ToString()), 0);

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
                objSchoolBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                if (e.CommandName.ToString() == "Edit1")
                {
                   // txtAccStartDate.Enabled = false;
                    objResults = objSchoolBl.School_Select(Convert.ToInt32(ViewState["SchoolMID"].ToString()));

                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {

                            txtSchoolName.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLNAMEENG].ToString();
                            txtSchoolNameGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLNAMEGUJ].ToString();
                            txtSchoolTiming.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLTIMING].ToString();
                            ddlAcademicMonth.SelectedValue = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACADEMICMONTH].ToString();
                            txtSchoolCode.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLCODE].ToString();
                            ViewState["Bytes"] = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLLOGO];
                            if (ViewState["Bytes"].ToString() != "")
                            {
                                imgphoto.ImageUrl = "GetImage.ashx?SchoolMID=" + ViewState["SchoolMID"];

                            }
                            else
                            {
                                imgphoto.ImageUrl = "~/images/noimage-big.jpg";
                                //lbtnRemovePhoto.Visible = false;
                            }
                            txtAbbreviation.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLABBREVIATION].ToString();
                            txtAccStartDate.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ACCOUNTSTARTDATE].ToString();
                            txtSchoolAddress.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ADDRESSENG].ToString();
                            txtSchoolAddressGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ADDRESSGUJ].ToString();
                            if (objResults.resultDT.Rows[0][SchoolBO.SCHOOL_AREATYPE].ToString() == "")
                            {
                                //rblAreaType.SelectedItem.Text = "";
                            }
                            else
                            {
                                rblAreaType.SelectedValue= objResults.resultDT.Rows[0][SchoolBO.SCHOOL_AREATYPE].ToString();
                            }
                            if (objResults.resultDT.Rows[0][SchoolBO.SCHOOL_AREASUBTYPE].ToString() == "")
                            {
                                //rblAreaSubType.SelectedItem.Text = "";
                            }
                            else
                            {
                                rblAreaSubType.SelectedValue = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_AREASUBTYPE].ToString();
                            }

                            txtTown.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_TOWNENG].ToString();
                            txtTownGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_TOWNGUJ].ToString();
                            txtATPOEng.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ATPONOENG].ToString();
                            txtATPOGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ATPONOGUJ].ToString();
                            txtTalukaEng.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_TALUKAENG].ToString();
                            txttalukaGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_TALUKAGUJ].ToString();
                            txtDistrict.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_DISTRICTENG].ToString();
                            txtDistrictGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_DISTRICTGUJ].ToString();
                            txtState.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_STATEENG].ToString();
                            txtStateGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_STATEGUJ].ToString();
                            txtCountry.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_COUNTRYENG].ToString();
                            txtCountryGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_COUNTRYGUJ].ToString();
                            txtPinCode.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_PINCODE].ToString();
                            txtTelephoneNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_TELEPHONENO].ToString();
                            txtMobileNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_MOBILENO].ToString();
                            txtEmailID.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_EMAILID].ToString();
                            txtAlternateEmail.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ALTERNATEEMAILID].ToString();
                            txtFax.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_FAXNO].ToString();
                            txtWebsite.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_WEBSITE].ToString();
                            txtSSCIndexNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SSCINDEXNO].ToString();
                            txtCommerceIndexNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_HSCCOMMERCEINDEXNO].ToString();
                            txtScienceIndexNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_HSCSCIENCEINDEXNO].ToString();
                            txtArtsIndexNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_HSCARTSINDEXNO].ToString();
                            txtRegistrationCode.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_REGISTRATIONCODE].ToString();
                            txtRegistrationName.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_REGISTEREDNAMEENG].ToString();
                            txtRegistrationCodeGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_REGISTREREDNAMEGUJ].ToString();
                            // txtRegisteredAddress.Text = dtSchool.Rows[0][SchoolBO.SCHOOL_REGISTEREDADDRESSENG].ToString();
                            txtRegisteredddressGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_REGISTEREDADDRESSGUJ].ToString();
                            txtSchoolMotto.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLMOTTOENG].ToString();
                            txtSchoolMottoGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLMOTTOGUJ].ToString();
                            txtSchoolVision.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLVISIONENG].ToString();
                            txtSchoolVisionGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_SCHOOLVISIONGUJ].ToString();
                            if (Convert.ToInt32(objResults.resultDT.Rows[0][SchoolBO.SCHOOL_ISONRENT].ToString()) == 1)
                            {
                                chkIsRent.Checked = true;
                            }
                            else
                            {
                                chkIsRent.Checked = false;
                            }
                            txtOwnerName.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_OWNERNAMEENG].ToString();
                            txtOwnerNameGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_OWNERNAMEGUJ].ToString();
                            txtOwnerAddress.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_OWNERADDRESSENG].ToString();
                            txtOwnerAddressGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_OWNERADDRESSGUJ].ToString();
                            txtWordNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_WORDNO].ToString();
                            txtWordName.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_WORDNAMEENG].ToString();
                            txtWardNameGuj.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_WORDNAMEGUJ].ToString();
                            txtPlotNo.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_PLOTNO].ToString();
                            txtPlotArea.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_PLOTAREA].ToString();
                            ddlConstructionYear.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_CONSTRUNCTIONYEAR].ToString();
                            txtNoOfFloor.Text = objResults.resultDT.Rows[0][SchoolBO.SCHOOL_NOOFFLOORS].ToString();

                            ApplicationResult objResultsBankEdit = new ApplicationResult();
                            DataTable dtBankAssociation = new DataTable();
                            objResultsBankEdit = objBankBl.BankAssociation_Select_TrustMID(0, Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                            if (objResultsBankEdit != null)
                            {

                                dtBankAssociation = objResultsBankEdit.resultDT;
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
                else if ((e.CommandName.ToString() == "Delete1"))
                {

                    objControls.EnableControls(Master.FindControl("ContentPlaceHolder1"));

                    objResults = objSchoolBl.Validate_School_Delete(Convert.ToInt32(ViewState["SchoolMID"].ToString()));
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(objResults.resultDT.Rows[1]["SchoolMID"]) == Convert.ToInt32(ViewState["SchoolMID"].ToString()))
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('There are Section(s)/Student(s) associated with this School. To delete this School you need to delete Section(s)/Student(s) first.');</script>");
                                goto Exit;
                            }
                        }
                    }
                    ApplicationResult objResultsDelete = new ApplicationResult();
                    ApplicationResult objResultsBankDelete = new ApplicationResult();
                    objResultsDelete = objSchoolBl.School_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {

                        ClearAll();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='JavaScript'>alert('School deleted successfully.');</script>");
                        GridDataBind();
                    }

                }

            Exit: ;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bank Association Gridview RowCommand
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

        #region Bank Association Gridview RowDataBound
        protected void gvbankAssociation_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        #endregion

        #region Bank Association Gridview RowDeleting
        protected void gvbankAssociation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        #endregion

        #region Bank Association Gridview RowEditing
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
            SchoolBL objSchoolBl = new SchoolBL();
            ApplicationResult objResults = new ApplicationResult();
            if (ViewState["Mode"].ToString() == "Save")
            {
                objResults = objSchoolBl.School_ValidateName_SchoolName(txtSchoolName.Text,txtAbbreviation.Text, -1);
            }
            else
            {
                objResults = objSchoolBl.School_ValidateName_SchoolName(txtSchoolName.Text,txtAbbreviation.Text, Convert.ToInt32(ViewState["SchoolMID"].ToString()));
            }
            if (objResults.resultDT.Rows.Count > 0)

                return true;
            return false;

        }
        #endregion

        #region Clear all
        private void ClearAll()
        {

            ViewState["Mode"] = "Save";
            ViewState["dtBank"] = null;
            ViewState["SchoolMID"] = 0;
            ViewState["Bytes"] = null;
            ViewState["strPhoto"] = null;
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            gvbankAssociation.Visible = false;
            rblAreaSubType.SelectedIndex = -1;
        }
        #endregion

        #region Insert Bank
        public void InsertBank()
        {
            try
            {
                ApplicationResult objResultsBankInsert = new ApplicationResult();
                DataTable dt = (DataTable)ViewState["dtBank"];
                BankAssociationBO objBankBo = new BankAssociationBO();
                BankAssociationBL objBankBl = new BankAssociationBL();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objBankBo.SchoolMID = Convert.ToInt32(ViewState["SchoolMID"].ToString());
                        objBankBo.TrustMID = 0;
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
                ViewState["dtBank"] = "";
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }

        }
        #endregion

        #region PanelGrid_VisibilityMode

        //panel grid true & false according to condition
        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                gvSchool.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
                lnkAddNewSchool.Visible = true;
            }
            else if (intMode == 2)
            {
                hfTab.Value = "0";
                gvSchool.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
                lnkAddNewSchool.Visible = true;
            }
        }

        #endregion
    }
}