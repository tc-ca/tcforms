using System.Runtime.Serialization;

namespace KEEN.Entities.Models.Fields.Types
{
    public enum HeaderType
    {
        [EnumMember(Value = "h1")]
        H1,
        [EnumMember(Value = "h2")]
        H2,
        [EnumMember(Value = "h3")]
        H3
    }
}
