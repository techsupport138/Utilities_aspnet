using Stripe;

namespace Utilities_aspnet.Repositories;

public interface IPaymentRepository {
	Task<GenericResponse<string?>> IncreaseWalletBalance(double amount, string zarinPalMerchantId);
	Task<GenericResponse<string?>> BuyProduct(Guid productId, string zarinPalMerchantId);
	Task<GenericResponse<string?>> StripeBuyProduct(Guid productId, string? extraparams);

	Task<GenericResponse> WalletCallBack(
		int amount,
		string authority,
		string status,
		string userId,
		string zarinPalMerchantId);

	Task<GenericResponse> CallBack(
		Guid productId,
		string authority,
		string status,
		string zarinPalMerchantId);
}

public class PaymentRepository : IPaymentRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public PaymentRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<string?>> IncreaseWalletBalance(double amount, string zarinPalMerchantId) {
		string? userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;

		try {
			UserEntity? user = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);
			Payment payment = new(zarinPalMerchantId, amount.ToInt());
			string callbackUrl = $"{Server.ServerAddress}/Payment/WalletCallBack/{user?.Id}/{amount}";
			string desc = $"شارژ کیف پول به مبلغ {amount}";
			PaymentRequestResponse? result = payment.PaymentRequest(desc, callbackUrl, "", user?.PhoneNumber).Result;

			await _dbContext.Set<TransactionEntity>().AddAsync(new TransactionEntity {
				Amount = amount,
				Authority = result.Authority,
				CreatedAt = DateTime.Now,
				Descriptions = desc,
				GatewayName = "ZarinPal",
				UserId = userId,
				StatusId = TransactionStatus.Pending
			});
			await _dbContext.SaveChangesAsync();

