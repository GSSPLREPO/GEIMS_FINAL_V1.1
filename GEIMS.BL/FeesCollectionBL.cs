using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    /// <summary>
    /// Class Created By : NafisaMulla, 1/4/2015
    /// Summary description for Organisation.
    /// </summary>
    public class FeesCollectionBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All FeesCollection Details
        /// <summary>
        /// To Select All data from the tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_FeesCollection_M_SelectAll";
                DataTable dtFeesCollection = new DataTable();
                dtFeesCollection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtFeesCollection);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select FeesCollection Details by FeesCollectionMID
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionMID from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intFeesCollectionMID"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Select(int intFeesCollectionMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesCollectionMID;

                strStoredProcName = "usp_tbl_FeesCollection_M_Select";

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

        #region Select for Fecollection with optional and compulsory Fees
        public ApplicationResult Fee_Collection_WithOptionalAndCompulsoryFees(int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                //pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                //pSqlParameter[0].Direction = ParameterDirection.Input;
                //pSqlParameter[0].Value = intSchoolMID;

                //pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                //pSqlParameter[1].Direction = ParameterDirection.Input;
                //pSqlParameter[1].Value = intClassMID;

                //pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                //pSqlParameter[2].Direction = ParameterDirection.Input;
                //pSqlParameter[2].Value = intDivisionTID;

                //pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                //pSqlParameter[3].Direction = ParameterDirection.Input;
                //pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                strStoredProcName = "usp_tbl_Fee_Collection_M_WithOptionalAndCompulsoryFees";

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

        #region Delete FeesCollection Details by FeesCollectionMID
        /// <summary>
        /// To Delete details of FeesCollection for selected FeesCollectionMID from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intFeesCollectionMID"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Delete(int intFeesCollectionMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesCollectionMID;

                strStoredProcName = "usp_tbl_FeesCollection_M_Delete";

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

        #region Select for Fecollection with Past Detail
        public ApplicationResult Fee_Collection_PastDetail(int intSchoolMID, int intClassMID, int intDivisionTID, string strAcademicYear, int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intStudentMID;

                strStoredProcName = "usp_tbl_FeesCollection_M_Select_PaidFees";

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

        #region Insert FeesCollection Details
        /// <summary>
        /// To Insert details of FeesCollection in tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objFeesCollectionBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Insert(FeesCollectionBO objFeesCollectionBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[20];

                //pSqlParameter[0] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                //pSqlParameter[0].Direction = ParameterDirection.Input;
                //pSqlParameter[0].Value = objFeesCollectionBO.ReceiptNo;

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesCollectionBO.SchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesCollectionBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesCollectionBO.StudentMID;

                pSqlParameter[3] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesCollectionBO.ClassMID;

                pSqlParameter[4] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCollectionBO.DivisionTID;

                pSqlParameter[5] = new SqlParameter("@FeesToBePaid", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCollectionBO.FeesToBePaid;

                pSqlParameter[6] = new SqlParameter("@AmountPaid", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesCollectionBO.AmountPaid;

                pSqlParameter[7] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objFeesCollectionBO.Date;

                pSqlParameter[8] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objFeesCollectionBO.AcademicYear;

                pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objFeesCollectionBO.LastModifiedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objFeesCollectionBO.LastModifiedDate;

                pSqlParameter[11] = new SqlParameter("@Isdeleted", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objFeesCollectionBO.Isdeleted;

                pSqlParameter[12] = new SqlParameter("@CancellationReason", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objFeesCollectionBO.CancellationReason;

                pSqlParameter[13] = new SqlParameter("@ClassWiseTemplateIDs", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objFeesCollectionBO.ClassWiseTemplateIDs;

                pSqlParameter[14] = new SqlParameter("@ModeOfPayment", SqlDbType.Int);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objFeesCollectionBO.ModeOfPayment;

                pSqlParameter[15] = new SqlParameter("@AccountNumber", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objFeesCollectionBO.AccountNumber;

                pSqlParameter[16] = new SqlParameter("@ChequeNo", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objFeesCollectionBO.ChequeNo;

                pSqlParameter[17] = new SqlParameter("@BankName", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objFeesCollectionBO.BankName;

                pSqlParameter[18] = new SqlParameter("@BranchName", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objFeesCollectionBO.BranchName;

                pSqlParameter[19] = new SqlParameter("@IFSCODE", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objFeesCollectionBO.IFSCODE;


                sSql = "usp_tbl_FeesCollection_M_Insert";
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
                objFeesCollectionBO = null;
            }
        }
        #endregion

        #region Update FeesCollection Details
        /// <summary>
        /// To Update details of FeesCollection in tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objFeesCollectionBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Update(FeesCollectionBO objFeesCollectionBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@FeesCollectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesCollectionBO.FeesCollectionMID;

                pSqlParameter[1] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesCollectionBO.ReceiptNo;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesCollectionBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesCollectionBO.TrustMID;

                pSqlParameter[4] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCollectionBO.StudentMID;

                pSqlParameter[5] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCollectionBO.ClassMID;

                pSqlParameter[6] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesCollectionBO.DivisionTID;

                pSqlParameter[7] = new SqlParameter("@FeesToBePaid", SqlDbType.Float);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objFeesCollectionBO.FeesToBePaid;

                pSqlParameter[8] = new SqlParameter("@AmountPaid", SqlDbType.Float);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objFeesCollectionBO.AmountPaid;

                pSqlParameter[9] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objFeesCollectionBO.Date;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objFeesCollectionBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objFeesCollectionBO.LastModifiedDate;

                pSqlParameter[12] = new SqlParameter("@Isdeleted", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objFeesCollectionBO.Isdeleted;

                pSqlParameter[13] = new SqlParameter("@CancellationReason", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objFeesCollectionBO.CancellationReason;


                sSql = "usp_tbl_FeesCollection_M_Update";
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
                objFeesCollectionBO = null;
            }
        }
        #endregion

        #region Select FeesCollection Details by FeesCollectionName
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionName from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Select_byFeesCollectionName(string strFeesCollectionName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFeesCollectionName;

                strStoredProcName = "usp_tbl_FeesCollection_M_Select_ByFeesCollection";

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

        #region ValidateName for FeesCollection
        /// <summary>
        /// Function which validates whether the FeesCollectionName already exits in tbl_FeesCollection_M table.
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="strFeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_ValidateName(string strFeesCollectionName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFeesCollectionName;

                strStoredProcName = "usp_tbl_FeesCollection_M_Validate_FeesCollectionName";

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

        #region Select All FeesCollectionT Details
        /// <summary>
        /// To Select All data from the tbl_FeesCollection_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_FeesCollection_T_SelectAll";
                DataTable dtFeesCollectionT = new DataTable();
                dtFeesCollectionT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtFeesCollectionT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select FeesCollectionT Details by FeesCollectionTID
        /// <summary>
        /// Select all details of FeesCollectionT for selected FeesCollectionTID from tbl_FeesCollection_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intFeesCollectionTID"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_Select(int intFeesCollectionTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesCollectionTID;

                strStoredProcName = "usp_tbl_FeesCollection_T_Select";

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

        #region Delete FeesCollectionT Details by FeesCollectionTID
        /// <summary>
        /// To Delete details of FeesCollectionT for selected FeesCollectionTID from tbl_FeesCollection_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intFeesCollectionTID"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_Delete(int intFeesCollectionTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesCollectionTID;

                strStoredProcName = "usp_tbl_FeesCollection_T_Delete";

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

        #region Insert FeesCollectionT Details
        /// <summary>
        /// To Insert details of FeesCollectionT in tbl_FeesCollection_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objFeesCollectionTBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_Insert(FeesCollectionTBO objFeesCollectionTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesCollectionTBO.FeesCollectionMID;

                
                //pSqlParameter[1] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                //pSqlParameter[1].Direction = ParameterDirection.Input;
                //pSqlParameter[1].Value = objFeesCollectionTBO.BudgetSubHeadingMID;

                pSqlParameter[1] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesCollectionTBO.ClassWiseFeesTemplateTID;

                //pSqlParameter[2] = new SqlParameter("@StudentFeesTemplateTID", SqlDbType.Int);
                //pSqlParameter[2].Direction = ParameterDirection.Input;
                //pSqlParameter[2].Value = objFeesCollectionTBO.StudentFeesTemplateTID;

                pSqlParameter[2] = new SqlParameter("@FeesAmount", SqlDbType.Float);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesCollectionTBO.FeesAmount;

                pSqlParameter[3] = new SqlParameter("@Discount", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesCollectionTBO.Discount;

                pSqlParameter[4] = new SqlParameter("@RemainingAmount", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCollectionTBO.RemainingAmount;

                pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCollectionTBO.LastModifiedUserID;

                pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesCollectionTBO.LastModifiedDate;

                pSqlParameter[7] = new SqlParameter("@Isdeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objFeesCollectionTBO.Isdeleted;

                sSql = "usp_tbl_FeesCollection_T_Insert";
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
                objFeesCollectionTBO = null;
            }
        }
        #endregion

        #region Update FeesCollectionT Details
        /// <summary>
        /// To Update details of FeesCollectionT in tbl_FeesCollection_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="objFeesCollectionTBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_Update(FeesCollectionTBO objFeesCollectionTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];


                pSqlParameter[0] = new SqlParameter("@FeesCollectionTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesCollectionTBO.FeesCollectionTID;

                pSqlParameter[1] = new SqlParameter("@FeesCollectionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesCollectionTBO.FeesCollectionMID;

                pSqlParameter[2] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesCollectionTBO.ClassWiseFeesTemplateTID;

                //pSqlParameter[3] = new SqlParameter("@StudentFeesTemplateTID", SqlDbType.Int);
                //pSqlParameter[3].Direction = ParameterDirection.Input;
                //pSqlParameter[3].Value = objFeesCollectionTBO.StudentFeesTemplateTID;

                pSqlParameter[3] = new SqlParameter("@FeesAmount", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesCollectionTBO.FeesAmount;

                pSqlParameter[4] = new SqlParameter("@Discount", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesCollectionTBO.Discount;

                pSqlParameter[5] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesCollectionTBO.LastModifiedUserID;

                pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesCollectionTBO.LastModifiedDate;

                pSqlParameter[7] = new SqlParameter("@Isdeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objFeesCollectionTBO.Isdeleted;


                sSql = "usp_tbl_FeesCollection_T_Update";
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
                objFeesCollectionTBO = null;
            }
        }
        #endregion

        #region Select FeesCollectionT Details by FeesCollectionTName
        /// <summary>
        /// Select all details of FeesCollectionT for selected FeesCollectionTName from tbl_FeesCollection_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionTName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_Select_byFeesCollectionTName(string strFeesCollectionTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFeesCollectionTName;

                strStoredProcName = "usp_tbl_FeesCollection_T_Select_ByFeesCollectionT";

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

        #region ValidateName for FeesCollectionT
        /// <summary>
        /// Function which validates whether the FeesCollectionTName already exits in tbl_FeesCollection_T table.
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="strFeesCollectionTName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollectionT_ValidateName(string strFeesCollectionTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesCollectionTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFeesCollectionTName;

                strStoredProcName = "usp_tbl_FeesCollection_T_Validate_FeesCollectionTName";

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

        #region Select FeesCollection of School Yearly
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionName from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Select_byYearlySchoolWise(int intTrustMID, int intSchoolMID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_FeeCollection_M_SchoolWiseReport";

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

        #region Select FeesCollection of Class Wise
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionName from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Select_byClasslWise(int intTrustMID, int intSchoolMID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_Fee_Collection_M_ClassWiseFeeReport";

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

        #region Select FeesCollection Report
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionName from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_Report(int intTrustMID, int intSchoolMID, int intFeeCategoryMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@FeeCollectionMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intFeeCategoryMID;

                strStoredProcName = "usp_Rpt_FeecollectionReciept";

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

        #region Select FeesCollection For RePrint  Report
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionName from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult FeesCollection_RePrint(int intTrustMID, int intSchoolMID, int intReceiptNo, int intStudentMID)
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

                pSqlParameter[2] = new SqlParameter("@ReceiptNo", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intReceiptNo;

                pSqlParameter[3] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intStudentMID;

                strStoredProcName = "usp_Rpt_FeecollectionReciept_Reprint";

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

        #region Outstanding Fees Report
        /// <summary>
        /// Select all details of FeesCollection for selected FeesCollectionName from tbl_FeesCollection_M table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult OutstandingFees_Report(int intSchoolMID, string strDivisionTIDs, string strAcademicYear, int intStatusMasterID, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTIDs", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strDivisionTIDs;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@StatusMasterID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intStatusMasterID;

                pSqlParameter[4] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strFromDate;

                pSqlParameter[5] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strToDate;

                strStoredProcName = "usp_rpt_OutstandingFees";

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

        #region TempFunction

        //public ApplicationResult Test(string strDivisionTIDs)
        //{
        //    try
        //    {
        //        pSqlParameter = new SqlParameter[1];

        //        pSqlParameter[0] = new SqlParameter("@DivisionTIDs", SqlDbType.VarChar);
        //        pSqlParameter[0].Direction = ParameterDirection.Input;
        //        pSqlParameter[0].Value = strDivisionTIDs;

        //        strStoredProcName = "usp_rpt_Testfunction";

        //        DataTable dtResult = new DataTable();
        //        dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
        //        ApplicationResult objResults = new ApplicationResult(dtResult);
        //        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
        //        return objResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region Compact Report
        /// <summary>
        /// Created By : Vishal Rathod, 22/09/2015
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult Yearwise_FeesCollection_for_Accounting(int intSchoolMID, string strDivisionTIDs, string strFromDate, string strToDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTIDs", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strDivisionTIDs;
               
                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strToDate;

                strStoredProcName = "usp_rpt_Yearwise_FeesCollection_for_Accounting";

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

        #region Compact First Report
        /// <summary>
        /// Created By : Vishal Rathod, 22/09/2015
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult Compact_FirstReport(int intSchoolMID, string strDivisionTIDs, string strFromDate, string strToDate ,string strYear ,int intTrustId)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTIDs", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strDivisionTIDs;

                pSqlParameter[2] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strFromDate;

                pSqlParameter[3] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strToDate;

                pSqlParameter[4] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strYear;

                pSqlParameter[5] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intTrustId;

                strStoredProcName = "usp_rpt_Compact_Final";

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

        #region Select for FeesCancellation
        public ApplicationResult Fee_Collection_ForCancellation(int intStudentMID, string strFeeCollectionTIDs, int intLastModifiedUserID, string strLastModifiedDate, string strCancellationReason, string strFeeCollectionMIDs)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@FeesCollectionTIDs", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strFeeCollectionTIDs;

                pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intLastModifiedUserID;

                pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strLastModifiedDate;

                pSqlParameter[4] = new SqlParameter("@CancellationReason", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strCancellationReason;

                pSqlParameter[5] = new SqlParameter("@FeesCollectionMIDs", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strFeeCollectionMIDs;

                strStoredProcName = "usp_tbl_Fee_Collection_M_ForCancellation";

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

        #region Compact Report
        /// <summary>
        /// Created By : Vishal Rathod, 22/09/2015
        /// Modified By :
        /// </summary>
        /// <param name="FeesCollectionName"></param>
        /// <returns></returns>
        public ApplicationResult Compact_Report(string strDivisionTIDs, string strToDate, string strYear, int intSchoolMID, int intTrustId , string strFromDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                
                pSqlParameter[0] = new SqlParameter("@DivisionTIDs", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strDivisionTIDs;
               
                pSqlParameter[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strToDate;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strYear;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                pSqlParameter[4] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intTrustId;

                pSqlParameter[5] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = strFromDate;


                strStoredProcName = "usp_rpt_Compact_Final";

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


