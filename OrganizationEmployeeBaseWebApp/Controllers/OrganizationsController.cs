using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.Models.OrganizationViewModels;
using OrganizationEmployeeBaseWebApp.Services.Interfaces;
using OrganizationEmployeeBaseWebApp.Services.Services;
using System.Net;

namespace OrganizationEmployeeBaseWebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class OrganizationsController : Controller
    {
        protected readonly IOrganizationService _organizationService;
        protected readonly IMapper _mapper;

        public OrganizationsController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Organizations()
        {
            var response = await _organizationService.GetOrganizations();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return View(response.Data);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization(CreateOrganizationViewModel organization)
        {
            if (ModelState.IsValid)
            {
                var response = await _organizationService.CreateOrganization(_mapper.Map<OrganizationDTO>(organization));
                return new JsonResult(response) { StatusCode = (int)response.StatusCode };
            }
            return PartialView("_CreateOrganizationForm", organization);
        }

        [HttpPut]
        public async Task<IActionResult> EditOrganization(EditOrganizationViewModel organization)
        {
            if (ModelState.IsValid)
            {
                var response = await _organizationService.EditOrganization(_mapper.Map<OrganizationDTO>(organization));
                return new JsonResult(response) { StatusCode = (int)response.StatusCode };
            }
            return PartialView("_EditOrganizationForm", organization);
        }

        [HttpDelete("{organizationId}")]
        public async Task<IActionResult> DeleteOrganization(int organizationId)
        {
            var response = await _organizationService.DeleteOrganization(organizationId);
            return new JsonResult(response){ StatusCode = (int)response.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> LoadOrganizationsFromCSV(IFormFile file)
        {
            var response = await _organizationService.LoadOrganizationsFromCSV(file);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> ExportOrganizationsToCSV()
        {
            var response = await _organizationService.ExportOrganizations();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return File(response.Data!, "text/csv");
            }
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }


    }
}
