namespace Utilities_aspnet.Repositories;

public interface IVoteRepository {
	Task<GenericResponse> CreateUpdateVote(VoteCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<VoteReadDto>>> CreateUpdateVoteFields(VoteFieldCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<VoteReadDto>>> ReadVoteFields(Guid id);
	GenericResponse<IQueryable<VoteEntity>> ReadProductVote(Guid productId, string userId);
}

public class VoteRepository : IVoteRepository {
	private readonly DbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IHttpContextAccessor _httpContextAccessor;
	
	public VoteRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<IEnumerable<VoteReadDto>>> CreateUpdateVoteFields(VoteFieldCreateUpdateDto dto) {
		foreach (VoteFieldDto item in dto.VoteFields)
			try {
				VoteFieldEntity? up = await _dbContext.Set<VoteFieldEntity>().FirstOrDefaultAsync(x => x.ProductId == dto.ProductId && x.Id == item.Id);
				if (up != null) {
					up.Title = item.Title;
					await _dbContext.SaveChangesAsync();
				}
				else {
					_dbContext.Set<VoteFieldEntity>().Add(new VoteFieldEntity {
						ProductId = dto.ProductId,
						Title = item.Title
					});
				}

				await _dbContext.SaveChangesAsync();
			}
			catch { }

		IQueryable<VoteFieldEntity> entity = _dbContext.Set<VoteFieldEntity>().Where(x => x.ProductId == dto.ProductId);

		return new GenericResponse<IEnumerable<VoteReadDto>>(_mapper.Map<IEnumerable<VoteReadDto>>(entity));
	}

	public async Task<GenericResponse<IEnumerable<VoteReadDto>>> ReadVoteFields(Guid id) {
		IEnumerable<VoteFieldEntity> entity = await _dbContext.Set<VoteFieldEntity>()
			.Where(x => x.ProductId == id)
			.Include(x => x.Votes).ToListAsync();

		return new GenericResponse<IEnumerable<VoteReadDto>>(_mapper.Map<IEnumerable<VoteReadDto>>(entity));
	}

	public GenericResponse<IQueryable<VoteEntity>> ReadProductVote(Guid productId, string userId) {
		IQueryable<VoteEntity> i = _dbContext.Set<VoteEntity>()
			.Include(x => x.Product)
			.Include(x => x.VoteField)
			.Where(x => x.ProductId == productId && x.UserId == userId);

		return new GenericResponse<IQueryable<VoteEntity>>(i);
	}

	public async Task<GenericResponse> CreateUpdateVote(VoteCreateUpdateDto dto) {
		string? userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
		foreach (VoteDto item in dto.Votes) {
			VoteEntity? update = await _dbContext.Set<VoteEntity>()
				.FirstOrDefaultAsync(x => x.ProductId == dto.ProductId && x.VoteFieldId == item.VoteFieldId && x.UserId == userId);
			if (update != null) {
				update.Score = item.Score;
				ProductEntity pp = (await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == dto.ProductId))!;
				if (pp.VoteCount == null) pp.VoteCount = item.Score;
				else pp.VoteCount = pp.VoteCount += item.Score;
				_dbContext.Update(pp);
				await _dbContext.SaveChangesAsync();
			}
			else {
				await _dbContext.Set<VoteEntity>().AddAsync(new VoteEntity {
					ProductId = dto.ProductId,
					Score = item.Score,
					VoteFieldId = item.VoteFieldId,
					UserId = userId
				});
				ProductEntity pp = (await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == dto.ProductId))!;
				if (pp.VoteCount == null) pp.VoteCount = item.Score;
				else pp.VoteCount = pp.VoteCount += item.Score;
				_dbContext.Update(pp);
				await _dbContext.SaveChangesAsync();
			}
		}

		return new GenericResponse();
	}
}