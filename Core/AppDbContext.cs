using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.Core {
    public class AppDbContext : IdentityDbContext<UserEntity> {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        
        public virtual DbSet<UserEntity> User { get; set; } = null!;
        public virtual DbSet<MediaEntity> Media { get; set; } = null!;
        public virtual DbSet<OtpEntity> Otp { get; set; } = null!;
    }
}