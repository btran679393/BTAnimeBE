using Microsoft.EntityFrameworkCore;
using BTAnime.Models;

namespace BTAnime.DbContext
{
    public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<AnimeModel> Animes { get; set; }
        public DbSet<SeasonsModel> Seasons { get; set; }
        public DbSet<EpisodesModel> Episodes { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔒 Enforce one favorite per anime per user
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.AnimeID })
                .IsUnique();

            // 👤 Favorite → User relationship
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📺 Favorite → Anime relationship
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Anime)
                .WithMany(a => a.Favorites)
                .HasForeignKey(f => f.AnimeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
