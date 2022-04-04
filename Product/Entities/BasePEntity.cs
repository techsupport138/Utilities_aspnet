using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Product.Entities;

public class BasePEntity : BaseEntity {

    public bool Publish { get; set; } = false;
    public bool Enable { get; set; } = false;

    [StringLength(200)]
    public string Title { get; set; }

    [StringLength(200)]
    public string Brand { get; set; }

    [StringLength(500)]
    public string Lid { get; set; }

    [Column(TypeName = "NTEXt")]
    public string Details { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity UserEntity { get; set; }
    public string UserId { get; set; }

    

    public ICollection<MediaEntity>? Media { get; set; }

}