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
    /// Class Created By : Chintan, 10/31/2014
	/// Summary description for Organisation.
    /// </summary>
	public class PurchaseBL 
	{
		#region user defined variables
        public string sSql;
        public string strStoredProcName;
        public SqlParameter[] pSqlParameter = null;
        #endregion
		
		
		#region Select All Purchase Details
        /// <summary>
        /// To Select All data from the tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
		/// Modified By :
        /// </summary>
		/// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult  Purchase_SelectAll()
        {
			try
            {
				sSql = "usp_tbl_Purchase_M_SelectAll";

                DataTable dtPurchase  = new DataTable();
                dtPurchase = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPurchase);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
		
		#region Select Purchase Details by PurchaseID
        /// <summary>
        /// Select all details of Purchase for selected PurchaseID from tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
		/// Modified By :
        /// </summary>
        /// <param name="intPurchaseID"></param>
        /// <returns></returns>
		public ApplicationResult Purchase_Select(string strFromDate, string strToDate, int intFlag, int intPurchaseID, int intTrustMID, int intSchoolMID)
		{
            try
            {
                pSqlParameter = new SqlParameter[6];

                pSqlParameter[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = strFromDate;

                pSqlParameter[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = strToDate;

                pSqlParameter[2] = new SqlParameter("@Flag", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = intFlag;

				pSqlParameter[3] = new SqlParameter("@PurchaseID", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = intPurchaseID;

                pSqlParameter[4] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = intTrustMID;

                pSqlParameter[5] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = intSchoolMID;

				strStoredProcName = "usp_tbl_Purchase_M_Select";

                DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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
		
		#region Delete Purchase Details by PurchaseID
        /// <summary>
        /// To Delete details of Purchase for selected PurchaseID from tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
		/// Modified By :
        /// </summary>
        /// <param name="intPurchaseID"></param>
        /// <returns></returns>
		public ApplicationResult Purchase_Delete(int intPurchaseID)
		{
            try
            {
                pSqlParameter = new SqlParameter[1];
				
				pSqlParameter[0] = new SqlParameter("@PurchaseID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPurchaseID;

				strStoredProcName = "usp_tbl_Purchase_M_Delete";

                DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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
		
		#region Insert Purchase Details
		/// <summary>
        /// To Insert details of Purchase in tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
		/// Modified By :
        /// </summary>
        /// <param name="objPurchaseBO"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_Insert(PurchaseBO objPurchaseBO)
        {
            try
            {
				pSqlParameter = new SqlParameter[21];
                
				
          		pSqlParameter[0] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objPurchaseBO.TrustMID;
 
				pSqlParameter[1] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objPurchaseBO.SchoolMID;
 
				pSqlParameter[2] = new SqlParameter("@VendorID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objPurchaseBO.VendorID;
 
				pSqlParameter[3] = new SqlParameter("@PONO",SqlDbType.VarChar);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objPurchaseBO.PONO;
 
				pSqlParameter[4] = new SqlParameter("@PODate",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objPurchaseBO.PODate;
 
				pSqlParameter[5] = new SqlParameter("@BillNO",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objPurchaseBO.BillNO;
 
				pSqlParameter[6] = new SqlParameter("@BillDate",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPurchaseBO.BillDate;

                pSqlParameter[7] = new SqlParameter("@VoucharNO", SqlDbType.VarChar);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPurchaseBO.VoucharNO;

                pSqlParameter[8] = new SqlParameter("@VoucharDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPurchaseBO.VoucharDate;
 
				pSqlParameter[9] = new SqlParameter("@PaymentType",SqlDbType.Char);
				pSqlParameter[9].Direction = ParameterDirection.Input;
          		pSqlParameter[9].Value = objPurchaseBO.PaymentType;
 
				pSqlParameter[10] = new SqlParameter("@PaymentMode",SqlDbType.VarChar);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objPurchaseBO.PaymentMode;
 
				pSqlParameter[11] = new SqlParameter("@VAT",SqlDbType.Float);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objPurchaseBO.VAT;
 
				pSqlParameter[12] = new SqlParameter("@AddVAT",SqlDbType.Float);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objPurchaseBO.AddVAT;
 
				pSqlParameter[13] = new SqlParameter("@CST",SqlDbType.Float);
				pSqlParameter[13].Direction = ParameterDirection.Input;
          		pSqlParameter[13].Value = objPurchaseBO.CST;
 
				pSqlParameter[14] = new SqlParameter("@TotalAmount",SqlDbType.Float);
				pSqlParameter[14].Direction = ParameterDirection.Input;
          		pSqlParameter[14].Value = objPurchaseBO.TotalAmount;
 
				pSqlParameter[15] = new SqlParameter("@Discount",SqlDbType.Float);
				pSqlParameter[15].Direction = ParameterDirection.Input;
          		pSqlParameter[15].Value = objPurchaseBO.Discount;
 
				pSqlParameter[16] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[16].Direction = ParameterDirection.Input;
          		pSqlParameter[16].Value = objPurchaseBO.IsDeleted;
 
				pSqlParameter[17] = new SqlParameter("@CreatedDate",SqlDbType.VarChar);
				pSqlParameter[17].Direction = ParameterDirection.Input;
          		pSqlParameter[17].Value = objPurchaseBO.CreatedDate;
 
				pSqlParameter[18] = new SqlParameter("@CreatedUserID",SqlDbType.Int);
				pSqlParameter[18].Direction = ParameterDirection.Input;
          		pSqlParameter[18].Value = objPurchaseBO.CreatedUserID;
 
				pSqlParameter[19] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[19].Direction = ParameterDirection.Input;
          		pSqlParameter[19].Value = objPurchaseBO.LastModifiedDate;
 
				pSqlParameter[20] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[20].Direction = ParameterDirection.Input;
          		pSqlParameter[20].Value = objPurchaseBO.LastModifiedUserID;

				sSql = "usp_tbl_Purchase_M_Insert";

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
                objPurchaseBO = null;
            }
        }
        #endregion
		
		#region Update Purchase Details
		/// <summary>
        /// To Update details of Purchase in tbl_Purchase_M table
        /// Created By : Chintan, 10/31/2014
		/// Modified By :
        /// </summary>
        /// <param name="objPurchaseBO"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_Update(PurchaseBO objPurchaseBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[20];
                
				
          		pSqlParameter[0] = new SqlParameter("@PurchaseID",SqlDbType.Int);
				pSqlParameter[0].Direction = ParameterDirection.Input;
          		pSqlParameter[0].Value = objPurchaseBO.PurchaseID;
 
				pSqlParameter[1] = new SqlParameter("@TrustMID",SqlDbType.Int);
				pSqlParameter[1].Direction = ParameterDirection.Input;
          		pSqlParameter[1].Value = objPurchaseBO.TrustMID;
 
				pSqlParameter[2] = new SqlParameter("@SchoolMID",SqlDbType.Int);
				pSqlParameter[2].Direction = ParameterDirection.Input;
          		pSqlParameter[2].Value = objPurchaseBO.SchoolMID;
 
				pSqlParameter[3] = new SqlParameter("@VendorID",SqlDbType.Int);
				pSqlParameter[3].Direction = ParameterDirection.Input;
          		pSqlParameter[3].Value = objPurchaseBO.VendorID;
 
				pSqlParameter[4] = new SqlParameter("@PONO",SqlDbType.VarChar);
				pSqlParameter[4].Direction = ParameterDirection.Input;
          		pSqlParameter[4].Value = objPurchaseBO.PONO;
 
				pSqlParameter[5] = new SqlParameter("@PODate",SqlDbType.VarChar);
				pSqlParameter[5].Direction = ParameterDirection.Input;
          		pSqlParameter[5].Value = objPurchaseBO.PODate;
 
				pSqlParameter[6] = new SqlParameter("@BillNO",SqlDbType.VarChar);
				pSqlParameter[6].Direction = ParameterDirection.Input;
          		pSqlParameter[6].Value = objPurchaseBO.BillNO;
 
				pSqlParameter[7] = new SqlParameter("@BillDate",SqlDbType.VarChar);
				pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPurchaseBO.BillDate;

                pSqlParameter[8] = new SqlParameter("@VoucharNO", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPurchaseBO.VoucharNO;

                pSqlParameter[9] = new SqlParameter("@VoucharDate", SqlDbType.VarChar);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPurchaseBO.VoucharDate;
 
				pSqlParameter[10] = new SqlParameter("@PaymentType",SqlDbType.Char);
				pSqlParameter[10].Direction = ParameterDirection.Input;
          		pSqlParameter[10].Value = objPurchaseBO.PaymentType;
 
				pSqlParameter[11] = new SqlParameter("@PaymentMode",SqlDbType.VarChar);
				pSqlParameter[11].Direction = ParameterDirection.Input;
          		pSqlParameter[11].Value = objPurchaseBO.PaymentMode;

				pSqlParameter[12] = new SqlParameter("@VAT",SqlDbType.Float);
				pSqlParameter[12].Direction = ParameterDirection.Input;
          		pSqlParameter[12].Value = objPurchaseBO.VAT;
 
				pSqlParameter[13] = new SqlParameter("@AddVAT",SqlDbType.Float);
				pSqlParameter[13].Direction = ParameterDirection.Input;
          		pSqlParameter[13].Value = objPurchaseBO.AddVAT;
 
				pSqlParameter[14] = new SqlParameter("@CST",SqlDbType.Float);
				pSqlParameter[14].Direction = ParameterDirection.Input;
          		pSqlParameter[14].Value = objPurchaseBO.CST;
 
				pSqlParameter[15] = new SqlParameter("@TotalAmount",SqlDbType.Float);
				pSqlParameter[15].Direction = ParameterDirection.Input;
          		pSqlParameter[15].Value = objPurchaseBO.TotalAmount;
 
				pSqlParameter[16] = new SqlParameter("@Discount",SqlDbType.Float);
				pSqlParameter[16].Direction = ParameterDirection.Input;
          		pSqlParameter[16].Value = objPurchaseBO.Discount;

				pSqlParameter[17] = new SqlParameter("@IsDeleted",SqlDbType.Int);
				pSqlParameter[17].Direction = ParameterDirection.Input;
          		pSqlParameter[17].Value = objPurchaseBO.IsDeleted;
 
				pSqlParameter[18] = new SqlParameter("@LastModifiedDate",SqlDbType.VarChar);
				pSqlParameter[18].Direction = ParameterDirection.Input;
          		pSqlParameter[18].Value = objPurchaseBO.LastModifiedDate;
 
				pSqlParameter[19] = new SqlParameter("@LastModifiedUserID",SqlDbType.Int);
				pSqlParameter[19].Direction = ParameterDirection.Input;
          		pSqlParameter[19].Value = objPurchaseBO.LastModifiedUserID;

				sSql = "usp_tbl_Purchase_M_Update";

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
                objPurchaseBO = null;
            }
        }
        #endregion

        #region Select All PurchaseT Details
        /// <summary>
        /// To Select All data from the tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_SelectAll()
        {
            try
            {
                sSql = "usp_tbl_Purchase_T_SelectAll";
                DataTable dtPurchase = new DataTable();
                dtPurchase = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, null);

                ApplicationResult objResults = new ApplicationResult(dtPurchase);
                objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select PurchaseT Details by PurchaseTID
        /// <summary>
        /// Select all details of Purchase for selected PurchaseTID from tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseTID"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_Select(int intPurchaseTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PurchaseTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPurchaseTID;

                strStoredProcName = "usp_tbl_Purchase_T_Select";

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

        #region Select all details of Purchase for selected PurchaseID from tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseTID"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_Select_PurchaseID(int intPurchaseID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PurchaseID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPurchaseID;

                strStoredProcName = "usp_tbl_Purchase_T_Select_PurchaseID";

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

        #region Delete PurchaseT Details by PurchaseTID
        /// <summary>
        /// To Delete details of Purchase for selected PurchaseTID from tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="intPurchaseTID"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_Delete(int intPurchaseTID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@PurchaseTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intPurchaseTID;

                strStoredProcName = "usp_tbl_Purchase_T_Delete";

                DataTable dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
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

        #region Insert PurchaseT Details
        /// <summary>
        /// To Insert details of Purchase in tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objPurchaseTBO"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_Insert(PurchaseTBO objPurchaseTBO, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@PurchaseID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPurchaseTBO.PurchaseID;

                pSqlParameter[1] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPurchaseTBO.UOMID;

                pSqlParameter[2] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPurchaseTBO.MaterialID;

                pSqlParameter[3] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPurchaseTBO.Quantity;

                pSqlParameter[4] = new SqlParameter("@Rate", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPurchaseTBO.Rate;

                pSqlParameter[5] = new SqlParameter("@TotalAmount", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPurchaseTBO.TotalAmount;

                pSqlParameter[6] = new SqlParameter("@DeliveryNote", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPurchaseTBO.DeliveryNote;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPurchaseTBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPurchaseTBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPurchaseTBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPurchaseTBO.LastModifiedDate;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objPurchaseTBO.LastModifiedUserID;

                pSqlParameter[12] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = intTrustMID;
                
                pSqlParameter[13] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = intSchoolMID;
                
                sSql = "usp_tbl_Purchase_T_Insert";

                //int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
                //}
                //else
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                //    return objResults;
                //}

                DataTable dtResult = new DataTable();
                dtResult = DatabaseTransaction.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    int intStatus = Convert.ToInt32(dtResult.Rows[0]["Status"].ToString());
                    if (intStatus == 1)
                    {
                        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;        
                    }
                    else
                    {
                        objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    }
                }
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objPurchaseTBO = null;
            }
        }
        #endregion

        #region Insert PurchaseT Details without Database Transaction
        /// <summary>
        /// To Insert details of Purchase in tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objPurchaseTBO"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_Insert_withoutTransaction(PurchaseTBO objPurchaseTBO, int intTrustMID, int intSchoolMID)
        {
            try
            {
                pSqlParameter = new SqlParameter[14];


                pSqlParameter[0] = new SqlParameter("@PurchaseID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPurchaseTBO.PurchaseID;

                pSqlParameter[1] = new SqlParameter("@UOMID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPurchaseTBO.UOMID;

                pSqlParameter[2] = new SqlParameter("@MaterialID", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPurchaseTBO.MaterialID;

                pSqlParameter[3] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPurchaseTBO.Quantity;

                pSqlParameter[4] = new SqlParameter("@Rate", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPurchaseTBO.Rate;

                pSqlParameter[5] = new SqlParameter("@TotalAmount", SqlDbType.Float);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPurchaseTBO.TotalAmount;

                pSqlParameter[6] = new SqlParameter("@DeliveryNote", SqlDbType.VarChar);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPurchaseTBO.DeliveryNote;

                pSqlParameter[7] = new SqlParameter("@IsDeleted", SqlDbType.Int);
                pSqlParameter[7].Direction = ParameterDirection.Input;
                pSqlParameter[7].Value = objPurchaseTBO.IsDeleted;

                pSqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.VarChar);
                pSqlParameter[8].Direction = ParameterDirection.Input;
                pSqlParameter[8].Value = objPurchaseTBO.CreatedDate;

                pSqlParameter[9] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
                pSqlParameter[9].Direction = ParameterDirection.Input;
                pSqlParameter[9].Value = objPurchaseTBO.CreatedUserID;

                pSqlParameter[10] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[10].Direction = ParameterDirection.Input;
                pSqlParameter[10].Value = objPurchaseTBO.LastModifiedDate;

                pSqlParameter[11] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[11].Direction = ParameterDirection.Input;
                pSqlParameter[11].Value = objPurchaseTBO.LastModifiedUserID;

                pSqlParameter[12] = new SqlParameter("@TrustMID", SqlDbType.Int);
                pSqlParameter[12].Direction = ParameterDirection.Input;
                pSqlParameter[12].Value = intTrustMID;

                pSqlParameter[13] = new SqlParameter("@SchoolMID", SqlDbType.Int);
                pSqlParameter[13].Direction = ParameterDirection.Input;
                pSqlParameter[13].Value = intSchoolMID;

                sSql = "usp_tbl_Purchase_T_Insert";

                //int iResult = DatabaseTransaction.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
                //}
                //else
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                //    return objResults;
                //}

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    int intStatus = Convert.ToInt32(dtResult.Rows[0]["Status"].ToString());
                    if (intStatus == 1)
                    {
                        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    }
                    else
                    {
                        objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    }
                }
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objPurchaseTBO = null;
            }
        }
        #endregion

        #region Update PurchaseT Details
        /// <summary>
        /// To Update details of Purchase in tbl_Purchase_T table
        /// Created By : Chintan, 10/31/2014
        /// Modified By :
        /// </summary>
        /// <param name="objPurchaseTBO"></param>
        /// <returns></returns>
        public ApplicationResult Purchase_T_Update(PurchaseTBO objPurchaseTBO)
        {
            try
            {
                pSqlParameter = new SqlParameter[7];

                pSqlParameter[0] = new SqlParameter("@PurchaseTID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = objPurchaseTBO.PurchaseTID;

                pSqlParameter[1] = new SqlParameter("@PurchaseID", SqlDbType.Int);
                pSqlParameter[1].Direction = ParameterDirection.Input;
                pSqlParameter[1].Value = objPurchaseTBO.PurchaseID;

                pSqlParameter[2] = new SqlParameter("@Quantity", SqlDbType.Int);
                pSqlParameter[2].Direction = ParameterDirection.Input;
                pSqlParameter[2].Value = objPurchaseTBO.Quantity;

                pSqlParameter[3] = new SqlParameter("@Rate", SqlDbType.Float);
                pSqlParameter[3].Direction = ParameterDirection.Input;
                pSqlParameter[3].Value = objPurchaseTBO.Rate;

                pSqlParameter[4] = new SqlParameter("@TotalAmount", SqlDbType.Float);
                pSqlParameter[4].Direction = ParameterDirection.Input;
                pSqlParameter[4].Value = objPurchaseTBO.TotalAmount;

                pSqlParameter[5] = new SqlParameter("@LastModifiedDate", SqlDbType.VarChar);
                pSqlParameter[5].Direction = ParameterDirection.Input;
                pSqlParameter[5].Value = objPurchaseTBO.LastModifiedDate;

                pSqlParameter[6] = new SqlParameter("@LastModifiedUserID", SqlDbType.Int);
                pSqlParameter[6].Direction = ParameterDirection.Input;
                pSqlParameter[6].Value = objPurchaseTBO.LastModifiedUserID;

                sSql = "usp_tbl_Purchase_T_Update";

                //int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, sSql, pSqlParameter);

                //if (iResult > 0)
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                //    return objResults;
                //}
                //else
                //{
                //    ApplicationResult objResults = new ApplicationResult();
                //    objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                //    return objResults;
                //}

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, sSql, pSqlParameter);
                ApplicationResult objResults = new ApplicationResult(dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    int intStatus = Convert.ToInt32(dtResult.Rows[0]["Status"].ToString());
                    if (intStatus == 1)
                    {
                        objResults.status = ApplicationResult.CommonStatusType.SUCCESS;
                    }
                    else
                    {
                        objResults.status = ApplicationResult.CommonStatusType.FAILURE;
                    }
                }
                return objResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objPurchaseTBO = null;
            }
        }
        #endregion
		
	}
}
		
		