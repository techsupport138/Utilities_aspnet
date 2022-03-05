using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
