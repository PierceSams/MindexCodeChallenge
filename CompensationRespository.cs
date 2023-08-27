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
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRespository(ILogger<ICompensationRepository> logger, CompensationContext employeeContext)
        {
            _compensationContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);         
            return compensation;
        }

        public Compensation GetByEmployeeId(string id)
        {
            //Get compensation that has an employee with the matching id.
            return _compensationContext.Compensations.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Compensation Update(string id)
        {
            //Get compensation that has an employee with the matching id.
            return _compensationContext.Compensations.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

    }
}
