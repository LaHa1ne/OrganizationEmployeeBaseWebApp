using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.DbConfigurations
{
    public class OrganizationDbConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> entity)
        {
            entity.ToTable("organizations");

            entity.HasKey(o => o.OrganizationId);
            entity.Property(o => o.Name).IsRequired();
            entity.Property(o => o.INN).IsRequired();
            entity.Property(o => o.LegalAddress).IsRequired();
            entity.Property(o => o.ActualAddress).IsRequired();

            entity.HasData(
                new Organization { OrganizationId = 1, Name = "Организация 1", INN = "1234567890", LegalAddress = "Юридический адрес 1", ActualAddress = "Фактический адрес 1" },
                new Organization { OrganizationId = 2, Name = "Организация 2", INN = "0987654321", LegalAddress = "Юридический адрес 2", ActualAddress = "Фактический адрес 2" },
                new Organization { OrganizationId = 3, Name = "Организация 3", INN = "1234554321", LegalAddress = "Юридический адрес 3", ActualAddress = "Фактический адрес 3" }
                );
        }
    }
}