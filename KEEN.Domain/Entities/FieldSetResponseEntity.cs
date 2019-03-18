using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY016_FIELD_SET_RESPONSE")]
    public class FieldSetResponseEntity
    {
        [Key]
        [Column("SUBMISSION_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubmissionId { get; set; }

        [Key]
        [Column("SECTION_ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionId { get; set; }

        [Key]
        [Column("FIELD_ID", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FieldId { get; set; }

        [Key]
        [Column("FIELD_SET_ID", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FieldSetId { get; set; }

        [Key]
        [Column("FIELD_SET_VALUE_CD", Order = 4)]
        [StringLength(20)]
        public string FieldSetValueCd { get; set; }

        [Column("SELECTED_IND")]
        public int SelectedInd { get; set; }

        [NotMapped]
        public bool IsSelected => SelectedInd == 1;

        [Column("DATE_CREATED_DTE")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_LAST_UPDATE_DTE")]
        public DateTime? DateLastUpdated { get; set; }

        [Column("DATE_DELETED_DTE")]
        public DateTime? DateDeleted { get; set; }

        [Column("USER_CREATED_ID")]
        public int UserCreatedId { get; set; }

        [Column("USER_LAST_UPDATE_ID")]
        public int? UserLastUpdateId { get; set; }

        public virtual FieldSetEntity FieldSet { get; set; }

        public virtual FieldSetValueEntity FieldSetValue { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        public virtual SectionEntity Section { get; set; }

        public virtual FieldEntity Field { get; set; }

        public virtual SubmissionEntity Submission { get; set; }
    }
}
