using System.Collections.Generic;
using Newtonsoft.Json;

namespace KEEN.Entities.Models
{
    public class Section
    {
        [JsonProperty(PropertyName = "sectionId")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "nextSectionId")]
        public int? NextSectionId { get; set; }

        [JsonProperty(PropertyName = "prevSectionId")]
        public int? PreviousSectionId { get; set; }

        [JsonProperty(PropertyName = "formId")]
        public int FormId { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public IList<Field> Fields { get; set; }
    }
}
