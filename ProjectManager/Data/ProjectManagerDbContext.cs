using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Models;

namespace ProjectManager.Data
{
    public class ProjectManagerDbContext : DbContext
    {
        public ProjectManagerDbContext(DbContextOptions<ProjectManagerDbContext> options) : base(options) 
        { 
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(30);

            modelBuilder.Entity<Project>()
                .Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            // analogiczny sposób zapisu:
            //modelBuilder.Entity<Project>(entity =>
            //{
            //    entity.Property(p => p.Name)
            //        .IsRequired(true)
            //        .HasMaxLength(30);
            //    entity.Property(p => p.Description)
            //        .HasMaxLength(500)
            //        .IsRequired(false);
            //    entity.Property(p => p.CreatedDate)
            //        .HasDefaultValueSql("GETDATE()");
            //});
        }
    }
}
