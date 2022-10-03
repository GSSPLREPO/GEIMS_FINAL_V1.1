using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using GEIMS.BL;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;
using log4net;


namespace GEIMS.Client.UI
{
    public partial class AdmissionForm : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(AdmissionForm));
        public static string srclink;
        public static int FormNo;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GetSchoolName();
                   // BindSyllabusMaster();
                   // BindYear();
                    //BindEmployeeList();
                    ViewState["Mode"] = "Save";


                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Data Saved Successfully.');</script>");
            }
        }

       
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string newlink = srclink;
            //string script = "window.onload = function() { newSrc('" + newlink + "'); };";
            //ClientScript.RegisterStartupScript(this.GetType(), "newSrc", script, true);

            

            //Response.Write("<script> window.open('" + newlink + "','_blank'); </script>");



            btnSaveClass.Visible = false;
            AdmissionFormBO objAdmissionFormBO = new AdmissionFormBO();
            AdmissionFormBL objAdmissionFormBL = new AdmissionFormBL();

            ApplicationResult objResult = new ApplicationResult();
            DataTable dtResult = new DataTable();

            objAdmissionFormBO.SchoolMID = Convert.ToInt32(ddlSchoolName.SelectedValue);
            objAdmissionFormBO.SectionID = Convert.ToInt32(ddlSection.SelectedValue);
            objResult = objAdmissionFormBL.AddmissionForm_Select(objAdmissionFormBO.SchoolMID, objAdmissionFormBO.SectionID);



            ApplicationResult objResult1 = new ApplicationResult();
            //CODE FOR AUTO INCREMENT NUMBER START

            if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
            {

                DataTable dtResult1 = objResult.resultDT;
                if (dtResult1.Rows.Count > 0)
                {
                    FormNo = Convert.ToInt32(dtResult1.Rows[0]["AdmissionFormNo"]);
                    FormNo = (FormNo) + 1;
           }
                else
                {
                    FormNo = 0;
                    FormNo = (FormNo) + 1;
                 }

                Session["FormNo"] = FormNo;
                //CODE FOR AUTO INCREMENT NUMBER END




                // ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record updated successfully.');</script>");
            }

            if (Convert.ToInt32(ddlSection.SelectedValue) == 1)
            {
                srclink = "AdmissionFormEng.aspx";//secondary English Medium



              //  Response.Write("<script> window.print('" + "AdmissionFormEng.aspx" + "','_blank'); </script>");

                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");

               

            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1016)
            {
                srclink = "AdmissionFormEng.aspx";//Higher secondary English Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 4)
            {
                srclink = "AdmissionFormGuj.aspx"; //secondary Gujarati Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1017)
            {
                srclink = "AdmissionFormGuj.aspx";// Higher secondary Gujarati Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1012)
            {
                srclink = "AdmissionFormGuj.aspx"; //K.G. Gujarati Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1013)
            {
                srclink = "AdmissionFormEng.aspx"; //K.G. English Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1014)
            {
                srclink = "AdmissionFormGuj.aspx"; //Primary Gujarati Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1015)
            {
                srclink = "AdmissionFormEng.aspx"; //Primary English Medium
                Response.Write("<script> window.open('" + srclink + "','_blank'); </script>");
            }
           


            btnSaveClass_Click(sender, e);

        }
        #region School Selected Change Event
        protected void ddlSchoolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                SectionBL objSectionBl = new SectionBL();

                if(ddlSchoolName.SelectedIndex !=0)
                {
                    objResult = objSectionBl.Section_SelectAll_ForDropDown(Convert.ToInt32(ddlSchoolName.SelectedValue));

                    if (objResult != null)
                    {
                        ddlSection.Enabled = true;
                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            objControls.BindDropDown_ListBox(objResult.resultDT, ddlSection, "SectionName", "SectionMID");


                        }
                        ddlSection.Items.Insert(0, new ListItem("--Select--", ""));


                        if (Convert.ToInt32(ddlSchoolName.SelectedValue) == 0)
                        {
                            ddlSection.Enabled = false;
                        }
                        else if (Convert.ToInt32(ddlSchoolName.SelectedValue) == 3)
                        {
                            ListItem removeItem = ddlSection.Items.FindByValue("1011");
                            ddlSection.Items.Remove(removeItem);

                        }


                    }
                }
                else
                {
                    ddlSection.Enabled = false;
                }



            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Section Seclected Change Event
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplicationResult objResult = new ApplicationResult();
                Controls objControls = new Controls();
                ClassBL objClassBl = new ClassBL();
                if(ddlSection.SelectedIndex!=0)
                {
                    objResult = objClassBl.Class_SelectAll_SectionWise_ForDropDown(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSchoolName.SelectedValue));
                    if (objResult != null)
                    {

                        if (objResult.resultDT.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(ddlSection.SelectedValue) == 1)
                            {
                                srclink = "AdmissionFormEng.aspx";//secondary English Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1016)
                            {
                                srclink = "AdmissionFormEng.aspx";//Higher secondary English Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 4)
                            {
                                srclink = "AdmissionFormGuj.aspx"; //secondary Gujarati Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1017)
                            {
                                srclink = "AdmissionFormGuj.aspx";// Higher secondary Gujarati Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1012)
                            {
                                srclink = "AdmissionFormGuj.aspx"; //K.G. Gujarati Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1013)
                            {
                                srclink = "AdmissionFormEng.aspx"; //K.G. English Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1014)
                            {
                                srclink = "AdmissionFormGuj.aspx"; //Primary Gujarati Medium
                            }
                            else if (Convert.ToInt32(ddlSection.SelectedValue) == 1015)
                            {
                                srclink = "AdmissionFormEng.aspx"; //Primary English Medium
                            }


                            //     objControls.BindDropDown_ListBox(objResult.resultDT, ddlClassName, "ClassName", "ClassMID");
                        }
                        // ddlClassName.Items.Insert(0, new ListItem("--Select--", ""));


                    }
                }
                else
                {

                }

               
            }
            catch (Exception)
            {

                throw;
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
        #endregion//

        #region Panel Visibility Mode
        public void PanelVisibility(int intcode)
        {
            if (intcode == 1)
            {
               // divGrid.Visible = true;
                //tabs.Visible = false;
                
            }
            else if (intcode == 2)
            {
                //divGrid.Visible = false;
                //tabs.Visible = true;
                
            }
        }
        #endregion

        #region Fetch School

        public void GetSchoolName()
        {
            ApplicationResult objResult = new ApplicationResult();
            Controls objControls = new Controls();
            SchoolBL objSchoolBl = new SchoolBL();

            objResult = objSchoolBl.School_SelectAll_ForDropDOwn(Convert.ToInt32(Session[ApplicationSession.TRUSTID]));
            if (objResult != null)
            {

                if (objResult.resultDT.Rows.Count > 0)
                {
                    objControls.BindDropDown_ListBox(objResult.resultDT, ddlSchoolName, "SchoolNameEng", "SchoolMID");

                }
                ddlSchoolName.Items.Insert(0, new ListItem("--Select--", ""));
                ddlSection.Items.Insert(0, new ListItem("--Select--", ""));
               
            }
        }

        #endregion

        #region Fetch Section data For DropDown 
        private DataTable FetchSection(int intSchoolMID)
        {
            //DataTable dtDivision = new DataTable();

            SectionBL objSectionBL = new SectionBL();
            SectionBO objSectionBO = new SectionBO();
            ApplicationResult objResults = new ApplicationResult();

            objResults = objSectionBL.Section_SelectAll_ForDropDown(intSchoolMID);
            if (objResults.status == ApplicationResult.CommonStatusType.SUCCESS && objResults.resultDT.Rows.Count > 0)
            {

            }
            return objResults.resultDT;
        }

        #endregion

        protected void lnkAddNewClass_OnClick(object sender, EventArgs e)
        {

        }

        protected void lnkViewList_OnClick(object sender, EventArgs e)
        {

        }

        

        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSchoolName.SelectedIndex != 0 && ddlSection.SelectedIndex != 0 )
                {

                    AdmissionFormBO objAdmissionFormBO = new AdmissionFormBO();
                    AdmissionFormBL objAdmissionFormBL = new AdmissionFormBL();
                                        
                    ApplicationResult objResult = new ApplicationResult();
                    DataTable dtResult = new DataTable();


                    objAdmissionFormBO.AdmissionFormNo = Convert.ToString(FormNo);
                    objAdmissionFormBO.SchoolMID = Convert.ToInt32(ddlSchoolName.SelectedValue);
                    objAdmissionFormBO.SectionID = Convert.ToInt32(ddlSection.SelectedValue);
                    var year = DateTime.Now.Year.ToString();                    
                    objAdmissionFormBO.Year = year;



                    if (ViewState["Mode"].ToString() == "Save")
                    {
                        objAdmissionFormBO.CreatedByID = Convert.ToInt32(Session[ApplicationSession.USERID]);
                        objAdmissionFormBO.CreatedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objResult = objAdmissionFormBL.AdmissionForm_Insert(objAdmissionFormBO);

                        //Commented on 27/09/2022 Bhandavi
                        //alert message is coming after clicking on print and preview button,
                        //if (objResult.status == ApplicationResult.CommonStatusType.SUCCESS)
                        //{
                        //    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Record saved successfully.');</script>");
                        //}
                    }

                    ClearAll();
                    //BindSyllabusMaster();
                    //PanelVisibility(1);
                    ddlSection.Items.Clear();
                    btnSaveClass.Visible = false;
                    //Added on 27/09/2022 Bhandavi
                    //ection name dropdown is blank(Not showing select in section dropdown after print and preview button clicked)
                    ddlSection.Items.Insert(0, new ListItem("--Select--", ""));
                }


                
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }

      
    }

}