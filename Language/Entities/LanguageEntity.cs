using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Language.Entities;

[Table("Languages")]
public class LanguageEntity
{
    [Key]
    [BsonId]
    [StringLength(5)]    
    public string Symbol { get; set; }

    [StringLength(50)]
    [Column(TypeName = "NVARCHAR")]
    [Required]
    public string LanguageName { get; set; }
}
