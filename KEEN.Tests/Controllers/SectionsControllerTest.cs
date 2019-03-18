using System.Collections.Generic;
using System.Linq;
using KEEN.Controllers;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models.Fields;
using KEEN.Entities.Models.Fields.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KEEN.Tests.Controllers
{
    [TestClass]
    public class SectionsControllerTest
    {
        private static MockRepository InitRepo()
        {
            var i = 1;

            var form1 = new FormEntity {Id = 1};
            var form2 = new FormEntity {Id = 2};

            return new MockRepository(
                form1.AddSection(new SectionEntity
                {
                    Id = 2,
                    DisplaySort = 2,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Section 2" },
                        { "fr", "Section fr" }
                    }.ToResource(),
                    SectionFields = new List<SectionFieldEntity>()
                    {
                        TestHelpers.CreateField(2, 1, "text", sort: 2),
                        TestHelpers.CreateField(2, 2, "text", sort: 1, resource: new Dictionary<string, string>
                        {
                            { "en", "Field" },
                            { "fr", "Field fr" } 
                        }.ToResource())
                    }
                }),
                form1.AddSection(new SectionEntity
                {
                    Id = 1,
                    DisplaySort = 1,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Section 1" }
                    }.ToResource(),
                    SectionFields = new List<SectionFieldEntity>()
                    {
                        TestHelpers.CreateField(1, i++, "text"),
                        TestHelpers.CreateField(1, i++, "password"),
                        TestHelpers.CreateField(1, i++, "email"),
                        TestHelpers.CreateField(1, i++, "color"),
                        TestHelpers.CreateField(1, i++, "telephone"),
                        TestHelpers.CreateField(1, i++, "number"),
                        TestHelpers.CreateField(1, i++, "date"),
                        TestHelpers.CreateField(1, i++, "textarea"),
                        TestHelpers.CreateField(1, i++, "tinymce"),
                        TestHelpers.CreateField(1, i++, "quill"),
                        TestHelpers.CreateField(1, i++, "select", new Dictionary<string, string>
                        {
                            { "1", "one" },
                            { "2", "two" }
                        }.ToFieldSet()),
                        TestHelpers.CreateField(1, i++, "radio", new Dictionary<string, string>
                        {
                            { "1", "one" },
                            { "2", "two" },
                            { "3", "three" }
                        }.ToFieldSet()),
                        TestHelpers.CreateField(1, i++, "checkbox", new Dictionary<string, string>
                        {
                            { "1", "one" },
                            { "2", "two" },
                            { "3", "three" }
                        }.ToFieldSet()),
                        TestHelpers.CreateField(1, i++, "p"),
                        TestHelpers.CreateField(1, i++, "address"),
                        TestHelpers.CreateField(1, i++, "blockquote"),
                        TestHelpers.CreateField(1, i++, "canvas"),
                        TestHelpers.CreateField(1, i++, "output"),
                        TestHelpers.CreateField(1, i++, "h1"),
                        TestHelpers.CreateField(1, i++, "h2"),
                        TestHelpers.CreateField(1, i, "h3")
                    }
                }),
                form2.AddSection(new SectionEntity
                {
                    Id = 3,
                    DisplaySort = 1,
                    Resource = new Dictionary<string, string>
                    {
                        { "en", "Section 3" }
                    }.ToResource()
                }),
                new SubmissionEntity
                {
                    Id = 1,
                    FormId = 1,
                    UserId = 1,
                    IsComplete = false
                },
                new SubmissionSectionEntity
                {
                    SubmissionId = 1,
                    SectionId = 1,
                    FieldResponses = new List<FieldResponseEntity>
                    {
                        new FieldResponseEntity { SectionId = 1, FieldId = 1, Text = "Textbox Test" },
                        new FieldResponseEntity { SectionId = 1, FieldId = 11, Text = "2" },
                        new FieldResponseEntity { SectionId = 1, FieldId = 13, Text = "1,3" }
                    }
                },
                new UserProgramEntity { UserId = 1, ProgramId = 1 },
                new SubmissionEntity
                {
                    Id = 1,
                    FormId = 1,
                    UserId = 2,
                    IsComplete = true
                },
                new SubmissionSectionEntity
                {
                    SubmissionId = 1,
                    SectionId = 1,
                    FieldResponses = new List<FieldResponseEntity>
                    {
                        new FieldResponseEntity { SectionId = 1, FieldId = 1, Text = "Textbox Test" },
                        new FieldResponseEntity { SectionId = 1, FieldId = 11, Text = "2" },
                        new FieldResponseEntity { SectionId = 1, FieldId = 13, Text = "1,3" }
                    }
                },
                new UserProgramEntity { UserId = 2, ProgramId = 1 }
            );
        }

        [TestMethod]
        public void GetSectionsForProgram()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());

            // Act
            var sections = controller.Get(1, 1);

            // Asserts
            Assert.IsNotNull(sections);
            Assert.AreEqual(2, sections.Count());
            Assert.AreEqual(1, sections.First().Id);
            Assert.AreEqual(2, sections.First().NextSectionId);
            Assert.IsNull(sections.First().PreviousSectionId);
            Assert.AreEqual(1, sections.Last().PreviousSectionId);
            Assert.IsNull(sections.Last().NextSectionId);
            Assert.IsNull(sections.Last().Fields);
        }

        [TestMethod]
        public void TestAddFields()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());

            // Act
            var sections = controller.Get(1, 1, true, 1);

            // Asserts
            Assert.IsNotNull(sections.Last().Fields);
            Assert.AreEqual(2, sections.Last().Fields.Count());
            Assert.AreEqual("Textbox Test", ((TextField)sections.First().Fields.First()).Value);
        }

        [TestMethod]
        public void GetSection()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "en" }
            });

            // Act
            var section = controller.Get(1, 1, 2);

            // Asserts
            Assert.IsNotNull(section);
            Assert.AreEqual("Section 2", section.Label);
            Assert.IsNotNull(section.Fields);
            Assert.AreEqual(2, section.Fields.Count);
            Assert.AreEqual(2, section.Fields.First().Id);
        }

        [TestMethod]
        public void TestFields()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());

            // Act
            var section = controller.Get(1, 1, 1, 1);

            // Asserts
            var field = section.Fields.ElementAt(0);
            Assert.IsTrue(field is TextField);
            Assert.IsTrue(((TextField)field).SubType is TextType.TextField);
            Assert.AreEqual("Textbox Test", ((TextField)field).Value);
            Assert.AreEqual(2000, ((TextField)field).MaxLength);

            field = section.Fields.ElementAt(1);
            Assert.IsTrue(field is TextField);
            Assert.IsTrue(((TextField)field).SubType is TextType.Password);
            Assert.AreEqual(2000, ((TextField)field).MaxLength);

            field = section.Fields.ElementAt(2);
            Assert.IsTrue(field is TextField);
            Assert.IsTrue(((TextField)field).SubType is TextType.Email);
            Assert.AreEqual(2000, ((TextField)field).MaxLength);

            field = section.Fields.ElementAt(3);
            Assert.IsTrue(field is TextField);
            Assert.IsTrue(((TextField)field).SubType is TextType.Color);
            Assert.AreEqual(2000, ((TextField)field).MaxLength);

            field = section.Fields.ElementAt(4);
            Assert.IsTrue(field is TextField);
            Assert.IsTrue(((TextField)field).SubType is TextType.Telephone);
            Assert.AreEqual(2000, ((TextField)field).MaxLength);

            field = section.Fields.ElementAt(5);
            Assert.IsTrue(field is NumberField);

            field = section.Fields.ElementAt(6);
            Assert.IsTrue(field is DateField);

            field = section.Fields.ElementAt(7);
            Assert.IsTrue(field is TextAreaField);
            Assert.IsTrue(((TextAreaField)field).SubType is TextAreaType.TextArea);
            Assert.AreEqual(2000, ((TextAreaField)field).MaxLength);
            Assert.AreEqual(10, ((TextAreaField)field).Rows);

            field = section.Fields.ElementAt(8);
            Assert.IsTrue(field is TextAreaField);
            Assert.IsTrue(((TextAreaField)field).SubType is TextAreaType.TinyMce);
            Assert.AreEqual(2000, ((TextAreaField)field).MaxLength);
            Assert.AreEqual(10, ((TextAreaField)field).Rows);

            field = section.Fields.ElementAt(9);
            Assert.IsTrue(field is TextAreaField);
            Assert.IsTrue(((TextAreaField)field).SubType is TextAreaType.Quill);
            Assert.AreEqual(2000, ((TextAreaField)field).MaxLength);
            Assert.AreEqual(10, ((TextAreaField)field).Rows);

            field = section.Fields.ElementAt(10);
            Assert.IsTrue(field is SelectField);
            Assert.AreEqual(3, ((SelectField)field).Values.Count);
            Assert.AreEqual(1, ((SelectField) field).Values.Count(v => v.IsSelected));
            Assert.AreEqual("2", ((SelectField)field).Values.First(v => v.IsSelected).Value);

            field = section.Fields.ElementAt(11);
            Assert.IsTrue(field is RadioGroupField);
            Assert.AreEqual(3, ((RadioGroupField)field).Values.Count);

            field = section.Fields.ElementAt(12);
            Assert.IsTrue(field is CheckboxGroupField);
            Assert.AreEqual(3, ((CheckboxGroupField)field).Values.Count);
            Assert.AreEqual(2, ((CheckboxGroupField)field).Values.Count(v => v.IsSelected));
            Assert.AreEqual("1", ((CheckboxGroupField)field).Values.First(v => v.IsSelected).Value);
            Assert.AreEqual("3", ((CheckboxGroupField)field).Values.Last(v => v.IsSelected).Value);

            field = section.Fields.ElementAt(13);
            Assert.IsTrue(field is ParagraphField);
            Assert.IsTrue(((ParagraphField)field).SubType is ParagraphType.P);

            field = section.Fields.ElementAt(14);
            Assert.IsTrue(field is ParagraphField);
            Assert.IsTrue(((ParagraphField)field).SubType is ParagraphType.Address);

            field = section.Fields.ElementAt(15);
            Assert.IsTrue(field is ParagraphField);
            Assert.IsTrue(((ParagraphField)field).SubType is ParagraphType.Blockquote);

            field = section.Fields.ElementAt(16);
            Assert.IsTrue(field is ParagraphField);
            Assert.IsTrue(((ParagraphField)field).SubType is ParagraphType.Canvas);

            field = section.Fields.ElementAt(17);
            Assert.IsTrue(field is ParagraphField);
            Assert.IsTrue(((ParagraphField)field).SubType is ParagraphType.Output);

            field = section.Fields.ElementAt(18);
            Assert.IsTrue(field is HeaderField);
            Assert.IsTrue(((HeaderField)field).SubType is HeaderType.H1);

            field = section.Fields.ElementAt(19);
            Assert.IsTrue(field is HeaderField);
            Assert.IsTrue(((HeaderField)field).SubType is HeaderType.H2);

            field = section.Fields.ElementAt(20);
            Assert.IsTrue(field is HeaderField);
            Assert.IsTrue(((HeaderField)field).SubType is HeaderType.H3);
        }

        [TestMethod]
        public void TestSubmitted()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());

            // Act
            var section = controller.Get(1, 1, 1, 2);

            // Asserts
            var field = section.Fields.ElementAt(0);
            Assert.IsTrue(string.IsNullOrEmpty(((TextField)field).Value));
        }

        [TestMethod]
        public void TestFrench()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());
            controller.SetHeaders(new Dictionary<string, string>
            {
                { "ACCEPT-LANGUAGE", "fr" }
            });

            // Act
            var section = controller.Get(1, 1, 2);
            var field = section.Fields.First();
            
            // Assert
            Assert.AreEqual("Section fr", section.Label);
            Assert.IsNull(section.Labels);
            Assert.AreEqual("Field fr", field.Label);
            Assert.IsNull(field.Labels);
        }

        [TestMethod]
        public void TestNoLanguage()
        {
            // Arrange
            var controller = new SectionsController(InitRepo());

            // Act
            var section = controller.Get(1, 1, 2);
            var field = section.Fields.First();

            // Assert
            Assert.IsNull(section.Label);
            Assert.AreEqual(2, section.Labels.Count());
            Assert.IsNull(field.Label);
            Assert.AreEqual(2, field.Labels.Count());
        }
    }
}
