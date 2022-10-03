using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.DataAccess;
using System.Data;

namespace GEIMS.ReportUI
{
    public partial class SchoolAcoountingInfoReport : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(SchoolAcoountingInfoReport));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("../UserLogin.aspx");
                }
                if (!Page.IsPostBack)
                {
                    divReport.Visible = false;
                    divTv.Visible = false;
                    divNSS.Visible = false;
                    divCareer.Visible = false;
                    divEco.Visible = false;
                    BindSchool();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            divReport.Visible = false;
            pnlStudentInfo.Visible = true;

        }
        #endregion

        #region Go button Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                BindgvReport();
                //divReport.Visible = true;
                //pnlStudentInfo.Visible = false;

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Bind GridView
        public void BindgvReport()
        {
            SchoolBL objSchoolBL = new SchoolBL();
            ApplicationResult objResult = new ApplicationResult();
            objResult = objSchoolBL.School_AccountingInfoReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchool.SelectedValue),0);
            if (objResult != null)
            {
                dlSchoolInfoGujarati.DataSource = null;
                dlSchoolInfoGujarati1.DataSource = null;

                if (objResult.resultDT.Rows.Count > 0)
                {
                    dlSchoolInfoGujarati.Visible = true;
                    dlSchoolInfoGujarati1.Visible = true;
                }
                else
                {
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found.');", true);
                }
            }

            dlSchoolInfoGujarati.DataSource = objResult.resultDT;
            dlSchoolInfoGujarati.DataBind();

            dlStaffInfo.DataSource = objResult.resultDT;
            dlStaffInfo.DataBind();

            dlStaffInfo1.DataSource = objResult.resultDT;
            dlStaffInfo1.DataBind();


            dlSchoolInfoGujarati1.DataSource = objResult.resultDT;
            dlSchoolInfoGujarati1.DataBind();
           
          
            //Bind PrePrimary Datalist

            objResult = objSchoolBL.School_AccountingInfoReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchool.SelectedValue), 1);
            if (objResult != null)
            {
                dlPrePrimary.DataSource = null;
                dlPrePrimary1.DataSource = null;
                if (objResult.resultDT.Rows.Count > 0)
                {
                    dlPrePrimary.Visible = true;
                    dlPrePrimary1.Visible = true;
                }
                else
                {
                    dlPrePrimary.Visible = false;
                    dlPrePrimary1.Visible = false;
                }
            }
            dlPrePrimary.DataSource = objResult.resultDT;
            dlPrePrimary.DataBind();
            dlPrePrimary1.DataSource = objResult.resultDT;
           dlPrePrimary1.DataBind();

            //Bind Primary Datalist

            objResult = objSchoolBL.School_AccountingInfoReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchool.SelectedValue), 2);
            if (objResult != null)
            {
                dlPrimary.DataSource = null;
                dlPrimary1.DataSource = null;
                if (objResult.resultDT.Rows.Count > 0)
                {
                    dlPrimary.Visible = true;
                    dlPrimary1.Visible = true;
                }
                else
                {
                    dlPrimary.Visible = false;
                    dlPrimary1.Visible = false;
                }
            }
            dlPrimary.DataSource = objResult.resultDT;
            dlPrimary.DataBind();
            dlPrimary1.DataSource = objResult.resultDT;
            dlPrimary1.DataBind();
            //Bind Secondary Datalist

            objResult = objSchoolBL.School_AccountingInfoReport(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(ddlSchool.SelectedValue), 3);
            if (objResult != null)
            {
                dlSecondary.DataSource = null;
                dlSecondary1.DataSource = null;
                if (objResult.resultDT.Rows.Count > 0)
                {
                    dlSecondary.Visible = true;
                    dlSecondary1.Visible = true;
                }
                else
                {
                    dlSecondary.Visible = false;
                    dlSecondary1.Visible = false;
                }
            }
            dlSecondary.DataSource = objResult.resultDT;
            dlSecondary.DataBind();
           dlSecondary1.DataSource = objResult.resultDT;
            dlSecondary1.DataBind();

          
        }

        #endregion

        #region Back Button Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Method to bind School
        private void BindSchool()
        {
            SchoolBL objSchoolBL = new SchoolBL();
            Controls objControls = new Controls();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objSchoolBL.School_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResults != null)
            {
                if (objResults.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResults.resultDT, ddlSchool, "SchoolNameEng", "SchoolMID");

                }
                ddlSchool.Items.Insert(0, new ListItem("-Select-", ""));

            }
        }
        #endregion

        #region Print Button Event
        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "getPrint('divTransferGujaratiPrint');", true);

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Item DataBound Event

        protected void dlSchoolInfoGujarati_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSVSName = (Label)e.Item.FindControl("lblSVSName");
                Label lblQdcName = (Label)e.Item.FindControl("lblQdcName");
                Label lblPramukhName = (Label)e.Item.FindControl("lblPramukhName");
                Label lblPramukhNo = (Label)e.Item.FindControl("lblPramukhNo");
                Label lblClerkName = (Label)e.Item.FindControl("lblClerkName");
                Label lblClerkNo = (Label)e.Item.FindControl("lblClerkNo");
                Label lblComputerNo = (Label)e.Item.FindControl("lblComputerNo");
                Label lblGroundSize = (Label)e.Item.FindControl("lblGroundSize");
                Label lblUrinaryRoom = (Label)e.Item.FindControl("lblUrinaryRoom");
                Label lblFireNo = (Label)e.Item.FindControl("lblFireNo");
                Label lblTV = (Label)e.Item.FindControl("lblTV");
                Label lblTVNo = (Label)e.Item.FindControl("lblTVNo");
                Label lblLCDNo = (Label)e.Item.FindControl("lblLCDNo");
                Label lblEco = (Label)e.Item.FindControl("lblEco");
                Label lblEcoNo = (Label)e.Item.FindControl("lblEcoNo");
                Label llbEcoName = (Label)e.Item.FindControl("llbEcoName");
                Label lblNSS = (Label)e.Item.FindControl("lblNSS");
                Label lblNSSNo = (Label)e.Item.FindControl("lblNSSNo");
                Label lblNssName = (Label)e.Item.FindControl("lblNssName");
                Label lblCareer = (Label)e.Item.FindControl("lblCareer");
                Label lblCareerCorner = (Label)e.Item.FindControl("lblCareerCorner");
                Label lblCareerCornerName = (Label)e.Item.FindControl("lblCareerCornerName");
                Label lblBuldingInfo = (Label)e.Item.FindControl("lblBuldingInfo");
                Label lblRoomNo = (Label)e.Item.FindControl("lblRoomNo");
                Label llbRoomSize = (Label)e.Item.FindControl("llbRoomSize");

                lblSVSName.Text = txtSVSName.Text;
                lblQdcName.Text = txtQDCName.Text;
                lblPramukhName.Text = txtPramukhName.Text;
                lblPramukhNo.Text = lblPramukhNo.Text;
                lblClerkName.Text = txtClerkName.Text;
                lblClerkNo.Text = txtClerkNo.Text;
                lblComputerNo.Text = txtComputerNo.Text;
                lblGroundSize.Text = txtGroundSize.Text;
                lblUrinaryRoom.Text = txtUrinaryRoomNo.Text;
                lblFireNo.Text = txtFireSaftyNo.Text;
                if (chkTv.Checked == true)
                {
                    lblTV.Text = "હા છે.";
                    lblTVNo.Text = txtTV.Text;
                    lblLCDNo.Text = txtLCDNo.Text;
                }
                else
                {
                    lblTV.Text = "ના નથી.";
                    lblTVNo.Text = "0";
                    lblLCDNo.Text = "0";
                }
                if (chkEco.Checked == true)
                {
                    lblEco.Text = "હા છે.";
                    lblEcoNo.Text = txtEcoNo.Text;
                    llbEcoName.Text = txtEcoName.Text;
                }
                else
                {
                    lblEco.Text = "ના નથી.";
                    lblEcoNo.Text = "0";
                    llbEcoName.Text = "0";
                }
                if (chkNSS.Checked == true)
                {
                    lblNSS.Text = "હા છે.";
                    lblNSSNo.Text = txtNSSNO.Text;
                    lblNssName.Text = txtNSSName.Text;
                }
                else
                {
                    lblNSS.Text = "ના નથી.";
                    lblNSSNo.Text = "0";
                    lblNssName.Text = "0";
                }
                if (chkCareer.Checked == true)
                {
                    lblCareer.Text = "હા છે.";
                    lblCareerCorner.Text = txtCareerNo.Text;
                    lblCareerCornerName.Text = txtCareerName.Text;
                }
                else
                {
                    lblCareer.Text = "ના નથી.";
                    lblCareerCorner.Text = "0";
                    lblCareerCornerName.Text = "0";
                }
                lblBuldingInfo.Text = txtBuildingInfo.Text;
                lblRoomNo.Text = txtRoomNo.Text;
                llbRoomSize.Text = txtRoomSize.Text;
            }
        }

        protected void dlSchoolInfoGujarati1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSVSName = (Label)e.Item.FindControl("lblSVSName1");
                Label lblQdcName = (Label)e.Item.FindControl("lblQdcName1");
                Label lblPramukhName = (Label)e.Item.FindControl("lblPramukhName1");
                Label lblPramukhNo = (Label)e.Item.FindControl("lblPramukhNo1");
                Label lblClerkName = (Label)e.Item.FindControl("lblClerkName1");
                Label lblClerkNo = (Label)e.Item.FindControl("lblClerkNo1");
                Label lblComputerNo = (Label)e.Item.FindControl("lblComputerNo1");
                Label lblGroundSize = (Label)e.Item.FindControl("lblGroundSize1");
                Label lblUrinaryRoom = (Label)e.Item.FindControl("lblUrinaryRoom1");
                Label lblFireNo = (Label)e.Item.FindControl("lblFireNo1");
                Label lblTV = (Label)e.Item.FindControl("lblTV1");
                Label lblTVNo = (Label)e.Item.FindControl("lblTVNo1");
                Label lblLCDNo = (Label)e.Item.FindControl("lblLCDNo1");
                Label lblEco = (Label)e.Item.FindControl("lblEco1");
                Label lblEcoNo = (Label)e.Item.FindControl("lblEcoNo1");
                Label llbEcoName = (Label)e.Item.FindControl("llbEcoName1");
                Label lblNSS = (Label)e.Item.FindControl("lblNSS1");
                Label lblNSSNo = (Label)e.Item.FindControl("lblNSSNo1");
                Label lblNssName = (Label)e.Item.FindControl("lblNssName1");
                Label lblCareer = (Label)e.Item.FindControl("lblCareer1");
                Label lblCareerCorner = (Label)e.Item.FindControl("lblCareerCorner1");
                Label lblCareerCornerName = (Label)e.Item.FindControl("lblCareerCornerName1");
                Label lblBuldingInfo = (Label)e.Item.FindControl("lblBuldingInfo1");
                Label lblRoomNo = (Label)e.Item.FindControl("lblRoomNo1");
                Label llbRoomSize = (Label)e.Item.FindControl("llbRoomSize1");

                lblSVSName.Text = txtSVSName.Text;
                lblQdcName.Text = txtQDCName.Text;
                lblPramukhName.Text = txtPramukhName.Text;
                lblPramukhNo.Text = lblPramukhNo.Text;
                lblClerkName.Text = txtClerkName.Text;
                lblClerkNo.Text = txtClerkNo.Text;
                lblComputerNo.Text = txtComputerNo.Text;
                lblGroundSize.Text = txtGroundSize.Text;
                lblUrinaryRoom.Text = txtUrinaryRoomNo.Text;
                lblFireNo.Text = txtFireSaftyNo.Text;
                if (chkTv.Checked == true)
                {
                    lblTV.Text = "હા છે.";
                    lblTVNo.Text = txtTV.Text;
                    lblLCDNo.Text = txtLCDNo.Text;
                }
                else
                {
                    lblTV.Text = "ના નથી.";
                    lblTVNo.Text = "0";
                    lblLCDNo.Text = "0";
                }
                if (chkEco.Checked == true)
                {
                    lblEco.Text = "હા છે.";
                    lblEcoNo.Text = txtEcoNo.Text;
                    llbEcoName.Text = txtEcoName.Text;
                }
                else
                {
                    lblEco.Text = "ના નથી.";
                    lblEcoNo.Text = "0";
                    llbEcoName.Text = "0";
                }
                if (chkNSS.Checked == true)
                {
                    lblNSS.Text = "હા છે.";
                    lblNSSNo.Text = txtNSSNO.Text;
                    lblNssName.Text = txtNSSName.Text;
                }
                else
                {
                    lblNSS.Text = "ના નથી.";
                    lblNSSNo.Text = "0";
                    lblNssName.Text = "0";
                }
                if (chkCareer.Checked == true)
                {
                    lblCareer.Text = "હા છે.";
                    lblCareerCorner.Text = txtCareerNo.Text;
                    lblCareerCornerName.Text = txtCareerName.Text;
                }
                else
                {
                    lblCareer.Text = "ના નથી.";
                    lblCareerCorner.Text = "0";
                    lblCareerCornerName.Text = "0";
                }
                lblBuldingInfo.Text = txtBuildingInfo.Text;
                lblRoomNo.Text = txtRoomNo.Text;
                llbRoomSize.Text = txtRoomSize.Text;
            }
        }

        protected void dlStaffInfo_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblaacharyOpendl = (Label)e.Item.FindControl("lblaacharyOpendl");
                Label lblJSOpendl = (Label)e.Item.FindControl("lblJSOpendl");
                Label lblSSOpendl = (Label)e.Item.FindControl("lblSSOpendl");
                Label lblVSOpendl = (Label)e.Item.FindControl("lblVSOpendl");
                Label lblpatavalaOpendl = (Label)e.Item.FindControl("lblpatavalaOpendl");
                Label lblFSOpendl = (Label)e.Item.FindControl("lblFSOpendl");
                Label lblaacharyClosedl = (Label)e.Item.FindControl("lblaacharyClosedl");
                Label lblJSClosedl = (Label)e.Item.FindControl("lblJSClosedl");
                Label lblSSClosedl = (Label)e.Item.FindControl("lblSSClosedl");
                Label lblVSClosedl = (Label)e.Item.FindControl("lblVSClosedl");
                Label lblpatavalaClosedl = (Label)e.Item.FindControl("lblpatavalaClosedl");
                Label lblFSClosedl = (Label)e.Item.FindControl("lblFSClosedl");
                Label lblTotalPrincipalClosedl = (Label)e.Item.FindControl("lblTotalPrincipalClosedl");
                Label lblaacharyTotaldl = (Label)e.Item.FindControl("lblaacharyTotaldl");
                Label lblJSTotaldl = (Label)e.Item.FindControl("lblJSTotaldl");
                Label lblkarkunOpen = (Label)e.Item.FindControl("lblkarkunOpen");
                Label lblkarkunClose = (Label)e.Item.FindControl("lblkarkunClose");
                Label lblKarkundl = (Label)e.Item.FindControl("lblKarkunTotaldl");
                Label lblFSTotaldl = (Label)e.Item.FindControl("lblFSTotaldl");
                Label lblSSTotaldl = (Label)e.Item.FindControl("lblSSTotaldl");
                Label lblVSTotaldl = (Label)e.Item.FindControl("lblVSTotaldl");
                Label lblpatavalaTotaldl = (Label)e.Item.FindControl("lblpatavalaTotaldl");
                Label lblTotalPrincipalTotaldl = (Label)e.Item.FindControl("lblTotalPrincipalTotaldl");
                Label lblTotalPrincipalOpendl = (Label)e.Item.FindControl("lblTotalPrincipalOpendl");


                lblaacharyOpendl.Text = txtPrincipalOpen.Text;
                lblaacharyClosedl.Text = txtPrincipalClose.Text;
                lblJSOpendl.Text = txtTeacherOpen.Text;
                lblJSClosedl.Text = txtTeacherClose.Text;
                lblSSOpendl.Text = txtSahayakOpen.Text;
                lblSSClosedl.Text = txtSahayakClose.Text;
                lblVSOpendl.Text = txtVahivatiOpen.Text;
                lblVSClosedl.Text = txtVahivatiClose.Text;
                lblpatavalaOpendl.Text = txtPatavalaOpen.Text;
                lblpatavalaClosedl.Text = txtPatavalaClose.Text;
                lblFSOpendl.Text = txtFSahayakOpen.Text;
                lblFSClosedl.Text = txtFSahayakClose.Text;
                lblkarkunOpen.Text = txtClerkOpen.Text;
                lblkarkunClose.Text = txtClerkClose.Text;
                lblaacharyTotaldl.Text = Convert.ToString(Convert.ToInt32(txtPrincipalOpen.Text) + Convert.ToInt32(txtPrincipalClose.Text));
                lblJSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtTeacherOpen.Text) + Convert.ToInt32(txtTeacherClose.Text));
                lblKarkundl.Text = Convert.ToString(Convert.ToInt32(txtClerkOpen.Text) + Convert.ToInt32(txtClerkClose.Text));
                lblSSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtSahayakOpen.Text) + Convert.ToInt32(txtSahayakClose.Text));
                lblVSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtVahivatiOpen.Text) + Convert.ToInt32(txtVahivatiClose.Text));
                lblpatavalaTotaldl.Text = Convert.ToString(Convert.ToInt32(txtPatavalaOpen.Text) + Convert.ToInt32(txtPatavalaClose.Text));
                lblFSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtFSahayakOpen.Text) + Convert.ToInt32(txtFSahayakClose.Text));
                lblTotalPrincipalOpendl.Text = Convert.ToString(Convert.ToInt32(txtPrincipalOpen.Text) + Convert.ToInt32(txtTeacherOpen.Text) + Convert.ToInt32(txtSahayakOpen.Text) + Convert.ToInt32(txtVahivatiOpen.Text) + Convert.ToInt32(txtPatavalaOpen.Text) + Convert.ToInt32(txtFSahayakOpen.Text) + Convert.ToInt32(txtClerkOpen.Text));
                lblTotalPrincipalClosedl.Text = Convert.ToString(Convert.ToInt32(txtPrincipalClose.Text) + Convert.ToInt32(txtTeacherClose.Text) + Convert.ToInt32(txtSahayakClose.Text) + Convert.ToInt32(txtVahivatiClose.Text) + Convert.ToInt32(txtPatavalaClose.Text) + Convert.ToInt32(txtFSahayakClose.Text) + Convert.ToInt32(txtClerkClose.Text));
                lblTotalPrincipalTotaldl.Text = Convert.ToString(Convert.ToInt32(lblTotalPrincipalOpendl.Text) + Convert.ToInt32(lblTotalPrincipalClosedl.Text));
            }
        }
        protected void dlStaffInfo1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblaacharyOpendl = (Label)e.Item.FindControl("lblaacharyOpendl1");
                Label lblJSOpendl = (Label)e.Item.FindControl("lblJSOpendl1");
                Label lblSSOpendl = (Label)e.Item.FindControl("lblSSOpendl1");
                Label lblVSOpendl = (Label)e.Item.FindControl("lblVSOpendl1");
                Label lblpatavalaOpendl = (Label)e.Item.FindControl("lblpatavalaOpendl1");
                Label lblFSOpendl = (Label)e.Item.FindControl("lblFSOpendl1");
                Label lblaacharyClosedl = (Label)e.Item.FindControl("lblaacharyClosedl1");
                Label lblJSClosedl = (Label)e.Item.FindControl("lblJSClosedl1");
                Label lblSSClosedl = (Label)e.Item.FindControl("lblSSClosedl1");
                Label lblVSClosedl = (Label)e.Item.FindControl("lblVSClosedl1");
                Label lblpatavalaClosedl = (Label)e.Item.FindControl("lblpatavalaClosedl1");
                Label lblFSClosedl = (Label)e.Item.FindControl("lblFSClosedl1");
                Label lblTotalPrincipalClosedl = (Label)e.Item.FindControl("lblTotalPrincipalClosedl1");
                Label lblaacharyTotaldl = (Label)e.Item.FindControl("lblaacharyTotaldl1");
                Label lblJSTotaldl = (Label)e.Item.FindControl("lblJSTotaldl1");
                Label lblkarkunOpen = (Label)e.Item.FindControl("lblkarkunOpen1");
                Label lblkarkunClose = (Label)e.Item.FindControl("lblkarkunClose1");
                Label lblKarkundl = (Label)e.Item.FindControl("lblKarkunTotaldl1");
                Label lblFSTotaldl = (Label)e.Item.FindControl("lblFSTotaldl1");
                Label lblSSTotaldl = (Label)e.Item.FindControl("lblSSTotaldl1");
                Label lblVSTotaldl = (Label)e.Item.FindControl("lblVSTotaldl1");
                Label lblpatavalaTotaldl = (Label)e.Item.FindControl("lblpatavalaTotaldl1");
                Label lblTotalPrincipalTotaldl = (Label)e.Item.FindControl("lblTotalPrincipalTotaldl1");
                Label lblTotalPrincipalOpendl = (Label)e.Item.FindControl("lblTotalPrincipalOpendl1");


                lblaacharyOpendl.Text = txtPrincipalOpen.Text;
                lblaacharyClosedl.Text = txtPrincipalClose.Text;
                lblJSOpendl.Text = txtTeacherOpen.Text;
                lblJSClosedl.Text = txtTeacherClose.Text;
                lblSSOpendl.Text = txtSahayakOpen.Text;
                lblSSClosedl.Text = txtSahayakClose.Text;
                lblVSOpendl.Text = txtVahivatiOpen.Text;
                lblVSClosedl.Text = txtVahivatiClose.Text;
                lblpatavalaOpendl.Text = txtPatavalaOpen.Text;
                lblpatavalaClosedl.Text = txtPatavalaClose.Text;
                lblFSOpendl.Text = txtFSahayakOpen.Text;
                lblFSClosedl.Text = txtFSahayakClose.Text;
                lblkarkunOpen.Text = txtClerkOpen.Text;
                lblkarkunClose.Text = txtClerkClose.Text;
                lblaacharyTotaldl.Text = Convert.ToString(Convert.ToInt32(txtPrincipalOpen.Text) + Convert.ToInt32(txtPrincipalClose.Text));
                lblJSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtTeacherOpen.Text) + Convert.ToInt32(txtTeacherClose.Text));
                lblKarkundl.Text = Convert.ToString(Convert.ToInt32(txtClerkOpen.Text) + Convert.ToInt32(txtClerkClose.Text));
                lblSSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtSahayakOpen.Text) + Convert.ToInt32(txtSahayakClose.Text));
                lblVSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtVahivatiOpen.Text) + Convert.ToInt32(txtVahivatiClose.Text));
                lblpatavalaTotaldl.Text = Convert.ToString(Convert.ToInt32(txtPatavalaOpen.Text) + Convert.ToInt32(txtPatavalaClose.Text));
                lblFSTotaldl.Text = Convert.ToString(Convert.ToInt32(txtFSahayakOpen.Text) + Convert.ToInt32(txtFSahayakClose.Text));
                lblTotalPrincipalOpendl.Text = Convert.ToString(Convert.ToInt32(txtPrincipalOpen.Text) + Convert.ToInt32(txtTeacherOpen.Text) + Convert.ToInt32(txtSahayakOpen.Text) + Convert.ToInt32(txtVahivatiOpen.Text) + Convert.ToInt32(txtPatavalaOpen.Text) + Convert.ToInt32(txtFSahayakOpen.Text) + Convert.ToInt32(txtClerkOpen.Text));
                lblTotalPrincipalClosedl.Text = Convert.ToString(Convert.ToInt32(txtPrincipalClose.Text) + Convert.ToInt32(txtTeacherClose.Text) + Convert.ToInt32(txtSahayakClose.Text) + Convert.ToInt32(txtVahivatiClose.Text) + Convert.ToInt32(txtPatavalaClose.Text) + Convert.ToInt32(txtFSahayakClose.Text) + Convert.ToInt32(txtClerkClose.Text));
                lblTotalPrincipalTotaldl.Text = Convert.ToString(Convert.ToInt32(lblTotalPrincipalOpendl.Text) + Convert.ToInt32(lblTotalPrincipalClosedl.Text));
            }
        }

        #endregion

        #region CheckBox Event

        protected void chkTv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTv.Checked == true)
            {
                divTv.Visible = true;
            }
            else
            {
                divTv.Visible = false;
            }
        }

        protected void chkEco_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEco.Checked == true)
            {
                divEco.Visible = true;
            }
            else
            {
                divEco.Visible = false;
            }
        }

        protected void chkNSS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNSS.Checked == true)
            {
                divNSS.Visible = true;
            }
            else
            {
                divNSS.Visible = false;
            }
        }

        protected void chkCareer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCareer.Checked == true)
            {
                divCareer.Visible = true;
            }
            else
            {
                divCareer.Visible = false;
            }
        }

        #endregion

        #region Report Button Event
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Client.UI/TrustReports.aspx?Mode=StatutoryReports");
        }
        #endregion


    }
}