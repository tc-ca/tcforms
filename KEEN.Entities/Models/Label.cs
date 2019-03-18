using Newtonsoft.Json;

namespace KEEN.Entities.Models
{
    public class Label
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
    }
}
