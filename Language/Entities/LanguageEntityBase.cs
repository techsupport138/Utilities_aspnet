
namespace Utilities_aspnet.Language.Entities
{
    [Table("Languages")]
    public class LanguageEntityBase
    {

        [Key]
        //[BsonId]
        [StringLength(5)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Symbol { get; set; }
    }
}