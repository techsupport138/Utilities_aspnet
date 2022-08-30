using AutoMapper.QueryableExtensions;

namespace Utilities_aspnet.Repositories;

public interface IProductRepositoryV2 {
	Task<GenericResponse<ProductEntity>> Create(ProductCreateUpdateDto dto, CancellationToken ct);
	GenericResponse<IQueryable<ProductEntity>> Filter(ProductFilterDto dto);
	Task<GenericResponse<ProductEntity?>> ReadById(Guid id, CancellationToken ct);
	Task<GenericResponse<ProductEntity>> Update(ProductCreateUpdateDto dto, CancellationToken ct);
	Task<GenericResponse> Delete(Guid id, CancellationToken ct);
}

public class ProductRepositoryV2 : IProductRepositoryV2 {
	private readonly DbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper _mapper;
	private readonly ICategoryRepository _categoryRepository;

	public ProductRepositoryV2(
		DbContext context,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		ICategoryRepository categoryRepository) {
		_context = context;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		_categoryRepository = categoryRepository;
	}

	public async Task<GenericResponse<ProductEntity>> Create(ProductCreateUpdateDto dto, CancellationToken ct) {
		if (dto == null) throw new ArgumentException("Dto must not be null", nameof(dto));
		ProductEntity entity = _mapper.Map<ProductEntity>(dto);

		ProductEntity e = await entity.FillDataV2(dto, _httpContextAccessor, _context);
		EntityEntry<ProductEntity> i = await _context.Set<ProductEntity>().AddAsync(e, ct);
		await _context.SaveChangesAsync(ct);

		return new GenericResponse<ProductEntity>(i.Entity);
	}

	public GenericResponse<IQueryable<ProductEntity>> Filter(ProductFilterDto dto) {
		IQueryable<ProductEntity> q = _context.Set<ProductEntity>().Include(i => i.Media);

		if (dto.ShowCategories.IsTrue()) q = q.Include(i => i.Categories);
		if (dto.ShowComments.IsTrue()) q = q.Include(i => i.Comments);
		if (dto.ShowForms.IsTrue()) q = q.Include(i => i.Forms);
		if (dto.ShowMedia.IsTrue()) q = q.Include(i => i.Media);
		if (dto.ShowReports.IsTrue()) q = q.Include(i => i.Reports);
		if (dto.ShowTeams.IsTrue()) q = q.Include(i => i.Teams)!.ThenInclude(x => x.User).ThenInclude(x => x.Media);
		if (dto.ShowVotes.IsTrue()) q = q.Include(i => i.Votes);
		if (dto.ShowVoteFields.IsTrue()) q = q.Include(i => i.VoteFields);
		if (dto.ShowCreator.IsTrue()) q = q.Include(i => i.User).ThenInclude(x => x!.Media);

		if (dto.Title.IsNotNullOrEmpty()) q = q.Where(x => (x.Title ?? "").Contains(dto.Title!));
		if (dto.Subtitle.IsNotNullOrEmpty()) q = q.Where(x => (x.Subtitle ?? "").Contains(dto.Subtitle!));
		if (dto.Type.IsNotNullOrEmpty()) q = q.Where(x => (x.Type ?? "").Contains(dto.Type!));
		if (dto.Details.IsNotNullOrEmpty()) q = q.Where(x => (x.Details ?? "").Contains(dto.Details!));
		if (dto.Description.IsNotNullOrEmpty()) q = q.Where(x => (x.Description ?? "").Contains(dto.Description!));
		if (dto.Author.IsNotNullOrEmpty()) q = q.Where(x => (x.Author ?? "").Contains(dto.Author!));
		if (dto.Email.IsNotNullOrEmpty()) q = q.Where(x => (x.Email ?? "").Contains(dto.Email!));
		if (dto.PhoneNumber.IsNotNullOrEmpty()) q = q.Where(x => (x.PhoneNumber ?? "").Contains(dto.PhoneNumber!));
		if (dto.Address.IsNotNullOrEmpty()) q = q.Where(x => (x.Address ?? "").Contains(dto.Address!));
		if (dto.Unit.IsNotNullOrEmpty()) q = q.Where(x => x.Unit == dto.Unit);
		if (dto.UseCase.IsNotNullOrEmpty()) q = q.Where(x => x.UseCase == dto.UseCase);
		if (dto.State.IsNotNullOrEmpty()) q = q.Where(x => x.State == dto.State);
		if (dto.UserId.IsNotNullOrEmpty()) q = q.Where(x => x.UserId == dto.UserId);
		if (dto.StartPriceRange.HasValue) q = q.Where(x => x.Price >= dto.StartPriceRange);
		if (dto.Status.HasValue) q = q.Where(x => x.Status >= dto.Status);
		if (dto.EndPriceRange.HasValue) q = q.Where(x => x.Price <= dto.EndPriceRange);
		if (dto.Enabled.HasValue) q = q.Where(x => x.Enabled == dto.Enabled);
		if (dto.IsForSale.HasValue) q = q.Where(x => x.IsForSale == dto.IsForSale);
		if (dto.VisitsCount.HasValue) q = q.Where(x => x.VisitsCount == dto.VisitsCount);
		if (dto.Length.HasValue) q = q.Where(x => x.Length.ToInt() == dto.Length.ToInt());
		if (dto.Width.HasValue) q = q.Where(x => x.Width.ToInt() == dto.Width.ToInt());
		if (dto.Height.HasValue) q = q.Where(x => x.Height.ToInt() == dto.Height.ToInt());
		if (dto.Weight.HasValue) q = q.Where(x => x.Weight.ToInt() == dto.Weight.ToInt());
		if (dto.MinOrder.HasValue) q = q.Where(x => x.MinOrder >= dto.MinOrder);
		if (dto.MaxOrder.HasValue) q = q.Where(x => x.MaxOrder <= dto.MaxOrder);
		if (dto.MinPrice.HasValue) q = q.Where(x => x.MinPrice <= dto.MinPrice);
		if (dto.MaxPrice.HasValue) q = q.Where(x => x.MaxPrice <= dto.MaxPrice);
		if (dto.StartDate.HasValue) q = q.Where(x => x.StartDate >= dto.StartDate);
		if (dto.EndDate.HasValue) q = q.Where(x => x.EndDate <= dto.EndDate);
		if (dto.Query.IsNotNullOrEmpty())
			q = q.Where(x => (x.Title ?? "")
				            .Contains(dto.Query!) || (x.Subtitle ?? "")
				            .Contains(dto.Query!) || (x.Description ?? "")
				            .Contains(dto.Query!));

		int totalCount = q.Count();

		if (dto.Categories != null && dto.Categories.Any()) q = q.Where(x => x.Categories.Any(y => dto.Categories.ToList().Contains(y.Id)));

		if (dto.FilterOrder.HasValue)
			q = dto.FilterOrder switch {
				ProductFilterOrder.LowPrice => q.OrderBy(x => x.Price),
				ProductFilterOrder.HighPrice => q.OrderByDescending(x => x.Price),
				ProductFilterOrder.AToZ => q.OrderBy(x => x.Title),
				ProductFilterOrder.ZToA => q.OrderByDescending(x => x.Title),
				_ => q.OrderBy(x => x.CreatedAt)
			};

		if (dto.OrderByVotes.IsTrue()) q = q.OrderBy(x => x.VoteCount);
		if (dto.OrderByAtoZ.IsTrue()) q = q.OrderBy(x => x.Title);
		if (dto.OrderByZtoA.IsTrue()) q = q.OrderByDescending(x => x.Title);

		q = q.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);

