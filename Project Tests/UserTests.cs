using Project_App;
namespace Project_Tests
{
    public class UserTests
    {
        private readonly IPassword _passwordManager = new MyPasswordManager();

        // ----------------------------
        // User Creation
        // ----------------------------
        [Fact]
        public void Constructor_WithValidData_CreatesUser()
        {
            var user = new User(
                "testuser",
                "Password123!",
                "John",
                "Doe",
                "123456789",
                _passwordManager);

            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("testuser", user.UserName);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
            Assert.Equal("123456789", user.PhoneNumber);
            Assert.Equal("Password123!", user.HashedPassword);
        }

        // ----------------------------
        // Username Validation
        // ----------------------------
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void SetUserName_Empty_Throws(string userName)
        {
            var user = CreateValidUser();

            var ex = Assert.Throws<ArgumentException>(() =>
                user.SetUserName(userName));

            Assert.Equal("Username is required.", ex.Message);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("a")]
        public void SetUserName_TooShort_Throws(string userName)
        {
            var user = CreateValidUser();

            Assert.Throws<ArgumentException>(() =>
                user.SetUserName(userName));
        }

        // ----------------------------
        // Name Validation
        // ----------------------------
        [Fact]
        public void SetName_WithDigits_Throws()
        {
            var user = CreateValidUser();

            var ex = Assert.Throws<ArgumentException>(() =>
                user.SetName("J0hn", "Doe"));

            Assert.Equal("Names cannot contain numbers.", ex.Message);
        }

        [Fact]
        public void SetName_EmptyFirstName_Throws()
        {
            var user = CreateValidUser();

            Assert.Throws<ArgumentException>(() =>
                user.SetName("", "Doe"));
        }

        // ----------------------------
        // Phone Number
        // ----------------------------
        [Fact]
        public void SetPhoneNumber_Empty_Throws()
        {
            var user = CreateValidUser();

            Assert.Throws<ArgumentException>(() =>
                user.SetPhoneNumber(""));
        }

        [Fact]
        public void SetPhoneNumber_Valid_SetsValue()
        {
            var user = CreateValidUser();

            user.SetPhoneNumber("987654321");

            Assert.Equal("987654321", user.PhoneNumber);
        }

        // ----------------------------
        // Password
        // ----------------------------
        [Fact]
        public void SetPassword_Empty_Throws()
        {
            var user = CreateValidUser();

            Assert.Throws<ArgumentException>(() =>
                user.SetPassword(""));
        }

        [Fact]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            var user = CreateValidUser();

            bool result = user.VerifyPassword("Password123!");

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WrongPassword_ReturnsFalse()
        {
            var user = CreateValidUser();

            bool result = user.VerifyPassword("WrongPassword");

            Assert.False(result);
        }

        // ----------------------------
        // Helpers
        // ----------------------------
        private User CreateValidUser()
        {
            return new User(
                "validuser",
                "Password123!",
                "John",
                "Doe",
                "123456789",
                _passwordManager);
        }
    }
}
