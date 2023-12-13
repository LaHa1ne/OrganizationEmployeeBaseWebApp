using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataLayer.Entities
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; } = null!;
        public string INN { get; set; } = null!;
        public string LegalAddress { get; set; } = null!;
        public string ActualAddress { get; set; } = null!;

        public ICollection<Employee> Employees = null!;
    }
}
