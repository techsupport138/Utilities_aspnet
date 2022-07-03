namespace Utilities_aspnet.Repositories;

public interface IPaymentRepository
{
    Task<GenericResponse<string?>> IncreaseWalletBalance(decimal amount, string zarinPalMerchantId);
    Task<GenericResponse> WalletCallBack(int amount, string authority, string status, string userId, string zarinPalMerchantId);
}

public class PaymentRepository : IPaymentRepository
{
    private readonly DbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public PaymentRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<string?>> IncreaseWalletBalance(decimal amount, string zarinPalMerchantId)
    {
        string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;

        try
        {
            UserEntity user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x=>x.Id == userId);
            int Amount = Decimal.ToInt32(amount);
            var payment = new Zarinpal.Payment(zarinPalMerchantId, Amount);
            var callbackUrl = string.Format("{0}Payment/WalletCallBack/{1}/{2}", Server.ServerAddress, user?.Id, Amount);
            var Desc = $"شارژ کیف پول به مبلغ {Amount}";
            //var result = payment.PaymentRequest(Desc, callbackUrl, "", _user.PhoneNumber).Result;
            var result = payment.PaymentRequest(Desc, callbackUrl, "", user?.PhoneNumber).Result;
            ///todo
            ///save to db 
            await _context.Set<TransactionEntity>().AddAsync(new TransactionEntity()
            {
                Amount = Amount,
                Authority = result.Authority,
                CreatedAt = DateTime.Now,
                Descriptions = Desc,
                GatewayName = "ZarinPal",
                UserId = userId,
                //PayDateTime = DateTime.Now,
                StatusId = TransactionStatus.Pending

            });
            await _context.SaveChangesAsync();

            if (result.Status == 100 && result.Authority.Length == 36)
            {
                var url = $"https://www.zarinpal.com/pg/StartPay/{result.Authority}";
                return new GenericResponse<string?>(url, UtilitiesStatusCodes.BadRequest);
            }
            else
            {
                return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest);
            }
        }
        catch (Exception ex)
        {
            return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest);
        }

        
    }

    public async Task<GenericResponse> WalletCallBack(int amount, string authority, string status, string userId, string zarinPalMerchantId)
    {
        if (userId.IsNullOrEmpty()) { return new GenericResponse(UtilitiesStatusCodes.BadRequest); }

        var _user = await _context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);
        //int Amount = Decimal.ToInt32(model.Price);
        int Amount = amount;
        var payment = new Zarinpal.Payment(zarinPalMerchantId, Amount);
        if (!status.Equals("OK"))
        {
            return new GenericResponse(UtilitiesStatusCodes.BadRequest);
        }
        var verify = payment.Verification(authority).Result;
        //verify.RefId
        var _pay = _context.Set<TransactionEntity>().FirstOrDefault(x => x.Authority == authority);
        _pay.StatusId = (TransactionStatus?)Math.Abs(verify.Status);
        _pay.RefId = verify.RefId;
        _pay.UpdatedAt = DateTime.Now;
        _context.Set<TransactionEntity>().Update(_pay);

        //_user.Credit = _user.Credit + amount;
        _user.Wallet = _user.Wallet + amount;
        _context.Set<UserEntity>().Update(_user);

        _context.SaveChanges();
        return new GenericResponse(UtilitiesStatusCodes.Success);
    }

}