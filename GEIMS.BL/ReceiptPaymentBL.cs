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
    /// Class Created By : Viral, 10/22/2014
	/// Summary description for Organisation.
    /// </summary>
	public class ReceiptPaymentBL 
	{
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All ReceiptPayment Details
        /// <summary>
        /// To Select All data from the tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_SelectAll(int intTrustID, int intSchoolID, int intYear, string strFromDate, string strToDate, string strTransactionType)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolID;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intYear;

                pSqlParameter[3] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strFromDate;

                pSqlParameter[4] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strToDate;

                pSqlParameter[5] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strTransactionType;

                sSql = "usp_tbl_ReceiptPaymentDetail_M_SelectAll";
                DataTable dtReceiptPayment = new DataTable();
                dtReceiptPayment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtReceiptPayment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select ReceiptPayment Details by ReceiptPaymentID
        /// <summary>
        /// Select all details of ReceiptPayment for selected ReceiptPaymentID from tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="intReceiptPaymentID"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_Select(string strReceiptCode, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ReceiptCode", SqlDbType.NVarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strReceiptCode;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_ReceiptPaymentDetail_M_Select";

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

        #region Delete ReceiptPayment Details by ReceiptPaymentID
        /// <summary>
        /// To Delete details of ReceiptPayment for selected ReceiptPaymentID from tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="intReceiptPaymentID"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_Delete(string strReceiptCode, string strOperationType)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ReceiptCode", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strReceiptCode;

                pSqlParameter[1] = new SqlParameter("@OperationType", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strOperationType;

                strStoredProcName = "usp_tbl_ReceiptPaymentDetail_M_Delete";

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

        #region Delete ReceiptPayment Details by ReceiptPaymentID
        /// <summary>
        /// To Delete details of ReceiptPayment for selected ReceiptPaymentID from tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="intReceiptPaymentID"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_Delete_Transaction(string strReceiptCode, string strOprationType)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ReceiptCode", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strReceiptCode;

                pSqlParameter[1] = new SqlParameter("@OperationType", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strOprationType;

                strStoredProcName = "usp_tbl_ReceiptPaymentDetail_M_Delete";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;

                //int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
                //}
                //else
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                //    return objResults;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insert ReceiptPayment Details
        /// <summary>
        /// To Insert details of ReceiptPayment in tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="objReceiptPaymentBO"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_Insert(ReceiptPaymentBO objReceiptPaymentBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[24];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objReceiptPaymentBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objReceiptPaymentBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objReceiptPaymentBO.SectionMID;

                pSqlParameter[3] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objReceiptPaymentBO.BudgetCategoryMID;

                pSqlParameter[4] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objReceiptPaymentBO.BudgetHeadingMID;

                pSqlParameter[5] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objReceiptPaymentBO.BudgetSubHeadingMID;

                pSqlParameter[6] = new SqlParameter("@ReceiptPaymentNo", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objReceiptPaymentBO.ReceiptPaymentNo;

                pSqlParameter[7] = new SqlParameter("@ReceiptPaymentCode", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objReceiptPaymentBO.ReceiptPaymentCode;

                pSqlParameter[8] = new SqlParameter("@ReceiptPaymentDate", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objReceiptPaymentBO.ReceiptPaymentDate;

                pSqlParameter[9] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objReceiptPaymentBO.Year;

                pSqlParameter[10] = new SqlParameter("@TransactionMode", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objReceiptPaymentBO.TransactionMode;

                pSqlParameter[11] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objReceiptPaymentBO.TransactionType;

                pSqlParameter[12] = new SqlParameter("@GeneralLedger", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objReceiptPaymentBO.GeneralLedger;

                pSqlParameter[13] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objReceiptPaymentBO.LedgerID;

                pSqlParameter[14] = new SqlParameter("@Amount", SqlDbType.Float);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objReceiptPaymentBO.Amount;

                pSqlParameter[15] = new SqlParameter("@BankName", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objReceiptPaymentBO.BankName;

                pSqlParameter[16] = new SqlParameter("@BranchName", SqlDbType.NVarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objReceiptPaymentBO.BranchName;

                pSqlParameter[17] = new SqlParameter("@ChequeNo", SqlDbType.Int);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objReceiptPaymentBO.ChequeNo;

                pSqlParameter[18] = new SqlParameter("@Narration", SqlDbType.NChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objReceiptPaymentBO.Narration;

                pSqlParameter[19] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objReceiptPaymentBO.CreatedDate;

                pSqlParameter[20] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objReceiptPaymentBO.CreatedBy;

                pSqlParameter[21] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objReceiptPaymentBO.IsDeleted;

                pSqlParameter[22] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[22].Direction = ParameterDirection.Input;
                pSqlParameter[22].Value = objReceiptPaymentBO.LastModifideUserID;

                pSqlParameter[23] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[23].Direction = ParameterDirection.Input;
                pSqlParameter[23].Value = objReceiptPaymentBO.LastModifideDate;


                sSql = "usp_tbl_ReceiptPaymentDetail_M_Insert";
                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
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
                objReceiptPaymentBO = null;
            }
        }
        #endregion

        #region Update ReceiptPayment Details
        /// <summary>
        /// To Update details of ReceiptPayment in tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="objReceiptPaymentBO"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_Update(ReceiptPaymentBO objReceiptPaymentBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[20];


                //pSqlParameter[0] = new SqlParameter("@ReceiptPaymentID",SqlDbType.Int);
                //pSqlParameter[0].Direction = ParameterDirection.Input;
                //pSqlParameter[0].Value = objReceiptPaymentBO.ReceiptPaymentID;

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objReceiptPaymentBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objReceiptPaymentBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@ReceiptPaymentNo", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objReceiptPaymentBO.ReceiptPaymentNo;

                pSqlParameter[3] = new SqlParameter("@ReceiptPaymentCode", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objReceiptPaymentBO.ReceiptPaymentCode;

                pSqlParameter[4] = new SqlParameter("@ReceiptPaymentDate", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objReceiptPaymentBO.ReceiptPaymentDate;

                pSqlParameter[5] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objReceiptPaymentBO.Year;

                pSqlParameter[6] = new SqlParameter("@TransactionMode", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objReceiptPaymentBO.TransactionMode;

                pSqlParameter[7] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objReceiptPaymentBO.TransactionType;

                pSqlParameter[8] = new SqlParameter("@GeneralLedger", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objReceiptPaymentBO.GeneralLedger;

                pSqlParameter[9] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objReceiptPaymentBO.LedgerID;

                pSqlParameter[10] = new SqlParameter("@Amount", SqlDbType.Float);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objReceiptPaymentBO.Amount;

                pSqlParameter[11] = new SqlParameter("@BankName", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objReceiptPaymentBO.BankName;

                pSqlParameter[12] = new SqlParameter("@BranchName", SqlDbType.NVarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objReceiptPaymentBO.BranchName;

                pSqlParameter[13] = new SqlParameter("@ChequeNo", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objReceiptPaymentBO.ChequeNo;

                pSqlParameter[14] = new SqlParameter("@Narration", SqlDbType.NChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objReceiptPaymentBO.Narration;

                pSqlParameter[15] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objReceiptPaymentBO.CreatedDate;

                pSqlParameter[16] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objReceiptPaymentBO.CreatedBy;

                pSqlParameter[17] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objReceiptPaymentBO.IsDeleted;

                pSqlParameter[18] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objReceiptPaymentBO.LastModifideUserID;

                pSqlParameter[19] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objReceiptPaymentBO.LastModifideDate;


                sSql = "usp_tbl_ReceiptPaymentDetail_M_Update";
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
                objReceiptPaymentBO = null;
            }
        }
        #endregion

        #region Select ReceiptPayment Details by ReceiptPaymentName
        /// <summary>
        /// Select all details of ReceiptPayment for selected ReceiptPaymentName from tbl_ReceiptPaymentDetail_M table
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="ReceiptPaymentName"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_Select_byReceiptPaymentName(string strReceiptPaymentName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ReceiptPaymentName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strReceiptPaymentName;

                strStoredProcName = "usp_tbl_ReceiptPaymentDetail_M_Select_ByReceiptPayment";

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

        #region ValidateName for ReceiptPayment
        /// <summary>
        /// Function which validates whether the ReceiptPaymentName already exits in tbl_ReceiptPaymentDetail_M table.
        /// Created By : Viral, 10/22/2014
        /// Modified By :
        /// </summary>
        /// <param name="strReceiptPaymentName"></param>
        /// <returns></returns>
        public ApplicationResult ReceiptPayment_ValidateName(string strReceiptPaymentName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ReceiptPaymentName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strReceiptPaymentName;

                strStoredProcName = "usp_tbl_ReceiptPaymentDetail_M_Validate_ReceiptPaymentName";

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
