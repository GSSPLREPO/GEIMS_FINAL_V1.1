using System;
using System.Web;
using System.Data;
using GEIMS.BL;
using GEIMS.Common;

namespace GEIMS.Client.UI
{
	/// <summary>
	/// Summary description for GetImage
	/// </summary>
	public class GetImage : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			DataTable dtResult = new DataTable();
			SchoolBL objSchoolBL = new SchoolBL();
            StudentBL objStudentBL = new StudentBL();
            EmployeeMBL objEmployeeBL = new EmployeeMBL();
			TrustBL objTrustBL = new TrustBL();
			ApplicationResult objResult = new ApplicationResult();
			Controls objControl = new Controls();
			byte[] imgByte = { };
            if (context.Request.QueryString["StudentMID"] != null)
            {
                objResult = objStudentBL.Student_Select(Convert.ToInt32(context.Request.QueryString["StudentMID"].ToString()),0);
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        imgByte = (byte[])objResult.resultDT.Rows[0]["StudentPhoto"];
                    }
                }
            }
			else if (context.Request.QueryString["SchoolMID"] != null)
			{
				objResult = objSchoolBL.School_Select(Convert.ToInt32(context.Request.QueryString["SchoolMID"].ToString()));
				if (objResult != null)
				{
					if (objResult.resultDT.Rows.Count > 0)
					{
						 imgByte = (byte[])objResult.resultDT.Rows[0]["SchoolLogo"];
					}
				}
			}
            else if (context.Request.QueryString["EmployeeMID"] != null)
            {
                objResult = objEmployeeBL.EmployeeM_Select(Convert.ToInt32(context.Request.QueryString["EmployeeMID"].ToString()));
                if (objResult != null)
                {
                    if (objResult.resultDT.Rows.Count > 0)
                    {
                        imgByte = (byte[])objResult.resultDT.Rows[0]["Photo"];
                    }
                }
            }
			else
			{
				objResult = objTrustBL.Trust_Select(Convert.ToInt32(context.Request.QueryString["TrustMID"].ToString()));
				if (objResult != null)
				{
					if (objResult.resultDT.Rows.Count > 0)
					{
						 imgByte = (byte[])objResult.resultDT.Rows[0]["TrustLogo"];
					}
				}
			}
			
			// objResult = objVisitorBL.Visitor_Select(1);
			 
					
					context.Response.BinaryWrite(imgByte);
					context.Response.End();
					context.Response.Clear();
					context.Response.ClearHeaders();
					context.Response.ClearContent();
				
			

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