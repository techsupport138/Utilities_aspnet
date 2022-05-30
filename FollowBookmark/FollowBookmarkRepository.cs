namespace Utilities_aspnet.FollowBookmark;

public interface IFollowBookmarkRepository {
    Task<GenericResponse<FollowReadDto>> GetFollowers(string id);
    Task<GenericResponse<FollowingReadDto>> GetFollowing(string id);
    Task<GenericResponse> Follow(string sourceUserId, FollowWriteDto dto);
    Task<GenericResponse> RemoveFollowings(string targetUserId, FollowWriteDto dto);
    Task<GenericResponse> RemoveFollowers(string sourceUserId, FollowWriteDto dto);

    // void ToggleBookmarkProduct(string userId, long id);
    // void ToggleBookmarkProject(string userId, long id);
    // void ToggleBookmarkTutorial(string userId, long id);
    // void ToggleBookmarkEvent(string userId, long id);
    // void ToggleBookmarkAd(string userId, long id);
    // void ToggleBookmarkCompany(string userId, long id);
    // void ToggleBookmarkTender(string userId, long id);
    // void ToggleBookmarkService(string userId, long id);
    // void ToggleBookmarkMagazine(string userId, long id);
    // void ToggleBookmarkTag(string userId, long id);
    // void ToggleBookmarkSpeciality(string userId, long id);
    void ToggleBookmark(string userId, Guid id);
}

public class FollowBookmarkRepository : IFollowBookmarkRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public FollowBookmarkRepository(DbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }
    
    public void ToggleBookmark(string userId, Guid id) {
        BookmarkEntity? oldBookmark = _context.Set<BookmarkEntity>()
            .FirstOrDefault(x => (
                x.ProductId == id ||
                x.ProjectId == id ||
                x.TutorialId == id ||
                x.EventId == id ||
                x.AdId == id ||
                x.CompanyId == id ||
                x.TenderId == id ||
                x.ServiceId == id ||
                x.MagazineId == id ||
                x.TagId == id ||
                x.SpecialityId == id
            ) && x.UserId == userId);
        if (oldBookmark == null) {
            BookmarkEntity bookmark = new() {
                SpecialityId = id,
                UserId = userId
            };
            _context.Set<BookmarkEntity>().Add(bookmark);
            _context.SaveChanges();
        }
        else {
            _context.Set<BookmarkEntity>().Remove(oldBookmark);
            _context.SaveChangesAsync();
        }
    }

    public async Task<GenericResponse<FollowReadDto>> GetFollowers(string id) {
        List<UserEntity?> followers = await _context.Set<FollowEntity>()
            .AsNoTracking()
            .Where(x => x.SourceUserId == id)
            .Include(x => x.TargetUser)
            .ThenInclude(x => x.Media)
            .Select(x => x.TargetUser)
            .ToListAsync();

        IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(followers);

        return new GenericResponse<FollowReadDto>(new FollowReadDto {Followers = users});
    }

    public async Task<GenericResponse<FollowingReadDto>> GetFollowing(string id) {
        List<UserEntity?> followings = await _context.Set<FollowEntity>()
            .AsNoTracking()
            .Where(x => x.TargetUserId == id)
            .Include(x => x.SourceUser)
            .ThenInclude(x => x.Media)
            .Select(x => x.SourceUser)
            .ToListAsync();

        IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(followings);

        return new GenericResponse<FollowingReadDto>(new FollowingReadDto {Followings = users});
    }

    public async Task<GenericResponse> Follow(string sourceUserId, FollowWriteDto parameters) {
        List<string> users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        foreach (string? targetUserId in users) {
            if (await _context.Set<FollowEntity>()
                    .AnyAsync(x => x.SourceUserId == sourceUserId && x.TargetUserId == targetUserId))
                continue;

            FollowEntity follow = new() {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            await _context.Set<FollowEntity>().AddAsync(follow);
            await _context.Set<FollowEntity>().AddAsync(follow);
        }

        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
    }

    public async Task<GenericResponse> RemoveFollowings(string targetUserId, FollowWriteDto parameters) {
        List<string> users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        List<FollowEntity> followings = await _context.Set<FollowEntity>()
            .Where(x => parameters.Followers.Contains(x.SourceUserId) && x.TargetUserId == targetUserId)
            .ToListAsync();

        _context.Set<FollowEntity>().RemoveRange(followings);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
    }

    public async Task<GenericResponse> RemoveFollowers(string sourceUserId, FollowWriteDto parameters) {
        List<string> users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        List<FollowEntity> followings = await _context.Set<FollowEntity>()
            .Where(x => parameters.Followers.Contains(x.TargetUserId) && x.SourceUserId == sourceUserId)
            .ToListAsync();

        _context.Set<FollowEntity>().RemoveRange(followings);
        await _context.SaveChangesAsync();

        return new GenericResponse(UtilitiesStatusCodes.Success, "عملیات با موفقیت انجام شد");
    }
}