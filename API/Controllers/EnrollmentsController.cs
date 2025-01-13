using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : BaseController<Enrollment, EnrollmentRepository, int>
    {
        private readonly EnrollmentRepository EnrollmentRepository;
        public EnrollmentsController(EnrollmentRepository EnrollmentRepository) : base(EnrollmentRepository)
        {
            this.EnrollmentRepository = EnrollmentRepository;
        }

        /*[HttpPost("Enroll")]
        public ActionResult Login(Enrollment enrollment)
        {
            string login = enrollmentRepository.Login(loginVM);
            return login switch
            {
                "1" => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed (Password salah)" }),
                "2" => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed (Email dan password salah)" }),
                _ => Ok(new { status = HttpStatusCode.OK, JWT = login, message = "Login Successfull" })
                // _ => NotFound(new { status = HttpStatusCode.NotFound, message = "error" })
            };
        }*/

        /*[HttpGet("getenrollmentdata/{UserId}")]
        public ActionResult GetEnrollmentData(string UserId)
        {
            var result = EnrollmentRepository.GetEnrollment(UserId);
            return (Ok(new { status = HttpStatusCode.OK, result = result, message = " Successfull" }));
            
        }*/

        [HttpGet("getenrollmentdata/{UserId}")]
        public ActionResult GetEnrollmentData(string UserId)
        {
            try
            {
                var result = EnrollmentRepository.GetEnrollment(UserId);
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
        [HttpPost("PostReturn")]
        public virtual ActionResult PostReturn(Enrollment entity)
        {
            var result = EnrollmentRepository.EnrollMid(entity);
            return Ok(new { status = 200, result, message = "Data Berhasil Ditambahkan" });
        }
        [HttpGet("CheckEnrollment/{userId}/{courseId}")]
        public virtual ActionResult CheckReview(string userId, int courseId)
        {
            var result = EnrollmentRepository.CheckEnrollment(userId, courseId);
            return Ok(result);
        }
    }
}
