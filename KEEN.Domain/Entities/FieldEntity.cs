using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY005_FIELD")]
    public class FieldEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FieldEntity()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("FIELD_ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("FIELD_TYPE_CD")]
        public string FieldTypeCode { get; set; }

        [Column("RESOURCE_ID")]
        public int ResourceId { get; set; }

        [Column("FIELD_SET_ID")]
        public int? FieldSetId { get; set; }

        [StringLength(250)]
        [Column("CSS_CLASSES_TXT")]
        public string CssClasses { get; set; }

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

        public virtual FieldTypeEntity FieldType { get; set; }

        public virtual FieldSetEntity FieldSet { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldValidationEntity> FieldValidations { get; set; } = new HashSet<FieldValidationEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SectionFieldEntity> SectionFields { get; set; } = new HashSet<SectionFieldEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldResponseEntity> FieldResponses { get; set; } = new HashSet<FieldResponseEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldSetResponseEntity> FieldSetResponses { get; set; } = new HashSet<FieldSetResponseEntity>();
    }
}
