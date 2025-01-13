using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }     
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult CourseReview()
        {
            return View();
        }
        public IActionResult Categories()
        {
            return View();
        }
        public new IActionResult Unauthorized()
        {
            return View();
        }
        public IActionResult Forbidden()
        {
            return View();
        }
        public IActionResult Notfound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/login?logout=true");

        }
    }
}
