using System.Runtime.Serialization;

namespace KEEN.Entities.Models.Fields.Types
{
    public enum TextAreaType
    {
        [EnumMember(Value = "textarea")]
        TextArea,
        [EnumMember(Value = "tinymce")]
        TinyMce,
        [EnumMember(Value = "quill")]
        Quill
    }
}
