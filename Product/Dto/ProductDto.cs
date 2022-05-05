using Utilities_aspnet.User.Dtos;

namespace Utilities_aspnet.Product.Dto;

public class GetProductDto {
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public bool? Enabled { get; set; }
    public bool? IsForSale { get; set; }
    public bool? IsBookmarked { get; set; }
    public decimal? Price { get; set; }
    public int? VisitsCount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public UserReadDto? User { get; set; }
    public LocationReadDto? Location { get; set; }
    public IEnumerable<MediaDto>? Media { get; set; }
    public IEnumerable<IdTitleDto>? Categories { get; set; }
    public IEnumerable<IdTitleDto>? Team { get; set; }
    public IEnumerable<IdTitleDto>? Tags { get; set; }
    public IEnumerable<VoteReadDto>? Votes { get; set; }
}

public class AddUpdateProductDto {
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? SubTitle { get; set; }
    public decimal? Price { get; set; }
    public bool? IsForSale { get; set; }
    public bool? Enabled { get; set; }
    public Guid? Location { get; set; }
    public IEnumerable<Guid>? Categories { get; set; }
    public IEnumerable<Guid>? Specialties { get; set; }
    public IEnumerable<Guid>? Tags { get; set; }
    public IEnumerable<Guid>? Teams { get; set; }
    public IEnumerable<VoteFieldCreateDto>? Votes { get; set; }
}

public class AutoMapperProject : Profile {
    public AutoMapperProject() {
        CreateMap<ProductEntity, AddUpdateProductDto>().ReverseMap();
        CreateMap<ProductEntity, GetProductDto>().ReverseMap();
        CreateMap<AddUpdateProductDto, GetProductDto>().ReverseMap();
    }
}