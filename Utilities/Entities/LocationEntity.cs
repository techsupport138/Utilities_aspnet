using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Utilities.Entities {
    [Table("Location")]
    public class LocationEntity {

        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public LocationEntity? Parent { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public IEnumerable<MediaEntity>? Media { get; set; }

        [Required]
        [EnumDataType(typeof(LocationType))]
        public LocationType Type { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Guid? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity? Product { get; set; }
        public Guid? AdsId { get; set; }

        [ForeignKey(nameof(AdsId))]
        public AdsEntity? Ads { get; set; }
    }
}