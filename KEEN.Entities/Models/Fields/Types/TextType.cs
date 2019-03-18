using System.Runtime.Serialization;

namespace KEEN.Entities.Models.Fields.Types
{
    public enum TextType
    {
        [EnumMember(Value = "text")]
        TextField,
        [EnumMember(Value = "password")]
        Password,
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "color")]
        Color,
        [EnumMember(Value = "tel")]
        Telephone
    }
}
