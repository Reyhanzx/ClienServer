using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountRepository repository;

    public AccountController(AccountRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost("/Register")]
    public async Task<ActionResult> Register(RegisterVM register)
    {
        try
        {
            var result = await repository.Register(register);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "gagal"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "berhasil"
                });
            }
        }
        catch
        {

            return BadRequest(new
            {
                StatusCode = 400,
                Message = "salah! "
            });
        }

    }

    [HttpPost("/Login")]
    public async Task<ActionResult> Login(LoginVM entity)
    {
        try
        {
            var result = await repository.Login(entity);
            if (result == false)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "gagal"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "berhasil"
                });
            }
        }
        catch
        {

            return BadRequest(new
            {
                StatusCode = 400,
                Message = "salah! "
            });
        }

    }

}
