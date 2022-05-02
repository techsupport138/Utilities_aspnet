using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Language.Entities;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Base;

public class BaseContentEntity : BaseEntity {

    public BaseContentEntity()
    {
        MediaList=new List<MediaEntity>();
    }

    [ForeignKey(nameof(LanguageId))]
    public LanguageEntity Language { get; set; } 
    [DefaultValue("fa-IR")]
    public string LanguageId { get; set; } = "fa-IR";


    [ForeignKey(nameof(CategoryId))]
    public CategoryEntity Category { get; set; }
    public Guid CategoryId { get; set; }

    //[ForeignKey(nameof(IntroMediaId))]
    //public MediaEntity? IntroMediaEntity { get; set; }
    //public Guid? IntroMediaId { get; set; }


    [StringLength(500)]
    [Column(TypeName = "NVARCHAR")]
    [Required]
    public string Title { get; set; }

    public bool Enable { get; set; } = true;

    [StringLength(10)]
    public string TinyURL { get; set; }

    [Column(TypeName = "ntext")]
    [Display(Name = "خلاصه")]
    public string? Lid { get; set; }

    [Column(TypeName = "ntext")]
    [Display(Name = "متن")]
    public string? Body { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }
    public string? UserId { get; set; }

    public int NumberOfLikes { get; set; } = 0;

    [StringLength(100)]
    public string? Author { get; set; }

    [Required]
    [EnumDataType(typeof(ContentUseCase))]
    public ContentUseCase UseCase { get; set; }


    public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;

    public ICollection<MediaEntity>? MediaList { get; set; }

}