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
    public class UsersController : BaseController<User, UserRepository, string>
    {
        private readonly UserRepository userRepository;
        public UsersController(UserRepository userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("getmasterdata/{ID}")]
        public ActionResult GetTheMasterData(string ID)
        {
            try
            {
                var result = userRepository.GetMasterData(ID);
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
                var result = userRepository.GetMasterData();
                if (result.Count() == 0)
                {
                    return NotFound(new { Status = 404, Result = result, Message = "Data tidak ditemukan" });

                }
                else
                {
                    return Ok(new { Status = 200, Result = result, Message = $"{result.Count()} data ditemukan" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }
        [HttpGet("getstudentdata")]
        public ActionResult GetTheStudentData()
        {
            try
            {
                var result = userRepository.GetStudentData();
                if (result == null)
                {
                    return NotFound(new { Status = 404, Result = result, Message = "Data tidak ditemukan" });

                }
                else
                {
                    return Ok(new { Status = 200, Result = result, Message = "data ditemukan" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [HttpPut("UpdateStatus")]
        public ActionResult UpdateStatus(User student)
        {
            var result = userRepository.UpdateStatus(student);
            return Ok(new { status = 200, result, message = "Status student berubah menjadi request" });
        }

    }
}
