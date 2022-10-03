using System.Data;

namespace GEIMS.Common
{
    /// <summary>
    /// Summary description for Application Result.
    /// </summary>
    public class ApplicationResult
    {

        public CommonStatusType status;
        public string errorException;
        public DataTable resultDT;
        public DataSet resutlDS;
        public object resultObj;
        public int resultInt = 0;


        public enum CommonStatusType
        {
            FAILURE = 0,
            SUCCESS = 1,
            RECORD = 2,
            RECORD_EXISTS = 3,
            RECORD_NOTEXISTS = 4,
            RECORD_FK_VIOLATION = 5

        }


        public ApplicationResult()
        {
            status = CommonStatusType.SUCCESS;
            errorException = null;
            resultDT = null;
            resutlDS = null;
            resultObj = null;
            resultInt = 0;
        }


        public ApplicationResult(DataTable dt)
        {
            //
            // TODO: Add constructor logic here
            //
            status = CommonStatusType.SUCCESS;
            errorException = null;
            resultDT = dt;
            resultObj = null;
            resultInt = 0;
        }

        public ApplicationResult(DataSet ds)
        {
            //
            // TODO: Add constructor logic here
            //
            status = CommonStatusType.SUCCESS;
            errorException = null;
            //resultDT = dt;
            resutlDS = ds;
            resultObj = null;
            resultInt = 0;
        }


        public ApplicationResult(string errMsg)
        {
            //
            // TODO: Add constructor logic here
            //
            status = CommonStatusType.FAILURE;
            errorException = errMsg;
            resultDT = null;
            resutlDS = null;
            resultObj = null;
            resultInt = 0;

        }

        public ApplicationResult(int integerresult)
        {
            //
            // TODO: Add constructor logic here
            //
            status = CommonStatusType.SUCCESS;
            errorException = null;
            resultDT = null;
            resutlDS = null;
            resultObj = null;
            resultInt = integerresult;
        }

        public ApplicationResult(object objResult)
        {
            status = CommonStatusType.SUCCESS;
            errorException = null;
            resultDT = null;
            resutlDS = null;
            resultObj = objResult;
            resultInt = 0;
        }
    }
}
