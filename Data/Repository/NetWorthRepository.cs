using Microsoft.Extensions.Configuration;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;

namespace nw_api.Data.Repository
{
    public class NetWorthRepository : INetWorthRepository
    {
        private readonly IConfiguration _config;

        public NetWorthRepository(IConfiguration config)
        {
            _config = config;
        }

        public void InsertNetWorth(NetWorth netWorth)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string insertCommand = "INSERT INTO networthreport (id, userid, datecreated, total)" + 
                                         " VALUES (@id, @userid,  @datecreated, @total)";
            using var cmd = new NpgsqlCommand(insertCommand, conn);
            cmd.Parameters.AddWithValue("id", netWorth.Id);
            cmd.Parameters.AddWithValue("userid", netWorth.UserId);
            cmd.Parameters.AddWithValue("datecreated", netWorth.DateTimeCreated);
            cmd.Parameters.AddWithValue("total", netWorth.Total);
            cmd.ExecuteNonQuery();
        }
    }
}