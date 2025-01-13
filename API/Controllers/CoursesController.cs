using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController<Course, CourseRepository, int>
    {
        private readonly CourseRepository courseRepository;
        public CoursesController(CourseRepository CourseRepository) : base(CourseRepository)
        {
            this.courseRepository = CourseRepository;
        }
        [HttpGet("MasterCourse")]
        public ActionResult MasterData()
        {
            var getMaster = courseRepository.MasterCourse();
            //return Ok(getMaster);
            if (getMaster != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getMaster, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = getMaster, message = "Data tidak ditemukan" });
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("getmasterdata/{ID}")]
        public ActionResult GetTheMasterData(int ID)
        {
            try
            {
                var result = courseRepository.GetMasterData(ID);
                if (result == null)
                {
                    return NotFound(new { Status = 404, Result = result, Message = "Data tidak ditemukan" });
                }
                else
                {
                    return Ok(new { Status = 200, Result = result, Message = "Data ditemukan" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("getmasterdata")]
        public ActionResult GetTheMasterData()
        {
            try
            {
                var result = courseRepository.GetMasterData();
                int count = result.Count();
                if (count == 0)
                {
                    return NotFound(new { Status = 404, Result = result, Message = "Data tidak ditemukan" });
                }
                else
                {
                    return Ok(new { Status = 200, Result = result, Message = $"{count} data ditemukan" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }
        [HttpPut("UpdateStatus")]
        public ActionResult UpdateStatus(Course course)
        {
            var result = courseRepository.UpdateStatus(course);
            return Ok(new { status = 200, result, message = "Data Berhasil Diupdate" });
        }
        [HttpGet("getbyuser/{ID}")]
        public ActionResult GetByUser(string ID)
        {
            try
            {
                var result = courseRepository.GetByUser(ID);
                if (!result.Any())
                {
                    return NotFound(new { Status = 404, Result = result, Message = "Data tidak ditemukan" });
                }
                else
                {
                    return Ok(new { Status = 200, Result = result, Message = "Data ditemukan" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }
    }
}
