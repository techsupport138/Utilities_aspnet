using Utilities_aspnet.Entities;

namespace Utilities_aspnet.Repositories;

public interface IProductRepository
{
    Task<GenericResponse<ProductEntity>> Create(ProductCreateUpdateDto dto, CancellationToken ct);
    Task<GenericResponse<ProductEntity>> CreateWithFiles(ProductCreateUpdateDto dto, CancellationToken ct);
    GenericResponse<IQueryable<ProductEntity>> Filter(ProductFilterDto dto);
    Task<GenericResponse<ProductEntity?>> ReadById(Guid id, CancellationToken ct);
    Task<GenericResponse<ProductEntity>> Update(ProductCreateUpdateDto dto, CancellationToken ct);
    Task<GenericResponse> Delete(Guid id, CancellationToken ct);
}

public class ProductRepository : IProductRepository
{
    private readonly DbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFollowBookmarkRepository _followBookmarkRepository;
    private readonly IUploadRepository _uploadRepository;

    public ProductRepository(DbContext dbContext, IHttpContextAccessor httpContextAccessor, IFollowBookmarkRepository followBookmarkRepository, IUploadRepository uploadRepository)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _followBookmarkRepository = followBookmarkRepository;
        _uploadRepository = uploadRepository;
    }

    public async Task<GenericResponse<ProductEntity>> Create(ProductCreateUpdateDto dto, CancellationToken ct)
    {
        ProductEntity entity = new();

        if (dto.ProductInsight is not null)
            dto.ProductInsight.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name;

        ProductEntity e = await entity.FillData(dto, _dbContext);
        e.VisitsCount = 1;
        e.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        e.CreatedAt = DateTime.Now;

        EntityEntry<ProductEntity> i = await _dbContext.Set<ProductEntity>().AddAsync(e, ct);
        await _dbContext.SaveChangesAsync(ct);

        return new GenericResponse<ProductEntity>(i.Entity);
    }

    public async Task<GenericResponse<ProductEntity>> CreateWithFiles(ProductCreateUpdateDto dto, CancellationToken ct)
    {
        ProductEntity entity = new();
        List<MediaEntity> medias = new();
        if (dto.Upload is not null)
        {
            var mediaList = await _uploadRepository.Upload(dto.Upload);
            if (mediaList.Result is not null)
            {
                medias.AddRange(mediaList.Result);
            }
        }

        if (dto.ProductInsight is not null)
            dto.ProductInsight.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name;

        ProductEntity e = await entity.FillData(dto, _dbContext);
        e.Media = medias;
        e.VisitsCount = 1;
        e.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        e.CreatedAt = DateTime.Now;

        EntityEntry<ProductEntity> i = await _dbContext.Set<ProductEntity>().AddAsync(e, ct);
        await _dbContext.SaveChangesAsync(ct);

        return new GenericResponse<ProductEntity>(i.Entity);
    }


    public GenericResponse<IQueryable<ProductEntity>> Filter(ProductFilterDto dto)
    {
        IQueryable<ProductEntity> q = _dbContext.Set<ProductEntity>();
        q = q.Where(x => x.DeletedAt == null);
        if (!dto.ShowExpired) q = q.Where(w => w.ExpireDate == null || w.ExpireDate >= DateTime.Now);

        string? guestUser = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        if (dto.FilterByAge != null && dto.FilterByAge == true && !string.IsNullOrEmpty(guestUser))
        {
            UserEntity? user = _dbContext.Set<UserEntity>().FirstOrDefault(f => f.Id == guestUser);
            if (user != null && user.Birthdate.HasValue)
            {
                AgeCategory ageCatg = Utils.CalculateAgeCategories(user.Birthdate.Value);
                q = q.Where(w => w.AgeCategory == ageCatg);
            }
            q = q.Where(x => x.UserId == dto.UserId);
        }

        if (dto.AgeCategory is not null)
            q.Where(w => w.AgeCategory == dto.AgeCategory);

        if (dto.FilterByAge == null && dto.UserId.IsNotNullOrEmpty()) q = q.Where(x => x.UserId == dto.UserId);

        if (dto.Title.IsNotNullOrEmpty()) q = q.Where(x => (x.Title ?? "").Contains(dto.Title!));
        if (dto.Subtitle.IsNotNullOrEmpty()) q = q.Where(x => (x.Subtitle ?? "").Contains(dto.Subtitle!));
        if (dto.Type.IsNotNullOrEmpty()) q = q.Where(x => (x.Type ?? "").Contains(dto.Type!));
        if (dto.Details.IsNotNullOrEmpty()) q = q.Where(x => (x.Details ?? "").Contains(dto.Details!));
        if (dto.Description.IsNotNullOrEmpty()) q = q.Where(x => (x.Description ?? "").Contains(dto.Description!));
        if (dto.Author.IsNotNullOrEmpty()) q = q.Where(x => (x.Author ?? "").Contains(dto.Author!));
        if (dto.Email.IsNotNullOrEmpty()) q = q.Where(x => (x.Email ?? "").Contains(dto.Email!));
        if (dto.PhoneNumber.IsNotNullOrEmpty()) q = q.Where(x => (x.PhoneNumber ?? "").Contains(dto.PhoneNumber!));
        if (dto.Address.IsNotNullOrEmpty()) q = q.Where(x => (x.Address ?? "").Contains(dto.Address!));
        if (dto.KeyValues1.IsNotNullOrEmpty()) q = q.Where(x => (x.KeyValues1 ?? "").Contains(dto.KeyValues1!));
        if (dto.KeyValues2.IsNotNullOrEmpty()) q = q.Where(x => (x.KeyValues2 ?? "").Contains(dto.KeyValues2!));
        if (dto.Unit.IsNotNullOrEmpty()) q = q.Where(x => x.Unit == dto.Unit);
        if (dto.UseCase.IsNotNullOrEmpty()) q = q.Where(x => x.UseCase.Contains(dto.UseCase));
        if (dto.State.IsNotNullOrEmpty()) q = q.Where(x => x.State == dto.State);
        if (dto.StateTr1.IsNotNullOrEmpty()) q = q.Where(x => x.StateTr1 == dto.StateTr2);
        if (dto.StateTr2.IsNotNullOrEmpty()) q = q.Where(x => x.StateTr2 == dto.StateTr2);
        if (dto.StartPriceRange.HasValue) q = q.Where(x => x.Price >= dto.StartPriceRange);
        if (dto.Currency.HasValue) q = q.Where(x => x.Currency == dto.Currency);
        if (dto.HasComment.IsTrue()) q = q.Where(x => x.Comments.Any());
        if (dto.HasOrder.IsTrue()) q = q.Where(x => x.OrderDetails.Any());
        if (dto.HasDiscount.IsTrue()) q = q.Where(x => x.DiscountPercent != null || x.DiscountPrice != null);
        if (dto.EndPriceRange.HasValue) q = q.Where(x => x.Price <= dto.EndPriceRange);
        if (dto.Enabled.HasValue) q = q.Where(x => x.Enabled == dto.Enabled);
        if (dto.IsForSale.HasValue) q = q.Where(x => x.IsForSale == dto.IsForSale);
        if (dto.VisitsCount.HasValue) q = q.Where(x => x.VisitsCount == dto.VisitsCount);
        if (dto.Length.HasValue) q = q.Where(x => x.Length.ToInt() == dto.Length.ToInt());
        if (dto.ResponseTime.HasValue) q = q.Where(x => x.ResponseTime.ToInt() == dto.ResponseTime.ToInt());
        if (dto.OnTimeDelivery.HasValue) q = q.Where(x => x.OnTimeDelivery.ToInt() == dto.OnTimeDelivery.ToInt());
        if (dto.Length.HasValue) q = q.Where(x => x.Length.ToInt() == dto.Length.ToInt());
        if (dto.Status.HasValue) q = q.Where(x => x.Status == dto.Status);
        if (dto.Width.HasValue) q = q.Where(x => x.Width.ToInt() == dto.Width.ToInt());
        if (dto.Height.HasValue) q = q.Where(x => x.Height.ToInt() == dto.Height.ToInt());
        if (dto.Weight.HasValue) q = q.Where(x => x.Weight.ToInt() == dto.Weight.ToInt());
        if (dto.MinOrder.HasValue) q = q.Where(x => x.MinOrder >= dto.MinOrder);
        if (dto.MaxOrder.HasValue) q = q.Where(x => x.MaxOrder <= dto.MaxOrder);
        if (dto.MinPrice.HasValue) q = q.Where(x => x.MinPrice >= dto.MinPrice);
        if (dto.MaxPrice.HasValue) q = q.Where(x => x.MaxPrice <= dto.MaxPrice);
        if (dto.StartDate.HasValue) q = q.Where(x => x.StartDate >= dto.StartDate);
        if (dto.EndDate.HasValue) q = q.Where(x => x.EndDate <= dto.EndDate);
        if (dto.Query.IsNotNullOrEmpty())
            q = q.Where(x => (x.Title ?? "").Contains(dto.Query!) ||
                             (x.Subtitle ?? "").Contains(dto.Query!) ||
                             (x.Author ?? "").Contains(dto.Query!) ||
                             (x.Details ?? "").Contains(dto.Query!) ||
                             (x.Description ?? "").Contains(dto.Query!));

        if (dto.ShowCategories.IsTrue()) q = q.Include(i => i.Categories);
        if (dto.ShowCategoriesFormFields.IsTrue()) q = q.Include(i => i.Categories).ThenInclude(i => i.FormFields);
        if (dto.ShowCategoryMedia.IsTrue()) q = q.Include(i => i.Categories)!.ThenInclude(i => i.Media);
        if (dto.ShowComments.IsTrue()) q = q.Include(i => i.Comments)!.ThenInclude(i => i.LikeComments);
        if (dto.ShowOrders.IsTrue()) q = q.Include(i => i.OrderDetails);
        if (dto.ShowForms.IsTrue()) q = q.Include(i => i.Forms);
        if (dto.ShowFormFields.IsTrue()) q = q.Include(i => i.Forms)!.ThenInclude(i => i.FormField);
        if (dto.ShowMedia.IsTrue()) q = q.Include(i => i.Media);
        if (dto.ShowReports.IsTrue()) q = q.Include(i => i.Reports);
        if (dto.ShowTeams.IsTrue()) q = q.Include(i => i.Teams)!.ThenInclude(x => x.User).ThenInclude(x => x!.Media);
        if (dto.ShowVotes.IsTrue()) q = q.Include(i => i.Votes);
        if (dto.ShowVoteFields.IsTrue()) q = q.Include(i => i.VoteFields);
        if (dto.ShowCreator.IsTrue()) q = q.Include(i => i.User).ThenInclude(x => x!.Media);
        if (dto.ShowVisitProducts.IsTrue()) q = q.Include(i => i.VisitProducts);
        if (dto.Categories != null && dto.Categories.Any()) q = q.Where(x => x.Categories.Any(y => dto.Categories.ToList().Contains(y.Id)));
        if (dto.CategoriesAnd != null && dto.CategoriesAnd.Any()) q = q.Where(x => x.Categories.All(y => dto.CategoriesAnd.ToList().Contains(y.Id)));
        if (dto.OrderByVotes.IsTrue()) q = q.OrderBy(x => x.VoteCount);
        if (dto.OrderByVotesDecending.IsTrue()) q = q.OrderByDescending(x => x.VoteCount);
        if (dto.OrderByAtoZ.IsTrue()) q = q.OrderBy(x => x.Title);
        if (dto.OrderByZtoA.IsTrue()) q = q.OrderByDescending(x => x.Title);
        if (dto.OrderByPriceAccending.IsTrue()) q = q.OrderBy(x => x.Price);
        if (dto.OrderByPriceDecending.IsTrue()) q = q.OrderByDescending(x => x.Price);
        if (dto.OrderByCreatedDate.IsTrue()) q = q.OrderByDescending(x => x.CreatedAt);
        if (dto.OrderByCreaedDateDecending.IsTrue()) q = q.OrderByDescending(x => x.CreatedAt);

        if (dto.MinValue.HasValue)
            q = q.Where(x => x.Value.ToInt() >= dto.MinValue ||
                             x.Value1.ToInt() >= dto.MinValue ||
                             x.Value2.ToInt() >= dto.MinValue ||
                             x.Value3.ToInt() >= dto.MinValue ||
                             x.Value4.ToInt() >= dto.MinValue ||
                             x.Value5.ToInt() >= dto.MinValue ||
                             x.Value6.ToInt() >= dto.MinValue ||
                             x.Value7.ToInt() >= dto.MinValue ||
                             x.Value8.ToInt() >= dto.MinValue ||
                             x.Value9.ToInt() >= dto.MinValue ||
                             x.Value10.ToInt() >= dto.MinValue ||
                             x.Value11.ToInt() >= dto.MinValue ||
                             x.Value12.ToInt() >= dto.MinValue);

        if (dto.MaxValue.HasValue)
            q = q.Where(x => x.Value.ToInt() <= dto.MaxValue ||
                             x.Value1.ToInt() <= dto.MaxValue ||
                             x.Value2.ToInt() <= dto.MaxValue ||
                             x.Value3.ToInt() <= dto.MaxValue ||
                             x.Value4.ToInt() <= dto.MaxValue ||
                             x.Value5.ToInt() <= dto.MaxValue ||
                             x.Value6.ToInt() <= dto.MaxValue ||
                             x.Value7.ToInt() <= dto.MaxValue ||
                             x.Value8.ToInt() <= dto.MaxValue ||
                             x.Value9.ToInt() <= dto.MaxValue ||
                             x.Value10.ToInt() <= dto.MaxValue ||
                             x.Value11.ToInt() <= dto.MaxValue ||
                             x.Value12.ToInt() <= dto.MaxValue);
        if (dto.IsFollowing)
        {
            var following = _followBookmarkRepository.GetFollowing(dto.UserId ?? "");
            q = q.Where(x => following.Result.ToList().Any(y => y.Id == x.UserId));
        }

        q.Where(w => w.VisitProducts != null)
                       .Where(w => w.VisitProducts.Any(a => a.ProductId == w.Id && a.UserId != (!string.IsNullOrEmpty(guestUser) ? guestUser : "")))
                       .ToList()
                       .ForEach(f => f.IsSeen = true);

        int totalCount = q.Count();
        q = q.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);

        return new GenericResponse<IQueryable<ProductEntity>>(q.AsNoTracking())
        {
            TotalCount = totalCount,
            PageCount = totalCount % dto.PageSize == 0 ? totalCount / dto.PageSize : totalCount / dto.PageSize + 1,
            PageSize = dto.PageSize
        };
    }

    public async Task<GenericResponse<ProductEntity?>> ReadById(Guid id, CancellationToken ct)
    {
        ProductEntity? i = await _dbContext.Set<ProductEntity>()
            .Include(i => i.Media)
            .Include(i => i.Categories).ThenInclude(x => x.Media)
            .Include(i => i.Reports)
            .Include(i => i.Bookmarks)
            .Include(i => i.Votes)
            .Include(i => i.Comments)!.ThenInclude(x => x.LikeComments)
            .Include(i => i.User).ThenInclude(x => x.Media)
            .Include(i => i.User).ThenInclude(x => x.Categories)
            .Include(i => i.Forms)!.ThenInclude(x => x.FormField)
            .Include(i => i.Teams)!.ThenInclude(x => x.User).ThenInclude(x => x.Media)
            .Include(i => i.VoteFields)!.ThenInclude(x => x.Votes)
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null, ct);
        if (i == null) return new GenericResponse<ProductEntity?>(null, UtilitiesStatusCodes.NotFound, "Not Found");

        string? userId = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        UserEntity? user = await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(f => f.Id == userId, ct);
        if (user is not null)
        {
            var vp = await _dbContext.Set<VisitProducts>().FirstOrDefaultAsync(a => a.UserId == user.Id && a.ProductId == i.Id);
            if (vp is null)
            {
                VisitProducts visitProduct = new()
                {
                    CreatedAt = DateTime.Now,
                    ProductId = i.Id,
                    UserId = user.Id,
                };
                await _dbContext.Set<VisitProducts>().AddAsync(visitProduct, ct);
            }
            if (i.VisitProducts != null && !i.VisitProducts.Any()) i.VisitsCount = 1;
            else if (i.VisitProducts != null) i.VisitsCount = i.VisitProducts.Count() + 1;
            _dbContext.Update(i);
            await _dbContext.SaveChangesAsync(ct);
        }

        if (i.ProductInsights?.Any() != null)
        {
            i.ProductInsights.GroupBy(g => g.Reaction).ToList().ForEach(item =>
                item.Select(s => s.Count == item.Count()));
        }

        i.Comments = _dbContext.Set<CommentEntity>().Where(w => w.ProductId == i.Id && w.DeletedAt == null);
        i.CommentsCount = i.Comments.Count();
        i.IsSeen = true;

        return new GenericResponse<ProductEntity?>(i);
    }

    public async Task<GenericResponse<ProductEntity>> Update(ProductCreateUpdateDto dto, CancellationToken ct)
    {
        ProductEntity? entity = await _dbContext.Set<ProductEntity>()
            .Include(x => x.Categories)
            .Include(x => x.Teams)
            .Where(x => x.Id == dto.Id)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
            return new GenericResponse<ProductEntity>(new ProductEntity());

        if (dto.ProductInsight is not null)
            dto.ProductInsight.UserId = _httpContextAccessor.HttpContext!.User.Identity!.Name;

        ProductEntity e = await entity.FillData(dto, _dbContext);
        _dbContext.Update(e);
        await _dbContext.SaveChangesAsync(ct);

        return new GenericResponse<ProductEntity>(e);
    }

    public async Task<GenericResponse> Delete(Guid id, CancellationToken ct)
    {
        ProductEntity? i = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (i != null)
        {
            i.DeletedAt = DateTime.Now;
            _dbContext.Update(i);
            await _dbContext.SaveChangesAsync(ct);
            return new GenericResponse(message: "Deleted");
        }
        return new GenericResponse(UtilitiesStatusCodes.NotFound, "Notfound");
    }

}

