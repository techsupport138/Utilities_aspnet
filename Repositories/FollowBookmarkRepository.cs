namespace Utilities_aspnet.Repositories;

public interface IFollowBookmarkRepository {
	Task<GenericResponse<FollowReadDto>> GetFollowers(string id);
	Task<GenericResponse<FollowingReadDto>> GetFollowing(string id);
	Task<GenericResponse> ToggleFollow(string sourceUserId, FollowCreateDto dto);
	Task<GenericResponse> RemoveFollowings(string targetUserId, FollowCreateDto dto);
	Task<GenericResponse<BookmarkReadDto>> ReadBookmarks();

	Task<GenericResponse> ToggleBookmark(BookmarkCreateDto dto);
}

public class FollowBookmarkRepository : IFollowBookmarkRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;

	public FollowBookmarkRepository(
		DbContext context,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		IProductRepository productRepository) {
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		_productRepository = productRepository;
	}

	public async Task<GenericResponse> ToggleBookmark(BookmarkCreateDto dto) {
		BookmarkEntity? oldBookmark = _context.Set<BookmarkEntity>()
			.FirstOrDefault(x => (
				                     x.ProductId == dto.ProductId ||
				                     x.CategoryId == dto.CategoryId) &&
			                     x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name!);
		if (oldBookmark == null) {
			BookmarkEntity bookmark = new() {UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!};

			if (dto.ProductId.HasValue) bookmark.ProductId = dto.ProductId;

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
		GenericResponse<IEnumerable<ProductReadDto>> products =
			await _productRepository.Read(new FilterProductDto {IsBookmarked = true});

		BookmarkReadDto dto = new() {
			Products = products.Result
		};

		return new GenericResponse<BookmarkReadDto>(_mapper.Map<BookmarkReadDto>(dto));
	}

	public async Task<GenericResponse<FollowReadDto>> GetFollowers(string id) {
		IEnumerable<UserEntity?> followers = await _context.Set<FollowEntity>()
			.AsNoTracking()
			.Where(x => x.FollowsUserId == id)
			.Include(x => x.FollowerUser)
			.ThenInclude(x => x.Media)
			.Select(x => x.FollowerUser)
			.ToListAsync();

		IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(followers);

		return new GenericResponse<FollowReadDto>(new FollowReadDto {Followers = users});
	}

	public async Task<GenericResponse<FollowingReadDto>> GetFollowing(string id) {
		IEnumerable<UserEntity?> followings = await _context.Set<FollowEntity>()
			.AsNoTracking()
			.Where(x => x.FollowerUserId == id)
			.Include(x => x.FollowsUser)
			.ThenInclude(x => x.Media)
			.Select(x => x.FollowsUser)
			.ToListAsync();

		IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(followings);

		return new GenericResponse<FollowingReadDto>(new FollowingReadDto {Followings = users});
	}

	public async Task<GenericResponse> ToggleFollow(string sourceUserId, FollowCreateDto parameters) {


			FollowEntity? follow = await _context.Set<FollowEntity>()
				.FirstOrDefaultAsync(x => x.FollowerUserId == sourceUserId && x.FollowsUserId == parameters.UserId);
			if (follow != null) {
				_context.Set<FollowEntity>().Remove(follow);
			}
			else {
				follow = new FollowEntity {
					FollowerUserId = sourceUserId,
					FollowsUserId = parameters.UserId
				};

				await _context.Set<FollowEntity>().AddAsync(follow);
			}

		await _context.SaveChangesAsync();

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}

	public async Task<GenericResponse> RemoveFollowings(string userId, FollowCreateDto parameters) {

		FollowEntity? following = await _context.Set<FollowEntity>()
			.Where(x => x.FollowerUserId == parameters.UserId && x.FollowsUserId == userId)
			.FirstOrDefaultAsync();
		if(following != null)
        {
			_context.Set<FollowEntity>().Remove(following);
			await _context.SaveChangesAsync();
		}


		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}