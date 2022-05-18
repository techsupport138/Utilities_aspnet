using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.IdTitle;
using Utilities_aspnet.Utilities.Dtos;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Data {
    public interface IDataRepository {
        Task<IEnumerable<ContactInfoItemDto>> GetContactInfoItem();
        Task<object> GetAllEnums();
    }

    public class DataRepository : IDataRepository {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public DataRepository(DbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ContactInfoItemDto>> GetContactInfoItem() {
            List<ContactInfoItemEntity> contactInfoItem = await _context.Set<ContactInfoItemEntity>().Include(x => x.Media).ToListAsync();

            return _mapper.Map<IEnumerable<ContactInfoItemDto>>(contactInfoItem);
        }

        public async Task<object> GetAllEnums() {
            List<IdTitleReadDto> approvalStatuses = EnumExtension.GetValues<ApprovalStatus>();
            List<IdTitleReadDto> fileTypes = EnumExtension.GetValues<FileTypes>();
            List<IdTitleReadDto> visibilityType = EnumExtension.GetValues<VisibilityType>();
            List<IdTitleReadDto> contentUseCase = EnumExtension.GetValues<ContentUseCase>();

            var result = new {
                approvalStatuses,
                fileTypes,
                visibilityType,
                contentUseCase
            };

            return result;
        }
    }
}