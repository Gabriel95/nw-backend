using nw_api.Data.Entities;
using nw_api.Models;

namespace nw_api.Interfaces
{
    public interface IUserService
    {
        public User GetUserByEmailAndPassword(UserLoginModel userLogin);
        public User Insert(UserRegisterModel userRegisterModel);
    }
}