using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : Nafisamulla, 4/7/2014
	/// Summary description for Organisation.
	/// </summary>
	public class EmployeeBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion

		#region Select All Employee Details
		/// <summary>
		/// To Select All data from the tbl_Employee_M table
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Employee_SelectAll()
		{
			try
			{
				sSql = "usp_tbl_Employee_M_SelectAll";
				DataTable dtEmployee = new DataTable();
				dtEmployee = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtEmployee);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

        #region Select All Employee For Reporting To
        /// <summary>
        /// To Select All data from the tbl_Employee_M table
        /// Created By : Nafisamulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Employee_SelectAll_ForReportingTo()
        {
            try
            {
                sSql = "usp_tbl_Employee_M_SelectAll_ForReportingTo";
                DataTable dtEmployee = new DataTable();
                dtEmployee = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtEmployee);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select Employee Details by EmployeeID
		/// <summary>
		/// Select all details of Employee for selected EmployeeID from tbl_Employee_M table
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intEmployeeID"></param>
		/// <returns></returns>
		public ApplicationResult Employee_Select(int intEmployeeID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intEmployeeID;

				strStoredProcName = "usp_tbl_Employee_M_Select";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Delete Employee Details by EmployeeID
		/// <summary>
		/// To Delete details of Employee for selected EmployeeID from tbl_Employee_M table
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intEmployeeID"></param>
		/// <returns></returns>
		public ApplicationResult Employee_Delete(int intEmployeeID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@EmployeeID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intEmployeeID;

				strStoredProcName = "usp_tbl_Employee_M_Delete";

				int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				if (iResult > 0)
				{
					ApplicationResult objResults = new ApplicationResult();
					objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
					return objResults;
				}
				else
				{
					ApplicationResult objResults = new ApplicationResult();
					objResults.status = ApplicationResult.CommonStatusType.FAILURE;
					return objResults;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Insert Employee Details
		/// <summary>
		/// To Insert details of Employee in tbl_Employee_M table
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objEmployeeBO"></param>
		/// <returns></returns>
		public ApplicationResult Employee_Insert(EmployeeBO objEmployeeBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[21];


				pSqlParameter[0] = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objEmployeeBO.EmployeeName;

				pSqlParameter[1] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objEmployeeBO.EmployeeCode;

				pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objEmployeeBO.TrustMID;

				pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objEmployeeBO.SchoolMID;

				pSqlParameter[4] = new SqlParameter("@EmployeeRoleID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objEmployeeBO.EmployeeRoleID;

				pSqlParameter[5] = new SqlParameter("@DesignationID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objEmployeeBO.DesignationID;

				pSqlParameter[6] = new SqlParameter("@Address", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objEmployeeBO.Address;

				pSqlParameter[7] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objEmployeeBO.ContactNo;

				pSqlParameter[8] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objEmployeeBO.MobileNo;

				pSqlParameter[9] = new SqlParameter("@Email", SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objEmployeeBO.Email;

				pSqlParameter[10] = new SqlParameter("@UserName", SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
				pSqlParameter[10].Value = objEmployeeBO.UserName;

				pSqlParameter[11] = new SqlParameter("@Password", SqlDbType.VarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
				pSqlParameter[11].Value = objEmployeeBO.Password;

				pSqlParameter[12] = new SqlParameter("@JoinDate", SqlDbType.VarChar);
				pSqlParameter[12].Direction = ParameterDirection.Input;
				pSqlParameter[12].Value = objEmployeeBO.JoinDate;

				pSqlParameter[13] = new SqlParameter("@BirthDate", SqlDbType.VarChar);
				pSqlParameter[13].Direction = ParameterDirection.Input;
				pSqlParameter[13].Value = objEmployeeBO.BirthDate;

				pSqlParameter[14] = new SqlParameter("@MarriageDate", SqlDbType.VarChar);
				pSqlParameter[14].Direction = ParameterDirection.Input;
				pSqlParameter[14].Value = objEmployeeBO.MarriageDate;

				pSqlParameter[15] = new SqlParameter("@ResignedDate", SqlDbType.VarChar);
				pSqlParameter[15].Direction = ParameterDirection.Input;
				pSqlParameter[15].Value = objEmployeeBO.ResignedDate;

				pSqlParameter[16] = new SqlParameter("@ResignReason", SqlDbType.VarChar);
				pSqlParameter[16].Direction = ParameterDirection.Input;
				pSqlParameter[16].Value = objEmployeeBO.ResignReason;

				pSqlParameter[17] = new SqlParameter("@IsResigned", SqlDbType.Int);
				pSqlParameter[17].Direction = ParameterDirection.Input;
				pSqlParameter[17].Value = objEmployeeBO.IsResigned;

				pSqlParameter[18] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[18].Direction = ParameterDirection.Input;
				pSqlParameter[18].Value = objEmployeeBO.IsDeleted;

				pSqlParameter[19] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[19].Direction = ParameterDirection.Input;
				pSqlParameter[19].Value = objEmployeeBO.LastModifiedUserID;

				pSqlParameter[20] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[20].Direction = ParameterDirection.Input;
				pSqlParameter[20].Value = objEmployeeBO.LastModifiedDate;


				sSql = "usp_tbl_Employee_M_Insert";
				int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

				if (iResult > 0)
				{
					ApplicationResult objResults = new ApplicationResult();
					objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
					return objResults;
				}
				else
				{
					ApplicationResult objResults = new ApplicationResult();
					objResults.status = ApplicationResult.CommonStatusType.FAILURE;
					return objResults;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				objEmployeeBO = null;
			}
		}
		#endregion

		#region Update Employee Details
		/// <summary>
		/// To Update details of Employee in tbl_Employee_M table
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objEmployeeBO"></param>
		/// <returns></returns>
		public ApplicationResult Employee_Update(EmployeeBO objEmployeeBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[22];


				pSqlParameter[0] = new SqlParameter("@EmployeeID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objEmployeeBO.EmployeeID;

				pSqlParameter[1] = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objEmployeeBO.EmployeeName;

				pSqlParameter[2] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objEmployeeBO.EmployeeCode;

				pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objEmployeeBO.TrustMID;

				pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objEmployeeBO.SchoolMID;

				pSqlParameter[5] = new SqlParameter("@EmployeeRoleID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objEmployeeBO.EmployeeRoleID;

				pSqlParameter[6] = new SqlParameter("@DesignationID", SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objEmployeeBO.DesignationID;

				pSqlParameter[7] = new SqlParameter("@Address", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objEmployeeBO.Address;

				pSqlParameter[8] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objEmployeeBO.ContactNo;

				pSqlParameter[9] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objEmployeeBO.MobileNo;

				pSqlParameter[10] = new SqlParameter("@Email", SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
				pSqlParameter[10].Value = objEmployeeBO.Email;

				pSqlParameter[11] = new SqlParameter("@UserName", SqlDbType.VarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
				pSqlParameter[11].Value = objEmployeeBO.UserName;

				pSqlParameter[12] = new SqlParameter("@Password", SqlDbType.VarChar);
				pSqlParameter[12].Direction = ParameterDirection.Input;
				pSqlParameter[12].Value = objEmployeeBO.Password;

				pSqlParameter[13] = new SqlParameter("@JoinDate", SqlDbType.VarChar);
				pSqlParameter[13].Direction = ParameterDirection.Input;
				pSqlParameter[13].Value = objEmployeeBO.JoinDate;

				pSqlParameter[14] = new SqlParameter("@BirthDate", SqlDbType.VarChar);
				pSqlParameter[14].Direction = ParameterDirection.Input;
				pSqlParameter[14].Value = objEmployeeBO.BirthDate;

				pSqlParameter[15] = new SqlParameter("@MarriageDate", SqlDbType.VarChar);
				pSqlParameter[15].Direction = ParameterDirection.Input;
				pSqlParameter[15].Value = objEmployeeBO.MarriageDate;

				pSqlParameter[16] = new SqlParameter("@ResignedDate", SqlDbType.VarChar);
				pSqlParameter[16].Direction = ParameterDirection.Input;
				pSqlParameter[16].Value = objEmployeeBO.ResignedDate;

				pSqlParameter[17] = new SqlParameter("@ResignReason", SqlDbType.VarChar);
				pSqlParameter[17].Direction = ParameterDirection.Input;
				pSqlParameter[17].Value = objEmployeeBO.ResignReason;

				pSqlParameter[18] = new SqlParameter("@IsResigned", SqlDbType.Int);
				pSqlParameter[18].Direction = ParameterDirection.Input;
				pSqlParameter[18].Value = objEmployeeBO.IsResigned;

				pSqlParameter[19] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[19].Direction = ParameterDirection.Input;
				pSqlParameter[19].Value = objEmployeeBO.IsDeleted;

				pSqlParameter[20] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[20].Direction = ParameterDirection.Input;
				pSqlParameter[20].Value = objEmployeeBO.LastModifiedUserID;

				pSqlParameter[21] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[21].Direction = ParameterDirection.Input;
				pSqlParameter[21].Value = objEmployeeBO.LastModifiedDate;


				sSql = "usp_tbl_Employee_M_Update";
				int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

				if (iResult > 0)
				{
					ApplicationResult objResults = new ApplicationResult();
					objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
					return objResults;
				}
				else
				{
					ApplicationResult objResults = new ApplicationResult();
					objResults.status = ApplicationResult.CommonStatusType.FAILURE;
					return objResults;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				objEmployeeBO = null;
			}
		}
		#endregion

		#region Select Employee Details by EmployeeName
		/// <summary>
		/// Select all details of Employee for selected EmployeeName from tbl_Employee_M table
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="EmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Employee_Select_byEmployeeName(string strEmployeeName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strEmployeeName;

				strStoredProcName = "usp_tbl_Employee_M_Select_ByEmployee";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select Employee Details for Payroll Reports
		/// <summary>
		/// Select Employee Details For Salary Summary Report
		/// Created By : Yogesh Patel, 12/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="EmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Employee_Select_ForSalarySummary(int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				//pSqlParameter[0] = new SqlParameter("@Department", SqlDbType.Int);
				//pSqlParameter[0].Direction = ParameterDirection.Input;
				//pSqlParameter[0].Value = intDepartment;

				pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intSchoolMID;

				//pSqlParameter[2] = new SqlParameter("@Type", SqlDbType.Int);
				//pSqlParameter[2].Direction = ParameterDirection.Input;
				//pSqlParameter[2].Value = intType;

				//pSqlParameter[3] = new SqlParameter("@Designation", SqlDbType.VarChar);
				//pSqlParameter[3].Direction = ParameterDirection.Input;
				//pSqlParameter[3].Value = StrDesignation;



				strStoredProcName = "usp_tbl_Get_EmployeeList";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select  Details for Salary Summary
		/// <summary>
		/// Select Employee Details For Salary Summary Report
		/// Created By : Yogesh Patel, 16/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="EmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Employee_SalarySummary(int intTrustMID, int intMonth,int intYear,int intSchoolMID, int intIsEarning)
		{
			try
			{
				pSqlParameter = new SqlParameter[5];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = 1;

				pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intMonth;

				pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = intYear;

				//pSqlParameter[3] = new SqlParameter("@IsApproved", SqlDbType.Int);
				//pSqlParameter[3].Direction = ParameterDirection.Input;
				//pSqlParameter[3].Value = 1;

				pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = intSchoolMID;

				pSqlParameter[4] = new SqlParameter("@IsEarning", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = intIsEarning;


				strStoredProcName = "usp_rpt_EmployeeSalarySummaryReport";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Insert  Employee for Reports
		/// <summary>
		/// Insert Employee For  Report
		/// Created By : Yogesh Patel, 16/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="EmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Insert_Employee_TempTable(int IntEmployeeMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value =  IntEmployeeMID;

				



				strStoredProcName = "usp_tbl_Employee_M_Insert_Temp";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select  Details for PF Register
		/// <summary>
		/// Select Employee Details For Salary Summary Report
		/// Created By : Yogesh Patel, 16/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="EmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Employee_PFRegister(int intTrustMID, int intMonth, int intYear,int intIsApproved, int intSchoolMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[5];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = 1;

				pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intMonth;

				pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = intYear;

                pSqlParameter[3] = new SqlParameter("@IsApproved", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = 1;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = intSchoolMID;

				


				strStoredProcName = "usp_rpt_Employee_PF_Register";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select  Details for Bank_Statement
		/// <summary>
		/// Select Employee Details For Bank_Statement Report
		/// Created By : Yogesh Patel, 16/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="EmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Bank_Statement(int intTrustMID, int intSchoolMID, int intMonth, int intYear, int intinttype )
		{
			try
			{
				pSqlParameter = new SqlParameter[5];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = 1;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intSchoolMID;

				pSqlParameter[2] = new SqlParameter("@Month", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = intMonth;

				pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = intYear;

				pSqlParameter[4] = new SqlParameter("@IntType", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = 0;

				




				strStoredProcName = "usp_rpt_Employee_Bank_Statement";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select  Details for Professional Tax
		/// <summary>
		/// Select Employee Details ForProfessional_Tax Report
		/// Created By : Yogesh Patel, 25/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="Professional_Tax"></param>
		/// <returns></returns>
		public ApplicationResult Professional_Tax(int intTrustMID, int intSchoolMID, int intMonth, int intYear, int intinttype)
		{
			try
			{
				pSqlParameter = new SqlParameter[5];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = 1;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intSchoolMID;

				pSqlParameter[2] = new SqlParameter("@Month", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = intMonth;

				pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = intYear;

				pSqlParameter[4] = new SqlParameter("@IntType", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = 0;

				strStoredProcName = "usp_rpt_Employee_ProfessionalTax";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select  Details for Professional Tax
		/// <summary>
		/// Select Employee Details ForProfessional_Tax Report
		/// Created By : Yogesh Patel, 25/5/2022
		/// Modified By :
		/// </summary>
		/// <param name="Professional_Tax"></param>
		/// <returns></returns>
		public ApplicationResult EDLI_Report(int intTrustMID, int intSchoolMID, int intMonth, int intYear, int intinttype)
		{
			try
			{
				pSqlParameter = new SqlParameter[5];

				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = 1;

				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = intSchoolMID;

				pSqlParameter[2] = new SqlParameter("@Month", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = intMonth;

				pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = intYear;

				pSqlParameter[4] = new SqlParameter("@IntType", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = 0;

				strStoredProcName = "usp_rpt_Employee_EDLI_Report";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region ValidateName for Employee
		/// <summary>
		/// Function which validates whether the EmployeeName already exits in tbl_Employee_M table.
		/// Created By : Nafisamulla, 4/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strEmployeeName"></param>
		/// <returns></returns>
		public ApplicationResult Employee_ValidateName(string strEmployeeName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strEmployeeName;

				strStoredProcName = "usp_tbl_Employee_M_Validate_EmployeeName";

				DataTable dtResult = new DataTable();
				dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
				ApplicationResult objResults = new ApplicationResult(dtResult);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}


