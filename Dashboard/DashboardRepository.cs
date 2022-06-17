using Utilities_aspnet.Comment;
using Utilities_aspnet.User;

namespace Utilities_aspnet.Dashboard {
    public interface IDashboardRepository {
        Task<GenericResponse<DashboardDto>> FilterReports(FilterDashboardDto model);
    }

    public class DashboardRepository : IDashboardRepository {
        private readonly DbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public DashboardRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<GenericResponse<DashboardDto>> FilterReports(FilterDashboardDto model) {
            DashboardDto dashboardDto = new() {
                CountProducts = await _context.Set<ProductEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountReports = await _context.Set<ReportEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountBookmarks = await _context.Set<BookmarkEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountBrands = await _context.Set<BrandEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountCategories = await _context.Set<CategoryEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountComments = await _context.Set<CommentEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountSpecialities = await _context.Set<SpecialityEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountTags = await _context.Set<TagEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync(),
                CountUsers = await _context.Set<UserEntity>()
                    .Where(x => x.CreatedAt >= model.StardDate && x.CreatedAt <= model.EndDate).CountAsync()
            };


            return new GenericResponse<DashboardDto>(dashboardDto);
        }
    }
}