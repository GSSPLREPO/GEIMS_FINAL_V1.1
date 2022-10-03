using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;

namespace GEIMS.Client.UI
{
    public partial class DocumentMaster : PageBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DocumentMaster));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[ApplicationSession.USERID] != null || Session[ApplicationSession.USERID] != "")
            { 
                hdnLastUserID.Value = Session[ApplicationSession.USERID].ToString();
            }
            if (Session["UserName"] == null)
            {
                Response.Redirect("../UserLogin.aspx");
            }

            if (!IsPostBack)
            {
                bindSchool();
                bindTrust();
                string Serverpath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                string sDirPath = Server.MapPath(Serverpath);
                hdnUploadFilePath.Value = sDirPath;
            }
        }

        #region bindSchool
        public void bindSchool()
        {
            try
            {
                #region Bind School

                SchoolBL objSchoolBL = new SchoolBL();
                SchoolBO objSchoolBO = new SchoolBO();
                DocumentBL objDocumentBl = new DocumentBL();
                Controls objControls = new Controls();
                ApplicationResult objResults = new ApplicationResult();

                objResults = objSchoolBL.School_SelectAll_All();
                if (objResults != null)
                {
                    if (objResults.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResults.resultDT, ddlSchoolID, "SchoolNameEng", "SchoolMID");

                    }
                    ddlSchoolID.Items.Insert(0, new ListItem("-Select-", "-1"));

                }
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region bindTrust
        public void bindTrust()
        {
            try
            {
                #region Bind Trust

                TrustBL objTrustBL = new TrustBL();
                TrustBO objTrustBO = new TrustBO();
                Controls objControls = new Controls();
                ApplicationResult objResultTrust = new ApplicationResult();
                objResultTrust = objTrustBL.Trust_SelectAll();
                if (objResultTrust != null)
                {
                    if (objResultTrust.resultDT.Rows.Count > 0)
                    {
                        objControls.BindDropDown_ListBox(objResultTrust.resultDT, ddlTrustID, "TrustNameEng", "TrustMID");

                    }
                    ddlTrustID.Items.Insert(0, new ListItem("-Select-", "-1"));

                }
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Create Folder
        protected void CreateFolder()
        {
            try
            {
                Random random = new Random();
                string RanNumber = Convert.ToString(random.Next(1, 1000000000));

                string Serverpath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                string sDirPath = Server.MapPath(Serverpath + RanNumber);
                DirectoryInfo ObjSearchDir = new DirectoryInfo(sDirPath);

                if (!ObjSearchDir.Exists)
                {
                    ObjSearchDir.Create();
                    hdnFileFolder.Value = RanNumber;
                    hdnUploadFilePath.Value = sDirPath;
                }
                else
                {
                    hdnFileFolder.Value = "0";
                }

            }
            catch (Exception ex)
            {
                logger.Error("Error", ex);
                ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "<script>alert('Oops! There is some technical issue. Please Contact to your administrator.');</script>");
            }
        }
        #endregion

        #region Webservice
        [System.Web.Services.WebMethod]
        public static string LoadDocument(int intSchoolID, int intTrustMID, int intStudentMID, int intEmployeeMID)
        {
            try
            {
                #region Bind Section
                DataTable dtStudent = new DataTable();
                DocumentBL objDocumentBL = new DocumentBL();
                DocumentBO objDocumentBO = new DocumentBO();

                ApplicationResult objResultSection = new ApplicationResult();
                objResultSection = objDocumentBL.Document_SelectAll_ForDropDown(intSchoolID, intTrustMID, intStudentMID, intEmployeeMID);
                if (objResultSection != null)
                {
                    dtStudent = objResultSection.resultDT;
                    if (dtStudent.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtStudent.Rows.Count; i++)
                        {

                        }
                    }

                }
                string res = "";
                res = DataSetToJSON(dtStudent);
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string DataSetToJSON(DataTable dt)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            JavaScriptSerializer json = new JavaScriptSerializer();

            return json.Serialize(rows);
        }

        #endregion

    }
}