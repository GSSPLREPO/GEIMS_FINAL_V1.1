using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GEIMS.BO
{
   public class BudgetCategoryMBO
    {
        #region Budget Category Class Properties

        public const string BudgetCategory_TABLE = "tbl_BudgetCategory_M";
        public const string BudgetCategory_BUDGETCATEGORYMID = "BudgetCategoryMID";
        public const string BudgetCategory_CATEGORYNAME = "CategoryName";    
        public const string BudgetCategory_ISDELETED = "IsDeleted";
        public const string BudgetCategory_CREATEDBY = "CreatedBy";
        public const string BudgetCategory_CREATEDDATE = "CreatedDate";
        public const string BudgetCategory_MODIFIEDBY = "ModifiedBy";
        public const string BudgetCategory_MODIFIEDDATE = "ModifiedDate";

        private int intBudgetCategoryMID = 0;
        private string strCategoryName = string.Empty;    
        private int intIsDeleted = 0;
        private int intCreatedBy = 0;
        private DateTime strCreatedDate = DateTime.UtcNow;
        private int intModifiedBy = 0;
        private DateTime strLastModifiedDate = DateTime.UtcNow;

        #endregion

        #region  ---Properties---

        public int BudgetCategoryMID
        {
            get { return intBudgetCategoryMID; }
            set { intBudgetCategoryMID = value; }
        }
        public string CategoryName
        {
            get { return strCategoryName; }
            set { strCategoryName = value; }
        }
        public int IsDeleted
        {
            get { return intIsDeleted; }
            set { intIsDeleted = value; }
        }
        public int CreatedBy
        {
            get { return intCreatedBy; }
            set { intCreatedBy = value; }
        }
        public DateTime CreatedDate
        {
            get { return strCreatedDate; }
            set { strCreatedDate = value; }
        }
        public int ModifiedBy
        {
            get { return intModifiedBy; }
            set { intModifiedBy = value; }
        }
        public DateTime ModifiedDate
        {
            get { return strLastModifiedDate; }
            set { strLastModifiedDate = value; }
        }
        #endregion
    }
}
