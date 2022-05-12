using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Dtos;

namespace Utilities_aspnet.User.Dtos {
    public class GetMobileVerificationCodeForLoginDto {
        [Required]
        public string Mobile { get; set; }
    }

    public class VerifyMobileForLoginDto {
        [Required]
        public string Mobile { get; set; }

        [Required]
        public string VerificationCode { get; set; }
    }

    public class RegisterWithEmailDto {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool Keep { get; set; } = true;
    }

    public class RegisterFormWithEmailDto {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool Keep { get; set; } = true;

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginWithEmailDto {
        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool Keep { get; set; } = true;
    }

    public class ChangePasswordDto {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }

    public class UserReadDto {
        public string? Token { get; set; }
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Bio { get; set; }
        public string? AppUserName { get; set; }
        public string? AppPhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public IEnumerable<MediaDto>? Media { get; set; }

        public List<IdTitleDto> Colors { get; set; }
        public List<IdTitleDto> Specialties { get; set; }
        public List<IdTitleDto> Favorites { get; set; }
    }

    public class UpdateProfileDto {
        public List<Guid> Colors { get; set; }
        public List<Guid> Specialties { get; set; }
        public List<Guid> Favorites { get; set; }
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Headline { get; set; }
        public string? AppUserName { get; set; }
        public string? AppPhoneNumber { get; set; }

        public IEnumerable<ContactInformationCreateDto>? ContactInformation { get; set; }
    }

    public class AutoMapperUsers : Profile {
        public AutoMapperUsers() {
            CreateMap<UserEntity, UserReadDto>().ForMember(x => x.Colors, x => x.MapFrom(x => x.Colors.Select(x => x.Color.Id).ToList()));
            CreateMap<UserEntity, UserReadDto>()
                .ForMember(x => x.Favorites, x => x.MapFrom(x => x.Favorites.Select(x => x.Favorite.Id).ToList()));
            CreateMap<UserEntity, UserReadDto>()
                .ForMember(x => x.Specialties, x => x.MapFrom(x => x.Specialties.Select(x => x.Specialty.Id).ToList()));
            CreateMap<UserEntity, UserReadDto>().ForMember(x => x.Media, x => x.MapFrom(x => x.Media));
        }
    }
}