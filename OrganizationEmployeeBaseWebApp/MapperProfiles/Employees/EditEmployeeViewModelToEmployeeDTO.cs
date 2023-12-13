using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.Models.EmployeeViewModels;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Employees
{
    public class EditEmployeeViewModelToEmployeeDTO : Profile
    {
        public EditEmployeeViewModelToEmployeeDTO()
        {
            CreateMap<EditEmployeeViewModel, EmployeeDTO>()
                  .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
        }
    }
}
