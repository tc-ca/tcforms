using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models.Fields
{
    public class ParagraphField : Field
    {
        public ParagraphField()
        {
            Type = FieldType.Paragraph;
        }

        [JsonProperty(PropertyName = "subtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ParagraphType SubType { get; set; }
    }
}
