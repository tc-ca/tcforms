namespace NTC.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NTC.YY001_FORM_USER")]
    public partial class UserEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserEntity()
        {
        }

        [Key]
        [Column("USER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleEntity> RoleCreated { get; set; } = new HashSet<RoleEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleEntity> RoleLastUpdated { get; set; } = new HashSet<RoleEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEntity> UsersCreated { get; set; } = new HashSet<UserEntity>();

        public virtual UserEntity UserCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEntity> UsersLastUpdated { get; set; } = new HashSet<UserEntity>();

        public virtual UserEntity UserLastUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteUserEntity> RemoteUserCreated { get; set; } = new HashSet<RemoteUserEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteUserEntity> RemoteUserLastUpdated { get; set; } = new HashSet<RemoteUserEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleEntity> UserRoleUser { get; set; } = new HashSet<UserRoleEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleEntity> UserRoleCreated { get; set; } = new HashSet<UserRoleEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleEntity> UserRoleLastUpdated { get; set; } = new HashSet<UserRoleEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMappingReferenceEntity> UserMappingReferenceUser { get; set; } = new HashSet<UserMappingReferenceEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMappingReferenceEntity> UserMappingReferenceCreated { get; set; } = new HashSet<UserMappingReferenceEntity>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMappingReferenceEntity> UserMappingReferenceLastUpdated { get; set; } = new HashSet<UserMappingReferenceEntity>();
    }
}
