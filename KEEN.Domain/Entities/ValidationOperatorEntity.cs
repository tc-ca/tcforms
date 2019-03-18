using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.TY006_VALIDATION_OPERATOR")]
    public class ValidationOperatorEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ValidationOperatorEntity()
        {
        }

        [Key]
        [StringLength(20)]
        [Column("VALIDATION_OPERATOR_CD")]
        public string Code { get; set; }

        [Column("RESOURCE_ID")]
        public int ResourceId { get; set; }

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

        public virtual ResourceEntity Resource { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValidationRuleEntity> ValidationRules { get; set; } = new HashSet<ValidationRuleEntity>();
    }
}
