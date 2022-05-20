namespace Utilities_aspnet.Base.Data; 

public interface ICategoryRepository : IBaseRepository {
    List<KVPCategoryVM> Get(CategoryFilter filter);
    Task<GetCategoryDto> GetById(Guid id);
    Task<GenericResponse> NewCategory(NewCategoryDto newCategory);
    Task<GenericResponse> UpdateCategory(NewCategoryDto newCategory);
    Task<GenericResponse> DeleteCategory(Guid id);
}

public class CategoryRepository : BaseRepository, ICategoryRepository {
    private readonly IUploadRepository _UploadRepository;

    public CategoryRepository(DbContext context, IMapper mapper, IUploadRepository uploadRepository)
        : base(context, mapper) {
        _UploadRepository = uploadRepository;
    }

    // todo hello
    public List<KVPCategoryVM> Get(CategoryFilter filter) {
        List<KVPCategoryVM> content = _context.Set<CategoryEntity>().Include(x => x.Media)
            .Include(x => x.Parent)
            .Where(x => !filter.OnlyParent || x.ParentId == null && filter.OnlyParent)
            .Select(w => new KVPCategoryVM {
                // Key = w.Id,
                // // Image = w.Media.FileName,
                // Value = w.Title,
                // CategoryFor = w.UseCase,
                // ParentId = w.ParentId,
                // Childs = w.Parent.Select(x => new KVPCategoryVM()
                // {
                //     Key = x.CategoryId,
                //     Image = x.Media.FileName,
                //     Value = x.Title,
                //     CategoryFor = x.CategoryFor,
                //     ParentId = x.ParentId,
                //     ParentTitle = x.Parent.Title
                // }).ToList()
            }).ToList();
        return content;
    }

    public async Task<GenericResponse> NewCategory(NewCategoryDto newCategory) {
        GenericResponse res = null;

        CategoryEntity? cat = new() {
            UseCase = newCategory.CategoryFor,
            ParentId = newCategory.ParentId,
            Title = newCategory.Title,
            Id = Guid.NewGuid()
        };

        if (newCategory.File != null) {
            List<IFormFile> f = new() {newCategory.File};
            res = await _UploadRepository.UploadMedia(new UploadDto {
                Files = f,
                UserId = null
            });

            // cat.MediaId = res.Ids[0];
        }

        await _context.Set<CategoryEntity>().AddAsync(cat);
        await _context.SaveChangesAsync();
        return new GenericResponse(UtilitiesStatusCodes.Success, $"Cat {cat.Title} Created!", id: cat.Id);
    }

    public async Task<GenericResponse> DeleteCategory(Guid id) {
        CategoryEntity? cat = _context.Set<CategoryEntity>()
            // .Include(x => x.InverseParent)
            .Where(x => x.Id == id).First();
        // if (cat.MediaId != null)
        // await _UploadRepository.DeleteMedia(cat.MediaId.Value);
        // if (cat.InverseParent.Count != 0)
        // {
        // return new GenericResponse(UtilitiesStatusCodes.Unhandled, "Has Any Child");
        // }
        _context.Set<CategoryEntity>().Remove(cat);
        await _context.SaveChangesAsync();
        return new GenericResponse(UtilitiesStatusCodes.Success, $"Category {cat.Title} delete Success", id: cat.Id);
    }

    public async Task<GetCategoryDto> GetById(Guid id) {
        CategoryEntity cat = await _context.Set<CategoryEntity>()
            .Include(x => x.Media)
            // .Include(x => x.InverseParent)
            .Include(x => x.Parent)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<GetCategoryDto>(cat);
    }

    public async Task<GenericResponse> UpdateCategory(NewCategoryDto category) {
        CategoryEntity? cat = _context.Set<CategoryEntity>()
            .Where(x => x.Id == category.CategoryId).First();
        if (category.File != null) {
            List<IFormFile> f = new() {category.File};
            GenericResponse? res = await _UploadRepository.UploadMedia(new UploadDto {
                Files = f,
                UserId = null
            });
            // cat.MediaId = res.Ids[0];
        }


        cat.Title = category.Title;
        cat.ParentId = category.ParentId;
        _context.Set<CategoryEntity>().Update(cat);
        await _context.SaveChangesAsync();
        return new GenericResponse(UtilitiesStatusCodes.Success, $"Category {cat.Title} update Success", id: cat.Id);
    }
}