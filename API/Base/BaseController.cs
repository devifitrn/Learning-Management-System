using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Base
{
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity,Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            if (result.Count() == 0)
            {
                return NotFound(new { status = 404, result, message = "Data Tidak Ditemukan" });
            }
            return Ok(new { status = 200, result, message = "Data Ditemukan" });
        }
        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            if (result == null)
            {
                return NotFound(new { status = 404, result, message = "Data Tidak Ditemukan" });
            }
            return Ok(new { status = 200, result, message = "Data Ditemukan" });
        }

        [HttpPost]
        public virtual ActionResult Post(Entity entity)
        {
            var result = repository.Insert(entity);
            return Ok(new { status = 200, result, message = "Data Berhasil Ditambahkan" });
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                var result = repository.Update(entity);
                return Ok(new { status = 200, result, message = "Data Berhasil Diupdate" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex.Message });
            }
        }
            
        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var check = repository.Get(key);
            if (check == null)
            {
                return Ok(new { status = 200, result = 0, message = "Data Tidak Ditemukan" });
            }
            var result = repository.Delete(key);
            return Ok(new { status = 200, result, message = "Data Berhasil Dihapus" });
        }
    }
}
