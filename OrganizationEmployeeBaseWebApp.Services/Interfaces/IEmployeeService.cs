using Microsoft.AspNetCore.Http;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.Responses;

namespace OrganizationEmployeeBaseWebApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<BaseResponse<List<EmployeeDTO>>> GetOrganizationEmployees(int organizationId);
        Task<BaseResponse<EmployeeDTO>> CreateEmployee(EmployeeDTO employee);
        Task<BaseResponse<EmployeeDTO>> EditEmployee(EmployeeDTO employee);
        Task<BaseResponse<bool>> DeleteEmployee(int employeeId);
        Task<BaseResponse<List<EmployeeDTO>>> LoadEmployeesFromCSV(int organizationId, IFormFile file);
        Task<BaseResponse<byte[]>> ExportOrganizationEmployees(int organizationId);
    }
}
