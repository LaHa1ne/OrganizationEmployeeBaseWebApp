using OrganizationEmployeeBaseWebApp.DataAccessLayer.Repositories;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee?> GetEmployeeByEmployeeId(int EmployeeId);

        Task<List<Employee>> GetOrganizationEmployees(int organizationId);
    }
}
