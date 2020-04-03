using System;
using System.Collections;
using System.Collections.Generic;
using nw_api.Data.Entities;
using nw_api.Models;

namespace nw_api.Data.Interfaces
{
    public interface INetWorthRepository
    {
        public void InsertNetWorth(NetWorth netWorth);
        public IEnumerable<NetWorth> GetNetWorths(Guid userId, int amount);
    }
}