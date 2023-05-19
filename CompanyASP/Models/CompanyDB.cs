using Microsoft.EntityFrameworkCore;
using CompanyASP.Models;

namespace CompanyASP.Models
{
    public class CompanyDB : DbContext
    {
        public CompanyDB(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<CompanyASP.Models.EmployeeView>? EmployeeView { get; set; }

    }
}
