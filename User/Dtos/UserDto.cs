using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Dtos;

namespace Utilities_aspnet.User.Dtos
{
    public class RegisterWithMobileDto
    {
        [Required]
        public string Mobile { get; set; } = null!;
    }

    public class LoginWithMobileDto
    {
        [Required]
        public string Mobile { get; set; } = null!;

        [Required]
        public string VerificationCode { get; set; } = null!;
    }

    public class RegisterWithEmailDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }

    public class LoginWithEmailDto
    {
        [Required]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(256)]
        public string Password { get; set; } = null!;
    }

    public class ChangePasswordDto
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
    }

    public class UserReadDto
    {
        public string? Token { get; set; }
        public string Link { get; set; }
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Bio { get; set; }
        public long Point { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<MediaDto>? Media { get; set; }
    }


    public class AutoMapperUsers : Profile
    {
        public AutoMapperUsers()
        {
            CreateMap<UserEntity, UserReadDto>().ForMember(dest =>
                    dest.Link,
                opt => opt.MapFrom(src => "http://95.216.63.209:5012/api/user/" + src.Id)).ReverseMap();
        }
    }
}