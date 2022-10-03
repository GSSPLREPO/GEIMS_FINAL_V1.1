using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Web.UI;
using log4net;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class ShiftMaster : System.Web.UI.Page
    {
        #region Declaration
        private static ILog logger = LogManager.GetLogger(typeof(ShiftMaster));
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    GridDataBind();
                    ViewState["Mode"] = "Save";
                    //ViewState["DivisionMode"] = "Save";
                    //ViewState["ClassMID"] = 0;


                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #endregion 

        #region ViewList Section Click

        protected void lnkViewList_Click(object sender, EventArgs e)
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["ShiftMID"] = null;
            PanelGrid_VisibilityMode(1);
        }
        #endregion

        #region Gridview Shift RowCommand Event
        protected void gvShiftMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ViewState["ShiftMID"] = e.CommandArgument.ToString();
                //Controls objControls = new Controls();
                //ShiftMasterBL objShiftMasterBl = new ShiftMasterBL();
                //ApplicationResult objResults = new ApplicationResult();


                if (e.CommandName.ToString() == "Delete1")
                {

                    ApplicationResult objResult = new ApplicationResult();
                    ShiftMasterBL objShiftMasterBl = new ShiftMasterBL();
                    objResult = objShiftMasterBl.Shift_Delete(Convert.ToInt32(ViewState["ShiftMID"].ToString()));
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Shift deleted successfully.');</script>");
                        GridDataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('There are Employee(s) associated with this Shift. To delete this Shift you need to delete Employee(s) first.');</script>");
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

        #region Clear all
        private void ClearAll()
        {
            Controls objControls = new Controls();
            objControls.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["ShiftMID"] = null;
            ViewState["Mode"] = "Save";

        }
        #endregion

        #region ValidateName
        public bool ValidateName()
        {
            try
            {
                ShiftMasterBL objShiftMasterBl = new ShiftMasterBL();
                ApplicationResult objResults = new ApplicationResult();
                DataTable dtShift = new DataTable();


                objResults = objShiftMasterBl.Shift_ValidateName(txtShiftName.Text, Convert.ToInt32(Session[ApplicationSession.TRUSTID]));


                if (objResults.resultDT.Rows.Count > 0)

                    return true;
                return false;
            }
            catch (Exception ex)
            {
                //throw ex;
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                return false;
            }
        }
        #endregion

        #region PanelGrid_VisibilityMode

        private void PanelGrid_VisibilityMode(int intMode)
        {
            if (intMode == 1)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                lnkViewList.Visible = false;
            }
            else if (intMode == 2)
            {

                divGrid.Visible = false;
                tabs.Visible = true;
                lnkViewList.Visible = true;
            }
        }

        #endregion PanelGrid_VisibilityMode

        #region Bind grid for Class
        private void GridDataBind()
        {
            try
            {
                ShiftMasterBL objShiftBl = new ShiftMasterBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objShiftBl.Shift_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvShiftMaster.DataSource = objResult.resultDT;
                        gvShiftMaster.DataBind();
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

        #region Save Button Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ShiftMasterBL objShiftBl = new ShiftMasterBL();
                ShiftMasterBO objShiftBo = new ShiftMasterBO();
                //DivisionTBO objDivisionBo = new DivisionTBO();
                //DivisionTBL objDivisionBl = new DivisionTBL();
                //Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();

                if (TxtStartTime.Text.Contains(":") && TxtRecessStart.Text.Contains(":") && TxtRecessEnd.Text.Contains(":") && TxtEndTime.Text.Contains(":"))
                {
                   // btnSave.Enabled = true;

                    if (ValidateName() == true)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Shift Name " + txtShiftName.Text + " already Exists.');</script>");
                        goto Exit;
                    }
                    objShiftBo.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());

                    objShiftBo.ShiftName = txtShiftName.Text;
                    objShiftBo.StartTime =  TxtStartTime.Text.ToString();
                    objShiftBo.RecessStartTime = TxtRecessStart.Text.ToString();
                    objShiftBo.RecessEndTime = TxtRecessEnd.Text.ToString();
                    objShiftBo.EndTime = TxtEndTime.Text.ToString();
                    objShiftBo.TotalWH = "";
                    objShiftBo.FirstHalfWH = "";
                    objShiftBo.SecondHalfWH = "";
                    objShiftBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    objShiftBo.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);



                    #region RollBack Transaction Starts

                    //DatabaseTransaction.OpenConnectionTransation();
                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        //objResults = objClasstBl.Class_ValidateName(txtClassName.Text, Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                        //if (objResults != null)
                        //{
                        //    if (objResults.resultDT.Rows.Count > 0)
                        //    {
                        //        ViewState["ClassMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                        //        SaveClass_T();
                        //    }
                        //    else
                        //    {
                        objResults = objShiftBl.Shift_Insert(objShiftBo);
                        if (objResults != null)
                        {
                            if (objResults.resultDT.Rows.Count > 0)
                            {
                                ViewState["ShiftMID"] = Convert.ToInt32(objResults.resultDT.Rows[0][0].ToString());
                                //SaveClass_T();
                                ClearAll();
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Record saved successfully.');</script>");
                                GridDataBind();
                                PanelGrid_VisibilityMode(1);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Shift Name " + txtShiftName.Text + " Already Exists.');</script>");
                            }

                        }

                        //    }
                        //}

                    }

                // DatabaseTransaction.CommitTransation();
                #endregion
                Exit:;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Time is not in correct formate.');</script>");
                    TxtStartTime.Focus();
                    //btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Add New Click Event
        protected void lnkAddNewShift_Click(object sender, EventArgs e)
        {
            ClearAll();
            PanelGrid_VisibilityMode(2);
        }
        #endregion

        protected void txtShiftName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvShiftMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       

        protected void TxtStartTime_TextChanged(object sender, EventArgs e)
        {

        }
    }
}