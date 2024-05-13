using Microsoft.AspNetCore.Mvc;
using Learning.Model;
using Learning.Repo;
using Learning.ViewModel;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpPost]
    public IActionResult Add([FromForm]EmployeeViewModel employeeView)
    {
        var filePath = Path.Combine("Storage", employeeView.Photo.FileName);
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        employeeView.Photo.CopyTo(fileStream);
        
        var employee = new Employee(employeeView.Name, employeeView.Age, filePath);
        
        _employeeRepository.Add(employee);

        return Ok();
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        var employees = _employeeRepository.Get();

        return Ok(employees);
    }

    [Authorize]
    [HttpGet]
    [Route("{id}/")]
    public IActionResult Find(int id)
    {
        var employee = _employeeRepository.Get(id); //pode retornar nulo

        if (employee == null)
        {
            return Ok("Employee not found"); // Retorna HTTP 200 OK com uma mensagem personalizada
        }

        return Ok(employee);
    }
    
    [Authorize]
    [HttpGet]
    [Route("{id}/download")]
    public IActionResult DownloadPhoto(int id)
    {
        var employee = _employeeRepository.Get(id);

        var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

        return File(dataBytes, "image/png");
    }

}