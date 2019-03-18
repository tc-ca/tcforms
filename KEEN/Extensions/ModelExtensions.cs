using KEEN.Entities.Models;
using KEEN.Entities.Models.Fields;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEEN.Extensions
{
    public static class ModelExtensions
    {
        public static void FillResponse(this Field field, string response)
        {
            switch (field)
            {
                case TextField text:
                    text.Value = response;
                    break;
                case TextAreaField text:
                    text.Value = response;
                    break;
                case NumberField number:
                    number.Value = Convert.ToDouble(response);
                    break;
                case DateField date:
                    date.Value = Convert.ToDateTime(response);
                    break;
                case SelectField select:
                    select.Values.SelectValues(response);
                    break;
                case RadioGroupField radio:
                    radio.Values.SelectValues(response);
                    break;
                case CheckboxGroupField checkbox:
                    checkbox.Values.SelectValues(response.Split(','));
                    break;
            }
        }

        public static void SelectValues(this IEnumerable<SelectFieldValue> values, IEnumerable<string> responses)
        {
            values.Where(v => responses.Contains(v.Value)).ForEach(v => v.IsSelected = true);
        }

        public static void SelectValues(this IEnumerable<SelectFieldValue> values, string response)
        {
            values.SelectValues(new List<string>{ response });
        }
    }
}