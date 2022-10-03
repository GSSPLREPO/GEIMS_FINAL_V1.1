using System;
using System.Data;
using System.Web;
using System.IO;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;

namespace GEIMS.Client.UI
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {
        #region Declaretion
        Controls objControls = new Controls();
        DocumentBO objDocumentBo = new DocumentBO();
        DocumentBL objDocumentBl = new DocumentBL();
        DataTable dtDocument = new DataTable();
        int intDocMID = 0;
        #endregion

        int mFileSize = 0;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.QueryString["path"] != null && context.Request.QueryString["file"] != null && context.Request.QueryString["SchoolID"] != null && context.Request.QueryString["TrustID"] != null && context.Request.QueryString["DocMID"] != null && context.Request.QueryString["EmployeeMID"] != null)
                {


                    string Serverpath = "";
                    string filename = context.Request.QueryString["file"].ToString();
                    // Serverpath = Serverpath + "\\" + filename;

                    if (context.Request.QueryString["SchoolID"].ToString() != "0")
                    {
                        Serverpath = "../Document/SchoolDocument/";
                        Serverpath = Serverpath + filename;
                        File.Delete(context.Server.MapPath(Serverpath));
                        objDocumentBo.SchoolMID = Convert.ToInt32(context.Request.QueryString["SchoolID"].ToString());
                        objDocumentBo.TrustMID = 0;
                        objDocumentBo.StudentMID = 0;
                        objDocumentBo.EmployeeMID = 0;


                        ApplicationResult objResultsDelete = new ApplicationResult();
                        objDocumentBo.DocumentMID = Convert.ToInt32(context.Request.QueryString["DocMID"].ToString());
                        objResultsDelete = objDocumentBl.Document_Delete(objDocumentBo.DocumentMID);
                        if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                        }

                    }
                    else if (context.Request.QueryString["TrustID"].ToString() != "0")
                    {
                        Serverpath = "../Document/TrustDocument/";
                        Serverpath = Serverpath + filename;
                        File.Delete(context.Server.MapPath(Serverpath));
                        objDocumentBo.TrustMID = Convert.ToInt32(context.Request.QueryString["TrustID"].ToString());
                        objDocumentBo.SchoolMID = 0;
                        objDocumentBo.StudentMID = 0;
                        objDocumentBo.EmployeeMID = 0;

                        ApplicationResult objResultsDelete = new ApplicationResult();
                        objDocumentBo.DocumentMID = Convert.ToInt32(context.Request.QueryString["DocMID"].ToString());
                        objResultsDelete = objDocumentBl.Document_Delete(objDocumentBo.DocumentMID);
                        if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                        }
                    }
                    if (context.Request.QueryString["EmployeeMID"].ToString() != "0")
                    {
                        Serverpath = "../Document/EmployeeDocument/";
                        Serverpath = Serverpath + filename;
                        File.Delete(context.Server.MapPath(Serverpath));
                        objDocumentBo.EmployeeMID = Convert.ToInt32(context.Request.QueryString["EmployeeMID"].ToString());
                        objDocumentBo.SchoolMID = 0;
                        objDocumentBo.StudentMID = 0;
                        objDocumentBo.TrustMID = 0;


                        ApplicationResult objResultsDelete = new ApplicationResult();
                        objDocumentBo.DocumentMID = Convert.ToInt32(context.Request.QueryString["DocMID"].ToString());
                        objResultsDelete = objDocumentBl.Document_Delete(objDocumentBo.DocumentMID);
                        if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                        }
                    }
                    else
                    {
                        Serverpath = "../Document/StudentDocument/";
                        Serverpath = Serverpath + filename;
                        File.Delete(context.Server.MapPath(Serverpath));
                        objDocumentBo.StudentMID = Convert.ToInt32(context.Request.QueryString["StudentMID"].ToString());
                        objDocumentBo.SchoolMID = 0;
                        objDocumentBo.TrustMID = 0;
                        objDocumentBo.EmployeeMID = 0;

                        ApplicationResult objResultsDelete = new ApplicationResult();
                        objDocumentBo.DocumentMID = Convert.ToInt32(context.Request.QueryString["DocMID"].ToString());
                        objResultsDelete = objDocumentBl.Document_Delete(objDocumentBo.DocumentMID);
                        if (objResultsDelete.status == ApplicationResult.CommonStatusType.SUCCESS)
                        {

                        }
                    }
                    //for deleting existing File by file name
                }
                else if (context.Request.QueryString["filepath"] != null && context.Request.QueryString["file"] != null && context.Request.QueryString["SchoolID"] != null && context.Request.QueryString["TrustID"] != null && context.Request.QueryString["EmployeeMID"] != null)
                {

                    //for downloading existing File

                    string filepath = "";
                    string file = context.Request.QueryString["file"].ToString();
                    if (context.Request.QueryString["SchoolID"].ToString() != "0")
                    {
                        filepath = "../Document/SchoolDocument/";
                        filepath = filepath + file;
                        //objDocumentBo.SchoolMID = Convert.ToInt32(context.Request.QueryString["SchoolID"].ToString());
                        //objDocumentBo.TrustMID = 0;
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", file));
                        context.Response.WriteFile(filepath);
                        context.Response.Flush();

                    }
                    else if (context.Request.QueryString["TrustID"].ToString() != "0")
                    {
                        filepath = "../Document/TrustDocument/";
                        filepath = filepath + file;
                        //objDocumentBo.TrustMID = Convert.ToInt32(context.Request.QueryString["TrustID"].ToString());
                        //objDocumentBo.SchoolMID = 0;
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", file));
                        context.Response.WriteFile(filepath);
                        context.Response.Flush();
                    }
                    else if (context.Request.QueryString["EmployeeMID"].ToString() != "0")
                    {
                        filepath = "../Document/EmployeeDocument/";
                        filepath = filepath + file;
                        //objDocumentBo.TrustMID = Convert.ToInt32(context.Request.QueryString["TrustID"].ToString());
                        //objDocumentBo.SchoolMID = 0;
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", file));
                        context.Response.WriteFile(filepath);
                        context.Response.Flush();
                    }
                    else
                    {
                        filepath = "../Document/StudentDocument/";
                        filepath = filepath + file;
                        //objDocumentBo.TrustMID = Convert.ToInt32(context.Request.QueryString["TrustID"].ToString());
                        //objDocumentBo.SchoolMID = 0;
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", file));
                        context.Response.WriteFile(filepath);
                        context.Response.Flush();
                    }
                    //if (File.Exists(filepath))
                    //{
                    //    context.Response.Clear();
                    //    context.Response.ContentType = "application/octet-stream";
                    //    context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", file));
                    //    context.Response.WriteFile(filepath  + file);
                    //    context.Response.Flush();
                    //}

                }
                else
                {
                    //for uploading new File

                    //objDocumentBo.TrustMID = context.Request.QueryString["TrustID"].ToString();
                    string Serverpath = "";
                    if (context.Request.QueryString["SchoolID"].ToString() != "0")
                    {
                        Serverpath = "../Document/SchoolDocument/";
                    }
                    else if (context.Request.QueryString["TrustID"].ToString() != "0")
                    {
                        Serverpath = "../Document/TrustDocument/";
                    }
                    else if (context.Request.QueryString["EmployeeMID"].ToString() != "0")
                    {
                        Serverpath = "../Document/EmployeeDocument/";
                    }
                    else
                    {
                        Serverpath = "../Document/StudentDocument/";
                    }
                    var postedFile = context.Request.Files[0];
                    string filesize = System.Configuration.ConfigurationManager.AppSettings["FileSize"];
                    mFileSize = postedFile.ContentLength / 1048576;

                    if (mFileSize <= Convert.ToInt32(filesize))
                    {
                        // Get Server Folder to upload file
                        // string foldername = context.Request.QueryString["id"].ToString();
                        //Serverpath = Serverpath + foldername;
                        string Savepath = context.Server.MapPath(Serverpath);
                        string file;

                        //For IE to get file name
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
                        {
                            string[] files = postedFile.FileName.Split(new char[] { '\\' });
                            file = files[files.Length - 1];

                        }
                        //For Other Browser to get file name
                        else
                        {
                            file = postedFile.FileName;
                        }
                        if (context.Request.QueryString["SchoolID"].ToString() != "0")
                        {
                            Serverpath = "../Document/SchoolDocument/";
                            Serverpath = Serverpath + file;
                            objDocumentBo.SchoolMID = Convert.ToInt32(context.Request.QueryString["SchoolID"].ToString());
                            objDocumentBo.TrustMID = 0;
                            objDocumentBo.StudentMID = 0;
                            objDocumentBo.EmployeeMID = 0;
                        }
                        else if (context.Request.QueryString["TrustID"].ToString() != "0")
                        {
                            Serverpath = "../Document/TrustDocument/";
                            Serverpath = Serverpath + file;
                            objDocumentBo.TrustMID = Convert.ToInt32(context.Request.QueryString["TrustID"].ToString());
                            objDocumentBo.SchoolMID = 0;
                            objDocumentBo.StudentMID = 0;
                            objDocumentBo.EmployeeMID = 0;
                        }
                        else if (context.Request.QueryString["EmployeeMID"].ToString() != "0")
                        {
                            Serverpath = "../Document/EmployeeDocument/";
                            Serverpath = Serverpath + file;
                            objDocumentBo.EmployeeMID = Convert.ToInt32(context.Request.QueryString["EmployeeMID"].ToString());
                            objDocumentBo.SchoolMID = 0;
                            objDocumentBo.StudentMID = 0;
                            objDocumentBo.TrustMID = 0;
                        }
                        else
                        {
                            Serverpath = "../Document/StudentDocument/";
                            Serverpath = Serverpath + file;
                            objDocumentBo.StudentMID = Convert.ToInt32(context.Request.QueryString["StudentMID"].ToString());
                            objDocumentBo.SchoolMID = 0;
                            objDocumentBo.TrustMID = 0;
                            objDocumentBo.EmployeeMID = 0;
                        }
                        //if (!Directory.Exists(Savepath))
                        //    Directory.CreateDirectory(Savepath);

                        string fileDirectory = Savepath + file;
                        postedFile.SaveAs(fileDirectory);

                       
                        objDocumentBo.DocumentName = file;
                        objDocumentBo.DocumentPath = Serverpath;
                        objDocumentBo.LastModifiedDate = DateTime.UtcNow.AddHours(5.5).ToString();
                        objDocumentBo.LastModifiedUserID = Convert.ToInt32(context.Request.QueryString["UserID"].ToString());
                        ApplicationResult objResultsInsert = new ApplicationResult();
                        objResultsInsert = objDocumentBl.Document_Insert(objDocumentBo);
                        string msg;
                        
                        if (objResultsInsert.status == ApplicationResult.CommonStatusType.SUCCESS && objResultsInsert.resultDT.Rows.Count > 0)
                        {

                            dtDocument = objResultsInsert.resultDT;
                             intDocMID = Convert.ToInt32(dtDocument.Rows[0][0].ToString());
                             msg = "{";
                             msg += string.Format("error:'{0}',\n", string.Empty);
                             msg += string.Format("upfile:'{0}',\n", file);
                             msg += string.Format("DocMID:'{0}'\n", intDocMID);
                             msg += "}";
                             context.Response.Write(msg);
                           // context.Response.Write(msg);
                        }
                        else
                        {
                            //msg = "{";
                            //msg += string.Format("error:'{0}',\n", string.Empty);
                            //msg += string.Format("upfile:'{0}'\n", file);
                            //msg += string.Format("DocMID:'{0}'", intDocMID);
                            //msg += "}";
                            //context.Response.Write(msg);
                        }

                        //Set response message

                    }
                }
            }
            catch (Exception ex)
            {
                //context.Response.Write("Error: " + ex.Message);
            }
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}