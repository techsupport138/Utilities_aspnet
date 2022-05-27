namespace Utilities_aspnet.Follow;

public class FollowEntity : BaseEntity {
    /// <summary>
    /// follower
    /// </summary>
    public string? SourceUserId { get; set; }

    /// <summary>
    /// following
    /// </summary>
    public string? TargetUserId { get; set; }

    public UserEntity? SourceUser { get; set; }
    public UserEntity? TargetUser { get; set; }
}