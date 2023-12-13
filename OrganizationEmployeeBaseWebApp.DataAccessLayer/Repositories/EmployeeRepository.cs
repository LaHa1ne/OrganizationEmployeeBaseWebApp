using Microsoft.EntityFrameworkCore;
using OrganizationEmployeeBaseWebApp.DataAccessLayer.Interfaces;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext db) : base(db) { }
        public async Task<Employee?> GetEmployeeByEmployeeId(int employeeId)
        {
            return await _db.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<List<Employee>> GetOrganizationEmployees(int organizationId)
        {
            return await _db.Employees.Where(e => e.OrganizationId == organizationId).ToListAsync();
        }
    }
}
