using Client.Base;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Client.Repositories.Data;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class ResourcesController : BaseController<Resource, ResourceRepository, int>
    {
        private readonly ResourceRepository repository;
        public ResourcesController(ResourceRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Upload")]
        public async Task<JsonResult> UploadAsync([FromForm] ResourceUploadVM uploadVM)
        {
            long size = uploadVM.File.Sum(f => f.Length);
            foreach (var formFile in uploadVM.File)
            {
                if (formFile.Length > 0)
                {
                    var extension = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                    string fileName = DateTime.Now.Ticks + extension;
                    extension = formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                    // full path to file in temp location
                    var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\resources"); //we are using Temp file name just for the example. Add your own file path.
                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\resources",fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    Resource resource = new Resource
                    {
                        SubContentId = uploadVM.SubContentId,
                        FileName = fileName,
                        Type = extension
                    };
                    repository.Post(resource);
                }
            }
            return Json(uploadVM.SubContentId);
        }
    }
}
