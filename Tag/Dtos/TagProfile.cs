using AutoMapper;
using Utilities_aspnet.Tag.Entities;

namespace Utilities_aspnet.Tag.Dtos; 

public class TagProfile : Profile {
    public TagProfile() {
        CreateMap<TagEntity, CreateTagDto>().ReverseMap();
        CreateMap<TagEntity, UpdateTagDto>().ReverseMap();
        CreateMap<TagEntity, GetTagDto>().ReverseMap();
    }
}