using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GEIMS.DataAccess;
using GEIMS.Bo;
using GEIMS.Common;

namespace GEIMS.Bl
{
	/// <summary>
    /// Class Created By : Nirmal, 08-02-2016
	/// Summary description for Organisation.
    /// </summary>
	public class LeaveBl 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
					
		#region Select All Leave Details
        /// <summary>
        /// To Select All data from the tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
		/// Modified By :
        /// </summary>
		public ApplicationResult  Leave_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Leave_M_SelectAll";
                DataTable dtLeave  = new DataTable();
                dtLeave = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeave);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All DutyLeave Details
        /// <summary>
        /// To Select All data from the tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeave_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_DutyLeave_M_SelectAll";
                DataTable dtLeave = new DataTable();
                dtLeave = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtLeave);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All Leave For Apply
        /// <summary>
        /// To Select All data from the tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_SelectAll_ForApply(int intEmpID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmpID;

                sSql = "usp_tbl_Leave_M_SelectAll_ForApply";
             
                DataTable dtLeave = new DataTable();
                dtLeave = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtLeave);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All DutyLeave For Apply
        /// <summary>
        /// To Select All data from the tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_DutyLeave_ForApply(int intEmpID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmpID;

                sSql = "usp_tbl_Duty_Leave_M_SelectAll_ForApply";
               

                DataTable dtLeave = new DataTable();
                dtLeave = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtLeave);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Leave Details by LeaveID
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select(int intLeaveID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveID;

				strStoredProcName = "usp_tbl_Leave_M_Select";
				
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

        #region Select Leave Name Details by LeaveID
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_SelectName_ByID(int intLeaveID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveID;

                strStoredProcName = "usp_tbl_Leave_M_Select_ByID";

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

        #region Select DutyLeave Details by LeaveID
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeave_Select(int intDutyLeaveID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@DutyLeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDutyLeaveID;

                strStoredProcName = "usp_tbl_DutyLeave_M_Select";

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

        #region Select Leave For Deduction
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select_ForDeduction(string strYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strYear;

                strStoredProcName = "usp_tbl_Leave_M_Select_ForDeduction";

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

        #region Select DutyLeave For Deduction
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_DutySelect_ForDeduction(string strYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);

                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strYear;

                strStoredProcName = "usp_tbl_DutyLeave_M_Select_ForDeduction";

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

        #region Select Leaves for Balance
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select_ForBalance(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_Leave_M_Select_ForBalance";

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

        #region Select Leaves for Balance check
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select_ForBalance_Check(int intEmployeeMID,int IntLeaveId)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;
              

                pSqlParameter[1] = new SqlParameter("@LeaveId", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = IntLeaveId;

                strStoredProcName = "usp_tbl_Leave_M_Select_ForBalance_Check";

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


        #region Select DutyLeaves for Balance
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeave_Select_ForBalance(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_DutyLeave_M_Select_ForBalance";

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

        #region Select Leaves for Payroll Balance
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select_ForPayrollBalance(int intEmployeeMID, string strMonth,int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                strStoredProcName = "usp_tbl_Leave_M_Select_ForPayrollBalance_New";

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

        #region Select DutyLeaves for Payroll Balance
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_DutySelect_ForPayrollBalance(int intEmployeeMID, string strMonth)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                strStoredProcName = "usp_tbl_DutyLeave_M_Select_ForPayrollBalance";

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

        #region Select Leaves for Payroll Leave
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select_ForPayrollLeave(int intEmployeeMID, string strMonth,int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                strStoredProcName = "usp_tbl_Leave_M_Select_ForPayrollLeave_New";

                DataTable dtTable = new DataTable();
                dtTable = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtTable);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select DutyLeaves for Payroll Leave
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeave_Select_ForPayrollLeave(int intEmployeeMID, string strMonth)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                strStoredProcName = "usp_tbl_DutyLeave_M_Select_ForPayrollLeave";

                DataTable dtTable = new DataTable();
                dtTable = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtTable);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Leaves for Absent Days
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Select_ForAbsentDays(int intEmployeeMID, string strMonth)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                strStoredProcName = "usp_tbl_Leave_M_Select_ForAbsentDays";

                DataTable dtTable = new DataTable();
                dtTable = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtTable);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select DutyLeaves for Absent Days
        /// <summary>
        /// Select all details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_DutySelect_ForAbsentDays(int intEmployeeMID, string strMonth)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                strStoredProcName = "usp_tbl_DutyLeave_M_Select_ForAbsentDays";

                DataTable dtTable = new DataTable();
                dtTable = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtTable);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete Leave Details by LeaveID
        /// <summary>
        /// To Delete details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Delete(int intLeaveID, int intLastModifiedBy, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveID;
											
				pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;
				
				pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Leave_M_Delete";
				
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

        #region Delete DutyLeave Details by LeaveID
        /// <summary>
        /// To Delete details of Leave for selected LeaveID from tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult DutyLeave_Delete(int intLeaveID, int intLastModifiedBy, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLeaveID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_DutyLeave_M_Delete";

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

        #region Insert Leave Details
        /// <summary>
        /// To Insert details of Leave in tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
        /// Modified By :
        /// </summary>
        public ApplicationResult Leave_Insert(LeaveBo objLeaveBo)
        {
            try
            {
				pSqlParameter = new SqlParameter[13];
                
				
          		pSqlParameter[0] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objLeaveBo.TrustMID;
 
				pSqlParameter[1] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objLeaveBo.SchoolMID;
 
				pSqlParameter[2] = new SqlParameter("@LeaveCode",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objLeaveBo.LeaveCode;
 
				pSqlParameter[3] = new SqlParameter("@LeaveName",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objLeaveBo.LeaveName;
 
				pSqlParameter[4] = new SqlParameter("@LeaveDescription",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objLeaveBo.LeaveDescription;

                pSqlParameter[5] = new SqlParameter("@IsDeduction", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objLeaveBo.IsDeduction;
 
				pSqlParameter[6] = new SqlParameter("@IsCarryForward",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objLeaveBo.IsCarryForward;
 
				pSqlParameter[7] = new SqlParameter("@Year",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objLeaveBo.Year;
 
				pSqlParameter[8] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objLeaveBo.IsDeleted;
 
				pSqlParameter[9] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objLeaveBo.CreatedUserID;
 
				pSqlParameter[10] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objLeaveBo.CreatedDate;
 
				pSqlParameter[11] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objLeaveBo.LastModifiedUserID;
 
				pSqlParameter[12] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objLeaveBo.LastModifiedDate;
 
		
		
				sSql = "usp_tbl_Leave_M_Insert";
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
                objLeaveBo = null;
            }
        }
        #endregion
		
		#region Update Leave Details
		/// <summary>
        /// To Update details of Leave in tbl_Leave_M table
        /// Created By : Nirmal, 08-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Leave_Update(LeaveBo objLeaveBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@LeaveID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objLeaveBo.LeaveID;
 
				pSqlParameter[1] = new SqlParameter("@LeaveCode",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objLeaveBo.LeaveCode;
 
				pSqlParameter[2] = new SqlParameter("@LeaveName",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objLeaveBo.LeaveName;
 
				pSqlParameter[3] = new SqlParameter("@LeaveDescription",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objLeaveBo.LeaveDescription;
 
				pSqlParameter[4] = new SqlParameter("@IsDeduction",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objLeaveBo.IsDeduction;
 
				pSqlParameter[5] = new SqlParameter("@IsCarryForward",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objLeaveBo.IsCarryForward;
 
				pSqlParameter[6] = new SqlParameter("@Year",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objLeaveBo.Year;
 
				pSqlParameter[7] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objLeaveBo.LastModifiedUserID;
 
				pSqlParameter[8] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objLeaveBo.LastModifiedDate;
 
		
				sSql = "usp_tbl_Leave_M_Update";
                                
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
                objLeaveBo = null;
            }
        }
        #endregion
				
		#region ValidateName for Leave 
        /// <summary>
        /// Function which validates whether the LeaveName already exits in tbl_Leave_M table.
        /// Created By : Nirmal, 08-02-2016
		/// Modified By :
        /// </summary>
        public ApplicationResult Leave_ValidateName(int intTrustMID, int intSchoolID, int intLeaveID, string strName, string strYear)
		{
            try
            {
				pSqlParameter = new SqlParameter[5];
				
				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;
				
				
				pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolID;

                pSqlParameter[2] = new SqlParameter("@LeaveID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLeaveID;

				pSqlParameter[3] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strName;

                pSqlParameter[4] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strYear;
				

				strStoredProcName = "usp_tbl_Leave_M_ValidateName";
				
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
