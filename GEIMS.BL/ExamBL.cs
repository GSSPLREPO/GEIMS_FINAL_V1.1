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
    public class ExamBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All Exam Details
        /// <summary>
        /// To Select All data from the tbl_Examination_M table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Exam_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Examination_M_SelectAll";
                DataTable dtExam = new DataTable();
                dtExam = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtExam);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Exam Details by ExaminationID
        /// <summary>
        /// Select all details of Exam for selected ExaminationID from tbl_Examination_M table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExaminationID"></param>
        /// <returns></returns>
        public ApplicationResult Exam_Select(int intExaminationID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExaminationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExaminationID;

                strStoredProcName = "usp_tbl_Examination_M_Select";

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

        #region Delete Exam Details by ExaminationID
        /// <summary>
        /// To Delete details of Exam for selected ExaminationID from tbl_Examination_M table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExaminationID"></param>
        /// <returns></returns>
        public ApplicationResult Exam_Delete(int intExaminationID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExaminationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExaminationID;

                strStoredProcName = "usp_tbl_Examination_M_Delete";

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

        #region Insert Exam Details
        /// <summary>
        /// To Insert details of Exam in tbl_Examination_M table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="objExamBo"></param>
        /// <returns></returns>
        public ApplicationResult Exam_Insert(ExamBo objExamBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];


                pSqlParameter[0] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamBo.Name;

                pSqlParameter[1] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamBo.Description;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamBo.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamBo.SchoolMID;

                pSqlParameter[4] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamBo.CreatedUserID;

                pSqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamBo.CreatedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamBo.LastModifideDate;

                pSqlParameter[7] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamBo.LastModifideUserID;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamBo.IsDeleted;


                sSql = "usp_tbl_Examination_M_Insert";
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
                objExamBo = null;
            }
        }
        #endregion

        #region Update Exam Details
        /// <summary>
        /// To Update details of Exam in tbl_Examination_M table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="objExamBo"></param>
        /// <returns></returns>
        public ApplicationResult Exam_Update(ExamBo objExamBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@ExaminationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamBo.ExaminationID;

                pSqlParameter[1] = new SqlParameter("@Name", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamBo.Name;

                pSqlParameter[2] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamBo.Description;

                pSqlParameter[3] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamBo.TrustMID;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamBo.SchoolMID;

                pSqlParameter[5] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamBo.CreatedUserID;

                pSqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamBo.CreatedDate;

                pSqlParameter[7] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamBo.LastModifideDate;

                pSqlParameter[8] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamBo.LastModifideUserID;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objExamBo.IsDeleted;


                sSql = "usp_tbl_Examination_M_Update";
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
                objExamBo = null;
            }
        }
        #endregion




        #region Select Exam Details by ExamName
        /// <summary>
        /// Select all details of Exam for selected ExamName from tbl_Examination_M table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="ExamName"></param>
        /// <returns></returns>
        public ApplicationResult Exam_Select_byExamName(string strExamName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strExamName;

                strStoredProcName = "usp_tbl_Examination_M_Select_ByExam";

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


        #region ValidateName for Exam
        /// <summary>
        /// Function which validates whether the ExamName already exits in tbl_Examination_M table.
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="strExamName"></param>
        /// <returns></returns>
        public ApplicationResult Exam_ValidateName(string strExamName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strExamName;

                strStoredProcName = "usp_tbl_Examination_M_Validate_ExamName";

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


