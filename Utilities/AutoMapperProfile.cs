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
        CreateMap<BrandEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<BrandEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ReferenceEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ReferenceEntity, IdTitleCreateUpdateDto>().ReverseMap();

        // Product
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

        // Others
        CreateMap<MediaEntity, MediaDto>().ReverseMap();

        // Content
        CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
        CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();

        // Location
        CreateMap<LocationEntity, LocationReadDto>().ReverseMap();

        // User
        CreateMap<UserEntity, UserReadDto>().ReverseMap();

        // Form
        CreateMap<FormEntity, FormFieldDto>().ReverseMap();
        CreateMap<FormFieldEntity, FormFieldDto>().ReverseMap();
    }
}