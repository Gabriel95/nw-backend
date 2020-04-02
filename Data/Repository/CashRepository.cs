using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;

namespace nw_api.Data.Repository
{
    public class CashRepository : ICashRepository
    {private readonly IConfiguration _config;

        public CashRepository(IConfiguration config)
        {
            _config = config;
        }
        public void InsertCash(Cash cash, Guid netWorthId)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string insertCommand = "INSERT INTO cashandcashequivalents (id, checkingaccounts, savingsaccounts, moneymarketaccounts, savingbonds, cds, cashvalueoflifeinsurance, networthid)" + 
                " VALUES (@id, @checkingaccounts, @savingsaccounts, @moneymarketaccounts, @savingbonds, @cds, @cashvalueoflifeinsurance, @networthid)";
            using var cmd = new NpgsqlCommand(insertCommand, conn);
            cmd.Parameters.AddWithValue("id", cash.Id);
            cmd.Parameters.AddWithValue("checkingaccounts", cash.CheckingAccounts);
            cmd.Parameters.AddWithValue("savingsaccounts", cash.SavingsAccounts);
            cmd.Parameters.AddWithValue("moneymarketaccounts", cash.MoneyMarketAccounts);
            cmd.Parameters.AddWithValue("savingbonds", cash.SavingsBonds);
            cmd.Parameters.AddWithValue("cds", cash.CDs);
            cmd.Parameters.AddWithValue("cashvalueoflifeinsurance", cash.CashValueOfLifeInsurance);
            cmd.Parameters.AddWithValue("networthid", netWorthId);
            cmd.ExecuteNonQuery();
        }
    }
}