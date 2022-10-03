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
    /// Class Created By : Viral, 10/13/2014
	/// Summary description for Organisation.
    /// </summary>
	public class JournalVoucherMBL 
	{
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All JournalVoucherM Details
        /// <summary>
        /// To Select All data from the tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_SelectAll(int intTrustMID, int intSchoolMID, int intYear, string strEntryType, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                pSqlParameter[3] = new SqlParameter("@EntryType", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strEntryType;

                pSqlParameter[4] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strFromDate;

                pSqlParameter[5] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strToDate;

                sSql = "usp_tbl_JournalVoucher_M_SelectAll";
                DataTable dtJournalVoucherM = new DataTable();
                dtJournalVoucherM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtJournalVoucherM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select JournalVoucherM Details by JournalID
        /// <summary>
        /// Select all details of JournalVoucherM for selected JournalID from tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_Select(string strVoucherCode, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@VoucherCode", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strVoucherCode;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_JournalVoucher_M_Select";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Delete JournalVoucherM Details by JournalID
        /// <summary>
        /// To Delete details of JournalVoucherM for selected JournalID from tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_Delete(string strVoucherCode, string strOperationType)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@VoucherCode", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strVoucherCode;

                pSqlParameter[1] = new SqlParameter("@OperationType", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strOperationType;

                strStoredProcName = "usp_tbl_JournalVoucher_M_Delete";

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

        #region Delete JournalVoucherM Details by JournalID
        /// <summary>
        /// To Delete details of JournalVoucherM for selected JournalID from tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_Delete_Transaction(string strVoucherCode, string strOperationType)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@VoucherCode", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strVoucherCode;

                pSqlParameter[1] = new SqlParameter("@OperationType", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strOperationType;


                strStoredProcName = "usp_tbl_JournalVoucher_M_Delete";

                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Insert JournalVoucherM Details
        /// <summary>
        /// To Insert details of JournalVoucherM in tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="objJournalVoucherMBO"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_Insert(JournalVoucherMBO objJournalVoucherMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[21];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objJournalVoucherMBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objJournalVoucherMBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objJournalVoucherMBO.SectionMID;

                pSqlParameter[3] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objJournalVoucherMBO.BudgetCategoryMID;

                pSqlParameter[4] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objJournalVoucherMBO.BudgetHeadingMID;

                pSqlParameter[5] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objJournalVoucherMBO.BudgetSubHeadingMID;

                pSqlParameter[6] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objJournalVoucherMBO.LedgerID;

                pSqlParameter[7] = new SqlParameter("@VoucherNo", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objJournalVoucherMBO.VoucherNo;

                pSqlParameter[8] = new SqlParameter("@VoucherCode", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objJournalVoucherMBO.VoucherCode;

                pSqlParameter[9] = new SqlParameter("@VoucherDate", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objJournalVoucherMBO.VoucherDate;

                pSqlParameter[10] = new SqlParameter("@Amount", SqlDbType.Float);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objJournalVoucherMBO.Amount;

                pSqlParameter[11] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objJournalVoucherMBO.TransactionType;

                pSqlParameter[12] = new SqlParameter("@OperationType", SqlDbType.NVarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objJournalVoucherMBO.OperationType;

                pSqlParameter[13] = new SqlParameter("@ChequeNumber", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objJournalVoucherMBO.ChequeNumber;

                pSqlParameter[14] = new SqlParameter("@Description", SqlDbType.NVarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objJournalVoucherMBO.Description;

                pSqlParameter[15] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objJournalVoucherMBO.CreatedDate;

                pSqlParameter[16] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objJournalVoucherMBO.CreatedBy;

                pSqlParameter[17] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objJournalVoucherMBO.IsDeleted;

                pSqlParameter[18] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objJournalVoucherMBO.LastModifideUserID;

                pSqlParameter[19] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objJournalVoucherMBO.LastModifideDate;

                pSqlParameter[20] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objJournalVoucherMBO.Year;

                sSql = "usp_tbl_JournalVoucher_M_Insert";
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
                objJournalVoucherMBO = null;
            }
        }
        #endregion

        #region Update JournalVoucherM Details
        /// <summary>
        /// To Update details of JournalVoucherM in tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="objJournalVoucherMBO"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_Update(JournalVoucherMBO objJournalVoucherMBO, string strFromDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[20];

                pSqlParameter[0] = new SqlParameter("@JournalID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objJournalVoucherMBO.JournalID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objJournalVoucherMBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objJournalVoucherMBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objJournalVoucherMBO.SectionMID;

                pSqlParameter[4] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objJournalVoucherMBO.BudgetCategoryMID;

                pSqlParameter[5] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objJournalVoucherMBO.BudgetHeadingMID;

                pSqlParameter[6] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objJournalVoucherMBO.BudgetSubHeadingMID;

                pSqlParameter[7] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objJournalVoucherMBO.LedgerID;

                pSqlParameter[8] = new SqlParameter("@VoucherNo", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objJournalVoucherMBO.VoucherNo;

                pSqlParameter[9] = new SqlParameter("@VoucherCode", SqlDbType.NVarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objJournalVoucherMBO.VoucherCode;

                pSqlParameter[10] = new SqlParameter("@VoucherDate", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objJournalVoucherMBO.VoucherDate;

                pSqlParameter[11] = new SqlParameter("@Amount", SqlDbType.Float);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objJournalVoucherMBO.Amount;

                pSqlParameter[12] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objJournalVoucherMBO.TransactionType;

                pSqlParameter[13] = new SqlParameter("@OperationType", SqlDbType.NVarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objJournalVoucherMBO.OperationType;

                pSqlParameter[14] = new SqlParameter("@ChequeNumber", SqlDbType.Int);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objJournalVoucherMBO.ChequeNumber;

                pSqlParameter[15] = new SqlParameter("@Description", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objJournalVoucherMBO.Description;

                //pSqlParameter[12] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                //pSqlParameter[12].Direction = ParameterDirection.Input;
                //pSqlParameter[12].Value = objJournalVoucherMBO.CreatedDate;

                //pSqlParameter[13] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                //pSqlParameter[13].Direction = ParameterDirection.Input;
                //pSqlParameter[13].Value = objJournalVoucherMBO.CreatedBy;

                //pSqlParameter[14] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                //pSqlParameter[14].Direction = ParameterDirection.Input;
                //pSqlParameter[14].Value = objJournalVoucherMBO.IsDeleted;

                pSqlParameter[16] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objJournalVoucherMBO.LastModifideUserID;

                pSqlParameter[17] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objJournalVoucherMBO.LastModifideDate;

                pSqlParameter[18] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objJournalVoucherMBO.Year;

                pSqlParameter[19] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = strFromDate;

                sSql = "usp_tbl_JournalVoucher_M_Update";

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
                objJournalVoucherMBO = null;
            }
        }
        #endregion

        #region Select JournalVoucherM Details by JournalVoucherMName
        /// <summary>
        /// Select all details of JournalVoucherM for selected JournalVoucherMName from tbl_JournalVoucher_M table
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="JournalVoucherMName"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_Select_byJournalVoucherMName(string strJournalVoucherMName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@JournalVoucherMName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strJournalVoucherMName;

                strStoredProcName = "usp_tbl_JournalVoucher_M_Select_ByJournalVoucherM";

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

        #region ValidateName for JournalVoucherM
        /// <summary>
        /// Function which validates whether the JournalVoucherMName already exits in tbl_JournalVoucher_M table.
        /// Created By : Viral, 10/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="strJournalVoucherMName"></param>
        /// <returns></returns>
        public ApplicationResult JournalVoucherM_ValidateName(string strJournalVoucherMName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@JournalVoucherMName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strJournalVoucherMName;

                strStoredProcName = "usp_tbl_JournalVoucher_M_Validate_JournalVoucherMName";

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

        #region Get Max Date

        public ApplicationResult GetMaxDate(int intTrustId, int intSchoolId, string strEntryType, int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustId;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolId;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                pSqlParameter[3] = new SqlParameter("@EntryType", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strEntryType;

                strStoredProcName = "tbl_JournalVoucher_M_Select_Max_Date";

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

        #region Select JournalVoucher Register Detail
        /// <summary>
        ///
        /// Created By : Nafisa, 10/3/2015
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult JournalRegisterReport(int intTrustMID, int intSchoolMID, string strFromDate, string strToDate, int intIsNarration)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strToDate;

                pSqlParameter[4] = new SqlParameter("@IsNarration", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intIsNarration;

                strStoredProcName = "usp_Rpt_JournalRegister";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Select Day Book Report Detail
        /// <summary>
        ///
        /// Created By : Nafisa, 10/3/2015
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult Select_DayBookReport(int intTrustMID, int intSchoolMID, string strFromDate, string strToDate, int intIsNarration)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strToDate;

                pSqlParameter[4] = new SqlParameter("@IsNarration", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intIsNarration;

                strStoredProcName = "usp_Rpt_DayBookNew";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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
