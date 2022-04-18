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
            Language = new List<KVMIVM>();
            Colors = new List<KVMVM>();
            Favorites = new List<KVMVM>();
            Specialties = new List<KVMVM>();
            SpecialtyCategories = new List<KVMVM>();
            GeoList = new List<KVPIVM>();
            Categories = new List<KVPCategoryVM>();
            UserRole = new List<KVMIVM>();
        }
        public List<KVMIVM> Language { get; set; }
        public List<KVMVM> Colors { get; set; }
        public List<KVMVM> Favorites { get; set; }
        public List<KVMVM> SpecialtyCategories { get; set; }
        public List<KVMVM> Specialties { get; set; }
        public List<KVMIVM> UserRole { get; set; }
        public List<KVPIVM> GeoList { get; set; }
        public List<KVPCategoryVM> Categories { get; internal set; }
        
    }
}
