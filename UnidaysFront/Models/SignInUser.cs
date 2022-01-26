using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnidaysFront.Models
{
    public class SignInUser
    {
        public SignInUser(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
