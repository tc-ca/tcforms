using System.Collections.Generic;
using Newtonsoft.Json;

namespace KEEN.Entities.Models
{
    public class Form
    {
        [JsonProperty(PropertyName = "formId")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "programId")]
        public int ProgramId { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonProperty(PropertyName = "sections")]
        public IList<Section> Sections { get; set; }
    }
}
