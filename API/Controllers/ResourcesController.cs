using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : BaseController<Resource, ResourceRepository, int>
    {
        private readonly ResourceRepository resourceRepository;
        public ResourcesController(ResourceRepository resourceRepository) : base(resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }
    }
}
