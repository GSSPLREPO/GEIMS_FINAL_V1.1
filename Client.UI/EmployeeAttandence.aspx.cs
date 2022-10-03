using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class EmployeeAttandence : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(EmployeeAttandence));
        #region PageLoad
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
                    btnSave.Enabled = false;
                    txtdate.Attributes.Add("readonly", "readonly");
                    ViewState["Mode"] = "Save";
                    divNote.Visible = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion



        #region BindGrid of Empolyee
        protected void BindEmpolyeeGrid()
        {
            try
            {
                EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
                ApplicationResult objResult = new ApplicationResult();
                EmployeeMBL objEmployeeBL = new EmployeeMBL();
                objResult = objEmployeeBL.Select_Employee_ForAttandance(Convert.ToInt32(Session[ApplicationSession.TRUSTID]), Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    gvEmployee.DataSource = objResult.resultDT;
                    gvEmployee.DataBind();
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        divGrid.Visible = true;
                        btnSave.Enabled = true;
                        gvEmployee.Visible = true;
                        objResult = objEmployeeAttendenceBL.Select_Employee_ForAttandanceDateWise(txtdate.Text);
                        if (objResult != null)
                        {
                            if (objResult.resultDT.Rows.Count > 0)
                            {
                                ViewState["Mode"] = "Edit";
                                ViewState["EmployeeAttendenceMID"] = objResult.resultDT.Rows[0][EmployeeattendanceBo.EMPLOYEEATTENDANCE_EMPLOYEEATTANDANCEMID].ToString();

                                foreach (GridViewRow row in gvEmployee.Rows)
                                {
                                    for (int i = 0; i < objResult.resultDT.Rows.Count; i++)
                                    {
                                        if (row.Cells[0].Text ==
                                            objResult.resultDT.Rows[i][
                                                EmployeeattendanceBo.EMPLOYEEATTENDANCE_EMPLOYEEMID].ToString())
                                        {
                                            ((CheckBox)row.FindControl("chkChild")).Checked = true;
                                            (((TextBox)row.FindControl("txtIntime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeattendanceBo.EMPLOYEEATTENDANCE_INTIME].ToString();

                                            (((TextBox)row.FindControl("txtRecOuttime")).Text) =
                                             objResult.resultDT.Rows[i][
                                                 EmployeeattendanceBo.EMPLOYEEATTENDANCE_RECOUTTIME].ToString();

                                            (((TextBox)row.FindControl("txtRecIntime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeattendanceBo.EMPLOYEEATTENDANCE_RECINTIME].ToString();
                                         
                                            (((TextBox)row.FindControl("txtOuttime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeattendanceBo.EMPLOYEEATTENDANCE_OUTTIME].ToString();
                                            (((Label)row.FindControl("lblTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeattendanceBo.EMPLOYEEATTENDANCE_TIME].ToString();
                                            (((TextBox)row.FindControl("txtTotalTime")).Text) =
                                                objResult.resultDT.Rows[i][
                                                    EmployeeattendanceBo.EMPLOYEEATTENDANCE_TotalTime].ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ViewState["Mode"] = "Save";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckAll();", true);
                            }
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        divGrid.Visible = false;
                        ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Insert Employee For Attendence.');</script>");
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



        #region View Button Click Event
        protected void btnViewGrid_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = true;
                BindEmpolyeeGrid();
                divNote.Visible = true;
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
            EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
            EmployeeattendanceBo objEmployeeAttendenceBO = new EmployeeattendanceBo();
            ApplicationResult objResults = new ApplicationResult();
            try
            {
                int intCount = 0;
                int k = 0;
                DatabaseTransaction.OpenConnectionTransation();

                foreach (GridViewRow row in gvEmployee.Rows)
                {

                    ViewState["EmployeeMID"] = Convert.ToInt32(row.Cells[0].Text);
                    objEmployeeAttendenceBO.EmployeeMID = Convert.ToInt32(row.Cells[0].Text);
                    objEmployeeAttendenceBO.TrustMID = Convert.ToInt32(Session[ApplicationSession.TRUSTID]);
                    objEmployeeAttendenceBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    objEmployeeAttendenceBO.Date = txtdate.Text;
                    objEmployeeAttendenceBO.InTime =
                       (((TextBox)row.FindControl("txtIntime")).Text);
                    objEmployeeAttendenceBO.RecOutTime =
                      (((TextBox)row.FindControl("txtRecOuttime")).Text);
                    objEmployeeAttendenceBO.RecInTime =
                    (((TextBox)row.FindControl("txtRecIntime")).Text);
                    objEmployeeAttendenceBO.OutTime =
                      (((TextBox)row.FindControl("txtOuttime")).Text);
                    objEmployeeAttendenceBO.Time = (((Label)row.FindControl("lblTime")).Text);
                    objEmployeeAttendenceBO.TotalTime = (((TextBox)row.FindControl("txtTotalTime")).Text);
                    

                    if (((CheckBox)row.FindControl("chkChild")).Checked)
                    {
                        if (objEmployeeAttendenceBO.RecOutTime == "" || objEmployeeAttendenceBO.RecInTime == "")
                        {
                            objEmployeeAttendenceBO.RecOutTime = "23:58";
                            objEmployeeAttendenceBO.RecInTime = "23:59";

                        }
                        intCount += 1;
                        if (objEmployeeAttendenceBO.InTime == "0" || objEmployeeAttendenceBO.OutTime == "0" || objEmployeeAttendenceBO.InTime == "" || objEmployeeAttendenceBO.OutTime == "")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script language='javascript'>alert('Please Fill Time " + row.Cells[0].Text +
                                ".');</script>");
                            break;
                        }
                        DateTime dtIntime = Convert.ToDateTime((((TextBox)row.FindControl("txtIntime")).Text));
                        DateTime dtOutTime = Convert.ToDateTime((((TextBox)row.FindControl("txtOuttime")).Text));
                        
                        DateTime dtRecOutTime = Convert.ToDateTime((((TextBox)row.FindControl("txtRecOuttime")).Text));
                        DateTime dtRecIntime = Convert.ToDateTime((((TextBox)row.FindControl("txtRecIntime")).Text));



                      
                           

                            if ((dtIntime >= dtOutTime) ) 
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                "<script language='javascript'>alert('Time Format Should be 24Hours Or OutTime Must be greater then InTime.');</script>");
                            break;
                        }
                        else
                        {
                            ApplicationResult objResultsInsert = new ApplicationResult();
                            if (ViewState["Mode"].ToString() == "Save")
                            {
                                objEmployeeAttendenceBO.CreatedModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objEmployeeAttendenceBO.CreateModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objResultsInsert =
                                    objEmployeeAttendenceBL.Employeeattendance_Insert(objEmployeeAttendenceBO);
                                divGrid.Visible = false;
                                btnSave.Enabled = false;
                                k += 1;
                            }
                            else
                            {
                                objEmployeeAttendenceBO.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                                objEmployeeAttendenceBO.LastModifiedUserID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                objEmployeeAttendenceBO.EmployeeMID = Convert.ToInt32(ViewState["EmployeeMID"].ToString());
                                objResults = objEmployeeAttendenceBL.Employeeattendance_Update(objEmployeeAttendenceBO);
                                if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS)
                                {
                                    k += 1;
                                    divGrid.Visible = false;
                                    btnSave.Enabled = false;
                                }
                            }
                        }
                        
                    }
                }
                if (k == intCount)
                {
                    DatabaseTransaction.CommitTransation();
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script language='javascript'>alert('Employee Attendence saved Successfully.');</script>");
                    divNote.Visible = false;
                }
                else
                {
                    DatabaseTransaction.RollbackTransation();
                }
            }
            catch (Exception ex)
            {
                DatabaseTransaction.RollbackTransation();
                DatabaseTransaction.connection.Close();
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region button Upload Click Event
        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    EmployeeattendanceBl objEmployeeAttendenceBL = new EmployeeattendanceBl();
                    EmployeeattendanceBo objEmployeeAttendenceBO = new EmployeeattendanceBo();
                    ApplicationResult objResults = new ApplicationResult();
                    ApplicationResult objBiomatric = new ApplicationResult();

                    string filename = Path.GetFileName(FileUpload1.FileName);
                    //System.IO.File.Delete(Server.MapPath("../Attendance/" + filename)); 
                    FileUpload1.SaveAs(Server.MapPath("../Attendance/") + filename);
                    StreamReader objInput = new StreamReader(Server.MapPath("../Attendance/" + filename),
                        System.Text.Encoding.Default);
                    string contents = objInput.ReadToEnd().Replace(' ','0');
                    string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\r\n", RegexOptions.None);
                    
                  //  string[] split = System.Text.RegularExpression.Regex.Replace(contents, @"\\s+", "");
                   // System.Text.RegularExpressions.Regex.Split(contents, "\\s+", RegexOptions.None);
                    var Attendance = string.Empty;
                    foreach (string row in split)
                    {
                        if (Attendance == "")
                            Attendance = row.Replace("X", "");
                        else
                            Attendance = Attendance + "," + row.Replace("X", "");
                    }
                    if (Attendance != "")
                    {
                        objEmployeeAttendenceBO.TrustMID =
                            Convert.ToInt32(Session[ApplicationSession.TRUSTID].ToString());
                        objEmployeeAttendenceBO.SchoolMID =
                            Convert.ToInt32(Session[ApplicationSession.SCHOOLID].ToString());
                        objEmployeeAttendenceBO.IsManual = 0; // 0 for Biomatric and 1 for Manual attendance.
                        objEmployeeAttendenceBO.CreateModifiedUserID =
                            Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objEmployeeAttendenceBO.CreatedModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();

                        //objBiomatric = objEmployeeAttendenceBL.Employeeattendance_Select_ForBiomatric(Attendance,
                        //    filename);
                        //if (objBiomatric != null)
                        //{
                        //    if (objBiomatric.resultDT.Rows.Count > 0)
                        //    {
                        //       ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
                        //    }
                        //}
                        //else
                        //{
                        //}
                        objResults = objEmployeeAttendenceBL.Employeeattendance_Insert_ForBioMatric(
                            objEmployeeAttendenceBO, Attendance, filename);
                    }
                    objInput.Close();
                    System.IO.File.Delete(Server.MapPath("../Attendance/" + filename));
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Employee Attendance Updated Successfully.');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Select the File.');</script>");
                }
            }
                catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }





        #endregion

        protected void gvEmployee_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}