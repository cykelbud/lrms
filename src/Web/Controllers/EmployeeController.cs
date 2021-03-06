﻿using System;
using System.Threading.Tasks;
using Employee.Core.ApplicationServices;
using Employee.Requests;
using Employee.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet()]
        public async Task<EmployeeDto[]> GetAllEmployees()
        {
            return await _employeeService.GetAll();
        }

        [HttpGet("{employeeId}")]
        public async Task<EmployeeDto> GetEmployee(Guid employeeId)
        {
            return await _employeeService.GetEmployee(employeeId);
        }

        [HttpPost("")]
        public async Task<Guid> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            return await _employeeService.CreateEmployee(request);
        }
    }
}
