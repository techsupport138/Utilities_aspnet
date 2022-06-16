namespace Utilities_aspnet.Transaction;

public interface ITransactionRepository
{
    Task<GenericResponse<List<TransactionReadDto>>> Read();
    Task<GenericResponse<List<TransactionReadDto>>> ReadMine();
    Task<GenericResponse<TransactionReadDto?>> Create(TransactionCreateDto dto);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly DbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public TransactionRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }



    public async Task<GenericResponse<TransactionReadDto?>> Create(TransactionCreateDto dto)
    {
        dto.UserId = _httpContextAccessor.HttpContext.User.Identity.Name;
        TransactionEntity entity = _mapper.Map<TransactionEntity>(dto);
        await _dbContext.Set<TransactionEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();


        return new GenericResponse<TransactionReadDto?>(_mapper.Map<TransactionReadDto>(entity));
    }



    public async Task<GenericResponse<List<TransactionReadDto>>> Read()
    {
        List<TransactionEntity> model =
            await _dbContext.Set<TransactionEntity>().ToListAsync();
        return new GenericResponse<List<TransactionReadDto>>(_mapper.Map<List<TransactionReadDto>>(model));
    }
    public async Task<GenericResponse<List<TransactionReadDto>>> ReadMine()
    {
        List<TransactionEntity> model =
            await _dbContext.Set<TransactionEntity>().Where(i => i.UserId == _httpContextAccessor.HttpContext.User.Identity.Name).ToListAsync();
        return new GenericResponse<List<TransactionReadDto>>(_mapper.Map<List<TransactionReadDto>>(model));
    }


}