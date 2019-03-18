using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY014_SUBMISSION")]
    public class SubmissionEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubmissionEntity()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SUBMISSION_ID")]
        public int Id { get; set; }

        [Column("FORM_ID")]
        public int FormId { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

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

        public virtual UserEntity User { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        public virtual FormEntity Form { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubmissionSectionEntity> SubmissionSections { get; set; } = new HashSet<SubmissionSectionEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldSetResponseEntity> FieldSetResponses { get; set; } = new HashSet<FieldSetResponseEntity>();
    }
}
