using System;
using nw_api.Models;

namespace nw_api.Interfaces
{
    public interface INetWorthService
    {
        public void AddNetWorth(NetWorthModel netWorthModel);
        public CurrentNetWorthModel GetCurrentNetWorth(Guid userId);
    }
}