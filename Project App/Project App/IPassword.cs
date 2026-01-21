using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_App
{
    public interface IPassword
    {
        public string HashPassword(string password);
        public bool VerifyHashedPassword(string hashedPassword, string password);
        
    }
}
