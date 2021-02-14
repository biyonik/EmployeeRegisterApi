using EmployeeRegisterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegisterApi.Context
{
    public class EmployeeAppContext: DbContext
    {
        public EmployeeAppContext(DbContextOptions<EmployeeAppContext> options): base(options)
        {
            
        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}