using medic_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
