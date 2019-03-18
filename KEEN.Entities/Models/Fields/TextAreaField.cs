using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models.Fields
{
    public class TextAreaField : Field
    {
        public TextAreaField()
        {
            Type = FieldType.TextArea;
            CssClasses = "form-control";
            Rows = 10;
            MaxLength = 2000;
        }

        [JsonProperty(PropertyName = "subtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TextAreaType SubType { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public int Rows { get; set; }
        
        [JsonProperty(PropertyName = "maxlength")]
        public int MaxLength { get; set; }
    }
}
