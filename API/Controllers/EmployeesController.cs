using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController<string, Employee, EmployeeRepository>
{
    private readonly EmployeeRepository repository;

    public EmployeesController(EmployeeRepository repository) : base(repository)
    {
        this.repository = repository;
    }
}
