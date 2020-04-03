namespace nw_api.Models
{
    public class LiabilitiesDetailModel
    {
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