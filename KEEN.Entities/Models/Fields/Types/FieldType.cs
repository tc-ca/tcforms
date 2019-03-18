using System.Runtime.Serialization;

namespace KEEN.Entities.Models.Fields.Types
{
    public enum FieldType
    {
        [EnumMember(Value = "header")]
        Header,
        [EnumMember(Value = "paragraph")]
        Paragraph,
        [EnumMember(Value = "checkbox-group")]
        CheckboxGroup,
        [EnumMember(Value = "date")]
        Date,
        [EnumMember(Value = "number")]
        Number,
        [EnumMember(Value = "radio-group")]
        RadioGroup,
        [EnumMember(Value = "select")]
        Select,
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "textarea")]
        TextArea
    }
}
