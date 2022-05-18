namespace Utilities_aspnet.Statistic.Entities
{
    [Table("Statistic")]
    public class StatisticEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StatisticId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public byte Hours { get; set; }
        public byte Minutes { get; set; }
        [Required]
        [StringLength(50)]
        public string Controller { get; set; }
        [Required]
        [StringLength(50)]
        public string Action { get; set; }
        [StringLength(500)]
        public string Url { get; set; }
        [StringLength(500)]
        public string Referrer { get; set; }
        [Column("IP")]
        [StringLength(50)]
        public string Ip { get; set; }
        [StringLength(500)]
        public string Agent { get; set; }
        [StringLength(50)]
        public string UserAgentFamily { get; set; }
        [Column("OSFamily")]
        [StringLength(50)]
        public string Osfamily { get; set; }
        [StringLength(50)]
        public string DeviceFamily { get; set; }
    }
}
