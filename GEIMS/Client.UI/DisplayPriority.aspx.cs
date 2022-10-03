using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class DisplayPriority : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(DisplayPriority));

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindClass();
                BindRadioButtonList();
                BindFeesCategory();
                divClass.Visible = false;
                divFeesCategory.Visible = false;
            }
        }
        #endregion

        #region Bind Fees Category
        public void BindFeesCategory()
        {
            try
            {
                FeesCategoryBL objFeesCategoryBL = new FeesCategoryBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objFeesCategoryBL.FeesCategory_SelectAll(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvFees.DataSource = objResult.resultDT;
                        gvFees.DataBind();
                        //PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        //PanelGrid_VisibilityMode(2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Bind Class
        public void BindClass()
        {
            try
            {
                ClassBL objClasstBl = new ClassBL();
                ApplicationResult objResult = new ApplicationResult();

                objResult = objClasstBl.Class_SelectAll_WithDivision(Convert.ToInt32(Session[ApplicationSession.SCHOOLID]));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        gvClass.DataSource = objResult.resultDT;
                        gvClass.DataBind();
                        //PanelGrid_VisibilityMode(1);
                    }
                    else
                    {
                        //PanelGrid_VisibilityMode(2);
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

        #region Bind Radio Button List
        public void BindRadioButtonList()
        {
            rblToggleView.Items.Add(new ListItem("Class", "1"));
            rblToggleView.Items.Add(new ListItem("Fees Category", "2"));
        }
        #endregion

        #region Button Go Click Event
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (rblToggleView.SelectedItem.Text == "Class")
            {
                try
                {
                    ApplicationResult objResult = new ApplicationResult();
                    ClassBL objClassBL = new ClassBL();
                    ClassBO objClassBO = new ClassBO();
                    #region RollBack Transaction Starts

                    DatabaseTransaction.OpenConnectionTransation();
                    for (int i = 0; i < gvClass.Rows.Count; i++)
                    {
                        TextBox txtSequence = (TextBox)gvClass.Rows[i].FindControl("txtSequence");
                        if (txtSequence.Text != "")
                        {
                            objClassBO.Priority = Convert.ToInt32(txtSequence.Text);
                            objClassBO.ClassMID = Convert.ToInt32(gvClass.Rows[i].Cells[0].Text);
                            objClassBO.ClassName = gvClass.Rows[i].Cells[1].Text;
                            objClassBO.SchoolMID = Convert.ToInt32(Session[ApplicationSession.SCHOOLID]);


                            objResult = objClassBL.Class_Update_For_Priority(objClassBO);
                            if (objResult != null)
                            {

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator...');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script>alert('Enter the priority.');</script>");
                            break;
                        }
                    }
                    DatabaseTransaction.CommitTransation();
                    #endregion

                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                        "<script>alert('Record Saved Successfully.');</script>");


                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

                }
            }
            else if (rblToggleView.SelectedItem.Text == "Fees Category")
            {
                try
                {
                    ApplicationResult objResult = new ApplicationResult();
                    FeesCategoryBL objFeesCategoryBL = new FeesCategoryBL();
                    FeesCategoryBO objFeesCategoryBO = new FeesCategoryBO();
                    #region RollBack Transaction Starts

                    DatabaseTransaction.OpenConnectionTransation();
                    for (int i = 0; i < gvFees.Rows.Count; i++)
                    {
                        TextBox txtSequence = (TextBox)gvFees.Rows[i].FindControl("txtSequence1");
                        if (txtSequence.Text != "")
                        {
                            objFeesCategoryBO.Priority = Convert.ToInt32(txtSequence.Text);
                            objFeesCategoryBO.FeesCategoryMID = Convert.ToInt32(gvFees.Rows[i].Cells[0].Text);
                            objFeesCategoryBO.FeesName = gvFees.Rows[i].Cells[1].Text;

                            objResult = objFeesCategoryBL.FeesCategory_Update_For_Priority(objFeesCategoryBO);
                            if (objResult != null)
                            {

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script>alert('Oops! There is some technical issue. Please Contact to your administrator...');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                                    "<script>alert('Enter the priority.');</script>");
                            break;
                        }

                    }
                    DatabaseTransaction.CommitTransation();
                    #endregion

                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp",
                        "<script>alert('Record Saved Successfully.');</script>");

                }
                catch (Exception ex)
                {
                    logger.Error("Error", ex);
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");

                }

            }
            else
            {

            }
        }
        #endregion

        protected void rblToggleView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblToggleView.SelectedItem.Text == "Class")
            {
                divClass.Visible = true;
                divFeesCategory.Visible = false;
            }
            else if (rblToggleView.SelectedItem.Text == "Fees Category")
            {
                divClass.Visible = false;
                divFeesCategory.Visible = true;
            }
            else
            {

            }

        }

        protected void rblToggleView_TextChanged(object sender, EventArgs e)
        {

        }
    }
}