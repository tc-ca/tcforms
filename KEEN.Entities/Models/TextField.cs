using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models
{
    public class TextField : Field
    {
        public TextField()
        {
            Type = FieldType.Text;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TextType SubType { get; set; }
    }
}