		return new GenericResponse<IQueryable<ProductEntity>>(q.AsNoTracking()) {
			TotalCount = totalCount,
			PageCount = totalCount % dto.PageSize == 0 ? totalCount / dto?.PageSize : totalCount / dto?.PageSize + 1,
			PageSize = dto?.PageSize
		};
	}

	public async Task<GenericResponse<ProductEntity?>> ReadById(Guid id, CancellationToken ct) {
		ProductEntity? i = await _context.Set<ProductEntity>()
			.Include(i => i.Media)
			.Include(i => i.Categories)
			.Include(i => i.Reports)
			.Include(i => i.Bookmarks)
			.Include(i => i.Votes)
			.Include(i => i.Comments)!.ThenInclude(x => x.LikeComments)
			.Include(i => i.User).ThenInclude(x => x.Media)
			.Include(i => i.User).ThenInclude(x => x.Categories)
			.Include(i => i.Forms)!.ThenInclude(x => x.FormField)
			.Include(i => i.Teams)!.ThenInclude(x => x.User).ThenInclude(x => x.Media)
			.Include(i => i.VoteFields)!.ThenInclude(x => x.Votes)
			.AsNoTracking()
			.FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null, ct);
		if (i == null) return new GenericResponse<ProductEntity?>(null, UtilitiesStatusCodes.NotFound, "Not Found");
		await Update(new ProductCreateUpdateDto {Id = i.Id, VisitsCountPlus = 1}, ct);
		return new GenericResponse<ProductEntity?>(i);
	}

	public async Task<GenericResponse<ProductEntity>> Update(ProductCreateUpdateDto dto, CancellationToken ct) {
		ProductEntity? entity = await _context.Set<ProductEntity>()
			.Include(x => x.Categories)
			.Include(x => x.Teams).Where(x => x.Id == dto.Id)
			.FirstOrDefaultAsync(ct);

		if (entity == null)
			return new GenericResponse<ProductEntity>(new ProductEntity());

		ProductEntity e = await entity.FillDataV2(dto, _httpContextAccessor, _context);
		_context.Update(e);
		await _context.SaveChangesAsync(ct);

		return new GenericResponse<ProductEntity>(e);
	}

	public async Task<GenericResponse> Delete(Guid id, CancellationToken ct) {
		ProductEntity? i = await _context.Set<ProductEntity>().FindAsync(id, ct);
		if (i != null) {
			await Update(new ProductCreateUpdateDto {
				Id = id,
				DeletedAt = DateTime.Now,
			}, ct);
			return new GenericResponse(message: "Deleted");
		}
		return new GenericResponse(UtilitiesStatusCodes.NotFound, "Notfound");
	}
}

