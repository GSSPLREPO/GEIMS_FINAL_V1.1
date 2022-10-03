using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;

namespace GEIMS.BL
{
    public class ExamConfigurationBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion



        #region Select All ExamConfigurationBL Details
        /// <summary>
        /// To Select All data from the ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfigurationBL_SelectAll(int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                sSql = "usp_tbl_ExamConfiguration_SelectAll";
                DataTable dtExamConfigurationBL = new DataTable();
                dtExamConfigurationBL = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtExamConfigurationBL);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Select ExamConfigurationBL Details by ExamConfigId
        /// <summary>
        /// Select all details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfigurationBL_Select(int intExamConfigId)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ExamConfigId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamConfigId;

                strStoredProcName = "usp_tbl_ExamConfiguration_Select";

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

        #region Select Subject Details by ClassMID,DivisionId,AcademicYear 
        /// <summary>
        /// Select all details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfiguration_Select_Subject(int intClassMID, int intDivisionTID, string strAcademicYear, string strExam)
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

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@Exam", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strExam;


                strStoredProcName = "usp_tbl_ExamConfiguration_Select_Subject";

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










        #region Select Subject Details by ExamConfigID
        /// <summary>
        /// Select all details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfiguration_Select_Subject_ForEdit(int intExamConfigID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ExamConfigID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamConfigID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_ExamConfiguration_Select_Subject_ForEdit";

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


        #region Select Subject Details by ExamConfigID For Source ListBox
        /// <summary>
        /// Select all details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfiguration_Select_Subject_ForEdit_SourceListBox(int intExamConfigID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ExamConfigID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamConfigID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_ExamConfiguration_Select_Subject_ForEdit_SourceListBox";

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



        #region Delete ExamConfigurationBL Details by ExamConfigId
        /// <summary>
        /// To Delete details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfigurationBL_Delete(int intExamConfigId, int intLastModifiedBy, string strLastModifiedDate)
        {
            try
            {
                pSqlParameter = new SqlParameter[3];

                pSqlParameter[0] = new SqlParameter("@ExamConfigId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intExamConfigId;

                pSqlParameter[1] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intLastModifiedBy;

                pSqlParameter[2] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strLastModifiedDate;

                strStoredProcName = "usp_tbl_ExamConfiguration_Delete";

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



        #region Insert ExamConfigurationBL Details
        /// <summary>
        /// To Insert details of ExamConfigurationBL in ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfigurationBL_Insert(ExamConfigurationBO objExamConfigurationBLBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[11];


                pSqlParameter[0] = new SqlParameter("@ClassId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamConfigurationBLBo.ClassId;

                pSqlParameter[1] = new SqlParameter("@DivisionId", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamConfigurationBLBo.DivisionId;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamConfigurationBLBo.AcademicYear;

                pSqlParameter[3] = new SqlParameter("@Exam", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamConfigurationBLBo.Exam;

                pSqlParameter[4] = new SqlParameter("@SubjectId", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamConfigurationBLBo.SubjectId;

                pSqlParameter[5] = new SqlParameter("@CreatedByUserId", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamConfigurationBLBo.CreatedByUserId;

                pSqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamConfigurationBLBo.CreatedDate;

                pSqlParameter[7] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamConfigurationBLBo.LastModifiedBy;

                pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.NVarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamConfigurationBLBo.LastModifiedDate;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objExamConfigurationBLBo.IsDeleted;

                pSqlParameter[10] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objExamConfigurationBLBo.SchoolMID;




                sSql = "usp_tbl_ExamConfiguration_Insert";
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
                objExamConfigurationBLBo = null;
            }
        }
        #endregion



        #region Update ExamConfigurationBL Details
        /// <summary>
        /// To Update details of ExamConfigurationBL in ExamConfiguration table
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfigurationBL_Update(ExamConfigurationBO objExamConfigurationBLBo)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];


                pSqlParameter[0] = new SqlParameter("@ExamConfigId", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objExamConfigurationBLBo.ExamConfigId;

                pSqlParameter[1] = new SqlParameter("@ClassId", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objExamConfigurationBLBo.ClassId;

                pSqlParameter[2] = new SqlParameter("@DivisionId", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objExamConfigurationBLBo.DivisionId;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objExamConfigurationBLBo.AcademicYear;

                pSqlParameter[4] = new SqlParameter("@Exam", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objExamConfigurationBLBo.Exam;

                pSqlParameter[5] = new SqlParameter("@SubjectId", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objExamConfigurationBLBo.SubjectId;
                
                pSqlParameter[6] = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objExamConfigurationBLBo.LastModifiedBy;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objExamConfigurationBLBo.LastModifiedDate;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objExamConfigurationBLBo.IsDeleted;


                sSql = "usp_tbl_ExamConfiguration_Update";

                DataTable ResultDT= Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                if (ResultDT.Rows[0][0].ToString() == "333")
                {
                    ApplicationResult objResults = new ApplicationResult(ResultDT);
                    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    return objResults;
                }
                else if (ResultDT.Rows[0][0].ToString() == "222")
                {
                    ApplicationResult objResults = new ApplicationResult(ResultDT);
                    objResults.status = ApplicationResult.CommonStatusType.RECORD_FK_VIOLATION;
                    return objResults;
                }
                else
                {
                    ApplicationResult objResults = new ApplicationResult(ResultDT);
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
                objExamConfigurationBLBo = null;
            }
        }
        #endregion



        #region ValidateName for ExamConfigurationBL
        /// <summary>
        /// Function which validates whether the ExamConfigurationBLName already exits in ExamConfiguration table.
        /// Created By : Vishal, 1/7/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfigurationBL_ValidateName(int intClassID, int intDivisionID, string strExam, string strAcademicYear, int intExamConfigID , int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassID;

                pSqlParameter[1] = new SqlParameter("@DivisionMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intDivisionID;

                pSqlParameter[2] = new SqlParameter("@Exam", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strExam;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@ExamConfigID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intExamConfigID;

                pSqlParameter[5] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intSchoolMID;

                strStoredProcName = "usp_tbl_ExamConfiguration_ValidateName";

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



        #region Display Report Details by ClassMID,DivisionId,AcademicYear and Exam
        /// <summary>
        /// Select all details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/20/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfiguration_Select_Result_ClassWise(int intClassMID, int intDivisionTID, string strAcademicYear,int intSchoolMID)
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

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;
                
                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "Demo_WithMarks";

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


        #region Display Grade Report Details by ClassMID,DivisionId,AcademicYear and Exam
        /// <summary>
        /// Select all details of ExamConfigurationBL for selected ExamConfigId from ExamConfiguration table
        /// Created By : Vishal, 1/20/2016
        /// Modified By :
        /// </summary>
        public ApplicationResult ExamConfiguration_Select_Result_ClassWise_GradeReport(int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID)
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

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;
                
                pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intSchoolMID;

                strStoredProcName = "Demo_WithGrade";

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
