using Utilities_aspnet.Follow.Entities;

namespace Utilities_aspnet.Follow.Data;

public interface IFollowRepository
{
    Task<GenericResponse<FollowReadDto>> GetFollowers(string id);
    Task<GenericResponse<FollowingReadDto>> GetFollowing(string id);
    Task<GenericResponse> Follow(string sourceUserId, FollowWriteDto parameters);
    Task<GenericResponse> RemoveFollowings(string targetUserId, FollowWriteDto parameters);
    Task<GenericResponse> RemoveFollowers(string sourceUserId, FollowWriteDto parameters);
}

public class FollowRepository : IFollowRepository
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public FollowRepository(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<FollowReadDto>> GetFollowers(string id)
    {
        var followers = await _context.Set<FollowEntity>()
            .AsNoTracking()
            .Where(x => x.SourceUserId == id)
            .Include(x => x.TargetUser)
            .ThenInclude(x => x.Media)
            .Select(x => x.TargetUser)
            .ToListAsync();

        var users = _mapper.Map<IEnumerable<UserReadDto>>(followers);

        return new GenericResponse<FollowReadDto>(new FollowReadDto { Followers = users });
    }

    public async Task<GenericResponse<FollowingReadDto>> GetFollowing(string id)
    {
        var followings = await _context.Set<FollowEntity>()
            .AsNoTracking()
            .Where(x => x.TargetUserId == id)
            .Include(x => x.SourceUser)
            .ThenInclude(x => x.Media)
            .Select(x => x.SourceUser)
            .ToListAsync();

        var users = _mapper.Map<IEnumerable<UserReadDto>>(followings);

        return new GenericResponse<FollowingReadDto>(new FollowingReadDto { Followings = users });
    }

    public async Task<GenericResponse> Follow(string sourceUserId, FollowWriteDto parameters)
    {
        var users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        var queryable = _context.Set<FollowEntity>();

        users.ForEach(async targetUserId =>
        {
            if (await queryable.AnyAsync(x => x.SourceUserId == sourceUserId && x.TargetUserId == targetUserId))
                return;

            var follow = new FollowEntity()
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            await queryable.AddAsync(follow);
        });

        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "عملیات با موفقیت انجام شد");
    }

    public async Task<GenericResponse> RemoveFollowings(string targetUserId, FollowWriteDto parameters)
    {
        var users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        var followings = await _context.Set<FollowEntity>()
            .Where(x => parameters.Followers.Contains(x.SourceUserId) && x.TargetUserId == targetUserId)
            .ToListAsync();

        _context.Set<FollowEntity>().RemoveRange(followings);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "عملیات با موفقیت انجام شد");
    }

    public async Task<GenericResponse> RemoveFollowers(string sourceUserId, FollowWriteDto parameters)
    {
        var users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        var followings = await _context.Set<FollowEntity>()
            .Where(x => parameters.Followers.Contains(x.TargetUserId) && x.SourceUserId == sourceUserId)
            .ToListAsync();

        _context.Set<FollowEntity>().RemoveRange(followings);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "عملیات با موفقیت انجام شد");
    }
}