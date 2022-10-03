using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
    /// Class Created By : Darshan, 09/02/2014
	/// Summary description for Organisation.
    /// </summary>
	public class SubjectMBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        public static readonly string mstrConnString = System.Configuration.ConfigurationSettings.AppSettings["DBConnString"];

        #endregion
		

		
		#region Select All SubjectM Details
        /// <summary>
        /// To Select All data from the tbl_Subject_M table
        /// Created By : Darshan, 09/02/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  SubjectM_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Subject_M_SelectAll";
                DataTable dtSubjectM  = new DataTable();
                dtSubjectM = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtSubjectM);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




        



        #region Select SubjectM Details by SubjectMID
        /// <summary>
        /// Select all details of SubjectM for selected SubjectMID from tbl_Subject_M table
        /// Created By : Darshan, 09/02/2014
		/// Modified By :
        /// </summary>
        /// <param name="intSubjectMID"></param>
        /// <returns></returns>
		public ApplicationResult SubjectM_Select(int intSubjectMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSubjectMID;

				strStoredProcName = "usp_tbl_Subject_M_Select";
				
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

        #region Select Subject Details By School
        public ApplicationResult SubjectM_Select_By_School(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Subject_M_Select_By_School";

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

        #region Select Subject Details For examConfig
        public ApplicationResult SubjectM_Select_By_ClassDivision(int intClassMID, int intDivisionTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                strStoredProcName = "usp_tbl_Subject_M_Select_By_ClassDivision";

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
		

		#region Delete SubjectM Details by SubjectMID
        /// <summary>
        /// To Delete details of SubjectM for selected SubjectMID from tbl_Subject_M table
        /// Created By : Darshan, 09/02/2014
		/// Modified By :
        /// </summary>
        /// <param name="intSubjectMID"></param>
        /// <returns></returns>
		public ApplicationResult SubjectM_Delete(int intSubjectMID, int intLastModifiedUserID, string strLastModifiedDate)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSubjectMID;

                pSqlParameter[1] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedUserID;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

				strStoredProcName = "usp_tbl_Subject_M_Delete";
				
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
		


		#region Insert SubjectM Details
		/// <summary>
        /// To Insert details of SubjectM in tbl_Subject_M table
        /// Created By : Darshan, 09/02/2014
		/// Modified By :
        /// </summary>
        /// <param name="objSubjectMBO"></param>
        /// <returns></returns>
        public ApplicationResult SubjectM_Insert(SubjectMBO objSubjectMBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[6];
                
				
          		pSqlParameter[0] = new SqlParameter("@NameEng",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objSubjectMBO.NameEng;
 
				pSqlParameter[1] = new SqlParameter("@NameGuj",SqlDbType.NVarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objSubjectMBO.NameGuj;
 
				pSqlParameter[2] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objSubjectMBO.Description;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSubjectMBO.SchoolMID;
 
				pSqlParameter[4] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objSubjectMBO.CreatedUserID;
 
				pSqlParameter[5] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objSubjectMBO.CreatedDate;
 
				
				sSql = "usp_tbl_Subject_M_Insert";
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
                objSubjectMBO = null;
            }
        }
        #endregion
		


		#region Update SubjectM Details
		/// <summary>
        /// To Update details of SubjectM in tbl_Subject_M table
        /// Created By : Darshan, 09/02/2014
		/// Modified By :
        /// </summary>
        /// <param name="objSubjectMBO"></param>
        /// <returns></returns>
        public ApplicationResult SubjectM_Update(SubjectMBO objSubjectMBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];
                
				
          		pSqlParameter[0] = new SqlParameter("@SubjectMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objSubjectMBO.SubjectMID;
 
				pSqlParameter[1] = new SqlParameter("@NameEng",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objSubjectMBO.NameEng;
 
				pSqlParameter[2] = new SqlParameter("@NameGuj",SqlDbType.NVarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objSubjectMBO.NameGuj;
 
				pSqlParameter[3] = new SqlParameter("@Description",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objSubjectMBO.Description;

                pSqlParameter[4] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objSubjectMBO.LastModifiedUserID;
 
				pSqlParameter[5] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objSubjectMBO.LastModifiedDate;

		
				sSql = "usp_tbl_Subject_M_Update";
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
                objSubjectMBO = null;
            }
        }
        #endregion



        #region ValidateName for Subject
        public ApplicationResult SubjectM_ValidateName(int intSchoolMID, int intSubjectMID, string strNameENG)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSubjectMID;

                pSqlParameter[2] = new SqlParameter("@NameENG", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strNameENG;

                strStoredProcName = "usp_tbl_Subject_M_ValidateName";

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
