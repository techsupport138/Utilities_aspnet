namespace Utilities_aspnet.Follow.Entities;

public class FollowEntity
{
    public Guid SourceUserId { get; set; }
    public Guid TargetUserId { get; set; }
    public bool IsRequest { get; set; }

    public UserEntity SourceUser { get; set; }
    public UserEntity TargetUser { get; set; }
}