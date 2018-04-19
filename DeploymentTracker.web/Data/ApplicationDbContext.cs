using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeploymentTracker.web.Models;

namespace DeploymentTracker.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EnvironmentEntity> Environents{ get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ChecklistTemplateEntity> ChecklistTemplates { get; set; }
        public DbSet<ChecklistEntity> Checklists { get; set; }

        public DbSet<ChecklistTemplateTaskEntity> ChecklistTemplateTasks { get; set; }
        public DbSet<ChecklistTaskEntity> ChecklistTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<EnvironmentEntity>().ToTable("EnvironmentEntity");
            builder.Entity<TaskEntity>().ToTable("TaskEntity");
            builder.Entity<ChecklistTemplateEntity>().ToTable("ChecklistTemplateEntity");
            builder.Entity<ChecklistEntity>().ToTable("ChecklistEntity");

            builder.Entity<ChecklistTemplateTaskEntity>().ToTable("ChecklistTemplateTaskEntity");
            builder.Entity<ChecklistTaskEntity>().ToTable("ChecklistTaskEntity");
        }
    }
}
