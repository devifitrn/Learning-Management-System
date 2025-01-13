using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubContentsController : BaseController<SubContent, SubContentRepository, int>
    {
        private readonly SubContentRepository subContentRepository;
        public SubContentsController(SubContentRepository subContentRepository) : base(subContentRepository)
        {
            this.subContentRepository = subContentRepository;
        }
    }
}
