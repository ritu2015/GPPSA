using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPPSA.Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.DepartmentName).IsRequired().HasMaxLength(100);
            builder.HasData(
                new Department
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DepartmentName = "Human Resources"
                },
                new Department
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    DepartmentName = "Finance"
                },
                new Department
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    DepartmentName = "IT"
                },
                new Department
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    DepartmentName = "Operations"
                }
            );
        }
    }
}