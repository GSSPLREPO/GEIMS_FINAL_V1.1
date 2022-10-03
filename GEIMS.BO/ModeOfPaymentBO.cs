using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class ModeOfPaymentBO
    {
        #region Class Class Properties

        public const string MODEOFPAYMENT_TABLE = "tbl_ModeOfPayment_M";
        public const string MODEOFPAYMENT_ID = "ID";
        public const string MODEOFPAYMENT_MODEOFPAYMENTNAME = "ModeOfPaymentName";

        private int intID = 0;
        private string strModeOfPaymentName = string.Empty;

        #endregion

        #region ---Properties---
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public string ModeOfPaymentName
        {
            get { return strModeOfPaymentName; }
            set { strModeOfPaymentName = value; }
        }

        #endregion
    }
}
