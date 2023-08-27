using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }
        //REST endpoint for retrieving the reporting structure of a given employee
        [HttpGet("{id}", Name = "getEmployeeStructure")]
        public IActionResult GetEmployeeReportingStructure(String id)
        {
            _logger.LogDebug($"Received employee reporting structure request for '{id}'");

            var reportingStructure = _reportingStructureService.GetByEmployeeId(id);

            if (reportingStructure == null)
                return NotFound();

            return Ok(reportingStructure);
        }
    }
}
