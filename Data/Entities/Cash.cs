using System;

namespace nw_api.Data.Entities
{
    public class Cash
    {
        public Guid Id { get; set; }
        public decimal CheckingAccounts { get; set; }
        public decimal SavingsAccounts { get; set; }
        public decimal MoneyMarketAccounts { get; set; }
        public decimal SavingsBonds { get; set; }
        public decimal CDs { get; set; }
        public decimal CashValueOfLifeInsurance { get; set; }

        public decimal GetTotal()
        {
            return CheckingAccounts + SavingsAccounts + MoneyMarketAccounts + SavingsBonds + CDs +
                   CashValueOfLifeInsurance;
        }
    }
}