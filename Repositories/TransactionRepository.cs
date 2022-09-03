namespace Utilities_aspnet.Repositories;

public interface ITransactionRepository {
	GenericResponse<IQueryable<TransactionEntity>> Read();
	GenericResponse<IQueryable<TransactionEntity>> ReadMine();
	Task<GenericResponse<TransactionEntity>> Create(TransactionEntity dto);
}

public class TransactionRepository : ITransactionRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public TransactionRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<TransactionEntity>> Create(TransactionEntity entity) {
		entity.UserId ??= _httpContextAccessor.HttpContext?.User.Identity?.Name;
		await _dbContext.Set<TransactionEntity>().AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse<TransactionEntity>(entity);
	}

	public GenericResponse<IQueryable<TransactionEntity>> Read() => new(_dbContext.Set<TransactionEntity>());

	public GenericResponse<IQueryable<TransactionEntity>> ReadMine() {
		IQueryable<TransactionEntity> i = _dbContext.Set<TransactionEntity>()
			.Where(i => i.UserId == _httpContextAccessor.HttpContext!.User.Identity!.Name);
		return new GenericResponse<IQueryable<TransactionEntity>>(i);
	}
}