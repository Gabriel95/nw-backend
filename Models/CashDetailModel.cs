namespace nw_api.Models
{
    public class CashDetailModel
    {
        public decimal CheckingAccounts { get; set; }
        public decimal SavingsAccounts { get; set; }
        public decimal MoneyMarketAccounts { get; set; }
        public decimal SavingsBonds { get; set; }
        public decimal CDs { get; set; }
        public decimal CashValueOfLifeInsurance { get; set; }
    }
}