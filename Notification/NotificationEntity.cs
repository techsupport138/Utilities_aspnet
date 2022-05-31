
namespace Utilities_aspnet.Notification
{
    public class NotificationEntity : BaseEntity
    {
        [StringLength(450)]
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;
        [StringLength(500)]
        public string? Message { get; set; }
        [StringLength(450)]
        public string? Link { get; set; }
        public bool Visited { get; set; }
        public NotificationUseCase UseCase { get; set; }
        public virtual UserEntity? User { get; set; }
        public ICollection<MediaEntity>? Media { get; set; }
    }


    public class NotificationDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        public bool Visited { get; set; }
        public NotificationUseCase UseCase { get; set; }
        public List<MediaDto>? Media { get; set; }
    }

    public class CreateNotificationDto
    {
        public string Title { get; set; } = null!;
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public string? Link { get; set; }
        public string? Media { get; set; }
        public NotificationUseCase UseCase { get; set; }
    }


    public enum NotificationUseCase
    {
        Message = 100,
        News = 101,
        Notification = 102
    }
}
