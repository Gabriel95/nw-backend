using nw_api.Data.Entities;

namespace nw_api.Data.Interfaces
{
    public interface IUserRepository
    {
        public User GetByEmailAndPassword(string email, string password);
        public void Insert(User user);
    }
}