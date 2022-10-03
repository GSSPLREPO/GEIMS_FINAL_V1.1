using System;
using System.Web.UI.WebControls;
using System.Data;
using GEIMS.BL;
using GEIMS.Common;
using log4net;
using System.Web.UI;

namespace GEIMS.Client.UI
{
    public partial class MainSection : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(MainSection));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                  
                        #region ManageRole
                        //string sPath = Page.Page.AppRelativeVirtualPath;
                        //string sRet = sPath.Substring(sPath.LastIndexOf('/') + 1);
                        //ManageRolls MR = new ManageRolls();
                        //HtmlControl a = (HtmlControl)this.Page.Master.FindControl("hrefMainPanel");
                        //if (a != null)
                        //{
                        //    if (a.Visible == true)
                        //    {
                        //        MR.manageRolls(sRet, Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.USERID].ToString()), Request.QueryString["Menu"].ToString(), 1);
                        //    }
                        //}
                        //else
                        //{
                        //    MR.manageRolls(sRet, Convert.ToInt32(Session[ApplicationSession.ROLEID].ToString()), Convert.ToInt32(Session[ApplicationSession.USERID].ToString()), Request.QueryString["Menu"].ToString(), 0);
                        //}
                        #endregion

                        GridDataBind();

                    //if (Session[ApplicationSession.ROLENAME].ToString() != "Administrator")
                    //{
                    //for (int i = 0; i < grCollegeDetails.Rows.Count; i++)
                    //{
                    //    if (Convert.ToInt32(grCollegeDetails.Rows[i].Cells[0].Text) == Convert.ToInt32(Session[ApplicationSession.SCHOOLID]))
                    //    {
                    //        grCollegeDetails.Rows[i].Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        grCollegeDetails.Rows[i].Enabled = false;
                    //    }
                    //}
                    //}
                    //else
                    //{
                    //    for (int i = 0; i < grCollegeDetails.Rows.Count; i++)
                    //    {
                    //        //if (Convert.ToInt32(grCollegeDetails.Rows[i].Cells[0].Text) == Convert.ToInt32(Session[ApplicationSession.COLLEGEID]))
                    //        //{
                    //        grCollegeDetails.Rows[i].Enabled = true;
                    //        //}
                    //    }
                    //}

                }
                catch (Exception ex)
                {
                   logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
                }
            }
        }
        #region Bind Grid
        private void GridDataBind()
        {
            try
            {
                lblMsg.Visible = false;
                SchoolBL objSchoolBL = new SchoolBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objSchoolBL.School_SelectAll(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
                if (objResult != null)
                {
                    DataTable dtCollege = new DataTable();
                    dtCollege = objResult.resultDT;
                    if (dtCollege.Rows.Count > 0)
                    {
						grCollDetails.DataSource = dtCollege;
						grCollDetails.DataBind();
                    } 
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "No School Added";
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

        #region Gridview Rowcommand event
        protected void grCollegeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Manage")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
					Session[ApplicationSession.SCHOOLNAME] = grCollDetails.Rows[row.RowIndex].Cells[1].Text;
                    Session[ApplicationSession.SCHOOLID] = Convert.ToInt32(e.CommandArgument.ToString());
                    //Session[ApplicationSession.GTU_GU_CODE] = grCollegeDetails.Rows[row.RowIndex].Cells[2].Text;
                    //Session[ApplicationSession.ADMISSION_TYPE] = grCollegeDetails.Rows[row.RowIndex].Cells[3].Text;

                    Response.Redirect("../Client.UI/SchoolHome.aspx", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Gridview Page index
        protected void grCollegeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
				grCollDetails.PageIndex = e.NewPageIndex;
                GridDataBind();
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        protected void grCollegeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
			//if (e.Row.RowType == DataControlRowType.DataRow)
			//{
			//	e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#3B5998';");
			//	e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
			//}
        }

    }
}