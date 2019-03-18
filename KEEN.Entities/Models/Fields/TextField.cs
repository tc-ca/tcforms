using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models.Fields
{
    public class TextField : Field
    {
        public TextField()
        {
            Type = FieldType.Text;
            CssClasses = "form-control";
            MaxLength = 2000;
        }

        [JsonProperty(PropertyName = "subtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TextType SubType { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "maxlength")]
        public int MaxLength { get; set; }
    }
}
