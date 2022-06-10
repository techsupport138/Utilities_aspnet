using Utilities_aspnet.User;

namespace Utilities_aspnet.FollowBookmark;

public interface IFollowBookmarkRepository {
    Task<GenericResponse<FollowReadDto>> GetFollowers(string id);
    Task<GenericResponse<FollowingReadDto>> GetFollowing(string id);
    Task<GenericResponse> ToggleFollow(string sourceUserId, FollowWriteDto dto);
    Task<GenericResponse> RemoveFollowings(string targetUserId, FollowWriteDto dto);
    Task<GenericResponse<BookmarkReadDto>> ReadBookmarks();

    Task<GenericResponse> ToggleBookmark(BookmarkCreateDto dto);
}

public class FollowBookmarkRepository : IFollowBookmarkRepository {
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductRepository<ProductEntity> _productRepository;
    private readonly IProductRepository<AdEntity> _adRepository;
    private readonly IProductRepository<DailyPriceEntity> _dailyPriceRepository;
    private readonly IProductRepository<CompanyEntity> _companyRepository;
    private readonly IProductRepository<MagazineEntity> _magazineRepository;
    private readonly IProductRepository<ProjectEntity> _projectRepository;
    private readonly IProductRepository<ServiceEntity> _serviceRepository;
    private readonly IProductRepository<TenderEntity> _tenderRepository;
    private readonly IProductRepository<TutorialEntity> _tutorialRepository;

    public FollowBookmarkRepository(
        DbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IProductRepository<ProductEntity> productRepository,
        IProductRepository<AdEntity> adRepository,
        IProductRepository<DailyPriceEntity> dailyPriceRepository,
        IProductRepository<CompanyEntity> companyRepository,
        IProductRepository<MagazineEntity> magazineRepository,
        IProductRepository<ProjectEntity> projectRepository,
        IProductRepository<ServiceEntity> serviceRepository,
        IProductRepository<TenderEntity> tenderRepository,
        IProductRepository<TutorialEntity> tutorialRepository) {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _productRepository = productRepository;
        _adRepository = adRepository;
        _dailyPriceRepository = dailyPriceRepository;
        _companyRepository = companyRepository;
        _magazineRepository = magazineRepository;
        _projectRepository = projectRepository;
        _serviceRepository = serviceRepository;
        _tenderRepository = tenderRepository;
        _tutorialRepository = tutorialRepository;
    }

    public async Task<GenericResponse> ToggleBookmark(BookmarkCreateDto dto) {
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

    public async Task<GenericResponse<BookmarkReadDto>> ReadBookmarks() {
        GenericResponse<IEnumerable<ProductReadDto>> ads =
            await _adRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> products =
            await _productRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> dailyPrices =
            await _dailyPriceRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> companies =
            await _companyRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> magazines =
            await _magazineRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> projects =
            await _projectRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> services =
            await _serviceRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> tenders =
            await _tenderRepository.Read(new FilterProductDto {IsBookmarked = true});
        GenericResponse<IEnumerable<ProductReadDto>> tutorials =
            await _tutorialRepository.Read(new FilterProductDto {IsBookmarked = true});

        BookmarkReadDto dto = new BookmarkReadDto {
            Ads = ads.Result,
            Products = products.Result,
            DailyPrices = dailyPrices.Result,
            Companies = companies.Result,
            Magazines = magazines.Result,
            Projects = projects.Result,
            Services = services.Result,
            Tenders = tenders.Result,
            Tutorials = tutorials.Result
        };

        return new GenericResponse<BookmarkReadDto>(_mapper.Map<BookmarkReadDto>(dto));
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