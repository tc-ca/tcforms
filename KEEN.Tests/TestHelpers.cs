using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using KEEN.Domain.Entities;
using MoreLinq;

namespace KEEN.Tests
{
    internal static class TestHelpers
    {
        public static ResourceEntity ToResource(this IDictionary<string, string> labels)
        {
            var resource = new ResourceEntity {Id = 1};
            labels.ForEach((kvp, i) =>
            {
                var language = new LanguageEntity
                {
                    Id = i,
                    LanguageTag = kvp.Key
                };
                var label = new LabelEntity
                {
                    ResourceId = resource.Id,
                    LanguageId = language.Id,
                    Language = language,
                    Text = kvp.Value
                };
                resource.Labels.Add(label);
            });

            return resource;
        }

        public static FieldSetEntity ToFieldSet(this IDictionary<string, string> values)
        {
            var set = new FieldSetEntity { Id = 1 };

            values.ForEach(kvp => set.Values.Add(new FieldSetValueEntity
            {
                FieldSetId = set.Id,
                Code = kvp.Key,
                Resource = new Dictionary<string, string> { { "en", kvp.Value } }.ToResource()
            }));

            return set;
        }

        public static SectionEntity AddSection(this FormEntity form, SectionEntity section)
        {
            section.Form = form;
            section.FormId = form.Id;
            form.Sections.Add(section);
            return section;
        }

        public static FormEntity MapSections(this FormEntity form)
        {
            form.Sections.ForEach(section =>
            {
                section.FormId = form.Id;
                section.Form = form;
            });
            return form;
        }

        public static void SetHeaders(this ApiController controller, IEnumerable<KeyValuePair<string, string>> headers)
        {
            var context = new HttpControllerContext();
            var request = new HttpRequestMessage();
            headers.ForEach(kvp => request.Headers.Add(kvp.Key, kvp.Value));
            context.Request = request;
            controller.ControllerContext = context;
        }

        public static SectionFieldEntity CreateField(int sectionId, int fieldId, string type, FieldSetEntity fieldSet = null, ResourceEntity resource = null, byte? sort = null)
        {
            return new SectionFieldEntity
            {
                SectionId = sectionId,
                FieldId = fieldId,
                DisplaySort = sort ?? Convert.ToByte(fieldId),
                Field = new FieldEntity
                {
                    Id = fieldId,
                    FieldTypeCode = type,
                    FieldSet = fieldSet,
                    Resource = resource
                }
            };
        }
    }
}
