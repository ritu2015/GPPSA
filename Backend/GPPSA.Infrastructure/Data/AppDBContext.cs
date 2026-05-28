using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Domain;
using GPPSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPPSA.Infrastructure.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {}
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee-Department relationship
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e=>e.EmployeeCode).IsUnique(); // Ensure EmployeeCode is unique
                entity.Property(e=> e.Salary).HasColumnType("decimal(18,2)"); // Configure Salary precision
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict); // Optional: specify delete behavior
            });
            //Apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }
       
    }
}