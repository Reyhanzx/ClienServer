using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Contexts;
using API.Models;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : BaseController<int, Education, EducationRepository>
{
    private readonly EducationRepository repository;
    public EducationsController(EducationRepository repository) : base(repository)
    {
        this.repository = repository;
    }

}
