using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class BudgetEntryScreenMBO
    {
        #region Budget Entry  Screen Master Properties

        public const string BudgetEntryScreen_TABLE = "tbl_BudgetEntryScreen_M";
        public const string BudgetScreenId_BUDGETSCREENID = "BudgetScreenId";       
        public const string BudgetEntryScreen_TRUSTMID = "TrustMID";
        public const string BudgetEntryScreen_SCHOOLMID = "SchoolMID";
        public const string BudgetEntryScreen_CURRENTYEAR = "CurrentYear";
        public const string BudgetCategory_BUDGETCATEGORYMID = "BudgetCategoryMID";
        public const string BudgetHeading_BUDGETHEADINGMID = "BudgetHeadingMID";
        public const string BudgetSubHeading_BUDGETSUBHEADINGMID = "BudgetSubHeadingMID";
        public const string BudgetAdminGeneralMGTRole_ADMINGENERALMGTROLE = "AdminGeneralMGTRole";
        public const string BudgetTotalAmount_BUDGETTOTALAMOUNT = "TotalAmount";
        public const string BudgetEntryScreen_ISDELETED = "IsDeleted";
        public const string BudgetEntryScreen_CREATEDBY = "CreatedBy";
        public const string BudgetEntryScreen_CREATEDDATE = "CreatedDate";
        public const string BudgetEntryScreen_MODIFIEDBY = "ModifiedBy";
        public const string BudgetEntryScreen_MODIFIEDDATE = "ModifiedDate";

        private int intBudgetScreenId = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private string strCurrentYear = string.Empty;
        private int intBudgetCategoryMID = 0;
        private int intBudgetHeadingMID = 0;
        private int intBudgetSubHeadingMID = 0;
        private decimal decAdminGeneralMGTRole = 0;
        private decimal decTotalAmount = 0;
        private int intIsDeleted = 0;
        private int intCreatedBy = 0;
        private DateTime strCreatedDate = DateTime.UtcNow;
        private int intModifiedBy = 0;
        private DateTime strLastModifiedDate = DateTime.UtcNow;

        #endregion

        #region  ---Properties---
        public int BudgetScreenId
        {
            get { return intBudgetScreenId; }
            set { intBudgetScreenId = value; }
        }
        public int TrustMID
        {
            get { return intTrustMID; }
            set { intTrustMID = value; }
        }
        public int SchoolMID
        {
            get { return intSchoolMID; }
            set { intSchoolMID = value; }
        }
        public string CurrentYear
        {
            get { return strCurrentYear; }
            set { strCurrentYear = value; }
        }
        public int BudgetCategoryMID
        {
            get { return intBudgetCategoryMID; }
            set { intBudgetCategoryMID = value; }
        }
        public int BudgetHeadingMID
        {
            get { return intBudgetHeadingMID; }
            set { intBudgetHeadingMID = value; }
        }
        public int BudgetSubHeadingMID
        {
            get { return intBudgetSubHeadingMID; }
            set { intBudgetSubHeadingMID = value; }
        }
        public decimal AdminGeneralMGTRole
        {
            get { return decAdminGeneralMGTRole; }
            set { decAdminGeneralMGTRole = value; }
        }
        public decimal TotalAmount
        {
            get { return decTotalAmount; }
            set { decTotalAmount = value; }
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
