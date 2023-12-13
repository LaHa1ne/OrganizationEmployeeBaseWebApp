using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataLayer.DTOs
{
    public class OrganizationDTO
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; } = null!;
        public string INN { get; set; } = null!;
        public string LegalAddress { get; set; } = null!;
        public string ActualAddress { get; set; } = null!;
    }
}
