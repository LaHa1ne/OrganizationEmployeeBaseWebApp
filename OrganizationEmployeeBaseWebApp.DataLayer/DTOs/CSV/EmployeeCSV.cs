using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.DataLayer.DTOs.CSV
{
    public class EmployeeCSV
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string PassportSeries { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
    }
}
