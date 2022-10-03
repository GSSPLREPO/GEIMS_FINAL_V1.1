using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Bl;
using GEIMS.BL;
using GEIMS.Bo;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;


namespace GEIMS.Leave.DutyLeave
{
    public partial class ApproveDutyLeave : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ApproveDutyLeave));
        public static int EmployeeMID;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    //BindLeaveApprove();
                    PanelVisibility(1);
                    ViewState["Mode"] = "Save";
                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }

        }
        #endregion

        #region BindLeaveType
        private void BindLeaveType()
        {
            try
            {
                LeaveBl objLeaveBl = new LeaveBl();
                ApplicationResult objResults = new ApplicationResult();
                Controls objControls = new Controls();
                for (int i = 0; i < gvLeave.Rows.Count; i++)
                {
                    DropDownList ddlLeaveType = (DropDownList)gvLeave.Rows[i].Cells[3].FindControl("ddlLeaveType");
                    objResults = objLeaveBl.DutyLeave_SelectAll();
                    if (objResults != null)
                    {
                        if (objResults.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResults.resultDT, ddlLeaveType, "LeaveName", "LeaveID");
                        }
                        ddlLeaveType.Items.Insert(0, new ListItem("-Select-", ""));
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

        #region Save Button Click Event
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                LeaveApprovalBo objLeaveApprovalBo = new LeaveApprovalBo();
                LeaveApprovalBl objLeaveApprovalBl = new LeaveApprovalBl();

                LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();
                LeaveApplyBo objLeaveApplyBo = new LeaveApplyBo();

                LeaveTemplateBo objLeaveTemplateBo = new LeaveTemplateBo();
                LeaveTemplateBl objLeaveTemplateBl = new LeaveTemplateBl();
                ApplicationResult objResult1 = new ApplicationResult();

                ApplicationResult objResult = new ApplicationResult();
                int a = 0;
                int b = 0;

                if (ViewState["Mode"].ToString() == "Approve")
                {
                    objLeaveApplyBo.DutyLeaveApplylID = Convert.ToInt32(ViewState["DutyLeaveApplyID"].ToString());
                    objLeaveApplyBo.ApprovedDate = txtApprovedDate.Text; //Dont use read only mode
                    objLeaveApplyBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                    objLeaveApplyBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                   // DatabaseTransaction.OpenConnectionTransation();
                    objResult = objLeaveApplyBl.DutyLeaveApply_Update_ForApproval(objLeaveApplyBo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                    }
                    foreach (GridViewRow row in gvLeave.Rows)
                    {
                        a += 1;
                        objLeaveApprovalBo.DutyLeaveApprovalID =
                            Convert.ToInt32(((Label)row.FindControl("lblLeaveApprovalID")).Text);
                        objLeaveApprovalBo.NAReason = txtReason.Text;
                        objLeaveApprovalBo.LastModifiedBy =
                            Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objLeaveApprovalBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                        if (((CheckBox)row.FindControl("cbApprove")).Checked)
                        {
                            objResult = objLeaveApprovalBl.DutyLeaveApproval_Update_ForApproval(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;

                            }
                        }
                        else
                        {
                            objResult = objLeaveApprovalBl.DutyLeaveApproval_Update_ForReject(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                            }
                        }

                    }
                    if (a == b)
                    {


                        // Leave Templete Update Total Leave for Duty Leave

                        objLeaveTemplateBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());

                        //objLeaveTemplateBo.CreatedBy = Convert.ToInt32(gvLeaveApprove.Rows[0].Cells[1].Text);
                        objLeaveTemplateBo.CreatedBy = Convert.ToInt32(ViewState["EmployeeMID"].ToString());

                        objLeaveTemplateBo.IsDeleted = 0;
                        objLeaveTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        //DatabaseTransaction.OpenConnectionTransation();
                        objResult1 = objLeaveTemplateBl.DutyLeaveTotal_Update_ForApproval(objLeaveTemplateBo);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                       "<script>alert('Leave application has been successfully approved.');</script>");
                        }
                        // Leave Templete Update Total Leave for Duty Leave

                        //DatabaseTransaction.transaction.Commit();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully approved.');</script>");
                        ClearAll();

                        PanelVisibility(1);
                        // Leave Templete Update Total Leave for Duty Leave
                    }
                    else
                    {
                        DatabaseTransaction.RollbackTransation();
                    }
                    LeaveApplyBind_Applied();
                }             
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    objLeaveApplyBo.DutyLeaveApplylID = Convert.ToInt32(ViewState["DutyLeaveApplyID"].ToString());
                    //int EmpID = 0;
                    //EmpID=
                    objLeaveApplyBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                    objLeaveApplyBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    //DatabaseTransaction.OpenConnectionTransation();
                    objResult = objLeaveApplyBl.DutyLeaveApply_Update_ForApproval(objLeaveApplyBo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        

                    }

                    foreach (GridViewRow row in gvLeave.Rows)
                    {
                        a += 1;
                        objLeaveApprovalBo.DutyLeaveApprovalID =
                            Convert.ToInt32(((Label)row.FindControl("lblLeaveApprovalID")).Text);
                        objLeaveApprovalBo.NAReason = txtReason.Text;
                        objLeaveApprovalBo.LastModifiedBy =
                            Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objLeaveApprovalBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        if (((CheckBox)row.FindControl("cbApprove")).Checked)
                        {
                            objResult = objLeaveApprovalBl.DutyLeaveApproval_Update_ForApproval(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                            }
                        }
                        else
                        {
                            objResult = objLeaveApprovalBl.DutyLeaveApproval_Update_ForReject(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                            }
                        }
                    }
                    if (a == b)
                    {

                        // Leave Templete Update Total Leave for Duty Leave



                        objLeaveTemplateBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objLeaveTemplateBo.CreatedBy = Convert.ToInt32(ViewState["EmployeeMID"].ToString());

                        objLeaveTemplateBo.IsDeleted = 0;
                        objLeaveTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        //DatabaseTransaction.OpenConnectionTransation();
                        objResult1 = objLeaveTemplateBl.DutyLeaveTotal_Update_ForApproval(objLeaveTemplateBo);
                        if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                       "<script>alert('Leave application has been successfully approved.');</script>");
                        }
                        // Leave Templete Update Total Leave for Duty Leave


                        //DatabaseTransaction.transaction.Commit();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Duty Leave application has been successfully Updated.');</script>");
                        ClearAll();
                        PanelVisibility(1);
                    }
                    else
                    {
                        DatabaseTransaction.RollbackTransation();
                    }
                    LeaveApplyBind_Approved();
                }
                              

            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
            finally
            {
                DatabaseTransaction.connection.Close();
            }

        }
        #endregion

        #region LeaveApplyBind for "Applied"
        //Name : Arpit Shah 
        //Date : 26-12-2021
        //Description : This method is used to direct value pass for "Applied".
        public void LeaveApplyBind_Applied()
        {
            ApplicationResult objResult = new ApplicationResult();
            LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();

            //Note : Direct Dropdown value passed for "Applied" = 0
            //objResult = objLeaveApplyBl.DutyLeaveApply_Select(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
            objResult = objLeaveApplyBl.DutyLeaveApply_Select(Convert.ToInt32(0), Convert.ToInt32(Session[ApplicationSession.USERID]));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvLeaveApprove.DataSource = objResult.resultDT;
                    gvLeaveApprove.DataBind();
                    PanelVisibility(2);
                    if (ddlStatus.SelectedIndex == 1)
                    {
                        gvLeaveApprove.Columns[7].Visible = false;
                    }
                    else if (ddlStatus.SelectedIndex == 2)
                    {
                        gvLeaveApprove.Columns[6].Visible = false;  //Changes
                        gvLeaveApprove.Columns[7].Visible = true;
                    }
                }
                else
                {
                    PanelVisibility(3);
                }
            }
        }
        #endregion

        #region LeaveApplyBind for "Approved"
        //Name : Arpit Shah 
        //Date : 26-12-2021
        //Description : This method is used to direct value pass for "Approved".
        public void LeaveApplyBind_Approved()
        {
            ApplicationResult objResult = new ApplicationResult();
            LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();

            //Note : Direct Dropdown value passed for "Applied" = 1
            //objResult = objLeaveApplyBl.DutyLeaveApply_Select(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
            objResult = objLeaveApplyBl.DutyLeaveApply_Select(Convert.ToInt32(1), Convert.ToInt32(Session[ApplicationSession.USERID]));
            if (objResult != null)
            {
                if (objResult.resultDT.Rows.Count > 0)
                {
                    gvLeaveApprove.DataSource = objResult.resultDT;
                    gvLeaveApprove.DataBind();
                    PanelVisibility(2);
                    if (ddlStatus.SelectedIndex == 1)
                    {
                        gvLeaveApprove.Columns[7].Visible = false;
                    }
                    else if (ddlStatus.SelectedIndex == 2)
                    {
                        gvLeaveApprove.Columns[6].Visible = false;  //Changes
                        gvLeaveApprove.Columns[7].Visible = true;
                    }
                }
                else
                {
                    PanelVisibility(3);
                }
            }
        }
        #endregion

        #region Go button click event
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue.ToString() == "-1")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                    "<script>alert('Select Status.');</script>");
            }
            else
            {
                ApplicationResult objResult = new ApplicationResult();
                LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();

                objResult = objLeaveApplyBl.DutyLeaveApply_Select(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvLeaveApprove.DataSource = objResult.resultDT;
                        gvLeaveApprove.DataBind();
                        PanelVisibility(2);
                        if (ddlStatus.SelectedIndex == 1)
                        {
                            gvLeaveApprove.Columns[7].Visible = false;
                        }
                        else if (ddlStatus.SelectedIndex == 2)
                        {
                            gvLeaveApprove.Columns[7].Visible = true;
                        }
                    }
                    else
                    {
                        PanelVisibility(3);
                    }
                }
            }
        }
        #endregion

        #region Leave GridView Events [Row Command, Pre Render]

        protected void gvRole_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();
                if (e.CommandName.ToString() == "Edit1")
                {
                    if (ddlStatus.SelectedValue != "0")
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        ViewState["DutyLeaveApplyID"] = commandArgs[0];
                        ViewState["EmployeeMID"] = commandArgs[1];
                        //ViewState["DutyLeaveApplyID"] = e.CommandArgument.ToString();
                        //ViewState["EmployeeMID"] = e.CommandArgument.ToString();

                        objResult =
                            objLeaveApplyBl.DutyLeaveApply_Select_ForApprove(Convert.ToInt32(ViewState["DutyLeaveApplyID"]));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                DataTable dt = new DataTable();
                                dt.Rows.Clear();
                                dt.Columns.Add("NO");
                                for (var i = 1; i <= objResult.resultDT.Rows.Count; i++)
                                {
                                    dt.Rows.Add(this.ToString());
                                }
                                gvLeave.DataSource = dt;
                                gvLeave.DataBind();
                                BindLeaveType();
                                for (var i = 0; i < gvLeave.Rows.Count; i++)
                                {
                                    if (
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_ISAPPROVED].ToString() ==
                                        "1")
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[1].FindControl("cbApprove"))).Checked = true;
                                    }
                                    else if (objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_ISAPPROVED].ToString() ==
                                        "2")
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[1].FindControl("cbApprove"))).Checked = false;
                                    }
                                    ((Label)(gvLeave.Rows[i].Cells[0].FindControl("lblLeaveApprovalID"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_DUTYLEAVEAPPROVALID]
                                            .ToString();
                                    ((TextBox)(gvLeave.Rows[i].Cells[2].FindControl("txtGridDates"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_APPLYDATE].ToString();
                                    ((DropDownList)(gvLeave.Rows[i].Cells[4].FindControl("ddlLeaveType")))
                                        .SelectedValue =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_LEAVEID].ToString();
                                    if (
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_ISHALFDAY].ToString() ==
                                        "1")
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[3].FindControl("cbHalfDay"))).Checked = true;
                                    }
                                    else
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[3].FindControl("cbHalfDay"))).Checked = false;
                                    }
                                    ((TextBox)(gvLeave.Rows[i].Cells[2].FindControl("txtGridDates"))).Enabled = false;
                                    ((DropDownList)(gvLeave.Rows[i].Cells[4].FindControl("ddlLeaveType"))).Enabled =
                                        false;
                                    ((CheckBox)(gvLeave.Rows[i].Cells[3].FindControl("cbHalfDay"))).Enabled = false;
                                }
                                ViewState["Mode"] = "Edit";
                                PanelVisibility(4);
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('This leave has not approved yet,You can not edit.');</script>");
                    }
                }
                else if (e.CommandName.ToString() == "Approve1")
                {
                    if (ddlStatus.SelectedValue.ToString() != "1")
                    {
                        #region Bind Leave Balance
                        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        int rowIndex = gvr.RowIndex;

                        int empid = Convert.ToInt32(gvLeaveApprove.Rows[rowIndex].Cells[1].Text);
                        LeaveBl objLeaveBl = new LeaveBl();
                        objResult =
                           objLeaveBl.DutyLeave_Select_ForBalance(empid);//Convert.ToInt32(Session[ApplicationSession.USERID].ToString())  + <%# Eval("EmployeeMID") %>
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                gvLeaveBalance.DataSource = objResult.resultDT;
                                gvLeaveBalance.DataBind();
                            }
                        }
                        #endregion

                        //objLeaveApprovalBo.EmployeeMID = Convert.ToInt32(e.CommandArgument.ToString());
                        //ViewState["DutyLeaveApplyID"] = e.CommandArgument.ToString();
                        //ViewState["EmployeeMID"] = e.CommandArgument.ToString();
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        ViewState["DutyLeaveApplyID"] = commandArgs[0];
                        //ViewState["EmployeeMID"] = commandArgs[1];
                        ViewState["EmployeeMID"] = e.CommandArgument.ToString();
                        objResult =
                            objLeaveApplyBl.DutyLeaveApply_Select_ForApprove(Convert.ToInt32(ViewState["DutyLeaveApplyID"]));
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                DataTable dt = new DataTable();
                                dt.Rows.Clear();
                                dt.Columns.Add("NO");
                                for (var i = 1; i <= objResult.resultDT.Rows.Count; i++)
                                {
                                    dt.Rows.Add(this.ToString());
                                }
                                gvLeave.DataSource = dt;
                                gvLeave.DataBind();
                                BindLeaveType();
                                for (var i = 0; i < gvLeave.Rows.Count; i++)
                                {
                                    ((Label)(gvLeave.Rows[i].Cells[0].FindControl("lblLeaveApprovalID"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_DUTYLEAVEAPPROVALID]
                                            .ToString();
                                    ((TextBox)(gvLeave.Rows[i].Cells[2].FindControl("txtGridDates"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_APPLYDATE].ToString();
                                    ((DropDownList)(gvLeave.Rows[i].Cells[4].FindControl("ddlLeaveType")))
                                        .SelectedValue =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_LEAVEID].ToString();
                                    if (
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.DUTYLEAVEAPPROVAL_ISHALFDAY].ToString() ==
                                        "1")
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[3].FindControl("cbHalfDay"))).Checked = true;
                                    }
                                    else
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[3].FindControl("cbHalfDay"))).Checked = false;
                                    }
                                    ((TextBox)(gvLeave.Rows[i].Cells[2].FindControl("txtGridDates"))).Enabled = false;
                                    ((DropDownList)(gvLeave.Rows[i].Cells[4].FindControl("ddlLeaveType"))).Enabled =
                                        false;
                                    ((CheckBox)(gvLeave.Rows[i].Cells[3].FindControl("cbHalfDay"))).Enabled = false;
                                }
                                ViewState["Mode"] = "Approve";
                                PanelVisibility(4);
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('This Leave has already approved.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

        #region GVLeave Row created event
        protected void gvLeave_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }
        #endregion

        protected void gvRole_OnPreRender(object sender, EventArgs e)
        {
            try
            {
                if (gvLeaveApprove.Rows.Count > 0)
                {
                    gvLeaveApprove.UseAccessibleHeader = true;
                    gvLeaveApprove.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Clear All Method
        public void ClearAll()
        {
            Controls objControl = new Controls();
            objControl.ClearForm(Master.FindControl("ContentPlaceHolder1"));
            ViewState["Mode"] = "Save";
        }
        #endregion

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
                divGrid.Visible = false;
                divContent3.Visible = false;
                divReason.Visible = false;
                pnlApproveLeave.Visible = false;
                btnSave.Text = "Save";
            }
            else if (intcode == 2)
            {
                divGrid.Visible = true;
                tabs.Visible = false;
                divContent3.Visible = false;
                btnSave.Text = "Save";
            }
            else if (intcode == 3)
            {
                divGrid.Visible = false;
                tabs.Visible = false;
                divContent3.Visible = true;
                btnSave.Text = "Save";
            }
            else if (intcode == 4)
            {
                divGrid.Visible = false;
                tabs.Visible = false;
                divContent3.Visible = false;
                pnlApproveLeave.Visible = true;
                divReason.Visible = true;
                btnSave.Text = "Approve Leave";
            }
        }
        #endregion

        protected void gvLeaveApprove_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvLeaveBalance_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvLeave_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}