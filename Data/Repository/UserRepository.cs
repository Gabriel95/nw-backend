using Microsoft.Extensions.Configuration;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;
using nw_api.Interfaces;

namespace nw_api.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Encryption _encryption;
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _encryption = new Encryption();
            _config = config;
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string selectCommand = "SELECT id, email, firstname, lastname, password, salt FROM users WHERE email = @email LIMIT 1";
            using (var cmd = new NpgsqlCommand(selectCommand, conn))
            {
                cmd.Parameters.AddWithValue("email", email);
                using var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    return null;
                reader.Read();
                var user = new User
                {
                    Id = reader.GetGuid(0),
                    Email = reader.GetString(1),
                    FirstName = reader.GetString(2),
                    LastName = reader.GetString(3),
                    Password = reader.GetString(4),
                    Salt = new byte[128 / 8]
                };
                reader.GetBytes(5, 0, user.Salt, 0, 128 / 8);
                var passwordToValidate = _encryption.GetHash(password, user.Salt);
                if (passwordToValidate.Equals(user.Password))
                    return user;
            }
            return null;
        }

        public void Insert(User user)
        {
            var salt = _encryption.GetSalt();
            user.Salt = salt;
            user.Password = _encryption.GetHash(user.Password, user.Salt);
            
            using var conn = new NpgsqlConnection(_config["ConnectionString"]);
            conn.Open();
            const string insertCommand =
                "INSERT INTO users (id, email, password, salt, firstname, lastname) VALUES (@id, @email, @password, @salt, @firstname, @lastname)";
            using var cmd = new NpgsqlCommand(insertCommand, conn);
            cmd.Parameters.AddWithValue("id", user.Id);
            cmd.Parameters.AddWithValue("email", user.Email);
            cmd.Parameters.AddWithValue("password", user.Password);
            cmd.Parameters.AddWithValue("salt", user.Salt);
            cmd.Parameters.AddWithValue("firstname", user.FirstName);
            cmd.Parameters.AddWithValue("lastname", user.LastName);
            cmd.ExecuteNonQuery();
        }
    }
}