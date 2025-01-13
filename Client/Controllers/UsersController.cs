using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class UsersController : BaseController<User, UserRepository, string>
    {
        private readonly UserRepository repository;
        public UsersController(UserRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("getmasterdata")]
        public async Task<JsonResult> GetMasterData()
        {
            var result = await repository.GetMasterData();
            return Json(result);
        }

        [HttpGet("getmasterdata/{NIK}")]
        public async Task<JsonResult> GetMasterData(string NIK)
        {
            var result = await repository.GetMasterData(NIK);
            return Json(result);
        }
        [HttpGet("getstudents")]
        public async Task<JsonResult> GetStudents()
        {
            var result = await repository.GetStudentData();
            return Json(result);
        }

        [HttpPut]
        public JsonResult UpdateId(User user)
        {
            var result = repository.UpdateNIK(user);
            return Json(result);
        }

        [HttpGet("getenrollment")]
        public async Task<JsonResult> GetEnrollment()
        {
            var result = await repository.MasterEnroll();
            return Json(result);
        }


        [HttpPut("InstructorSignup")]
        public JsonResult InstructorSignup(User student)
        {
            var result = repository.AssignRequestStatus(student);
            return Json(result);
        }
    }
}
