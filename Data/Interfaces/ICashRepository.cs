using System;
using nw_api.Data.Entities;

namespace nw_api.Data.Interfaces
{
    public interface ICashRepository
    {
        public void InsertCash(Cash cash, Guid netWorthId);
    }
}