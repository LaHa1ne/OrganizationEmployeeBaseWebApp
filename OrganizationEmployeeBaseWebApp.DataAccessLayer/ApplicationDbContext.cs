using Microsoft.EntityFrameworkCore;
using OrganizationEmployeeBaseWebApp.DataAccessLayer.DbConfigurations;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.Migrate();
        }

        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationDbConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeDbConfiguration());
        }

    }
}
