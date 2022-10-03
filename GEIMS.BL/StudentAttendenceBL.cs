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
    public class StudentAttendenceBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All StudentAttendence Details
        /// <summary>
        /// To Select All data from the tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_StudentAttendence_M_SelectAll";
                DataTable dtStudentAttendence = new DataTable();
                dtStudentAttendence = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtStudentAttendence);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select StudentAttendence Details by StudentAttendenceMID
        /// <summary>
        /// Select all details of StudentAttendence for selected StudentAttendenceMID from tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentAttendenceMID"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_Select(int intStudentAttendenceMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentAttendenceMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentAttendenceMID;

                strStoredProcName = "usp_tbl_StudentAttendence_M_Select";

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

        #region Select StudentAttendence Details by DateWise
        /// <summary>
        /// Select all details of StudentAttendence for selected StudentAttendenceMID from tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentAttendenceMID"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_Select_Datewise(string strDate, int intClassMID, int intDivisionTID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strDate;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_StudentAttendence_M_Select_DateWise";

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

        #region Delete StudentAttendence Details by StudentAttendenceMID
        /// <summary>
        /// To Delete details of StudentAttendence for selected StudentAttendenceMID from tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentAttendenceMID"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_Delete(int intStudentAttendenceMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentAttendenceMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentAttendenceMID;

                strStoredProcName = "usp_tbl_StudentAttendence_M_Delete";

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

        #region Insert StudentAttendence Details
        /// <summary>
        /// To Insert details of StudentAttendence in tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentAttendenceBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_Insert(StudentAttendenceBO objStudentAttendenceBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@StudentAttendenceMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentAttendenceBO.StudentAttendenceMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentAttendenceBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentAttendenceBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentAttendenceBO.EmployeeMID;

                pSqlParameter[4] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentAttendenceBO.ClassMID;

                pSqlParameter[5] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentAttendenceBO.DivisionTID;

                pSqlParameter[6] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentAttendenceBO.AcademicYear;

                pSqlParameter[7] = new SqlParameter("@PresentStudentIDs", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentAttendenceBO.PresentStudentIDs;

                pSqlParameter[8] = new SqlParameter("@AbsentStudentIds", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentAttendenceBO.AbsentStudentIds;

                pSqlParameter[9] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentAttendenceBO.Date;

                pSqlParameter[10] = new SqlParameter("@Time", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentAttendenceBO.Time;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudentAttendenceBO.LastModifiedUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objStudentAttendenceBO.LastModifiedDate;

                pSqlParameter[13] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objStudentAttendenceBO.IsDeleted;


                sSql = "usp_tbl_StudentAttendence_M_Insert";
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
                objStudentAttendenceBO = null;
            }
        }
        #endregion

        #region Update StudentAttendence Details
        /// <summary>
        /// To Update details of StudentAttendence in tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="objStudentAttendenceBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_Update(StudentAttendenceBO objStudentAttendenceBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@StudentAttendenceMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objStudentAttendenceBO.StudentAttendenceMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objStudentAttendenceBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objStudentAttendenceBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objStudentAttendenceBO.EmployeeMID;

                pSqlParameter[4] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objStudentAttendenceBO.ClassMID;

                pSqlParameter[5] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objStudentAttendenceBO.DivisionTID;

                pSqlParameter[6] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objStudentAttendenceBO.AcademicYear;

                pSqlParameter[7] = new SqlParameter("@PresentStudentIDs", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objStudentAttendenceBO.PresentStudentIDs;

                pSqlParameter[8] = new SqlParameter("@AbsentStudentIds", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objStudentAttendenceBO.AbsentStudentIds;

                pSqlParameter[9] = new SqlParameter("@Date", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentAttendenceBO.Date;

                pSqlParameter[10] = new SqlParameter("@Time", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objStudentAttendenceBO.Time;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objStudentAttendenceBO.LastModifiedUserID;

                pSqlParameter[12] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objStudentAttendenceBO.LastModifiedDate;

                pSqlParameter[13] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objStudentAttendenceBO.IsDeleted;


                sSql = "usp_tbl_StudentAttendence_M_Update";
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
                objStudentAttendenceBO = null;
            }
        }
        #endregion

        #region Select StudentAttendence Details by StudentAttendenceName
        /// <summary>
        /// Select all details of StudentAttendence for selected StudentAttendenceName from tbl_StudentAttendence_M table
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="StudentAttendenceName"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_Select_byStudentAttendenceName(string strStudentAttendenceName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentAttendenceName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentAttendenceName;

                strStoredProcName = "usp_tbl_StudentAttendence_M_Select_ByStudentAttendence";

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


        #region ValidateName for StudentAttendence
        /// <summary>
        /// Function which validates whether the StudentAttendenceName already exits in tbl_StudentAttendence_M table.
        /// Created By : Nafisa, 27-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="strStudentAttendenceName"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_ValidateName(string strStudentAttendenceName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@StudentAttendenceName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentAttendenceName;

                strStoredProcName = "usp_tbl_StudentAttendence_M_Validate_StudentAttendenceName";

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

        #region Select StudentAttendence Monthly report
        /// <summary>
        /// Created By : Nafisa Mulla
        /// Modified By :
        /// </summary>
        /// <param name="intStudentAttendenceMID"></param>
        /// <returns></returns>
        public ApplicationResult StudentAttendence_MonthlyReport(int intSchoolMID, int intClassMID, int intDivisionTID, string strAcademicYear, int intMonthID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intMonthID;

                strStoredProcName = "usp_Rpt_StudentAttendanceReportMonthly";

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

        #region Select StudentAttendence Monthly report
        /// <summary>
        /// Created By : Nafisa Mulla
        /// Modified By :
        /// </summary>
        /// <param name="intStudentAttendenceMID"></param>
        /// <returns></returns>
        public ApplicationResult Sarasari_Report(int intTrustMID, int intSchoolMID, string strAcademicYear, int intMonthID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intTrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intSchoolMID;

                pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strAcademicYear;

                pSqlParameter[3] = new SqlParameter("@Month", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intMonthID;

                strStoredProcName = "usp_Rpt_SarasariReport";

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


