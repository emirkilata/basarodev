
using internshipTechnicalProject.Domain.Point;
using Microsoft.EntityFrameworkCore;

namespace internshipTechnicalProject.Infrastructure
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            
        }

        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.X).IsRequired();
                entity.Property(p => p.Y).IsRequired();
            });
        }
    }
}

