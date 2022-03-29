using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Utilities_aspnet.Utilities.Entities;


public class BaseEntity
{
    [Key]
    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; } = Guid.NewGuid();
    [Required]
    public DateTime CreatedAt { get; } = DateTime.Now;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }
}