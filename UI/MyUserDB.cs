using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_App;
namespace UI
{
    class MyUserDB:IUserDB
    {
        List<User> users=new List<User>();
        public bool AddUser(User user) { users.Add(user); 
                                         return true;
        }
        public bool UpdateUser(User user)
        {
            return false;
        }
        public User? FetchUser(string userName)
        {
            foreach(User user in users)
            {
                if(user.UserName == userName) return user;
            }
            return null;
        }
        public bool CheckUsername(string username)
        {
            foreach (User user in users)
            {
                if (user.UserName == username) return true;
            }
            return false;
        }
    }
}
