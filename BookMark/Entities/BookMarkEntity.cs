using Utilities_aspnet.Product;

namespace Utilities_aspnet.BookMark.Entities {
    public class BookMarkEntity : BaseEntity {
        public UserEntity UserEntity { get; set; }
        public string UserId { get; set; }


        public AdsEntity? Ads { get; set; }
        public Guid? AdsId { get; set; }

        public ProjectEntity? Project { get; set; }
        public Guid? ProjectId { get; set; }

        public ProductEntity? Product { get; set; }
        public Guid? ProductId { get; set; }

        public TutorialEntity? Tutorial { get; set; }
        public Guid? TutorialId { get; set; }
    }
}