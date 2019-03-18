using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY008_XREF_FIELD_VALIDATION")]
    public class FieldValidationEntity
    {
        [Key]
        [Column("FIELD_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FieldId { get; set; }

        [Key]
        [Column("VALIDATION_ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ValidationId { get; set; }

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

        public virtual ValidationEntity Validation { get; set; }
    }
}