			if (result.Status == 100 && result.Authority.Length == 36) {
				string url = $"https://www.zarinpal.com/pg/StartPay/{result.Authority}";
				return new GenericResponse<string?>(url);
			}
			return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest);
		}
		catch (Exception ex) {
			return new GenericResponse<string?>(ex.Message, UtilitiesStatusCodes.BadRequest);
		}
	}

	public async Task<GenericResponse> WalletCallBack(
		int amount,
		string authority,
		string status,
		string userId,
		string zarinPalMerchantId) {
		if (userId.IsNullOrEmpty()) {
			return new GenericResponse(UtilitiesStatusCodes.BadRequest);
		}

		UserEntity user = (await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId))!;
		Payment payment = new(zarinPalMerchantId, amount);
		if (!status.Equals("OK")) {
			return new GenericResponse(UtilitiesStatusCodes.BadRequest);
		}
		PaymentVerificationResponse? verify = payment.Verification(authority).Result;
		TransactionEntity? pay = _dbContext.Set<TransactionEntity>().FirstOrDefault(x => x.Authority == authority);
		if (pay != null) {
			pay.StatusId = (TransactionStatus?) Math.Abs(verify.Status);
			pay.RefId = verify.RefId;
			pay.UpdatedAt = DateTime.Now;
			_dbContext.Set<TransactionEntity>().Update(pay);
		}
		
		user.Wallet += amount;
		_dbContext.Set<UserEntity>().Update(user);

		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse<string?>> BuyProduct(Guid productId, string zarinPalMerchantId) {
		string? userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;

		try {
			ProductEntity product = (await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == productId))!;
			UserEntity? user = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);
			Payment payment = new(zarinPalMerchantId, product.Price.ToInt());
			string callbackUrl = $"{Server.ServerAddress}/Payment/CallBack/{productId}";
			string desc = $"خرید محصول {product.Title}";
			PaymentRequestResponse? result = payment.PaymentRequest(desc, callbackUrl, "", user?.PhoneNumber).Result;
			await _dbContext.Set<TransactionEntity>().AddAsync(new TransactionEntity {
				Amount = product.Price.ToInt(),
				Authority = result.Authority,
				CreatedAt = DateTime.Now,
				Descriptions = desc,
				GatewayName = "ZarinPal",
				UserId = userId,
				ProductId = productId,
				StatusId = TransactionStatus.Pending
			});
			await _dbContext.SaveChangesAsync();

			if (result.Status == 100 && result.Authority.Length == 36) {
				string url = $"https://www.zarinpal.com/pg/StartPay/{result.Authority}";
				return new GenericResponse<string?>(url);
			}
			return new GenericResponse<string?>("", UtilitiesStatusCodes.BadRequest);
		}
		catch (Exception ex) {
			return new GenericResponse<string?>(ex.Message, UtilitiesStatusCodes.BadRequest);
		}
	}

	public async Task<GenericResponse> CallBack(
		Guid productId,
		string authority,
		string status,
		string zarinPalMerchantId) {
		ProductEntity product = (await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == productId))!;
		Payment payment = new(zarinPalMerchantId, product.Price.ToInt());
		if (!status.Equals("OK")) {
			return new GenericResponse(UtilitiesStatusCodes.BadRequest);
		}
		PaymentVerificationResponse? verify = payment.Verification(authority).Result;
		TransactionEntity? pay = await _dbContext.Set<TransactionEntity>().FirstOrDefaultAsync(x => x.Authority == authority);
		if (pay != null) {
			pay.StatusId = (TransactionStatus?) Math.Abs(verify.Status);
			pay.RefId = verify.RefId;
			pay.UpdatedAt = DateTime.Now;
			_dbContext.Set<TransactionEntity>().Update(pay);
		}

		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}


	public async Task<GenericResponse<string?>> StripeBuyProduct(Guid productId,string? extraparams)
	{
		string? userId = _httpContextAccessor.HttpContext?.User.Identity?.Name;

		try
		{
			OrderEntity order = (await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x => x.Id == productId))!;
			UserEntity? user = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == userId);

			if(order == null || order.TotalPrice==0)
				return new GenericResponse<string?>("zero price", UtilitiesStatusCodes.BadRequest);

			if (!long.TryParse(order.TotalPrice.ToString(), out long tprice))
			return new GenericResponse<string?>("zero l price", UtilitiesStatusCodes.BadRequest);

			#region Stripe
			// Set your secret key. Remember to switch to your live secret key in production.
			// See your keys here: https://dashboard.stripe.com/apikeys
			StripeConfiguration.ApiKey = "sk_test_51MYdbtDl26fbDZBl5mwqqA1uyCJQCHG8uNave9q0tHWsOni6W79IEb753a0rRpgnwqyr97E8nY8FFKetPvl3CFVu00vXwSicjS";

				var options = new PaymentIntentCreateOptions
				{
					Amount = tprice,
					Currency = "usd",
					PaymentMethodTypes = new List<string>
    {
        //"bancontact",
        "card",
        //"eps",
        //"giropay",
        //"ideal",
        //"p24",
        //"sepa_debit",
        //"sofort",
    },
				};
			PaymentIntentService service = new PaymentIntentService();
			PaymentIntent paymentIntent= await service.CreateAsync(options);
			#endregion



			//await _dbContext.Set<TransactionEntity>().AddAsync(new TransactionEntity
			//{
			//	Amount = order.TotalPrice.ToInt(),
			//	Authority = paymentIntent.StripeResponse.ToString(),
			//	CreatedAt = DateTime.Now,
			//	Descriptions = "ClientSecret"+paymentIntent.ClientSecret,
			//	GatewayName = "Stripe",
			//	UserId = userId,
			//	ProductId = productId,
			//	StatusId = TransactionStatus.Pending
			//});
			//await _dbContext.SaveChangesAsync();

			
			return new GenericResponse<string?>(paymentIntent.ClientSecret, UtilitiesStatusCodes.Success);
		}
		catch (Exception ex)
		{
			return new GenericResponse<string?>(ex.Message, UtilitiesStatusCodes.BadRequest);
		}
	}

	public async Task<GenericResponse> StripeCallBack(
		Guid productId,
		string authority,
		string status,
		string zarinPalMerchantId)
	{
		ProductEntity product = (await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == productId))!;
		Payment payment = new(zarinPalMerchantId, product.Price.ToInt());
		if (!status.Equals("OK"))
		{
			return new GenericResponse(UtilitiesStatusCodes.BadRequest);
		}
		PaymentVerificationResponse? verify = payment.Verification(authority).Result;
		TransactionEntity? pay = await _dbContext.Set<TransactionEntity>().FirstOrDefaultAsync(x => x.Authority == authority);
		if (pay != null)
		{
			pay.StatusId = (TransactionStatus?)Math.Abs(verify.Status);
			pay.RefId = verify.RefId;
			pay.UpdatedAt = DateTime.Now;
			_dbContext.Set<TransactionEntity>().Update(pay);
		}

		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}
}