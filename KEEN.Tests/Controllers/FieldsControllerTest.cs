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
    public class FieldsControllerTest
    {
        private static MockRepository InitRepo()
        {
            return new MockRepository(
                new SubmissionEntity
                {
                    Id = 1,
                    FormId = 1,
                    UserId = 1,
                    SubmissionSections = new List<SubmissionSectionEntity>
                    {
                        new SubmissionSectionEntity
                        {
                            SubmissionId = 1,
                            SectionId = 1,
                            FieldResponses = new List<FieldResponseEntity>
                            {
                                new FieldResponseEntity
                                {
                                    FieldId = 1,
                                    Text = "test@email.com",
                                    Field = new FieldEntity
                                    {
                                        Id = 1,
                                        FieldTypeCode = "contact"
                                    }
                                },
                                new FieldResponseEntity
                                {
                                    FieldId = 2,
                                    Text = "test2",
                                    Field = new FieldEntity
                                    {
                                        Id = 2,
                                        FieldTypeCode = "text"
                                    }
                                },
                                new FieldResponseEntity
                                {
                                    FieldId = 3,
                                    Text = "test3@email.com",
                                    Field = new FieldEntity
                                    {
                                        Id = 3,
                                        FieldTypeCode = "contact"
                                    }
                                }
                            }
                        }
                    }
                }
            );
        }
        [TestMethod]
        public void GetFieldsByType()
        {
            // Arrange
            var repo = InitRepo();
            var controller = new FieldsController(repo);

            // Act
            var fields = controller.Get(1, 1, 1, "contact");

            // Assert
            Assert.AreEqual(2, fields.Count);
            Assert.AreEqual("test@email.com", ((TextField) fields.First()).Value);
        }
    }
}