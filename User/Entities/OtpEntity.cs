namespace Utilities_aspnet.User.Entities;

public class OtpEntity : BaseEntity {
    [Required]
    [StringLength(4)]
    public string OtpCode { get; set; } 

    public UserEntity User { get; set; } 

    [Required]
    [StringLength(450)]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } 
}