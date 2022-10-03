using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 20/6/2014
	/// Summary description for Organisation.
	/// </summary>
	public class StudentFeesTemplateTBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
				
		#region Select All StudentFeesTemplateT Details
        /// <summary>
        /// To Select All data from the tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  StudentFeesTemplateT_SelectAll(int intIsLate)
        {
			try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intIsLate;

				sSql = "usp_tbl_StudentFeesTemplate_T_SelectAll";
                DataTable dtStudentFeesTemplateT  = new DataTable();
                dtStudentFeesTemplateT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtStudentFeesTemplateT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select StudentFeesTemplateT Details by StudentFeesTemplateTID
        /// <summary>
        /// Select all details of StudentFeesTemplateT for selected StudentFeesTemplateTID from tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
        /// <param name="intStudentFeesTemplateTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentFeesTemplateT_Select(int intStudentMID, string strAcademicYear)
		{
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentMID;

                pSqlParameter[1] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAcademicYear;

				strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Select";

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

        #region Select for StudentFeeTemplate FeesNameWise
        public ApplicationResult StudentFeeTemplate_FeesNameWise(string strSchoolMID, string strClassMID, string strDivisionName, string strAcademicYear, int intIsLate)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strSchoolMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = strDivisionName;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intIsLate;

                strStoredProcName = "usp_tbl_StudentFeeTemplate_T_Select_FeesNameWise";

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

        #region Select for StudentFeeTemplate FeesName  With Amount
        public ApplicationResult StudentFeeTemplate_Select_FeesName_With_Amount(int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID)
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

                strStoredProcName = "usp_tbl_Select_FeesCategory_With_Amount";

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
		
		#region Delete StudentFeesTemplateT Details by StudentFeesTemplateTID
        /// <summary>
        /// To Delete details of StudentFeesTemplateT for selected StudentFeesTemplateTID from tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
        /// <param name="intStudentFeesTemplateTID"></param>
        /// <returns></returns>
		public ApplicationResult StudentFeesTemplateT_Delete(int intStudentFeesTemplateTID, int intIsLate)
		{
            try
            {
                pSqlParameter = new SqlParameter[2];
				
				pSqlParameter[0] = new SqlParameter("@StudentFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intIsLate;

				strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Delete";

                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Delete StudentFeesTemplateT Details by StudentFeesTemplateTID
        /// <summary>
        /// To Delete details of StudentFeesTemplateT for selected StudentFeesTemplateTID from tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentFeesTemplateTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentFeesTemplateT_Delete_By_ClassWiseTemplateTID(int intStudentFeesTemplateTID, int intIsLate)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intIsLate;

                strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Delete_By_ClassTemplateTID";

                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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
		
		#region Insert StudentFeesTemplateT Details
		/// <summary>
        /// To Insert details of StudentFeesTemplateT in tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
        /// <param name="objStudentFeesTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentFeesTemplateT_Insert(StudentFeesTemplateTBO objStudentFeesTemplateTBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[13];
                
				
          		pSqlParameter[0] = new SqlParameter("@FeesCategoryMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objStudentFeesTemplateTBO.FeesCategoryMID;
 
				pSqlParameter[1] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objStudentFeesTemplateTBO.TrustMID;
 
				pSqlParameter[2] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objStudentFeesTemplateTBO.SchoolMID;
 
				pSqlParameter[3] = new SqlParameter("@ClassMID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objStudentFeesTemplateTBO.ClassMID;
 
				pSqlParameter[4] = new SqlParameter("@DivisionTID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objStudentFeesTemplateTBO.DivisionTID;
 
				pSqlParameter[5] = new SqlParameter("@StudentMID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objStudentFeesTemplateTBO.StudentMID;
 
				pSqlParameter[6] = new SqlParameter("@ClassWiseFeesTemplateTID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objStudentFeesTemplateTBO.ClassWiseFeesTemplateTID;
 
				pSqlParameter[7] = new SqlParameter("@FeesAmount",SqlDbType.Float);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objStudentFeesTemplateTBO.FeesAmount;
 
				pSqlParameter[8] = new SqlParameter("@AcademicYear",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objStudentFeesTemplateTBO.AcademicYear;

                pSqlParameter[9] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objStudentFeesTemplateTBO.IsLate;

				pSqlParameter[10] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objStudentFeesTemplateTBO.LastModifiedUserID;
 
				pSqlParameter[11] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objStudentFeesTemplateTBO.LastModifiedDate;
 
				pSqlParameter[12] = new SqlParameter("@Isdeleted",SqlDbType.Int);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objStudentFeesTemplateTBO.Isdeleted;
		
				sSql = "usp_tbl_StudentFeesTemplate_T_Insert";

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
                objStudentFeesTemplateTBO = null;
            }
        }
        #endregion
		
		#region Update StudentFeesTemplateT Details
		/// <summary>
        /// To Update details of StudentFeesTemplateT in tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
        /// <param name="objStudentFeesTemplateTBO"></param>
        /// <returns></returns>
        public ApplicationResult StudentFeesTemplateT_Update(StudentFeesTemplateTBO objStudentFeesTemplateTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[13];
                
				
          		pSqlParameter[0] = new SqlParameter("@StudentFeesTemplateTID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objStudentFeesTemplateTBO.StudentFeesTemplateTID;
 
				pSqlParameter[1] = new SqlParameter("@FeesCategoryMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objStudentFeesTemplateTBO.FeesCategoryMID;
 
				pSqlParameter[2] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objStudentFeesTemplateTBO.TrustMID;
 
				pSqlParameter[3] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objStudentFeesTemplateTBO.SchoolMID;
 
				pSqlParameter[4] = new SqlParameter("@ClassMID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objStudentFeesTemplateTBO.ClassMID;
 
				pSqlParameter[5] = new SqlParameter("@DivisionTID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objStudentFeesTemplateTBO.DivisionTID;
 
				pSqlParameter[6] = new SqlParameter("@StudentMID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objStudentFeesTemplateTBO.StudentMID;
 
				pSqlParameter[7] = new SqlParameter("@ClassWiseFeesTemplateTID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objStudentFeesTemplateTBO.ClassWiseFeesTemplateTID;
 
				pSqlParameter[8] = new SqlParameter("@FeesAmount",SqlDbType.Float);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objStudentFeesTemplateTBO.FeesAmount;
 
				pSqlParameter[9] = new SqlParameter("@AcademicYear",SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objStudentFeesTemplateTBO.AcademicYear;
 
				pSqlParameter[10] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objStudentFeesTemplateTBO.LastModifiedUserID;
 
				pSqlParameter[11] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objStudentFeesTemplateTBO.LastModifiedDate;
 
				pSqlParameter[12] = new SqlParameter("@Isdeleted",SqlDbType.Int);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objStudentFeesTemplateTBO.Isdeleted;

		
				sSql = "usp_tbl_StudentFeesTemplate_T_Update";
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
                objStudentFeesTemplateTBO = null;
            }
        }
        #endregion

        #region Delete Students Detail
        /// <summary>
        /// To Delete details of StudentFeesTemplateT for selected StudentFeesTemplateTID from tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentFeesTemplateTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentFeesTemplateT_Delete_ForInsert(int intStudentFeesTemplateTID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intStudentFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Delete_ForInsert";

                int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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
				
		#region Select StudentFeesTemplateT Details by StudentFeesTemplateTName
        /// <summary>
        /// Select all details of StudentFeesTemplateT for selected StudentFeesTemplateTName from tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
        /// <param name="StudentFeesTemplateTName"></param>
        /// <returns></returns>
		public ApplicationResult StudentFeesTemplateT_Select_byStudentFeesTemplateTName(string strStudentFeesTemplateTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@StudentFeesTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentFeesTemplateTName;

				strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Select_ByStudentFeesTemplateT";
				
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
				
		#region ValidateName for StudentFeesTemplateT 
        /// <summary>
        /// Function which validates whether the StudentFeesTemplateTName already exits in tbl_StudentFeesTemplate_T table.
        /// Created By : NafisaMulla, 02-09-2014
		/// Modified By :
        /// </summary>
        /// <param name="strStudentFeesTemplateTName"></param>
        /// <returns></returns>
		public ApplicationResult StudentFeesTemplateT_ValidateName(string strStudentFeesTemplateTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@StudentFeesTemplateTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strStudentFeesTemplateTName;

				strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Validate_StudentFeesTemplateTName";
				
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

        #region Select StudentFeesTemplateT Details for ClassFeesTemplate
        /// <summary>
        /// Select all details of StudentFeesTemplateT for selected StudentFeesTemplateTID from tbl_StudentFeesTemplate_T table
        /// Created By : NafisaMulla, 02-09-2014
        /// Modified By :
        /// </summary>
        /// <param name="intStudentFeesTemplateTID"></param>
        /// <returns></returns>
        public ApplicationResult StudentFeesTemplateT_SelectAll_For_ClassTemplate(int intSchoolMID, int intClassMID, int intDivisionTID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

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

                strStoredProcName = "usp_tbl_StudentFeesTemplate_T_SelectAll_For_ClassTemplate";

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

        #region Select for StudentFeeTemplate for ClassWiseStudentTemplate 
        public ApplicationResult StudentFeeTemplate_Select_ClassWiseStudentTemplate(int intSchoolMID, int intClassMID, int intDivisionTID, string strAcademicYear, int intIsLate, int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

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

                pSqlParameter[4] = new SqlParameter("@IsLate", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intIsLate;

                pSqlParameter[5] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStudentMID;

                strStoredProcName = "usp_tbl_StudentFeesTemplate_T_Select_ClassWiseStudentTemplate";

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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


