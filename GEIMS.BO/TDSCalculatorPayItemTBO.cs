using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class TDSCalculatorPayItemTBO
    {
        #region TDS CalculatorPayitem Class Properties

        public const string TDSCALCULATORPAYITEM_TABLE = "TDSCalculatorPayItem";
        public const string TDSCALCULATORPAYITEM_ID = "ID";
        public const string TDSCALCULATORPAYITEM_TDSCALCULATORSDETAILSID = "TDSCalculatorsDetailsID";
        public const string TDSCALCULATORPAYITEM_TDSCALCULATORSID = "TDSCalculatorsID";
        public const string TDSCALCULATORPAYITEM_EMPLOYEEMID = "EmployeeMID";
        public const string TDSCALCULATORPAYITEM_USERTEMPLATEID = "UserTemplateID";
        public const string TDSCALCULATORPAYITEM_USERPAYITEMTEMPLATEID = "UserPayItemTemplateID";
        public const string TDSCALCULATORPAYITEM_PAYITEMMID = "PayItemMID";
        public const string TDSCALCULATORPAYITEM_NAME = "Name"; //PayItemName
        public const string TDSCALCULATORPAYITEM_ACTUALAMOUNT = "ActualAmount";
        public const string TDSCALCULATORPAYITEM_NOOFMONTHSMULTIPLIEDAMOUNT = "NoOfMonthsMultipliedAmount";
        public const string TDSCALCULATORPAYITEM_IsDeleted = "IsDeleted";
        public const string TDSCALCULATORPAYITEM_CreatedUserID = "CreatedUserID";
        public const string TDSCALCULATORPAYITEM_CreatedDate = "CreatedDate";
        public const string TDSCALCULATORPAYITEM_LastModifiedUserID = "LastModifiedUserID";
        public const string TDSCALCULATORPAYITEM_LastModifiedDate = "LastModifiedDate";

        private int intID = 0;
        private int intTDSCalculatorsDetailsID = 0;
        private int intTDSCalculatorsID = 0;
        private int intEmployeeMID = 0;
        private int intUserTemplateID = 0;
        private int intUserPayItemTemplateID = 0;
        private int intPayItemMID = 0;
        private string strName = string.Empty; //PayItemName
        private double dbActualAmount = 0.00;
        private double dbNoOfMonthsMultipliedAmount = 0.00;
        private int intIsDeleted = 0;
        private int intCreatedUserID = 0;
        private string strCreatedDate = string.Empty;
        private int intLastModifiedUserID = 0;
        private string strLastModifiedDate = string.Empty;

        #endregion

        #region ---Properties---
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int TDSCalculatorsDetailsID
        {
            get { return intTDSCalculatorsDetailsID; }
            set { intTDSCalculatorsDetailsID = value; }
        }
        public int TDSCalculatorsID
        {
            get { return intTDSCalculatorsID; }
            set { intTDSCalculatorsID = value; }
        }
        public int EmployeeMID
        {
            get { return intEmployeeMID; }
            set { intEmployeeMID = value; }
        }
        public int UserTemplateID
        {
            get { return intUserTemplateID; }
            set { intUserTemplateID = value; }
        }
        public int UserPayItemTemplateID
        {
            get { return intUserPayItemTemplateID; }
            set { intUserPayItemTemplateID = value; }
        }

        public int PayItemMID
        {
            get { return intPayItemMID; }
            set { intPayItemMID = value; }
        }
        public string Name //PayItemName
        {
            get { return strName; }
            set { strName = value; }
        }
        public double ActualAmount
        {
            get { return dbActualAmount; }
            set { dbActualAmount = value; }
        }
        public double NoOfMonthsMultipliedAmount
        {
            get { return dbNoOfMonthsMultipliedAmount; }
            set { dbNoOfMonthsMultipliedAmount = value; }
        }
        public string CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int CreatedUserID
        {
            get { return intCreatedUserID; }
            set { intCreatedUserID = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        public int LastModifideUserID
        {
            get { return intLastModifiedUserID; }
            set { intLastModifiedUserID = value; }
        }
        public string LastModifideDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }
        #endregion
    }
}
