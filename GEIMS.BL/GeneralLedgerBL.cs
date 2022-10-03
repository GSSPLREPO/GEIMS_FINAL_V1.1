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
    /// Class Created By : Viral, 10/7/2014
	/// Summary description for Organisation.
    /// </summary>
	public class GeneralLedgerBL 
	{
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All GeneralLedger Details
        /// <summary>
        /// To Select All data from the tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_SelectAll(int intTrustID, int intSchoolID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolID;

                sSql = "usp_tbl_GeneralLedger_M_SelectAll";
                DataTable dtGeneralLedger = new DataTable();
                dtGeneralLedger = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtGeneralLedger);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All GeneralLedger Details for Journal Entry
        /// <summary>
        /// To Select All data from the tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_SelectAll_JournalEntry(int intTrustID, int intSchoolID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolID;

                sSql = "usp_tbl_GeneralLedger_M_SelectAll_JournalEntry";
                DataTable dtGeneralLedger = new DataTable();
                dtGeneralLedger = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtGeneralLedger);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select All GeneralLedger Details for Contra Entry

        /// <summary>
        /// To Select All data from the tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <param name="intTrustId"></param>
        /// <param name="intSchoolId"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_SelectAll_ContraEntry(int intTrustId, int intSchoolId)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustId;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolId;

                sSql = "usp_tbl_GeneralLedger_M_SelectAll_ContraEntry";
                DataTable dtGeneralLedger = new DataTable();
                dtGeneralLedger = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtGeneralLedger);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select GeneralLedger Details by LedgerID
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Select(int intLedgerID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLedgerID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_GeneralLedger_M_Select";

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

        #region Select GeneralLedger Cascade by LedgerID
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_M_Cascade(int intLedgerID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@LedgerID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intLedgerID
                };
                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intTrustMID
                };
                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = intSchoolMID
                };             

                strStoredProcName = "usp_tbl_GeneralLedger_M_Cascade";

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

        #region Select GeneralLedger Details by LedgerID for Receipt & Payment
        /// <summary>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Select_Receipt_Payment(int intTypeID, int intTrustID, int intSchoolID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                //pSqlParameter[0] = new SqlParameter("@LedgerID", SqlDbType.Int);
                //pSqlParameter[0].Direction = ParameterDirection.Input;
                //pSqlParameter[0].Value = intLedgerID;

                pSqlParameter[0] = new SqlParameter("@TypeID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTypeID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolID;

                strStoredProcName = "usp_tbl_GeneralLedger_M_Select_Receipt_Payment";

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

        #region Delete GeneralLedger Details by LedgerID
        /// <summary>
        /// To Delete details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intLedgerID"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Delete(int intLedgerID, int intUserID, string strLastModifideDate, string strOperationType)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intLedgerID;

                pSqlParameter[1] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifideDate;

                pSqlParameter[3] = new SqlParameter("@OperationType", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strOperationType;

                strStoredProcName = "usp_tbl_GeneralLedger_M_Delete";

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

        #region Insert GeneralLedger Details
        /// <summary>
        /// To Insert details of GeneralLedger in tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objGeneralLedgerBO"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Insert(GeneralLedgerBO objGeneralLedgerBO, int intYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objGeneralLedgerBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objGeneralLedgerBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@AccountName", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objGeneralLedgerBO.AccountName;

                pSqlParameter[3] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objGeneralLedgerBO.AccountGroupID;

                pSqlParameter[4] = new SqlParameter("@OpeningBalance", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objGeneralLedgerBO.OpeningBalance;

                pSqlParameter[5] = new SqlParameter("@BalanceType", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objGeneralLedgerBO.BalanceType;

                pSqlParameter[6] = new SqlParameter("@Description", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objGeneralLedgerBO.Description;

                pSqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objGeneralLedgerBO.CreatedDate;

                pSqlParameter[8] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objGeneralLedgerBO.CreatedUserID;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objGeneralLedgerBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objGeneralLedgerBO.LastModifideUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objGeneralLedgerBO.LastModifideDate;

                pSqlParameter[12] = new SqlParameter("@Year", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = intYear;

                sSql = "usp_tbl_GeneralLedger_M_Insert";
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
                objGeneralLedgerBO = null;
            }
        }
        #endregion

        #region Update GeneralLedger Details
        /// <summary>
        /// To Update details of GeneralLedger in tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objGeneralLedgerBO"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Update(GeneralLedgerBO objGeneralLedgerBO, string strFromDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];

                pSqlParameter[0] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objGeneralLedgerBO.LedgerID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objGeneralLedgerBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objGeneralLedgerBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@AccountName", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objGeneralLedgerBO.AccountName;

                pSqlParameter[4] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objGeneralLedgerBO.AccountGroupID;

                pSqlParameter[5] = new SqlParameter("@OpeningBalance", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objGeneralLedgerBO.OpeningBalance;

                pSqlParameter[6] = new SqlParameter("@BalanceType", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objGeneralLedgerBO.BalanceType;

                pSqlParameter[7] = new SqlParameter("@Description", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objGeneralLedgerBO.Description;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objGeneralLedgerBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objGeneralLedgerBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objGeneralLedgerBO.IsDeleted;

                pSqlParameter[11] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objGeneralLedgerBO.LastModifideUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifideDate", SqlDbType.NVarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objGeneralLedgerBO.LastModifideDate;

                pSqlParameter[13] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = strFromDate;

                sSql = "usp_tbl_GeneralLedger_M_Update";
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
                objGeneralLedgerBO = null;
            }
        }
        #endregion

        #region Select GeneralLedger Details by GeneralLedgerName
        /// <summary>
        /// Select all details of GeneralLedger for selected GeneralLedgerName from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="GeneralLedgerName"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Select_byGeneralLedgerName(string strGeneralLedgerName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@GeneralLedgerName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strGeneralLedgerName;

                strStoredProcName = "usp_tbl_GeneralLedger_M_Select_ByGeneralLedger";

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

        #region ValidateName for GeneralLedger
        /// <summary>
        /// Function which validates whether the GeneralLedgerName already exits in tbl_GeneralLedger_M table.
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="strGeneralLedgerName"></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_ValidateName(int intTrustID, int intSchoolID, string strAccountName, int intLedgerID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolID;

                pSqlParameter[2] = new SqlParameter("@AccountName", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAccountName;

                pSqlParameter[3] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intLedgerID;

                strStoredProcName = "usp_tbl_GeneralLedger_M_ValidateName";

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

        #region cash/bank book report
        /// <summary/>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// <returns></returns>
        public ApplicationResult GeneralLedger_Select_Rpt_CashBankBook(int intTrustMId, int intSchookMId, int intLedgerId, string strTransactionType, string strFromDate, string strToDate, int intIsNarration)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMId;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchookMId;

                pSqlParameter[2] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLedgerId;

                pSqlParameter[3] = new SqlParameter("@TransactionType", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strTransactionType;

                pSqlParameter[4] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strFromDate;

                pSqlParameter[5] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strToDate;

                pSqlParameter[6] = new SqlParameter("@IsNarration", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = intIsNarration;

                strStoredProcName = "usp_Rpt_CashbokBankbook";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                //DataSet dsResult = new DataSet();
                //dsResult = Database.ExecuteDataSet(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region General Ledger report
        /// <summary/>
        /// Created By : Nafisa, 10/3/2015
        /// Modified By :
        /// <returns></returns>
        public ApplicationResult GeneralLedgerReport(int intTrustMId, int intSchoolMId, int intLedgerId, string strFromDate, string strToDate, int intIsNarration)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMId;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMId;

                pSqlParameter[2] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLedgerId;

                pSqlParameter[3] = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strFromDate;

                pSqlParameter[4] = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strToDate;

                pSqlParameter[5] = new SqlParameter("@IsNarration", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intIsNarration;

                strStoredProcName = "usp_Rpt_GeneralLedger";

                DataTable dsResult = new DataTable();
                dsResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dsResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region cash/bank book report
        /// <summary/>
        /// Select all details of GeneralLedger for selected LedgerID from tbl_GeneralLedger_M table
        /// Created By : Viral, 10/7/2014
        /// Modified By :
        /// <returns></returns>
        public ApplicationResult Select_OpeningBalanceForAccounting(int intTrustMId, int intSchoolMId, int intLedgerId, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMId;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMId;

                pSqlParameter[2] = new SqlParameter("@GenderLederID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLedgerId;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strToDate;


                strStoredProcName = "usp_FindOpeningBalanceForAccounting";

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

        #region Select Trial Balance Report Detail
        /// <summary>
        ///
        /// Created By : Nafisa, 10/3/2015
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult Select_TrialBalanceReport(int intTrustMID, int intSchoolMID, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

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

                strStoredProcName = "usp_rpt_TrialBalance";

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

        #region Select General Ledger for Ledger Reports
        /// <summary>
        ///
        /// Created By : Nafisa, 10/3/2015
        /// Modified By :
        /// </summary>
        /// <param name="intJournalID"></param>
        /// <returns></returns>
        public ApplicationResult Select_GeneralLedgers(int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_GeneralLedger_M_SelectListForReport";

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

        #region FeesGroup_M_Fetch_GeneralLedger
        /// <summary>
        /// To Check Ledger details of FeesGroup in tbl_GeneralLedger_M table
        /// Created By : Arpit Shah, 17-03-2021
        /// Modified By :
        /// </summary>
        /// <param name="objFeesGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_M_Fetch_GeneralLedger(string strAccountName, int intTrustMID, int intSchoolMID, int intAccountGroupID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@AccountName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strAccountName;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@AccountGroupID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intAccountGroupID;

                //pSqlParameter[4] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                //pSqlParameter[4].Direction = ParameterDirection.Input;
                //pSqlParameter[4].Value = objGeneralLedgerBO.@IsDeleted;

                sSql = "usp_tbl_FeesGroup_M_Fetch_GeneralLedger";

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
                //objGeneralLedgerBO = null;
            }
        }
        #endregion
    }
}
