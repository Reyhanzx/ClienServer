using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Contexts;
using API.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : ControllerBase
{
    private readonly EducationRepository repository;
    public EducationsController(EducationRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult> Insert(Education entity)
    {
        try
        {
            var result = await repository.Insert(entity);
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

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await repository.GetAll();
        if (result == null)
        {
            return Ok(new
            {
                StatusCode = 200,
                Massage = "kosong",
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Massage = "ada",
                Data = result
            });
        }
    }
    [HttpPut]
    public async Task<ActionResult> Update(Education entity)
    {
        try
        {
            var result = await repository.Update(entity);
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

    [HttpDelete]
    public async Task<ActionResult> Delete(int key)
    {
        var result = await repository.Delete(key);
        if (result == null)
        {
            return Ok(new
            {
                StatusCode = 200,
                Massage = "kosong",
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Massage = "ada",
                Data = result
            });
        }
    }

    [HttpGet]
    [Route("{key}")]
    public async Task<ActionResult> GetById(int key)
    {
        try
        {
            var result = await repository.GetById(key);
            if (result == null)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Massage = "kosong",
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Massage = "ada",
                    Data = result
                });
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
}
