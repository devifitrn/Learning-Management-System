using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : BaseController<Content, ContentRepository, int>
    {
        private readonly ContentRepository contentRepository;
        public ContentsController(ContentRepository contentRepository) : base(contentRepository)
        {
            this.contentRepository = contentRepository;
        }
    }
}
