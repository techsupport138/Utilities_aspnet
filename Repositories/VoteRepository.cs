namespace Utilities_aspnet.Repositories;

public interface IVoteRepository {
	Task<GenericResponse> CreateUpdateVote(VoteCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<VoteReadDto>>> CreateUpdateVoteFields(VoteFieldCreateUpdateDto dto);
	Task<GenericResponse<IEnumerable<VoteReadDto>>> ReadVoteFields(Guid id);
}

public class VoteRepository : IVoteRepository {
	private readonly DbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IProductRepositoryV2 _productRepositoryV2;

	public VoteRepository(
		DbContext dbContext,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		IProductRepositoryV2 productRepositoryV2) {
		_dbContext = dbContext;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		_productRepositoryV2 = productRepositoryV2;
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
		IEnumerable<VoteFieldEntity> entity = await _dbContext.Set<VoteFieldEntity>().Where(x => x.ProductId == id).Include(x => x.Votes).ToListAsync();

		return new GenericResponse<IEnumerable<VoteReadDto>>(_mapper.Map<IEnumerable<VoteReadDto>>(entity));
	}

	public async Task<GenericResponse> CreateUpdateVote(VoteCreateUpdateDto dto) {
		string? userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
		foreach (VoteDto item in dto.Votes)
			try {
				VoteEntity? update = await _dbContext.Set<VoteEntity>()
					.FirstOrDefaultAsync(x => x.ProductId == dto.ProductId && x.VoteFieldId == item.VoteFieldId && x.UserId == userId);
				if (update != null) {
					update.Score = item.Score;
					await _dbContext.SaveChangesAsync();
					await _productRepositoryV2.Update(new ProductCreateUpdateDto {
						Id = dto.ProductId,
						ScoreMinus = update.Score > item.Score ? item.Score : 0,
						ScorePlus = update.Score < item.Score ? item.Score : 0,
					}, CancellationToken.None);
				}
				else {
					_dbContext.Set<VoteEntity>().Add(new VoteEntity {
						ProductId = dto.ProductId,
						Score = item.Score,
						VoteFieldId = item.VoteFieldId,
						UserId = userId
					});
					await _productRepositoryV2.Update(new ProductCreateUpdateDto {
						Id = dto.ProductId,
						ScoreMinus = update.Score > item.Score ? item.Score : 0,
						ScorePlus = update.Score < item.Score ? item.Score : 0,
					}, CancellationToken.None);
					await _dbContext.SaveChangesAsync();
				}
			}
			catch {
				return new GenericResponse(UtilitiesStatusCodes.BadRequest);
			}

		return new GenericResponse();

		void UpdateProductScore() {
			
		}
	}
}