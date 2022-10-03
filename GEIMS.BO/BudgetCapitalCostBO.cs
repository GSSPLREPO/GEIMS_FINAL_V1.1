using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEIMS.BO
{
    public class BudgetCapitalCostBO
    {
        #region Budget Category Class Properties

        public const string BudgetCapitalCost_TABLE = "tbl_CapitalCost_M";
        public const string BudgetCapitalCostId_BUDGETCAPITALCOSTID = "CapitalCostId";
        public const string BudgetSectionMID_SECTIONMID = "SectionMID";
        public const string EMPLOYEE_TRUSTMID = "TrustMID";
        public const string EMPLOYEE_SCHOOLMID = "SchoolMID";
        public const string BudgetHeading_BUDGETCATEGORYMID = "BudgetCategoryMID";        
        public const string BudgetHeading_BUDGETHEADINGMID = "BudgetHeadingMID";
        public const string BudgetSubHeading_BUDGETSUBHEADINGMID = "BudgetSubHeadingMID"; 
        public const string BudgetQuantity_BUDGETQUANTITY = "Quantity";
        public const string BudgetUOMID_BUDGETUOMID = "UOMID";
        public const string BudgetRatePerUnit_BUDGETRATEPERUNIT = "RatePerUnit";
        public const string BudgetTotalAmount_BUDGETTOTALAMOUNT = "TotalAmount";
        public const string BudgetCapitalCost_CURRENTYEAR = "CurrentYear";
        public const string BudgetHeading_ISDELETED = "IsDeleted";
        public const string BudgetHeading_CREATEDBY = "CreatedBy";
        public const string BudgetHeading_CREATEDDATE = "CreatedDate";
        public const string BudgetHeading_MODIFIEDBY = "ModifiedBy";
        public const string BudgetHeading_MODIFIEDDATE = "ModifiedDate";

        private int intCapitalCostId = 0;
        private int intSectionMID = 0;
        private int intTrustMID = 0;
        private int intSchoolMID = 0;
        private int intBudgetCategoryMID = 0;
        private int intBudgetHeadingMID = 0;
        private int intBudgetSubHeadingMID = 0;
        private int intQuantity = 0;
        private int intUOMID = 0;
        private decimal decRatePerUnit = 0;
        private decimal decTotalAmount = 0;
        private string strCurrentYear = string.Empty;
        private int intIsDeleted = 0;
        private int intCreatedBy = 0;
        private DateTime strCreatedDate = DateTime.UtcNow;
        private int intModifiedBy = 0;
        private DateTime strLastModifiedDate = DateTime.UtcNow;

        #endregion

        #region  ---Properties---
        public int CapitalCostId
        {
            get { return intCapitalCostId; }
            set { intCapitalCostId = value; }
        }
        public int SectionMID
        {
            get { return intSectionMID; }
            set { intSectionMID = value; }
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
        public int Quantity
        {
            get { return intQuantity; }
            set { intQuantity = value; }
        }
        public int UOMID
        {
            get { return intUOMID; }
            set { intUOMID = value; }
        }
        public decimal RatePerUnit
        {
            get { return decRatePerUnit; }
            set { decRatePerUnit = value; }
        }
        public decimal TotalAmount
        {
            get { return decTotalAmount; }
            set { decTotalAmount = value; }
        }
        public string CurrentYear
        {
            get { return strCurrentYear; }
            set { strCurrentYear = value; }
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
