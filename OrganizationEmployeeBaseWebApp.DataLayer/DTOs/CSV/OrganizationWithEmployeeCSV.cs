using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataLayer.DTOs.CSV
{
    public class OrganizationWithEmployeeCSV
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; } = null!;
        public string INN { get; set; } = null!;
        public string LegalAddress { get; set; } = null!;
        public string ActualAddress { get; set; } = null!;
        public int? EmployeeId { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Patronymic { get; set; } 
        public DateOnly? DateOfBirth { get; set; }
        public string? PassportSeries { get; set; } 
        public string? PassportNumber { get; set; } 
    }
}
