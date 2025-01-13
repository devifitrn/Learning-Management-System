using Client.Base;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Client.Repositories.Data;
using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class CoursesController : BaseController<Course, CourseRepository, int>
    {
        private readonly CourseRepository repository;
        public CoursesController(CourseRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

       /* public IActionResult CourseView()
        {
            return View();
        }*/
        [HttpGet("GetCourseData")]
        public async Task<JsonResult> GetCourseData()
        {
            var result = await repository.GetCourseData();
            return Json(result);
        }
        /*[HttpPost]
        public JsonResult Register(RegistrationVM registrationVM)
        {
            var result = repository.Register(registrationVM);
            return Json(result);
        }*/
        [HttpPost("Upload")]
        public async Task<JsonResult> UploadAsync([FromForm] CourseUploadVM uploadVM)
        {
            string fileName = null;
                if (uploadVM.Foto.Length > 0)
                {
                    var extension = "." + uploadVM.Foto.FileName.Split('.')[uploadVM.Foto.FileName.Split('.').Length - 1];
                    fileName = DateTime.Now.Ticks + extension;
                    // full path to file in temp location
                    var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\images"); //we are using Temp file name just for the example. Add your own file path.
                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\images",
                       fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await uploadVM.Foto.CopyToAsync(stream);
                    }
                }

            Course course = new Course
            {
                UserId = uploadVM.UserId,
                Title = uploadVM.Title,
                Description = uploadVM.Description,
                Price = uploadVM.Price,
                Features = uploadVM.Features,
                Picture = fileName
            };
            var result = repository.Post(course);
            return Json(result);
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

        [HttpPut("updatestatus")]
        public JsonResult UpdateStatus([FromBody]Course course)
        {
            var result = repository.UpdateStatus(course);
            return Json(result);
        }
        [HttpGet("GetByUser/{Id}")]
        public async Task<JsonResult> GetByUser(string Id)
        {
            var result = await repository.GetByUser(Id);
            return Json(result);
        }
        

    }
}
