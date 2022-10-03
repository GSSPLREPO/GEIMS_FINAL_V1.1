using System;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using System.Data;
using System.Web.SessionState;
using log4net;

namespace GEIMS
{
    public partial class UserLogin : System.Web.UI.Page
    {
        #region Declaration

        private static ILog logger = LogManager.GetLogger(typeof(UserLogin));

        #endregion Declaration

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[ApplicationSession.USERID] == null)
                {

                }
                else
                {
                    int UserId = 0;
                    int SchoolId = 0;
                    UserId = Convert.ToInt32(Session[ApplicationSession.USERID]);
                    SchoolId = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);
                    if (SchoolId != 0)
                    {
                        Response.Redirect("Client.UI/SchoolHome.aspx");
                    }
                    else
                    {
                        Response.Redirect("Client.UI/TrustHome.aspx");
                    }

                    //EmployeeMBL objEmployeeMBL = new EmployeeMBL();
                    //ApplicationResult objResult = new ApplicationResult();

                    //objResult = objEmployeeMBL.Employee_CheckForLogin
                }

            }

        }
        #endregion

        #region Button Login Click Event
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtUsername.Text !="" && txtPassword.Text != "")
                {
                    if (Session[ApplicationSession.USERID] == null)
                    {
                        lblMsg.Visible = false;
                        EmployeeMBL objEmployeeMBL = new EmployeeMBL();
                        ApplicationResult objResult = new ApplicationResult();

                        objResult = objEmployeeMBL.Employee_CheckForLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                        if (objResult != null)
                        {
                            DataTable dtEmployee = new DataTable();
                            dtEmployee = objResult.resultDT;
                            if (dtEmployee.Rows.Count > 0)
                            {
                                Session[ApplicationSession.USERID] =
                                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                                Session[ApplicationSession.USERNAME] =
                                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_USERNAME].ToString();
                                Session[ApplicationSession.ROLEID] =
                                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_ROLEID].ToString();
                                Session[ApplicationSession.EMPCODE] =
                                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEECODE].ToString();
                                Session[ApplicationSession.REPORTINGTO] =
                                    dtEmployee.Rows[0]["ReportingTo"].ToString();

                                Session[ApplicationSession.SCHOOLID] =
                                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                //if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() == "0")
                                //{
                                //    Session[ApplicationSession.TRUSTID] = dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
                                //}
                                //else if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() != "0")
                                //{
                                //    Session[ApplicationSession.TRUSTID] = dtEmployee.Rows[0]["SchTrustMID"].ToString();
                                //    //Session[ApplicationSession.TRUSTID] = "0";
                                //}

                                //if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() != "0")
                                //{
                                //    Session[ApplicationSession.SCHOOLID] = dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                //}

                                //log4net.LogicalThreadContext.Properties["UserID"] = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                //logger.Info("User Login");

                                if (dtEmployee.Rows[0]["TrustMID"].ToString() != "0" &&
                                    dtEmployee.Rows[0]["SchoolMID"].ToString() == "0")
                                {
                                    Session[ApplicationSession.TRUSTID] =
                                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
                                    Session[ApplicationSession.TRUSTNAME] = dtEmployee.Rows[0]["TrustNameEng"].ToString();
                                    Session[ApplicationSession.SCHOOLID] =
                                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                    Session[ApplicationSession.ISPANEL] =
                                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                    Response.Redirect("Client.UI/TrustHome.aspx", false);
                                }
                                else if (dtEmployee.Rows[0]["SchoolMID"].ToString() != "0")
                                {
                                    Session[ApplicationSession.TRUSTID] =
                                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
                                    Session[ApplicationSession.TRUSTNAME] = dtEmployee.Rows[0]["TrustNameEng"].ToString();
                                    Session[ApplicationSession.SCHOOLID] =
                                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                    Session[ApplicationSession.SCHOOLNAME] = dtEmployee.Rows[0]["SchoolNameEng"].ToString();
                                    Session[ApplicationSession.ISPANEL] =
                                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                    Response.Redirect("Client.UI/SchoolHome.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("../UserLogin.aspx", false);
                                }
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "UserName or Password Incorrect.";
                                txtPassword.Text = "";
                                txtUsername.Text = "";
                            }
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "UserName or Password Incorrect.";
                            txtPassword.Text = "";
                            txtUsername.Text = "";
                        }
                    }
                    else
                    {
                        if (Session[ApplicationSession.USERID] != null)
                        {
                            string UserID = Convert.ToString(Session[ApplicationSession.USERID] == null);
                            Session.Abandon();

                            //if (UserID=="False")
                            //{
                            //    Response.Redirect("Client.UI/TrustHome.aspx", false);
                            //}
                            //lblMsg.Visible = true;
                            //lblMsg.Text = "User Already Logged In.";

                            if (UserID == "False")
                            {
                                lblMsg.Visible = false;
                                EmployeeMBL objEmployeeMBL = new EmployeeMBL();
                                ApplicationResult objResult = new ApplicationResult();

                                objResult = objEmployeeMBL.Employee_CheckForLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                                if (objResult != null)
                                {
                                    DataTable dtEmployee = new DataTable();
                                    dtEmployee = objResult.resultDT;
                                    if (dtEmployee.Rows.Count > 0)
                                    {
                                        Session[ApplicationSession.USERID] =
                                            dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
                                        Session[ApplicationSession.USERNAME] =
                                            dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_USERNAME].ToString();
                                        Session[ApplicationSession.ROLEID] =
                                            dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_ROLEID].ToString();
                                        Session[ApplicationSession.SCHOOLID] =
                                            dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                        //if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() == "0")
                                        //{
                                        //    Session[ApplicationSession.TRUSTID] = dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
                                        //}
                                        //else if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() != "0")
                                        //{
                                        //    Session[ApplicationSession.TRUSTID] = dtEmployee.Rows[0]["SchTrustMID"].ToString();
                                        //    //Session[ApplicationSession.TRUSTID] = "0";
                                        //}

                                        //if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() != "0")
                                        //{
                                        //    Session[ApplicationSession.SCHOOLID] = dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                        //}

                                        //log4net.LogicalThreadContext.Properties["UserID"] = Convert.ToInt32(Session[ApplicationSession.USERID]);
                                        //logger.Info("User Login");

                                        if (dtEmployee.Rows[0]["TrustMID"].ToString() != "0" &&
                                            dtEmployee.Rows[0]["SchoolMID"].ToString() == "0")
                                        {
                                            Session[ApplicationSession.TRUSTID] =
                                                dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
                                            Session[ApplicationSession.TRUSTNAME] = dtEmployee.Rows[0]["TrustNameEng"].ToString();
                                            Session[ApplicationSession.SCHOOLID] =
                                                dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                            Session[ApplicationSession.ISPANEL] =
                                                dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                            Response.Redirect("Client.UI/TrustHome.aspx", false);
                                        }
                                        else if (dtEmployee.Rows[0]["SchoolMID"].ToString() != "0")
                                        {
                                            Session[ApplicationSession.TRUSTID] =
                                                dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
                                            Session[ApplicationSession.TRUSTNAME] = dtEmployee.Rows[0]["TrustNameEng"].ToString();
                                            Session[ApplicationSession.SCHOOLID] =
                                                dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                            Session[ApplicationSession.SCHOOLNAME] = dtEmployee.Rows[0]["SchoolNameEng"].ToString();
                                            Session[ApplicationSession.ISPANEL] =
                                                dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
                                            Response.Redirect("Client.UI/SchoolHome.aspx", false);
                                        }
                                        else
                                        {
                                            Response.Redirect("../UserLogin.aspx", false);
                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Visible = true;
                                        lblMsg.Text = "UserName or Password Incorrect.";
                                        txtPassword.Text = "";
                                        txtUsername.Text = "";
                                    }
                                }
                                else
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = "UserName or Password Incorrect.";
                                    txtPassword.Text = "";
                                    txtUsername.Text = "";
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "UserName or Password Incorrect.";
                    txtPassword.Text = "";
                    txtUsername.Text = "";                   
                }             
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //try
            //{
            //    if (Session[ApplicationSession.USERID] == null)
            //    {
            //        lblMsg.Visible = false;
            //        EmployeeMBL objEmployeeMBL = new EmployeeMBL();
            //        ApplicationResult objResult = new ApplicationResult();

            //        objResult = objEmployeeMBL.Employee_CheckForLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            //        if (objResult != null)
            //        {
            //            DataTable dtEmployee = new DataTable();
            //            dtEmployee = objResult.resultDT;
            //            if (dtEmployee.Rows.Count > 0)
            //            {
            //                Session[ApplicationSession.USERID] =
            //                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_EMPLOYEEMID].ToString();
            //                Session[ApplicationSession.USERNAME] =
            //                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_USERNAME].ToString();
            //                Session[ApplicationSession.ROLEID] =
            //                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_ROLEID].ToString();
            //                Session[ApplicationSession.SCHOOLID] =
            //                    dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
            //                //if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() == "0")
            //                //{
            //                //    Session[ApplicationSession.TRUSTID] = dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
            //                //}
            //                //else if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() != "0")
            //                //{
            //                //    Session[ApplicationSession.TRUSTID] = dtEmployee.Rows[0]["SchTrustMID"].ToString();
            //                //    //Session[ApplicationSession.TRUSTID] = "0";
            //                //}

            //                //if (dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString() != "0")
            //                //{
            //                //    Session[ApplicationSession.SCHOOLID] = dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
            //                //}

            //                //log4net.LogicalThreadContext.Properties["UserID"] = Convert.ToInt32(Session[ApplicationSession.USERID]);
            //                //logger.Info("User Login");

            //                if (dtEmployee.Rows[0]["TrustMID"].ToString() != "0" &&
            //                    dtEmployee.Rows[0]["SchoolMID"].ToString() == "0")
            //                {
            //                    Session[ApplicationSession.TRUSTID] =
            //                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
            //                    Session[ApplicationSession.TRUSTNAME] = dtEmployee.Rows[0]["TrustNameEng"].ToString();
            //                    Session[ApplicationSession.SCHOOLID] =
            //                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
            //                    Session[ApplicationSession.ISPANEL] =
            //                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
            //                    Response.Redirect("Client.UI/TrustHome.aspx", false);
            //                }
            //                else if (dtEmployee.Rows[0]["SchoolMID"].ToString() != "0")
            //                {
            //                    Session[ApplicationSession.TRUSTID] =
            //                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_TRUSTMID].ToString();
            //                    Session[ApplicationSession.TRUSTNAME] = dtEmployee.Rows[0]["TrustNameEng"].ToString();
            //                    Session[ApplicationSession.SCHOOLID] =
            //                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
            //                    Session[ApplicationSession.SCHOOLNAME] = dtEmployee.Rows[0]["SchoolNameEng"].ToString();
            //                    Session[ApplicationSession.ISPANEL] =
            //                        dtEmployee.Rows[0][EmployeeMBO.EMPLOYEEM_SCHOOLMID].ToString();
            //                    Response.Redirect("Client.UI/SchoolHome.aspx", false);
            //                }
            //                else
            //                {
            //                    Response.Redirect("../UserLogin.aspx", false);
            //                }
            //            }
            //            else
            //            {
            //                lblMsg.Visible = true;
            //                lblMsg.Text = "UserName or Password Incorrect.";
            //                txtPassword.Text = "";
            //                txtUsername.Text = "";
            //            }
            //        }
            //        else
            //        {
            //            lblMsg.Visible = true;
            //            lblMsg.Text = "UserName or Password Incorrect.";
            //            txtPassword.Text = "";
            //            txtUsername.Text = "";
            //        }
            //    }
            //    else
            //    {
            //        int UserId = 0;
            //        UserId = Convert.ToInt32(ApplicationSession.USERID);

            //        Session.Abandon();
            //        //Response.Redirect("UserLogin.aspx", false);
            //        Response.Redirect("../UserLogin.aspx", false);
            //        //if (UserId != null || UserId != 0)
            //        //{
            //        //    Response.Redirect("Client.UI/TrustHome.aspx", false);
            //        //}
            //        //lblMsg.Visible = true;
            //        //lblMsg.Text = "User Already Logged In.";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;             
            //}
        }
        #endregion
    }
}