using Microsoft.AspNetCore.Mvc;
using Learning.Model;
using Learning.Repo;
using Learning.ViewModel;

namespace Learning.Controller;

[ApiController]
[Route("api/v1/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    [HttpPost]
    public IActionResult Add(EmployeeViewModel employeeView)
    {
        var employee = new Employee(employeeView.Name, employeeView.Age, null);
        
        _employeeRepository.Add(employee);

        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var employees = _employeeRepository.Get();

        return Ok(employees);
    }
}