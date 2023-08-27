using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Models;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        //Assuming an employee will only have one compensation at a time. So we will update if they already have one and create one if they don't
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for employee Id'{compensation.Employee.EmployeeId}'");
            
            //Retrieve employee information
            var employee = _employeeService.GetById(compensation.Employee.EmployeeId);
            compensation.Employee = employee;

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.Employee.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
