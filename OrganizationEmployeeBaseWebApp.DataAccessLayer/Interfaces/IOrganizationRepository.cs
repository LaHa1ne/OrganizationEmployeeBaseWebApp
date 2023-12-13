using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.Interfaces
{
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Task<Organization?> GetOrganizationByOrganizationId(int organizationId);
        Task<List<Organization>> GetOrganizations();
        Task<List<Organization>> GetOrganizationsWithEmployees();
    }
}
