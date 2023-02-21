namespace Utilities_aspnet.Repositories;

public interface IReportRepository
{
	Task<GenericResponse<ReportEntity?>> Create(ReportEntity dto);
	GenericResponse<IQueryable<ReportEntity>> Read(ReportFilterDto dto);
	GenericResponse<List<string>> TopReport(ReportFilterDto dto);
	GenericResponse<List<ReportResponseDto>> CompletationInformation(ReportFilterDto dto);
	Task<GenericResponse<ReportEntity?>> ReadById(Guid id);
	Task<GenericResponse> Delete(Guid id);
}

public class ReportRepository : IReportRepository
{
	private readonly DbContext _dbContext;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public ReportRepository(DbContext context, IHttpContextAccessor httpContextAccessor)
	{
		_dbContext = context;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<GenericResponse<ReportEntity?>> Create(ReportEntity dto)
	{
		ReportEntity entity = new()
		{
			CreatorUserId = _httpContextAccessor.HttpContext!.User.Identity!.Name!,
			Title = dto.Title,
			Description = dto.Description
		};
		if (dto.ProductId.HasValue) entity.ProductId = dto.ProductId;
		if (!dto.UserId.IsNotNullOrEmpty()) entity.UserId = dto.UserId;
		await _dbContext.Set<ReportEntity>().AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return await ReadById(entity.Id);
	}

	public GenericResponse<IQueryable<ReportEntity>> Read(ReportFilterDto dto)
	{
		IQueryable<ReportEntity> entities = _dbContext.Set<ReportEntity>().AsNoTracking();
		if (dto.User == true) entities = entities.Include(x => x.User).ThenInclude(x => x!.Media);
		if (dto.Product == true) entities = entities.Include(x => x.Product).ThenInclude(x => x!.Media);
		return new GenericResponse<IQueryable<ReportEntity>>(entities);
	}

	public GenericResponse<List<string>> TopReport(ReportFilterDto dto)
	{
		List<string> keywords = new List<string>();
		if (dto.ReportType == ReportType.TopKeyword)
		{
			int count = dto.Count ?? 10;
			string[] useCase = new string[] { "product", "capacity" };
			IQueryable<ProductEntity> plist1 = _dbContext.Set<ProductEntity>().AsNoTracking();
			if (dto.UserId.IsNotNullOrEmpty()) plist1 = plist1.Where(x => x.UserId == dto.UserId);
			plist1 = plist1.Where(o => o.KeyValues2 != null && o.KeyValues2 != "");
			plist1 = plist1.Where(o => useCase.Contains(o.UseCase)).Take(count).OrderByDescending(p => p.CreatedAt);

			IQueryable<ProductEntity> plist2 = _dbContext.Set<ProductEntity>().AsNoTracking();
			if (dto.UserId.IsNotNullOrEmpty())
			{
				plist2 = plist2.Where(x => x.UserId == dto.UserId);
				plist2 = plist2.Where(x => x.Bookmarks != null && x.Bookmarks.Any(b => b.UserId == dto.UserId)).OrderByDescending(b => b.CreatedAt);
			}
			plist2 = plist2.Where(o => o.KeyValues2 != null && o.KeyValues2 != "");
			plist2 = plist2.Where(o => useCase.Contains(o.UseCase)).Take(count);

			var plists = (plist1.Concat(plist2)).Select(p => p.KeyValues2);


			foreach (var item in plists.ToList())
			{
				keywords.AddRange(item.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList());
			}
			keywords = keywords.GroupBy(s => s.Trim()).OrderByDescending(x => x.Count()).Take(count).Select(x => x.First()).ToList();


		}


		return new GenericResponse<List<string>>(keywords);
	}

	public GenericResponse<List<ReportResponseDto>> CompletationInformation(ReportFilterDto dto)
	{
		List<ReportResponseDto> outputReport = new List<ReportResponseDto>();
		if (true)
		{
			string[] useCase = new string[] { "product", "capacity", "supplier" };
			IQueryable<ProductEntity> p = _dbContext.Set<ProductEntity>().Include(i=>i.Categories).AsNoTracking();
			if (dto.UserId.IsNotNullOrEmpty()) p = p.Where(x => x.UserId == dto.UserId);
			List<ProductEntity> plist = p.Where(o => useCase.Contains(o.UseCase) && o.DeletedAt == null).ToList();
			if (plist is null || plist.Count()==0) return new GenericResponse<List<ReportResponseDto>>(null, UtilitiesStatusCodes.NotFound);

			#region profile
				List<int> pcount = new List<int>();
				ProductEntity profile = plist.Where(p => p.UseCase == "supplier").FirstOrDefault();
				if (profile != null)
				{
					pcount.Add(profile.Title.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Subtitle.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Description.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Details.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Address.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.PhoneNumber.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Link.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Website.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(profile.Email.IsNullOrEmpty() ? 0 : 1);
				}
				ReportResponseDto profileCompletation = new ReportResponseDto();
				profileCompletation.Title = "profileCompletation";
				profileCompletation.Count = profile?.VisitsCount;
				profileCompletation.Total = Math.Round(pcount.Average(),2);

			#endregion

			#region product
			List<double> productscount = new List<double>();
			int totalProductVisitCount=0;
			foreach (ProductEntity product in plist.Where(p => p.UseCase == "product"))
			{
				List<int> prcount = new List<int>();
				//ProductEntity product = plist.Where(p => p.UseCase == "product").FirstOrDefault();
				if (product != null)
				{
					pcount.Add(product.Title.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.Subtitle.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.Description.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 1 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 2 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 3 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 4 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 5 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 6 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 7 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 8 ? 0 : 1);
					pcount.Add(product.Categories?.Count() > 9 ? 0 : 1);

					pcount.Add(product.Unit.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.State.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.Packaging.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.Shipping.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.KeyValues2.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(product.Width==null ? 0 : 1);
					pcount.Add(product.Height == null ? 0 : 1);
					pcount.Add(product.Weight == null ? 0 : 1);
					pcount.Add(product.MinOrder == null ? 0 : 1);
					pcount.Add(product.MaxOrder == null ? 0 : 1);
					pcount.Add(product.MaxPrice == null ? 0 : 1);
					pcount.Add(product.MinPrice == null ? 0 : 1);
					pcount.Add(product.Latitude == null ? 0 : 1);
					pcount.Add(product.Longitude == null ? 0 : 1);
					pcount.Add(product.Email.IsNullOrEmpty() ? 0 : 1);

					totalProductVisitCount += product.VisitsCount ?? 0;

					productscount.Add(pcount.Average());

					
				}
			}
		
				ReportResponseDto productsCompletation = new ReportResponseDto();
				productsCompletation.Title = "productsCompletation";
				productsCompletation.Count = totalProductVisitCount;
				productsCompletation.Total = Math.Round(productscount.Average(),2);
				productsCompletation.ProductId = plist.Where(p => p.UseCase == "product").MaxBy(p => p.VisitsCount)?.Id ?? null;

			#endregion

			#region capacity
			List<double> capacityscount = new List<double>();
			int totalCapacityVisitCount = 0;
			foreach (ProductEntity capacity in plist.Where(p => p.UseCase == "capacity"))
			{
				List<int> prcount = new List<int>();
				//capacityEntity product = plist.Where(p => p.UseCase == "product").FirstOrDefault();
				if (capacity != null)
				{
					pcount.Add(capacity.Title.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Subtitle.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Description.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 1 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 2 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 3 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 4 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 5 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 6 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 7 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 8 ? 0 : 1);
					pcount.Add(capacity.Categories?.Count() > 9 ? 0 : 1);

					pcount.Add(capacity.Unit.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.State.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Packaging.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Shipping.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.KeyValues2.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Width == null ? 0 : 1);
					pcount.Add(capacity.Height == null ? 0 : 1);
					pcount.Add(capacity.Weight == null ? 0 : 1);
					pcount.Add(capacity.MinOrder == null ? 0 : 1);
					pcount.Add(capacity.MaxOrder == null ? 0 : 1);
					pcount.Add(capacity.MaxPrice == null ? 0 : 1);
					pcount.Add(capacity.MinPrice == null ? 0 : 1);
					pcount.Add(capacity.Latitude == null ? 0 : 1);
					pcount.Add(capacity.Longitude == null ? 0 : 1);
					pcount.Add(capacity.Email.IsNullOrEmpty() ? 0 : 1);

					pcount.Add(capacity.Value.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value1.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value2.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value3.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value4.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value5.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value6.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value7.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value8.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value9.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value10.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value11.IsNullOrEmpty() ? 0 : 1);
					pcount.Add(capacity.Value12.IsNullOrEmpty() ? 0 : 1);

					totalCapacityVisitCount += capacity.VisitsCount ?? 0;

					capacityscount.Add(pcount.Average());
				}
			}
			ReportResponseDto capacitysCompletation = new ReportResponseDto();
			capacitysCompletation.Title = "capacitysCompletation";
			capacitysCompletation.Count = totalCapacityVisitCount;
			capacitysCompletation.Total = Math.Round(capacityscount.Average(),2);
			capacitysCompletation.ProductId = plist.Where(p => p.UseCase == "capacity").MaxBy(p => p.VisitsCount)?.Id ?? null;

			#endregion

			outputReport.Add(profileCompletation);
			outputReport.Add(productsCompletation);
			outputReport.Add(capacitysCompletation);

		}


		return new GenericResponse<List<ReportResponseDto>>(outputReport);
	}

	public async Task<GenericResponse> Delete(Guid id)
	{
		ReportEntity? report = await _dbContext.Set<ReportEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		if (report == null) return new GenericResponse(UtilitiesStatusCodes.NotFound);
		_dbContext.Set<ReportEntity>().Remove(report);
		await _dbContext.SaveChangesAsync();
		return new GenericResponse();
	}

	public async Task<GenericResponse<ReportEntity?>> ReadById(Guid id)
	{
		ReportEntity? entity = await _dbContext.Set<ReportEntity>()
			.Include(x => x.User)
			.Include(x => x.Product)
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);
		return new GenericResponse<ReportEntity?>(entity);
	}


}