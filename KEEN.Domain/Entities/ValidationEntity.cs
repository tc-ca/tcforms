using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY006_VALIDATION")]
    public class ValidationEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ValidationEntity()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VALIDATION_ID")]
        public int Id { get; set; }

        [Column("ADMIN_RESOURCE_ID")]
        public int AdminResourceId { get; set; }

        [Column("ERROR_RESOURCE_ID")]
        public int ErrorResourceId { get; set; }

        [Column("ERROR_MESSAGE_RESOURCE_ID")]
        public int ErrorMessageResourceId { get; set; }

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

        public virtual ResourceEntity AdminResource { get; set; }

        public virtual ResourceEntity ErrorResource { get; set; }

        public virtual ResourceEntity ErrorMessageResource { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValidationRuleEntity> ValidationRules { get; set; } = new HashSet<ValidationRuleEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldValidationEntity> FieldValidations { get; set; } = new HashSet<FieldValidationEntity>();
    }
}
