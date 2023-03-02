using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UniversitiesController : BaseController<int, University, UniversityRepository>
{
    private readonly UniversityRepository repository;

    public UniversitiesController(UniversityRepository repository) : base(repository)
    {
        this.repository = repository;
    }
}
