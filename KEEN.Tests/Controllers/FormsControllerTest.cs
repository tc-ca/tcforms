using System.Collections.Generic;
using System.Linq;
using KEEN.Controllers;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KEEN.Tests.Controllers
{
    [TestClass]
    public class FormsControllerTest
    {
        private static MockRepository InitRepo()
        {
            return new MockRepository(
                new FormEntity
                {
                    Id = 1,
                    ProgramId = 1,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Form 1" },
                        { "fr", "Test fr" }
                    }.ToResource()
                },
                new FormEntity
                {
                    Id = 2,
                    ProgramId = 1,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Form 2" }
                    }.ToResource(),
                    Sections = new List<SectionEntity>
                    {
                        new SectionEntity
                        {
                            Id = 1
                        },
                        new SectionEntity
                        {
                            Id = 2
                        },
                        new SectionEntity
                        {
                            Id = 3
                        }
                    }
                }.MapSections(),
                new FormEntity
                {
                    Id = 3,
                    ProgramId = 2,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Form 3" }
                    }.ToResource()
                }
            );
        }

        [TestMethod]
        public void GetAllForProgram()
        {
            // Arrange
            var controller = new FormsController(InitRepo());

            // Act
            var forms = controller.Get(1);

            // Assert
            Assert.IsNotNull(forms);
            Assert.AreEqual(2, forms.Count());
        }

        [TestMethod]
        public void GetOne()
        {
            // Arrange
            var controller = new FormsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "en" }
            });

            // Act
            var form = controller.Get(1, 2);

            // Assert
            Assert.IsNotNull(form);
            Assert.AreEqual(form.Label, "Form 2");
            Assert.IsNotNull(form.Sections);
            Assert.AreEqual(3, form.Sections.Count);
        }

        [TestMethod]
        public void TestFrench()
        {
            // Arrange
            var controller = new FormsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "fr" }
            });

            // Act
            var form = controller.Get(1, 1);

            // Assert
            Assert.AreEqual(form.Label, "Test fr");
            Assert.IsNull(form.Labels);
        }

        [TestMethod]
        public void TestNoLanguage()
        {
            // Arrange
            var controller = new FormsController(InitRepo());

            // Act
            var form = controller.Get(1, 1);

            // Assert
            Assert.IsNull(form.Label);
            Assert.AreEqual(2, form.Labels.Count());
        }
    }
}
