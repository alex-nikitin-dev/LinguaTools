using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj
{
    public class Credentials
    {
        public string UserName;
        public string Password;

        public Credentials(string userName, string password)
        {
            Password = password;
            UserName = userName;
        }
    }
}
