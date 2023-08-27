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

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for employee Id'{compensation.Employee.EmployeeId}'");

            //Check to see if we already have a compensation for this employee.
            //I am going with the assumption that an employee will only have one compensation at a time.
            //This could be different if we needed to keep track of historic compensations for example.
            var compensationCheck = _compensationService.GetByEmployeeId(compensation.Employee.EmployeeId);

            if (compensationCheck != null)
            {
                _compensationService.Replace(compensationCheck, compensation);
            }
            else 
            { 

                //Retrieve employee information
                var employee = _employeeService.GetById(compensation.Employee.EmployeeId);

                //Return not found if given invalid employee Id
                if (employee == null)
                    return NotFound();

                compensation.Employee = employee;

                _compensationService.Create(compensation);
            }
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
