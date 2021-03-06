﻿using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;

namespace nw_api.Data.Repository
{
    public class InvestedAssetsRepository : IInvestedAssetsRepository
    {
        private readonly IConfiguration _config;

        public InvestedAssetsRepository(IConfiguration config)
        {
            _config = config;
        }

        public void InsertInvestedAssets(InvestedAssets investedAssets, Guid netWorthId)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string insertCommand = "INSERT INTO investedassets (id, brokerage, ira, rothira, k401, sepira, keogh, pension, annuity, realestate, soleproprietorship, partnership, ccorporation, scorporation, limitedliabilitycompany, networthid)" + 
                                         " VALUES (@id, @brokerage, @ira, @rothira, @k401, @sepira, @keogh, @pension, @annuity, @realestate, @soleproprietorship, @partnership, @ccorporation, @scorporation, @limitedliabilitycompany, @networthid)";

            using var cmd = new NpgsqlCommand(insertCommand, conn);
            cmd.Parameters.AddWithValue("id", investedAssets.Id);
            cmd.Parameters.AddWithValue("brokerage", investedAssets.Brokerage);
            cmd.Parameters.AddWithValue("ira", investedAssets.Ira);
            cmd.Parameters.AddWithValue("rothira", investedAssets.RothIra);
            cmd.Parameters.AddWithValue("k401", investedAssets.K401);
            cmd.Parameters.AddWithValue("sepira", investedAssets.SepIra);
            cmd.Parameters.AddWithValue("keogh", investedAssets.Keogh);
            cmd.Parameters.AddWithValue("pension", investedAssets.Pension);
            cmd.Parameters.AddWithValue("annuity", investedAssets.Annuity);
            cmd.Parameters.AddWithValue("realestate", investedAssets.RealEstate);
            cmd.Parameters.AddWithValue("soleproprietorship", investedAssets.SoleProprietorship);
            cmd.Parameters.AddWithValue("partnership", investedAssets.Partnership);
            cmd.Parameters.AddWithValue("ccorporation", investedAssets.CCorporation);
            cmd.Parameters.AddWithValue("scorporation", investedAssets.SCorporation);
            cmd.Parameters.AddWithValue("limitedliabilitycompany", investedAssets.LimitedLiabilityCompany);
            cmd.Parameters.AddWithValue("networthid", netWorthId);
            cmd.ExecuteNonQuery();
        }

        public InvestedAssets GetInvestedAssets(Guid netWorthId)
        {
            const string selectCommand =
                "SELECT id, brokerage, ira, rothira, k401, sepira, keogh, pension, annuity, realestate, soleproprietorship, partnership, ccorporation, scorporation, limitedliabilitycompany" + 
                " FROM investedassets WHERE networthid = @networthid";
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            using var cmd = new NpgsqlCommand(selectCommand, conn);
            cmd.Parameters.AddWithValue("networthid", netWorthId);
            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return null;
            reader.Read();
            var investedAssets = new InvestedAssets
            {
                Id = reader.GetGuid(0),
                Brokerage = reader.GetDecimal(1),
                Ira = reader.GetDecimal(2),
                RothIra = reader.GetDecimal(3),
                K401 = reader.GetDecimal(4),
                SepIra = reader.GetDecimal(5),
                Keogh = reader.GetDecimal(6),
                Pension = reader.GetDecimal(7),
                Annuity = reader.GetDecimal(8),
                RealEstate = reader.GetDecimal(9),
                SoleProprietorship = reader.GetDecimal(10),
                Partnership = reader.GetDecimal(11),
                CCorporation = reader.GetDecimal(12),
                SCorporation = reader.GetDecimal(13),
                LimitedLiabilityCompany = reader.GetDecimal(14)
            };
            return investedAssets;
        }
    }
}