namespace Utilities_aspnet.User.Dtos;

public class FollowReadDto {
    public IEnumerable<UserReadDto>? Followers { get; set; }
}

public class FollowWriteDto {
    public IEnumerable<string>? Followers { get; set; }
}

public class FollowingReadDto {
    public IEnumerable<UserReadDto>? Followings { get; set; }
}