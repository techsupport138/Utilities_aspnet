namespace Utilities_aspnet.Repositories;

public interface ITransactionRepository {
	Task<GenericResponse<IEnumerable<TransactionEntity>>> Read();
	Task<GenericResponse<IEnumerable<TransactionEntity>>> ReadMine();
	Task<GenericResponse<TransactionEntity?>> Create(TransactionCreateDto dto);
}

public class TransactionRepository : ITransactionRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;

	public TransactionRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<TransactionEntity?>> Create(TransactionCreateDto dto) {
		dto.UserId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
		TransactionEntity entity = _mapper.Map<TransactionEntity>(dto);
		await _dbContext.Set<TransactionEntity>().AddAsync(entity);
		await _dbContext.SaveChangesAsync();

		return new GenericResponse<TransactionEntity?>(_mapper.Map<TransactionEntity>(entity));
	}

	public async Task<GenericResponse<IEnumerable<TransactionEntity>>> Read() {
		IEnumerable<TransactionEntity> model = await _dbContext.Set<TransactionEntity>().ToListAsync();
		return new GenericResponse<IEnumerable<TransactionEntity>>(_mapper.Map<IEnumerable<TransactionEntity>>(model));
	}

	public async Task<GenericResponse<IEnumerable<TransactionEntity>>> ReadMine() {
		IEnumerable<TransactionEntity> model = await _dbContext.Set<TransactionEntity>()
			.Where(i => i.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name).ToListAsync();
		return new GenericResponse<IEnumerable<TransactionEntity>>(_mapper.Map<IEnumerable<TransactionEntity>>(model));
	}
}