namespace Utilities_aspnet.FollowBookmark;

public interface IFollowBookmarkRepository {
    Task<GenericResponse<FollowReadDto>> GetFollowers(string id);
    Task<GenericResponse<FollowingReadDto>> GetFollowing(string id);
    Task<GenericResponse> ToggleFollow(string sourceUserId, FollowWriteDto dto);
    Task<GenericResponse> RemoveFollowings(string targetUserId, FollowWriteDto dto);
    Task<GenericResponse<IEnumerable<BookmarkEntity>>> GetBookMarks();

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
    Task<GenericResponse> ToggleBookmark(BookmarkWriteDto dto);
}

public class FollowBookmarkRepository : IFollowBookmarkRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FollowBookmarkRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse> ToggleBookmark(BookmarkWriteDto dto) {
        BookmarkEntity? oldBookmark = _context.Set<BookmarkEntity>()
            .FirstOrDefault(x => (
                x.ProductId == dto.ProductId ||
                x.ProjectId == dto.ProductId ||
                x.TutorialId == dto.TutorialId ||
                x.EventId == dto.EventId ||
                x.AdId == dto.AdId ||
                x.CompanyId == dto.CompanyId ||
                x.TenderId == dto.TenderId ||
                x.ServiceId == dto.ServiceId ||
                x.MagazineId == dto.MagazineId ||
                x.TagId == dto.TagId ||
                x.SpecialityId == dto.SpecialityId
            ) && x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!);
        if (oldBookmark == null) {
            BookmarkEntity bookmark = new() {UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!};

            if (dto.ProductId.HasValue) bookmark.ProductId = dto.ProductId;
            if (dto.ProjectId.HasValue) bookmark.ProjectId = dto.ProjectId;
            if (dto.TutorialId.HasValue) bookmark.TutorialId = dto.TutorialId;
            if (dto.EventId.HasValue) bookmark.EventId = dto.EventId;
            if (dto.AdId.HasValue) bookmark.AdId = dto.AdId;
            if (dto.CompanyId.HasValue) bookmark.CompanyId = dto.CompanyId;
            if (dto.TenderId.HasValue) bookmark.TenderId = dto.TenderId;
            if (dto.ServiceId.HasValue) bookmark.ServiceId = dto.ServiceId;
            if (dto.MagazineId.HasValue) bookmark.MagazineId = dto.MagazineId;
            if (dto.TagId.HasValue) bookmark.TagId = dto.TagId;
            if (dto.SpecialityId.HasValue) bookmark.SpecialityId = dto.SpecialityId;
            
            await _context.Set<BookmarkEntity>().AddAsync(bookmark);
            await _context.SaveChangesAsync();
        }
        else {
            _context.Set<BookmarkEntity>().Remove(oldBookmark);
            await _context.SaveChangesAsync();
        }

        return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
    }

    public async Task<GenericResponse<IEnumerable<BookmarkEntity>>> GetBookMarks() {
        List<BookmarkEntity>? bookmarks = await _context.Set<BookmarkEntity>()
            .AsNoTracking()
            .Include(x => x.Ad)
            .Include(x => x.Color)
            .Include(x => x.Company)
            .Include(x => x.Event)
            .Include(x => x.Magazine)
            .Include(x => x.Product)
            .Include(x => x.Project)
            .Include(x => x.Service)
            .Include(x => x.Speciality)
            .Include(x => x.Tag)
            .Include(x => x.Tutorial)
            .Where(x => x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!)
            .ToListAsync();

        return new GenericResponse<IEnumerable<BookmarkEntity>>(bookmarks);
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

    public async Task<GenericResponse> ToggleFollow(string sourceUserId, FollowWriteDto parameters) {
        List<string> users = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => parameters.Followers.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        foreach (string? targetUserId in users) {
            FollowEntity? follow = await _context.Set<FollowEntity>()
                .FirstOrDefaultAsync(x => x.SourceUserId == sourceUserId && x.TargetUserId == targetUserId);
            if (follow != null) {
                _context.Set<FollowEntity>().Remove(follow);
            }
            else {
                follow = new FollowEntity {
                    SourceUserId = sourceUserId,
                    TargetUserId = targetUserId
                };

                await _context.Set<FollowEntity>().AddAsync(follow);
            }
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
}