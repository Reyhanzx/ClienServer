using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountRepository repository;
    private readonly IConfiguration configuration;

    public AccountController(AccountRepository repository, IConfiguration configuration)
    {
        this.repository = repository;
        this.configuration = configuration;
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
            //var result = await repository.Login(entity);
            //if (result == false)
            //{
            //return BadRequest(new
            //{
            //    StatusCode = 400,
            //    Message = "gagal"
            //});
            //}
            //else
            //{
            //    var userdata = await repository.GetUserdata(entity.Email);

            //    var roles = await repository.GetRolesByNik(entity.Email);

            //    var claims = new List<Claim>()
            //    {
            //    new Claim(ClaimTypes.Email, userdata.Email),
            //    new Claim(ClaimTypes.Name, userdata.FullName)
            //    };

            //    foreach (var item in roles)
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, item));
            //    }

            //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            //    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //    var token = new JwtSecurityToken(
            //        issuer: configuration["JWT:Issuer"],
            //        audience: configuration["JWT:Audience"],
            //        claims: claims,
            //        expires: DateTime.Now.AddMinutes(10),
            //        signingCredentials: signIn
            //        );

            //    var generateToken = new JwtSecurityTokenHandler().WriteToken(token);

            //    HttpContext.Session.SetString("jwtoken", generateToken);
            //return Ok(new
            //{
            //    StatusCode = 200,
            //    Message = "berhasil"
            //});
            //}
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
                var userdata = await repository.GetUserdata(entity.Email);

                var roles = await repository.GetRolesByNik(entity.Email);

                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
                };

                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "berhasil",
                    Data = generateToken
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
