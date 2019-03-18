using System.Collections.Generic;
using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;

namespace KEEN.Entities.Models.Fields
{
    public class CheckboxGroupField : Field
    {
        public CheckboxGroupField()
        {
            Type = FieldType.CheckboxGroup;

        }

        [JsonProperty(PropertyName = "values")]
        public IList<SelectFieldValue> Values { get; set; }
    }
}
