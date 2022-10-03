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
    public class FeesGroupBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion

        #region Select All FeesGroup Details
        /// <summary>
        /// To Select All data from the tbl_FeesGroup_M table
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_SelectAll(int intTrustMID, int intSchoolMID)
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

                sSql = "usp_tbl_FeesGroup_M_SelectAll";
                DataTable dtFeesGroup = new DataTable();
                dtFeesGroup = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtFeesGroup);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select FeesGroup Details by FeesGroupID
        /// <summary>
        /// Select all details of FeesGroup for selected FeesGroupID from tbl_FeesGroup_M table
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name="intFeesGroupID"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_Select(int intFeesGroupID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesGroupID;

                strStoredProcName = "usp_tbl_FeesGroup_M_Select";

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

        #region Delete FeesGroup Details by FeesGroupID
        /// <summary>
        /// To Delete details of FeesGroup for selected FeesGroupID from tbl_FeesGroup_M table
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name="intFeesGroupID"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_Delete(int intFeesGroupID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@FeesGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesGroupID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;


                strStoredProcName = "usp_tbl_FeesGroup_M_Delete";

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

        #region Insert FeesGroup Details
        /// <summary>
        /// To Insert details of FeesGroup in tbl_FeesGroup_M table
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name="objFeesGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_Insert(FeesGroupBo objFeesGroupBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];

                pSqlParameter[0] = new SqlParameter("@FeeGroupName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesGroupBO.FeeGroupName;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesGroupBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesGroupBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesGroupBO.CreatedUserID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesGroupBO.LastModifiedUserID;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesGroupBO.CreatedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesGroupBO.LastModifiedDate;

                pSqlParameter[7] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objFeesGroupBO.Description;

                //pSqlParameter[8] = new SqlParameter("@SectionMID", SqlDbType.Int);
                //pSqlParameter[8].Direction = ParameterDirection.Input;
                //pSqlParameter[8].Value = objFeesGroupBO.SectionMID;

                pSqlParameter[8] = new SqlParameter("@BudgetCategoryMID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objFeesGroupBO.BudgetCategoryMID;

                pSqlParameter[9] = new SqlParameter("@BudgetHeadingMID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objFeesGroupBO.BudgetHeadingMID;

                pSqlParameter[10] = new SqlParameter("@BudgetSubHeadingMID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objFeesGroupBO.BudgetSubHeadingMID;

                pSqlParameter[11] = new SqlParameter("@LedgerID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objFeesGroupBO.LedgerID;

                sSql = "usp_tbl_FeesGroup_M_Insert";
                
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
                objFeesGroupBO = null;
            }
        }
        #endregion

        #region Update FeesGroup Details
        /// <summary>
        /// To Update details of FeesGroup in tbl_FeesGroup_M table
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name="objFeesGroupBO"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_Update(FeesGroupBo objFeesGroupBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];


                pSqlParameter[0] = new SqlParameter("@FeesGroupID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objFeesGroupBO.FeesGroupID;

                pSqlParameter[1] = new SqlParameter("@FeeGroupName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objFeesGroupBO.FeeGroupName;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objFeesGroupBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objFeesGroupBO.SchoolMID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objFeesGroupBO.LastModifiedUserID;

                pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objFeesGroupBO.LastModifiedDate;

                pSqlParameter[6] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objFeesGroupBO.Description;


                sSql = "usp_tbl_FeesGroup_M_Update";
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
                objFeesGroupBO = null;
            }
        }
        #endregion

        #region Select FeesGroup Details by FeesGroupName
        /// <summary>
        /// Select all details of FeesGroup for selected FeesGroupName from tbl_FeesGroup_M table
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name="FeesGroupName"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_Select_byFeesGroupName(string strFeesGroupName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesGroupName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFeesGroupName;

                strStoredProcName = "usp_tbl_FeesGroup_M_Select_ByFeesGroup";

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

        #region ValidateName for FeesGroup
        /// <summary>
        /// Function which validates whether the FeesGroupName already exits in tbl_FeesGroup_M table.
        /// Created By : Nafisa, 19-03-2015
        /// Modified By :
        /// </summary>
        /// <param name="strFeesGroupName"></param>
        /// <returns></returns>
        public ApplicationResult FeesGroup_ValidateName(string strFeesGroupName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@FeesGroupName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFeesGroupName;

                strStoredProcName = "usp_tbl_FeesGroup_M_Validate_FeesGroupName";

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

        #region Select All Section Details DropDown
        /// <summary>
        /// To Select All data from the tbl_genrelLedger_M
        /// Created By : Nirmal, 20-04-2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult GeneralLedger_SelectAll_FeesGroup_dropdown(int intSchoolMID, int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                sSql = "usp_tbl_GeneralLedger_M_DropDown";

                DataTable dtSection = new DataTable();
                dtSection = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtSection);
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


