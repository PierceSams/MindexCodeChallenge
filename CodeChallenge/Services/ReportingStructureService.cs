using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;
        private ReportingStructure reportingStructure;
        //
        private bool firstCall = true;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            reportingStructure = new ReportingStructure();
        }

        public ReportingStructure GetByEmployeeId(string employeeId)
        {
            if(!String.IsNullOrEmpty(employeeId))
            {
                reportingStructure.Employee = _employeeRepository.GetById(employeeId);
                //Recursive method for getting all children employees.
                GetNumberOfReports(employeeId);

                return reportingStructure;

            }

            return null;
        }

        private void GetNumberOfReports(string employeeId)
        {
            Employee employee;
            /* Check if this is our first call. 
             * This isn't necessary, but I wanted to avoid the extra database call.
             */
            if (firstCall)
            {
                employee = reportingStructure.Employee;
                firstCall = false;
            }
            else 
            {
                employee = _employeeRepository.GetById(employeeId);
            }
            //If this employee has direct reports we need to add them to our count and check for more.
            if (employee.DirectReports != null)
            {
                //Increment number of reports for this reporting structure.
                reportingStructure.NumberOfReports += employee.DirectReports.Count;
                //Loop through each found direct report and call this function again to check if they have direct reports
                //until we get to the bottom of the tree.
                foreach (var managedEmployee in employee.DirectReports)
                {
                    GetNumberOfReports(managedEmployee.EmployeeId);
                }
            }
        }

    }
}
