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
    public class RoleRightsBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All RoleRights Details
        /// <summary>
        /// To Select All data from the tbl_RoleRights_T table
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_RoleRights_T_SelectAll";
                DataTable dtRoleRights = new DataTable();
                dtRoleRights = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtRoleRights);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select RoleRights Details by PK_RoleRightsTID
        /// <summary>
        /// Select all details of RoleRights for selected PK_RoleRightsTID from tbl_RoleRights_T table
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPK_RoleRightsTID"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_Select(int intRoleID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_RoleRights_T_Select";

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

        #region Select RoleRights Details by RoleRightsName
        /// <summary>
        /// Select all details of RoleRights for selected RoleRightsName from tbl_RoleRights_T table
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name="RoleRightsName"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_Select_byRoleRightsName(string strRoleRightsName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@RoleRightsName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strRoleRightsName;

                strStoredProcName = "usp_tbl_RoleRights_T_Select_ByRoleRights";

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

        #region Select All RoleRightScreenName Details
        /// <summary>
        /// To Select All data from the tbl_RoleRightsSreen_M table
        /// Created By : Amruta, 10/11/2012
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult RoleRightsScreenName_SelectAll(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;
                //sSql = "usp_tbl_RoleRightsSreen_M_SelectAll";
                sSql = "usp_tbl_RoleRightScreenName_M_ForGrid_Bind";

                DataTable dtRoleRightScreenName = new DataTable();
                dtRoleRightScreenName = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtRoleRightScreenName);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select RoleRights_T Details by RoleRightsID For Authorisation
        /// <summary>
        /// Select all details of RoleRights_T for selected RoleRightsID from tbl_RoleRights_T table
        /// Created By : Amruta, 11/11/2012
        /// Modified By :
        /// </summary>
        /// <param name="intFK_RoleID"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_T_For_Authorisation(int intRoleID, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_RoleRights_T_For_Authorisation";

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



        #region Delete RoleRights Details by PK_RoleRightsTID
        /// <summary>
        /// To Delete details of RoleRights for selected PK_RoleRightsTID from tbl_RoleRights_T table
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name="intFK_RoleID"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_Delete(int intRoleID, int intTrustMID, int intSchoolMID, int intScreenID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@ScreenID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intScreenID;

                strStoredProcName = "usp_tbl_RoleRights_T_Delete";

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



        #region Insert RoleRights Details
        /// <summary>
        /// To Insert details of RoleRights in tbl_RoleRights_T table
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name="objRoleRightsBO"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_Insert(RoleRightsBO objRoleRightsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];


                pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objRoleRightsBO.RoleID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objRoleRightsBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objRoleRightsBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@ScreenID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objRoleRightsBO.ScreenID;


                sSql = "usp_tbl_RoleRights_T_Insert";
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
                objRoleRightsBO = null;
            }
        }
        #endregion



        #region Update RoleRights Details
        /// <summary>
        /// To Update details of RoleRights in tbl_RoleRights_T table
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name="objRoleRightsBO"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_Update(RoleRightsBO objRoleRightsBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];


                pSqlParameter[0] = new SqlParameter("@RoleRightsTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objRoleRightsBO.RoleRightsTID;

                pSqlParameter[1] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objRoleRightsBO.RoleID;

                pSqlParameter[2] = new SqlParameter("@ScreenID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objRoleRightsBO.ScreenID;


                sSql = "usp_tbl_RoleRights_T_Update";
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
                objRoleRightsBO = null;
            }
        }
        #endregion



        #region ValidateName for RoleRights
        /// <summary>
        /// Function which validates whether the RoleRightsName already exits in tbl_RoleRights_T table.
        /// Created By : Darshan, 26/Mar/2014
        /// Modified By :
        /// </summary>
        /// <param name="strRoleRightsName"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_ValidateName(string strRoleRightsName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@RoleRightsName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strRoleRightsName;

                strStoredProcName = "usp_tbl_RoleRights_T_Validate_RoleRightsName";

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


        #region Select RoleRights_T Details by RoleRightsID For Authorisation
        /// <summary>
        /// Select all details of RoleRights_T for selected RoleRightsID from tbl_RoleRights_T table
        /// Created By : Amruta, 11/11/2012
        /// Modified By :
        /// </summary>
        /// <param name="intFK_RoleID"></param>
        /// <returns></returns>
        public ApplicationResult RoleRights_T_Select(int intRoleID, int intTrustMID, int intSchoolMID, int intScreenID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intRoleID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSchoolMID;

                pSqlParameter[3] = new SqlParameter("@ScreenID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intScreenID;

                strStoredProcName = "usp_tbl_RoleRights_T_Select";

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
