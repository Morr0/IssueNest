using IssueNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Data
{
    interface IDataProvider
    {
        List<User> GetUsers();
        User GetUserById(int id);
        bool CreateNewUser(User user);
        bool DeleteUser(User user);
        bool UpdateUser(User user);

    }
}
