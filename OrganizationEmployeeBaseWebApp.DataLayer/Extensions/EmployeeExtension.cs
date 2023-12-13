using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;

namespace OrganizationEmployeeBaseWebApp.DataLayer.Extensions
{
    public static class EmployeeExtension
    {
        public static void UpdateInfo(this Employee employee, EmployeeDTO employeeDTO)
        {
            employee.FirstName = employeeDTO.FirstName;
            employee.LastName = employeeDTO.LastName;
            employee.Patronymic = employeeDTO.Patronymic;
            employee.DateOfBirth = employeeDTO.DateOfBirth;
            employee.PassportSeries = employeeDTO.PassportSeries;
            employee.PassportNumber = employeeDTO.PassportNumber;
            employee.OrganizationId = employeeDTO.OrganizationId;
        }
    }
}
