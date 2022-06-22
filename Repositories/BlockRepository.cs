namespace Utilities_aspnet.Repositories;

public interface IBlockRepository {
	Task<GenericResponse<BlockReadDto>> ReadMine();
	Task<GenericResponse<BlockReadDto>> Read();
	Task<GenericResponse> ToggleBlock(BlockCreateDto dto);
}

public class BlockRepository : IBlockRepository {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public BlockRepository(
		DbContext context,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor)
	{
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	

	public async Task<GenericResponse<BlockReadDto>> ReadMine() {

		IEnumerable<UserEntity?> blocks = await _context.Set<BlockEntity>()
			.AsNoTracking()
			.Where(x => x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
			.Include(x => x.BlockedUser)
			.ThenInclude(x => x.Media)
			.Select(x => x.BlockedUser)
			.ToListAsync();

		IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(blocks);

		return new GenericResponse<BlockReadDto>(new BlockReadDto { Blocks = users});
	}
	
	public async Task<GenericResponse<BlockReadDto>> Read() {

		IEnumerable<UserEntity?> blocks = await _context.Set<BlockEntity>()
			.AsNoTracking()
			.Include(x => x.BlockedUser)
			.ThenInclude(x => x.Media)
			.Select(x => x.BlockedUser)
			.ToListAsync();

		IEnumerable<UserReadDto>? users = _mapper.Map<IEnumerable<UserReadDto>>(blocks);

		return new GenericResponse<BlockReadDto>(new BlockReadDto { Blocks = users});
	}

	

	public async Task<GenericResponse> ToggleBlock(BlockCreateDto dto) {


		BlockEntity? block = await _context.Set<BlockEntity>()
				.FirstOrDefaultAsync(x => x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name && x.BlockedUserId == dto.UserId);
			if (block != null) {
				_context.Set<BlockEntity>().Remove(block);
			}
			else {
			block = new BlockEntity {
					UserId = _httpContextAccessor.HttpContext.User.Identity.Name,
					BlockedUserId = dto.UserId
				};

				await _context.Set<BlockEntity>().AddAsync(block);
			}

		await _context.SaveChangesAsync();

		return new GenericResponse(UtilitiesStatusCodes.Success, "Mission Accomplished");
	}

}