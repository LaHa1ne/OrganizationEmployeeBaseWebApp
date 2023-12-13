using AutoMapper;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;

namespace OrganizationEmployeeBaseWebApp.MapperProfiles.Organizations
{
    public class OrganizationToOrganizationDTO : Profile
    {
        public OrganizationToOrganizationDTO() 
        {
            CreateMap<Organization, OrganizationDTO>();
        }  
    }
}
