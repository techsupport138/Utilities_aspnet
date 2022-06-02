namespace Utilities_aspnet.User.Dtos;

public class GetMobileVerificationCodeForLoginDto {
    public string Mobile { get; set; }
    public bool SendSMS { get; set; } = false;
}

public class VerifyMobileForLoginDto {
    public string Mobile { get; set; }
    public string VerificationCode { get; set; }
}

public class RegisterWithEmailDto {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ReturnUrl { get; set; }
    public bool Keep { get; set; } = true;
}

public class RegisterFormWithEmailDto {
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ReturnUrl { get; set; }
    public bool Keep { get; set; } = true;
    public string ConfirmPassword { get; set; }
}

public class LoginWithEmailDto {
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ReturnUrl { get; set; }
    public bool Keep { get; set; } = true;
}

public class ChangePasswordDto {
    public string OldPassword { get; set; }
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
    public bool IsAdmin { get; set; }

    public List<IdTitleReadDto>? Colors { get; set; }
    public List<IdTitleReadDto>? Specialties { get; set; }
    public List<IdTitleReadDto>? Favorites { get; set; }
}

public class UpdateProfileDto {
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public bool? Suspend { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public MediaDto? Media { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<Guid>? Colors { get; set; }
    public List<Guid>? Specialties { get; set; }
    public List<Guid>? Favorites { get; set; }
    public List<int>? Locations { get; set; }
    public List<IdTitleCreateUpdateDto>? ContactInformation { get; set; }
}

public class CreateUserDto {
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public string? Password { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public List<Guid>? Colors { get; set; }
    public List<Guid>? Specialties { get; set; }
    public List<Guid>? Favorites { get; set; }
    public List<int>? Locations { get; set; }
    public MediaDto? Media { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<IdTitleCreateUpdateDto>? ContactInformation { get; set; }
}