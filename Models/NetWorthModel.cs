using System;

namespace nw_api.Models
{
    public class NetWorthModel
    {
        public Guid UserId { get; set; }
        //Cash and Cash Equivalents
        public decimal CheckingAccounts { get; set; }
        public decimal SavingsAccounts { get; set; }
        public decimal MoneyMarketAccounts { get; set; }
        public decimal SavingsBonds { get; set; }
        public decimal CDs { get; set; }
        public decimal CashValueOfLifeInsurance { get; set; }
        
        //Invested Assets
        public decimal Brokerage { get; set; }
        public decimal Ira { get; set; }
        public decimal RothIra { get; set; }
        public decimal K401 { get; set; }
        public decimal SepIra { get; set; }
        public decimal Keogh { get; set; }
        public decimal Pension { get; set; }
        public decimal Annuity { get; set; }
        public decimal RealEstate { get; set; }
        public decimal SoleProprietorship { get; set; }
        public decimal Partnership { get; set; }
        public decimal CCorporation { get; set; }
        public decimal SCorporation { get; set; }
        public decimal LimitedLiabilityCompany { get; set; }
        
        //Use Assets
        public decimal PrincipalHome { get; set; }
        public decimal VacationHome { get; set; }
        public decimal CarsTrucksBoats { get; set; }
        public decimal HomeFurnishings { get; set; }
        public decimal ArtAntiquesCoinsCollectibles { get; set; }
        public decimal JewelryFurs { get; set; }
        
        //Liabilities
        public decimal CreditCardBalances { get; set; }
        public decimal EstimatedIncomeTaxOwed { get; set; }
        public decimal OtherOutstandingBills { get; set; }
        public decimal HomeMortgage { get; set; }
        public decimal HomeEquityLoan { get; set; }
        public decimal MortgagesOnRentalProperties { get; set; }
        public decimal CarLoans { get; set; }
        public decimal StudentLoans { get; set; }
        public decimal LifeInsurancePolicyLoans { get; set; }
        public decimal OtherLongTermDebt { get; set; }
    }
}