using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<AccessRequest> AccessRequests { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<AccessRequest>()
              .HasOne(r => r.Decision)
              .WithOne(d => d.AccessRequest)
              .HasForeignKey<Decision>(d => d.AccessRequestId);
        }
    }
}
