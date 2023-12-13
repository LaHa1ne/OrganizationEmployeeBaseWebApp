using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using OrganizationEmployeeBaseWebApp.DataAccessLayer.Interfaces;
using OrganizationEmployeeBaseWebApp.DataAccessLayer.Repositories;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs;
using OrganizationEmployeeBaseWebApp.DataLayer.DTOs.CSV;
using OrganizationEmployeeBaseWebApp.DataLayer.Entities;
using OrganizationEmployeeBaseWebApp.DataLayer.Extensions;
using OrganizationEmployeeBaseWebApp.DataLayer.Responses;
using OrganizationEmployeeBaseWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.Services.Services
{
    public class OrganizationService : IOrganizationService
    {
        protected readonly IOrganizationRepository _organizationRepository;
        protected readonly IMapper _mapper;

        public OrganizationService(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrganizationDTO>> CreateOrganization(OrganizationDTO organization)
        {
            try
            {
                var newOrganization = new Organization();
                newOrganization.UpdateInfo(organization);

                await _organizationRepository.Create(newOrganization);
                return new BaseResponse<OrganizationDTO>()
                {
                    Description = "Организация создана",
                    Data = _mapper.Map<OrganizationDTO>(newOrganization),
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrganizationDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteOrganization(int organizationId)
        {
            try
            {
                var organization = await _organizationRepository.GetOrganizationByOrganizationId(organizationId);
                if (organization == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Организация не найдена",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                await _organizationRepository.Delete(organization);
                return new BaseResponse<bool>()
                {
                    Description = "Организация удалена",
                    Data = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<OrganizationDTO>> EditOrganization(OrganizationDTO organization)
        {
            try
            {
                var editedOrganization = await _organizationRepository.GetOrganizationByOrganizationId(organization.OrganizationId);
                if (editedOrganization == null)
                {
                    return new BaseResponse<OrganizationDTO>()
                    {
                        Description = "Организация не найдена",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                editedOrganization!.UpdateInfo(organization);
                await _organizationRepository.Update(editedOrganization!);
                return new BaseResponse<OrganizationDTO>()
                {
                    Description = "Информация об организации отредактирована",
                    Data = _mapper.Map<OrganizationDTO>(editedOrganization),
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrganizationDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<OrganizationDTO>>> GetOrganizations()
        {
            try
            {
                return new BaseResponse<List<OrganizationDTO>>()
                {
                    Description = "Организации получены",
                    Data = _mapper.Map<List<OrganizationDTO>>(await _organizationRepository.GetOrganizations()),
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrganizationDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<OrganizationDTO>>> LoadOrganizationsFromCSV(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new BaseResponse<List<OrganizationDTO>>()
                    {
                        Description = "Файл не найден или пуст",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var organizationsCSV = await csv.GetRecordsAsync<OrganizationWithEmployeeCSV>().ToListAsync();
                        var organizations = organizationsCSV.ToOrganizations();
                        await _organizationRepository.CreateRange(organizations);

                        return new BaseResponse<List<OrganizationDTO>>()
                        {
                            Description = "Организации загружены",
                            Data = _mapper.Map<List<OrganizationDTO>>(organizations),
                            StatusCode = HttpStatusCode.OK
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrganizationDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<byte[]>> ExportOrganizations()
        {
            try
            {
                var organizations = await _organizationRepository.GetOrganizationsWithEmployees();
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.Unicode))
                    {
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            var organizationsCSV = organizations.ToOrganizationsCSV();
                            await csv.WriteRecordsAsync(organizationsCSV);
                        }
                    }
                    var fileContent = memoryStream.ToArray();

                    return new BaseResponse<byte[]>()
                    {
                        Description = "Организации выгружены",
                        Data = fileContent,
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<byte[]>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
