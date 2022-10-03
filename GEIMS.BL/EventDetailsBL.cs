using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEIMS.BO;
using GEIMS.Common;
using GEIMS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace GEIMS.BL
{
    public class EventDetailsBL
    {
        #region User Defined Variables
        public SqlParameter[] pSqlParameter = null;
        public string strStoredProcName;
        #endregion

       /* #region Insert EventCategory Details
        ///<summary>
        ///To insert Details of EventCategory in tbl_EventCategories
        ///Created By Chirag, 09/28/2021
        ///Modifird BY :
        ///</summary>
        ///<param name="objEventCategoryBO"></param>
        ///<returns></returns>

        public ApplicationResult EventDetails_Insert(EventDetailsBO objEventDetailsBO)
        {
            pSqlParameter = new SqlParameter[4];

            pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = objEventDetailsBO.ScheduledEventID;

            pSqlParameter[1] = new SqlParameter("@EventDetailDescription", SqlDbType.VarChar);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = objEventDetailsBO.EventDetailDescription;

            pSqlParameter[2] = new SqlParameter("@CreatedUserID", SqlDbType.Int);
            pSqlParameter[2].Direction = ParameterDirection.Input;
            pSqlParameter[2].Value = objEventDetailsBO.CreatedUserID;

            pSqlParameter[3] = new SqlParameter("@CreatedUserDate", SqlDbType.VarChar);
            pSqlParameter[3].Direction = ParameterDirection.Input;
            pSqlParameter[3].Value = objEventDetailsBO.CreatedDate;

           

            strStoredProcName = "usp_tbl_EventDetails_Insert";

            int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
            if (iResult > 0)
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            else
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                return objResult;
            }
        }

        #endregion*/

        #region EventDetails Update
        public ApplicationResult EventDetails_Update(EventDetailsBO objEventDetailsBO)
        {
            pSqlParameter = new SqlParameter[1];

            pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = objEventDetailsBO.ScheduledEventID;

            strStoredProcName = "usp_tbl_EventDetails_Update";

            int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
            if (iResult > 0)
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            else
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                return objResult;
            }
        }
        #endregion

        #region EventDetails Images Insert
        public ApplicationResult EventDetailsImages_Insert(EventDetailsBO objEventDetailsBO)
        {
            pSqlParameter = new SqlParameter[2];

            pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = objEventDetailsBO.ScheduledEventID;

            pSqlParameter[1] = new SqlParameter("@ImageName", SqlDbType.VarChar);
            pSqlParameter[1].Direction = ParameterDirection.Input;
            pSqlParameter[1].Value = objEventDetailsBO.ImageName;

            strStoredProcName = "usp_tbl_EventDetails_InsertImages";

            int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
            if (iResult > 0)
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            else
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                return objResult;
            }
        }

        #endregion

        #region EventDetails Data Delete for image

        public ApplicationResult EventDetails_Delete(string ImageName)
        {
            pSqlParameter = new SqlParameter[1];

            pSqlParameter[0] = new SqlParameter("@ImageName", SqlDbType.VarChar);
            pSqlParameter[0].Direction = ParameterDirection.Input;
            pSqlParameter[0].Value = ImageName;

            strStoredProcName = "usp_tbl_EventDetails_Delete";

            int iResult = Database.ExecuteNonQuery(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
            if (iResult > 0)
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            else
            {
                ApplicationResult objResult = new ApplicationResult();
                objResult.status = ApplicationResult.CommonStatusType.FAILURE;
                return objResult;
            }
        }

        #endregion

        #region Fetch EventImages and Description in DataList using Scheduled Event ID from tbl_EventDetials Table
        public ApplicationResult EventDetails_Images_Select(int intScheduledEventID)
        {
            try
            {
                pSqlParameter = new SqlParameter[1];

                pSqlParameter[0] = new SqlParameter("@ScheduledEventID", SqlDbType.Int);
                pSqlParameter[0].Direction = ParameterDirection.Input;
                pSqlParameter[0].Value = intScheduledEventID;

                strStoredProcName = "usp_tbl_EventDetails_Select";

                DataTable dtResult = new DataTable();
                dtResult = Database.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName, pSqlParameter);
                ApplicationResult objResult = new ApplicationResult(dtResult);
                objResult.status = ApplicationResult.CommonStatusType.SUCCESS;
                return objResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
