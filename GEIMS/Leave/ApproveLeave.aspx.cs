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

namespace GEIMS.Leave
{
    public partial class ApproveLeave : PageBase
    {
        #region Declaration
        private static ILog logger = LogManager.GetLogger(typeof(ApproveLeave));
        int intLeaveApplyID = 0;
        #endregion

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
                    objLeaveApplyBo.LeaveApplylID = Convert.ToInt32(ViewState["LeaveApplyID"].ToString());
                    objLeaveApplyBo.ApprovedDate = txtApprovedDate.Text; //Dont use read only mode
                    objLeaveApplyBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                    objLeaveApplyBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                    DatabaseTransaction.OpenConnectionTransation();
                    objResult = objLeaveApplyBl.LeaveApply_Update_ForApproval(objLeaveApplyBo);
                    if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                    {
                        //objLeaveTemplateBo.LeaveTemplateID = Convert.ToInt32(ViewState["LeaveTemplateID"].ToString());
                        //objLeaveTemplateBo.LeaveTemplateID = Convert.ToInt32(ViewState["LeaveTemplateID"].ToString());
                        //objLeaveTemplateBo.LastModifiedBy = Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        //objLeaveTemplateBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        //DatabaseTransaction.OpenConnectionTransation();
                        //objResult1 = objLeaveTemplateBo.LeaveApply_Update_ForApproval(objLeaveTemplateBo);
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
                            //When Check box is selected so Leave is Cancelled or Reject
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForApproval(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully approved');</script>");
                            }
                        }
                        else
                        {
                            //When Check box is not selected so Leave is Cancelled or Reject
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForReject(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                           "<script>alert('Leave application has been successfully rejected');</script>");
                            }
                        }
                    }

                    if (a == b)
                    {
                        DatabaseTransaction.transaction.Commit();
                        //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                        //    "<script>alert('Leave application has been successfully approved');</script>");
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
                    objLeaveApplyBo.ApprovedDate = Convert.ToString(txtApprovedDate.Text == "" ? "0" : txtApprovedDate.Text);
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
                        objLeaveApprovalBo.NAReason = txtReason.Text == "" ? "0" : txtReason.Text;
                        objLeaveApprovalBo.LastModifiedBy =
                            Convert.ToInt32(Session[ApplicationSession.USERID].ToString());
                        objLeaveApprovalBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        if (((CheckBox)row.FindControl("cbApprove")).Checked)
                        {
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForApproval(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully Updated for approved.');</script>");
                            }
                        }
                        else
                        {
                            objResult = objLeaveApprovalBl.LeaveApproval_Update_ForReject(objLeaveApprovalBo);
                            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                            {
                                b += 1;
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                            "<script>alert('Leave application has been successfully Updated for reject.');</script>");
                            }
                        }
                    }
                    if (a == b)
                    {
                        DatabaseTransaction.transaction.Commit();
                        //ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                        //    "<script>alert('Leave application has been successfully Updated.');</script>");
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
            //objResult = objLeaveApplyBl.LeaveApply_Select(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
            objResult = objLeaveApplyBl.LeaveApply_Select(Convert.ToInt32(0), Convert.ToInt32(Session[ApplicationSession.USERID]));
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
            //objResult = objLeaveApplyBl.LeaveApply_Select(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
            objResult = objLeaveApplyBl.LeaveApply_Select(Convert.ToInt32(1), Convert.ToInt32(Session[ApplicationSession.USERID]));
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

                objResult = objLeaveApplyBl.LeaveApply_Select(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(Session[ApplicationSession.USERID]));
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

        protected void gvRole_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //After Apply Leave can be Change/Edit or Reject
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                ApplicationResult objResult1 = new ApplicationResult();
                LeaveApplyBl objLeaveApplyBl = new LeaveApplyBl();

                if (e.CommandName.ToString() == "Edit1")
                {
                    if (ddlStatus.SelectedValue != "0")
                    {
                        ViewState["LeaveApplyID"] = e.CommandArgument.ToString();
                        intLeaveApplyID = Convert.ToInt32(e.CommandArgument);

                        objResult =
                            objLeaveApplyBl.LeaveApply_Select_ForApprove(intLeaveApplyID);

                        //Bind tbl_LeaveApproval
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
                           //Arpit Shah
                           //Date : 22-11-2021
                           //Description: After Leave Apply if in correct so change / edit or cancell Leave

                           //Bind tbl_LeaveApply
                            objResult1 = objLeaveApplyBl.LeaveApply_Select_ForApprove_ApprovedDate(intLeaveApplyID);
                            
                            if (objResult1 != null)
                            {
                                DataTable dtResult1 = new DataTable();
                                dtResult1 = objResult1.resultDT;
                                txtApprovedDate.Text = dtResult1.Rows[0]["ApprovedDate"].ToString();
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

                        //Change for Show as a reporting person wise [Arpit Shah]
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
        #endregion

        #region GVLeave Row created event
        protected void gvLeave_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }
        #endregion

        #region gvRole_OnPreRender
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

       protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}