namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoryEntity, CategoryReadDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryCreateUpdateDto>().ReverseMap();

        CreateMap<ProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore());

        CreateMap<MediaEntity, MediaDto>().ForMember(x => x.Link,
                                                     c => c.MapFrom(
                                                         v => v.Link == null
                                                             ? $"{Server.ServerAddress}/Medias/{v.FileName}"
                                                             : v.Link)).ReverseMap();

        CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
        CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();

        CreateMap<TeamEntity, TeamReadDto>().ReverseMap();

        CreateMap<LocationEntity, LocationReadDto>()
            .ForMember(x => x.I,c => c.MapFrom(v => v.Id))
            .ForMember(x => x.T,c => c.MapFrom(v => v.Title))
            .ForMember(x => x.lat,c => c.MapFrom(v => v.Latitude))
            .ForMember(x => x.lon,c => c.MapFrom(v => v.Longitude))
            .ForMember(x => x.Ch,c => c.MapFrom(v => v.Children))
            .ReverseMap();


        CreateMap<UserEntity, UserReadDto>().ReverseMap();
        CreateMap<FormEntity, FormDto>().ReverseMap();
        CreateMap<FormEntity, FormFieldDto>().ReverseMap();
        CreateMap<FormFieldEntity, FormFieldDto>().ReverseMap();
        CreateMap<NotificationEntity, NotificationDto>().ReverseMap();
        CreateMap<CommentEntity, CommentCreateUpdateDto>().ReverseMap();
        CreateMap<CommentEntity, CommentReadDto>().ReverseMap();

        CreateMap<TransactionEntity, TransactionReadDto>().ReverseMap();
        CreateMap<TransactionEntity, TransactionCreateDto>().ReverseMap();

        CreateMap<UserCreateUpdateDto, UserEntity>()
            .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password))
            .ForMember(x => x.Location, y => y.Ignore())
            .ForMember(x => x.Media, y => y.Ignore());
    }
}