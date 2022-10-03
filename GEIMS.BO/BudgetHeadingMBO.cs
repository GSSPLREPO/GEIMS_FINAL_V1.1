using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class BudgetHeadingMBO
    {
        #region Budget Category Class Properties

        public const string BudgetHeading_TABLE = "tbl_BudgetHeading_M";
        public const string BudgetHeading_BUDGETHEADINGMID = "BudgetHeadingMID";
        public const string BudgetHeading_BUDGETCATEGORYMID = "BudgetCategoryMID";
        public const string BudgetHeading_HEADINGNAME = "HeadingName";
        public const string BudgetHeading_ISDELETED = "IsDeleted";
        public const string BudgetHeading_CREATEDBY = "CreatedBy";
        public const string BudgetHeading_CREATEDDATE = "CreatedDate";
        public const string BudgetHeading_MODIFIEDBY = "ModifiedBy";
        public const string BudgetHeading_MODIFIEDDATE = "ModifiedDate";

        private int intBudgetHeadingMID = 0;
        private int intBudgetCategoryMID = 0;
        private string strHeadingName = string.Empty;
        private int intIsDeleted = 0;
        private int intCreatedBy = 0;
        private DateTime strCreatedDate = DateTime.UtcNow;
        private int intModifiedBy = 0;
        private DateTime strLastModifiedDate = DateTime.UtcNow;

        #endregion

        #region  ---Properties---

        public int BudgetHeadingMID
        {
            get { return intBudgetHeadingMID; }
            set { intBudgetHeadingMID = value; }   
        }
        public int BudgetCategoryMID
        {
            get { return intBudgetCategoryMID; }
            set { intBudgetCategoryMID = value; }
        }
        public string HeadingName
        {
            get { return strHeadingName; }
            set { strHeadingName = value; }
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
