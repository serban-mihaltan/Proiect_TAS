using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_App
{
    public interface IUserDB
    {
        public bool AddUser(User user);
        public bool UpdateUser(User user);
        public User? FetchUser(string userName);
        public bool CheckUsername(string username);
    }
}
