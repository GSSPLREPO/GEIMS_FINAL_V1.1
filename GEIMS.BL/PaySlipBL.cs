using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    /// <summary>
    /// Class Created By : NafisaMulla, 17-04-2015
    /// Summary description for Organisation.
    /// </summary>
    public class PaySlipBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All PaySlip Details
        /// <summary>
        /// To Select All data from the tbl_Payslip_M table
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Payslip_M_SelectAll";
                DataTable dtPaySlip = new DataTable();
                dtPaySlip = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPaySlip);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select PaySlip Details by PayslipID
        /// <summary>
        /// Select all details of PaySlip for selected PayslipID from tbl_Payslip_M table
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPayslipID"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_Select(int intPayslipID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayslipID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayslipID;

                strStoredProcName = "usp_tbl_Payslip_M_Select";

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

        #region Delete PaySlip Details by PayslipID
        /// <summary>
        /// To Delete details of PaySlip for selected PayslipID from tbl_Payslip_M table
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="intPayslipID"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_Delete(int intPayslipID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PayslipID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPayslipID;

                strStoredProcName = "usp_tbl_Payslip_M_Delete";

                sSql = "usp_tbl_Employee_M_Insert";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
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

        #region Insert PaySlip Details
        /// <summary>
        /// To Insert details of PaySlip in tbl_Payslip_M table
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_Insert(PaySlipBo objPaySlipBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@UserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipBO.UserID;

                pSqlParameter[2] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipBO.Month;

                pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipBO.Year;

                pSqlParameter[4] = new SqlParameter("@TotalDaysofMonth", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipBO.TotalDaysofMonth;

                pSqlParameter[5] = new SqlParameter("@EarnedDaysofMonth", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipBO.EarnedDaysofMonth;

                pSqlParameter[6] = new SqlParameter("@PaySlipSendforApproval", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipBO.PaySlipSendforApproval;

                pSqlParameter[7] = new SqlParameter("@PayslipApproved", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPaySlipBO.PayslipApproved;

                pSqlParameter[8] = new SqlParameter("@Excemption", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPaySlipBO.Excemption;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPaySlipBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPaySlipBO.CreatedUserID;

                pSqlParameter[11] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objPaySlipBO.CreatedDate;

                pSqlParameter[12] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objPaySlipBO.LastModifiedUserID;

                pSqlParameter[13] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objPaySlipBO.LastModifiedDate;


                sSql = "usp_tbl_Payslip_M_Insert";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objPaySlipBO = null;
            }
        }
        #endregion

        #region Update PaySlip Details
        /// <summary>
        /// To Update details of PaySlip in tbl_Payslip_M table
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_Update(PaySlipBo objPaySlipBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[15];


                pSqlParameter[0] = new SqlParameter("@PayslipID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipBO.PayslipID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@UserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipBO.UserID;

                pSqlParameter[3] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipBO.Month;

                pSqlParameter[4] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipBO.Year;

                pSqlParameter[5] = new SqlParameter("@TotalDaysofMonth", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipBO.TotalDaysofMonth;

                pSqlParameter[6] = new SqlParameter("@EarnedDaysofMonth", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipBO.EarnedDaysofMonth;

                pSqlParameter[7] = new SqlParameter("@PaySlipSendforApproval", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPaySlipBO.PaySlipSendforApproval;

                pSqlParameter[8] = new SqlParameter("@PayslipApproved", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPaySlipBO.PayslipApproved;

                pSqlParameter[9] = new SqlParameter("@Excemption", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPaySlipBO.Excemption;

                pSqlParameter[10] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPaySlipBO.IsDeleted;

                pSqlParameter[11] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objPaySlipBO.CreatedUserID;

                pSqlParameter[12] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objPaySlipBO.CreatedDate;

                pSqlParameter[13] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objPaySlipBO.LastModifiedUserID;

                pSqlParameter[14] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objPaySlipBO.LastModifiedDate;


                sSql = "usp_tbl_Payslip_M_Update";
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
                objPaySlipBO = null;
            }
        }
        #endregion

        #region Update PaySlip Details Selected Part
        /// <summary>
        /// To Update details of PaySlip in tbl_Payslip_M table
        /// Created By : NafisaMMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="objPaySlipBO"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_Update_SelectedPart(PaySlipBo objPaySlipBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];



                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPaySlipBO.UserID;

                pSqlParameter[1] = new SqlParameter("@PaySlipSendforApproval", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPaySlipBO.PaySlipSendforApproval;

                pSqlParameter[2] = new SqlParameter("@PayslipApproved", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPaySlipBO.PayslipApproved;

                pSqlParameter[3] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPaySlipBO.LastModifiedUserID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPaySlipBO.LastModifiedDate;

                pSqlParameter[5] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPaySlipBO.Month;

                pSqlParameter[6] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPaySlipBO.Year;


                sSql = "usp_tbl_PaySlip_M_Update_SelectedPart";
                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

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
                objPaySlipBO = null;
            }
        }
        #endregion

        #region Select PaySlip Details by PaySlipName
        /// <summary>
        /// Select all details of PaySlip for selected PaySlipName from tbl_Payslip_M table
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="PaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_Select_byPaySlipName(string strPaySlipName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPaySlipName;

                strStoredProcName = "usp_tbl_Payslip_M_Select_ByPaySlip";

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

        #region ValidateName for PaySlip
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult PaySlip_ValidateName(string strPaySlipName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PaySlipName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strPaySlipName;

                strStoredProcName = "usp_tbl_Payslip_M_Validate_PaySlipName";

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

        #region PayItem Selection ForProcessPayroll
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult PayItem_Select_ForProcessPayroll(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_PayItemReport_M_Select_TrustWise";

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

        #region TrustReport Selection ForPayRoll
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult TrustReport_Select_ForPayRoll(int intTrustMID, int intMonth, int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMonth;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                strStoredProcName = "usp_rpt_TrustReport_Select_ForPayRoll";

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

        #region PayRoll Process Export TO Excel
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult PayRollProcess_ExportTOExcel(int intTrustMID, int intMonth, int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intMonth;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                strStoredProcName = "usp_ProcessPayroll_ExcelExport";

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

        #region TrustReport Selection ForPayRoll
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult Select_Employee_ForPaySlip(string strTrustMID, string strMonth, string strYear, int intIsApproved, string intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strYear;

                pSqlParameter[3] = new SqlParameter("@IsApproved", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intIsApproved;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                strStoredProcName = "usp_rpt_EmployeePaySlipReport";
                //strStoredProcName = "usp_rpt_EmployeePaySlipReport1"; //Report
                //strStoredProcName = "usp_rpt_EmployeePaySlipReport_Yearly";

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

        #region TrustReport Selection ForPayRoll
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult Select_Employee_ForPaySlip_Report(string strTrustMID, string strMonth, string strYear, int intIsApproved, string intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustMID;

                pSqlParameter[1] = new SqlParameter("@Month", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strMonth;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strYear;

                pSqlParameter[3] = new SqlParameter("@IsApproved", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intIsApproved;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                //strStoredProcName = "usp_rpt_EmployeePaySlipReport";
                strStoredProcName = "usp_rpt_EmployeePaySlipReport1"; //Report
                //strStoredProcName = "usp_rpt_EmployeePaySlipReport_Yearly";

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

        #region TrustReport Selection For Employee Payroll report Yearly
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult Select_EmployeePayroll_Yearly(string strTrustMID,string strSchoolMID, string strEmployeeMID, string strYear, int intIsApproved)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strSchoolMID;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strEmployeeMID;

                pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strYear;

                pSqlParameter[4] = new SqlParameter("@IsApproved", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intIsApproved;

                strStoredProcName = "usp_rpt_EmployeePaySlipReport_Yearly";

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

        #region TrustReport Selection For Employee Payroll For PaySlip
        /// <summary>
        /// Function which validates whether the PaySlipName already exits in tbl_Payslip_M table.
        /// Created By : NafisaMulla, 17-04-2015
        /// Modified By :
        /// </summary>
        /// <param name="strPaySlipName"></param>
        /// <returns></returns>
        public ApplicationResult Select_EmployeeDetail_ForPaySlipPrint(int intTrustMID, int intSchoolMID, int intEmployeeMID,int intMonth, int intYear, int intType)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intEmployeeMID;

                pSqlParameter[3] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intMonth;

                pSqlParameter[4] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;  
                pSqlParameter[4].Value = intYear;

                pSqlParameter[5] = new SqlParameter("@IntType", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intType;

                strStoredProcName = "usp_tbl_EmployeeDetail_ForPaySlip";

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


