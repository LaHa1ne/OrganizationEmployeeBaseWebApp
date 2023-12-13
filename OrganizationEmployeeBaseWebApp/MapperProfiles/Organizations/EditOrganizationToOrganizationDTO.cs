using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.Models.OrganizationViewModels;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Organizations
{
    public class EditOrganizationToOrganizationDTO : Profile
    {
        public EditOrganizationToOrganizationDTO()
        {
            CreateMap<EditOrganizationViewModel, OrganizationDTO>();
        }
    }
}