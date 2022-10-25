namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile {
	public AutoMapperProfile() {
		CreateMap<ProductEntity, ProductReadDto>()
			.ForMember(x => x.Score, c => c.MapFrom(v => v.Votes == null || !v.Votes.Any() ? 0 : v.Votes.Sum(x => x.Score) / v.Votes.Count()))
			.ForMember(x => x.CommentsCount, c => c.MapFrom(v => v.Comments == null ? 0 : v.Comments.Count()))
			.ForMember(x => x.IsBookmarked, c => c.MapFrom(v => v.Bookmarks != null && v.Bookmarks.Any() && v.Bookmarks.Any(x => x.UserId == Server.UserId)));
		CreateMap<VoteFieldEntity, VoteReadDto>()
			.ForMember(x => x.Score, c => c.MapFrom(v => v.Votes == null || !v.Votes.Any() ? 0 : v.Votes.Sum(x => x.Score) / v.Votes.Count())).ReverseMap();
		CreateMap<CommentEntity, CommentReadDto>()
			.ForMember(x => x.IsLiked,
			           c => c.MapFrom(v => v.LikeComments != null && v.LikeComments.Any() && v.LikeComments.Any(x => x.UserId == Server.UserId))).ReverseMap();
	}
}