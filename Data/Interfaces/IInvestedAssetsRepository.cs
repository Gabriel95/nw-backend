using System;
using nw_api.Data.Entities;

namespace nw_api.Data.Interfaces
{
    public interface IInvestedAssetsRepository
    {
        public void InsertInvestedAssets(InvestedAssets investedAssets, Guid netWorthId);
        public InvestedAssets GetInvestedAssets(Guid netWorthId);
    }
}