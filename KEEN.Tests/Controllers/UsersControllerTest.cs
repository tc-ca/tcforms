using System.Linq;
using KEEN.Controllers;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KEEN.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        private static MockRepository InitRepo()
        {
            return new MockRepository(
                new UserProgramEntity
                {
                    ProgramId = 1,
                    UserId = 1,
                    IdentifierText = "Test 1"
                },
                new UserProgramEntity
                {
                    ProgramId = 2,
                    UserId = 1,
                    IdentifierText = "Test 2"
                }
            );
        }

        [TestMethod]
        public void TestExistingUser()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new UsersController(repo);

            // Act
            var userId = controller.Post(1, "Test 1");
            
            // Assert
            Assert.AreEqual(2, repo.Get<UserProgramEntity>().Count);
            Assert.AreEqual(1, userId);
        }

        [TestMethod]
        public void TestExistingUserDifferentProgram()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new UsersController(repo);

            // Act
            var userId = controller.Post(1, "Test 2");

            // Assert
            Assert.AreEqual(3, repo.Get<UserProgramEntity>().Count);
            var userProgram = repo.Get<UserProgramEntity>().Last();
            Assert.AreEqual(userId, userProgram.UserId);
            Assert.AreEqual("Test 2", userProgram.IdentifierText);
        }

        [TestMethod]
        public void TestNewUser()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new UsersController(repo);

            // Act
            var userId = controller.Post(1, "Test 3");

            // Assert
            Assert.AreEqual(3, repo.Get<UserProgramEntity>().Count);
            var userProgram = repo.Get<UserProgramEntity>().Last();
            Assert.AreEqual(userId, userProgram.UserId);
            Assert.AreEqual("Test 3", userProgram.IdentifierText);
        }
    }
}
