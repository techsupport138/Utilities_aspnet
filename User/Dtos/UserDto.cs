using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace Utilities_aspnet.User.Dtos;

public class GetMobileVerificationCodeForLoginDto {
    [Required]
    public string Mobile { get; set; }

    public bool SendSMS { get; set; } = false;
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

    public List<IdTitleReadDto> Colors { get; set; }
    public List<IdTitleReadDto> Specialties { get; set; }
    public List<IdTitleReadDto> Favorites { get; set; }
}

public class UpdateProfileDto {
    public UpdateProfileDto() {
        Colors = new List<Guid>();
        Specialties = new List<Guid>();
        Favorites = new List<Guid>();
        Locations = new List<int>();
        ContactInformation = new List<IdTitleCreateUpdateDto>();
    }

    public List<Guid> Colors { get; set; }
    public List<Guid> Specialties { get; set; }
    public List<Guid> Favorites { get; set; }
    public List<int> Locations { get; set; }
    public MediaDto? Media { get; set; }
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? AppEmail { get; set; }

    public List<IdTitleCreateUpdateDto>? ContactInformation { get; set; }
}

public class CreateProfileDto {
    public CreateProfileDto() {
        Colors = new List<Guid>();
        Specialties = new List<Guid>();
        Favorites = new List<Guid>();
        Locations = new List<int>();
        ContactInformation = new List<IdTitleCreateUpdateDto>();
    }

    public List<Guid> Colors { get; set; }
    public List<Guid> Specialties { get; set; }
    public List<Guid> Favorites { get; set; }
    public List<int> Locations { get; set; }
    public MediaDto? Media { get; set; }
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? AppEmail { get; set; }

    public List<IdTitleCreateUpdateDto>? ContactInformation { get; set; }
}