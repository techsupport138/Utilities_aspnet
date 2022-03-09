using System.ComponentModel.DataAnnotations;

namespace Utilities_aspnet.User.Dtos
{
    public class RequestVerificationCodeDto
    {
        [Required]
        public string Mobile { get; set; } = null!;
    }

    public class VerifyMobileForLoginDto
    {
        [Required]
        public string Mobile { get; set; } = null!;
        [Required]
        public string VerificationCode { get; set; } = null!;
    }
}
