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
    public class MeetingCategoryBL
    {
        #region User Defined Variables
        public SqlParameter[] pSqlParameter = null;
        public string strStoredProcName;
        #endregion

        #region Insert MeetingCategory Details
        ///<summary>
        ///To insert Details of MeetingCategory in tbl_MeetingCategories Table
        ///Created By Chirag, 20/10/2021
        ///Modifird BY :
        ///</summary>
        ///<param name="objMeetingCategoryBO"></param>
        ///<returns></returns>

        public ApplicationResult MeetingCategory_Insert(MeetingCategoryBO objMeetingCategoryBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingCategoryBO.CategoryName;

                pSqlParameter[1] = new SqlParameter("@CategoryDescription", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingCategoryBO.CategoryDescription;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingCategoryBO.TrustMID;

                pSqlParameter[3] = new SqlParameter("@CreatedByID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingCategoryBO.CreatedByID;

                pSqlParameter[4] = new SqlParameter("@CreatedByDate", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMeetingCategoryBO.CreatedByDate;

                strStoredProcName = "usp_tbl_MeetingCategory_Insert";

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

        #region Validate Name for MeetingCategory
        ///<summary>
        ///To Validate Meeting Category Name where insert new category into tbl_EventCategories Table
        ///Created By Chirag, 20/10/2021
        ///Modifird BY :
        ///</summary>
        public ApplicationResult MeetingCategory_ValidateName(int intTrustMID, int intCategoryID, string strCategoryName)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@CategoryID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intCategoryID;

                pSqlParameter[2] = new SqlParameter("@CategoryName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strCategoryName;

                strStoredProcName = "usp_tbl_MeetingCategory_ValidateName";

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

        #region Update MeetingCategory Details
        /// <summary>
        /// To Update details of MeetingCategory into tbl_MeetingCategories Table
        /// Created By : Chirag, 20/10/2021
        /// Modified By :
        /// </summary>
        /// <param name="objMeetingCategoryBO"></param>
        /// <returns></returns>

        public ApplicationResult MeetingCategory_Update(MeetingCategoryBO objMeetingCategoryBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@CategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objMeetingCategoryBO.CategoryID;

                pSqlParameter[1] = new SqlParameter("@CategoryName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objMeetingCategoryBO.CategoryName;

                pSqlParameter[2] = new SqlParameter("@CategoryDescription", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objMeetingCategoryBO.CategoryDescription;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objMeetingCategoryBO.TrustMID;

                pSqlParameter[4] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objMeetingCategoryBO.LastModifiedByID;

                pSqlParameter[5] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objMeetingCategoryBO.LastModifiedByDate;

                strStoredProcName = "usp_tbl_MeetingCategory_Update";

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

        #region Delete MeetingCategory Details by CategoryID
        /// <summary>
        /// To Delete details of MeetingCategory for selected CategoryID from tbl_MeetingCategories table
        /// Created By : Chirag, 20/10/2021
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        public ApplicationResult MeetingCategory_Select_For_Delete(int intCategoryID, int intLastModifiedByID, string strLastModifiedByDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@CategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intCategoryID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedByID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedByID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedByDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedByDate;

                strStoredProcName = "usp_tbl_MeetingCategory_Delete";

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

        #region Select MeetingCategory Details By Trust For GirdBind
        public ApplicationResult MeetingCategory_Select_By_Trust(int intTrustMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                strStoredProcName = "usp_tbl_MeetingCategory_Select_By_Trust";

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

        #region Select MeetingCategory Data By CategoryID When Click on Gridview Edit Button
        /// <summary>
        /// Select all details of MeetingCateogry for selected CategoryID from tbl_EventCategories table
        /// Created By : Chirag, 20/10/2021
        /// Modified By :
        /// </summary>
        /// <param name="intCategoryID"></param>
        /// <returns></returns>

        public ApplicationResult MeetingCategory_Select_For_Edit(int intCategoryID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@CategoryID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intCategoryID;

                strStoredProcName = "usp_tbl_MeetingCategory_Select_For_Edit";

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
