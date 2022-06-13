using Utilities_aspnet.Chat;

namespace Utilities_aspnet.User;

public class UserEntity : IdentityUser {
    public bool Suspend { get; set; } = false;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public string? Activity { get; set; }
    public double? Wallet { get; set; } = 0;
    public bool? ShowContactInfo { get; set; }
    public DateTime? Birthdate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public IEnumerable<LocationEntity>? Location { get; set; }
    public IEnumerable<FormEntity>? FormBuilders { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<ColorEntity>? Colors { get; set; }
    public IEnumerable<ContactInformationEntity>? ContactInformation { get; set; }
    public IEnumerable<ReportEntity>? Reports { get; set; }
    public IEnumerable<ProductEntity>? Products { get; set; }
    public IEnumerable<DailyPriceEntity>? DailyPrices { get; set; }
    public IEnumerable<ProjectEntity>? Projects { get; set; }
    public IEnumerable<TutorialEntity>? Tutorials { get; set; }
    public IEnumerable<EventEntity>? Events { get; set; }
    public IEnumerable<AdEntity>? Ads { get; set; }
    public IEnumerable<CompanyEntity>? Companies { get; set; }
    public IEnumerable<TenderEntity>? Tenders { get; set; }
    public IEnumerable<ServiceEntity>? Services { get; set; }
    public IEnumerable<MagazineEntity>? Magazines { get; set; }
    public IEnumerable<SpecialityEntity>? Specialties { get; set; }
    public IEnumerable<CategoryEntity>? Favorites { get; set; }
}

[Table("Otps")]
public class OtpEntity : BaseEntity {
    public string OtpCode { get; set; }

    public UserEntity User { get; set; }
    public string UserId { get; set; }
}

public class GetMobileVerificationCodeForLoginDto {
    public string Mobile { get; set; }
    public bool SendSMS { get; set; }
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
    public string? AppEmail { get; set; }
    public string? Type { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public string? Region { get; set; }
    public string? Activity { get; set; }
    public double? Wallet { get; set; }
    public bool? ShowContactInfo { get; set; }
    public bool IsAdmin { get; set; }
    public bool? Suspend { get; set; }
    public DateTime? BirthDate { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public IEnumerable<IdTitleReadDto>? Colors { get; set; }
    public IEnumerable<IdTitleReadDto>? Specialties { get; set; }
    public IEnumerable<CategoryEntity>? Favorites { get; set; }
}

public class UserCreateUpdateDto {
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public string? Password { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public string? Activity { get; set; }
    public bool? Suspend { get; set; }
    public double? Wallet { get; set; }
    public bool? ShowContactInfo { get; set; }
    public DateTime? BirthDate { get; set; }
    public IEnumerable<Guid>? Colors { get; set; }
    public IEnumerable<Guid>? Specialties { get; set; }
    public IEnumerable<Guid>? Favorites { get; set; }
    public IEnumerable<int>? Locations { get; set; }
    public IEnumerable<UploadDto>? Media { get; set; }
}