using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace GEIMS.BL
{
    public class EventCategoryBL
    {

        #region User Defined Variables
        public SqlParameter[] pSqlParameter=null;
        public string strStoredProcName;
        #endregion

        #region Insert EventCategory Details
        ///<summary>
        ///To insert Details of EventCategory in tbl_EventCategories
        ///Created By Chirag, 09/28/2021
        ///Modifird BY :
        ///</summary>
        ///<param name="objEventCategoryBO"></param>
        ///<returns></returns>

        public ApplicationResult EventCategory_Insert(EventCategoryBO objEventCategoryBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@EventCategoryName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEventCategoryBO.EventCategoryName;

                pSqlParameter[1] = new SqlParameter("@EventDescription", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEventCategoryBO.EventDescription;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEventCategoryBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEventCategoryBO.CreatedUserID;

                pSqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEventCategoryBO.CreatedDate;

                strStoredProcName = "usp_tbl_EventCategories_Insert";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                if (iResult > 0)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResult;
                }
                else
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validate Name for EventCategory
        public ApplicationResult EventCategory_ValidateName(int intTrustMID, int intEventCategoryID, string strEventCategoryName)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intEventCategoryID;

                pSqlParameter[2] = new SqlParameter("@EventCategoryName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strEventCategoryName;

                strStoredProcName = "usp_tbl_EventCategories_ValidateName";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update EventCategory Details
        /// <summary>
        /// To Update details of EventCategory in tbl_EventCategories table
        /// Created By : Chirag, 09/28/2021
		/// Modified By :
        /// </summary>
        /// <param name="objEventCategoryBO"></param>
        /// <returns></returns>
        
        public ApplicationResult EventCategory_Update(EventCategoryBO objEventCategoryBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEventCategoryBO.EventCategoryID;

                pSqlParameter[1] = new SqlParameter("@EventCategoryName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEventCategoryBO.EventCategoryName;

                pSqlParameter[2] = new SqlParameter("@EventDescription", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEventCategoryBO.EventDescription;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEventCategoryBO.TrustMID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEventCategoryBO.LastModifiedUserID;

                pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEventCategoryBO.LastModifiedDate;

                strStoredProcName = "usp_tbl_EventCategories_Update";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                if (iResult > 0)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResult;
                }
                else
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResult;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Select EventCategory Details By EventCategoryID
        /// <summary>
        /// Select all details of EventCateogyr for selected EventCategoryID from tbl_EventCategories table
        /// Created By : Chirag, 09/29/2021
        /// Modified By :
        /// </summary>
        /// <param name="intEventCategoryID"></param>
        /// <returns></returns>
        
        public ApplicationResult EventCategory_Select(int intEventCategoryID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEventCategoryID;

                strStoredProcName = "usp_tbl_EventCategory_Select";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EventCategory Details By Trust
        public ApplicationResult EventCategory_Select_By_Trust(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_EventCategory_Select_By_Trust";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Delete EventCategory Details by EventCategoryID
        /// <summary>
        /// To Delete details of EventCategory for selected EventCategoryID from tbl_EventCategories table
        /// Created By : Chirag, 09/29/2021
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        public ApplicationResult EventCategory_Delete(int intEventCategoryID, int intLastModifiedUserID, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEventCategoryID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_EventCategory_Delete";

                int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                if (iResult > 0)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResult;
                }
                else
                {
                    ApplicationResult objResult = new ApplicationResult();
                    objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                    return objResult;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check Reocrd Exits or not for delete
        public ApplicationResult CheckRecordForDelete(int intEventCategoryID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EventCategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEventCategoryID;

                strStoredProcName = "usp_tbl_EventCategory_CheckRecord";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
