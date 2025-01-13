using Client.Base;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Client.Repositories.Data;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class ContentsController : BaseController<Content, ContentRepository, int>
    {
        private readonly ContentRepository repository;
        public ContentsController(ContentRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
