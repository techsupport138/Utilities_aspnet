namespace Utilities_aspnet.Wallet.Data;

public interface IWalletRepository
{

}

public class WalletRepository : IWalletRepository
{
    public async Task<> GetWallet(string id);
}