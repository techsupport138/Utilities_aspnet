namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile {
	public AutoMapperProfile() {
		CreateMap<CategoryEntity, CategoryReadDto>().ReverseMap();
		CreateMap<CategoryEntity, CategoryCreateUpdateDto>().ReverseMap();

		CreateMap<OrderEntity, OrderReadDto>().ReverseMap();

		CreateMap<ProductEntity, ProductReadDto>()
			.ForMember(x => x.Score,
			           c => c.MapFrom(v => (v.Votes == null || v.Votes.Count() < 1)
				                          ? 0
				                          : (v.Votes.Sum(x => x.Score) / v.Votes.Count())))
			.ForMember(x => x.CommentsCount,
			           c => c.MapFrom(v => (v.Comments == null)
				                          ? 0
				                          : v.Comments.Count()))
			.ForMember(x => x.IsBookmarked,
			           c => c.MapFrom(v => (v.Bookmarks == null || v.Bookmarks.Count() < 1)
				                          ? false
				                          : v.Bookmarks.Any(x => x.UserId == Server.UserId)))
			.ForMember(x => x.MyVotes,
			           c => c.MapFrom(v => v.VoteFields != null
				                          ? v.VoteFields.Where(c => c.Votes != null
					                                               ? c.Votes.Any(
						                                               b => b.UserId == Server.UserId && b.ProductId == v.Id)
					                                               : false)
				                          : null))
			.ReverseMap();
		CreateMap<ProductEntity, ProductCreateUpdateDto>().ReverseMap()
			.ForMember(x => x.Locations, y => y.Ignore())
			.ForMember(x => x.Categories, y => y.Ignore())
			.ForMember(x => x.Teams, y => y.Ignore());

		CreateMap<MediaEntity, MediaDto>().ForMember(x => x.Link,
		                                             c => c.MapFrom(
			                                             v => v.Link == null
				                                             ? $"{Server.ServerAddress}/Medias/{v.FileName}"
				                                             : v.Link)).ReverseMap();

		CreateMap<ContentEntity, ContentReadDto>().ReverseMap();
		CreateMap<ContentEntity, ContentCreateUpdateDto>().ReverseMap();
		
		CreateMap<LocationEntity, LocationReadDto>()
			.ForMember(x => x.I, c => c.MapFrom(v => v.Id))
			.ForMember(x => x.N, c => c.MapFrom(v => v.Title))
			.ForMember(x => x.Lat, c => c.MapFrom(v => v.Latitude))
			.ForMember(x => x.Lon, c => c.MapFrom(v => v.Longitude))
			.ForMember(x => x.Ch, c => c.MapFrom(v => v.Children))
			.ReverseMap();

		CreateMap<VoteFieldEntity, VoteReadDto>()
			.ForMember(x => x.Score,
			           c => c.MapFrom(v => (v.Votes == null || v.Votes.Count() < 1)
				                          ? 0
				                          : (v.Votes.Sum(x => x.Score) / v.Votes.Count())))
			.ReverseMap();

		CreateMap<VoteFieldEntity, MyVoteReadDto>()
			.ForMember(x => x.Score,
			           c => c.MapFrom(v => (v.Votes == null || v.Votes.Count() < 1)
				                          ? 0
				                          : (v.Votes.FirstOrDefault(x => x.UserId == Server.UserId) != null
					                          ? v.Votes.FirstOrDefault(x => x.UserId == Server.UserId).Score ?? 0
					                          : 0)))
			.ReverseMap();

		CreateMap<UserEntity, UserReadDto>().ReverseMap();
		CreateMap<FormEntity, FormDto>().ReverseMap();
		CreateMap<FormEntity, FormFieldDto>().ReverseMap();
		CreateMap<FormFieldEntity, FormFieldDto>().ReverseMap();
		CreateMap<NotificationEntity, NotificationDto>().ReverseMap();
		CreateMap<CommentEntity, CommentCreateUpdateDto>().ReverseMap();
		CreateMap<CommentEntity, CommentReadDto>().ForMember(x => x.IsLiked,
					   c => c.MapFrom(v => (v.LikeComments == null || v.LikeComments.Count() < 1)
										  ? false
										  : v.LikeComments.Any(x => x.UserId == Server.UserId)))
			.ReverseMap();

		CreateMap<TransactionEntity, TransactionReadDto>().ReverseMap();
		CreateMap<TransactionEntity, TransactionCreateDto>().ReverseMap();

		CreateMap<ReportEntity, ReportReadDto>().ReverseMap();

		//CreateMap<BookmarkFolderEntity, BookmarkFolderReadDto>().ReverseMap();

		CreateMap<BookmarkEntity, BookmarkReadDto>().ReverseMap();

		CreateMap<UserEntity, UserMinimalReadDto>().ReverseMap();

		CreateMap<TopProductEntity, TopProductReadDto>().ReverseMap();

		CreateMap<TeamEntity, TeamReadDto>().ReverseMap();

		CreateMap<UserCreateUpdateDto, UserEntity>()
			.ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password))
			.ForMember(x => x.Location, y => y.Ignore())
			.ForMember(x => x.Media, y => y.Ignore());
	}
}