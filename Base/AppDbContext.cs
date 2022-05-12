// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Utilities_aspnet.Product;
// using Utilities_aspnet.Wallet.Entities;
// using ZNetCS.AspNetCore.Logging.EntityFrameworkCore;
//
// namespace Utilities_aspnet.Base;
//
// public class AppDbContext : IdentityDbContext<UserEntity> {
//     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
//
//     public DbSet<MediaEntity> Media { get; set; }
//     public DbSet<OtpEntity> Otp { get; set; }
//     public DbSet<DimDate> DimDate { get; set; }
//     public DbSet<VisitorEntity> Visitor { get; set; }
//     public DbSet<StatisticEntity> Statistic { get; set; }
//     public DbSet<UserRoleEntity> Roles { get; set; }
//     public DbSet<UserToRoleEntity> UserToRole { get; set; }
//     public DbSet<LocationEntity> Location { get; set; }
//     public DbSet<BankTransaction> BankTransaction { get; set; }
//     public DbSet<Transaction> Transaction { get; set; }
//     public DbSet<ColorEntity> Color { get; set; }
//     public DbSet<UserToColorEntity> UserToColor { get; set; }
//     public DbSet<FavoriteEntity> Favorite { get; set; }
//     public DbSet<UserToFavoriteEntity> UserToFavorit { get; set; }
//     public DbSet<SpecialtyEntity> Specialty { get; set; }
//     public DbSet<SpecialtyCategoryEntity> SpecialtyCategory { get; set; }
//     public DbSet<UserToSpecialtyEntity> UserToSpecialty { get; set; }
//     public DbSet<ShoppingListEntity> ShoppingList { get; set; }
//     public DbSet<Log> Log { get; set; }
//     public DbSet<ProductEntity> Product { get; set; }
//     public DbSet<ProjectEntity> Project { get; set; }
//     public DbSet<TutorialEntity> Tutorial { get; set; }
//     public DbSet<EventEntity> Event { get; set; }
//     public DbSet<AdEntity> Ad { get; set; }
//     public DbSet<CompanyEntity> Company { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder builder) {
//         base.OnModelCreating(builder);
//         builder.SeedColors();
//     }
// }
//
// public static class CaseSeederExtension {
//     public static void SeedColors(this ModelBuilder modelBuilder) {
//         List<ColorEntity> list = new() {
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Red", ColorHex = "c62828"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Pink", ColorHex = "ad1457"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Purple", ColorHex = "6a1b9a"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Deep Purple", ColorHex = "4527a0"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Indigo", ColorHex = "283593"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Blue", ColorHex = "1565c0"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Light Blue", ColorHex = "0277bd"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Cyan", ColorHex = "00838f"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Teal", ColorHex = "00695c"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Green", ColorHex = "2e7d32"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Light Green", ColorHex = "558b2f"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Yellow", ColorHex = "ffeb3b"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Orange", ColorHex = "ef6c00"},
//             new ColorEntity {Id = Guid.NewGuid(), Title = "Brown", ColorHex = "5d4037"},
//         };
//
//         modelBuilder.Entity<ColorEntity>().HasData(list);
//     }
// }