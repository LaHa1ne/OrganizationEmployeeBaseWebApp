using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.Models.OrganizationViewModels;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Organizations
{
    public class CreateOrganizationToOrganizationDTO : Profile
    {
        public CreateOrganizationToOrganizationDTO() 
        {
            CreateMap<CreateOrganizationViewModel, OrganizationDTO>();
        }
    }
}
