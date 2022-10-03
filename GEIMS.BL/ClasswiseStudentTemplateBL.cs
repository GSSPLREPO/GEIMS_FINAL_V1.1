using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
    public class ClasswiseStudentTemplateBL
    {
        /// <summary>
    /// Class Created By : Vishal, 9/10/2015
	/// Summary description for Organisation.
    /// </summary>
	
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		#region Select All ClasswiseStudentTemplateBO Details
        /// <summary>
        /// To Select All data from the tbl_ClassWiseStudentTemplate_T table
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  ClasswiseStudentTemplate_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_ClassWiseStudentTemplate_T_SelectAll";
                DataTable dtClasswiseStudentTemplateBO  = new DataTable();
                dtClasswiseStudentTemplateBO = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtClasswiseStudentTemplateBO);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select ClasswiseStudentTemplateBO Details by ClassWiseStudentTemplateTID
        /// <summary>
        /// Select all details of ClasswiseStudentTemplateBO for selected ClassWiseStudentTemplateTID from tbl_ClassWiseStudentTemplate_T table
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
        /// <param name="intClassWiseStudentTemplateTID"></param>
        /// <returns></returns>
		public ApplicationResult ClasswiseStudentTemplate_Select(int intClassMID, int intDivisionTID, int intStudentMID, string strAcademicYear)
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
                
                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intStudentMID;
                
                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

				strStoredProcName = "usp_tbl_ClassWiseStudentTemplate_T_Select";
				
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
		
		#region Delete ClasswiseStudentTemplateBO Details by ClassWiseStudentTemplateTID
        /// <summary>
        /// To Delete details of ClasswiseStudentTemplateBO for selected ClassWiseStudentTemplateTID from tbl_ClassWiseStudentTemplate_T table
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
        /// <param name="intClassWiseStudentTemplateTID"></param>
        /// <returns></returns>
		public ApplicationResult ClasswiseStudentTemplate_Delete(int intClassWiseStudentTemplateTID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@ClassWiseStudentTemplateTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassWiseStudentTemplateTID;

				strStoredProcName = "usp_tbl_ClassWiseStudentTemplate_T_Delete";
				
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
		
		#region Insert ClasswiseStudentTemplateBO Details
		/// <summary>
        /// To Insert details of ClasswiseStudentTemplateBO in tbl_ClassWiseStudentTemplate_T table
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
        /// <param name="objClasswiseStudentTemplateBOBo"></param>
        /// <returns></returns>
        public ApplicationResult ClasswiseStudentTemplate_Insert(ClasswiseStudentTemplateBO objClasswiseStudentTemplateBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[11];
                
				
          		pSqlParameter[0] = new SqlParameter("@FeesCategoryMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objClasswiseStudentTemplateBO.FeesCategoryMID;
 
				pSqlParameter[1] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objClasswiseStudentTemplateBO.TrustMID;
 
				pSqlParameter[2] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objClasswiseStudentTemplateBO.SchoolMID;
 
				pSqlParameter[3] = new SqlParameter("@ClassMID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objClasswiseStudentTemplateBO.ClassMID;
 
				pSqlParameter[4] = new SqlParameter("@DivisionTID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objClasswiseStudentTemplateBO.DivisionTID;
 
				pSqlParameter[5] = new SqlParameter("@StudentMID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objClasswiseStudentTemplateBO.StudentMID;
 
				pSqlParameter[6] = new SqlParameter("@FeesAmount",SqlDbType.Float);
				pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objClasswiseStudentTemplateBO.FeesAmount;
 
				pSqlParameter[7] = new SqlParameter("@AcademicYear",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objClasswiseStudentTemplateBO.AcademicYear;
 
				pSqlParameter[8] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objClasswiseStudentTemplateBO.LastModifiedDate;
 
				pSqlParameter[9] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objClasswiseStudentTemplateBO.LastModifiedUserID;
 
				pSqlParameter[10] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objClasswiseStudentTemplateBO.IsDeleted;

		
				sSql = "usp_tbl_ClassWiseStudentTemplate_T_Insert";
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
                objClasswiseStudentTemplateBO = null;
            }
        }
        #endregion
		
		#region Update ClasswiseStudentTemplateBO Details
		/// <summary>
        /// To Update details of ClasswiseStudentTemplateBO in tbl_ClassWiseStudentTemplate_T table
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
        /// <param name="objClasswiseStudentTemplateBOBo"></param>
        /// <returns></returns>
        public ApplicationResult ClasswiseStudentTemplate_Update(ClasswiseStudentTemplateBO objClasswiseStudentTemplateBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[12];
                
				
          		pSqlParameter[0] = new SqlParameter("@ClassWiseStudentTemplateTID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objClasswiseStudentTemplateBO.ClassWiseStudentTemplateTID;
 
				pSqlParameter[1] = new SqlParameter("@FeesCategoryMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objClasswiseStudentTemplateBO.FeesCategoryMID;
 
				pSqlParameter[2] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objClasswiseStudentTemplateBO.TrustMID;
 
				pSqlParameter[3] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objClasswiseStudentTemplateBO.SchoolMID;
 
				pSqlParameter[4] = new SqlParameter("@ClassMID",SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objClasswiseStudentTemplateBO.ClassMID;
 
				pSqlParameter[5] = new SqlParameter("@DivisionTID",SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objClasswiseStudentTemplateBO.DivisionTID;
 
				pSqlParameter[6] = new SqlParameter("@StudentMID",SqlDbType.Int);
				pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objClasswiseStudentTemplateBO.StudentMID;
 
				pSqlParameter[7] = new SqlParameter("@FeesAmount",SqlDbType.Float);
				pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objClasswiseStudentTemplateBO.FeesAmount;
 
				pSqlParameter[8] = new SqlParameter("@AcademicYear",SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objClasswiseStudentTemplateBO.AcademicYear;
 
				pSqlParameter[9] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objClasswiseStudentTemplateBO.LastModifiedDate;
 
				pSqlParameter[10] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objClasswiseStudentTemplateBO.LastModifiedUserID;
 
				pSqlParameter[11] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objClasswiseStudentTemplateBO.IsDeleted;

		
				sSql = "usp_tbl_ClassWiseStudentTemplate_T_Update";
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
                objClasswiseStudentTemplateBO = null;
            }
        }
        #endregion
		
		
		#region Select ClasswiseStudentTemplateBO Details by ClasswiseStudentTemplateBOName
        /// <summary>
        /// Select all details of ClasswiseStudentTemplateBO for selected ClasswiseStudentTemplateBOName from tbl_ClassWiseStudentTemplate_T table
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
        /// <param name="ClasswiseStudentTemplateBOName"></param>
        /// <returns></returns>
		public ApplicationResult ClasswiseStudentTemplate_Select_byClasswiseStudentTemplateName(string strClasswiseStudentTemplateBOName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@ClasswiseStudentTemplateBOName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strClasswiseStudentTemplateBOName;

				strStoredProcName = "usp_tbl_ClassWiseStudentTemplate_T_Select_ByClasswiseStudentTemplateBO";
				
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
		
		
		#region ValidateName for ClasswiseStudentTemplate 
        /// <summary>
        /// Function which validates whether the ClasswiseStudentTemplateBOName already exits in tbl_ClassWiseStudentTemplate_T table.
        /// Created By : Vishal, 9/10/2015
		/// Modified By :
        /// </summary>
        /// <param name="strClasswiseStudentTemplateBOName"></param>
        /// <returns></returns>
		public ApplicationResult ClasswiseStudentTemplate_ValidateName(string strClasswiseStudentTemplateBOName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@ClasswiseStudentTemplateBOName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strClasswiseStudentTemplateBOName;

				strStoredProcName = "usp_tbl_ClassWiseStudentTemplate_T_Validate_ClasswiseStudentTemplateBOName";
				
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
