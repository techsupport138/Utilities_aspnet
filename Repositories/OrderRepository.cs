namespace Utilities_aspnet.Repositories;

public interface IOrderRepository {
	GenericResponse<IQueryable<OrderEntity>> Filter(OrderFilterDto dto);
	Task<GenericResponse<OrderEntity>> ReadById(Guid id);
	Task<GenericResponse<OrderEntity?>> Create(OrderCreateUpdateDto dto);
	Task<GenericResponse<OrderEntity?>> Update(OrderCreateUpdateDto dto);
	Task<GenericResponse> Delete(Guid id);
	Task<GenericResponse> CreateOrderDetailToOrder(OrderDetailCreateUpdateDto dto);
	Task<GenericResponse> DeleteOrderDetail(Guid id);
}

public class OrderRepository : IOrderRepository {
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public OrderRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor) {
		_dbContext = dbContext;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<OrderEntity?>> Create(OrderCreateUpdateDto dto) {
		double totalPrice = 0;

		List<ProductEntity> listProducts = new();
		foreach (OrderDetailCreateUpdateDto item in dto.OrderDetails!) {
			ProductEntity? e = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
			if (e != null) listProducts.Add(e);
		}

		IEnumerable<string?> q = listProducts.GroupBy(x => x.UserId).Select(z => z.Key);
		if (q.Count() > 1)
			return new GenericResponse<OrderEntity?>(null, UtilitiesStatusCodes.BadRequest, "Cannot Add from multiple seller.");

		OrderEntity entityOrder = new() {
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
			ProductOwnerId = listProducts.First().UserId,
		};

		await _dbContext.Set<OrderEntity>().AddAsync(entityOrder);
		await _dbContext.SaveChangesAsync();

		foreach (OrderDetailCreateUpdateDto item in dto.OrderDetails) {
			ProductEntity? productEntity = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
			if (productEntity != null && productEntity.Stock < item.Count) {
				await Delete(entityOrder.Id);
				throw new ArgumentException("failed request! this request is more than stock!");
			}

			OrderDetailEntity orderDetailEntity = new() {
				OrderId = entityOrder.Id,
				ProductId = item.ProductId,
				Price = item.Price ?? productEntity?.Price,
				Count = item.Count
			};

			if (item.Categories.IsNotNullOrEmpty()) {
				List<CategoryEntity> listCategory = new();
				foreach (Guid i in item.Categories ?? new List<Guid>()) {
					CategoryEntity? e = await _dbContext.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == i);
					if (e != null) listCategory.Add(e);
				}
				orderDetailEntity.Categories = listCategory;
			}

			await _dbContext.Set<OrderDetailEntity>().AddAsync(orderDetailEntity);
			await _dbContext.SaveChangesAsync();

			totalPrice += Convert.ToDouble(productEntity?.Price ?? 0);
		}

		entityOrder.TotalPrice = totalPrice;
		entityOrder.DiscountPrice = totalPrice * dto.DiscountPercent / 100;

		return new GenericResponse<OrderEntity?>(entityOrder);
	}

	public async Task<GenericResponse<OrderEntity?>> Update(OrderCreateUpdateDto dto) {
		string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name!;
		OrderEntity? oldOrder = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(x => x.UserId == userId && x.Id == dto.Id);
		if (oldOrder == null) throw new ArgumentException("not found!");

		oldOrder.Description = dto.Description;
		oldOrder.ReceivedDate = dto.ReceivedDate;
		oldOrder.Status = dto.Status;
		oldOrder.TotalPrice = dto.TotalPrice;
		oldOrder.DiscountPrice = dto.DiscountPrice;
		oldOrder.PayType = dto.PayType;
		oldOrder.SendPrice = dto.SendPrice;
		oldOrder.SendType = dto.SendType;
		oldOrder.DiscountCode = dto.DiscountCode;
		oldOrder.DiscountPercent = dto.DiscountPercent;
		oldOrder.UpdatedAt = DateTime.Now;

		foreach (OrderDetailCreateUpdateDto item in dto.OrderDetails!) {
			OrderDetailEntity? oldOrderDetail = await _dbContext.Set<OrderDetailEntity>().FirstOrDefaultAsync(x => x.Id == item.Id);
			if (oldOrderDetail != null) {
				ProductEntity? product = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == item.ProductId);
				if (product != null) {
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

	public GenericResponse<IQueryable<OrderEntity>> Filter(OrderFilterDto dto) {
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
		if (dto.UserId.IsNotNullOrEmpty()) q = q.Where(x => x.UserId == dto.UserId);
		if (dto.ProductOwnerId.IsNotNullOrEmpty()) q = q.Where(x => x.ProductOwnerId == dto.ProductOwnerId);

		int totalCount = q.Count();

		q = q.AsNoTracking().Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);

		return new GenericResponse<IQueryable<OrderEntity>>(q) {
			TotalCount = totalCount,
			PageCount = totalCount % dto.PageSize == 0 ? totalCount / dto.PageSize : totalCount / dto.PageSize + 1,
			PageSize = dto.PageSize
		};
	}

	public async Task<GenericResponse<OrderEntity>> ReadById(Guid id) {
		OrderEntity? i = await _dbContext.Set<OrderEntity>()
			.Include(i => i.OrderDetails)!.ThenInclude(p => p.Product)
			.Include(i => i.User).ThenInclude(i => i.Media)
			.AsNoTracking()
			.FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);
		return new GenericResponse<OrderEntity>(i);
	}

	public async Task<GenericResponse> Delete(Guid id) {
		OrderEntity? i = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(i => i.Id == id);
		if (i != null) {
			_dbContext.Remove(i);
			await _dbContext.SaveChangesAsync();
		}
		else return new GenericResponse(UtilitiesStatusCodes.NotFound);
		return new GenericResponse();
	}

	public async Task<GenericResponse> CreateOrderDetailToOrder(OrderDetailCreateUpdateDto dto) {
		OrderEntity? e = await _dbContext.Set<OrderEntity>().Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id == dto.OrderId);
		if (e == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		EntityEntry<OrderDetailEntity> orderDetailEntity = await _dbContext.Set<OrderDetailEntity>().AddAsync(new OrderDetailEntity {
			ProductId = dto.ProductId,
			Count = dto.Count,
			OrderId = dto.OrderId,
			Price = dto.Price,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		});
		if (!e.OrderDetails.Any()) return new GenericResponse(UtilitiesStatusCodes.Unhandled);
		e.OrderDetails.Append(orderDetailEntity.Entity);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse> DeleteOrderDetail(Guid id) {
		OrderDetailEntity? e = await _dbContext.Set<OrderDetailEntity>().FirstOrDefaultAsync(x => x.Id == id);
		if (e == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		e.DeletedAt = DateTime.Now;
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}
}