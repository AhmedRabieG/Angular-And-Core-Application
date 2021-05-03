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
        public DbSet<IdentityRole> roles { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ClientLocation>  clientLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientLocation>().HasData(
                new ClientLocation() { ClientLocationID = 1, ClientLocationName = "Cairo" },
                new ClientLocation() { ClientLocationID = 2, ClientLocationName = "Alexandria" },
                new ClientLocation() { ClientLocationID = 3, ClientLocationName = "Dubai" },
                new ClientLocation() { ClientLocationID = 4, ClientLocationName = "Kuwait" },
                new ClientLocation() { ClientLocationID = 5, ClientLocationName = "London" },
                new ClientLocation() { ClientLocationID = 6, ClientLocationName = "El Reyad" }
            );

            modelBuilder.Entity<Plan>().HasData(
                new Plan() { ProjectID = 1, 
                    ProjectName = "Hospital Management System",
                    DateOfStart = Convert.ToDateTime("2017-8-1"),
                    Active = true, ClientLocationID = 2,
                    Status = "In Force", TeamSize = 14 },
                new Plan() { ProjectID = 2,
                    ProjectName = "Reporting Tool",
                    DateOfStart = Convert.ToDateTime("2018-3-16"),
                    Active = true, ClientLocationID = 1,
                    Status = "Support", TeamSize = 81 }
            );


        }
    }
}
