using System;
using nw_api.Data.Entities;

namespace nw_api.Data.Interfaces
{
    public interface ILiabilitiesRepository
    {
        public void InsertLiabilities(Liabilities liabilities, Guid netWorthId);
        public Liabilities GetLiabilities(Guid netWorthId);
    }
}