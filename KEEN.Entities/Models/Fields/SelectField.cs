using System.Collections.Generic;
using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;

namespace KEEN.Entities.Models.Fields
{
    public class SelectField : Field
    {
        public SelectField()
        {
            Type = FieldType.Select;
            CssClasses = "form-control";
        }

        [JsonProperty(PropertyName = "values")]
        public IList<SelectFieldValue> Values { get; set; }
    }
}