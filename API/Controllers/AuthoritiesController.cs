using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoritiesController : BaseController<Authority, AuthorityRepository, int>
    {
        private readonly AuthorityRepository authorityRepository;
        public AuthoritiesController(AuthorityRepository authorityRepository) : base(authorityRepository)
        {
            this.authorityRepository = authorityRepository;
        }
    }
}
