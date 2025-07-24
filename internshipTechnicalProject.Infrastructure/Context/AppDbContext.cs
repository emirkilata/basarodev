
using internshipTechnicalProject.Domain.Point;
using internshipTechnicalProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace internshipTechnicalProject.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Point> Points { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Polygon> Polygons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Line>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Geometry).IsRequired();
            });
            
            modelBuilder.Entity<Polygon>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Geometry).IsRequired();
            });
        }
    }
}

