
using Utilities_aspnet.Comment;

namespace Utilities_aspnet.Dashboard
{
    public interface IDashboardRepository
    {
        Task<GenericResponse<DashboardDto>> FilterReports(FilterDashboardDto model);
    }
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public DashboardRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<GenericResponse<DashboardDto>> FilterReports(FilterDashboardDto model)
        {
            DashboardDto dashboardDto = new DashboardDto();
            dashboardDto.CountTenders = await _context.Set<TenderEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountMagazine = await _context.Set<MagazineEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountReports = await _context.Set<ReportEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountAds = await _context.Set<AdEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountBookmarks = await _context.Set<BookmarkEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountBrands = await _context.Set<BrandEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountCategories = await _context.Set<CategoryEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountComments = await _context.Set<CommentEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountCompanies = await _context.Set<CompanyEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountDailyPrices = await _context.Set<DailyPriceEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountEvents = await _context.Set<EventEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountProjects = await _context.Set<ProjectEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountServices = await _context.Set<ServiceEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountSpecialities = await _context.Set<SpecialityEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountTags = await _context.Set<TagEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountTutorials = await _context.Set<TutorialEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();
            dashboardDto.CountUsers = await _context.Set<UserEntity>().Where(x=>x.CreatedAt>=model.StardDate && x.CreatedAt <= model.EndDate).CountAsync();


            
            return new GenericResponse<DashboardDto>(dashboardDto);
        }




    }
}
