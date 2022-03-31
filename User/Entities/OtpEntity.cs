using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.User.Entities;

public class OtpEntity : BaseEntity {
    [Required]
    [StringLength(4)]
    public string OtpCode { get; set; } = null!;

    public UserEntity User { get; set; } = null!;

    [Required]
    [StringLength(450)]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
}