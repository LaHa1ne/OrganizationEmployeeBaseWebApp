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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationEmployeeBaseWebApp.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IEmployeeRepository _employeeRepository;
        protected readonly IOrganizationRepository _organizationRepository;
        protected readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,  IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<List<EmployeeDTO>>> GetOrganizationEmployees(int organizationId)
        {
            try
            {
                var organization = await _organizationRepository.GetOrganizationByOrganizationId(organizationId);
                if (organization == null)
                {
                    return new BaseResponse<List<EmployeeDTO>>()
                    {
                        Description = "Организация не найдена",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
                return new BaseResponse<List<EmployeeDTO>>()
                {
                    Description = "Сотрудники организации получены",
                    Data = _mapper.Map<List<EmployeeDTO>>(await _employeeRepository.GetOrganizationEmployees(organizationId)),
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponse<List<EmployeeDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<EmployeeDTO>> CreateEmployee(EmployeeDTO employee)
        {
            try
            {
                var newEmployee = new Employee();
                newEmployee.UpdateInfo(employee);

                await _employeeRepository.Create(newEmployee);
                return new BaseResponse<EmployeeDTO>()
                {
                    Description = "Сотрудник создана",
                    Data = _mapper.Map<EmployeeDTO>(newEmployee),
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<EmployeeDTO>> EditEmployee(EmployeeDTO employee)
        {
            try
            {
                var editedEmployee = await _employeeRepository.GetEmployeeByEmployeeId(employee.EmployeeId);
                if (editedEmployee == null)
                {
                    return new BaseResponse<EmployeeDTO>()
                    {
                        Description = "Сотрудник не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                editedEmployee!.UpdateInfo(employee);
                await _employeeRepository.Update(editedEmployee!);
                return new BaseResponse<EmployeeDTO>()
                {
                    Description = "Информация о сотруднике отредактирована",
                    Data = _mapper.Map<EmployeeDTO>(editedEmployee),
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeId);
                if (employee == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Сотрудник не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                await _employeeRepository.Delete(employee);
                return new BaseResponse<bool>()
                {
                    Description = "Сотрудник удален",
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

        public async Task<BaseResponse<List<EmployeeDTO>>> LoadEmployeesFromCSV(int organizationId, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new BaseResponse<List<EmployeeDTO>>()
                    {
                        Description = "Файл не найден или пуст",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var employeesCSV = await csv.GetRecordsAsync<EmployeeCSV>().ToListAsync();
                        var employees = _mapper.Map<List<Employee>>(employeesCSV);
                        employees.ForEach(e => e.OrganizationId = organizationId);
                        await _employeeRepository.CreateRange(employees);

                        return new BaseResponse<List<EmployeeDTO>>()
                        {
                            Description = "Сотрудники загружены",
                            Data = _mapper.Map<List<EmployeeDTO>>(employees),
                            StatusCode = HttpStatusCode.OK
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<EmployeeDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<byte[]>> ExportOrganizationEmployees(int organizationId)
        {
            try
            {
                var organization = await _organizationRepository.GetOrganizationByOrganizationId(organizationId);
                if (organization == null)
                {
                    return new BaseResponse<byte[]>()
                    {
                        Description = "Организация не найдена",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.Unicode))
                    {
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            var employees = await _employeeRepository.GetOrganizationEmployees(organizationId);
                            var employeesCSV = _mapper.Map<List<EmployeeCSV>>(employees);
                            await csv.WriteRecordsAsync(employeesCSV);
                        }
                    }
                    var fileContent = memoryStream.ToArray();

                    return new BaseResponse<byte[]>()
                    {
                        Description = "Сотрудники выгружены",
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
