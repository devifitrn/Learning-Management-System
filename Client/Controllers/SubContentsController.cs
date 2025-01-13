using Client.Base;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Client.Repositories.Data;
using System;
using System.IO;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class SubContentsController : BaseController<SubContent, SubContentRepository, int>
    {
        private readonly SubContentRepository repository;
        public SubContentsController(SubContentRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Upload")]
        public async Task<JsonResult> UploadAsync([FromForm] SubContentUploadVM uploadVM)
        {
            string fileName = null;
            if (uploadVM.Video.Length > 0)
            {
                var extension = "." + uploadVM.Video.FileName.Split('.')[uploadVM.Video.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension;
                // full path to file in temp location
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\videos"); //we are using Temp file name just for the example. Add your own file path.
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\videos",
                   fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await uploadVM.Video.CopyToAsync(stream);
                }
            }

            SubContent subContent = new SubContent
            {
                ContentId = uploadVM.ContentId,
                Title = uploadVM.Title,
                VideoName = fileName
            };
            var result = repository.Post(subContent);
            return Json(result);
        }
    }
}
