using IssueNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Data
{
    public interface IDataProvider
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<bool> CreateNewUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> UpdateUser(User user);

    }
}
