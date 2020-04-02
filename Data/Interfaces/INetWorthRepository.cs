using nw_api.Data.Entities;

namespace nw_api.Data.Interfaces
{
    public interface INetWorthRepository
    {
        public void InsertNetWorth(NetWorth netWorth);
    }
}