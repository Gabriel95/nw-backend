﻿using System;
using System.Collections.Generic;
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

        public IEnumerable<NetWorth> GetNetWorths(Guid userId, int amount)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string selectCommand = "SELECT id, userid, datecreated, total FROM networthreport " + 
                                         "WHERE userid = @userid ORDER BY datecreated desc LIMIT @amount";
            var listToReturn = new List<NetWorth>();
            using var cmd = new NpgsqlCommand(selectCommand, conn);
            cmd.Parameters.AddWithValue("userid", userId);
            cmd.Parameters.AddWithValue("amount", amount);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var networth = new NetWorth
                {
                    Id = reader.GetGuid(0),
                    UserId = reader.GetGuid(1),
                    DateTimeCreated = reader.GetDateTime(2),
                    Total = reader.GetDecimal(3)
                };
                listToReturn.Add(networth);
            }
            return listToReturn;
        }
        
        public NetWorth GetNetWorth(Guid userId, Guid netWorthId)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string selectCommand = "SELECT id, userid, datecreated, total FROM networthreport " + 
                                         "WHERE userid = @userid AND id = @id ORDER BY datecreated desc LIMIT 1";
            using var cmd = new NpgsqlCommand(selectCommand, conn);
            cmd.Parameters.AddWithValue("id", netWorthId);
            cmd.Parameters.AddWithValue("userid", userId);
            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return null;
            reader.Read();
            var networth = new NetWorth
            {
                Id = reader.GetGuid(0),
                UserId = reader.GetGuid(1),
                DateTimeCreated = reader.GetDateTime(2),
                Total = reader.GetDecimal(3)
            };
            return networth;
        }

        public IEnumerable<NetWorth> GetAllNetWorths(Guid userId)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string selectCommand = "SELECT id, userid, datecreated, total FROM networthreport " + 
                                         "WHERE userid = @userid ORDER BY datecreated desc";
            var listToReturn = new List<NetWorth>();
            using var cmd = new NpgsqlCommand(selectCommand, conn);
            cmd.Parameters.AddWithValue("userid", userId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var networth = new NetWorth
                {
                    Id = reader.GetGuid(0),
                    UserId = reader.GetGuid(1),
                    DateTimeCreated = reader.GetDateTime(2),
                    Total = reader.GetDecimal(3)
                };
                listToReturn.Add(networth);
            }
            return listToReturn;
        }

        public NetWorth DeleteNetWorth(Guid userId, Guid netWorthId)
        {
            var netWorth = GetNetWorth(userId, netWorthId);

            if (netWorth == null)
                return null;
            
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string deleteCommand = "DELETE FROM networthreport WHERE id = @id AND userid = @userId";
            
            using var cmd = new NpgsqlCommand(deleteCommand, conn);
            cmd.Parameters.AddWithValue("id", netWorthId);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.ExecuteNonQuery();

            return netWorth;
        }
    }
}