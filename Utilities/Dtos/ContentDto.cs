using AutoMapper;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Dtos
{
    public class ContentDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }

        public string? SubTitle { get; set; }

        public string? Description { get; set; }
        public string? Link { get; set; }

        public List<MediaDto>? Media { get; set; }

        public List<ContactInformationDto>? ContactInformation { get; set; }
        public ContentUseCase UseCase { get; set; }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContentEntity, ContentDto>();
        }
    }
}