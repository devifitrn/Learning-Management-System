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
    public class CataloguesController : BaseController<Catalogue, CatalogueRepository, int>
    {
        private readonly CatalogueRepository repository;
        public CataloguesController(CatalogueRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
