
namespace Utilities_aspnet.Language.Entities
{
    [Table("Languages")]
    public class LanguageEntityBase
    {

        [Key]
        //[BsonId]
        [StringLength(5)]
        public string Symbol { get; set; }
    }
}