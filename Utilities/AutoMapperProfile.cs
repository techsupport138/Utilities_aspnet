using Utilities_aspnet.Comment;
using Utilities_aspnet.Transaction;
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
        CreateMap<ColorEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ColorEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ContactInfoItemEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ContactInfoItemEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<BrandEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<BrandEntity, IdTitleCreateUpdateDto>().ReverseMap();
        CreateMap<ReferenceEntity, IdTitleReadDto>().ReverseMap();
        CreateMap<ReferenceEntity, IdTitleCreateUpdateDto>().ReverseMap();
        
        CreateMap<ProductEntity, ProductReadDto>().ReverseMap();
        CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap()
            .ForMember(x => x.Locations, y => y.Ignore())
            .ForMember(x => x.Categories, y => y.Ignore())
            .ForMember(x => x.References, y => y.Ignore())
            .ForMember(x => x.Brands, y => y.Ignore())
            .ForMember(x => x.Specialities, y => y.Ignore())
            .ForMember(x => x.Tags, y => y.Ignore());

        CreateMap<MediaEntity, MediaDto>().ForMember(x=>x.Link, c=>c.MapFrom(v=>v.Link == null? $"{NetworkUtil.ServerAddress}/Medias/{v.FileName}" : v.Link)).ReverseMap();

        CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
        CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();
        CreateMap<ContactInformationEntity, ContactInformationReadDto>().ReverseMap();
        CreateMap<ContactInformationEntity, ContactInformationCreateUpdateDto>().ReverseMap();

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
            .ForMember(x => x.Colors, y => y.Ignore())
            .ForMember(x => x.Specialties, y => y.Ignore())
            .ForMember(x => x.Location, y => y.Ignore())
            .ForMember(x => x.Media, y => y.Ignore())
            .ForMember(x => x.ContactInformation, y => y.Ignore());
    }
}