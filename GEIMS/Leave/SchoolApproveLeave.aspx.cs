using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.Bl;
using GEIMS.Bo;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Leave
{
    public partial class SchoolApproveLeave : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(ApproveLeave));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    objResults = objLeaveBl.Leave_SelectAll();
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
        /// <summary>
        /// Comments on 29/09/2022 Bhandavi
        /// To approve or reject a leave
        /// If we reject a leave [IsApproved] flag to 2 and isDeleted flag is set to 1 in tbl_LeaveApply table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                LeaveApprovalBo objLeaveApprovalBo = new LeaveApprovalBo();
                LeaveApprovalBl objLeaveApprovalBl = new LeaveApprovalBl();
                LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();
                LeaveApplyBo objLeaveApplyBo = new LeaveApplyBo();
                ApplicationResult objResult = new ApplicationResult();
                int a = 0;
                int b = 0;

                if (ViewState["Mode"].ToString() == "Approve")
                {
                    objLeaveApplyBo.LeaveApplylID = Convert.ToInt32(ViewState["LeaveApplyID"].ToString());
                    objLeaveApplyBo.ApprovedDate = txtApprovedDate.Text; //Dont use read only mode
                    objLeaveApplyBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                    objLeaveApplyBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    DatabaseTransaction.OpenConnectionTransation();
                    objResult = objLeaveApplyBl.LeaveApply_Update_ForApproval(objLeaveApplyBo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                    }

                    foreach (GridViewRow row in gvLeave.Rows)
                    {
                        a += 1;
                        objLeaveApprovalBo.LeaveApprovalID =
                            Convert.ToInt32(((Label)row.FindControl("lblLeaveApprovalID")).Text);
                        objLeaveApprovalBo.NAReason = txtReason.Text;
                        objLeaveApprovalBo.LastModifiedBy =
                            Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objLeaveApprovalBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        if (((CheckBox)row.FindControl("cbApprove")).Checked)
                        {
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForApproval(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                            }
                        }
                        else
                        {
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForReject(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                //b += 1;
                                b = 0;
                            }
                        }
                    }
                    if (a == b)
                    {
                        DatabaseTransaction.transaction.Commit();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully approved.');</script>");
                        ClearAll();
                        PanelVisibility(1);
                    }
                    else if (a != b)
                    {
                        DatabaseTransaction.transaction.Commit();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully rejected.');</script>");
                        ClearAll();
                        PanelVisibility(1);
                    }
                    else
                    {
                        DatabaseTransaction.RollbackTransation();
                    }
                    LeaveApplyBind_Applied();
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    objLeaveApplyBo.LeaveApplylID = Convert.ToInt32(ViewState["LeaveApplyID"].ToString());
                    objLeaveApplyBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                    objLeaveApplyBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    DatabaseTransaction.OpenConnectionTransation();
                    objResult = objLeaveApplyBl.LeaveApply_Update_ForApproval(objLeaveApplyBo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                    }
                    foreach (GridViewRow row in gvLeave.Rows)
                    {
                        a += 1;
                        objLeaveApprovalBo.LeaveApprovalID =
                            Convert.ToInt32(((Label)row.FindControl("lblLeaveApprovalID")).Text);
                        objLeaveApprovalBo.NAReason = txtReason.Text;
                        objLeaveApprovalBo.LastModifiedBy =
                            Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objLeaveApprovalBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        if (((CheckBox)row.FindControl("cbApprove")).Checked)
                        {
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForApproval(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                            }
                        }
                        else
                        {
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForReject(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                            }
                        }
                    }
                    if (a == b)
                    {
                        DatabaseTransaction.transaction.Commit();
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully Updated.');</script>");
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
            //objResult = objLeaveApplyBl.LeaveApply_Select_ForPrincipal(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
            objResult = objLeaveApplyBl.LeaveApply_Select_ForPrincipal(Convert.ToInt32(0), Convert.ToInt32(Session[ApplicationSession.USERID]));
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
            //objResult = objLeaveApplyBl.LeaveApply_Select_ForPrincipal(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
            objResult = objLeaveApplyBl.LeaveApply_Select_ForPrincipal(Convert.ToInt32(1), Convert.ToInt32(Session[ApplicationSession.USERID]));
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
        /// <summary>
        /// Comments By Bhandavi 29/09/2022
        /// showing all applied/approved leaves grid based on selected status
        /// If status is applied then show the image for approve
        /// If status is approved then show image for change 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                objResult = objLeaveApplyBl.LeaveApply_Select_ForPrincipal(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
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
                            //CheckBox chk = (CheckBox)gvLeave.FindControl("cbHalfDay");
                            //chk.BackColor = System.Drawing.Color.Aqua;

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
        }
        #endregion

        #region Leave GridView Events [Row Command, Pre Render]
        /// <summary>
        /// Comments By Bhandavi 29/09/2022
        /// To change status of approved leave or to approve/reject a leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRole_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();

                //Edit1 = Change Status
                if (e.CommandName.ToString() == "Edit1")
                {
                    if (ddlStatus.SelectedValue != "0")
                    {
                        ViewState["LeaveApplyID"] = e.CommandArgument.ToString();
                        objResult =
                            objLeaveApplyBl.LeaveApply_Select_ForApprove(Convert.ToInt32(e.CommandArgument.ToString()));
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
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_ISAPPROVED].ToString() ==
                                        "1")
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[1].FindControl("cbApprove"))).Checked = true;
                                    }
                                    else if (objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_ISAPPROVED].ToString() ==
                                        "2")
                                    {
                                        ((CheckBox)(gvLeave.Rows[i].Cells[1].FindControl("cbApprove"))).Checked = false;
                                    }
                                    ((Label)(gvLeave.Rows[i].Cells[0].FindControl("lblLeaveApprovalID"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_LEAVEAPPROVALID]
                                            .ToString();
                                    ((TextBox)(gvLeave.Rows[i].Cells[2].FindControl("txtGridDates"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_APPLYDATE].ToString();
                                    ((DropDownList)(gvLeave.Rows[i].Cells[4].FindControl("ddlLeaveType")))
                                        .SelectedValue =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_LEAVEID].ToString();
                                    if (
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_ISHALFDAY].ToString() ==
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
                //Approve1 = Approve or Reject Status
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
                           objLeaveBl.Leave_Select_ForBalance(empid);//Convert.ToInt32(Session[ApplicationSession.USERID].ToString())  + <%# Eval("EmployeeMID") %>
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
                        ViewState["LeaveApplyID"] = e.CommandArgument.ToString();
                        objResult =
                            objLeaveApplyBl.LeaveApply_Select_ForApprove(Convert.ToInt32(e.CommandArgument.ToString()));
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
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_LEAVEAPPROVALID]
                                            .ToString();
                                    ((TextBox)(gvLeave.Rows[i].Cells[2].FindControl("txtGridDates"))).Text =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_APPLYDATE].ToString();
                                    ((DropDownList)(gvLeave.Rows[i].Cells[4].FindControl("ddlLeaveType")))
                                        .SelectedValue =
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_LEAVEID].ToString();
                                    if (
                                        objResult.resultDT.Rows[i][LeaveApprovalBo.LEAVEAPPROVAL_ISHALFDAY].ToString() ==
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
                                divReason.Visible = false;
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
    }
}