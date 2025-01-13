using API.Models;
using Client.Base;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("register")]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = repository.Register(registerVM);
            return Json(result);
        }
        [HttpPost("registerPhoto")]
        public async Task<JsonResult> RegisterPhoto([FromForm] RegisterFotoVM registerFoto)
        {
            string fileName = null;
            if (registerFoto.Foto.Length > 0)
            {
                var extension = "." + registerFoto.Foto.FileName.Split('.')[registerFoto.Foto.FileName.Split('.').Length - 1];
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
                    await registerFoto.Foto.CopyToAsync(stream);
                }
            }
            RegisterVM reg = new RegisterVM
            {
                FirstName = registerFoto.FirstName,
                LastName = registerFoto.LastName,
                PhoneNumber = registerFoto.PhoneNumber,
                BirthDate = registerFoto.BirthDate,
                Email = registerFoto.Email,
                Gender = registerFoto.Gender,
                Password = registerFoto.Password,
                Role = registerFoto.Role,
                ProfilePicture = fileName
            };
            var result = repository.Register(reg);
            return Json(result);
        }

    }
}
