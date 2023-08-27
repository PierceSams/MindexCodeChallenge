using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    //New interface for retrieving reporting structure.
    public interface IReportingStructureService
    {
        ReportingStructure GetByEmployeeId(string id);
    }
}
