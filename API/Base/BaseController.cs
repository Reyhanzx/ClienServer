using API.Models;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Key, Entity, Repository> : ControllerBase
        where Entity : class
        where Repository : IRepository<Key, Entity>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
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

        [HttpGet]
        [Route("{key}")]
        public async Task<ActionResult> GetById(Key key)
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

        [HttpPost]
        public async Task<ActionResult> Insert(Entity entity)
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

        [HttpPut]
        public async Task<ActionResult> Update(Entity entity)
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
        public async Task<ActionResult> Delete(Key key)
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
    }

   

}
