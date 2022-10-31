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
    public class SyllabusBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All Syllabus Details
    
        public ApplicationResult Syllabus_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Syllabus_M_SelectAll";
                DataTable dtDepartment = new DataTable();
                dtDepartment = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtDepartment);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Department By ID
        public ApplicationResult Syllabus_Select(int intSyllabusMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SyllabusMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSyllabusMID;

                strStoredProcName = "usp_tbl_Syllabus_M_Select";

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

        #region Delete Sylabus Details by ID
     
        public ApplicationResult Syllabus_Delete(int intSyllabusMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SyllabusMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSyllabusMID;

          

                strStoredProcName = "usp_tbl_Syllabus_M_Delete";

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


        #region Insert Syllabus Details

        public ApplicationResult Syllabus_Insert(SyllabusBO objSyllabusBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];


                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSyllabusBO.SchoolMID;

                pSqlParameter[1] = new SqlParameter("@SectionID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSyllabusBO.SectionID;

                pSqlParameter[2] = new SqlParameter("@ClassID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSyllabusBO.ClassID;

                pSqlParameter[3] = new SqlParameter("@DivisionID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSyllabusBO.DivisionID;

                pSqlParameter[4] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSyllabusBO.Year;

                pSqlParameter[5] = new SqlParameter("@ChapterNameAndNoENG", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSyllabusBO.ChapterNameAndNoENG;

                pSqlParameter[6] = new SqlParameter("@ChapterNameAndNoGUJ", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSyllabusBO.ChapterNameAndNoGUJ;

                pSqlParameter[7] = new SqlParameter("@SyllabusDetailsENG", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objSyllabusBO.SyllabusDetailsENG;

                pSqlParameter[8] = new SqlParameter("@SyllabusDetailsGUJ", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objSyllabusBO.SyllabusDetailsGUJ;

                pSqlParameter[9] = new SqlParameter("@SyllabusRemarks", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objSyllabusBO.SyllabusRemarks;

                pSqlParameter[10] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objSyllabusBO.CreatedByID;

                pSqlParameter[11] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objSyllabusBO.CreatedDate;

                pSqlParameter[12] = new SqlParameter("@SubjectID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objSyllabusBO.SubjectID;



                sSql = "usp_tbl_Syllabus_M_Insert";
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
              //  objDepartmentBO = null;
            }
        }
        #endregion

        #region Update Syllabus Details
      
        public ApplicationResult Syllabus_Update(SyllabusBO objSyllabusBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@SyllabusMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objSyllabusBO.SyllabusMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objSyllabusBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@SectionID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objSyllabusBO.SectionID;

                pSqlParameter[3] = new SqlParameter("@ClassID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objSyllabusBO.ClassID;

                pSqlParameter[4] = new SqlParameter("@DivisionID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objSyllabusBO.DivisionID;

                pSqlParameter[5] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objSyllabusBO.Year;

                pSqlParameter[6] = new SqlParameter("@ChapterNameAndNoENG", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objSyllabusBO.ChapterNameAndNoENG;

                pSqlParameter[7] = new SqlParameter("@ChapterNameAndNoGUJ", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objSyllabusBO.ChapterNameAndNoGUJ;

                pSqlParameter[8] = new SqlParameter("@SyllabusDetailsENG", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objSyllabusBO.SyllabusDetailsENG;

                pSqlParameter[9] = new SqlParameter("@SyllabusDetailsGUJ", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objSyllabusBO.SyllabusDetailsGUJ;

                pSqlParameter[10] = new SqlParameter("@SyllabusRemarks", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objSyllabusBO.SyllabusRemarks;        

                pSqlParameter[11] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objSyllabusBO.ModifiedByID;

                pSqlParameter[12] = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objSyllabusBO.ModifiedDate;

                pSqlParameter[13] = new SqlParameter("@SubjectId", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objSyllabusBO.SubjectID;

                sSql = "usp_tbl_Syllabus_M_Update";
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
                objSyllabusBO = null;
            }
        }
        #endregion

        #region Update Department Details
        /// <summary>
        /// To Update details of Department in tbl_Department_M table
        /// Created By : Darshan, 09/13/2014
        /// Modified By :
        /// </summary>
        /// <param name="objDepartmentBO"></param>
        /// <returns></returns>
        public ApplicationResult Department_Update(DepartmentBO objDepartmentBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];


                pSqlParameter[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objDepartmentBO.DepartmentID;

                pSqlParameter[1] = new SqlParameter("@DepartmentNameENG", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objDepartmentBO.DepartmentNameENG;

                pSqlParameter[2] = new SqlParameter("@DepartmentNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objDepartmentBO.DepartmentNameGUJ;

                pSqlParameter[3] = new SqlParameter("@Description", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objDepartmentBO.Description;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objDepartmentBO.SchoolMID;

                pSqlParameter[5] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objDepartmentBO.TrustMID;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objDepartmentBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objDepartmentBO.LastModifiedDate;


                sSql = "usp_tbl_Department_M_Update";
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
                objDepartmentBO = null;
            }
        }
        #endregion


        public ApplicationResult SyyllabusProgress_Count(int intSyllabusMID) //
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@SyllabusMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSyllabusMID;

                strStoredProcName = "usp_tbl_SyllabusPlanning_M_Select";

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
    }
}
