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
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureProject();
            modelBuilder.ConfigureProjectTask();
            modelBuilder.Seed();
        }
    }
}