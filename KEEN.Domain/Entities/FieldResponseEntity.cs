using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY012_FIELD_RESPONSE")]
    public class FieldResponseEntity
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

        [StringLength(2000)]
        [Column("FIELD_RESPONSE_TXT")]
        public string Text { get; set; }

        [Column("DOCUMENT_ID")]
        public int? DocumentId { get; set; }

        [Column("DATE_CREATED_DTE")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_LAST_UPDATE_DTE")]
        public DateTime? DateLastUpdate { get; set; }

        [Column("DATE_DELETED_DTE")]
        public DateTime? DateDeleted { get; set; }

        [Column("USER_CREATED_ID")]
        public int UserCreatedId { get; set; }

        [Column("USER_LAST_UPDATE_ID")]
        public int? UserLastUpdateId { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        public virtual FieldEntity Field { get; set; }

        public virtual DocumentEntity Document { get; set; }

        public virtual SubmissionSectionEntity SubmissionSection { get; set; }
    }
}
