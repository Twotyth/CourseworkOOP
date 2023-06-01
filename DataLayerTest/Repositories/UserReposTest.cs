using DataLayer.Dtos;
using DataLayer.Exceptions;
using DataLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTest.Repositories
{
    [TestClass]
    public class UserReposTest
    {
        private UserRepos _userRepos;

        [TestInitialize]
        public void Initialize()
        {
            _userRepos = new UserRepos();
        }

        [TestMethod]
        public void Add_ValidUserDto_AddsUserToRepository()
        {
            // Arrange
            var userDto = new ClientDto(1, "john", "password1234$", 100.0m);

            // Act
            _userRepos.Add(userDto);

            // Assert
            Assert.AreEqual(1, _userRepos.Count());
            Assert.IsTrue(_userRepos.Any(u => u.Login == "john"));
        }

        [TestMethod]
        public void Add_DuplicateLogin_ThrowsDuplicateException()
        {
            // Arrange
            var userDto1 = new ClientDto(1, "john", "password1234$", 100.0m);
            var userDto2 = new ClientDto(2, "john", "password1234$", 100.0m);

            // Act
            _userRepos.Add(userDto1);

            // Assert
            Assert.ThrowsException<DuplicateException>(() => _userRepos.Add(userDto2));
        }

        [TestMethod]
        public void Find_ExistingUserId_ReturnsRegisteredUser()
        {
            // Arrange
            var userDto = new ClientDto(1, "john", "password1234$", 100.0m);
            _userRepos.Add(userDto);

            // Act
            var result = _userRepos.Find(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("john", result.Login);
        }

        [TestMethod]
        public void Find_NonExistingUserId_ReturnsNull()
        {
            // Arrange
            var userDto = new ClientDto(1, "john", "password1234$", 100.0m);
            _userRepos.Add(userDto);

            // Act
            var result = _userRepos.Find(2);

            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void Delete_ExistingUser_RemovesUserFromRepository()
        {
            // Arrange
            var userDto1 = new ClientDto(1, "john", "password1$", 100.0m);
            var userDto2 = new ClientDto(2, "jane", "password1$", 200.0m);
            _userRepos.Add(userDto1);
            _userRepos.Add(userDto2);

            // Act
            var userToRemove = _userRepos.Find(u => u.Login == "john");
            _userRepos.Delete(userToRemove);

            // Assert
            Assert.AreEqual(1, _userRepos.Count());
            Assert.IsFalse(_userRepos.Any(u => u.Login == "john"));
            Assert.IsTrue(_userRepos.Any(u => u.Login == "jane"));
        }

    }
}
