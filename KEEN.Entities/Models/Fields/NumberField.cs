using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;

namespace KEEN.Entities.Models.Fields
{
    public class NumberField : Field
    {
        public NumberField()
        {
            Type = FieldType.Number;
            CssClasses = "form-control";
        }

        [JsonProperty(PropertyName = "min")]
        public int? Minimum { get; set; }

        [JsonProperty(PropertyName = "max")]
        public int? Maximum { get; set; }

        [JsonProperty(PropertyName = "value")]
        public double? Value { get; set; }
    }
}
