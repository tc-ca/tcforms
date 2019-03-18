using System.Collections.Generic;
using System.Linq;
using KEEN.Controllers;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KEEN.Tests.Controllers
{
    [TestClass]
    public class ProgramsControllerTest
    {
        private static MockRepository InitRepo()
        {
            return new MockRepository(
                new ProgramEntity
                {
                    Id = 1,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Test 1" },
                        { "fr", "Test fr" }
                    }.ToResource()
                },
                new ProgramEntity
                {
                    Id = 2,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Test 2" }
                    }.ToResource(),
                    Forms = new List<FormEntity>
                    {
                        new FormEntity
                        {
                            Id = 1,
                            ProgramId = 2
                        },
                        new FormEntity
                        {
                            Id = 2,
                            ProgramId = 2
                        },
                    }
                },
                new ProgramEntity
                {
                    Id = 3,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Test 3" },
                        { "fr-CA", "Test 3 FR" }
                    }.ToResource()
                }
            );
        }

        [TestMethod]
        public void TestGetAll()
        {
            // Arrange
            var controller = new ProgramsController(InitRepo());

            // Act
            var programs = controller.Get();

            // Assert
            Assert.IsNotNull(programs);
            Assert.AreEqual(3, programs.Count());
        }

        [TestMethod]
        public void TestGetOne()
        {
            // Arrange
            var controller = new ProgramsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "en" }
            });

            // Act
            var program = controller.Get(2);

            // Assert
            Assert.IsNotNull(program);
            Assert.AreEqual("Test 2", program.Label);
            Assert.IsNotNull(program.Forms);
            Assert.AreEqual(2, program.Forms.Count);
        }

        [TestMethod]
        public void TestFrench()
        {
            var controller = new ProgramsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "fr" }
            });

            // Act
            var program = controller.Get(1);

            // Assert
            Assert.AreEqual("Test fr", program.Label);
            Assert.IsNull(program.Labels);
        }

        [TestMethod]
        public void TestNoLanguage()
        {
            var controller = new ProgramsController(InitRepo());

            // Act
            var program = controller.Get(1);

            // Assert
            Assert.IsNull(program.Label);
            Assert.AreEqual(2, program.Labels.Count());
        }

        [TestMethod]
        public void TestLanguageQuality()
        {
            var controller = new ProgramsController(InitRepo());
            controller.SetHeaders(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ACCEPT-LANGUAGE", "es"),
                new KeyValuePair<string, string>("ACCEPT-LANGUAGE", "fr-CA;q=0.8"),
                new KeyValuePair<string, string>("ACCEPT-LANGUAGE", "en;q=0.5"),
                new KeyValuePair<string, string>("ACCEPT-LANGUAGE", "*;q=0.4")
            });

            // Act
            var program = controller.Get(3);

            // Assert
            Assert.AreEqual("Test 3 FR", program.Label);
        }

        [TestMethod]
        public void TestLanguageWildcard()
        {
            var controller = new ProgramsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "*" }
            });

            // Act
            var program = controller.Get(3);

            // Assert
            Assert.AreEqual("Test 3", program.Label);
        }
    }
}
