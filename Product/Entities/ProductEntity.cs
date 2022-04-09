using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.DailyPrice.Entities;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Base;

namespace Utilities_aspnet.Product.Entities;

public class ProductEntity : BasePEntity
{
    [ForeignKey(nameof(CategoryId))]
    public CategoryEntity ProductCategory { get; set; }
    public Guid CategoryId { get; set; }



    [ForeignKey(nameof(UserId))]
    public UserEntity? UserEntity { get; set; }
    public string? UserId { get; set; } = null;

}