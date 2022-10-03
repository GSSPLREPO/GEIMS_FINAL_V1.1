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
    public class ExamSubjectAssociationBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All ExamSubjectAssociation Details
        /// <summary>
        /// To Select All data from the tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_ExamSubjectAssociation_T_SelectAll";
                DataTable dtExamSubjectAssociation = new DataTable();
                dtExamSubjectAssociation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtExamSubjectAssociation);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select ExamSubjectAssociation Details by ExamSubjectAssociationID
        /// <summary>
        /// Select all details of ExamSubjectAssociation for selected ExamSubjectAssociationID from tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExamSubjectAssociationID"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_Select(int intExamSubjectAssociationID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamSubjectAssociationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamSubjectAssociationID;

                strStoredProcName = "usp_tbl_ExamSubjectAssociation_T_Select";

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

        #region Delete ExamSubjectAssociation Details by ExamSubjectAssociationID
        /// <summary>
        /// To Delete details of ExamSubjectAssociation for selected ExamSubjectAssociationID from tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExamSubjectAssociationID"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_Delete(int intExamSubjectAssociationID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamSubjectAssociationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamSubjectAssociationID;

                strStoredProcName = "usp_tbl_ExamSubjectAssociation_T_Delete";

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

        #region Insert ExamSubjectAssociation Details
        /// <summary>
        /// To Insert details of ExamSubjectAssociation in tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="objExamSubjectAssociationBo"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_Insert(ExamSubjectAssociationBo objExamSubjectAssociationBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@ExamClassDivisionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamSubjectAssociationBo.ExamClassDivisionID;

                pSqlParameter[1] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamSubjectAssociationBo.SubjectMID;

                pSqlParameter[2] = new SqlParameter("@ExamDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamSubjectAssociationBo.ExamDate;

                pSqlParameter[3] = new SqlParameter("@ExamTime", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamSubjectAssociationBo.ExamTime;

                pSqlParameter[4] = new SqlParameter("@TotalMarks", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamSubjectAssociationBo.TotalMarks;

                pSqlParameter[5] = new SqlParameter("@PassingMarks", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamSubjectAssociationBo.PassingMarks;

                pSqlParameter[6] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamSubjectAssociationBo.CreatedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamSubjectAssociationBo.LastModifideUserID;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamSubjectAssociationBo.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objExamSubjectAssociationBo.LastModifideDate;

                pSqlParameter[10] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objExamSubjectAssociationBo.IsDeleted;


                sSql = "usp_tbl_ExamSubjectAssociation_T_Insert";
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
                objExamSubjectAssociationBo = null;
            }
        }
        #endregion

        #region Update ExamSubjectAssociation Details
        /// <summary>
        /// To Update details of ExamSubjectAssociation in tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="objExamSubjectAssociationBo"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_Update(ExamSubjectAssociationBo objExamSubjectAssociationBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];


                pSqlParameter[0] = new SqlParameter("@ExamSubjectAssociationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamSubjectAssociationBo.ExamSubjectAssociationID;

                pSqlParameter[1] = new SqlParameter("@ExamClassDivisionID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamSubjectAssociationBo.ExamClassDivisionID;

                pSqlParameter[2] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamSubjectAssociationBo.SubjectMID;

                pSqlParameter[3] = new SqlParameter("@ExamDate", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamSubjectAssociationBo.ExamDate;

                pSqlParameter[4] = new SqlParameter("@ExamTime", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamSubjectAssociationBo.ExamTime;

                pSqlParameter[5] = new SqlParameter("@TotalMarks", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamSubjectAssociationBo.TotalMarks;

                pSqlParameter[6] = new SqlParameter("@PassingMarks", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamSubjectAssociationBo.PassingMarks;

                pSqlParameter[7] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamSubjectAssociationBo.CreatedUserID;

                pSqlParameter[8] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamSubjectAssociationBo.LastModifideUserID;

                pSqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objExamSubjectAssociationBo.CreatedDate;

                pSqlParameter[10] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objExamSubjectAssociationBo.LastModifideDate;

                pSqlParameter[11] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objExamSubjectAssociationBo.IsDeleted;


                sSql = "usp_tbl_ExamSubjectAssociation_T_Update";
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
                objExamSubjectAssociationBo = null;
            }
        }
        #endregion




        #region Select ExamSubjectAssociation Details by ExamSubjectAssociationName
        /// <summary>
        /// Select all details of ExamSubjectAssociation for selected ExamSubjectAssociationName from tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="ExamSubjectAssociationName"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_Select_byExamSubjectAssociationName(string strExamSubjectAssociationName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamSubjectAssociationName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strExamSubjectAssociationName;

                strStoredProcName = "usp_tbl_ExamSubjectAssociation_T_Select_ByExamSubjectAssociation";

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


        #region ValidateName for ExamSubjectAssociation
        /// <summary>
        /// Function which validates whether the ExamSubjectAssociationName already exits in tbl_ExamSubjectAssociation_T table.
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="strExamSubjectAssociationName"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_ValidateName(string strExamSubjectAssociationName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamSubjectAssociationName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strExamSubjectAssociationName;

                strStoredProcName = "usp_tbl_ExamSubjectAssociation_T_Validate_ExamSubjectAssociationName";

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

        #region Validation Details by ExamSubjectAssociationID
        /// <summary>
        /// Select all details of ExamSubjectAssociation for selected ExamSubjectAssociationID from tbl_ExamSubjectAssociation_T table
        /// Created By : NafisaMulla, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExamSubjectAssociationID"></param>
        /// <returns></returns>
        public ApplicationResult ExamSubjectAssociation_Validation(int intExamSubjectAssociationID, int intSubjectMID, string strExamDate, int intType)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ExamSubjectAssociationID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamSubjectAssociationID;

                pSqlParameter[1] = new SqlParameter("@SubjectMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSubjectMID;

                pSqlParameter[2] = new SqlParameter("@ExamDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strExamDate;

                pSqlParameter[3] = new SqlParameter("@Type", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intType;

                strStoredProcName = "usp_tbl_SubjectAssociation_T_Validation";

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


