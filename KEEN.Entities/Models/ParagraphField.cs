using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models
{
    public class ParagraphField : Field
    {
        public ParagraphField()
        {
            Type = FieldType.Paragraph;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public ParagraphType SubType { get; set; }
    }
}
