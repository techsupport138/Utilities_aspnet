namespace Utilities_aspnet.User.Entities {
    public class UserRoleEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}