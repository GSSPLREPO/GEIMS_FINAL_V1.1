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
    public class ExamClassAssociationBl
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All ExamClassAssociation Details
        /// <summary>
        /// To Select All data from the tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_ExaminationClassDivisionAssociation_T_SelectAll";
                DataTable dtExamClassAssociation = new DataTable();
                dtExamClassAssociation = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtExamClassAssociation);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select ExamClassAssociation Details by ExamClassDivisionID
        /// <summary>
        /// Select all details of ExamClassAssociation for selected ExamClassDivisionID from tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExamClassDivisionID"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_Select(int intExamClassDivisionID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamClassDivisionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamClassDivisionID;

                strStoredProcName = "usp_tbl_ExaminationClassDivisionAssociation_T_Select";

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

        #region Delete ExamClassAssociation Details by ExamClassDivisionID
        /// <summary>
        /// To Delete details of ExamClassAssociation for selected ExamClassDivisionID from tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExamClassDivisionID"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_Delete(int intExamClassDivisionID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamClassDivisionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamClassDivisionID;

                strStoredProcName = "usp_tbl_ExaminationClassDivisionAssociation_T_Delete";

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

        #region Insert ExamClassAssociation Details
        /// <summary>
        /// To Insert details of ExamClassAssociation in tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="objExamClassAssociationBo"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_Insert(ExamClassAssociationBo objExamClassAssociationBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@ExaminationID", SqlDbType.NChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamClassAssociationBo.ExaminationID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamClassAssociationBo.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamClassAssociationBo.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamClassAssociationBo.ClassMID;

                pSqlParameter[4] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamClassAssociationBo.DivisionTID;

                pSqlParameter[5] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamClassAssociationBo.AcademicYear;

                pSqlParameter[6] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamClassAssociationBo.FromDate;

                pSqlParameter[7] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamClassAssociationBo.ToDate;

                pSqlParameter[8] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamClassAssociationBo.CreatedUserID;

                pSqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objExamClassAssociationBo.CreatedDate;

                pSqlParameter[10] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objExamClassAssociationBo.LastModifideDate;

                pSqlParameter[11] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objExamClassAssociationBo.LastModifideUserID;

                pSqlParameter[12] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objExamClassAssociationBo.IsDeleted;


                sSql = "usp_tbl_ExaminationClassDivisionAssociation_T_Insert";
                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objExamClassAssociationBo = null;
            }
        }
        #endregion

        #region Update ExamClassAssociation Details
        /// <summary>
        /// To Update details of ExamClassAssociation in tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="objExamClassAssociationBo"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_Update(ExamClassAssociationBo objExamClassAssociationBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@ExamClassDivisionID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamClassAssociationBo.ExamClassDivisionID;

                pSqlParameter[1] = new SqlParameter("@ExaminationID", SqlDbType.NChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamClassAssociationBo.ExaminationID;

                pSqlParameter[2] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamClassAssociationBo.TrustMID;

                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamClassAssociationBo.SchoolMID;

                pSqlParameter[4] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamClassAssociationBo.ClassMID;

                pSqlParameter[5] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamClassAssociationBo.DivisionTID;

                pSqlParameter[6] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamClassAssociationBo.AcademicYear;

                pSqlParameter[7] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamClassAssociationBo.FromDate;

                pSqlParameter[8] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamClassAssociationBo.ToDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objExamClassAssociationBo.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objExamClassAssociationBo.CreatedDate;

                pSqlParameter[11] = new SqlParameter("@LastModifideDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objExamClassAssociationBo.LastModifideDate;

                pSqlParameter[12] = new SqlParameter("@LastModifideUserID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objExamClassAssociationBo.LastModifideUserID;

                pSqlParameter[13] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objExamClassAssociationBo.IsDeleted;


                sSql = "usp_tbl_ExaminationClassDivisionAssociation_T_Update";
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
                objExamClassAssociationBo = null;
            }
        }
        #endregion




        #region Select ExamClassAssociation Details by ExamClassAssociationName
        /// <summary>
        /// Select all details of ExamClassAssociation for selected ExamClassAssociationName from tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="ExamClassAssociationName"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_Select_byExamClassAssociationName(string strExamClassAssociationName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamClassAssociationName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strExamClassAssociationName;

                strStoredProcName = "usp_tbl_ExaminationClassDivisionAssociation_T_Select_ByExamClassAssociation";

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


        #region ValidateName for ExamClassAssociation
        /// <summary>
        /// Function which validates whether the ExamClassAssociationName already exits in tbl_ExaminationClassDivisionAssociation_T table.
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="strExamClassAssociationName"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_ValidateName(string strExamClassAssociationName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamClassAssociationName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strExamClassAssociationName;

                strStoredProcName = "usp_tbl_ExaminationClassDivisionAssociation_T_Validate_ExamClassAssociationName";

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

        #region Select ExamClassAssociation Details by ExamClassDivisionID
        /// <summary>
        /// Select all details of ExamClassAssociation for selected ExamClassDivisionID from tbl_ExaminationClassDivisionAssociation_T table
        /// Created By : NafisaMULLA, 8/6/2015
        /// Modified By :
        /// </summary>
        /// <param name="intExamClassDivisionID"></param>
        /// <returns></returns>
        public ApplicationResult ExamClassAssociation_SelectWithSubject(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_ExamClass_M_SelectAll_WithSubject";

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


