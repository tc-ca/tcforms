using System.Collections.Generic;
using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KEEN.Entities.Models
{
    public class Field
    {
        [JsonProperty(PropertyName = "name")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonProperty(PropertyName =  "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType Type { get; set; }

        [JsonProperty(PropertyName = "className")]
        public string CssClasses { get; set; }
    }
}
