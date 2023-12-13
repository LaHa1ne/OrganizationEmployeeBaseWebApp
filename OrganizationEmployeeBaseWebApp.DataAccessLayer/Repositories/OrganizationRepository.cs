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
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext db) : base(db) { }
        public async Task<Organization?> GetOrganizationByOrganizationId(int organizationId)
        {
            return await _db.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == organizationId);
        }

        public async Task<List<Organization>> GetOrganizations()
        {
            return await _db.Organizations.ToListAsync();
        }
        public async Task<List<Organization>> GetOrganizationsWithEmployees()
        {
            return await _db.Organizations.Include(o => o.Employees).ToListAsync();
        }
    }
}
