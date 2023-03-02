using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController<int, Role, RoleRepository>
{
    private readonly RoleRepository repository;

    public RolesController(RoleRepository repository) : base(repository)
    {
        this.repository = repository;
    }

   
}
