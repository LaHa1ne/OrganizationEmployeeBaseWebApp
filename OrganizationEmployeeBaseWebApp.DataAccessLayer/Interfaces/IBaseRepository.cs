using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        Task CreateRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
