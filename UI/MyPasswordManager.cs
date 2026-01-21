using Project_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UI
{
    public class MyPasswordManager : IPassword
    {
        public string HashPassword(string password) { return password; }
        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            return hashedPassword.Equals(HashPassword(password));
        }
    }
}
