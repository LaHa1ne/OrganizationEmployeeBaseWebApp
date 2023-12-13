using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using OrganizationEmployeeBaseWebApp.Models.EmployeeViewModels;
using OrganizationEmployeeBaseWebApp.Models.OrganizationViewModels;
using OrganizationEmployeeBaseWebApp.Services.Interfaces;
using OrganizationEmployeeBaseWebApp.Services.Services;
using System.Net;

namespace OrganizationEmployeeBaseWebApp.Controllers
{

    [Route("[controller]/[action]")]
    public class EmployeesController : Controller
    {
        protected readonly IEmployeeService _employeeService;
        protected readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet("{organizationId}")]
        public async Task<IActionResult> Employees(int organizationId)
        {
            var response = await _employeeService.GetOrganizationEmployees(organizationId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return View((response.Data, organizationId));
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.CreateEmployee(_mapper.Map<EmployeeDTO>(employee));
                return new JsonResult(response) { StatusCode = (int)response.StatusCode };
            }
            return PartialView("_CreateEmployeeForm", employee);
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee(EditEmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.EditEmployee(_mapper.Map<EmployeeDTO>(employee));
                return new JsonResult(response) { StatusCode = (int)response.StatusCode };
            }
            return PartialView("_EditEmployeeForm", employee);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var response = await _employeeService.DeleteEmployee(employeeId);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> LoadEmployeesFromCSV(int organizationId, IFormFile file)
        {
            var response = await _employeeService.LoadEmployeesFromCSV(organizationId, file);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpPost("{organizationId}")]
        public async Task<IActionResult> ExportOrganizationEmployeesToCSV(int organizationId)
        {
            var response = await _employeeService.ExportOrganizationEmployees(organizationId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return File(response.Data, "text/csv");
            }
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }
    }

}
