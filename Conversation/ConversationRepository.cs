
namespace Utilities_aspnet.Conversation
{
    public class Conversation : BaseEntity
    {
        [StringLength(450)]
        [ForeignKey(nameof(FromUserNavigation))]
        public string FromUserId { get; set; } = null!;
        [StringLength(450)]
        [ForeignKey(nameof(ToUserNavigation))]
        public string ToUserId { get; set; } = null!;
        [StringLength(500)]
        public string MessageText { get; set; } = null!;

        public virtual UserEntity FromUserNavigation { get; set; } = null!;
        public virtual UserEntity ToUserNavigation { get; set; } = null!;

    }
}
