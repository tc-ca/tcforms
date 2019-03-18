using System;
using System.Collections.Generic;
using System.Linq;
using KEEN.Domain.Entities;
using KEEN.Entities.Models;
using KEEN.Entities.Models.Fields;
using KEEN.Entities.Models.Fields.Types;
using Microsoft.Ajax.Utilities;

namespace KEEN.Extensions
{
    public static class MappingExtensions
    {
        public static string ToLabel(this ResourceEntity resource, IList<string> languages)
        {
            if (languages == null  || languages.Count == 0) return null;

            var labels = resource?.Labels?.Where(l => l.DateDeleted == null && l.Language.DateDeleted == null).ToList();
            if (labels == null || labels.Count == 0) return null;

            foreach (var lang in languages)
            {
                if (lang == "*") return labels.First().Text;
                var label = labels.FirstOrDefault(l => l.Language.LanguageTag.ToLower().StartsWith(lang.ToLower()));
                if (label != null) return label.Text;
            }

            return null;
        }

        public static IEnumerable<Label> ToLabels(this ResourceEntity resource, IList<string> languages = null)
        {
            if (languages != null && languages.Count > 0) return null;
            return resource?.Labels
                ?.Where(l => l.DateDeleted == null && l.Language.DateDeleted == null)
                .Select(l => new Label
                {
                    Text = l.Text,
                    Language = l.Language.LanguageTag
                });
        }

        public static Program ToProgram(this ProgramEntity program, IList<string> languages, bool addForms = true)
        {
            return new Program
            {
                Id = program.Id,
                Label = program.Resource.ToLabel(languages),
                Labels = program.Resource.ToLabels(languages),
                Forms =
                    addForms
                    ? program.Forms
                            .Where(f => f.DateDeleted == null)
                            .Select(f => f.ToForm(languages, false))
                            .ToList()
                    : null
            };
        }

        public static Form ToForm(this FormEntity form, IList<string> languages, bool addSections = true, bool addFields = false)
        {
            return new Form
            {
                Id = form.Id,
                ProgramId = form.ProgramId,
                Label = form.Resource.ToLabel(languages),
                Labels = form.Resource.ToLabels(languages),
                Sections =
                    addSections
                    ? form.Sections
                            .Where(s => s.DateDeleted == null)
                            .OrderBy(s => s.DisplaySort)
                            .Select(s => s.ToSection(languages, addFields))
                            .ToList()
                    : null
            };
        }

        public static Section ToSection(this SectionEntity section, IList<string> languages, bool addFields = true)
        {
            var orderedSections = section.Form.Sections
                .Where(s => s.DateDeleted == null)
                .OrderBy(s => s.DisplaySort)
                .ToList();
            var index = orderedSections.IndexOf(section);
            return new Section
            {
                Id = section.Id,
                NextSectionId = index == orderedSections.Count - 1 ? null : (int?)orderedSections[index + 1].Id,
                PreviousSectionId = index == 0 ? null : (int?)orderedSections[index - 1].Id,
                FormId = section.FormId,
                Label = section.Resource.ToLabel(languages),
                Labels = section.Resource.ToLabels(languages),
                Fields =
                    addFields
                    ? section.SectionFields
                            .Where(sf => sf.DateDeleted == null)
                            .OrderBy(sf => sf.DisplaySort)
                            .Select(sf => sf.Field.ToField(languages))
                            .ToList()
                    : null
            };
        }

        public static Field ToField(this FieldEntity field, IList<string> languages)
        {
            Field newField = null;

            var dict = new Dictionary<string, Func<Field>>
            {
                { "text", () => new TextField { SubType = TextType.TextField } },
                { "password", () => new TextField { SubType = TextType.Password } },
                { "email", () => new TextField { SubType = TextType.Email } },
                { "contact", () => new TextField { SubType = TextType.Email } },
                { "contact-cc", () => new TextField { SubType = TextType.Email } },
                { "color", () => new TextField { SubType = TextType.Color } },
                { "telephone", () => new TextField { SubType = TextType.Telephone } },
                { "number", () => new NumberField() },
                { "date", () => new DateField() },
                { "textarea", () => new TextAreaField { SubType = TextAreaType.TextArea } },
                { "tinymce", () => new TextAreaField { SubType = TextAreaType.TinyMce } },
                { "quill", () => new TextAreaField { SubType = TextAreaType.Quill } },
                { "select", () => new SelectField { Values = new List<SelectFieldValue>{new SelectFieldValue()}.Concat(field.FieldSet.ToSelectFieldValues(languages)).ToList() } },
                { "radio", () => new RadioGroupField { Values = field.FieldSet.ToSelectFieldValues(languages) } },
                { "checkbox", () => new CheckboxGroupField { Values = field.FieldSet.ToSelectFieldValues(languages) } },
                { "p", () => new ParagraphField { SubType = ParagraphType.P } },
                { "address", () => new ParagraphField { SubType = ParagraphType.Address } },
                { "blockquote", () => new ParagraphField { SubType = ParagraphType.Blockquote } },
                { "canvas", () => new ParagraphField { SubType = ParagraphType.Canvas } },
                { "output", () => new ParagraphField { SubType = ParagraphType.Output } },
                { "h1", () => new HeaderField { SubType = HeaderType.H1 } },
                { "h2", () => new HeaderField { SubType = HeaderType.H2 } },
                { "h3", () => new HeaderField { SubType = HeaderType.H3 } }
            };
            if (dict.ContainsKey(field.FieldTypeCode)) newField = dict[field.FieldTypeCode]();

            if (newField != null)
            {
                newField.Id = field.Id;
                newField.Label = field.Resource.ToLabel(languages);
                newField.Labels = field.Resource.ToLabels(languages);
                newField.CssClasses =
                    newField.CssClasses.IsNullOrWhiteSpace()
                    ? field.CssClasses
                    : newField.CssClasses + " " + field.CssClasses;
            }

            return newField;
        }

        public static IList<SelectFieldValue> ToSelectFieldValues(this FieldSetEntity fieldSet, IList<string> languages)
        {
            return fieldSet?.Values?.Where(v => v.DateDeleted == null).OrderBy(v => v.DisplaySort).Select(v => v.ToSelectFieldValue(languages)).ToList();
        }

        public static SelectFieldValue ToSelectFieldValue(this FieldSetValueEntity value, IList<string> languages)
        {
            return new SelectFieldValue
            {
                Label = value.Resource.ToLabel(languages),
                Labels = value.Resource.ToLabels(languages),
                Value = value.Code
            };
        }
    }
}