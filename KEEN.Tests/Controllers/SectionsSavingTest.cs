using KEEN.Controllers;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace KEEN.Tests.Controllers
{
    [TestClass]
    public class SectionsSavingTest
    {
        private static MockRepository InitRepo()
        {
            return new MockRepository(
                new SubmissionEntity
                {
                    Id = 1,
                    FormId = 1,
                    UserId = 1,
                    IsComplete = false,
                    SubmissionSections = new List<SubmissionSectionEntity>
                    {
                        new SubmissionSectionEntity
                        {
                            SubmissionId = 1,
                            SectionId = 1,
                            IsComplete = false,
                            FieldResponses = new List<FieldResponseEntity>
                            {
                                new FieldResponseEntity
                                {
                                    SubmissionId = 1,
                                    SectionId = 1,
                                    FieldId = 1,
                                    Text = "Test"
                                },
                                new FieldResponseEntity
                                {
                                    SubmissionId = 1,
                                    SectionId = 1,
                                    FieldId = 2,
                                    Text = "Test1"
                                }
                            }
                        }
                    }
                },
                new SectionEntity { Id = 1, FormId = 1 },
                new SectionEntity { Id = 2, FormId = 1 },
                new SectionEntity { Id = 6, FormId = 1 },
                new SectionFieldEntity { SectionId = 1, FieldId = 1 },
                new SectionFieldEntity { SectionId = 1, FieldId = 2 },
                new SectionFieldEntity { SectionId = 1, FieldId = 3 },
                new SectionFieldEntity { SectionId = 2, FieldId = 1 },
                new SectionFieldEntity { SectionId = 2, FieldId = 2 },
                new SectionFieldEntity { SectionId = 2, FieldId = 3 },
                new SectionFieldEntity { SectionId = 6, FieldId = 1 },
                new SectionFieldEntity { SectionId = 6, FieldId = 2 },
                new SectionFieldEntity { SectionId = 6, FieldId = 3 },
                new UserProgramEntity { UserId = 1, ProgramId = 1 },
                new UserProgramEntity { UserId = 2, ProgramId = 1 },
                new FieldEntity { Id = 1, FieldTypeCode = "checkbox", FieldSet = new FieldSetEntity { Id = 1 }},
                new FieldEntity { Id = 2, FieldTypeCode = "text", FieldSet = new FieldSetEntity { Id = 2 }},
                new FieldEntity { Id = 3, FieldTypeCode = "select", FieldSet = new FieldSetEntity { Id = 3 }},
                new FieldSetResponseEntity { SubmissionId = 1, SectionId = 1, FieldId = 1, FieldSetValueCd = "13-other", SelectedInd = 1 }
            );
        }

        [TestMethod]
        public void TestExistingSubmissionSection()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new SectionsController(repo);
            var formData = new Dictionary<int, string>()
            {
                {1, "test"},
                {2, "test2"},
                {3, "test3"}
            };

            // Act
            controller.Put(1, 1, 1, 1, formData);

            // Assert
            var oldResponses = repo.Get<SubmissionEntity>().First().SubmissionSections.First().FieldResponses;
            var fieldEntityResponse = repo.GetOne<FieldEntity>(r => r.Id == 1);
            var fieldSetResponsibilityResponse = repo.GetOne<FieldSetResponseEntity>(r => r.SubmissionId == 1
                                                                                          && r.SectionId == 1
                                                                                          && r.FieldId == 1
                                                                                          && r.FieldSetValueCd.Equals("13-other"));

            Assert.AreEqual("test", oldResponses.First(r => r.FieldId == 1).Text);
            Assert.AreEqual("test2", oldResponses.First(r => r.FieldId == 2).Text);
            Assert.AreEqual("test3", repo.GetOne<FieldResponseEntity>(r => r.SubmissionId == 1 && r.SectionId == 1 && r.FieldId == 3).Text);
            Assert.AreEqual("checkbox", fieldEntityResponse.FieldTypeCode);
            Assert.AreEqual(1, fieldSetResponsibilityResponse.SelectedInd);
        }

        [TestMethod]
        public void TestNewSubmissionSection()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new SectionsController(repo);
            var formData = new Dictionary<int, string>()
            {
                {1, "test"},
                {2, "test2"},
                {3, "test3"}
            };

            // Act
            controller.Put(1, 1, 2, 1, formData);

            // Assert
            var section = repo.GetOne<SubmissionSectionEntity>(s => s.SubmissionId == 1 && s.SectionId == 2);
            Assert.IsNotNull(section);

            var responses = repo.Get<FieldResponseEntity>(r => r.SubmissionId == 1 && r.SectionId == 2);

            Assert.AreEqual(3, responses.Count);
            Assert.AreEqual("test", responses.First(r => r.FieldId == 1).Text);
            Assert.AreEqual("test2", responses.First(r => r.FieldId == 2).Text);
            Assert.AreEqual("test3", responses.First(r => r.FieldId == 3).Text);

            var fieldEntityResponse = repo.GetOne<FieldEntity>(r => r.Id == 1);
            var fieldSetResponsibilityResponse = repo.GetOne<FieldSetResponseEntity>(r => r.SubmissionId == 1
                                                                                          && r.SectionId == 1
                                                                                          && r.FieldId == 1
                                                                                          && r.FieldSetValueCd.Equals("13-other"));

            Assert.AreEqual("checkbox", fieldEntityResponse.FieldTypeCode);
            Assert.AreEqual(1, fieldSetResponsibilityResponse.SelectedInd);


        }

        [TestMethod]
        public void TestNewSubmission()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new SectionsController(repo);
            var formData = new Dictionary<int, string>()
            {
                {1, "test"},
                {2, "test2"},
                {3, "test3"}
            };

            // Act
            controller.Put(1, 1, 6, 2, formData);

            // Assert
            var submission = repo.GetOne<SubmissionEntity>(s => s.UserId == 2);
            Assert.IsNotNull(submission);

            var section = repo.GetOne<SubmissionSectionEntity>(s => s.SectionId == 6);
            Assert.IsNotNull(section);

            var responses = repo.Get<FieldResponseEntity>(r => r.SectionId == 6);

            Assert.AreEqual(3, responses.Count);
            Assert.AreEqual("test", responses.First(r => r.FieldId == 1).Text);
            Assert.AreEqual("test2", responses.First(r => r.FieldId == 2).Text);
            Assert.AreEqual("test3", responses.First(r => r.FieldId == 3).Text);
        }
    }
}
