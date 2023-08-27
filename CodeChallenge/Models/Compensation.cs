using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
   
    public class Compensation
    {
        public int CompensationId { get; set; }
        public Employee Employee { get; set; }
        public string Salary { get; set; }
        public string EffectiveDate { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeFK { get; set; }
    }
}
