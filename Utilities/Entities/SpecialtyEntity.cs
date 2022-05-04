namespace Utilities_aspnet.Utilities.Entities {
    [Table("Specialties")]
    public class SpecialtyEntity : BaseEntity {
        [StringLength(100)]
        public string SpecialtyTitle { get; set; }

        public SpecialtyCategoryEntity Category { get; set; }
        public Guid CategoryId { get; set; }

        public MediaEntity Media { get; set; }
        public Guid MediaId { get; set; }
        
        public ProductEntity Product { get; set; }
        public Guid ProductId { get; set; }
    }
}