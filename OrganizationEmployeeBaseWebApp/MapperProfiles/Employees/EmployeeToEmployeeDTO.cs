using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Employees
{
    public class EmployeeToEmployeeDTO : Profile
    {
        public EmployeeToEmployeeDTO() 
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
