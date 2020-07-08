using IssueNest.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace IssueNest.Data
{
    public class DBDataProvider : IDataProvider
    {

        NpgsqlConnection connection;
        public DBDataProvider()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=root;Database=IssueNestDB";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        ~DBDataProvider()
        {
            connection.Close();
        }

        public async Task<bool> CreateNewUser(User user)
        {
            await using (var cmd = new NpgsqlCommand("INSERT INTO \"User\"(name, email, password) VALUES ('Hi', 'My', 'name');", connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }

            return true;
        }

        public async Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
