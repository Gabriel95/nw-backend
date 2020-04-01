using nw_api.Data.Entities;

namespace nw_api.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}