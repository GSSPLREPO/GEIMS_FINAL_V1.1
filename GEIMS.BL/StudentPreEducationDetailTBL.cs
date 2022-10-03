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
    /// Class Created By : Nafisa, 05-11-2014
    /// Summary description for Organisation.
    /// </summary>
    public class StudentPreEducationDetailTBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All StudentPreEducationDetailT Details
        /// <summary>
        /// To Select All data from the tbl_StudentPreEducationDetail_T table
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_SelectAll(int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                sSql = "usp_tbl_StudentPreEducationDetail_T_SelectAll";
                DataTable dtStudentPreEducationDetailT = new DataTable();
                dtStudentPreEducationDetailT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtStudentPreEducationDetailT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select StudentPreEducationDetailT Details by StudentEducationDetailTID
        /// <summary>
        /// Select all details of StudentPreEducationDetailT for selected StudentEducationDetailTID from tbl_StudentPreEducationDetail_T table
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentEducationDetailTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_Select(int intStudentEducationDetailTID)
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

        #region Delete StudentPreEducationDetailT Details by StudentEducationDetailTID
        /// <summary>
        /// To Delete details of StudentPreEducationDetailT for selected StudentEducationDetailTID from tbl_StudentPreEducationDetail_T table
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentEducationDetailTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_Delete(int intStudentEducationDetailTID)
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

        #region Insert StudentPreEducationDetailT Details
        /// <summary>
        /// To Insert details of StudentPreEducationDetailT in tbl_StudentPreEducationDetail_T table
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentPreEducationDetailTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_Insert(StudentPreEducationDetailTBO objStudentPreEducationDetailTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[15];


                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentPreEducationDetailTBO.StudentMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentPreEducationDetailTBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentPreEducationDetailTBO.SchoolName;

                pSqlParameter[3] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentPreEducationDetailTBO.Address;

                pSqlParameter[4] = new SqlParameter("@MediumName", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentPreEducationDetailTBO.MediumName;

                pSqlParameter[5] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentPreEducationDetailTBO.PassedExam;

                pSqlParameter[6] = new SqlParameter("@BoardName", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentPreEducationDetailTBO.BoardName;

                pSqlParameter[7] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentPreEducationDetailTBO.PassingYear;

                pSqlParameter[8] = new SqlParameter("@Town", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentPreEducationDetailTBO.Town;

                pSqlParameter[9] = new SqlParameter("@Taluka", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentPreEducationDetailTBO.Taluka;

                pSqlParameter[10] = new SqlParameter("@District", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentPreEducationDetailTBO.District;

                pSqlParameter[11] = new SqlParameter("@State", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudentPreEducationDetailTBO.State;

                pSqlParameter[12] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objStudentPreEducationDetailTBO.IsDeleted;

                pSqlParameter[13] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objStudentPreEducationDetailTBO.LastModifiedUserID;

                pSqlParameter[14] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objStudentPreEducationDetailTBO.LastModifiedDate;



                sSql = "usp_tbl_StudentPreEducationDetail_T_Insert";
                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

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
                objStudentPreEducationDetailTBO = null;
            }
        }
        #endregion

        #region Update StudentPreEducationDetailT Details
        /// <summary>
        /// To Update details of StudentPreEducationDetailT in tbl_StudentPreEducationDetail_T table
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentPreEducationDetailTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_Update(StudentPreEducationDetailTBO objStudentPreEducationDetailTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[16];


                pSqlParameter[0] = new SqlParameter("@StudentEducationDetailTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentPreEducationDetailTBO.StudentEducationDetailTID;

                pSqlParameter[1] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentPreEducationDetailTBO.StudentMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentPreEducationDetailTBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@SchoolName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentPreEducationDetailTBO.SchoolName;

                pSqlParameter[4] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentPreEducationDetailTBO.Address;

                pSqlParameter[5] = new SqlParameter("@MediumName", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentPreEducationDetailTBO.MediumName;

                pSqlParameter[6] = new SqlParameter("@PassedExam", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentPreEducationDetailTBO.PassedExam;

                pSqlParameter[7] = new SqlParameter("@BoardName", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentPreEducationDetailTBO.BoardName;

                pSqlParameter[8] = new SqlParameter("@PassingYear", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentPreEducationDetailTBO.PassingYear;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentPreEducationDetailTBO.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentPreEducationDetailTBO.LastModifiedUserID;

                pSqlParameter[11] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudentPreEducationDetailTBO.LastModifiedDate;

                pSqlParameter[12] = new SqlParameter("@Town", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objStudentPreEducationDetailTBO.Town;

                pSqlParameter[13] = new SqlParameter("@Taluka", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objStudentPreEducationDetailTBO.Taluka;

                pSqlParameter[14] = new SqlParameter("@District", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objStudentPreEducationDetailTBO.District;

                pSqlParameter[15] = new SqlParameter("@State", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objStudentPreEducationDetailTBO.State;




                sSql = "usp_tbl_StudentPreEducationDetail_T_Update";

                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

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
                objStudentPreEducationDetailTBO = null;
            }
        }
        #endregion




        #region Select StudentPreEducationDetailT Details by StudentPreEducationDetailTName
        /// <summary>
        /// Select all details of StudentPreEducationDetailT for selected StudentPreEducationDetailTName from tbl_StudentPreEducationDetail_T table
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="StudentPreEducationDetailTName"></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_Select_byStudentPreEducationDetailTName(string strStudentPreEducationDetailTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentPreEducationDetailTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentPreEducationDetailTName;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Select_ByStudentPreEducationDetailT";

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


        #region ValidateName for StudentPreEducationDetailT
        /// <summary>
        /// Function which validates whether the StudentPreEducationDetailTName already exits in tbl_StudentPreEducationDetail_T table.
        /// Created By : Nafisa, 05-11-2014
        /// Modified By :
        /// </summary>
        /// <param name="strStudentPreEducationDetailTName"></param>
        /// <returns></returns>
        public ApplicationResult StudentPreEducationDetailT_ValidateName(string strStudentPreEducationDetailTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentPreEducationDetailTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentPreEducationDetailTName;

                strStoredProcName = "usp_tbl_StudentPreEducationDetail_T_Validate_StudentPreEducationDetailTName";

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



