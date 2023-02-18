using System.Globalization;

namespace Utilities_aspnet.Repositories;

public interface IOrderRepository
{
	GenericResponse<IQueryable<OrderEntity>> Filter(OrderFilterDto dto);
	Task<GenericResponse<OrderEntity>> ReadById(Guid id);
	Task<GenericResponse<OrderEntity?>> Create(OrderCreateUpdateDto dto);
	Task<GenericResponse<OrderEntity?>> Update(OrderCreateUpdateDto dto);
	Task<GenericResponse> Delete(Guid id);
	Task<GenericResponse> CreateOrderDetailToOrder(OrderDetailCreateUpdateDto dto);
	Task<GenericResponse> DeleteOrderDetail(Guid id);
	GenericResponse<IQueryable<OrderSummaryResponseDto>> ReadOrderSummary(OrderSummaryRequestDto dto);
}

public class OrderRepository : IOrderRepository
{
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public OrderRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor)
	{
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<OrderEntity?>> Create(OrderCreateUpdateDto dto)
	{
		double totalPrice = 0;

		List<ProductEntity> listProducts = new();
		foreach (OrderDetailCreateUpdateDto item in dto.OrderDetails!)
		{
			ProductEntity? e = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
			if (e != null) listProducts.Add(e);
		}

		IEnumerable<string?> q = listProducts.GroupBy(x => x.UserId).Select(z => z.Key);
		if (q.Count() > 1)
			return new GenericResponse<OrderEntity?>(null, UtilitiesStatusCodes.MultipleSeller, "Cannot Add from multiple seller.");

		OrderEntity entityOrder = new()
		{
			Description = dto.Description,
			ReceivedDate = dto.ReceivedDate,
			UserId = _httpContextAccessor.HttpContext?.User.Identity?.Name!,
			DiscountPercent = dto.DiscountPercent,
			DiscountCode = dto.DiscountCode,
			PayType = PayType.Online,
			SendPrice = 0,
			SendType = SendType.Pishtaz,
			Status = OrderStatuses.Pending,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
			PayNumber = "",
			ProductUseCase = dto.ProductUseCase,
			ProductOwnerId = listProducts.First().UserId,
		};
		if (dto.UserId.IsNotNullOrEmpty())
			entityOrder.UserId = dto.UserId;

		await _dbContext.Set<OrderEntity>().AddAsync(entityOrder);
		await _dbContext.SaveChangesAsync();

		foreach (OrderDetailCreateUpdateDto item in dto.OrderDetails)
		{
			ProductEntity? productEntity = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
			if (productEntity != null && productEntity.Stock < item.Count)
			{
				await Delete(entityOrder.Id);
				throw new ArgumentException("failed request! this request is more than stock!");
			}

			OrderDetailEntity orderDetailEntity = new()
			{
				OrderId = entityOrder.Id,
				ProductId = item.ProductId,
				Price = item.Price ?? productEntity?.Price,
				Count = item.Count
			};

			if (item.Categories.IsNotNullOrEmpty())
			{
				List<CategoryEntity> listCategory = new();
				foreach (Guid i in item.Categories ?? new List<Guid>())
				{
					CategoryEntity? e = await _dbContext.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == i);
					if (e != null) listCategory.Add(e);
				}
				orderDetailEntity.Categories = listCategory;
			}

			await _dbContext.Set<OrderDetailEntity>().AddAsync(orderDetailEntity);
			await _dbContext.SaveChangesAsync();

			totalPrice += Convert.ToDouble(productEntity?.Price ?? 0);

		}
		entityOrder.TotalPrice = totalPrice = entityOrder.OrderDetails.Where(o => o.DeletedAt == null).Sum(x => x.Price ?? 0);
		entityOrder.DiscountPrice = totalPrice * dto.DiscountPercent / 100;

		_dbContext.Set<OrderEntity>().Update(entityOrder);
		await _dbContext.SaveChangesAsync();

		return new GenericResponse<OrderEntity?>(entityOrder);
	}

	public async Task<GenericResponse<OrderEntity?>> Update(OrderCreateUpdateDto dto)
	{
		string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name!;
		OrderEntity? oldOrder = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x => x.Id == dto.Id && (x.UserId == userId || x.ProductOwnerId == userId));
		if (oldOrder == null) return new GenericResponse<OrderEntity?>(null, UtilitiesStatusCodes.NotFound);

		oldOrder.Description = dto.Description ?? oldOrder.Description;
		oldOrder.ReceivedDate = dto.ReceivedDate ?? oldOrder.ReceivedDate;
		oldOrder.Status = dto.Status ?? oldOrder.Status;
		oldOrder.TotalPrice = dto.TotalPrice ?? oldOrder.TotalPrice;
		oldOrder.DiscountPrice = dto.DiscountPrice ?? oldOrder.DiscountPrice;
		oldOrder.PayType = dto.PayType ?? oldOrder.PayType;
		oldOrder.SendPrice = dto.SendPrice ?? oldOrder.SendPrice;
		oldOrder.SendType = dto.SendType ?? oldOrder.SendType;
		oldOrder.DiscountCode = dto.DiscountCode ?? oldOrder.DiscountCode;
		oldOrder.DiscountPercent = dto.DiscountPercent ?? oldOrder.DiscountPercent;
		oldOrder.UpdatedAt = DateTime.Now;
		oldOrder.ProductUseCase = dto.ProductUseCase ?? oldOrder.ProductUseCase;

		if (dto.OrderDetails != null)
			foreach (OrderDetailCreateUpdateDto item in dto.OrderDetails)
			{
				OrderDetailEntity? oldOrderDetail = await _dbContext.Set<OrderDetailEntity>().FirstOrDefaultAsync(x => x.Id == item.Id);
				if (oldOrderDetail != null)
				{
					ProductEntity? product = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
					if (product != null)
					{
						if (product.Stock < item.Count) throw new ArgumentException("failed request! this request is more than stock!");

						if (dto.Status == OrderStatuses.Paid)
							if (product.Stock > 0) product.Stock -= item.Count;
							else throw new ArgumentException("product's stock equals zero!");
					}

					oldOrderDetail.ProductId = item.ProductId;
					oldOrderDetail.Price = item.Price ?? product?.Price;
					oldOrderDetail.Count = item.Count;
				}
			}

		await _dbContext.SaveChangesAsync();

		return new GenericResponse<OrderEntity?>(oldOrder);
	}

	public GenericResponse<IQueryable<OrderEntity>> Filter(OrderFilterDto dto)
	{
		IQueryable<OrderEntity> q = _dbContext.Set<OrderEntity>().Include(x => x.OrderDetails.Where(x => x.DeletedAt == null));

		if (dto.ShowProducts.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product);
		if (dto.ShowCategories.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Categories);
		if (dto.ShowComments.IsTrue())
			q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Comments).ThenInclude(i => i.LikeComments);
		if (dto.ShowForms.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Forms);
		if (dto.ShowMedia.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Media);
		if (dto.ShowUser.IsTrue()) q = q.Include(x => x.User).ThenInclude(x => x.Media);
		if (dto.ShowReports.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Reports);
		if (dto.ShowTeams.IsTrue())
			q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Teams)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media);
		if (dto.ShowVotes.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.Votes);
		if (dto.ShowVoteFields.IsTrue()) q = q.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(i => i.VoteFields);

		if (dto.Id.HasValue) q = q.Where(x => x.Id == dto.Id);
		if (dto.Description.IsNotNullOrEmpty()) q = q.Where(x => (x.Description ?? "").Contains(dto.Description!));
		if (dto.ProductUseCase.IsNotNullOrEmpty()) q = q.Where(x => (x.ProductUseCase ?? "").Contains(dto.ProductUseCase!));
		if (dto.Status.HasValue) q = q.Where(x => x.Status == dto.Status);
		if (dto.TotalPrice.HasValue) q = q.Where(x => x.TotalPrice.ToInt() == dto.TotalPrice.ToInt());
		if (dto.DiscountPrice.HasValue) q = q.Where(x => x.DiscountPrice.ToInt() == dto.DiscountPrice.ToInt());
		if (dto.DiscountPercent.HasValue) q = q.Where(x => x.DiscountPercent == dto.DiscountPercent);
		if (dto.DiscountCode.IsNotNullOrEmpty()) q = q.Where(x => (x.DiscountCode ?? "").Contains(dto.DiscountCode!));
		if (dto.SendPrice.HasValue) q = q.Where(x => x.SendPrice.ToInt() == dto.SendPrice.ToInt());
		if (dto.SendType.HasValue) q = q.Where(x => x.SendType == dto.SendType);
		if (dto.PayType.HasValue) q = q.Where(x => x.PayType == dto.PayType);
		if (dto.PayDateTime.HasValue) q = q.Where(x => x.PayDateTime == dto.PayDateTime);
		if (dto.PayNumber.IsNotNullOrEmpty()) q = q.Where(x => (x.PayNumber ?? "").Contains(dto.PayNumber!));
		if (dto.ReceivedDate.HasValue) q = q.Where(x => x.ReceivedDate == dto.ReceivedDate);

		if (dto.UserId.IsNotNullOrEmpty() && dto.ProductOwnerId.IsNotNullOrEmpty())
			q = q.Where(x => x.ProductOwnerId == dto.ProductOwnerId || x.UserId == dto.UserId);
		else
		{
			if (dto.UserId.IsNotNullOrEmpty()) q = q.Where(x => x.UserId == dto.UserId);
			if (dto.ProductOwnerId.IsNotNullOrEmpty()) q = q.Where(x => x.ProductOwnerId == dto.ProductOwnerId);
		}
		if (dto.StartDate.HasValue) q = q.Where(x => x.CreatedAt >= dto.StartDate);
		if (dto.EndDate.HasValue) q = q.Where(x => x.CreatedAt <= dto.EndDate);

		int totalCount = q.Count();

		q = q.AsNoTracking().Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);

		return new GenericResponse<IQueryable<OrderEntity>>(q)
		{
			TotalCount = totalCount,
			PageCount = totalCount % dto.PageSize == 0 ? totalCount / dto.PageSize : totalCount / dto.PageSize + 1,
			PageSize = dto.PageSize
		};
	}

	public async Task<GenericResponse<OrderEntity>> ReadById(Guid id)
	{
		OrderEntity? i = await _dbContext.Set<OrderEntity>()
			.Include(i => i.OrderDetails)!.ThenInclude(p => p.Product)
			.Include(i => i.User).ThenInclude(i => i.Media)
			.AsNoTracking()
			.FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
		return new GenericResponse<OrderEntity>(i);
	}

	public async Task<GenericResponse> Delete(Guid id)
	{
		OrderEntity? i = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(i => i.Id == id);
		if (i != null)
		{
			_dbContext.Remove(i);
			await _dbContext.SaveChangesAsync();
		}
		else return new GenericResponse(UtilitiesStatusCodes.NotFound);
		return new GenericResponse();
	}

	public async Task<GenericResponse> CreateOrderDetailToOrder(OrderDetailCreateUpdateDto dto)
	{
		OrderEntity? e = await _dbContext.Set<OrderEntity>().Include(x => x.OrderDetails.Where(x => x.DeletedAt == null)).ThenInclude(y => y.Product).FirstOrDefaultAsync(x => x.Id == dto.OrderId);
		if (e == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);


		IEnumerable<string?> q = e.OrderDetails?.GroupBy(x => x.Product?.UserId).Select(z => z.Key);
		if (q.Count() > 1)
			return new GenericResponse(UtilitiesStatusCodes.MultipleSeller, "Cannot Add from multiple seller.");


		EntityEntry<OrderDetailEntity> orderDetailEntity = await _dbContext.Set<OrderDetailEntity>().AddAsync(new OrderDetailEntity
		{
			ProductId = dto.ProductId,
			Count = dto.Count,
			OrderId = dto.OrderId,
			Price = dto.Price,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		});
		if (!e.OrderDetails.Any()) return new GenericResponse(UtilitiesStatusCodes.Unhandled);
		e.OrderDetails.Append(orderDetailEntity.Entity);

		ProductEntity p = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(p => p.Id == orderDetailEntity.Entity.ProductId);

		e.TotalPrice = e.OrderDetails.Where(o => o.DeletedAt == null).Sum(x => x.Price ?? 0);
		e.UpdatedAt = DateTime.Now;
		e.ProductOwnerId = q.First() ?? p?.UserId;//order detail agar null get pownerid
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse> DeleteOrderDetail(Guid id)
	{
		OrderDetailEntity? e = await _dbContext.Set<OrderDetailEntity>().Include(e => e.Order).ThenInclude(d => d.OrderDetails).ThenInclude(d1 => d1.Product).FirstOrDefaultAsync(x => x.Id == id);

		if (e == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		e.DeletedAt = DateTime.Now;



		await _dbContext.SaveChangesAsync();
		e.Order.TotalPrice = e.Order.OrderDetails?.Where(o => o.DeletedAt == null).Sum(x => x.Price ?? 0);
		e.Order.UpdatedAt = DateTime.Now;
		e.Order.ProductOwnerId = e.Order.OrderDetails?.Where(o => o.DeletedAt == null && o.Product != null && o.Product.UserId != null).FirstOrDefault()?.Product?.UserId;
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public GenericResponse<IQueryable<OrderSummaryResponseDto>> ReadOrderSummary(OrderSummaryRequestDto dto)
	{
		string[] useCase = new string[] { "product", "capacity" };
		IQueryable<OrderEntity> q = _dbContext.Set<OrderEntity>()
			.Include(x => x.User)
			.Include(x => x.OrderDetails).ThenInclude(x => x.Product)
			.Where(x => x.DeletedAt == null)
			.Where(o => useCase.Contains(o.ProductUseCase)).AsNoTracking();


		if (dto.StartDate.HasValue) q = q.Where(x => x.CreatedAt >= dto.StartDate);
		if (dto.EndDate.HasValue) q = q.Where(x => x.CreatedAt <= dto.EndDate);

		if (dto.OrderType != OrderType.All)
		{
			if (dto.UserId == null)
				return new GenericResponse<IQueryable<OrderSummaryResponseDto>>(null);
			if (dto.OrderType == OrderType.Sale) q = q.Where(x => x.UserId == dto.UserId);
			if (dto.OrderType == OrderType.Purchase) q = q.Where(x => x.ProductOwnerId == dto.UserId);
		}

		bool isGratherThanMonth = true;

		if (dto.EndDate != null && dto.StartDate != null)
		{
			double x = (dto.EndDate.Value - dto.StartDate.Value).TotalDays;
			if (x.ToInt() <= 31) isGratherThanMonth = false;
		}
		IOrderedQueryable<OrderSummaryResponseDto> c;
		switch (dto.OrderReportType)
		{
			case OrderReportType.OrderDate:
				#region OrderDate      
				if (isGratherThanMonth)
				{
					c = q.GroupBy(o => new
					{
						(o.CreatedAt ?? DateTime.Now).Month,
						(o.CreatedAt ?? DateTime.Now).Year
					}).Select(g => new OrderSummaryResponseDto
					{
						Title = g.Key.Year + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
						Month = g.Key.Month,
						Year = g.Key.Year,
						Count = g.Count(),
						Total = g.Sum(x => x.TotalPrice),
						//UseCase = g.Max(y => y.OrderDetails.First().Product.UseCase),

					})
					   .OrderBy(a => a.Year)
					   .ThenBy(a => a.Month);
				}
				else
				{
					c = q.GroupBy(o => new
					{
						(o.CreatedAt ?? DateTime.Now).Day,
						(o.CreatedAt ?? DateTime.Now).Month
					}).Select(g => new OrderSummaryResponseDto
					{
						Title = g.Key.Day + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
						Month = g.Key.Month,
						Day = g.Key.Day,
						Count = g.Count(),
						Total = g.Sum(x => x.TotalPrice),
						//UseCase = g.Max(y => y.OrderDetails.First().Product.UseCase),

					})
				  .OrderBy(a => a.Month)
				  .ThenBy(a => a.Day);
				}
				#endregion
				break;
			case OrderReportType.OrderDateProductUseCase:
				#region OrderDateProductUseCase 

				if (isGratherThanMonth)
				{

					c = q.GroupBy(o => new
					{
						(o.CreatedAt ?? DateTime.Now).Month,
						(o.CreatedAt ?? DateTime.Now).Year,
						o.ProductUseCase
					}).Select(g => new OrderSummaryResponseDto
					{
						Title = g.Key.Year + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
						Month = g.Key.Month,
						Year = g.Key.Year,
						Count = g.Count(),
						Total = g.Sum(x => x.TotalPrice),
						UseCase = g.Max(y => y.ProductUseCase)

					})
						.OrderBy(a => a.Year)
						.ThenBy(a => a.Month);
				}
				else
				{
					c = q.GroupBy(o => new
					{
						(o.CreatedAt ?? DateTime.Now).Day,
						(o.CreatedAt ?? DateTime.Now).Month,
						o.ProductUseCase
					}).Select(g => new OrderSummaryResponseDto
					{
						Title = g.Key.Day + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
						Month = g.Key.Month,
						Day = g.Key.Day,
						Count = g.Count(),
						Total = g.Sum(x => x.TotalPrice),
						UseCase = g.Max(y => y.ProductUseCase)

					})
				   .OrderBy(a => a.Month)
				   .ThenBy(a => a.Day);
				}
				#endregion
				break;
			case OrderReportType.OrderProductUseCase:
				#region OrderProductUseCase 
				c = q.GroupBy(o => new
				{
					o.ProductUseCase
				}).Select(g => new OrderSummaryResponseDto
				{
					Title = g.Max(y => y.ProductUseCase),
					Count = g.Count(),
					Total = g.Sum(x => x.TotalPrice),
					UseCase = g.Max(y => y.ProductUseCase)

				})
					.OrderBy(a => a.Title);

				#endregion
				break;
			case OrderReportType.OrderState:
				#region OrderState 
				c = q.GroupBy(o => new
				{
					o.State
				}).Select(g => new OrderSummaryResponseDto
				{
					Title = g.Max(y => y.State),
					Count = g.Count(),
					Total = g.Sum(x => x.TotalPrice)

				})
					.OrderByDescending(a => a.Total);

				#endregion
				break;
			case OrderReportType.OrderStuse:
				#region OrderStuse 
				c = q.GroupBy(o => (int)o.Status
				).Select(g => new OrderSummaryResponseDto
				{
					Title = g.Max(y => ((int)y.Status).ToString()),
					Count = g.Count(),
					Total = g.Sum(x => x.TotalPrice),
					UseCase = g.Max(y => ((int)y.Status).ToString())

				})
					.OrderBy(a => a.Title);

				#endregion
				break;
			default:
				return new GenericResponse<IQueryable<OrderSummaryResponseDto>>(null);
				break;
		}


		return new GenericResponse<IQueryable<OrderSummaryResponseDto>>(c);


	}
}