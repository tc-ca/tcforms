using System.Collections.Generic;
using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;

namespace KEEN.Entities.Models.Fields
{
    public class RadioGroupField : Field
    {
        public RadioGroupField()
        {
            Type = FieldType.RadioGroup;
        }

        [JsonProperty(PropertyName = "values")]
        public IList<SelectFieldValue> Values { get; set; }
    }
}
