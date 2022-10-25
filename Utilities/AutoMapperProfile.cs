namespace Utilities_aspnet.Utilities;

public class AutoMapperProfile : Profile {
	public AutoMapperProfile() {
		CreateMap<VoteFieldEntity, VoteReadDto>()
			.ForMember(x => x.Score, c => c.MapFrom(v => v.Votes == null || !v.Votes.Any() ? 0 : v.Votes.Sum(x => x.Score) / v.Votes.Count())).ReverseMap();
	}
}