public static class ProductEntityExtension
{
    public static async Task<ProductEntity> FillData(this ProductEntity entity, ProductCreateUpdateDto dto, DbContext context)
    {
        entity.Title = dto.Title ?? entity.Title;
        entity.Subtitle = dto.Subtitle ?? entity.Subtitle;
        entity.Details = dto.Details ?? entity.Details;
        entity.Author = dto.Author ?? entity.Author;
        entity.PhoneNumber = dto.PhoneNumber ?? entity.PhoneNumber;
        entity.Link = dto.Link ?? entity.Link;
        entity.Website = dto.Website ?? entity.Website;
        entity.Email = dto.Email ?? entity.Email;
        entity.Type = dto.Type ?? entity.Type;
        entity.State = dto.State ?? entity.State;
        entity.StateTr1 = dto.StateTr1 ?? entity.StateTr1;
        entity.StateTr2 = dto.StateTr2 ?? entity.StateTr2;
        entity.Latitude = dto.Latitude ?? entity.Latitude;
        entity.RelatedIds = dto.RelatedIds ?? entity.RelatedIds;
        entity.ResponseTime = dto.ResponseTime ?? entity.ResponseTime;
        entity.OnTimeDelivery = dto.OnTimeDelivery ?? entity.OnTimeDelivery;
        entity.DiscountPrice = dto.DiscountPrice ?? entity.DiscountPrice;
        entity.DiscountPercent = dto.DiscountPercent ?? entity.DiscountPercent;
        entity.Longitude = dto.Longitude ?? entity.Longitude;
        entity.Description = dto.Description ?? entity.Description;
        entity.UseCase = dto.UseCase ?? entity.UseCase;
        entity.Price = dto.Price ?? entity.Price;
        entity.IsForSale = dto.IsForSale ?? entity.IsForSale;
        entity.Enabled = dto.Enabled ?? entity.Enabled;
        entity.VisitsCount = dto.VisitsCount ?? entity.VisitsCount;
        entity.Length = dto.Length ?? entity.Length;
        entity.Value = dto.Value ?? entity.Value;
        entity.Value1 = dto.Value1 ?? entity.Value1;
        entity.Value2 = dto.Value2 ?? entity.Value2;
        entity.Value3 = dto.Value3 ?? entity.Value3;
        entity.Value4 = dto.Value4 ?? entity.Value4;
        entity.Value5 = dto.Value5 ?? entity.Value5;
        entity.Value6 = dto.Value6 ?? entity.Value6;
        entity.Value7 = dto.Value7 ?? entity.Value7;
        entity.Value8 = dto.Value8 ?? entity.Value8;
        entity.Value9 = dto.Value9 ?? entity.Value9;
        entity.Value10 = dto.Value10 ?? entity.Value10;
        entity.Value11 = dto.Value11 ?? entity.Value11;
        entity.Value12 = dto.Value12 ?? entity.Value12;
        entity.Width = dto.Width ?? entity.Width;
        entity.Height = dto.Height ?? entity.Height;
        entity.Weight = dto.Weight ?? entity.Weight;
        entity.MinOrder = dto.MinOrder ?? entity.MinOrder;
        entity.MaxOrder = dto.MaxOrder ?? entity.MaxOrder;
        entity.MinPrice = dto.MinPrice ?? entity.MinPrice;
        entity.Shipping = dto.Shipping ?? entity.Shipping;
        entity.Port = dto.Port ?? entity.Port;
        entity.KeyValues1 = dto.KeyValues1 ?? entity.KeyValues1;
        entity.KeyValues2 = dto.KeyValues2 ?? entity.KeyValues2;
        entity.Packaging = dto.Packaging ?? entity.Packaging;
        entity.MaxPrice = dto.MaxPrice ?? entity.MaxPrice;
        entity.Unit = dto.Unit ?? entity.Unit;
        entity.Stock = dto.Stock ?? entity.Stock;
        entity.Address = dto.Address ?? entity.Address;
        entity.StartDate = dto.StartDate ?? entity.StartDate;
        entity.EndDate = dto.EndDate ?? entity.EndDate;
        entity.Status = dto.Status ?? entity.Status;
        entity.Currency = dto.Currency ?? entity.Currency;
        entity.DeletedAt = dto.DeletedAt ?? entity.DeletedAt;
        entity.UpdatedAt = DateTime.Now;
        entity.ExpireDate = dto.ExpireDate ?? entity.ExpireDate;
        entity.AgeCategory = dto.AgeCategory ?? entity.AgeCategory;

        if (dto.VisitsCountPlus.HasValue)
        {
            if (entity.VisitsCount == null) entity.VisitsCount = 1;
            else entity.VisitsCount += dto.VisitsCountPlus;
        }

        if (dto.ScorePlus.HasValue)
        {
            if (entity.VoteCount == null) entity.VoteCount = 1;
            else entity.VoteCount += dto.ScorePlus;
        }

        if (dto.ScoreMinus.HasValue)
        {
            if (entity.VoteCount == null) entity.VoteCount = 1;
            else entity.VoteCount -= dto.ScoreMinus;
        }

        if (dto.Categories.IsNotNullOrEmpty())
        {
            List<CategoryEntity> listCategory = new();
            foreach (Guid item in dto.Categories ?? new List<Guid>())
            {
                CategoryEntity? e = await context.Set<CategoryEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null) listCategory.Add(e);
            }
            entity.Categories = listCategory;
        }

