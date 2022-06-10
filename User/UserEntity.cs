namespace Utilities_aspnet.User;

public class UserEntity : IdentityUser {
    public bool Suspend { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public double? Wallet { get; set; } = 0;
    public DateTime? Birthdate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public List<LocationEntity>? Location { get; set; }
    public List<FormEntity>? FormBuilders { get; set; }
    public List<MediaEntity>? Media { get; set; }
    public List<ColorEntity>? Colors { get; set; }
    public List<FavoriteEntity>? Favorites { get; set; }
    public List<ContactInformationEntity>? ContactInformation { get; set; }
    public List<ReportEntity>? Reports { get; set; }
    public List<ProductEntity>? Products { get; set; }
    public List<DailyPriceEntity>? DailyPrices { get; set; }
    public List<ProjectEntity>? Projects { get; set; }
    public List<TutorialEntity>? Tutorials { get; set; }
    public List<EventEntity>? Events { get; set; }
    public List<AdEntity>? Ads { get; set; }
    public List<CompanyEntity>? Companies { get; set; }
    public List<TenderEntity>? Tenders { get; set; }
    public List<ServiceEntity>? Services { get; set; }
    public List<MagazineEntity>? Magazines { get; set; }
    public List<SpecialityEntity>? Specialties { get; set; }
}

[Table("Otps")]
public class OtpEntity : BaseEntity {
    public string OtpCode { get; set; }

    public UserEntity User { get; set; }
    public string UserId { get; set; }
}

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
    public string? AppEmail { get; set; }
    public DateTime? BirthDate { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public bool IsAdmin { get; set; }

    public List<IdTitleReadDto>? Colors { get; set; }
    public List<IdTitleReadDto>? Specialties { get; set; }
    public List<IdTitleReadDto>? Favorites { get; set; }
}

public class UpdateProfileDto {
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public bool? Suspend { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public double? Wallet { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<Guid>? Colors { get; set; }
    public List<Guid>? Specialties { get; set; }
    public List<Guid>? Favorites { get; set; }
    public List<int>? Locations { get; set; }
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
    public DateTime? BirthDate { get; set; }
}