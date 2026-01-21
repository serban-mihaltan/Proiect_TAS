using System;
using System.Linq;

namespace Project_App
{
    public class UserOperations
    {
        public static void CreateNewUser(string userName,string password,string firstName,string lastName,string phoneNumber,IPassword passwordManager,IUserDB userDB)
        {
            CheckUserName(userName, userDB);
            CheckPassword(password);
            CheckName(firstName, lastName);
            CheckPhoneNumber(phoneNumber);
            var user = new User(userName,password,firstName,lastName,phoneNumber,passwordManager);
            userDB.AddUser(user);
        }

        public static bool Loggin(string userName,string password,IUserDB userDB,IPassword passwordManager)
        {
            var user = userDB.FetchUser(userName);
            if (user == null)
                throw new Exception("User not found");
            return user.VerifyPassword(password);
        }
        private static void CheckUserName(string userName, IUserDB userDB)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new Exception("Invalid username");

            if (userName.Length < 5)
                throw new Exception("Username too short");

            if (userName.Length > 12)
                throw new Exception("Username too long");

            if (userDB.CheckUsername(userName))
                throw new Exception("Username already in use");
        }

        private static void CheckPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Invalid password");

            if (password.Length < 8)
                throw new Exception("Password must contain at least 8 characters");

            if (password.Length > 20)
                throw new Exception("Password must be shorter than 20 characters");

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));

            if (!hasUpper)
                throw new Exception("Password must contain at least one uppercase character");

            if (!hasLower)
                throw new Exception("Password must contain at least one lowercase character");

            if (!hasDigit)
                throw new Exception("Password must contain at least one digit");

            if (!hasSpecial)
                throw new Exception("Password must contain at least one special character");
        }

        private static void CheckPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new Exception("Invalid phone number");

            if (!phoneNumber.All(char.IsDigit))
                throw new Exception("Phone number must contain only digits");

            if (phoneNumber.Length < 7 || phoneNumber.Length > 15)
                throw new Exception("Invalid phone number length");
        }

        private static void CheckName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Last name is required");

            if (firstName.Any(char.IsDigit) || lastName.Any(char.IsDigit))
                throw new Exception("Names cannot contain numbers");
        }
    }
}
