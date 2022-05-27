namespace Utilities_aspnet.Wallet.Data;

public interface IWalletRepository
{

}

public class WalletRepository : IWalletRepository
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public WalletRepository(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}