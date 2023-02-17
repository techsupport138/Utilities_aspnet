namespace Utilities_aspnet.Repositories;

public interface IReportRepository
{
	Task<GenericResponse<ReportEntity?>> Create(ReportEntity dto);
	GenericResponse<IQueryable<ReportEntity>> Read(ReportFilterDto dto);
	GenericResponse<List<string>> TopReport(ReportFilterDto dto);

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