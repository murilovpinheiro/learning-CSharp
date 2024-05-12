using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Learning.Repo;

using Learning.Model;

public class Connection : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseNpgsql(
            "Server=localhost;" +
        "Port=5432;Database=employeedb;" +
        "User Id=postgres;" +
        "Password=2004;");

}