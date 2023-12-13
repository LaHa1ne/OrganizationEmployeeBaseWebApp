using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.DbConfigurations
{
    public class EmployeeDbConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity) 
        {
            entity.ToTable("employees");

            entity.HasKey(e => e.EmployeeId);
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Patronymic).IsRequired();
            entity.Property(e => e.DateOfBirth).IsRequired();
            entity.Property(e => e.PassportSeries).IsRequired();
            entity.Property(e => e.PassportNumber).IsRequired();

            entity.HasOne(e => e.Organization)
                .WithMany(o => o.Employees)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
             new Employee { EmployeeId = 1, FirstName = "Ксения", LastName = "Мельникова", Patronymic = "Васильевна", DateOfBirth = new DateOnly(2003,7,8), PassportSeries = "12345678", PassportNumber = "123456", OrganizationId = 1 },
             new Employee { EmployeeId = 2, FirstName = "Максим", LastName = "Пименов", Patronymic = "Евгеньевич", DateOfBirth = new DateOnly(1962, 12, 11), PassportSeries = "42156478", PassportNumber = "164352", OrganizationId = 2 },
             new Employee { EmployeeId = 3, FirstName = "Елизавета", LastName = "Миронова", Patronymic = "Елизавета", DateOfBirth = new DateOnly(1994, 8, 26), PassportSeries = "86541235", PassportNumber = "956431", OrganizationId = 2 },
             new Employee { EmployeeId = 4, FirstName = "Валерий", LastName = "Васильев", Patronymic = "Валентинович", DateOfBirth = new DateOnly(1986, 3, 2), PassportSeries = "84569721", PassportNumber = "154356", OrganizationId = 2 },
             new Employee { EmployeeId = 5, FirstName = "София", LastName = "Иванова", Patronymic = "Ивановна", DateOfBirth = new DateOnly(1975, 11, 1), PassportSeries = "13945125", PassportNumber = "864352", OrganizationId = 1 },
             new Employee { EmployeeId = 6, FirstName = "Игорь", LastName = "Девин", Patronymic = "Владимирович", DateOfBirth = new DateOnly(1986, 1, 30), PassportSeries = "63541235", PassportNumber = "124553", OrganizationId = 2 }
             );
        }
    }
}