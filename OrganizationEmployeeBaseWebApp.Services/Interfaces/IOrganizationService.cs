using Microsoft.AspNetCore.Http;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using OrganizationEmployeeBaseWebApp.DataLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<BaseResponse<List<OrganizationDTO>>> GetOrganizations();
        Task<BaseResponse<OrganizationDTO>> CreateOrganization(OrganizationDTO organization);
        Task<BaseResponse<OrganizationDTO>> EditOrganization(OrganizationDTO organization);
        Task<BaseResponse<bool>> DeleteOrganization(int organizationId);
        Task<BaseResponse<List<OrganizationDTO>>> LoadOrganizationsFromCSV(IFormFile file);
        Task<BaseResponse<byte[]>> ExportOrganizations();
    }
}
