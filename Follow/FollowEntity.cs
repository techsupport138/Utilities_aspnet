namespace Utilities_aspnet.Follow;

public class FollowEntity {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    /// <summary>
    /// follower
    /// </summary>
    public string SourceUserId { get; set; } = null!;

    /// <summary>
    /// following
    /// </summary>
    public string TargetUserId { get; set; } = null!;

    public UserEntity? SourceUser { get; set; }
    public UserEntity? TargetUser { get; set; }
}