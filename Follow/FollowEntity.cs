namespace Utilities_aspnet.Follow;

public class FollowEntity {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

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