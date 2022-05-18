using Utilities_aspnet.IdTitle;

namespace Utilities_aspnet.Utilities.Dtos
{
    public class EnumDto
    {
        public EnumDto()
        {
            Language = new List<IdTitleReadDto>();
            Colors = new List<IdTitleReadDto>();
            Favorites = new List<IdTitleReadDto>();
            Specialties = new List<IdTitleReadDto>();
            SpecialtyCategories = new List<IdTitleReadDto>();
            GeoList = new List<KVPIVM>();
            Categories = new List<KVPCategoryVM>();
            UserRole = new List<IdTitleReadDto>();
        }
        public List<IdTitleReadDto> Language { get; set; }
        public List<IdTitleReadDto> Colors { get; set; }
        public List<IdTitleReadDto> Favorites { get; set; }
        public List<IdTitleReadDto> SpecialtyCategories { get; set; }
        public List<IdTitleReadDto> Specialties { get; set; }
        public List<IdTitleReadDto> UserRole { get; set; }
        public List<KVPIVM> GeoList { get; set; }
        public List<KVPCategoryVM> Categories { get; internal set; }
        
    }
}
