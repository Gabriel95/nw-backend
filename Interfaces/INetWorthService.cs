using System;
using System.Collections;
using System.Collections.Generic;
using nw_api.Data.Entities;
using nw_api.Models;

namespace nw_api.Interfaces
{
    public interface INetWorthService
    {
        public void AddNetWorth(NetWorthModel netWorthModel);
        public CurrentNetWorthModel GetCurrentNetWorth(Guid userId);
        public IEnumerable<NetWorth> GetAllNetWorths(Guid userId);
        public NetWorth DeleteNetWorth(Guid userId, Guid netWorthId);
    }
}