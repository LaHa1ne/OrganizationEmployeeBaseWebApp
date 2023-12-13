using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs.CSV;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;

namespace OrganizationEmployeeBaseWebApp.DataLayer.Extensions
{
    public static class OrganizationExtension
    {
        public static void UpdateInfo(this Organization organization, OrganizationDTO organizationDTO)
        {
            organization.Name = organizationDTO.Name;
            organization.INN = organizationDTO.INN;
            organization.LegalAddress = organizationDTO.LegalAddress;
            organization.ActualAddress = organizationDTO.ActualAddress;
        }

        public static IEnumerable<OrganizationWithEmployeeCSV> ToOrganizationsCSV(this IEnumerable<Organization> organizations)
        {
            return organizations
                .SelectMany(org => org.Employees.DefaultIfEmpty(), (org, emp) => new OrganizationWithEmployeeCSV
                {
                    OrganizationId = org.OrganizationId,
                    Name = org.Name,
                    INN = org.INN,
                    LegalAddress = org.LegalAddress,
                    ActualAddress = org.ActualAddress,
                    EmployeeId = emp?.EmployeeId,
                    FirstName = emp?.FirstName,
                    LastName = emp?.LastName,
                    Patronymic = emp?.Patronymic,
                    DateOfBirth = emp?.DateOfBirth,
                    PassportSeries = emp?.PassportSeries,
                    PassportNumber = emp?.PassportNumber,
                });
        }

        public static IEnumerable<Organization> ToOrganizations(this IEnumerable<OrganizationWithEmployeeCSV> organizationCSVs)
        {
            return organizationCSVs
                .GroupBy(orgCSV => orgCSV.OrganizationId)
                .Select(group => new Organization
                {
                    Name = group.First().Name,
                    INN = group.First().INN,
                    LegalAddress = group.First().LegalAddress,
                    ActualAddress = group.First().ActualAddress,
                    Employees = group
                        .Where(emp => emp.EmployeeId.HasValue)
                        .Select(emp => new Employee
                        {
                            FirstName = emp.FirstName!,
                            LastName = emp.LastName!,
                            Patronymic = emp.Patronymic!,
                            DateOfBirth = emp.DateOfBirth!.Value,
                            PassportSeries = emp.PassportSeries!,
                            PassportNumber = emp.PassportNumber!,
                            OrganizationId = emp.OrganizationId,
                        })
                        .ToList()
                }).ToList();
        }
    }
}
