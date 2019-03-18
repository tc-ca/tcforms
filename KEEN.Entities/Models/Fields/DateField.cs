using System;
using KEEN.Entities.Models.Fields.Types;
using Newtonsoft.Json;

namespace KEEN.Entities.Models.Fields
{
    public class DateField : Field
    {
        public DateField()
        {
            Type = FieldType.Date;
            CssClasses = "form-control";
        }

        [JsonProperty(PropertyName = "value")]
        public DateTime? Value { get; set; }
    }
}
