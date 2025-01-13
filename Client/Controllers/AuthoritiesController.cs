using API.Models;
using Client.Base;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class AuthoritiesController : BaseController<Authority, AuthorityRepository, int>
    {
        private readonly AuthorityRepository repository;
        public AuthoritiesController(AuthorityRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpPost("PostInstructorRole")]
        public JsonResult PostInstructorRole(Authority authority)
        {
            var result = repository.Post(authority);
            return Json(result);
        }

    }
}
