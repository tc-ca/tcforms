using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY015_SUBMISSION_SECTION")]
    public class SubmissionSectionEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubmissionSectionEntity()
        {
        }

        [Key]
        [Column("SUBMISSION_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubmissionId { get; set; }

        [Key]
        [Column("SECTION_ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionId { get; set; }

        [Column("COMPLETE_IND")]
        public bool IsComplete { get; set; }

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

        public virtual SectionEntity Section { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldResponseEntity> FieldResponses { get; set; } = new HashSet<FieldResponseEntity>();

        public virtual SubmissionEntity Submission { get; set; }
    }
}
