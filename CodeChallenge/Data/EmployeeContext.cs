using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            
        }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne(e => e.Compensation).WithOne(c => c.Employee).HasForeignKey<Compensation>(c=> c.EmployeeFK);
       }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Compensation> Compensations { get; set; }
    }
}
