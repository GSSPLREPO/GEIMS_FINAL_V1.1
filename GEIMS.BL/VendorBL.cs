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
    /// <summary>
    /// Class Created By : Chintan, 10/28/2014
    /// Summary description for Organisation.
    /// </summary>
    public class VendorBL
    {
        #region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion


        #region Select All Vendor Details
        /// <summary>
        /// To Select All data from the tbl_Vendor_M table
        /// Created By : Chintan, 10/28/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Vendor_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Vendor_M_SelectAll";
                DataTable dtVendor = new DataTable();
                dtVendor = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtVendor);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Vendor Details by VendorID
        /// <summary>
        /// Select all details of Vendor for selected VendorID from tbl_Vendor_M table
        /// Created By : Chintan, 10/28/2014
        /// Modified By :
        /// </summary>
        /// <param name="intVendorID"></param>
        /// <returns></returns>
        public ApplicationResult Vendor_Select(int intVendorID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@VendorID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intVendorID;

                strStoredProcName = "usp_tbl_Vendor_M_Select";

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

        #region Delete Vendor Details by VendorID
        /// <summary>
        /// To Delete details of Vendor for selected VendorID from tbl_Vendor_M table
        /// Created By : Chintan, 10/28/2014
        /// Modified By :
        /// </summary>
        /// <param name="intVendorID"></param>
        /// <returns></returns>
        public ApplicationResult Vendor_Delete(int intVendorID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@VendorID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intVendorID;

                strStoredProcName = "usp_tbl_Vendor_M_Delete";

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

        #region Insert Vendor Details
        /// <summary>
        /// To Insert details of Vendor in tbl_Vendor_M table
        /// Created By : Chintan, 10/28/2014
        /// Modified By :
        /// </summary>
        /// <param name="objVendorBO"></param>
        /// <returns></returns>
        public ApplicationResult Vendor_Insert(VendorBO objVendorBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[21];


                pSqlParameter[0] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objVendorBO.TrustMID;

                pSqlParameter[1] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objVendorBO.SchoolMID;

                pSqlParameter[2] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objVendorBO.VendorName;

                pSqlParameter[3] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objVendorBO.Address;

                pSqlParameter[4] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objVendorBO.TelephoneNo;

                pSqlParameter[5] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objVendorBO.MobileNo;

                pSqlParameter[6] = new SqlParameter("@Fax", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objVendorBO.Fax;

                pSqlParameter[7] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objVendorBO.EmailID;

                pSqlParameter[8] = new SqlParameter("@TINGST", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objVendorBO.TINGST;

                pSqlParameter[9] = new SqlParameter("@TINCST", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objVendorBO.TINCST;

                pSqlParameter[10] = new SqlParameter("@BankName", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objVendorBO.BankName;

                pSqlParameter[11] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objVendorBO.AccountNo;

                pSqlParameter[12] = new SqlParameter("@AccountName", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objVendorBO.AccountName;

                pSqlParameter[13] = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objVendorBO.IFSCCode;

                pSqlParameter[14] = new SqlParameter("@PANNO", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objVendorBO.PANNO;

                pSqlParameter[15] = new SqlParameter("@TaxRegNo", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objVendorBO.TaxRegNo;

                pSqlParameter[16] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objVendorBO.IsDeleted;

                pSqlParameter[17] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objVendorBO.CreatedDate;

                pSqlParameter[18] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objVendorBO.CreatedUserID;

                pSqlParameter[19] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objVendorBO.LastModifiedDate;

                pSqlParameter[20] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objVendorBO.LastModifiedUserID;


                sSql = "usp_tbl_Vendor_M_Insert";
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
                objVendorBO = null;
            }
        }
        #endregion

        #region Update Vendor Details
        /// <summary>
        /// To Update details of Vendor in tbl_Vendor_M table
        /// Created By : Chintan, 10/28/2014
        /// Modified By :
        /// </summary>
        /// <param name="objVendorBO"></param>
        /// <returns></returns>
        public ApplicationResult Vendor_Update(VendorBO objVendorBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[22];


                pSqlParameter[0] = new SqlParameter("@VendorID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objVendorBO.VendorID;

                pSqlParameter[1] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objVendorBO.TrustMID;

                pSqlParameter[2] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objVendorBO.SchoolMID;

                pSqlParameter[3] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objVendorBO.VendorName;

                pSqlParameter[4] = new SqlParameter("@Address", SqlDbType.VarChar);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objVendorBO.Address;

                pSqlParameter[5] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objVendorBO.TelephoneNo;

                pSqlParameter[6] = new SqlParameter("@MobileNo", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objVendorBO.MobileNo;

                pSqlParameter[7] = new SqlParameter("@Fax", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objVendorBO.Fax;

                pSqlParameter[8] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objVendorBO.EmailID;

                pSqlParameter[9] = new SqlParameter("@TINGST", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objVendorBO.TINGST;

                pSqlParameter[10] = new SqlParameter("@TINCST", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objVendorBO.TINCST;

                pSqlParameter[11] = new SqlParameter("@BankName", SqlDbType.VarChar);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objVendorBO.BankName;

                pSqlParameter[12] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = objVendorBO.AccountNo;

                pSqlParameter[13] = new SqlParameter("@AccountName", SqlDbType.VarChar);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = objVendorBO.AccountName;

                pSqlParameter[14] = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
                pSqlParameter[14].Direction = ParameterDirection.Input;
                pSqlParameter[14].Value = objVendorBO.IFSCCode;

                pSqlParameter[15] = new SqlParameter("@PANNO", SqlDbType.VarChar);
                pSqlParameter[15].Direction = ParameterDirection.Input;
                pSqlParameter[15].Value = objVendorBO.PANNO;

                pSqlParameter[16] = new SqlParameter("@TaxRegNo", SqlDbType.VarChar);
                pSqlParameter[16].Direction = ParameterDirection.Input;
                pSqlParameter[16].Value = objVendorBO.TaxRegNo;

                pSqlParameter[17] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[17].Direction = ParameterDirection.Input;
                pSqlParameter[17].Value = objVendorBO.IsDeleted;

                pSqlParameter[18] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[18].Direction = ParameterDirection.Input;
                pSqlParameter[18].Value = objVendorBO.CreatedDate;

                pSqlParameter[19] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[19].Direction = ParameterDirection.Input;
                pSqlParameter[19].Value = objVendorBO.CreatedUserID;

                pSqlParameter[20] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[20].Direction = ParameterDirection.Input;
                pSqlParameter[20].Value = objVendorBO.LastModifiedDate;

                pSqlParameter[21] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[21].Direction = ParameterDirection.Input;
                pSqlParameter[21].Value = objVendorBO.LastModifiedUserID;


                sSql = "usp_tbl_Vendor_M_Update";
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
                objVendorBO = null;
            }
        }
        #endregion

        #region Select All Vendor List with Material Name
        /// <summary>
        /// To Select All data from the tbl_Vendor_M table
        /// Created By : Chintan, 10/28/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Vendor_MaterialListReport()
        {
            try
            {
                sSql = "usp_tbl_Vendor_M_MaterialListReport";
                DataTable dtVendor = new DataTable();
                dtVendor = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtVendor);
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


