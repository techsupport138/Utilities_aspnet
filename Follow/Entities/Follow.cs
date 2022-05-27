namespace Utilities_aspnet.Follow.Entities;

public class FollowEntity
{
    /// <summary>
    /// دنبال کننده - follower
    /// </summary>
    public string SourceUserId { get; set; } = null!;

    /// <summary>
    /// دنبال شونده - following
    /// </summary>
    public string TargetUserId { get; set; } = null!;

    public UserEntity SourceUser { get; set; } = null!;
    public UserEntity TargetUser { get; set; } = null!;
}