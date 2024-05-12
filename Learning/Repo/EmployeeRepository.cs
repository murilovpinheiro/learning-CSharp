using Learning.Model;

namespace Learning.Repo;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly Connection _context = new Connection();
    
    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public List<Employee> Get()
    {
        return _context.Employees.ToList();
    }

    public Employee? Get(int id)
    {
        return _context.Employees.Find(id);
    }
}