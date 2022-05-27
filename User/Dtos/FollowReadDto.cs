namespace Utilities_aspnet.User.Dtos;

public class FollowReadDto
{
    public FollowReadDto()
    {
        Followers = new List<UserReadDto>();
    }

    public IEnumerable<UserReadDto> Followers { get; set; }
}

public class FollowWriteDto
{
    public FollowWriteDto()
    {
        Followers = new List<string>();
    }

    public IEnumerable<string> Followers { get; set; }
}

public class FollowingReadDto
{
    public FollowingReadDto()
    {
        Followings = new List<UserReadDto>();
    }

    public IEnumerable<UserReadDto> Followings { get; set; }
}