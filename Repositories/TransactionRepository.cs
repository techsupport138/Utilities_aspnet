using Utilities_aspnet.Entities;

namespace Utilities_aspnet.Repositories;

public interface ITransactionRepository {
	Task<GenericResponse<IEnumerable<TransactionReadDto>>> Read();
	Task<GenericResponse<IEnumerable<TransactionReadDto>>> ReadMine();
	Task<GenericResponse<TransactionReadDto?>> Create(TransactionCreateDto dto);
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

	public async Task<GenericResponse<TransactionReadDto?>> Create(TransactionCreateDto dto) {
		dto.UserId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
		TransactionEntity entity = _mapper.Map<TransactionEntity>(dto);
		await _dbContext.Set<TransactionEntity>().AddAsync(entity);
		await _dbContext.SaveChangesAsync();

		return new GenericResponse<TransactionReadDto?>(_mapper.Map<TransactionReadDto>(entity));
	}

	public async Task<GenericResponse<IEnumerable<TransactionReadDto>>> Read() {
		IEnumerable<TransactionEntity> model =
			await _dbContext.Set<TransactionEntity>().ToListAsync();
		return new GenericResponse<IEnumerable<TransactionReadDto>>(_mapper.Map<IEnumerable<TransactionReadDto>>(model));
	}

	public async Task<GenericResponse<IEnumerable<TransactionReadDto>>> ReadMine() {
		IEnumerable<TransactionEntity> model =
			await _dbContext.Set<TransactionEntity>().Where(i => i.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)
				.ToListAsync();
		return new GenericResponse<IEnumerable<TransactionReadDto>>(_mapper.Map<IEnumerable<TransactionReadDto>>(model));
	}
}