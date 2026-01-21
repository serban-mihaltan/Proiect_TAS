using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_App
{
    public class User
    {
        private IPassword PasswordManager;
        public Guid Id { get; private set; } = Guid.NewGuid();
        //loggin information
        public string UserName { get; private set; } = null!;
        public string HashedPassword{ get; private set; } = null!;
        //random account details
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;

        public User(string userName,string password,string firstName,string lastName,string phoneNumber,IPassword passwordManager)
        {
            PasswordManager = passwordManager;
            SetUserName(userName);
            SetName(firstName, lastName);
            SetPhoneNumber(phoneNumber);
            SetPassword(password);
            
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

           HashedPassword=PasswordManager.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            bool result = PasswordManager.VerifyHashedPassword(this.HashedPassword, password);
            return result;
        }

        public void SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username is required.");

            if (userName.Length < 3 || userName.Length > 50)
                throw new ArgumentException("Username length is invalid.");

            UserName = userName;
        }

        public void SetName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.");

            if (ContainsDigit(firstName) || ContainsDigit(lastName))
                throw new ArgumentException("Names cannot contain numbers.");

            FirstName = firstName;
            LastName = lastName;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.");

            PhoneNumber = phoneNumber;
        }

        public static bool ContainsDigit(string value)
            => value.Any(char.IsDigit);
    }
}
