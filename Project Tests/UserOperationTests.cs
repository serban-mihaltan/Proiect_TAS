using System;
using Moq;
using Project_App;
using Xunit;

namespace Project_Tests
{
    public class UserOperationsTests
    {
        // ----------------------------
        // CreateNewUser – Success
        // ----------------------------
        [Fact]
        public void CreateNewUser_WithValidData_AddsUser()
        {
            // Arrange
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            // Act
            UserOperations.CreateNewUser(
                "testuser",
                "Password123!",
                "John",
                "Doe",
                "123456789",
                passwordMock,
                userDbMock.Object);

            // Assert
            userDbMock.Verify(
                db => db.AddUser(It.Is<User>(u => u.UserName == "testuser")),
                Times.Once);
        }

        // ----------------------------
        // CreateNewUser – Exceptions
        // ----------------------------
        //username already exists
        [Fact]
        public void CreateNewUser_DuplicateUsername()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername("testuser"))
                .Returns(true);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Username already in use", ex.Message);
        }
        //password checks
        [Fact]
        public void CreateNewUser_WeakPassword_ToShort()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);
            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "p!",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal(
                "Password must contain at least 8 characters",
                ex.Message);
        }
        [Fact]
        public void CreateNewUser_WeakPassword_ToLong()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);
            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "password123!aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal(
                "Password must be shorter than 20 characters",
                ex.Message);
        }
        [Fact]
        public void CreateNewUser_WeakPassword_UpperCharacter()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "password123!",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal(
                "Password must contain at least one uppercase character",
                ex.Message);
        }

        [Fact]
        public void CreateNewUser_WeakPassword_LowerCharacter()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "PASSWORD123!",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal(
                "Password must contain at least one lowercase character",
                ex.Message);
        }

        [Fact]
        public void CreateNewUser_WeakPassword_DigitCharacter()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "passwordPPP!",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal(
                "Password must contain at least one digit",
                ex.Message);
        }

        [Fact]
        public void CreateNewUser_WeakPassword_SpecialCharacter()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "password123ALP",
                    "John",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal(
                "Password must contain at least one special character",
                ex.Message);
        }
        [Fact]
        public void CreateNewUser_InvalidPhoneNumber()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "John",
                    "Doe",
                    "12AB",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Phone number must contain only digits", ex.Message);
        }
        [Fact]
        public void CreateNewUser_EmptyPhoneNumber()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "John",
                    "Doe",
                    "",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Invalid phone number", ex.Message);
        }

        [Fact]
        public void CreateNewUser_PhoneNumberTooShort()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "John",
                    "Doe",
                    "123",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Invalid phone number length", ex.Message);
        }

        [Fact]
        public void CreateNewUser_PhoneNumberTooLong()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "John",
                    "Doe",
                    "12345678901234567890",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Invalid phone number length", ex.Message);
        }
        [Fact]
        public void CreateNewUser_NameWithDigits()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "J0hn",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Names cannot contain numbers", ex.Message);
        }
        [Fact]
        public void CreateNewUser_EmptyFirstName()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "",
                    "Doe",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("First name is required", ex.Message);
        }

        [Fact]
        public void CreateNewUser_EmptyLastName()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns(false);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.CreateNewUser(
                    "testuser",
                    "Password123!",
                    "John",
                    "",
                    "123456789",
                    passwordMock,
                    userDbMock.Object));

            Assert.Equal("Last name is required", ex.Message);
        }
        // ----------------------------
        // Login
        // ----------------------------
        [Fact]
        public void Loggin_WithCorrectPassword()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            var user = new User(
                "testuser",
                "Password123!",
                "John",
                "Doe",
                "123456789",
                passwordMock);

            userDbMock
                .Setup(db => db.FetchUser("testuser"))
                .Returns(user);

            bool result = UserOperations.Loggin(
                "testuser",
                "Password123!",
                userDbMock.Object,
                passwordMock);

            Assert.True(result);
        }

        [Fact]
        public void Loggin_WithWrongPassword()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            var user = new User(
                "testuser",
                "Password123!",
                "John",
                "Doe",
                "123456789",
                passwordMock);

            userDbMock
                .Setup(db => db.FetchUser("testuser"))
                .Returns(user);

            bool result = UserOperations.Loggin(
                "testuser",
                "WrongPassword",
                userDbMock.Object,
                passwordMock);

            Assert.False(result);
        }

        [Fact]
        public void Loggin_UserNotFound()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            userDbMock
                .Setup(db => db.FetchUser(It.IsAny<string>()))
                .Returns((User?)null);

            var ex = Assert.Throws<Exception>(() =>
                UserOperations.Loggin(
                    "missinguser",
                    "Password123!",
                    userDbMock.Object,
                    passwordMock));

            Assert.Equal("User not found", ex.Message);
        }

        [Fact]
        public void CreateNewUser_MultipleUsers_MixedResults_VerifiesDbCalls()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            int addUserCallCount = 0;

            userDbMock
                .Setup(db => db.CheckUsername(It.IsAny<string>()))
                .Returns<string>(username => username == "existinguser");

            userDbMock
                .Setup(db => db.AddUser(It.IsAny<User>()))
                .Callback<User>(_ => addUserCallCount++);

            var actions = new Action[]
            {
        // should succeed
        () => UserOperations.CreateNewUser(
            "user1", "Password123!", "John", "Doe", "123456789",
            passwordMock, userDbMock.Object),

        // should fail (duplicate username)
        () => Assert.Throws<Exception>(() =>
            UserOperations.CreateNewUser(
                "existinguser", "Password123!", "Jane", "Doe", "123456789",
                passwordMock, userDbMock.Object)),

        // should fail (weak password)
        () => Assert.Throws<Exception>(() =>
            UserOperations.CreateNewUser(
                "user2", "password", "Jim", "Beam", "123456789",
                passwordMock, userDbMock.Object)),

        // should succeed
        () => UserOperations.CreateNewUser(
            "user3", "Password123!", "Alice", "Smith", "987654321",
            passwordMock, userDbMock.Object)
            };

            // Act (run in parallel)
            Parallel.Invoke(actions);

            // Assert
            Assert.Equal(2, addUserCallCount);

            userDbMock.Verify(
                db => db.AddUser(It.Is<User>(u =>
                    u.UserName == "user1" || u.UserName == "user3")),
                Times.Exactly(2));

            userDbMock.Verify(
                db => db.CheckUsername(It.IsAny<string>()),
                Times.Exactly(4));
        }
        [Fact]
        public void Loggin_MultipleAttempts_MixedResults_VerifiesDbCalls()
        {
            var passwordMock = new MyPasswordManager();
            var userDbMock = new Mock<IUserDB>();

            var validUser = new User(
                "validuser",
                "Password123!",
                "John",
                "Doe",
                "123456789",
                passwordMock);

            int fetchUserCallCount = 0;

            userDbMock
                .Setup(db => db.FetchUser(It.IsAny<string>()))
                .Callback(() => fetchUserCallCount++)
                .Returns<string>(username =>
                    username == "validuser" ? validUser : null);

            var actions = new Action[]
            {
        // success
        () =>
        {
            bool result = UserOperations.Loggin(
                "validuser", "Password123!",
                userDbMock.Object, passwordMock);

            Assert.True(result);
        },

        // wrong password
        () =>
        {
            bool result = UserOperations.Loggin(
                "validuser", "WrongPassword",
                userDbMock.Object, passwordMock);

            Assert.False(result);
        },

        // user not found
        () => Assert.Throws<Exception>(() =>
            UserOperations.Loggin(
                "missinguser", "Password123!",
                userDbMock.Object, passwordMock))
            };

            // Act
            Parallel.Invoke(actions);

            // Assert
            Assert.Equal(3, fetchUserCallCount);

            userDbMock.Verify(
                db => db.FetchUser("validuser"),
                Times.Exactly(2));

            userDbMock.Verify(
                db => db.FetchUser("missinguser"),
                Times.Once);
        }


    }
}
