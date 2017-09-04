using Microsoft.EntityFrameworkCore;

namespace VacationSolution.Web.Entities
{
    public class EntityContext : DbContext
    {

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {

        }
        DbSet<User> Users { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<VacationAvailable> VacationAvailable { get; set; }
        public DbSet<VacationRequest> VacationRequest { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOne(a => a.Profile)
                        .WithOne(b => b.User)
                        .HasForeignKey<Profile>(b => b.UserID);

            modelBuilder.Entity<User>()
                        .HasMany(v => v.VacationRequest)
                        .WithOne(u => u.User);

            modelBuilder.Entity<User>()
                        .HasOne(a => a.VacationAvailable)
                        .WithOne(b => b.User)
                        .HasForeignKey<VacationAvailable>(b => b.UserID);
            modelBuilder.Entity<VacationRequest>()
                        .HasKey(v => v.RequestID);

        }
    }
}
