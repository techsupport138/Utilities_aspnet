namespace Utilities_aspnet.Repositories;

public interface IDiscountRepository
{
    public Task<GenericResponse<DiscountReadDto>> Create(DiscountCreateUpdateDto dto);
    public GenericResponse<IQueryable<DiscountEntity>> Filter(DiscountFilterDto dto);
    public Task<GenericResponse<DiscountReadDto?>> Update(DiscountCreateUpdateDto dto);
    public Task<GenericResponse> Delete(Guid id);
    Task<GenericResponse<DiscountEntity>> ReadDiscountCode(string code);
}

public class DiscountRepository : IDiscountRepository
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DiscountRepository(
        DbContext dbContext,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<DiscountReadDto>> Create(DiscountCreateUpdateDto dto)
    {
        DiscountEntity entity = _mapper.Map<DiscountEntity>(dto);

        EntityEntry<DiscountEntity> i = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<DiscountReadDto>(_mapper.Map<DiscountReadDto>(i.Entity));
    }

    public GenericResponse<IQueryable<DiscountEntity>> Filter(DiscountFilterDto dto)
    {
        IQueryable<DiscountEntity> q = _dbContext.Set<DiscountEntity>().AsNoTracking();

        if (dto.Title.IsNotNullOrEmpty()) q = q.Where(x => (x.Title ?? "").Contains(dto.Title!));
        if (dto.Code.IsNotNullOrEmpty()) q = q.Where(x => (x.Code ?? "").Contains(dto.Code!));
        if (dto.DiscountPercent != null) q = q.Where(x => x.DiscountPercent == dto.DiscountPercent);
        if (dto.NumberUses != null) q = q.Where(x => x.NumberUses == dto.NumberUses);
        if (dto.StartDate != null) q = q.Where(x => x.StartDate <= dto.StartDate);
        if (dto.EndDate != null) q = q.Where(x => x.EndDate >= dto.EndDate);

        int totalCount = q.Count();

        q = q.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).AsNoTracking();

        IEnumerable<DiscountReadDto> readDto = _mapper.Map<IEnumerable<DiscountReadDto>>(q);

        return new GenericResponse<IQueryable<DiscountEntity>>(q)
        {
            TotalCount = totalCount,
            PageCount = totalCount % dto.PageSize == 0 ? totalCount / dto?.PageSize : totalCount / dto?.PageSize + 1,
            PageSize = dto?.PageSize
        };
    }

    public async Task<GenericResponse<DiscountReadDto?>> Update(DiscountCreateUpdateDto dto)
    {
        DiscountEntity? entity = await _dbContext.Set<DiscountEntity>().FirstOrDefaultAsync(item => item.Id == dto.Id);

        if (entity == null) return new GenericResponse<DiscountReadDto?>(null, UtilitiesStatusCodes.NotFound);
        entity.Title = dto.Title ?? entity.Title;
        entity.DiscountPercent = dto.DiscountPercent ?? entity.DiscountPercent;
        entity.NumberUses = dto.NumberUses ?? entity.NumberUses;
        entity.Code = dto.Code ?? entity.Code;
        entity.StartDate = dto.StartDate ?? entity.StartDate;
        entity.EndDate = dto.EndDate ?? entity.EndDate;
        entity.UpdatedAt = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<DiscountReadDto?>(_mapper.Map<DiscountReadDto>(entity));
    }

    public async Task<GenericResponse> Delete(Guid id)
    {
        DiscountEntity? i = await _dbContext.Set<DiscountEntity>().FirstOrDefaultAsync(i => i.Id == id);

        if (i != null)
        {
            _dbContext.Remove(i);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            return new GenericResponse(UtilitiesStatusCodes.NotFound, "Notfound");
        }

        return new GenericResponse();
    }

    public async Task<GenericResponse<DiscountEntity>> ReadDiscountCode(string code)
    {
        string userId = _httpContextAccessor.HttpContext?.User.Identity?.Name!;
        var discountEntity = await _dbContext.Set<DiscountEntity>().FirstOrDefaultAsync(p => p.Code.ToLower().Trim() == code.ToLower().Trim());
        if (discountEntity == null)
            throw new ArgumentException("Code not found!");

        var orders = await _dbContext.Set<OrderEntity>().Where(p => p.UserId == userId && p.DiscountCode == code && p.Status != OrderStatuses.Canceled).ToListAsync();
        if (orders.Count >= discountEntity.NumberUses)
            throw new ArgumentException("Maximum use of this code!");

        return new GenericResponse<DiscountEntity>(discountEntity);
    }
}
