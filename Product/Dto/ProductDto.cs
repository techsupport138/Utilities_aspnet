using AutoMapper;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.User.Dtos;

namespace Utilities_aspnet.Product.Dto;

public class GetProductDto {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public bool Publish { get; set; }
    public bool Enable { get; set; }
    public string? SubTitle { get; set; }
    public UserReadDto? User { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public decimal Price { get; set; }
}

public class AddUpdateProductDto {
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public bool Publish { get; set; }
    public bool Enable { get; set; }
    public string? SubTitle { get; set; }
    public UserReadDto? User { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public decimal Price { get; set; }
}

public class AutoMapperProject : Profile {
    public AutoMapperProject() {
        CreateMap<ProjectEntity, AddUpdateProductDto>().ReverseMap();
        CreateMap<ProjectEntity, GetProductDto>().ReverseMap();
        CreateMap<AddUpdateProductDto, GetProductDto>().ReverseMap();
    }
}