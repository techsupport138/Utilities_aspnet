using Utilities_aspnet.Comment;
using Utilities_aspnet.ShoppingCart;
using Utilities_aspnet.User;

namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
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
        CreateMap<BrandEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<BrandEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ReferenceEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ReferenceEntity, IdTitleCreateUpdateDto>().ReverseMap();

        CreateMap<BaseProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<BaseProductEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<ProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<ProjectEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProjectEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<DailyPriceEntity, ProductReadDto>().ReverseMap();
        CreateMap<DailyPriceEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<TutorialEntity, ProductReadDto>().ReverseMap();
        CreateMap<TutorialEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<EventEntity, ProductReadDto>().ReverseMap();
        CreateMap<EventEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<AdEntity, ProductReadDto>().ReverseMap();
        CreateMap<AdEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<CompanyEntity, ProductReadDto>().ReverseMap();
        CreateMap<CompanyEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<TenderEntity, ProductReadDto>().ReverseMap();
        CreateMap<TenderEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<ServiceEntity, ProductReadDto>().ReverseMap();
        CreateMap<ServiceEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());
        CreateMap<MagazineEntity, ProductReadDto>().ReverseMap();
        CreateMap<MagazineEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());


        CreateMap<MediaEntity, MediaDto>().ReverseMap();
        CreateMap<MediaEntity, MediaDto>().ReverseMap();

        CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
        CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();
        CreateMap<ContactInformationEntity, ContactInformationReadDto>().ReverseMap();
        CreateMap<ContactInformationEntity, ContactInformationCreateUpdateDto>().ReverseMap();

        CreateMap<LocationEntity, LocationReadDto>().ReverseMap();
        CreateMap<UserEntity, UserReadDto>().ReverseMap();
        CreateMap<FormEntity, FormDto>().ReverseMap();
        CreateMap<FormFieldEntity, FormFieldDto>().ReverseMap();
        CreateMap<NotificationEntity, NotificationDto>().ReverseMap();
        CreateMap<CommentEntity, CommentCreateUpdateDto>().ReverseMap();
        CreateMap<CommentEntity, CommentReadDto>().ReverseMap();

        CreateMap<CreateUpdateUserDto, UserEntity>()
            .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password))
            .ForMember(x => x.Colors, y => y.Ignore())
            .ForMember(x => x.Specialties, y => y.Ignore())
            .ForMember(x => x.Favorites, y => y.Ignore())
            .ForMember(x => x.Location, y => y.Ignore())
            .ForMember(x => x.Media, y => y.Ignore())
            .ForMember(x => x.ContactInformation, y => y.Ignore());

        CreateMap<ShoppingCartEntity, ShoppingCartReadDto>()
            .ForMember(x => x.ShoppingCartItems, y => y.MapFrom(x => x.ShoppingCartItems))
            .ReverseMap();
        CreateMap<ShoppingCartItemEntity, ShoppingCartItemReadDto>().ReverseMap();
        CreateMap<ShoppingCartItemEntity, ShoppingCartItemCreateDto>().ReverseMap();
    }
}