using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;
using System.Data.SqlClient;

namespace GEIMS.BL
{
    public class StudentPastEducationBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All StudentPastEducation Details
        /// <summary>
        /// To Select All data from the tbl_StudentPreEducationDetail_T table
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_StudentPreEducationDetail_T_SelectAll";
                DataTable dtStudentPastEducation = new DataTable();
                dtStudentPastEducation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtStudentPastEducation);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select StudentPastEducation Details by StudentEducationDetailTID
        /// <summary>
        /// Select all details of StudentPastEducation for selected StudentEducationDetailTID from tbl_StudentPreEducationDetail_T table
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentEducationDetailTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_Select(int intStudentEducationDetailTID)
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

        #region Delete StudentPastEducation Details by StudentEducationDetailTID
        /// <summary>
        /// To Delete details of StudentPastEducation for selected StudentEducationDetailTID from tbl_StudentPreEducationDetail_T table
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentEducationDetailTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_Delete(int intStudentEducationDetailTID)
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

        #region Insert StudentPastEducation Details
        /// <summary>
        /// To Insert details of StudentPastEducation in tbl_StudentPreEducationDetail_T table
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentPastEducationBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_Insert(StudentPastEducationBO objStudentPastEducationBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentPastEducationBO.StudentMID;

                pSqlParameter[1] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentPastEducationBO.SchoolName;

                pSqlParameter[2] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentPastEducationBO.Address;

                pSqlParameter[3] = new SqlParameter("@MediumName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentPastEducationBO.MediumName;

                pSqlParameter[4] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentPastEducationBO.PassedExam;

                pSqlParameter[5] = new SqlParameter("@BoardName", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentPastEducationBO.BoardName;

                pSqlParameter[6] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentPastEducationBO.PassingYear;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentPastEducationBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentPastEducationBO.LastModifiedUserID;

                pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentPastEducationBO.LastModifiedDate;


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
                objStudentPastEducationBO = null;
            }
        }
        #endregion

        #region Update StudentPastEducation Details
        /// <summary>
        /// To Update details of StudentPastEducation in tbl_StudentPreEducationDetail_T table
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentPastEducationBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_Update(StudentPastEducationBO objStudentPastEducationBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentPastEducationBO.StudentEducationDetailTID;

                pSqlParameter[1] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentPastEducationBO.StudentMID;

                pSqlParameter[2] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentPastEducationBO.SchoolName;

                pSqlParameter[3] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentPastEducationBO.Address;

                pSqlParameter[4] = new SqlParameter("@MediumName", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentPastEducationBO.MediumName;

                pSqlParameter[5] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentPastEducationBO.PassedExam;

                pSqlParameter[6] = new SqlParameter("@BoardName", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentPastEducationBO.BoardName;

                pSqlParameter[7] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentPastEducationBO.PassingYear;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentPastEducationBO.IsDeleted;

                pSqlParameter[9] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentPastEducationBO.LastModifiedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentPastEducationBO.LastModifiedDate;


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
                objStudentPastEducationBO = null;
            }
        }
        #endregion




        #region Select StudentPastEducation Details by StudentPastEducationName
        /// <summary>
        /// Select all details of StudentPastEducation for selected StudentPastEducationName from tbl_StudentPreEducationDetail_T table
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="StudentPastEducationName"></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_Select_byStudentPastEducationName(string strStudentPastEducationName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentPastEducationName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentPastEducationName;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Select_ByStudentPastEducation";

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


        #region ValidateName for StudentPastEducation
        /// <summary>
        /// Function which validates whether the StudentPastEducationName already exits in tbl_StudentPreEducationDetail_T table.
        /// Created By : Darshan, 04-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="strStudentPastEducationName"></param>
        /// <returns></returns>
        public ApplicationResult StudentPastEducation_ValidateName(string strStudentPastEducationName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentPastEducationName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentPastEducationName;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Validate_StudentPastEducationName";

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


