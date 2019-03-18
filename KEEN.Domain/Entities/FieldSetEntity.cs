using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.TY007_FIELD_SET")]
    public class FieldSetEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FieldSetEntity()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("FIELD_SET_ID")]
        public int Id { get; set; }

        [Column("RESOURCE_ID")]
        public int? ResourceId { get; set; }

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
        public virtual ICollection<FieldSetValueEntity> Values { get; set; } = new HashSet<FieldSetValueEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldEntity> Fields { get; set; } = new HashSet<FieldEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldSetResponseEntity> FieldSetResponses { get; set; } = new HashSet<FieldSetResponseEntity>();
    }
}
