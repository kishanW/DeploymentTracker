﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeploymentTracker.web.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace DeploymentTracker.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _context;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor context)
            : base(options)
        {
            _context = context;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries()
                                        .Where(x => x.Entity is BaseEntity 
                                                    && (x.State == EntityState.Added || x.State == EntityState.Modified)
                                                    );

            var currentUsername = !string.IsNullOrEmpty(_context?.HttpContext?.User?.Identity?.Name)
                                      ? _context.HttpContext.User.Identity.Name
                                      : "Anonymous";

            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).LastModifiedOn = DateTime.Now;
                ((BaseEntity)entity.Entity).LastModifiedBy = currentUsername;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedBy= currentUsername;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
