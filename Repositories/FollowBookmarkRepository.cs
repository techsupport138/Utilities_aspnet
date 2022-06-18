namespace Utilities_aspnet.Repositories;

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
			.Where(x => x.SourceUserId == id)
			.Include(x => x.TargetUser)
			.ThenInclude(x => x.Media)
			.Select(x => x.TargetUser)
			.ToListAsync();

		IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(followers);

		return new GenericResponse<FollowReadDto>(new FollowReadDto {Followers = users});
	}

	public async Task<GenericResponse<FollowingReadDto>> GetFollowing(string id) {
		IEnumerable<UserEntity?> followings = await _context.Set<FollowEntity>()
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
		IEnumerable<string> users = await _context.Set<UserEntity>()
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
		IEnumerable<string> users = await _context.Set<UserEntity>()
			.AsNoTracking()
			.Where(x => parameters.Followers.Contains(x.Id))
			.Select(x => x.Id)
			.ToListAsync();

		IEnumerable<FollowEntity> followings = await _context.Set<FollowEntity>()
			.Where(x => parameters.Followers.Contains(x.SourceUserId) && x.TargetUserId == targetUserId)
			.ToListAsync();

		_context.Set<FollowEntity>().RemoveRange(followings);
		await _context.SaveChangesAsync();

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}