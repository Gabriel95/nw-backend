using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;

namespace nw_api.Data.Repository
{
    public class LiabilitiesRepository : ILiabilitiesRepository
    {
        private readonly IConfiguration _config;

        public LiabilitiesRepository(IConfiguration config)
        {
            _config = config;
        }

        public void InsertLiabilities(Liabilities liabilities, Guid netWorthId)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string insertCommand = "INSERT INTO liabilities (id, creditcardbalances, estimatedincometaxowed, otheroutstandingbills, homemortgage, " + 
                                         "homeequityloan, mortgagesonrentalproperties, carloans, studentloans, lifeinsurancepolicyloans, otherlongtermdebt, networthid)" + 
                                         " VALUES (@id, @creditcardbalances, @estimatedincometaxowed, @otheroutstandingbills, @homemortgage, @homeequityloan, @mortgagesonrentalproperties, @carloans, @studentloans, @lifeinsurancepolicyloans, @otherlongtermdebt, @networthid)";
            using var cmd = new NpgsqlCommand(insertCommand, conn);
            cmd.Parameters.AddWithValue("id", liabilities.Id);
            cmd.Parameters.AddWithValue("creditcardbalances", liabilities.CreditCardBalances);
            cmd.Parameters.AddWithValue("estimatedincometaxowed", liabilities.EstimatedIncomeTaxOwed);
            cmd.Parameters.AddWithValue("otheroutstandingbills", liabilities.OtherOutstandingBills);
            cmd.Parameters.AddWithValue("homemortgage", liabilities.HomeMortgage);
            cmd.Parameters.AddWithValue("homeequityloan", liabilities.HomeEquityLoan);
            cmd.Parameters.AddWithValue("mortgagesonrentalproperties", liabilities.MortgagesOnRentalProperties);
            cmd.Parameters.AddWithValue("carloans", liabilities.CarLoans);
            cmd.Parameters.AddWithValue("studentloans", liabilities.StudentLoans);
            cmd.Parameters.AddWithValue("lifeinsurancepolicyloans", liabilities.LifeInsurancePolicyLoans);
            cmd.Parameters.AddWithValue("otherlongtermdebt", liabilities.OtherLongTermDebt);
            cmd.Parameters.AddWithValue("networthid", netWorthId);
            cmd.ExecuteNonQuery();
        }
    }
}