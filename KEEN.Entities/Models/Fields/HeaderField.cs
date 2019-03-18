using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models.Fields
{
    public class HeaderField : Field
    {
        public HeaderField()
        {
            Type = FieldType.Header;
        }

        [JsonProperty(PropertyName = "subtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderType SubType { get; set; }
    }
}
