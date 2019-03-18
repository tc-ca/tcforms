using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.TY008_FIELD_SET_VALUE")]
    public class FieldSetValueEntity
    {
        [Key]
        [Column("FIELD_SET_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FieldSetId { get; set; }

        [Key]
        [Column("FIELD_SET_VALUE_CD", Order = 1)]
        [StringLength(20)]
        public string Code { get; set; }

        [Column("DISPLAY_SRT")]
        public byte DisplaySort { get; set; }

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

        public virtual FieldSetEntity FieldSet { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldSetResponseEntity> FieldSetResponses { get; set; } = new HashSet<FieldSetResponseEntity>();
    }
}
