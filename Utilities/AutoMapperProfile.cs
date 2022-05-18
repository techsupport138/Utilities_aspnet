using Utilities_aspnet.Content;
using Utilities_aspnet.IdTitle;
using Utilities_aspnet.Product;
using Utilities_aspnet.User.Dtos;

namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        // IdTitle
        CreateMap<TagEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<TagEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<CategoryEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<CategoryEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<SpecialityEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<SpecialityEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<FavoriteEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<FavoriteEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ColorEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ColorEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ContactInfoItemEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ContactInfoItemEntity, IdTitleCreateUpdateDto>().ReverseMap();

        // Product
        CreateMap<BaseProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<BaseProductEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<ProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<ProjectEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProjectEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<TutorialEntity, ProductReadDto>().ReverseMap();
        CreateMap<TutorialEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<EventEntity, ProductReadDto>().ReverseMap();
        CreateMap<EventEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<AdEntity, ProductReadDto>().ReverseMap();
        CreateMap<AdEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<CompanyEntity, ProductReadDto>().ReverseMap();
        CreateMap<CompanyEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<TenderEntity, ProductReadDto>().ReverseMap();
        CreateMap<TenderEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<ServiceEntity, ProductReadDto>().ReverseMap();
        CreateMap<ServiceEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<MagazineEntity, ProductReadDto>().ReverseMap();
        CreateMap<MagazineEntity, ProductCreateUpdateDto>().ReverseMap();
        CreateMap<MediaEntity, MediaDto>().ReverseMap();

        // Content
        CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
        CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();

        // Location
        CreateMap<LocationEntity, LocationReadDto>().ReverseMap();

        // User
        CreateMap<UserEntity, UserReadDto>()
            .ForMember(x => x.Colors, x => x.MapFrom(x => x.Colors.Select(x => x.Color.Id).ToList()));
        CreateMap<UserEntity, UserReadDto>()
            .ForMember(x => x.Favorites, x => x.MapFrom(x => x.Favorites.Select(x => x.Favorite.Id).ToList()));
        CreateMap<UserEntity, UserReadDto>().ForMember(x => x.Media, x => x.MapFrom(x => x.Media));
    }
}