using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTaskManager.Models
{
    public class TaskManagerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, String>
    {
        public TaskManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Plan> plans { get; set; }
        public DbSet<Test> tests { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
