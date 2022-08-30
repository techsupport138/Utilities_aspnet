namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile {
	public AutoMapperProfile() {
		CreateMap<CategoryEntity, CategoryReadDto>().ReverseMap();

		CreateMap<OrderEntity, OrderReadDto>().ReverseMap();

		CreateMap<ProductEntity, ProductReadDto>()
			.ForMember(x => x.Score, c => c.MapFrom(v => (v.Votes == null || !v.Votes.Any()) ? 0 : (v.Votes.Sum(x => x.Score) / v.Votes.Count())))
			.ForMember(x => x.CommentsCount, c => c.MapFrom(v => (v.Comments == null) ? 0 : v.Comments.Count()))
			.ForMember(x => x.IsBookmarked, c => c.MapFrom(v => (v.Bookmarks != null && v.Bookmarks.Any()) && v.Bookmarks.Any(x => x.UserId == Server.UserId)));
		CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap().ForMember(x => x.Categories, y => y.Ignore()).ForMember(x => x.Teams, y => y.Ignore());

		CreateMap<VoteFieldEntity, VoteReadDto>()
			.ForMember(x => x.Score, c => c.MapFrom(v => (v.Votes == null || !v.Votes.Any()) ? 0 : v.Votes.Sum(x => x.Score) / v.Votes.Count())).ReverseMap();

		CreateMap<UserEntity, UserReadDto>().ReverseMap();
		CreateMap<FormEntity, FormDto>().ReverseMap();
		CreateMap<FormEntity, FormFieldDto>().ReverseMap();
		CreateMap<FormFieldEntity, FormFieldDto>().ReverseMap();
		CreateMap<NotificationEntity, NotificationDto>().ReverseMap();
		CreateMap<CommentEntity, CommentCreateUpdateDto>().ReverseMap();
		CreateMap<CommentEntity, CommentReadDto>().ForMember(x => x.IsLiked,
		                                                     c => c.MapFrom(v => (v.LikeComments != null && v.LikeComments.Any()) &&
		                                                                         v.LikeComments.Any(x => x.UserId == Server.UserId))).ReverseMap();

		CreateMap<TransactionEntity, TransactionReadDto>().ReverseMap();
		CreateMap<TransactionEntity, TransactionCreateDto>().ReverseMap();
		CreateMap<BookmarkEntity, BookmarkReadDto>().ReverseMap();
		CreateMap<UserEntity, UserMinimalReadDto>().ReverseMap();
		CreateMap<TopProductEntity, TopProductReadDto>().ReverseMap();
		CreateMap<TeamEntity, TeamReadDto>().ReverseMap();

		CreateMap<UserCreateUpdateDto, UserEntity>().ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password)).ForMember(x => x.Media, y => y.Ignore());

		CreateMap<OrderDetailEntity, OrderDetailReadDto>().ReverseMap();
	}
}