using AutoMapper;
using Utilities_aspnet.Utilities.Dtos;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Models.Dto
{
    public class ContactInfoItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<MediaDto> Media { get; set; }
    }
    public class AutoMapperContactInfoItems : Profile
    {
        public AutoMapperContactInfoItems()
        {
            CreateMap<ContactInfoItemEntity, ContactInfoItemDto>();
        }
    }
}
