namespace Utilities_aspnet.Follow;

public class FollowEntity {
    /// <summary>
    /// follower
    /// </summary>
    public string SourceUserId { get; set; } = null!;

    /// <summary>
    /// following
    /// </summary>
    public string TargetUserId { get; set; } = null!;

    public UserEntity SourceUser { get; set; } = null!;
    public UserEntity TargetUser { get; set; } = null!;
}