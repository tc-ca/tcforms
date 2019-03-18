using System.Collections.Generic;
using Newtonsoft.Json;

namespace KEEN.Entities.Models
{
    public class Program
    {
        [JsonProperty(PropertyName = "programId")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonProperty(PropertyName = "forms")]
        public IList<Form> Forms { get; set; }
    }
}
