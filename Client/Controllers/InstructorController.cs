using API.Models;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly CourseRepository repository;
        private readonly ILogger<InstructorController> _logger;
        public InstructorController(ILogger<InstructorController> logger, CourseRepository repository)
        {
            this.repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Course()
        {
            return View();
        }
        [Route("[controller]/course/view/{id}")]
        public async Task<IActionResult> CourseViewAsync(int id)
        {
            var result = await repository.Get(id);

            return View(result);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/login?logout=true");

        }
        /*public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var extension = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                    var fileName = DateTime.Now.Ticks + extension;
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
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePaths });
        }*/
    }
}
