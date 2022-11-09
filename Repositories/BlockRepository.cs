namespace Utilities_aspnet.Repositories;

public interface IBlockRepository {
	GenericResponse<IQueryable<UserEntity>> ReadMine();
	Task<GenericResponse> ToggleBlock(string userId);
}

public class BlockRepository : IBlockRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public BlockRepository(DbContext context, IHttpContextAccessor httpContextAccessor) {
		_context = context;
		_httpContextAccessor = httpContextAccessor;
	}

	public GenericResponse<IQueryable<UserEntity>> ReadMine() {
		IQueryable<UserEntity?> blocks = _context.Set<BlockEntity>()
			.Include(x => x.BlockedUser).ThenInclude(x => x!.Media)
			.Where(x => x.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name)
			.AsNoTracking()
			.Select(x => x.BlockedUser);
		return new GenericResponse<IQueryable<UserEntity>>(blocks);
	}

	public async Task<GenericResponse> ToggleBlock(string userId) {
		string? user = _httpContextAccessor.HttpContext!.User.Identity!.Name;
		BlockEntity? block = await _context.Set<BlockEntity>().FirstOrDefaultAsync(x => x.UserId == user && x.BlockedUserId == userId);
		if (block != null) _context.Set<BlockEntity>().Remove(block);
		else {
			block = new BlockEntity {
				UserId = user,
				BlockedUserId = userId
			};
			await _context.Set<BlockEntity>().AddAsync(block);
		}
		await _context.SaveChangesAsync();
		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}
}