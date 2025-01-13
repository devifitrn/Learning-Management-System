using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountRepository repository;
        private readonly ILogger<LoginController> _logger;
        public LoginController(AccountRepository repository, ILogger<LoginController> logger)
        {
            this.repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult VerifyLogin(LoginVM login)
        {
            var results = repository.login(login);
            if (results.Status == HttpStatusCode.OK)
            {
                var token = results.JWT;

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var id = jwt.Claims.First(claim => claim.Type == "Id").Value;
                var email = jwt.Claims.First(claim => claim.Type == "Email").Value;
                var roles = jwt.Claims.First(claim => claim.Type == "roles").Value;
                HttpContext.Session.SetString("Id", id);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("Roles", roles);
                HttpContext.Session.SetString("JWToken", token);

                return Json(new {results.Status, roles, results.Message});
            }
            else
            {
                return Json(new { results.Status, results.Message });
            }
        }
    }
}
