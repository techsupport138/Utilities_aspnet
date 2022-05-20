namespace Utilities_aspnet.Statistic.Entities; 

[Table("Visitor")]
public class VisitorEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long VisitorId { get; set; }

    [Column(TypeName = "date")]
    public DateTime Date { get; set; }

    public byte Hours { get; set; }
    public byte Minutes { get; set; }

    [Column("IP")]
    [StringLength(50)]
    public string Ip { get; set; }

    [StringLength(500)]
    public string LandingPage { get; set; }
}