public static class ProductEntityExtensionV2 {
	public static async Task<ProductEntity> FillDataV2(
		this ProductEntity entity,
		ProductCreateUpdateDto dto,
		IHttpContextAccessor httpContextAccessor,
		DbContext context) {
		entity.UserId = httpContextAccessor.HttpContext?.User.Identity?.Name;
		entity.Title = dto.Title ?? entity.Title;
		entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
		entity.Details = dto.Details ?? entity.Details;
		entity.Author = dto.Author ?? entity.Author;
		entity.PhoneNumber = dto.PhoneNumber ?? entity.PhoneNumber;
		entity.Link = dto.Link ?? entity.Link;
		entity.Website = dto.Website ?? entity.Website;
		entity.Email = dto.Email ?? entity.Email;
		entity.Latitude = dto.Latitude ?? entity.Latitude;
		entity.Longitude = dto.Longitude ?? entity.Longitude;
		entity.Description = dto.Description ?? entity.Description;
		entity.UseCase = dto.UseCase ?? entity.UseCase;
		entity.Price = dto.Price ?? entity.Price;
		entity.IsForSale = dto.IsForSale ?? entity.IsForSale;
		entity.Enabled = dto.Enabled ?? entity.Enabled;
		entity.VisitsCount = dto.VisitsCount ?? entity.VisitsCount;
		entity.Length = dto.Length ?? entity.Length;
		entity.Width = dto.Width ?? entity.Width;
		entity.Height = dto.Height ?? entity.Height;
		entity.Weight = dto.Weight ?? entity.Weight;
		entity.MinOrder = dto.MinOrder ?? entity.MinOrder;
		entity.MaxOrder = dto.MaxOrder ?? entity.MaxOrder;
		entity.MinPrice = dto.MinPrice ?? entity.MinPrice;
		entity.MaxPrice = dto.MaxPrice ?? entity.MaxPrice;
		entity.Unit = dto.Unit ?? entity.Unit;
		entity.Address = dto.Address ?? entity.Address;
		entity.StartDate = dto.StartDate ?? entity.StartDate;
		entity.EndDate = dto.EndDate ?? entity.EndDate;
		entity.Status = dto.Status ?? entity.Status;
		entity.DeletedAt = dto.DeletedAt ?? entity.DeletedAt;

		if (dto.VisitsCountPlus.HasValue)
			if (entity.VisitsCount == null) entity.VisitsCount = dto.VisitsCountPlus;
			else entity.VisitsCount += dto.VisitsCountPlus;

		if (dto.ScorePlus.HasValue)
			if (entity.VoteCount == null) entity.VoteCount = dto.ScorePlus;
			else entity.VoteCount += dto.ScorePlus;

		if (dto.ScoreMinus.HasValue)
			if (entity.VoteCount == null) entity.VoteCount = dto.ScoreMinus;
			else entity.VoteCount -= dto.ScorePlus;

		if (dto.Categories.IsNotNullOrEmpty()) {
			List<CategoryEntity> listCategory = new();
			foreach (Guid item in dto.Categories ?? new List<Guid>()) {
				CategoryEntity? e = await context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item);
				if (e != null) listCategory.Add(e);
			}
			entity.Categories = listCategory;
		}

		if (dto.Teams.IsNotNullOrEmpty()) {
			List<TeamEntity> listTeam = new();
			foreach (string item in dto.Teams ?? new List<string>()) {
				UserEntity? e = await context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == item);
				if (e != null) {
					TeamEntity t = new() {UserId = e.Id};
					await context.Set<TeamEntity>().AddAsync(t);
					listTeam.Add(t);
				}
			}
			entity.Teams = listTeam;
		}

		return entity;
	}
}