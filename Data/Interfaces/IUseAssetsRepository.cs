using System;
using nw_api.Data.Entities;

namespace nw_api.Data.Interfaces
{
    public interface IUseAssetsRepository
    {
        public void InsertUseAssets(UseAssets useAssets, Guid netWorthId);
    }
}