namespace Utilities_aspnet.User.Entities;

[Table("Otps")]
public class OtpEntity : BaseEntity {
    public string OtpCode { get; set; }

    public UserEntity User { get; set; }
    public string UserId { get; set; }
}