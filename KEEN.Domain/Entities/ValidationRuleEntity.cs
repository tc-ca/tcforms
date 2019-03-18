using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KEEN.Domain.Entities
{
    [Table("FAST.YY007_VALIDATION_RULE")]
    public class ValidationRuleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VALIDATION_RULE_ID")]
        public int Id { get; set; }

        [Column("VALIDATION_ID")]
        public int ValidationId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("VALIDATION_ATTRIBUTE_CD")]
        public string ValidationAttributeCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column("VALIDATION_OPERATOR_CD")]
        public string ValidationOperatorCode { get; set; }

        [StringLength(250)]
        [Column("VALUE_TXT")]
        public string Value { get; set; }

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

        public virtual ValidationAttributeEntity ValidationAttribute { get; set; }

        public virtual ValidationOperatorEntity ValidationOperator { get; set; }

        public virtual UserEntity UserCreated { get; set; }

        public virtual UserEntity UserLastUpdate { get; set; }

        public virtual ValidationEntity Validation { get; set; }
    }
}
