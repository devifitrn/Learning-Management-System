using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CataloguesController : BaseController<Catalogue, CatalogueRepository, int>
    {
        private readonly CatalogueRepository CatalogueRepository;
        public CataloguesController(CatalogueRepository CatalogueRepository) : base(CatalogueRepository)
        {
            this.CatalogueRepository = CatalogueRepository;
        }
    }
}
