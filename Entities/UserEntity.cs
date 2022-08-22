namespace Utilities_aspnet.Entities;

public class UserEntity : IdentityUser
{
    public bool Suspend { get; set; } = false;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? Headline { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public string? Instagram { get; set; }
    public string? Telegram { get; set; }
    public string? WhatsApp { get; set; }
    public string? LinkedIn { get; set; }
    public string? Dribble { get; set; }
    public string? SoundCloud { get; set; }
    public string? Pinterest { get; set; }
    public string? Website { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public string? Activity { get; set; }
    public string? Color { get; set; }
    public string? State { get; set; }
    public double? Wallet { get; set; } = 0;
    public double? Point { get; set; } = 0;
    public bool? ShowContactInfo { get; set; }
    public DateTime? Birthdate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? AccessLevel { get; set; }
    public string? Access { get; set; }

    [NotMapped]
    public bool IsFollowing { get; set; } = false;

    [NotMapped]
    public int? CountProducts { get; set; }

    [NotMapped]
    public int? CountFollowers { get; set; }

    [NotMapped]
    public int? CountFollowing { get; set; }

    [NotMapped]
    public bool IsAdmin { get; set; }

    [NotMapped]
    public string? Token { get; set; }

    [NotMapped]
    public GrowthRateReadDto? GrowthRate { get; set; }

    public int? GenderId { get; set; }
    public GenderEntity? Gender { get; set; }
    public IEnumerable<FormEntity>? FormBuilders { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<ProductEntity>? Products { get; set; }
    public IEnumerable<CategoryEntity>? Categories { get; set; }
    public IEnumerable<TransactionEntity>? Transactions { get; set; }
    public IEnumerable<TeamEntity>? Teams { get; set; }

    public IEnumerable<LikeCommentEntity>? LikeComments { get; set; }
}

[Table("Otps")]
public class OtpEntity : BaseEntity
{
    public string OtpCode { get; set; }

    public UserEntity User { get; set; }
    public string UserId { get; set; }
}

[Table("Teams")]
public class TeamEntity : BaseEntity
{
    public string? UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid? ProductId { get; set; }
    public ProductEntity? Product { get; set; }
}

public class TeamReadDto
{
    public Guid Id { get; set; }
    public UserMinimalReadDto? User { get; set; }
}

public class GetMobileVerificationCodeForLoginDto
{
    public string Mobile { get; set; }
    public bool SendSMS { get; set; }
}

public class VerifyMobileForLoginDto
{
    public string Mobile { get; set; }
    public string VerificationCode { get; set; }
}

public class RegisterDto
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public bool SendSMS { get; set; }
}

public class LoginWithPasswordDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UserReadDto
{
    public string? Token { get; set; }
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? UserName { get; set; }
    public string? Bio { get; set; }
    public string? AppUserName { get; set; }
    public string? AppPhoneNumber { get; set; }
    public string? AppEmail { get; set; }
    public string? Instagram { get; set; }
    public string? Telegram { get; set; }
    public string? WhatsApp { get; set; }
    public string? LinkedIn { get; set; }
    public string? Dribble { get; set; }
    public string? SoundCloud { get; set; }
    public string? Pinterest { get; set; }
    public string? Color { get; set; }
    public string? State { get; set; }
    public string? Type { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Headline { get; set; }
    public string? Website { get; set; }
    public string? Region { get; set; }
    public string? Activity { get; set; }
    public double? Wallet { get; set; }
    public double? Point { get; set; } = 0;
    public bool? ShowContactInfo { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsFollowing { get; set; } = false;
    public bool? Suspend { get; set; }
    public int? CountFollowers { get; set; }
    public int? CountFollowing { get; set; }
    public int? CountProducts { get; set; }
    public string? AccessLevel { get; set; }
    public string? Access { get; set; }
    public DateTime? BirthDate { get; set; }
    public GenderEntity? Gender { get; set; }
    public GrowthRateReadDto? GrowthRate { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryReadDto>? Categories { get; set; }
    public IEnumerable<ProductReadDto>? Products { get; set; }
    public IEnumerable<UserReadDto>? Followers { get; set; }
}

public class UserMinimalReadDto
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Color { get; set; }
    public string? State { get; set; }
    public int? CountFollowers { get; set; }
    public int? CountFollowing { get; set; }
    public int? CountProducts { get; set; }
    public string? AccessLevel { get; set; }
    public string? Access { get; set; }
    public GenderEntity? Gender { get; set; }
    public IEnumerable<MediaEntity>? Media { get; set; }
    public IEnumerable<CategoryReadDto>? Categories { get; set; }
}

public class GrowthRateReadDto
{
    public string? Id { get; set; }
    public double InterActive1 { get; set; }
    public double InterActive2 { get; set; }
    public double InterActive3 { get; set; }
    public double InterActive4 { get; set; }
    public double InterActive5 { get; set; }
    public double InterActive6 { get; set; }
    public double InterActive7 { get; set; }
    public double Feedback1 { get; set; }
    public double Feedback2 { get; set; }
    public double Feedback3 { get; set; }
    public double Feedback4 { get; set; }
    public double Feedback5 { get; set; }
    public double Feedback6 { get; set; }
    public double Feedback7 { get; set; }
    public double TotalInterActive { get; set; }
    public double TotalFeedback { get; set; }
}

public class UserCreateUpdateDto
{
    public string? Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
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
    public string? Instagram { get; set; }
    public string? Telegram { get; set; }
    public string? WhatsApp { get; set; }
    public string? LinkedIn { get; set; }
    public string? Dribble { get; set; }
    public string? SoundCloud { get; set; }
    public string? Pinterest { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public string? Activity { get; set; }
    public string? Color { get; set; }
    public string? State { get; set; }
    public bool? Suspend { get; set; }
    public double? Wallet { get; set; }
    public double? Point { get; set; } = 0;
    public bool? ShowContactInfo { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? GenderId { get; set; }
    public string? AccessLevel { get; set; }
    public string? Access { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<int>? Locations { get; set; }
}

public class UserFilterDto
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public bool? ShowGender { get; set; }
    public bool? ShowMedia { get; set; }
    public bool? ShowCategories { get; set; }
    public bool? ShowLocations { get; set; }
    public bool? ShowForms { get; set; }
    public bool? ShowProducts { get; set; }
    public bool? ShowTransactions { get; set; }
    public bool? ShowFollowings { get; set; }
}

public class SeederUserDto
{
    public List<UserCreateUpdateDto> Users { get; set; }
}