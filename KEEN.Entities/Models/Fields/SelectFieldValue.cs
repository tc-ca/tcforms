using System.Collections.Generic;
using Newtonsoft.Json;

namespace KEEN.Entities.Models.Fields
{
    public class SelectFieldValue
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "selected")]
        public bool IsSelected { get; set; }
    }
}
