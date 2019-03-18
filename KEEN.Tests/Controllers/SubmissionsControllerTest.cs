using System.Collections.Generic;
using System.Linq;
using KEEN.Controllers;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KEEN.Tests.Controllers
{
    [TestClass]
    public class SubmissionsControllerTest
    {
        private static MockRepository InitRepo()
        {
            var i = 1;
            return new MockRepository(
                new SubmissionEntity
                {
                    Id = 1,
                    UserId = 1,
                    FormId = 1,
                    IsComplete = false
                },
                new SubmissionEntity
                {
                    Id = 2,
                    UserId = 1,
                    FormId = 2,
                    IsComplete = true,
                    Form = new FormEntity
                    {
                        Id = 2,
                        Sections = new List<SectionEntity>
                        {
                            new SectionEntity
                            {
                                FormId = 2,
                                Id = 1,
                                SectionFields = new List<SectionFieldEntity>
                                {
                                    TestHelpers.CreateField(1, i++, "text"),
                                    TestHelpers.CreateField(1, i++, "text"),
                                    TestHelpers.CreateField(1, i++, "text")
                                }
                            },
                            new SectionEntity
                            {
                                FormId = 2,
                                Id = 2,
                                SectionFields = new List<SectionFieldEntity>
                                {
                                    TestHelpers.CreateField(2, i++, "text"),
                                    TestHelpers.CreateField(2, i, "text")
                                }
                            }
                        }
                    }.MapSections()
                },
                new SubmissionSectionEntity
                {
                    SubmissionId = 2,
                    SectionId = 1,
                    FieldResponses = new List<FieldResponseEntity>
                    {
                        new FieldResponseEntity { FieldId = 1, Text = "Field 1" },
                        new FieldResponseEntity { FieldId = 2, Text = "Field 2" },
                        new FieldResponseEntity { FieldId = 3, Text = "Field 3" }
                    }
                },
                new SubmissionSectionEntity
                {
                    SubmissionId = 2,
                    SectionId = 2,
                    FieldResponses = new List<FieldResponseEntity>
                    {
                        new FieldResponseEntity { FieldId = 4, Text = "Field 4" },
                        new FieldResponseEntity { FieldId = 5, Text = "Field 5" }
                    }
                }
            );
        }

        [TestMethod]
        public void TestGetSubmission()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new SubmissionsController(repo);

            // Act
            var sections = controller.Get(1, 1, 2).Sections;

            // Assert
            Assert.AreEqual(2, sections.Count);

            var field = sections.First().Fields.ElementAt(0);
            Assert.IsTrue(field is TextField);
            Assert.AreEqual("Field 1", ((TextField)field).Value);

            field = sections.First().Fields.ElementAt(1);
            Assert.IsTrue(field is TextField);
            Assert.AreEqual("Field 2", ((TextField)field).Value);

            field = sections.First().Fields.ElementAt(2);
            Assert.IsTrue(field is TextField);
            Assert.AreEqual("Field 3", ((TextField)field).Value);

            field = sections.Last().Fields.ElementAt(0);
            Assert.IsTrue(field is TextField);
            Assert.AreEqual("Field 4", ((TextField)field).Value);

            field = sections.Last().Fields.ElementAt(1);
            Assert.IsTrue(field is TextField);
            Assert.AreEqual("Field 5", ((TextField)field).Value);
        }

        [TestMethod]
        public void TestSubmitSubmission()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new SubmissionsController(repo);

            // Act
            controller.Put(1, 1, 1);

            // Assert
            Assert.IsTrue(repo.Get<SubmissionEntity>().First().IsComplete);
        }

        [TestMethod]
        public void GetSubmissionId()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new SubmissionsController(repo);

            // Act
            var submissionId = controller.GetCurrent(1, 1, 1);

            // Assert
            Assert.AreEqual(1, submissionId);
        }
    }
}
