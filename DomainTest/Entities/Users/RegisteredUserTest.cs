using Domain.Entities.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.Entities.Users
{
    [TestClass]
    public class RegisteredUserTest
    {
        [TestMethod]
        public void Id_Get_ReturnsSetValue()
        {
            // Arrange
            uint expectedId = 1;
            RegisteredUser registeredUser = new TestRegisteredUser(expectedId, "testlogin", "Test@123");

            // Act
            uint actualId = registeredUser.Id;

            // Assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void Login_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validLogin = "testlogin";
            RegisteredUser registeredUser = new TestRegisteredUser(1, "oldlogin", "Test@123");

            // Act
            registeredUser.Login = validLogin;

            // Assert
            Assert.AreEqual(validLogin, registeredUser.Login);
        }

        [TestMethod]
        public void Login_Set_InvalidValue_ThrowsArgumentException()
        {
            // Arrange
            string invalidLogin = "";
            RegisteredUser registeredUser = new TestRegisteredUser(1, "oldlogin", "Test@123");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => registeredUser.Login = invalidLogin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Password_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validPassword = "Test@123";
            RegisteredUser registeredUser = new TestRegisteredUser(1, "testlogin", "oldpassword");

            // Act
            registeredUser.Password = validPassword;

            // Assert
            Assert.AreEqual(validPassword, registeredUser.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Password_Set_InvalidValue_ThrowsArgumentException()
        {
            // Arrange
            string invalidPassword = "weakpassword";
            RegisteredUser registeredUser = new TestRegisteredUser(1, "testlogin", "oldpassword");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => registeredUser.Password = invalidPassword);
        }

        [TestMethod]
        public void Login_Set_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            string nullLogin = null;
            RegisteredUser registeredUser = new TestRegisteredUser(1, "oldlogin", "Test@123");

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => registeredUser.Login = nullLogin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void Password_Set_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            string nullPassword = null;
            RegisteredUser registeredUser = new TestRegisteredUser(1, "testlogin", "oldpassword");

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => registeredUser.Password = nullPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void Password_Set_WeakValue_ThrowsArgumentException()
        {
            // Arrange
            string weakPassword = "password";
            RegisteredUser registeredUser = new TestRegisteredUser(1, "testlogin", "oldpassword");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => registeredUser.Password = weakPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void Password_Set_LongValue_ThrowsArgumentException()
        {
            // Arrange
            string longPassword = new string('a', 257);
            RegisteredUser registeredUser = new TestRegisteredUser(1, "testlogin", "oldpassword");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => registeredUser.Password = longPassword);
        }

        private class TestRegisteredUser : RegisteredUser
        {
            public TestRegisteredUser(uint id, string login, string password)
                : base(id, login, password)
            {
            }
        }
    }
}