using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class CompensationRespository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRespository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetByEmployeeId(string id)
        {
            //Get compensation that has an employee with the matching id.
            return _employeeContext.Compensations.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Compensation Update(string id)
        {
            //Get compensation that has an employee with the matching id.
            return _employeeContext.Compensations.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

    }
}
