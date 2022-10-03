using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
    /// Class Created By : Darshan, 09/12/2014
	/// Summary description for Organisation.
    /// </summary>
	public class SubjectTBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		

		#region Select All SubjectT Details by Class Division
        /// To Select All data from the tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        public ApplicationResult  SubjectT_SelectAll_By_Class_Division(int intClassMID, int intDivisionTID)
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

                strStoredProcName = "usp_tbl_Subject_T_SelectAll_By_Class_Division";

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
		


		#region Select SubjectT Details by SubjectTID
        /// Select all details of SubjectT for selected SubjectTID from tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        public ApplicationResult SubjectT_Select(SubjectTBO objSubjectTBO)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSubjectTBO.ClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSubjectTBO.DivisionTID;

                pSqlParameter[2] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSubjectTBO.SubjectMID;

				strStoredProcName = "usp_tbl_Subject_T_Select";
				
                DataSet dsResult = new DataSet();
                dsResult = Database.ExecuteDataSet(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Select Subject Details by Class Division
        /// Select all details of SubjectT for selected SubjectTID from tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult Subject_Select_By_Class_Division(int intClassMID, int intDivisionTID, int intMode, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@Mode", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intMode;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_Subject_T_Select_By_Class_Division";

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

        #region Select All Teacher By Subject
        /// To Select All data from the tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult SubjectT_Select_Teacher(int intClassMID, int intDivisionTID, int intSubjectMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionTID;

                pSqlParameter[2] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intSubjectMID;

                strStoredProcName = "usp_tbl_Subject_T_Select_Teacher";

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
		


		#region Delete SubjectT Details by Class Division Subject
        /// To Delete details of SubjectT for selected SubjectTID from tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
		/// Modified By :
        public ApplicationResult SubjectT_Delete(SubjectTBO objSubjectTBO)
		{
            try
            {
                pSqlParameter = new SqlParameter[3];
				
				pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSubjectTBO.ClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSubjectTBO.DivisionTID;

                pSqlParameter[2] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSubjectTBO.SubjectMID;

				strStoredProcName = "usp_tbl_Subject_T_Delete";
				
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

        #region Delete SubjectT Association by Class Division Subject Employee
        /// To Delete details of SubjectT for selected SubjectTID from tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        public ApplicationResult SubjectT_Delete_Association(SubjectTBO objSubjectTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSubjectTBO.ClassMID;

                pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSubjectTBO.DivisionTID;

                pSqlParameter[2] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSubjectTBO.SubjectMID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSubjectTBO.EmployeeMID;

                strStoredProcName = "usp_tbl_Subject_T_Delete_Association";

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



        #region Insert SubjectT Details
        /// <summary>
        /// To Insert details of SubjectT in tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name="objSubjectTBO"></param>
        /// <returns></returns>
        public ApplicationResult SubjectT_Insert(SubjectTBO objSubjectTBO, string strEmployeeMIDs)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];


                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSubjectTBO.SchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSubjectTBO.ClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSubjectTBO.DivisionTID;

                pSqlParameter[3] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSubjectTBO.SubjectMID;

                pSqlParameter[4] = new SqlParameter("@EmployeeMIDs", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strEmployeeMIDs;


                sSql = "usp_tbl_Subject_T_Insert";
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
                objSubjectTBO = null;
            }
        }
        #endregion



        #region Update SubjectT Details
        /// <summary>
        /// To Update details of SubjectT in tbl_Subject_T table
        /// Created By : Darshan, 09/12/2014
        /// Modified By :
        /// </summary>
        /// <param name="objSubjectTBO"></param>
        /// <returns></returns>
        public ApplicationResult SubjectT_Update(SubjectTBO objSubjectTBO, string strEmployeeMIDs)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSubjectTBO.SchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSubjectTBO.ClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSubjectTBO.DivisionTID;

                pSqlParameter[3] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSubjectTBO.SubjectMID;

                pSqlParameter[4] = new SqlParameter("@EmployeeMIDs", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = strEmployeeMIDs;


                sSql = "usp_tbl_Subject_T_Update";
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
                objSubjectTBO = null;
            }
        }

        #endregion
	}
}
