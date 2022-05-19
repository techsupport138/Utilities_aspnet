namespace Utilities_aspnet.Product;

public interface IFormBuilderRepository
{

}

public class FormBuilderRepository : IFormBuilderRepository
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FormBuilderRepository(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

}