// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
// using Utilities_aspnet.Comment.Entities;
// using Utilities_aspnet.User.Entities;
// using Utilities_aspnet.Utilities.Entities;
//
// namespace Utilities_aspnet.Docs; 
//
// public class AppDbContext : IdentityDbContext<UserEntity> {
//     
//     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
//
//     public DbSet<ContactInformationEntity> ContactInformation { get; set; }
//     public DbSet<ContactInfoItemEntity> ContactInfoItem { get; set; }
//     public DbSet<CommentEntity> Comment { get; set; }
//     public DbSet<MediaEntity> Media { get; set; }
//     public DbSet<ContentEntity> Content { get; set; }
// }