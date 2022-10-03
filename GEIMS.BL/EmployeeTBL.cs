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
  public  class EmployeeTBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		#region Select All EmployeeFamilyT Details
        /// <summary>
        /// To Select All data from the tbl_EmployeeFamilyPerson_T table
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult EmployeeFamilyT_SelectAll(int intEmployeeMID)
        {
			try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

				sSql = "usp_tbl_EmployeeFamilyPerson_T_SelectAll";
                DataTable dtEmployeeFamilyT  = new DataTable();
                dtEmployeeFamilyT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmployeeFamilyT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select EmployeeFamilyT Details by FamilyPersonTID
        /// <summary>
        /// Select all details of EmployeeFamilyT for selected FamilyPersonTID from tbl_EmployeeFamilyPerson_T table
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
        /// <param name="intFamilyPersonTID"></param>
        /// <returns></returns>
		public ApplicationResult EmployeeFamilyT_Select(int intFamilyPersonTID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@FamilyPersonTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFamilyPersonTID;

				strStoredProcName = "usp_tbl_EmployeeFamilyPerson_T_Select";
				
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
		
		#region Delete EmployeeFamilyT Details by FamilyPersonTID
        /// <summary>
        /// To Delete details of EmployeeFamilyT for selected FamilyPersonTID from tbl_EmployeeFamilyPerson_T table
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
        /// <param name="intFamilyPersonTID"></param>
        /// <returns></returns>
		public ApplicationResult EmployeeFamilyT_Delete(int intFamilyPersonTID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@FamilyPersonTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intFamilyPersonTID;

				strStoredProcName = "usp_tbl_EmployeeFamilyPerson_T_Delete";
				
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
		
		#region Insert EmployeeFamilyT Details
		/// <summary>
        /// To Insert details of EmployeeFamilyT in tbl_EmployeeFamilyPerson_T table
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
        /// <param name="objEmployeeFamilyTBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeFamilyT_Insert(EmployeeFamilyTBO objEmployeeFamilyTBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[9];
                
				
          		pSqlParameter[0] = new SqlParameter("@FamilyPersonName",SqlDbType.VarChar);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objEmployeeFamilyTBO.FamilyPersonName;
 
				pSqlParameter[1] = new SqlParameter("@Occupation",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objEmployeeFamilyTBO.Occupation;
 
				pSqlParameter[2] = new SqlParameter("@Organisation",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objEmployeeFamilyTBO.Organisation;
 
				pSqlParameter[3] = new SqlParameter("@Qualification",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objEmployeeFamilyTBO.Qualification;
 
				pSqlParameter[4] = new SqlParameter("@ContactNo",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objEmployeeFamilyTBO.ContactNo;
 
				pSqlParameter[5] = new SqlParameter("@MobileNo",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objEmployeeFamilyTBO.MobileNo;
 
				pSqlParameter[6] = new SqlParameter("@EmailID",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objEmployeeFamilyTBO.EmailID;
 
				pSqlParameter[7] = new SqlParameter("@EmployeeMID",SqlDbType.Int);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objEmployeeFamilyTBO.EmployeeMID;
 
				pSqlParameter[8] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objEmployeeFamilyTBO.IsDeleted;

		
				sSql = "usp_tbl_EmployeeFamilyPerson_T_Insert";
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
                objEmployeeFamilyTBO = null;
            }
        }
        #endregion
		
		#region Update EmployeeFamilyT Details
		/// <summary>
        /// To Update details of EmployeeFamilyT in tbl_EmployeeFamilyPerson_T table
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
        /// <param name="objEmployeeFamilyTBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeFamilyT_Update(EmployeeFamilyTBO objEmployeeFamilyTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[10];
                
				
          		pSqlParameter[0] = new SqlParameter("@FamilyPersonTID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objEmployeeFamilyTBO.FamilyPersonTID;
 
				pSqlParameter[1] = new SqlParameter("@FamilyPersonName",SqlDbType.VarChar);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objEmployeeFamilyTBO.FamilyPersonName;
 
				pSqlParameter[2] = new SqlParameter("@Occupation",SqlDbType.VarChar);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objEmployeeFamilyTBO.Occupation;
 
				pSqlParameter[3] = new SqlParameter("@Organisation",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objEmployeeFamilyTBO.Organisation;
 
				pSqlParameter[4] = new SqlParameter("@Qualification",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objEmployeeFamilyTBO.Qualification;
 
				pSqlParameter[5] = new SqlParameter("@ContactNo",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objEmployeeFamilyTBO.ContactNo;
 
				pSqlParameter[6] = new SqlParameter("@MobileNo",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objEmployeeFamilyTBO.MobileNo;
 
				pSqlParameter[7] = new SqlParameter("@EmailID",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
          		pSqlParameter[7].Value = objEmployeeFamilyTBO.EmailID;
 
				pSqlParameter[8] = new SqlParameter("@EmployeeMID",SqlDbType.Int);
				pSqlParameter[8].Direction = ParameterDirection.Input;
          		pSqlParameter[8].Value = objEmployeeFamilyTBO.EmployeeMID;
 
				pSqlParameter[9] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objEmployeeFamilyTBO.IsDeleted;

		
				sSql = "usp_tbl_EmployeeFamilyPerson_T_Update";
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
                objEmployeeFamilyTBO = null;
            }
        }
        #endregion
		
		
		
		
		#region Select EmployeeFamilyT Details by EmployeeFamilyTName
        /// <summary>
        /// Select all details of EmployeeFamilyT for selected EmployeeFamilyTName from tbl_EmployeeFamilyPerson_T table
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
        /// <param name="EmployeeFamilyTName"></param>
        /// <returns></returns>
		public ApplicationResult EmployeeFamilyT_Select_byEmployeeFamilyTName(string strEmployeeFamilyTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@EmployeeFamilyTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmployeeFamilyTName;

				strStoredProcName = "usp_tbl_EmployeeFamilyPerson_T_Select_ByEmployeeFamilyT";
				
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
		
		
		#region ValidateName for EmployeeFamilyT 
        /// <summary>
        /// Function which validates whether the EmployeeFamilyTName already exits in tbl_EmployeeFamilyPerson_T table.
        /// Created By : Darshan, 10-10-2014
		/// Modified By :
        /// </summary>
        /// <param name="strEmployeeFamilyTName"></param>
        /// <returns></returns>
		public ApplicationResult EmployeeFamilyT_ValidateName(string strEmployeeFamilyTName)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@EmployeeFamilyTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmployeeFamilyTName;

				strStoredProcName = "usp_tbl_EmployeeFamilyPerson_T_Validate_EmployeeFamilyTName";
				
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
		
		////-------EmployeeExprience---------////////

        #region Select All EmployeeExpirenceT Details
        /// <summary>
        /// To Select All data from the tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_SelectAll(int intEmployeeID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeID;

                sSql = "usp_tbl_EmployeeExperience_T_SelectAll";
                DataTable dtEmployeeExpirenceT = new DataTable();
                dtEmployeeExpirenceT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmployeeExpirenceT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmployeeExpirenceT Details by EmployeeExperienceTID
        /// <summary>
        /// Select all details of EmployeeExpirenceT for selected EmployeeExperienceTID from tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeExperienceTID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_Select(int intEmployeeExperienceTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeExperienceTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeExperienceTID;

                strStoredProcName = "usp_tbl_EmployeeExperience_T_Select";

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

        #region Delete EmployeeExpirenceT Details by EmployeeExperienceTID
        /// <summary>
        /// To Delete details of EmployeeExpirenceT for selected EmployeeExperienceTID from tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeExperienceTID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_Delete(int intEmployeeExperienceTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeExperienceTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeExperienceTID;

                strStoredProcName = "usp_tbl_EmployeeExperience_T_Delete";

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

        #region Insert EmployeeExpirenceT Details
        /// <summary>
        /// To Insert details of EmployeeExpirenceT in tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="objEmployeeExpirenceTBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_Insert(EmployeeExpirenceTBO objEmployeeExpirenceTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[15];


                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeExpirenceTBO.EmployeeMID;

                pSqlParameter[1] = new SqlParameter("@OrganisationNameENG", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeExpirenceTBO.OrganisationNameENG;

                pSqlParameter[2] = new SqlParameter("@OrganisationNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeExpirenceTBO.OrganisationNameGUJ;

                pSqlParameter[3] = new SqlParameter("@OrganisationAddressENG", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeExpirenceTBO.OrganisationAddressENG;

                pSqlParameter[4] = new SqlParameter("@OrganisationAddressGUJ", SqlDbType.NVarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeExpirenceTBO.OrganisationAddressGUJ;

                pSqlParameter[5] = new SqlParameter("@DesignationENG", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeExpirenceTBO.DesignationENG;

                pSqlParameter[6] = new SqlParameter("@DesignationGUJ", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeExpirenceTBO.DesignationGUJ;

                pSqlParameter[7] = new SqlParameter("@DurationYear", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeExpirenceTBO.DurationYear;

                pSqlParameter[8] = new SqlParameter("@DurationMonth", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeExpirenceTBO.DurationMonth;

                pSqlParameter[9] = new SqlParameter("@JobResponsibilityENG", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeExpirenceTBO.JobResponsibilityENG;

                pSqlParameter[10] = new SqlParameter("@JobResponsibilityGUJ", SqlDbType.NVarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objEmployeeExpirenceTBO.JobResponsibilityGUJ;

                pSqlParameter[11] = new SqlParameter("@CTC", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objEmployeeExpirenceTBO.CTC;

                pSqlParameter[12] = new SqlParameter("@ReasonOfLeavingENG", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objEmployeeExpirenceTBO.ReasonOfLeavingENG;

                pSqlParameter[13] = new SqlParameter("@ReasonOfLeavingGUJ", SqlDbType.NVarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objEmployeeExpirenceTBO.ReasonOfLeavingGUJ;

                pSqlParameter[14] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objEmployeeExpirenceTBO.IsDeleted;


                sSql = "usp_tbl_EmployeeExperience_T_Insert";
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
                objEmployeeExpirenceTBO = null;
            }
        }
        #endregion

        #region Update EmployeeExpirenceT Details
        /// <summary>
        /// To Update details of EmployeeExpirenceT in tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="objEmployeeExpirenceTBO"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_Update(EmployeeExpirenceTBO objEmployeeExpirenceTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[16];


                pSqlParameter[0] = new SqlParameter("@EmployeeExperienceTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmployeeExpirenceTBO.EmployeeExperienceTID;

                pSqlParameter[1] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmployeeExpirenceTBO.EmployeeMID;

                pSqlParameter[2] = new SqlParameter("@OrganisationNameENG", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmployeeExpirenceTBO.OrganisationNameENG;

                pSqlParameter[3] = new SqlParameter("@OrganisationNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmployeeExpirenceTBO.OrganisationNameGUJ;

                pSqlParameter[4] = new SqlParameter("@OrganisationAddressENG", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmployeeExpirenceTBO.OrganisationAddressENG;

                pSqlParameter[5] = new SqlParameter("@OrganisationAddressGUJ", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmployeeExpirenceTBO.OrganisationAddressGUJ;

                pSqlParameter[6] = new SqlParameter("@DesignationENG", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmployeeExpirenceTBO.DesignationENG;

                pSqlParameter[7] = new SqlParameter("@DesignationGUJ", SqlDbType.NVarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmployeeExpirenceTBO.DesignationGUJ;

                pSqlParameter[8] = new SqlParameter("@DurationYear", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmployeeExpirenceTBO.DurationYear;

                pSqlParameter[9] = new SqlParameter("@DurationMonth", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objEmployeeExpirenceTBO.DurationMonth;

                pSqlParameter[10] = new SqlParameter("@JobResponsibilityENG", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objEmployeeExpirenceTBO.JobResponsibilityENG;

                pSqlParameter[11] = new SqlParameter("@JobResponsibilityGUJ", SqlDbType.NVarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objEmployeeExpirenceTBO.JobResponsibilityGUJ;

                pSqlParameter[12] = new SqlParameter("@CTC", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objEmployeeExpirenceTBO.CTC;

                pSqlParameter[13] = new SqlParameter("@ReasonOfLeavingENG", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objEmployeeExpirenceTBO.ReasonOfLeavingENG;

                pSqlParameter[14] = new SqlParameter("@ReasonOfLeavingGUJ", SqlDbType.NVarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objEmployeeExpirenceTBO.ReasonOfLeavingGUJ;

                pSqlParameter[15] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objEmployeeExpirenceTBO.IsDeleted;


                sSql = "usp_tbl_EmployeeExperience_T_Update";
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
                objEmployeeExpirenceTBO = null;
            }
        }
        #endregion




        #region Select EmployeeExpirenceT Details by EmployeeExpirenceTName
        /// <summary>
        /// Select all details of EmployeeExpirenceT for selected EmployeeExpirenceTName from tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="EmployeeExpirenceTName"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_Select_byEmployeeExpirenceTName(string strEmployeeExpirenceTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeExpirenceTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmployeeExpirenceTName;

                strStoredProcName = "usp_tbl_EmployeeExperience_T_Select_ByEmployeeExpirenceT";

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


        #region ValidateName for EmployeeExpirenceT
        /// <summary>
        /// Function which validates whether the EmployeeExpirenceTName already exits in tbl_EmployeeExperience_T table.
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="strEmployeeExpirenceTName"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_ValidateName(string strEmployeeExpirenceTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeExpirenceTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmployeeExpirenceTName;

                strStoredProcName = "usp_tbl_EmployeeExperience_T_Validate_EmployeeExpirenceTName";

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

        ////-------EmployeeQualification---------////////

        #region Select All EmpoyeeQualificationT Details
        /// <summary>
        /// To Select All data from the tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_SelectAll(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                sSql = "usp_tbl_EmployeeQualification_T_SelectAll";
                DataTable dtEmpoyeeQualificationT = new DataTable();
                dtEmpoyeeQualificationT = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);

                ApplicationResult objResults = new ApplicationResult(dtEmpoyeeQualificationT);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select EmpoyeeQualificationT Details by QualificationTID
        /// <summary>
        /// Select all details of EmpoyeeQualificationT for selected QualificationTID from tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intQualificationTID"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_Select(int intQualificationTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@QualificationTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intQualificationTID;

                strStoredProcName = "usp_tbl_EmployeeQualification_T_Select";

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

        #region Delete EmpoyeeQualificationT Details by QualificationTID
        /// <summary>
        /// To Delete details of EmpoyeeQualificationT for selected QualificationTID from tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intQualificationTID"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_Delete(int intQualificationTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@QualificationTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intQualificationTID;

                strStoredProcName = "usp_tbl_EmployeeQualification_T_Delete";

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

        #region Insert EmpoyeeQualificationT Details
        /// <summary>
        /// To Insert details of EmpoyeeQualificationT in tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="objEmpoyeeQualificationTBO"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_Insert(EmpoyeeQualificationTBO objEmpoyeeQualificationTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[8];


                pSqlParameter[0] = new SqlParameter("@QualificationNameENG", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmpoyeeQualificationTBO.QualificationNameENG;

                pSqlParameter[1] = new SqlParameter("@QualificationNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmpoyeeQualificationTBO.QualificationNameGUJ;

                pSqlParameter[2] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmpoyeeQualificationTBO.Year;

                pSqlParameter[3] = new SqlParameter("@Percentage", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmpoyeeQualificationTBO.Percentage;

                pSqlParameter[4] = new SqlParameter("@UniversityENG", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmpoyeeQualificationTBO.UniversityENG;

                pSqlParameter[5] = new SqlParameter("@UniversityGUJ", SqlDbType.NVarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmpoyeeQualificationTBO.UniversityGUJ;

                pSqlParameter[6] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmpoyeeQualificationTBO.EmployeeMID;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmpoyeeQualificationTBO.IsDeleted;


                sSql = "usp_tbl_EmployeeQualification_T_Insert";
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
                objEmpoyeeQualificationTBO = null;
            }
        }
        #endregion

        #region Update EmpoyeeQualificationT Details
        /// <summary>
        /// To Update details of EmpoyeeQualificationT in tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="objEmpoyeeQualificationTBO"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_Update(EmpoyeeQualificationTBO objEmpoyeeQualificationTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[9];


                pSqlParameter[0] = new SqlParameter("@QualificationTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objEmpoyeeQualificationTBO.QualificationTID;

                pSqlParameter[1] = new SqlParameter("@QualificationNameENG", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objEmpoyeeQualificationTBO.QualificationNameENG;

                pSqlParameter[2] = new SqlParameter("@QualificationNameGUJ", SqlDbType.NVarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objEmpoyeeQualificationTBO.QualificationNameGUJ;

                pSqlParameter[3] = new SqlParameter("@Year", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objEmpoyeeQualificationTBO.Year;

                pSqlParameter[4] = new SqlParameter("@Percentage", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objEmpoyeeQualificationTBO.Percentage;

                pSqlParameter[5] = new SqlParameter("@UniversityENG", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objEmpoyeeQualificationTBO.UniversityENG;

                pSqlParameter[6] = new SqlParameter("@UniversityGUJ", SqlDbType.NVarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objEmpoyeeQualificationTBO.UniversityGUJ;

                pSqlParameter[7] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objEmpoyeeQualificationTBO.EmployeeMID;

                pSqlParameter[8] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objEmpoyeeQualificationTBO.IsDeleted;


                sSql = "usp_tbl_EmployeeQualification_T_Update";
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
                objEmpoyeeQualificationTBO = null;
            }
        }
        #endregion




        #region Select EmpoyeeQualificationT Details by EmpoyeeQualificationTName
        /// <summary>
        /// Select all details of EmpoyeeQualificationT for selected EmpoyeeQualificationTName from tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="EmpoyeeQualificationTName"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_Select_byEmpoyeeQualificationTName(string strEmpoyeeQualificationTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmpoyeeQualificationTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmpoyeeQualificationTName;

                strStoredProcName = "usp_tbl_EmployeeQualification_T_Select_ByEmpoyeeQualificationT";

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


        #region ValidateName for EmpoyeeQualificationT
        /// <summary>
        /// Function which validates whether the EmpoyeeQualificationTName already exits in tbl_EmployeeQualification_T table.
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="strEmpoyeeQualificationTName"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_ValidateName(string strEmpoyeeQualificationTName)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmpoyeeQualificationTName", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strEmpoyeeQualificationTName;

                strStoredProcName = "usp_tbl_EmployeeQualification_T_Validate_EmpoyeeQualificationTName";

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

        #region Select EmpoyeeQualificationT Details by For Report
        /// <summary>
        /// Select all details of EmpoyeeQualificationT for selected QualificationTID from tbl_EmployeeQualification_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intQualificationTID"></param>
        /// <returns></returns>
        public ApplicationResult EmpoyeeQualificationT_SelectForReport(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_EmployeeQualification_T_SelectForReport";

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

        #region Select EmployeeExpirenceT Details by For Report
        /// <summary>
        /// Select all details of EmployeeExpirenceT for selected EmployeeExperienceTID from tbl_EmployeeExperience_T table
        /// Created By : Darshan, 10-10-2014
        /// Modified By :
        /// </summary>
        /// <param name="intEmployeeExperienceTID"></param>
        /// <returns></returns>
        public ApplicationResult EmployeeExpirenceT_SelectForReport(int intEmployeeMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@EmployeeMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intEmployeeMID;

                strStoredProcName = "usp_tbl_EmployeeExperience_T_SelectForReport";

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



