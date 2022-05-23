using LinkShortener.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Dal
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ShortedLinkEntity> ShortedLinks { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var initialUser = new UserEntity
            {
                Id = 1,
                Login = "initialUser",
                Password = "pass"
            };

            modelBuilder.Entity<UserEntity>()
                .HasData(initialUser);

            modelBuilder.Entity<UserEntity>()
                .OwnsOne(u => u.Balance)
                .HasData(new BalanceEntity
                {
                    OwnerId = 1,
                    Balance = 1000
                });
        }
    }
}
