using AutoMapper;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Product.Dto;

public class GetProjectDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Description { get; set; } = null!;
}
public class AddUpdateProjectDto
{
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class UpdateProjectDto
{
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class AutoMapperProject : Profile
{
    public AutoMapperProject()
    {
        CreateMap<ProjectEntity, AddUpdateProjectDto>().ReverseMap();
        CreateMap<ProjectEntity, UpdateProjectDto>().ReverseMap();
        CreateMap<ProjectEntity, GetProjectDto>().ReverseMap();
    }
}