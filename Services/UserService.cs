using System;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;
using nw_api.Interfaces;
using nw_api.Models;

namespace nw_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByEmailAndPassword(UserLoginModel userLogin)
        {
            return _userRepository.GetByEmailAndPassword(userLogin.Email, userLogin.Password);
        }

        public User Insert(UserRegisterModel userRegisterModel)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = userRegisterModel.Email,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Password = userRegisterModel.Password
            };
            _userRepository.Insert(user);
            return user;
        }
    }
}