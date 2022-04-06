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

    [ForeignKey(nameof(Language))]
    public LanguageEntity LanguageEntity { get; set; } 
    [DefaultValue("fa-IR")]
    public string Language { get; set; } = "fa-IR";


    [ForeignKey(nameof(CategoryId))]
    public CategoryEntity ContentCategory { get; set; }
    public Guid CategoryId { get; set; }

    //[ForeignKey(nameof(IntroMediaId))]
    //public MediaEntity? IntroMediaEntity { get; set; }
    //public Guid? IntroMediaId { get; set; } = null;


    [StringLength(500)]
    [Column(TypeName = "NVARCHAR")]
    [Required]
    public string Title { get; set; }

    [StringLength(10)]
    public string TinyURL { get; set; }

    [Column(TypeName = "ntext")]
    [Display(Name = "خلاصه")]
    public string? Lid { get; set; } = null;

    [Column(TypeName = "ntext")]
    [Display(Name = "متن")]
    public string? Body { get; set; } = null;

    [ForeignKey(nameof(UserID))]
    public UserEntity? UserEntity { get; set; }
    public string? UserID { get; set; } = null;

    public int NumberOfLikes { get; set; } = 0;

    [StringLength(100)]
    public string? Author { get; set; } = null;

    [Required]
    [EnumDataType(typeof(ContentUseCase))]
    public ContentUseCase UseCase { get; set; }


    public ContentStatusCase Status { get; set; } = ContentStatusCase.Draft;

    public ICollection<MediaEntity>? MediaList { get; set; }

}