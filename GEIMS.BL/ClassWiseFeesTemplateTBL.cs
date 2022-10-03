using System;
using System.Data;
using System.Data.SqlClient;
using GEIMS.DataAccess;
using GEIMS.BO;
using GEIMS.Common;

namespace GEIMS.BL
{
	/// <summary>
	/// Class Created By : NafisaMulla, 2/7/2014
	/// Summary description for Organisation.
	/// </summary>
	public class ClassWiseFeesTemplateTBL
	{
		#region user defined variables
		public string sSql;
		public string strStoredProcName;
		public SqlParameter[] pSqlParameter = null;
		#endregion


		#region Select All ClassWiseFeesTemplateT Details
		/// <summary>
		/// To Select All data from the tbl_ClassWiseFeesTemplate_T table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_SelectAll()
		{
			try
			{
				sSql = "usp_tbl_ClassWiseFeesTemplate_T_SelectAll";
				DataTable dtClassWiseFeesTemplateT = new DataTable();
				dtClassWiseFeesTemplateT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

				ApplicationResult objResults = new ApplicationResult(dtClassWiseFeesTemplateT);
				objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
				return objResults;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Select ClassWiseFeesTemplateT Details by ClassWiseFeesTemplateTID
		/// <summary>
		/// Select all details of ClassWiseFeesTemplateT for selected ClassWiseFeesTemplateTID from tbl_ClassWiseFeesTemplate_T table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intClassWiseFeesTemplateTID"></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_Select(int intClassWiseFeesTemplateTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intClassWiseFeesTemplateTID;

				strStoredProcName = "usp_tbl_ClassWiseFeesTemplate_T_Select";

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

		#region Delete ClassWiseFeesTemplateT Details by ClassWiseFeesTemplateTID
		/// <summary>
		/// To Delete details of ClassWiseFeesTemplateT for selected ClassWiseFeesTemplateTID from tbl_ClassWiseFeesTemplate_T table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="intClassWiseFeesTemplateTID"></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_Delete(int intClassWiseFeesTemplateTID)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intClassWiseFeesTemplateTID;

				strStoredProcName = "usp_tbl_ClassWiseFeesTemplate_T_Delete";

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

		#region Insert ClassWiseFeesTemplateT Details
		/// <summary>
		/// To Insert details of ClassWiseFeesTemplateT in tbl_ClassWiseFeesTemplate_T table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objClassWiseFeesTemplateTBO"></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_Insert(ClassWiseFeesTemplateTBO objClassWiseFeesTemplateTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[11];


				pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objClassWiseFeesTemplateTBO.TrustMID;

				pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objClassWiseFeesTemplateTBO.ClassMID;

				pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objClassWiseFeesTemplateTBO.SchoolMID;

				pSqlParameter[3] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objClassWiseFeesTemplateTBO.FeesCategoryMID;

				pSqlParameter[4] = new SqlParameter("@DivisionTID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objClassWiseFeesTemplateTBO.DivisionTID;

                //pSqlParameter[5] = new SqlParameter("@FeesAmount", SqlDbType.Float);
                //pSqlParameter[5].Direction = ParameterDirection.Input;
                //pSqlParameter[5].Value = objClassWiseFeesTemplateTBO.FeesAmount;

                pSqlParameter[5] = new SqlParameter("@FeesAmountForMale", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objClassWiseFeesTemplateTBO.FeesAmountForMale;

                pSqlParameter[6] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
				pSqlParameter[6].Value = objClassWiseFeesTemplateTBO.AcademicYear;

				pSqlParameter[7] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objClassWiseFeesTemplateTBO.LastModifiedUserID;

				pSqlParameter[8] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objClassWiseFeesTemplateTBO.LastModifiedDate;

				pSqlParameter[9] = new SqlParameter("@Isdeleted", SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objClassWiseFeesTemplateTBO.Isdeleted;             

                pSqlParameter[10] = new SqlParameter("@FeesAmountForFemale", SqlDbType.Float);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objClassWiseFeesTemplateTBO.FeesAmountForFemale;


                sSql = "usp_tbl_ClassWiseFeesTemplate_T_Insert";
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
				objClassWiseFeesTemplateTBO = null;
			}
		}
		#endregion

		#region Update ClassWiseFeesTemplateT Details
		/// <summary>
		/// To Update details of ClassWiseFeesTemplateT in tbl_ClassWiseFeesTemplate_T table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="objClassWiseFeesTemplateTBO"></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_Update(ClassWiseFeesTemplateTBO objClassWiseFeesTemplateTBO)
		{
			try
			{
				pSqlParameter = new SqlParameter[12];


				pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = objClassWiseFeesTemplateTBO.ClassWiseFeesTemplateTID;

				pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
				pSqlParameter[1].Value = objClassWiseFeesTemplateTBO.TrustMID;

				pSqlParameter[2] = new SqlParameter("@ClassMID", SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
				pSqlParameter[2].Value = objClassWiseFeesTemplateTBO.ClassMID;

				pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
				pSqlParameter[3].Value = objClassWiseFeesTemplateTBO.SchoolMID;

				pSqlParameter[4] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[4].Direction = ParameterDirection.Input;
				pSqlParameter[4].Value = objClassWiseFeesTemplateTBO.FeesCategoryMID;

                pSqlParameter[5] = new SqlParameter("@DivisionTID", SqlDbType.Int);
				pSqlParameter[5].Direction = ParameterDirection.Input;
				pSqlParameter[5].Value = objClassWiseFeesTemplateTBO.DivisionTID;

				//pSqlParameter[6] = new SqlParameter("@FeesAmount", SqlDbType.Float);
				//pSqlParameter[6].Direction = ParameterDirection.Input;
				//pSqlParameter[6].Value = objClassWiseFeesTemplateTBO.FeesAmount;

                pSqlParameter[6] = new SqlParameter("@FeesAmountForMale", SqlDbType.Float);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objClassWiseFeesTemplateTBO.FeesAmountForMale;

                pSqlParameter[7] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
				pSqlParameter[7].Value = objClassWiseFeesTemplateTBO.AcademicYear;

				pSqlParameter[8] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
				pSqlParameter[8].Value = objClassWiseFeesTemplateTBO.LastModifiedUserID;

				pSqlParameter[9] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
				pSqlParameter[9].Direction = ParameterDirection.Input;
				pSqlParameter[9].Value = objClassWiseFeesTemplateTBO.LastModifiedDate;

				pSqlParameter[10] = new SqlParameter("@Isdeleted", SqlDbType.Int);
				pSqlParameter[10].Direction = ParameterDirection.Input;
				pSqlParameter[10].Value = objClassWiseFeesTemplateTBO.Isdeleted;

                pSqlParameter[11] = new SqlParameter("@FeesAmountForFemale", SqlDbType.Float);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objClassWiseFeesTemplateTBO.FeesAmountForFemale;


                sSql = "usp_tbl_ClassWiseFeesTemplate_T_Update";
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
				objClassWiseFeesTemplateTBO = null;
			}
		}
		#endregion




		#region Select ClassWiseFeesTemplateT Details by ClassWiseFeesTemplateTName
		/// <summary>
		/// Select all details of ClassWiseFeesTemplateT for selected ClassWiseFeesTemplateTName from tbl_ClassWiseFeesTemplate_T table
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="ClassWiseFeesTemplateTName"></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_Select_byClassWiseFeesTemplateTName(string strClassWiseFeesTemplateTName)
		{
			try
			{
				pSqlParameter = new SqlParameter[1];

				pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTName", SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = strClassWiseFeesTemplateTName;

				strStoredProcName = "usp_tbl_ClassWiseFeesTemplate_T_Select_ByClassWiseFeesTemplateT";

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


		#region ValidateName for ClassWiseFeesTemplateT
		/// <summary>
		/// Function which validates whether the ClassWiseFeesTemplateTName already exits in tbl_ClassWiseFeesTemplate_T table.
		/// Created By : NafisaMulla, 2/7/2014
		/// Modified By :
		/// </summary>
		/// <param name="strClassWiseFeesTemplateTName"></param>
		/// <returns></returns>
		public ApplicationResult ClassWiseFeesTemplateT_ValidateName(int intClassWiseFeesTemplateTID, int intFeesCategoryMID)
		{
			try
			{
				pSqlParameter = new SqlParameter[2];

                pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
				pSqlParameter[0].Value = intClassWiseFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intFeesCategoryMID;

                strStoredProcName = "usp_tbl_ClassWiseFeesTemplate_T_ValidationName";

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

        #region Delete ClassWiseFeesTemplateT Details by ClassWise
        /// <summary>
        /// To Delete details of ClassWiseFeesTemplateT for selected ClassWiseFeesTemplateTID from tbl_ClassWiseFeesTemplate_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intClassWiseFeesTemplateTID"></param>
        /// <returns></returns>
        public ApplicationResult ClassWiseFeesTemplateT_Delete_Class(int intClassWiseFeesTemplateTID , int intClassMID, int intDivisionTID, string strAcademicYear)
        {
            try
            {
                pSqlParameter = new SqlParameter[4];

                pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassWiseFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                strStoredProcName = "usp_tbl_ClassWiseFeesTemplate_T_Delete_ClassWIse";

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

        #region Select All ClassWiseFeesTemplateT Details Fees Category Master
        /// <summary>
        /// To Select All data from the tbl_ClassWiseFeesTemplate_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult ClassWiseFeesTemplateT_Select_FeesCategory_M(int intFeesCategoryMID, int intClassMID, int intDivisionTID, string strAcademicYear,int intSchoolMID, int intStrudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@FeesCategoryMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFeesCategoryMID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                pSqlParameter[5] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStrudentMID;

           

                sSql = "usp_tbl_ClassWiseTemplate_T_FeeCategory_M";
                DataTable dtClassWiseFeesTemplateT = new DataTable();
                dtClassWiseFeesTemplateT = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtClassWiseFeesTemplateT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region ValidateName for ClassWiseFeeTemplate_T_Fee_Collection_M
        public ApplicationResult ClassWiseFeesTemplateT_Fee_Collection_M(int intClassWiseFeesTemplateTID, int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID, int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassWiseFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                pSqlParameter[5] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStudentMID;

                strStoredProcName = "usp_tbl_ClassWiseFeeTemplate_T_Fee_Collection_M";

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

        //#region ValidateName for ClassWiseFeeTemplate_T_SelectAll_Fee_Collection_M
        //public ApplicationResult ClassWiseFeesTemplateT_SelectAll_Fee_Collection_M(int intClassMID, string strDivisionName, string strAcademicYear, int intSchoolMID)
        //{
        //    try
        //    {
        //        pSqlParameter = new SqlParameter[4];

        //        pSqlParameter[0] = new SqlParameter("@ClassMID", SqlDbType.Int);
        //        pSqlParameter[0].Direction = ParameterDirection.Input;
        //        pSqlParameter[0].Value = intClassMID;

        //        pSqlParameter[1] = new SqlParameter("@DivisionTID", SqlDbType.VarChar);
        //        pSqlParameter[1].Direction = ParameterDirection.Input;
        //        pSqlParameter[1].Value = strDivisionName;

        //        pSqlParameter[2] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
        //        pSqlParameter[2].Direction = ParameterDirection.Input;
        //        pSqlParameter[2].Value = strAcademicYear;

        //        pSqlParameter[3] = new SqlParameter("@SchoolMID", SqlDbType.Int);
        //        pSqlParameter[3].Direction = ParameterDirection.Input;
        //        pSqlParameter[3].Value = intSchoolMID;

        //        strStoredProcName = "usp_tbl_ClassWiseFeeTemplate_T_Fee_Collection_M";

        //        DataTable dtResult = new DataTable();
        //        dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
        //        ApplicationResult objResults = new ApplicationResult(dtResult);
        //        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
        //        return objResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        #region ValidateName for ClassWiseFeeTemplate_T_in_StudentFeeTemplate_T
        public ApplicationResult ClassWiseFeeTemplate_T_in_StudentFeeTemplate_T(int intClassWiseFeesTemplateTID, int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID, int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@ClassWiseFeesTemplateTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intClassWiseFeesTemplateTID;

                pSqlParameter[1] = new SqlParameter("@ClassMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = intClassMID;

                pSqlParameter[2] = new SqlParameter("@DivisionTID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intDivisionTID;

                pSqlParameter[3] = new SqlParameter("@AcademicYear", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = strAcademicYear;

                pSqlParameter[4] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intSchoolMID;

                pSqlParameter[5] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intStudentMID;

                strStoredProcName = "usp_tbl_ClasswiseFee_T_IN_tbl_StudentFeeTemplate_T";

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


        #region Select ClassWiseFeesTemplateT Details by Report
        /// <summary>
        /// Select all details of ClassWiseFeesTemplateT for selected ClassWiseFeesTemplateTID from tbl_ClassWiseFeesTemplate_T table
        /// Created By : NafisaMulla, 2/7/2014
        /// Modified By :
        /// </summary>
        /// <param name="intClassWiseFeesTemplateTID"></param>
        /// <returns></returns>
        public ApplicationResult ClassWiseFeesTemplateT_Select_ForReport(int intSchoolMID, int intClassMID, int intDivisionTID, string strYear)
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
                pSqlParameter[3].Value = strYear;

                strStoredProcName = "usp_tbl_ClassWiseFeesTemplate_T_SelectInReport";

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

        #region ValidateName for StudentFeeTemplate_For_Validation
        public ApplicationResult StudentFeesTemplate_ForValidation(int intClassMID, int intDivisionTID, string strAcademicYear, int intSchoolMID, int intStudentMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[5];

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

                pSqlParameter[4] = new SqlParameter("@StudentMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intStudentMID;

                strStoredProcName = "usp_tbl_StudentFeesTemplate_T_For validation";

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


