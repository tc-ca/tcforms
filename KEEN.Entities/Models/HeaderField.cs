using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models
{
    public class HeaderField : Field
    {
        public HeaderField()
        {
            Type = FieldType.Header;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderType SubType { get; set; }
    }
}
