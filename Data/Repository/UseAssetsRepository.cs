using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;

namespace nw_api.Data.Repository
{
    public class UseAssetsRepository : IUseAssetsRepository
    {
        private readonly IConfiguration _config;

        public UseAssetsRepository(IConfiguration config)
        {
            _config = config;
        }

        public void InsertUseAssets(UseAssets useAssets, Guid netWorthId)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string insertCommand = "INSERT INTO useassets (id, principalhome, vacationhome, carstrucksboats, homefurnishing, artsantiquescoinscollectibles, jewelryfurs, networthid)" + 
                                         " VALUES (@id, @principalhome, @vacationhome, @carstrucksboats, @homefurnishing, @artsantiquescoinscollectibles, @jewelryfurs, @networthid)";
            using var cmd = new NpgsqlCommand(insertCommand, conn);
            cmd.Parameters.AddWithValue("id", useAssets.Id);
            cmd.Parameters.AddWithValue("principalhome", useAssets.PrincipalHome);
            cmd.Parameters.AddWithValue("vacationhome", useAssets.VacationHome);
            cmd.Parameters.AddWithValue("carstrucksboats", useAssets.CarsTrucksBoats);
            cmd.Parameters.AddWithValue("homefurnishing", useAssets.HomeFurnishings);
            cmd.Parameters.AddWithValue("artsantiquescoinscollectibles", useAssets.ArtAntiquesCoinsCollectibles);
            cmd.Parameters.AddWithValue("jewelryfurs", useAssets.JewelryFurs);
            cmd.Parameters.AddWithValue("networthid", netWorthId);
            cmd.ExecuteNonQuery();
        }

        public UseAssets GetUseAssets(Guid netWorthId)
        {
            const string selectCommand =
                "SELECT id, principalhome, vacationhome, carstrucksboats, homefurnishing, artsantiquescoinscollectibles, jewelryfurs" + 
                " FROM useassets WHERE networthid = @networthid";
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            using var cmd = new NpgsqlCommand(selectCommand, conn);
            cmd.Parameters.AddWithValue("networthid", netWorthId);
            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return null;
            reader.Read();
            var useAssets = new UseAssets
            {
                Id = reader.GetGuid(0),
                PrincipalHome = reader.GetDecimal(1),
                VacationHome = reader.GetDecimal(2),
                CarsTrucksBoats = reader.GetDecimal(3),
                HomeFurnishings = reader.GetDecimal(4),
                ArtAntiquesCoinsCollectibles = reader.GetDecimal(5),
                JewelryFurs = reader.GetDecimal(6)
            };
            return useAssets;
        }
    }
}