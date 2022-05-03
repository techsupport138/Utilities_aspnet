using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Dtos
{
    public class EnumDto
    {
        public EnumDto()
        {
            Language = new List<IdTitleDto>();
            Colors = new List<IdTitleDto>();
            Favorites = new List<IdTitleDto>();
            Specialties = new List<IdTitleDto>();
            SpecialtyCategories = new List<IdTitleDto>();
            GeoList = new List<KVPIVM>();
            Categories = new List<KVPCategoryVM>();
            UserRole = new List<IdTitleDto>();
        }
        public List<IdTitleDto> Language { get; set; }
        public List<IdTitleDto> Colors { get; set; }
        public List<IdTitleDto> Favorites { get; set; }
        public List<IdTitleDto> SpecialtyCategories { get; set; }
        public List<IdTitleDto> Specialties { get; set; }
        public List<IdTitleDto> UserRole { get; set; }
        public List<KVPIVM> GeoList { get; set; }
        public List<KVPCategoryVM> Categories { get; internal set; }
        
    }
}
