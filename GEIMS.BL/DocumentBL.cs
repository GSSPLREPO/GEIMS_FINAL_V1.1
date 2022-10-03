using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 1/4/2015
	/// Summary description for Organisation.
	/// </summary>
	public class DocumentBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion


		#region Select All Document Details
		/// <summary>
		/// To Select All data from the tbl_Document_M table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult Document_SelectAll(int intSchoolMID, int intTrustMID, int intStudentMID, int intEmployeeMID)
		{
			try
			{
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intStudentMID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intEmployeeMID;

				sSql = "usp_tbl_Document_M_SelectAll";
				DataTable dtDocument = new DataTable();
                dtDocument = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

				ApplicationResult objResults = new ApplicationResult(dtDocument);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

        #region Select All Document Details
        /// <summary>
        /// To Select All data from the tbl_Document_M table
        /// Created By : NafisaMulla, 1/4/2015
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Document_SelectAll_ForDropDown(int intSchoolMID, int intTrustMID, int intStudentMID, int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intSchoolMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intTrustMID;

                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intStudentMID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intEmployeeMID;

                sSql = "usp_tbl_Document_M_SelectAll_ForDropDown";
                DataTable dtDocument = new DataTable();
                dtDocument = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtDocument);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

		#region Select Document Details by DocumentMID
		/// <summary>
		/// Select all details of Document for selected DocumentMID from tbl_Document_M table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="intDocumentMID"></param>
		/// <returns></returns>
		public ApplicationResult Document_Select(int intDocumentMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@DocumentMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intDocumentMID;

				strStoredProcName = "usp_tbl_Document_M_Select";

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

		#region Delete Document Details by DocumentMID
		/// <summary>
		/// To Delete details of Document for selected DocumentMID from tbl_Document_M table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="intDocumentMID"></param>
		/// <returns></returns>
		public ApplicationResult Document_Delete(int intDocumentMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@DocumentMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intDocumentMID;

				strStoredProcName = "usp_tbl_Document_M_Delete";

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

		#region Insert Document Details
		/// <summary>
		/// To Insert details of Document in tbl_Document_M table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="objDocumentBO"></param>
		/// <returns></returns>
		public ApplicationResult Document_Insert(DocumentBO objDocumentBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[9];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objDocumentBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objDocumentBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objDocumentBO.StudentMID;

                pSqlParameter[3] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objDocumentBO.EmployeeMID;

                pSqlParameter[4] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objDocumentBO.DocumentName;

                pSqlParameter[5] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objDocumentBO.DocumentPath;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objDocumentBO.LastModifiedUserID;

                pSqlParameter[7] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objDocumentBO.LastModifiedDate;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objDocumentBO.IsDeleted;


                strStoredProcName = "usp_tbl_Document_M_Insert";
				
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
			finally
			{
				objDocumentBO = null;
			}
		}
		#endregion

		#region Update Document Details
		/// <summary>
		/// To Update details of Document in tbl_Document_M table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="objDocumentBO"></param>
		/// <returns></returns>
		public ApplicationResult Document_Update(DocumentBO objDocumentBO)
		{
			try
			{
                pSqlParameter = new SqlParameter[10];


                pSqlParameter[0] = new SqlParameter("@DocumentMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objDocumentBO.DocumentMID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objDocumentBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objDocumentBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objDocumentBO.StudentMID;

                pSqlParameter[4] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objDocumentBO.EmployeeMID;

                pSqlParameter[5] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objDocumentBO.DocumentName;

                pSqlParameter[6] = new SqlParameter("@DocumentPath", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objDocumentBO.DocumentPath;

                pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objDocumentBO.LastModifiedUserID;

                pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objDocumentBO.LastModifiedDate;

                pSqlParameter[9] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objDocumentBO.IsDeleted;


				sSql = "usp_tbl_Document_M_Update";
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
				objDocumentBO = null;
			}
		}
		#endregion




		#region Select Document Details by DocumentName
		/// <summary>
		/// Select all details of Document for selected DocumentName from tbl_Document_M table
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="DocumentName"></param>
		/// <returns></returns>
		public ApplicationResult Document_Select_byDocumentName(string strDocumentName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strDocumentName;

				strStoredProcName = "usp_tbl_Document_M_Select_ByDocument";

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


		#region ValidateName for Document
		/// <summary>
		/// Function which validates whether the DocumentName already exits in tbl_Document_M table.
		/// Created By : NafisaMulla, 1/4/2015
		/// Modified By :
		/// </summary>
		/// <param name="strDocumentName"></param>
		/// <returns></returns>
		public ApplicationResult Document_ValidateName(string strDocumentName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@DocumentName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strDocumentName;

				strStoredProcName = "usp_tbl_Document_M_Validate_DocumentName";

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


