using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Base
{
    [Route("/[controller]")]
    [Controller]
    public class BaseController<TEntity, TRepository, TId> : Controller
        where TEntity : class
        where TRepository : IRepository<TEntity, TId>
    {
        private readonly TRepository repository;
        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpGet("Get/{id}")]
        public async Task<JsonResult> Get(TId id)
        {
            var result = await repository.Get(id);
            return Json(result);
        }

        [HttpPost("Post")]
        public JsonResult Post([FromBody]TEntity entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }

        [HttpPut("Put")]
        public virtual JsonResult Put(TEntity entity)
        {
            var result = repository.Put(entity);
            return Json(result);
        }

        [HttpDelete("Delete/{id}")]
        public JsonResult Delete(TId id)
        {
            var result = repository.Delete(id);
            return Json(result);
        }
    }
}