        if (dto.Teams.IsNotNullOrEmpty())
        {
            List<TeamEntity> listTeam = new();
            foreach (string item in dto.Teams ?? new List<string>())
            {
                UserEntity? e = await context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == item);
                if (e != null)
                {
                    TeamEntity t = new() { UserId = e.Id };
                    await context.Set<TeamEntity>().AddAsync(t);
                    listTeam.Add(t);
                }
            }
            entity.Teams = listTeam;
        }

        if (dto.ProductInsight is not null)
        {
            List<ProductInsight> productInsights = new();
            ProductInsightDto? pInsight = dto.ProductInsight;
            UserEntity? e = await context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Id == pInsight.UserId);
            if (e != null)
            {
                ProductInsight pI;
                ProductInsight? oldProductInsight = await context.Set<ProductInsight>().FirstOrDefaultAsync(f => f.UserId == e.Id && f.ProductId == entity.Id.ToString());
                if (oldProductInsight is not null && oldProductInsight.Reaction != pInsight.Reaction)
                {
                    pI = new ProductInsight
                    {
                        UserId = e.Id,
                        Reaction = pInsight.Reaction,
                        UpdatedAt = DateTime.Now
                    };
                }
                else
                {
                    pI = new ProductInsight
                    {
                        UserId = e.Id,
                        Reaction = pInsight.Reaction,
                        CreatedAt = DateTime.Now
                    };
                }
                await context.Set<ProductInsight>().AddAsync(pI);
                productInsights.Add(pI);
            }
            entity.ProductInsights = productInsights;
        }
        return entity;
    }
}