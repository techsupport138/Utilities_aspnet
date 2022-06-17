using Utilities_aspnet.Category;
using Utilities_aspnet.Comment;
using Utilities_aspnet.Transaction;
using Utilities_aspnet.User;

namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        CreateMap<CategoryEntity, CategoryReadDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryCreateUpdateDto>().ReverseMap();

        CreateMap<ProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore());

        CreateMap<MediaEntity, MediaDto>().ForMember(x => x.Link,
            c => c.MapFrom(v => v.Link == null ? $"{NetworkUtil.ServerAddress}/Medias/{v.FileName}" : v.Link)).ReverseMap();

        CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
        CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();

        CreateMap<LocationEntity, LocationReadDto>().ReverseMap();
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