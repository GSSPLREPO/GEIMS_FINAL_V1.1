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
    /// Class Created By : NafisaMulla, 05-11-2014
    /// Summary description for Organisation.
    /// </summary>
    public class SchoolEducationDetailTBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All SchoolEducationDetailT Details
        /// <summary>
        /// To Select All data from the tbl_StudentPreEducationDetail_T table
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_StudentPreEducationDetail_T_SelectAll";
                DataTable dtSchoolEducationDetailT = new DataTable();
                dtSchoolEducationDetailT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtSchoolEducationDetailT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select SchoolEducationDetailT Details by StudentEducationDetailTID
        /// <summary>
        /// Select all details of SchoolEducationDetailT for selected StudentEducationDetailTID from tbl_StudentPreEducationDetail_T table
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentEducationDetailTID"></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_Select(int intStudentEducationDetailTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentEducationDetailTID;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Select";

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

        #region Delete SchoolEducationDetailT Details by StudentEducationDetailTID
        /// <summary>
        /// To Delete details of SchoolEducationDetailT for selected StudentEducationDetailTID from tbl_StudentPreEducationDetail_T table
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentEducationDetailTID"></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_Delete(int intStudentEducationDetailTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentEducationDetailTID;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Delete";

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

        #region Insert SchoolEducationDetailT Details
        /// <summary>
        /// To Insert details of SchoolEducationDetailT in tbl_StudentPreEducationDetail_T table
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="objSchoolEducationDetailTBO"></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_Insert(SchoolPreEducationDetailTBO objSchoolEducationDetailTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSchoolEducationDetailTBO.StudentMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSchoolEducationDetailTBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSchoolEducationDetailTBO.SchoolName;

                pSqlParameter[3] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSchoolEducationDetailTBO.Address;

                pSqlParameter[4] = new SqlParameter("@MediumName", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSchoolEducationDetailTBO.MediumName;

                pSqlParameter[5] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSchoolEducationDetailTBO.PassedExam;

                pSqlParameter[6] = new SqlParameter("@BoardName", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSchoolEducationDetailTBO.BoardName;

                pSqlParameter[7] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objSchoolEducationDetailTBO.PassingYear;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objSchoolEducationDetailTBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objSchoolEducationDetailTBO.LastModifiedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objSchoolEducationDetailTBO.LastModifiedDate;


                sSql = "usp_tbl_StudentPreEducationDetail_T_Insert";
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
                objSchoolEducationDetailTBO = null;
            }
        }
        #endregion

        #region Update SchoolEducationDetailT Details
        /// <summary>
        /// To Update details of SchoolEducationDetailT in tbl_StudentPreEducationDetail_T table
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="objSchoolEducationDetailTBO"></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_Update(SchoolPreEducationDetailTBO objSchoolEducationDetailTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSchoolEducationDetailTBO.StudentEducationDetailTID;

                pSqlParameter[1] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSchoolEducationDetailTBO.StudentMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSchoolEducationDetailTBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSchoolEducationDetailTBO.SchoolName;

                pSqlParameter[4] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSchoolEducationDetailTBO.Address;

                pSqlParameter[5] = new SqlParameter("@MediumName", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSchoolEducationDetailTBO.MediumName;

                pSqlParameter[6] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSchoolEducationDetailTBO.PassedExam;

                pSqlParameter[7] = new SqlParameter("@BoardName", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objSchoolEducationDetailTBO.BoardName;

                pSqlParameter[8] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objSchoolEducationDetailTBO.PassingYear;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objSchoolEducationDetailTBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objSchoolEducationDetailTBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objSchoolEducationDetailTBO.LastModifiedDate;


                sSql = "usp_tbl_StudentPreEducationDetail_T_Update";
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
                objSchoolEducationDetailTBO = null;
            }
        }
        #endregion




        #region Select SchoolEducationDetailT Details by SchoolEducationDetailTName
        /// <summary>
        /// Select all details of SchoolEducationDetailT for selected SchoolEducationDetailTName from tbl_StudentPreEducationDetail_T table
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="SchoolEducationDetailTName"></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_Select_bySchoolEducationDetailTName(string strSchoolEducationDetailTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolEducationDetailTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSchoolEducationDetailTName;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Select_BySchoolEducationDetailT";

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


        #region ValidateName for SchoolEducationDetailT
        /// <summary>
        /// Function which validates whether the SchoolEducationDetailTName already exits in tbl_StudentPreEducationDetail_T table.
        /// Created By : NafisaMulla, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="strSchoolEducationDetailTName"></param>
        /// <returns></returns>
        public ApplicationResult SchoolEducationDetailT_ValidateName(string strSchoolEducationDetailTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolEducationDetailTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSchoolEducationDetailTName;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Validate_SchoolEducationDetailTName";

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


