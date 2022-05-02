using AutoMapper;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Product.Dto;

public class GetProjectDto {
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public string Subtitle { get; set; } 
    public string Description { get; set; } 
}

public class AddUpdateProjectDto {
    public string Title { get; set; } 
    public string Subtitle { get; set; } 
    public string Description { get; set; } 
}

public class AutoMapperProject : Profile {
    public AutoMapperProject() {
        CreateMap<ProjectEntity, AddUpdateProjectDto>().ReverseMap();
        CreateMap<ProjectEntity, GetProjectDto>().ReverseMap();
        CreateMap<AddUpdateProjectDto, GetProjectDto>().ReverseMap();
    }
}