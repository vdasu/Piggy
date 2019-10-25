using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piggy
{
    public class User
    {
        public string userName { get; private set; }
        public string password { get; private set; }
        public bool isAdmin { get; private set; }

        public int userId { get; private set; }

        public User(int userId, string userName, string password, bool isAdmin)
        {
            this.userId = userId;
            this.userName = userName;
            this.password = password;
            this.isAdmin = isAdmin;
        }
    }
}