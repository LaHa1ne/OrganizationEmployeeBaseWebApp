using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs.CSV;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Employees
{
    public class EmployeeCSVToEmployee : Profile
    {
        public EmployeeCSVToEmployee() 
        {
            CreateMap<EmployeeCSV, Employee>()
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}