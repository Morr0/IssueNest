using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public interface IUserAuthService
    {
        bool Login(int email);
        bool Logout(int email);
    }

    public class UserAuthService : IUserAuthService
    {
        private HashSet<int> ids = new HashSet<int>();

        public bool Login(int id)
        {
            if (ids.Contains(id))
                return false;

            ids.Add(id);
            return true;
        }

        public bool Logout(int id)
        {
            if (ids.Contains(id))
                return ids.Remove(id);

            return false;
        }
    }
}
