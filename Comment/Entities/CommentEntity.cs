using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Comment.Entities;

public class CommentEntity : BaseEntity {

    [StringLength(200)]
    public string Title { get; set; }

    [StringLength(500)]
    [Column(TypeName = "NVARCHAR")]
    public string Note { get; set; }

    public double Point { get; set; } = 0;

    public UserEntity UserEntity { get; set; } = null!;
    public string UserId { get; set; } = null!;

    [ForeignKey("ServiceProvider")]
    public Guid? ServiceProviderId { get; set; }

    [ForeignKey("Project")]
    public Guid? ProjectId { get; set; }
}