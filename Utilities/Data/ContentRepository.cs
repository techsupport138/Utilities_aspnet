using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.Utilities.Dtos;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Data
{
    public interface IContentRepository
    {
        Task<IEnumerable<ContentDto>> Get();
    }

    public class ContentRepository : IContentRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        public ContentRepository(DbContext context, IMapper mapper) { _context = context; _mapper = mapper; }

        public async Task<IEnumerable<ContentDto>> Get()
        {
            var content = await _context.Set<ContentEntity>().Where(x => x.UseCase != ContentUseCase.HomeSlider || (x.UseCase == ContentUseCase.HomeSlider && x.ApprovalStatus == ApprovalStatus.Approved)).Include(x => x.Media).Include(x => x.ContactInformation).ThenInclude(x => x.ContactInfoItem).ThenInclude(c => c.Media)
                .ToListAsync();
            IEnumerable<ContentDto> contentDto = _mapper.Map<IEnumerable<ContentDto>>(content);
            return contentDto;
        }
    }
}
