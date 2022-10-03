using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 11/7/2014
	/// Summary description for Organisation.
	/// </summary>
	public class DivisionTBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion



		#region Select All DivisionT Details
		/// <summary>
		/// To Select All data from the tbl_Division_T table
		/// Created By : NafisaMulla, 11/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult DivisionT_SelectAll()
		{
			try
			{
				sSql = "usp_tbl_Division_T_SelectAll";
				DataTable dtDivisionT = new DataTable();
				dtDivisionT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtDivisionT);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

        #region Select All DivisionT Details For Exam
        /// <summary>
        /// To Select All data from the tbl_Division_T table
        /// Created By : NafisaMulla, 11/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult DivisionT_SelectAll_For_Exam(int intClassMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                strStoredProcName = "usp_tbl_Division_T_SelectAll_For_Exam";
                DataTable dtDivisionT = new DataTable();
                dtDivisionT = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtDivisionT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




        #region Select All Division Details Section Wise
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Division_SelectAll_ClassWise(int intClassMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                sSql = "usp_tbl_Division_M_ClassWise";

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

        #region Select All Division Details Section Wise
        /// <summary>
        /// To Select All data from the tbl_Section_M table
        /// Created By : NafisaMulla, 4/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Division_SelectAll_ClassWise_ForDropDown(int intClassMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                sSql = "usp_tbl_Division_M_ClassWise_ForDropDown";

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



		#region Select DivisionNames by Class
		public ApplicationResult DivisionT_Select_DivisionName_By_Class(int intClassMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

				strStoredProcName = "usp_tbl_Division_T_Select_DivisionName_By_Class";

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

        #region Select DivisionT Details by class
        public ApplicationResult DivisionT_Select_By_Class(int intClassMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                strStoredProcName = "usp_tbl_Division_T_Select_By_Class";

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

        #region Select DivisionT Details by DivisionTID
        public ApplicationResult DivisionT_Select_By_DivisionTID(int intDivisionTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intDivisionTID;

                strStoredProcName = "usp_tbl_Division_T_Select_By_DivisionTID";

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



		#region Delete DivisionT Details by DivisionTID
		/// <summary>
		/// To Delete details of DivisionT for selected DivisionTID from tbl_Division_T table
		/// Created By : NafisaMulla, 11/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intDivisionTID"></param>
		/// <returns></returns>
		public ApplicationResult DivisionT_Delete(int intDivisionTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@DivisionTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intDivisionTID;

				strStoredProcName = "usp_tbl_Division_T_Delete";

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

        #region Delete DivisionT Details by Class
        public ApplicationResult DivisionT_Delete_By_Class(int intClassMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                strStoredProcName = "usp_tbl_Division_T_Delete_By_Class";

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



		#region Insert DivisionT Details
		/// <summary>
		/// To Insert details of DivisionT in tbl_Division_T table
		/// Created By : NafisaMulla, 11/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objDivisionTBO"></param>
		/// <returns></returns>
		public ApplicationResult DivisionT_Insert(DivisionTBO objDivisionTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[4];


				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objDivisionTBO.ClassMID;

				pSqlParameter[1] = new SqlParameter("@DivisionName", SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objDivisionTBO.DivisionName;

				pSqlParameter[2] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objDivisionTBO.LastModifiedUserID;

				pSqlParameter[3] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objDivisionTBO.LastModifiedDate;

				
				sSql = "usp_tbl_Division_T_Insert";
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
				objDivisionTBO = null;
			}
		}
		#endregion



		#region Update DivisionT Details
		/// <summary>
		/// To Update details of DivisionT in tbl_Division_T table
		/// Created By : NafisaMulla, 11/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objDivisionTBO"></param>
		/// <returns></returns>
		public ApplicationResult DivisionT_Update(DivisionTBO objDivisionTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[6];


				pSqlParameter[0] = new SqlParameter("@DivisionTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objDivisionTBO.DivisionTID;

				pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objDivisionTBO.ClassMID;

				pSqlParameter[2] = new SqlParameter("@DivisionName", SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objDivisionTBO.DivisionName;

				pSqlParameter[3] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objDivisionTBO.LastModifiedUserID;

				pSqlParameter[4] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objDivisionTBO.LastModifiedDate;

				pSqlParameter[5] = new SqlParameter("@IsDeleted", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objDivisionTBO.IsDeleted;


				sSql = "usp_tbl_Division_T_Update";
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
				objDivisionTBO = null;
			}
		}
		#endregion
	}
}


