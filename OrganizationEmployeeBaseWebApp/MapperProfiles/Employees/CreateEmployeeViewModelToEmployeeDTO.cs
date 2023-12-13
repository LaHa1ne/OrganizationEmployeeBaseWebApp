using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.Models.EmployeeViewModels;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Employees
{
    public class CreateEmployeeViewModelToEmployeeDTO : Profile
    {
        public CreateEmployeeViewModelToEmployeeDTO()
        {
            CreateMap<CreateEmployeeViewModel, EmployeeDTO>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
        }
    }
}